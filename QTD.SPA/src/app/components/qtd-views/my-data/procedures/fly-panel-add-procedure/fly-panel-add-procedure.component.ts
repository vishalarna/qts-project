import { BreakpointObserver } from '@angular/cdk/layout';
import { TemplatePortal } from '@angular/cdk/portal';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe, formatDate } from '@angular/common';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProcedureCreateOptions } from 'src/app/_DtoModels/Procedure/ProcedureCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-procedure',
  templateUrl: './fly-panel-add-procedure.component.html',
  styleUrls: ['./fly-panel-add-procedure.component.scss'],
})
export class FlyPanelAddProcedureComponent implements OnInit, AfterViewInit {
  @Input() oldProcedure: Procedure;
  @Input() isCopy: boolean;
  @Input() procedureCheck:boolean;
  isEdit: boolean = false;
  showSpinner = false;
  ProcedureTitle: UntypedFormControl;
  issuingAuthority_list: any[] = [];
  initialIssuingAuthorityList: any[] = [];
  ProcedureNumber: any;
  datePipe = new DatePipe('en-us');
  addAnotherProcedure: boolean = false;
  addProcedure: boolean = true;
  addIssuingAuthority: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Output() autoSelect = new EventEmitter<any>();
  @Input() shouldNavigate = false;
  stepperOrientation: Observable<StepperOrientation>;
  step1Form: UntypedFormGroup;
  step2Form: UntypedFormGroup;
  step3Form: UntypedFormGroup;
  getDateInput:any;

  @ViewChild(MatStepper) stepper: MatStepper;

