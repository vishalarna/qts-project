import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Organization } from '@models/Organization/Organization';
import { CreatePublicClassScheduleRequestModel } from '@models/PublicClasses/CreatePublicClassScheduleRequestModel';
import { Store } from '@ngrx/store';
import { PublicClassScheduleRequestService } from 'src/app/_Services/QTD/public-class-schedule-request.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-fly-panel-registration-page',
  templateUrl: './fly-panel-registration-page.component.html',
  styleUrls: ['./fly-panel-registration-page.component.scss']
})
export class FlyPanelRegistrationPageComponent implements OnInit {

  processing = false;
  registrationForm: UntypedFormGroup;
  organizations: Organization[] = [];
  nercCerts: any[] = [];
  @Input() instanceName: string;
  publicUrl: string;
  certFilled: boolean;
  expiryFilled: boolean;
  certTypeFilled: boolean;
  disableSave: boolean = false;
  @Input() classId!: string;
  @Input() classDetails: any;
  organizationPlaceholder: string;
  loader: boolean = false;

  constructor(private store: Store<{ toggle: string }>,
    private fb: UntypedFormBuilder,
    public flyPanelSrvc: FlyInPanelService,
    private publicClassScheduleRequestService: PublicClassScheduleRequestService,
    private alert: SweetAlertService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen());
    this.initializeRegistrationForm();
    this.readyPublicOrgs();
    this.readyNercCerts();
  }

  initializeRegistrationForm() {
    this.registrationForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      emailAddr: ['', [Validators.required, Validators.email, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
      org: [''],
      nerccert: ['', [this.whitespaceOnlyValidator]],
      expiryDate: [''],
      nerccerttype: [''],
    });
    this.disableSavefunction();
  }

  async readyPublicOrgs() {
    this.organizations = await this.publicClassScheduleRequestService.getAllPublicOrganization(this.instanceName);
    this.organizationPlaceholder = this.organizations.length > 0 ? 'Select Organization' : 'No Public Organization Available'
  }

  async readyNercCerts() {
    this.nercCerts = await this.publicClassScheduleRequestService.getNercCertList(this.instanceName)
  }
  async AddPublicClassSchedule() {
    if (this.registrationForm.invalid) {
      this.alert.warningAlert('Incomplete Data');
      return;
    }
    let createOpt: CreatePublicClassScheduleRequestModel = {
      firstName: this.registrationForm.get('firstName')?.value,
      lastName: this.registrationForm.get('lastName')?.value,
      emailAddress: this.registrationForm.get('emailAddr')?.value,
      company: this.registrationForm.get('org')?.value.trim() || null,
      nercCertNumber: this.registrationForm.get('nerccert')?.value,
      nercCertExpiration: this.registrationForm.get('expiryDate')?.value,
      nercCertType: this.registrationForm.get('nerccerttype')?.value
    }
    this.processing = true;
    try {
      await this.publicClassScheduleRequestService.createpublicClassScheduleRequestAsync(this.instanceName, this.classId, createOpt);
      this.alert.successAlert('Registration Submitted', 'Thank you for registering. Your portal access request has been received and is currently under review. A notification will be sent to the email address provided, informing you of the approval status once the review is complete.');
    }
    catch (error) {
      if (
        error?.includes('Valued Member of our Training Community')
      ) {
        this.alert.errorAlertRedirect(error).then((result) => {
          if (result.isConfirmed) {
            this.router.navigate(['/auth/login']);
          }
        });
      } else {
        this.alert.errorAlert(error);
      }
    }
    finally {
      this.processing = false;
      this.flyPanelSrvc.close()
    }
  }

  whitespaceOnlyValidator(control) {
    const val: string = control.value || '';
    if (val.length === 0) {
      return null;
    }
    return val.trim().length > 0
      ? null
      : { whitespaceOnly: true };
  }

  disableSavefunction() {
    this.registrationForm.valueChanges.subscribe(_ => {
      const certVal = this.registrationForm.get('nerccert')?.value?.toString().trim();
      const expiryVal = this.registrationForm.get('expiryDate')?.value;
      const typeVal = this.registrationForm.get('nerccerttype')?.value?.toString().trim();
      this.certFilled = !!certVal;
      this.expiryFilled = !!expiryVal;
      this.certTypeFilled = !!typeVal;
      const anyFieldFilled = this.certFilled || this.expiryFilled || this.certTypeFilled;
      const allFieldFilled = this.certFilled && this.expiryFilled && this.certTypeFilled;
      this.disableSave = anyFieldFilled && !allFieldFilled;
    });
  }

  onSubmit() {
    this.AddPublicClassSchedule();
  }

  clearSelection(name: string) {
    this.registrationForm.get(name)?.setValue(null);
  }
}
