import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CertifyingBody } from 'src/app/_DtoModels/CertifyingBody/CertifyingBody';
import { CertifyingBodiesService } from 'src/app/_Services/QTD/certifying-bodies.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-issuingauthority',
  templateUrl: './fly-panel-add-issuingauthority.component.html',
  styleUrls: ['./fly-panel-add-issuingauthority.component.scss'],
})

export class FlyPanelAddIssuingAuthorityComponent implements OnInit {
  @Input() mode: 'Add' | 'Edit' | 'Copy' = 'Add';
  @Input() oldIssuingAuthority: any;
  @Input() isCopy: any;
  @Input() shouldNavigate = false;
  @Input() iaCheck:boolean;
  isEdit = false;
  showSpinner = false;
  AddAnotherIssuingAuthoriy: boolean = false;
  dateError = false;
  issuingAuthorityTitle: any;
  issuingAuthorityForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  issuingAuthorityDescription = '';
  issuingAuthorityNote = '';
  isNERCBool: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private certBodyService: CertifyingBodiesService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router
  ) {}

  ngOnInit(): void {
    


    this.readyIAForm();
    if (this.oldIssuingAuthority !== undefined) {
      this.insertDataIAForm();
    }
    this.isEdit = false;
  }


  ngAfterViewInit(): void {

  }

  closeLocation() {

    this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }


 /*  dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  } */

  readyIAForm() {
    this.issuingAuthorityForm = this.fb.group({
      issuingAuthorityTitle: new UntypedFormControl(this.issuingAuthorityTitle, [Validators.required,this.whitespaceOnlyValidator]),
      issuingAuthorityDescription: new UntypedFormControl(''),
      issuingAuthorityWebsite: new UntypedFormControl(''),
      EffectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      reason: new UntypedFormControl('',[Validators.required,this.whitespaceOnlyValidator]),
      AddAnother: new UntypedFormControl(false)
    });
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null; 
    } else {
      return { whitespaceOnly: true }; 
    }
  }


  async createNewIssuingAuthority() {
    this.showSpinner = true;
    
    var options = new CertifyingBody();
    options.desc = this.issuingAuthorityForm.get("issuingAuthorityDescription")?.value;
    options.name = this.issuingAuthorityForm.get("issuingAuthorityTitle")?.value;
    options.EffectiveDate = this.issuingAuthorityForm.get('EffectiveDate')?.value;
    options.isNERC = this.isNERCBool;
    if (this.isCopy) {
      options.name = this.oldIssuingAuthority.name.trim().toLowerCase() == options.name.trim().toLowerCase()
        ? options.name + ("-Copy") : options.name;
    }


    options.website = this.issuingAuthorityForm.get("issuingAuthorityWebsite")?.value;
    options.notes = this.issuingAuthorityForm.get("reason")?.value;
    await this.certBodyService.create(options).then((res: CertifyingBody) => {
      
      if (this.issuingAuthorityForm.get('AddAnother')?.value) {
        this.issuingAuthorityForm.reset();
        this.issuingAuthorityForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else{
        if(this.iaCheck){
          this.router.navigate([`/my-data/certifications/issuingauthority/${res.id}`]);
        }
        this.closed.emit('fp-add-sh-cat-closed');
        this.refresh.emit();
      }

      if(this.shouldNavigate){
        this.dataBroadcastService.navigateOnChange.next({type:"ia",data:res});
      }
      else{
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }
   /*    else
      {
        this.closed.emit('fp-add-sh-cat-closed');
      } */
    /*   this.refresh.emit(); */
      this.alert.successToast(`Successfull ${this.isCopy ? "Copied":"Created"} Issuing Authority `);

    }).finally(()=>{
      this.showSpinner = false;
    })
  }

  insertDataIAForm() {
    this.issuingAuthorityForm
      .get('issuingAuthorityTitle')
      ?.setValue(this.oldIssuingAuthority.name);
    this.issuingAuthorityForm
      .get('issuingAuthorityDescription')
      ?.setValue(this.oldIssuingAuthority.desc);
    this.issuingAuthorityForm
      .get('issuingAuthorityWebsite')
      ?.setValue(this.oldIssuingAuthority.website);
    this.issuingAuthorityForm
      .get('EffectiveDate')
      ?.setValue(
        this.datePipe.transform(
          Date.now(),
          'yyyy-MM-dd'
        )
      );
    this.issuingAuthorityForm.get('AddAnother')?.setValue(false);
    this.isNERCBool = this.oldIssuingAuthority.isNERC;
  }

  updateissuingAuthority() {
    
    this.showSpinner = true;

    var options = {
      name: this.issuingAuthorityForm.get('issuingAuthorityTitle')?.value,
      desc: this.issuingAuthorityForm.get('issuingAuthorityDescription')
        ?.value,
      website: this.issuingAuthorityForm.get('issuingAuthorityWebsite')?.value,
      EffectiveDate: this.issuingAuthorityForm.get('EffectiveDate')?.value,
      notes: this.issuingAuthorityForm.get('reason')?.value,
      isNERC:this.isNERCBool
    };

    this.certBodyService
      .update(this.oldIssuingAuthority.id, options)
      .then((res: any) => {
        this.closed.emit('fp-add-ins-cat-closed');
        this.refresh.emit();
        this.alert.successToast('Successfully Updated Issuing Authority');
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }


}
