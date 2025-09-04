import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Client } from '@models/Client/ClientViewModel';
import { IdentityProviderVM } from '@models/IdentityProvider/IdentityProviderVM';
import { Instance } from '@models/Instance/Instance';
import { InstanceCreateOptions } from '@models/Instance/InstanceCreateOptions';
import { InstanceUpdateOptions } from '@models/Instance/InstanceUpdateOptions';
import { IdentityProviderService } from 'src/app/_Services/Auth/identityProvider.service';
import { InstanceService } from 'src/app/_Services/Auth/instance.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-create-new-instance',
  templateUrl: './fly-panel-create-new-instance.component.html',
  styleUrls: ['./fly-panel-create-new-instance.component.scss']
})
export class FlyPanelCreateNewInstanceComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  instanceForm: UntypedFormGroup;
  @Output() newInstanceDetail: EventEmitter<any> = new EventEmitter<any>();
  @Output() updateInstanceDetail: EventEmitter<any> = new EventEmitter<any>();
  @Input() selectedClient: Client;
  @Input() instanceFlyInMode: string;
  @Input() selectedInstance: Instance;
  instanceCreateOptions: InstanceCreateOptions = new InstanceCreateOptions();
  instanceUpdateOptions: InstanceUpdateOptions = new InstanceUpdateOptions();
  loader: boolean = false;
  instanceIdentityProviders:IdentityProviderVM[] = [];
  defaultIdentityProvider:IdentityProviderVM;
  isLoading:boolean;

  constructor(private instanceService: InstanceService,
    private formBuilder: UntypedFormBuilder,
    private alert: SweetAlertService,
    private identityProviderService:IdentityProviderService) { }

  ngOnInit(): void {
    this.initializeInstanceForm();
    this.loadAsync();
  }
  
  async loadAsync(){
    this.isLoading = true;
    await this.getDefaultIdentityProvider();
    await this.setInstanceForm();
    if(this.instanceFlyInMode=='edit'){
      await this.getInstanceIdentityProviders();
    }
    this.isLoading = false;
  }
  
  initializeInstanceForm() {
    this.instanceForm = this.formBuilder.group({
      instanceName: [{ value: null, disabled: false }],
      isInBeta: [{ value: false, disabled: false }],
      databaseName: [{ value: null, disabled: false }],
      scormTenantName: [{ value: null, disabled: false }],
      clientAccountNumber: [{ value: null, disabled: false }],
      identityProvider: [{ value: null, disabled: false },Validators.required],
      mfaEnabled: [{ value: false, disabled: false }]
    });
  }

  setInstanceForm() {
    this.instanceForm.patchValue({
      instanceName: this.instanceFlyInMode === 'create' ? null : this.selectedInstance?.name,
      isInBeta: this.instanceFlyInMode === 'create' ? false : this.selectedInstance?.isInBeta,
      databaseName: this.instanceFlyInMode === 'create' ? null : this.selectedInstance?.instanceSetting?.databaseName,
      scormTenantName: this.instanceFlyInMode === 'create' ? null : this.selectedInstance?.instanceSetting?.scormTenant,
      clientAccountNumber: this.instanceFlyInMode === 'create' ? null : this.selectedInstance?.instanceSetting?.clientAccountNumber,
      identityProvider: this.instanceFlyInMode === 'create' ? this.defaultIdentityProvider?.id : (this.selectedInstance?.instanceSetting?.defaultIdentityProviderId ?? this.defaultIdentityProvider?.id),
      mfaEnabled: this.instanceFlyInMode === 'create' ? false : this.selectedInstance?.instanceSetting?.mfaEnabled
    });
  }

  async getInstanceIdentityProviders(){ 
    this.instanceIdentityProviders = await this.instanceService.getIdentityProvidersByInstanceName(this.instanceForm.get('instanceName')?.value);
  }

  async createInstanceAsync() {
    this.loader = true;
    this.instanceForm.disable();
    this.instanceCreateOptions.clientName = this.selectedClient.name;
    this.instanceCreateOptions.name = this.instanceForm.get('instanceName').value;
    this.instanceCreateOptions.databaseName = this.instanceForm.get('databaseName').value;
    this.instanceCreateOptions.isInBeta = this.instanceForm.get('isInBeta').value;
    this.instanceCreateOptions.scormTenant = this.instanceForm.get('scormTenantName').value;
    this.instanceCreateOptions.clientAccountNumber = this.instanceForm.get('clientAccountNumber').value;
    this.instanceCreateOptions.identityProviderId = this.instanceForm.get('identityProvider').value;
    this.instanceCreateOptions.mfaEnabled = this.instanceForm.get('mfaEnabled').value??false;
    this.instanceCreateOptions.createDatabase = true;
    this.instanceService.create(this.instanceCreateOptions).then((res) => {
      this.newInstanceDetail.emit(res);
      this.closed.emit()
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
      this.instanceForm.enable();
    });
  }

  async updateInstanceAsync() {
    this.loader = true;
    this.instanceForm.disable();
    this.instanceUpdateOptions.name = this.instanceForm.get('instanceName')?.value;
    this.instanceUpdateOptions.clientAccountNumber = this.instanceForm.get('clientAccountNumber')?.value;
    this.instanceUpdateOptions.isInBeta = this.instanceForm.get('isInBeta').value;
    this.instanceUpdateOptions.identityProviderId = this.instanceForm.get('identityProvider').value;
    this.instanceUpdateOptions.mfaEnabled = this.instanceForm.get('mfaEnabled').value;
    this.instanceService.update(this.instanceUpdateOptions?.name, this.instanceUpdateOptions).then((res) => {
      this.alert.successToast("Instance Updated Successfully");
      this.updateInstanceDetail.emit(res);
      this.closed.emit()
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
      this.instanceForm.enable();
    });
  }

  setDatabaseNameValue(event: any): void {
    const instanceName = event.target.value;
    const autofillValue = this.generateAutofillValue(instanceName);
    this.instanceForm.get('databaseName').setValue(autofillValue);
  }

  generateRandomString(length: number): string {
    let result = '';
    const characters = 'abcdefghijklmnopqrstuvwxyz0123456789';
    const charactersLength = characters.length;
    for (let i = 0; i < length; i++) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
  }

  generateAutofillValue(instanceName: string): string {
    const randomString = this.generateRandomString(8);
    return instanceName != "" ? `${instanceName}_${randomString}` : "";
  }

  isDisableSaveButton() {
    const form = this.instanceForm;
    const instanceName = form.get('instanceName').value;
    const databaseName = form.get('databaseName').value;
    const clientAccountNumber = form.get('clientAccountNumber').value;
    if (!instanceName || !databaseName || !clientAccountNumber ||
      !form.get('instanceName').valid || !form.get('databaseName').valid || !form.get('clientAccountNumber').valid) {
      return true;
    }
    if (instanceName === databaseName) {
      return true;
    }
    return false;
  }

  isDisableUpdateButton() {
    const form = this.instanceForm;
    let clientAccountNumber = form.get('clientAccountNumber')?.value;
    if ((clientAccountNumber === null || clientAccountNumber === undefined || clientAccountNumber === '' 
         || !form.get('clientAccountNumber').valid) && clientAccountNumber !== 0) {
        return true;
    }
    if(!form.get('identityProvider').valid) return true;
    
    return false;
}

async getDefaultIdentityProvider(){
  this.defaultIdentityProvider =  await this.identityProviderService.getIdentityProviderByName('Default Password Provider');
  return this.defaultIdentityProvider;
}

}
