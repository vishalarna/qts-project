import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe, formatDate } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Procedure_RegulatoryRequirement_LinkOptions } from 'src/app/_DtoModels/Procedure_RegulatoryRequirement_Link/Procedure_RegulatoryRequirement_LinkOptions';
import { RegulatoryRequirement } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RRCreateOptions } from 'src/app/_DtoModels/RegulatoryRequirements/RR_CreateOptions';
import { RRIssuingAuthority } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthority';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-add-rr',
  templateUrl: './fly-panel-add-rr.component.html',
  styleUrls: ['./fly-panel-add-rr.component.scss'],
})
export class FlyPanelAddRRComponent implements OnInit, OnDestroy, AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refreshRRData = new EventEmitter<any>();

  @Input() rrCheck:boolean;
  @Input() oldRRData: RegulatoryRequirement;
  @Input() makeCopy: boolean;
  @Input() oldId: any;

  @ViewChild(MatStepper) stepper: MatStepper;
  @Input() shouldNavigate = false;

  showSpinner: boolean = false;
  stepperOrientation: Observable<StepperOrientation>;
  addRR: boolean = true;
  addIA: boolean = false;
  step1Form: UntypedFormGroup;
  step2Form: UntypedFormGroup;
  step3Form: UntypedFormGroup;
  issuingAuthorities: any[] = [];
  iAId = "";
  file: File;
  fileAsString = "";
  procId = "";
