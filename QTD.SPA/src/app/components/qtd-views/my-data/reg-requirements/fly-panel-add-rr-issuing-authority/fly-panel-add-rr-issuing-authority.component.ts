import { DatePipe, formatDate } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RRIssuingAuthority } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthority';
import { RRIssuingAuthorityCreateOptions } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-rr-issuing-authority',
  templateUrl: './fly-panel-add-rr-issuing-authority.component.html',
  styleUrls: ['./fly-panel-add-rr-issuing-authority.component.scss']
})
export class FlyPanelAddRRIssuingAuthorityComponent implements OnInit, AfterViewInit {
  @Input() oldIssuingAuthority: RRIssuingAuthority;
  @Input() makeCopy: boolean;
  @Input() iaRRCheck:boolean;
  showSpinner = false;
  /* dateError = false; */
  datePipe = new DatePipe('en-us');
  addAnotherIssuingAuthority: boolean = false;
  IssuingAuthorityTitle: any;
  issuingAuthorityForm: UntypedFormGroup = new UntypedFormGroup({});
  @Input() shouldNavigate = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refreshRRIA = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private rrIAService: RRIssuingAuthorityService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    if (this.oldIssuingAuthority !== undefined || this.makeCopy) {
      this.readyFormWithData();
    }
    else {
      this.readyIssuingAuthorityForm();
    }
  }

  ngAfterViewInit(): void {

  }

  readyFormWithData() {
    this.issuingAuthorityForm = this.fb.group({
      IssuingAuthorityTitle: new UntypedFormControl(this.oldIssuingAuthority.title, [
        Validators.required,
      ]),
      IssuingAuthorityDescription: new UntypedFormControl(this.oldIssuingAuthority.description),
      IssuingAuthorityWebsite: new UntypedFormControl(this.oldIssuingAuthority.website),
     /*  Note: new FormControl(this.oldIssuingAuthority.notes, [
        Validators.required,
      ]), */
      date: new UntypedFormControl(this.datePipe.transform(this.oldIssuingAuthority.effectiveDate, "yyyy-MM-dd")),
      isAdd: new UntypedFormControl(false),
    });
  }

  closeIssuingAuthority() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }

  async saveissuingAuthority() {
    this.showSpinner = true;
    var options = new RRIssuingAuthorityCreateOptions();
    options.description = this.issuingAuthorityForm.get("IssuingAuthorityDescription")?.value;
    options.notes = this.issuingAuthorityForm.get("Note")?.value;
    options.effectiveDate = this.issuingAuthorityForm.get("date")?.value;
    options.title = this.issuingAuthorityForm.get("IssuingAuthorityTitle")?.value;
    if (this.makeCopy) {
      options.title = options.title.trim().toLowerCase() === this.oldIssuingAuthority.title.trim().toLowerCase()
        ? options.title.concat("-Copy") : options.title;
    }
    options.website = this.issuingAuthorityForm.get("IssuingAuthorityWebsite")?.value;
    await this.rrIAService.create(options).then(async (res: any) => {
      if(this.makeCopy){
        this.dataBroadcastService.navigateOnChange.next({type:"iarr",data:res});
        this.dataBroadcastService.updateMyDataNavBar.next(null);
        this.closed.emit('IA_Proc closed');
      }
      if (this.issuingAuthorityForm.get('isAdd')?.value) {
        this.issuingAuthorityForm.reset();
      }
      else {
        this.closed.emit('IA_Proc closed');
        if(this.iaRRCheck){
          this.router.navigate([`/my-data/reg-requirements/ia/${res.id}`]);
        }
      }
    
    if(this.shouldNavigate){
      this.dataBroadcastService.navigateOnChange.next({type:"iarr",data:res});
    }
    else{
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    }
      this.showSpinner = false;
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + " Issuing Authority Created");
      //this.dataBroadcastService.updateRRIA.next();
      this.refreshRRIA.emit();
    }).finally(() => {
      this.showSpinner = false;
    });
  }

  readyIssuingAuthorityForm() {
    this.issuingAuthorityForm = this.fb.group({
      IssuingAuthorityTitle: new UntypedFormControl(this.IssuingAuthorityTitle, [
        Validators.required,
      ]),
      IssuingAuthorityDescription: new UntypedFormControl(''),
      IssuingAuthorityWebsite: new UntypedFormControl(''),
      Note: new UntypedFormControl('',[
        Validators.required,
      ]),
      date: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      isAdd: new UntypedFormControl(false),
    });
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  } */

  async updateIssuingAuthority() {
    this.showSpinner = true;
    var options = new RRIssuingAuthorityCreateOptions();
    options.description = this.issuingAuthorityForm.get("IssuingAuthorityDescription")?.value;
    options.notes = this.issuingAuthorityForm.get("Note")?.value;
    options.effectiveDate = this.issuingAuthorityForm.get("date")?.value;
    options.title = this.issuingAuthorityForm.get("IssuingAuthorityTitle")?.value;
    options.website = this.issuingAuthorityForm.get("IssuingAuthorityWebsite")?.value;
    await this.rrIAService.update(this.oldIssuingAuthority.id, options).then(async (res: RRIssuingAuthority) => {
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + " Issuing Authority Updated Successfully");
      this.showSpinner = false;
      this.dataBroadcastService.updateRRIA.next(res);
      this.dataBroadcastService.updateRR.next(res);
      this.closed.emit('IA_Proc closed');
    }).catch(async (err: any) => {
      this.showSpinner = false;
      this.alert.errorToast("Error Updating " + await this.labelPipe.transform('Regulatory Requirement') + " Issuing Authority " + err?.toString());
    })
  }

  copyIssuingAuthority() {
    this.saveissuingAuthority();
  }
}
