import { Procedure } from './../../../../../_DtoModels/Procedure/Procedure';
import { ProceduresService } from './../../../../../_Services/QTD/procedures.service';
import { loggedInReducer } from './../../../../../_Statemanagement/reducer/state.signInReducer';
import { DatePipe, formatDate } from '@angular/common';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewContainerRef,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { Procedure_IssuingAuthorityCreateOptions } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthorityCreateOptions';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { ProcedureCreateOptions } from 'src/app/_DtoModels/Procedure/ProcedureCreateOptions';
import { Router } from '@angular/router';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'fly-panel-add-issuing-authority',
  templateUrl: './fly-panel-add-issuing-authority.component.html',
  styleUrls: ['./fly-panel-add-issuing-authority.component.scss'],
})
export class FlyPanelAddIssuingAuthorityComponent implements OnInit, AfterViewInit {
  @Input() oldIssuingAuthority: Procedure_IssuingAuthority;
  @Input() isCopy: boolean;
  @Input() iaCheck:boolean;
  @Input() shouldNavigate = false;
  isEdit = false;
  showSpinner = false;
  addAnotherIssuingAuthority: boolean = false;
 /*  dateError = false; */
  IssuingAuthorityTitle: any;
  issuingAuthorityForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  iADescription = "";
  iaNote = "";
  procedureArray : any [] = [];

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private proc_issueAuthService: IssuingAuthoritiesService,
    private procedures : ProceduresService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    if (this.oldIssuingAuthority !== undefined && !this.isCopy) {
      this.isEdit = true;
      this.readyIssuingAuthorityFormWithData();
    }
    else if (this.oldIssuingAuthority && this.isCopy) {
      this.isEdit = false;
      this.getProceduresById(this.oldIssuingAuthority.id);
      this.readyIssuingAuthorityFormWithData();
    }
    else {
      this.isEdit = false;
      this.readyIssuingAuthorityForm();
    }
  }

  ngAfterViewInit(): void {

  }

  closeIssuingAuthority() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null; 
    } else {
      return { whitespaceOnly: true }; 
    }
  }

  getProceduresById(id:any){
    this.proc_issueAuthService.getAll().then((res)=>{
      res.forEach((i)=>{
        if(i.id === id){
          this.procedureArray.push(i);
        }
      })
    }).catch(async (err)=>{
      this.alert.errorToast('Error fetching ' + await this.transformTitle('Procedure') + 's Issuing Authority');
    });
  }

  saveissuingAuthority() {
    if (!this.isEdit || this.isCopy) {
      this.saveData();
    }
    else {
      this.updateData();
    }
  }

  saveData() {
    
    this.showSpinner = true;
    var data = new Procedure_IssuingAuthorityCreateOptions();
    data.description = this.issuingAuthorityForm.get('IssuingAuthorityDescription')?.value;
    data.title = this.issuingAuthorityForm.get('IssuingAuthorityTitle')?.value;
    if (this.isCopy) {
      data.title = data.title.trim().toLowerCase() === this.oldIssuingAuthority.title.trim().toLowerCase()
        ? data.title.concat("-Copy"):data.title;
    }
    data.isActive = true;
    data.isDeleted = false;
    data.notes = this.issuingAuthorityForm.get("Note")?.value;
    data.website = this.issuingAuthorityForm.get("IssuingAuthorityWebsite")?.value;
    data.effectiveDate = this.issuingAuthorityForm.get("EffectiveDate")?.value;
    this.proc_issueAuthService.create(data).then(async (res: any) => {
      this.showSpinner = false;
       if(this.isCopy){
        this.alert.successToast(await this.transformTitle('Procedure') + " Issuing Authority Copied");
        this.router.navigate([`/my-data/procedures/ia/${res.id}`]);
        this.dataBroadcastService.updateMyDataNavBar.next(null);
        this.dataBroadcastService.navigateOnChange.next({type:"ia",data:res});
       // this.copyProcedure();
      }
      else{
        this.alert.successToast(await this.transformTitle('Procedure') + " Issuing Authority Saved");
      }
     // this.isCopy ? this.alert.successToast("Procedure Issuing Authority Copied") : this.alert.successToast("Procedure Issuing Authority Saved");
    //  this.checkAddAnother();
    if (this.issuingAuthorityForm.get("AddAnother")?.value && !this.isCopy) {
      this.issuingAuthorityForm.reset();
      this.issuingAuthorityForm.get("EffectiveDate")?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      this.issuingAuthorityForm.get("Note")?.setValue("");
      this.issuingAuthorityForm.get("IssuingAuthorityWebsite")?.setValue("");
      /* this.dateError = false; */
    }
    else {
      this.closed.emit('IA_Proc closed');
      if(this.iaCheck){
        this.router.navigate([`/my-data/procedures/ia/${res.id}`]);
      }
    }
    if(this.shouldNavigate){
      this.dataBroadcastService.navigateOnChange.next({type:"ia",data:res});
    }
    else{
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    }
      //this.dataBroadcastService.updateProcedureInNavBar.next();
    }).finally(() => {
      this.showSpinner = false;
    });
  }

  /* copyProcedure(){
    
    var data = new ProcedureCreateOptions();
    data.issuingAuthorityId = this.procedureArray[0].id;
    this.procedureArray[0]['procedures'].forEach(async (res:any) => {

      data.number = res.number.concat("-Copy");
      data.title = res.title.concat("-Copy");
      data.description = res.description;
      data.effectiveDate = res.effectiveDate;
      data.isDeleted = res.isDeleted;
      data.isActive = res.isActive;
      data.isPublished = res.isPublished;
      data.proceduresFileUpload = res.proceduresFileUpload;
      data.revisionNumber = res.revisionNumber;
      data.hyperlink = res.hyperlink;

      await this.procedures.create(data).then((res)=>{
        this.alert.successToast('Procedures Copied Successfully');
      }).catch((err)=>{
        this.alert.errorToast(err);
      });
    });


  } */

  updateData() {
    this.showSpinner = true;
    var data = new Procedure_IssuingAuthorityCreateOptions();
    data.description = this.issuingAuthorityForm.get('IssuingAuthorityDescription')?.value;
    data.title = this.issuingAuthorityForm.get('IssuingAuthorityTitle')?.value;
    data.isActive = true;
    data.isDeleted = false;
    data.notes = this.issuingAuthorityForm.get("Note")?.value;
    data.website = this.issuingAuthorityForm.get("IssuingAuthorityWebsite")?.value;
    data.effectiveDate = this.issuingAuthorityForm.get("EffectiveDate")?.value;
    this.proc_issueAuthService.update(this.oldIssuingAuthority.id, data).then(async (res: any) => {
      this.showSpinner = false;
      this.alert.successToast(await this.transformTitle('Procedure') + " Issuing Authority Updated Successfully");
      this.dataBroadcastService.updateProcIAData.next(null);
      this.dataBroadcastService.updateProcedureInNavBar.next(null);
      this.flyPanelSrvc.close();
    }).catch(async (err: any) => {
      this.showSpinner = false;
      this.alert.errorToast("Error Updating " + await this.transformTitle('Procedure') + " Issuing Authority It exists with same title already");
    })
  }

  checkAddAnother() {
    if (this.issuingAuthorityForm.get("AddAnother")?.value && !this.isCopy) {
      this.issuingAuthorityForm.reset();
      this.issuingAuthorityForm.get("EffectiveDate")?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      this.issuingAuthorityForm.get("Note")?.setValue("");
      this.issuingAuthorityForm.get("IssuingAuthorityWebsite")?.setValue("");
      /* this.dateError = false; */
    }
    else {
      this.closed.emit('IA_Proc closed');
    }
  }

  readyIssuingAuthorityFormWithData() {
    this.issuingAuthorityForm = this.fb.group({
      IssuingAuthorityTitle: new UntypedFormControl(this.oldIssuingAuthority.title, [
        Validators.required,
      ]),
      IssuingAuthorityDescription: new UntypedFormControl(this.oldIssuingAuthority.description),
      IssuingAuthorityWebsite: new UntypedFormControl(this.oldIssuingAuthority.website),
      /* EffectiveDate: new FormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")), */
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(this.oldIssuingAuthority.effectiveDate, "yyyy-MM-dd")),
      Note: new UntypedFormControl(this.oldIssuingAuthority.notes,[
        Validators.required,
      ]),
      AddAnother: new UntypedFormControl(false),
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

  readyIssuingAuthorityForm() {
    this.issuingAuthorityForm = this.fb.group({
      IssuingAuthorityTitle: new UntypedFormControl(this.IssuingAuthorityTitle,[Validators.required,this.whitespaceOnlyValidator]),
      IssuingAuthorityDescription: new UntypedFormControl(''),
      IssuingAuthorityWebsite: new UntypedFormControl(''),
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      Note: new UntypedFormControl('',[Validators.required,this.whitespaceOnlyValidator]),
      AddAnother: new UntypedFormControl(false),
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