/*   dateError = false; */
  subscriptions = new SubSink();
  fileUploaded = false;
  fileName = "";
  fileData = "";
  datePipe = new DatePipe('en-us');

  constructor(
    private fb: UntypedFormBuilder,
    public breakpointObserver: BreakpointObserver,
    private rrIAService: RRIssuingAuthorityService,
    private alert: SweetAlertService,
    private rrService: RegulatoryRequirementService,
    private procService: ProceduresService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe: LabelReplacementPipe
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.readyStep1Form();
    this.readyStep2Form();
    this.readyStep3Form();
    if (this.oldId !== undefined || this.makeCopy) {
      this.getData();
    }
  }

  ngAfterViewInit(): void {
    this.readyIssuingAuthorities();
    this.getProcedureId();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  async getData() {
    await this.rrService.get(this.oldId).then((res: RegulatoryRequirement) => {
      let temp = new Blob([res.uploads], { type: 'application/pdf' });
      if (temp.size > 0) {
        this.file = new File([temp], res.fileName);
        var reader = new FileReader();
        reader.readAsDataURL(this.file);
        reader.onloadend = () => {
          this.fileData = reader.result?.toString() ?? "";
          this.fileName = this.file.name;
          this.fileUploaded = true;
          this.step2Form.get('hyperlink')?.setValue("");
        }
      }
      this.oldRRData = res;
      this.readyFormsWithData();
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching " + await this.labelPipe.transform('Regulatory Requirement') + "s Data");
    })
  }

  readyFormsWithData() {
    this.step1Form.get('iaId')?.setValue(this.oldRRData.issuingAuthorityId);
    this.step2Form.get('number')?.setValue(this.oldRRData.number);
    this.step2Form.get('revNumber')?.setValue(this.oldRRData.revisionNumber);
    this.step2Form.get('name')?.setValue(this.oldRRData.title);
    this.step2Form.get('desc')?.setValue(this.oldRRData.description);
    if(this.oldRRData.effectiveDate){
      this.step3Form.get('effectiveDate')?.setValue(this.datePipe.transform( this.oldRRData.effectiveDate,'yyyy-MM-dd'));
    }else{
      this.step3Form.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), [Validators.required]);
    }

  }

  getProcedureId() {
    this.subscriptions.sink = this.route.params.subscribe((res: any) => {
      this.procId = res.id;
    });
  }

  iaLoader = false;
  async readyIssuingAuthorities() {
    this.iaLoader = true;
    await this.rrIAService.getAllWithoutIncludes().then((res: RRIssuingAuthority[]) => {
      this.issuingAuthorities = res;
    }).finally(()=>{
      this.iaLoader = false;
    })
  }

  readyStep1Form() {
    this.step1Form = this.fb.group({
      iaId: new UntypedFormControl({ value: '', disabled: (this.oldId !== undefined || this.makeCopy) }, [Validators.required]),
    });
  }

  readyStep2Form() {
    this.step2Form = this.fb.group({
      number: new UntypedFormControl('', [Validators.required]),
      revNumber: new UntypedFormControl(),
      name: new UntypedFormControl('', [Validators.required]),
      desc: new UntypedFormControl(''),
      hyperlink: new UntypedFormControl(),
    });
  }

  readyStep3Form() {
    this.step3Form = this.fb.group({
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), [Validators.required]),
      reason: new UntypedFormControl('', Validators.required),
      AddAnother: new UntypedFormControl(false),
    });
  }

  closeIAPanel() {
    this.addIA = false;
    this.addRR = true;
  }

  openIAPanel() {
    this.addIA = true;
    this.addRR = false;
  }

  saveRRData() {
    this.showSpinner = true;
    var options = new RRCreateOptions();
    options.description = this.step2Form.get("desc")?.value;
    options.effectiveDate = this.step3Form.get("effectiveDate")?.value;
    options.changeNotes = this.step3Form.get("reason")?.value;
    options.issuingAuthorityId = this.iAId;
    options.number = this.step2Form.get("number")?.value;
    options.revisionNumber = this.step2Form.get("revNumber")?.value;
    options.title = this.step2Form.get("name")?.value;
    if (this.makeCopy) {
      options.title = options.title.trim().toLowerCase() === this.oldRRData.title.trim().toLowerCase() ?
        options.title.concat('-Copy') : options.title;
      options.number = options.number.trim().toLowerCase() === this.oldRRData.number.trim().toLowerCase() ?
        options.number.concat('-Copy') : options.number;

      options.issuingAuthorityId = this.oldRRData.issuingAuthorityId;
    }
    if (this.fileUploaded) {
      options.file = this.fileData;
      options.fileName = this.fileName;
    }
    else{
      options.hyperLink = this.step2Form.get('hyperlink')?.value;
    }
    this.rrService.create(options).then(async (res: any) => {
      if (this.step3Form.get("AddAnother")?.value) {
        this.step1Form.reset();
        this.step2Form.reset();
        this.step3Form.reset();
        this.step3Form.get("effectiveDate")?.setValue(formatDate(Date.now(), 'yyyy-MM-dd', 'en'));
        this.fileData = "";
        this.fileName = "";
        this.fileUploaded = false;
        this.stepper.reset();
      }
      else {
        this.closed.emit('fp-add-RR-closed');
        if(this.rrCheck){
          this.router.navigate([`/my-data/reg-requirements/rr/${res.id}`]);
        }
      }
      if(this.shouldNavigate){
        this.dataBroadcastService.navigateOnChange.next({type:"rr",data:res});
      }
      else{
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + " Created");
     // this.dataBroadcastService.updateRR.next();
      this.refreshRRData.emit();
      this.showSpinner = false;
    }).catch(async (err: any) => {
      this.showSpinner = false;
      this.alert.errorToast("Error Saving " + await this.labelPipe.transform('Regulatory Requirement') + " : Number Already Exists");
    })
  }

  selectIAId(id: any) {
    this.iAId = id;
  }

  fileChange(file: any) {
    if (!file[0].type.toLowerCase().includes("application/pdf")) {
      this.alert.errorToast("Please Upload a valid pdf file");
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL(file[0]);
    reader.onloadend = () => {
      this.fileData = reader.result?.toString() ?? "";
      this.fileName = file[0].name;
      this.fileUploaded = true;
      this.step2Form.get('hyperlink')?.setValue("");
    }
  }

  refreshRRIAData() {
    this.refreshRRData.emit();
    this.readyIssuingAuthorities();
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  } */

  removeFile() {
    this.fileName = "";
    this.fileData = "";
    this.fileUploaded = false;
  }

  updateRRData() {
    this.showSpinner = true;
    var options = new RRCreateOptions();
    options.description = this.step2Form.get("desc")?.value;
    options.effectiveDate = this.step3Form.get("effectiveDate")?.value;
    options.changeNotes = this.step3Form.get("reason")?.value;
    options.issuingAuthorityId = this.oldRRData.issuingAuthorityId;
    options.number = this.step2Form.get("number")?.value;
    options.revisionNumber = this.step2Form.get("revNumber")?.value;
    options.title = this.step2Form.get("name")?.value;
    if (this.fileUploaded) {
      options.file = this.fileData;
      options.fileName = this.fileName;
    }
    else{
      options.hyperLink = this.step2Form.get('hyperlink')?.value;
    }
    this.rrService.update(this.oldId, options).then(async (res: RegulatoryRequirement) => {
      this.showSpinner = false;
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + " Updated");
      this.dataBroadcastService.updateRR.next({ id: res.id });
      this.closed.emit('fp-add-RR-closed');
    }).catch(async (err: any) => {
      this.showSpinner = false;
      this.alert.errorToast("Error Updating " + await this.labelPipe.transform('Regulatory Requirement') + "s Data : " + await this.labelPipe.transform('Regulatory Requirement') + " ID Already Exists");
    })
  }

  copyRRData() {

    this.showSpinner = true;
    var options = new RRCreateOptions();
    options.description = this.step2Form.get("desc")?.value;
    options.effectiveDate = this.step3Form.get("effectiveDate")?.value;
    options.changeNotes = this.step3Form.get("reason")?.value;
    options.issuingAuthorityId = this.iAId;
    options.number = this.step2Form.get("number")?.value;
    options.revisionNumber = this.step2Form.get("revNumber")?.value;
    options.title = this.step2Form.get("name")?.value;
      options.title = options.title.trim().toLowerCase() === this.oldRRData.title.trim().toLowerCase() ?
        options.title.concat('-Copy') : options.title;
      options.number = options.number.trim().toLowerCase() === this.oldRRData.number.trim().toLowerCase() ?
        options.number.concat('-Copy') : options.number;

      options.issuingAuthorityId = this.oldRRData.issuingAuthorityId;
    if (this.fileUploaded) {

      options.file = this.fileData;
      options.fileName = this.fileName;
    }
    else{
      options.hyperLink = this.step2Form.get('hyperlink')?.value;
    }
    this.rrService.copy(this.oldId,options).then(async (res: any) => {
     /*  if (this.step3Form.get("AddAnother")?.value) {
        this.step1Form.reset();
        this.step2Form.reset();
        this.step3Form.reset();
        this.step3Form.get("effectiveDate")?.setValue(formatDate(Date.now(), 'yyyy-MM-dd', 'en'));
        this.fileData = "";
        this.fileName = "";
        this.fileUploaded = false;
        this.stepper.reset();
      }
      else { */
        this.closed.emit('fp-add-RR-closed');
        this.router.navigate([`/my-data/reg-requirements/rr/${res.id}`]);
        this.dataBroadcastService.navigateOnChange.next({type:"rr",data:res});
     /*  } */
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + " Copied");
      this.dataBroadcastService.updateRR.next(null);
      this.refreshRRData.emit();
      this.showSpinner = false;
    }).catch(async (err: any) => {
      this.showSpinner = false;
      this.alert.errorToast("Error Saving " + await this.labelPipe.transform('Regulatory Requirement') + " : Number Already Exists");
    })

  }

}