  fileUploaded = false;
  fileName = '';
  fileData = '';
  isIssuingAuthorityDisabled: boolean = false;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    public breakpointObserver: BreakpointObserver,
    private proc_issueAuthService: IssuingAuthoritiesService,
    private procedureService: ProceduresService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe: LabelReplacementPipe,
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.getIssuingAuthorityList();
    this.readyFormsWithoutData();
    if (this.oldProcedure !== undefined) {
      this.readyFormsWithData();
    }
  }

  ngAfterViewInit(): void { }

  readyFormsWithData() {
    // this.step1Form = this.fb.group({
    //   IssuingAuthority: new FormControl(this.oldProcedure.issuingAuthorityId, [
    //     Validators.required,
    //   ]),
    // });
    this.step1Form.get('IssuingAuthority')?.setValue(this.oldProcedure.issuingAuthorityId);
    this.step2Form.get('ProcedureTitle')?.setValue(this.oldProcedure.title);
    this.step2Form.get('ProcedureDesc')?.setValue(this.oldProcedure.description);
    this.step2Form.get('number')?.setValue(this.oldProcedure.number);
    this.step2Form.get('revNumber')?.setValue(this.oldProcedure.revisionNumber);
    this.step2Form.get('hyperlink')?.setValue(this.oldProcedure.hyperlink);

    this.step3Form.get('RevisionNumber')?.setValue('');
    this.step3Form.get('EffectiveDate')?.setValue(this.datePipe.transform(this.oldProcedure.effectiveDate,'yyyy-MM-dd'));
    // this.step2Form = this.fb.group({
    //   ProcedureTitle: new FormControl(this.oldProcedure.title, [
    //     Validators.required,
    //   ]),
    //   ProcedureDesc: new FormControl(this.oldProcedure.description),
    //   number: new FormControl(this.oldProcedure.number, [Validators.required]),
    //   revNumber: new FormControl(this.oldProcedure.revisionNumber),
    //   hyperlink: new FormControl(),
    // });

    // this.step3Form = this.fb.group({
    //   RevisionNumber: new FormControl(''),
    //   EffectiveDate: new FormControl(
    //     this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
    //   ),
    // });

    this.fileData = this.oldProcedure.proceduresFileUpload;
      this.fileUploaded = this.fileData.length > 0;
      this.fileName = this.oldProcedure.fileName;
   
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null; 
    } else {
      return { whitespaceOnly: true }; 
    }
  }

  readyFormsWithoutData() {
    this.readyStep1Form();
    this.readyStep2Form();
    this.readyStep3Form();
  }

  async getIssuingAuthorityList() {
    await this.proc_issueAuthService
      .getAll()
      .then((res: any) => {
        if (res != null) {
          this.issuingAuthority_list = res;
          this.initialIssuingAuthorityList = Object.assign(this.issuingAuthority_list);
        }
      })
      .catch((err: any) => {
        this.alert.errorToast('Issuing Authority error');
      });
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  } */

  closeProcedure() {
    this.refresh.emit();
    if (this.step3Form.get('AddAnother')?.value) {
      this.step1Form.reset();
      this.step2Form.reset();
      this.step3Form.reset();
      this.fileData = "";
      this.fileName = "";
      this.fileUploaded = false;
      this.stepper.reset();
      this.step3Form
      .get('EffectiveDate')
      ?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
    } else {
      this.closed.emit('fp-add-proc-closed');
    }
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  selectIssuingAuthority() { }

  FlypanelClosed() {
    // Todo => get issuing authorities again here and refresh the dropdown
    this.addProcedure = true;
    this.addIssuingAuthority = false;
    this.refresh.emit();
    this.getIssuingAuthorityList();
  }

  keyPressNumbers(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
    // Only Numbers 0-9
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  selectedChanged(event: any) {
    if (event.selectedIndex === 1) {
    }
  }

  readyStep1Form() {
    this.step1Form = this.fb.group({
      IssuingAuthority: new UntypedFormControl('', [Validators.required]),
      searchTxt: new UntypedFormControl(''),
    });
  }

  readyStep2Form() {
    this.step2Form = this.fb.group({
      ProcedureTitle: new UntypedFormControl('',  [Validators.required,this.whitespaceOnlyValidator]),
      ProcedureDesc: new UntypedFormControl(''),
      number: new UntypedFormControl('',  [Validators.required,this.whitespaceOnlyValidator]),
      revNumber: new UntypedFormControl(),
      /* hyperlink: new FormControl('',[Validators.pattern('(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]?')]), */
      hyperlink: new UntypedFormControl(''),
    });
  }

  readyStep3Form() {
    this.step3Form = this.fb.group({
      RevisionNumber: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), [Validators.required]),
      AddAnother: new UntypedFormControl(false),
    });
  }

  saveLogic() {
    if (this.isEdit) {
      this.updateProcedure();
    } else {
      this.saveProcedure();
    }
  }

  saveProcedure() {
    this.showSpinner = true;
    let desc = " ";
    var data = new ProcedureCreateOptions();
    data.issuingAuthorityId = this.step1Form.get('IssuingAuthority')?.value;
    data.number = this.step2Form.get('number')?.value;
    data.title = this.step2Form.get('ProcedureTitle')?.value;
    /* if (this.isCopy) {
      data.title =
        data.title.trim().toLowerCase() ===
          this.oldProcedure.title.trim().toLowerCase()
          ? data.title.concat('-Copy')
          : data.title;
      data.number =
        data.number.trim().toLowerCase() ===
          this.oldProcedure.number.trim().toLowerCase()
          ? data.number.concat('-Copy')
          : data.number;
    } */
    data.description = this.step2Form.get('ProcedureDesc')?.value ?? desc;
    data.revisionNumber = this.step2Form.get('revNumber')?.value;
    data.effectiveDate = this.step3Form.get('EffectiveDate')?.value;
    data.changeNotes = this.step3Form.get('RevisionNumber')?.value;
    data.hyperlink = this.step2Form.get('hyperlink')?.value;
    data.isActive = true;
    data.isDeleted = false;
    data.isPublished = false;
    if (this.fileUploaded) {
      data.file = this.fileData;
      data.fileName = this.fileName;
    }
    this.procedureService
      .create(data)
      .then(async (res: any) => {
        this.showSpinner = false;
        this.dataBroadcastService.updateProcedureInNavBar.next(null);
        this.alert.successToast(await this.transformTitle('Procedure') + ' Saved');
        this.refresh.emit();
        if (this.step3Form.get('AddAnother')?.value) {
          this.step1Form.reset();
          this.step2Form.reset();
          this.step3Form.reset();
          this.fileData = "";
          this.fileName = "";
          this.fileUploaded = false;
          this.stepper.reset();
          this.step3Form
          .get('EffectiveDate')
          ?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
        } else {
          this.closed.emit('fp-add-proc-closed');
          if(this.procedureCheck){
            this.router.navigate([`/my-data/procedures/proc/${res.id}`]);
          }
        }
           // this.router.navigate([`/my-data/procedures/proc/${res.id}`]);
         /*  } */
        //this.closeProcedure();
        if(this.shouldNavigate){
          this.dataBroadcastService.navigateOnChange.next({type:"proc",data:res});
        }
        else{
          this.dataBroadcastService.updateMyDataNavBar.next(null);
        }
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  copyProcedure(){
    this.showSpinner = true;
    let desc = " ";
    var data = new ProcedureCreateOptions();
    data.issuingAuthorityId = this.step1Form.get('IssuingAuthority')?.value;
    data.number = this.step2Form.get('number')?.value;
    data.title = this.step2Form.get('ProcedureTitle')?.value;
    data.title = data.title.concat('-Copy');
    data.number = data.number.concat('-Copy');
    data.description = this.step2Form.get('ProcedureDesc')?.value;
    data.revisionNumber = this.step2Form.get('revNumber')?.value;
    data.effectiveDate = this.step3Form.get('EffectiveDate')?.value;
    data.changeNotes = this.step3Form.get('RevisionNumber')?.value;
    data.hyperlink = this.step2Form.get('hyperlink')?.value;
    data.isActive = true;
    data.isDeleted = false;
    data.isPublished = false;
    
    if (this.fileUploaded) {
      data.file = this.fileData;
      data.fileName = this.fileName;
    }
    this.procedureService
      .copy(this.oldProcedure.id,data)
      .then(async (res: any) => {
        this.showSpinner = false;
        this.dataBroadcastService.updateProcedureInNavBar.next(null);
         /*  if(this.isCopy){ */
            this.alert.successToast('Copy Of ' + await this.transformTitle('Procedure') + ' Saved');
            this.router.navigate([`/my-data/procedures/proc/${res.id}`]);
            this.dataBroadcastService.updateMyDataNavBar.next(null);
            this.dataBroadcastService.navigateOnChange.next({ type: "proc", data: res });
            this.autoSelect.emit(false);
         /*  }
          else if(!this.isCopy){
            this.alert.successToast('Procedure Saved');
            this.autoSelect.emit(true);
          } */
        this.closeProcedure();
       
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  updateProcedure() {
    let desc = " ";
    this.showSpinner = true;
    var data = new ProcedureCreateOptions();
    data.issuingAuthorityId = this.step1Form.get('IssuingAuthority')?.value;
    data.number = this.step2Form.get('number')?.value;
    data.title = this.step2Form.get('ProcedureTitle')?.value;
    data.description = this.step2Form.get('ProcedureDesc')?.value ?? desc;
    data.revisionNumber = this.step2Form.get('revNumber')?.value;
    data.effectiveDate = this.step3Form.get('EffectiveDate')?.value;
    data.changeNotes = this.step3Form.get('RevisionNumber')?.value;
    data.hyperlink = this.step2Form.get('hyperlink')?.value;
    data.isActive = true;
    data.isDeleted = false;
    data.isPublished = false;
    if (this.fileUploaded) {
      data.file = this.fileData;
      data.fileName = this.fileName;
    }
    this.procedureService
      .update(this.oldProcedure.id, data)
      .then(async (res: any) => {
        this.showSpinner = false;
        this.alert.successToast(await this.transformTitle('Procedure') + ' Successfully Updated');
        this.dataBroadcastService.updateProcedureInNavBar.next(null);
        this.dataBroadcastService.refreshProcedureData.next(null);
        this.closed.emit('fp-add-proc-closed');
      })
      .catch(async (err: any) => {
        this.showSpinner = false;
      });
  }

  fileChange(file: any) {
    if (!file[0].type.toLowerCase().includes('application/pdf')) {
      this.alert.errorToast('Please Upload a valid pdf file');
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL(file[0]);
    reader.onloadend = () => {
      this.fileData = reader.result?.toString() ?? '';
      this.fileName = file[0].name;
      this.fileUploaded = true;
      this.step2Form.get('hyperlink')?.setValue('');
    };
  }

  removeFile() {
    this.fileName = '';
    this.fileData = '';
    this.fileUploaded = false;
  }

 public issuingAuthoritySearch(value: any) {
    var filterString =this.step1Form.get('searchTxt')?.value;

    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    }
    else {
      filterString = "";
    }
    this.issuingAuthority_list = this.initialIssuingAuthorityList.filter((f) => {
      const title = `${f?.title}`;
      return title.toLowerCase().includes(filterString);
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
