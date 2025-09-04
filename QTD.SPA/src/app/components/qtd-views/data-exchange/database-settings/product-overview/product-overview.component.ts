import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {MatLegacyTableDataSource as MatTableDataSource} from '@angular/material/legacy-table';
import {ApiClientSettingsService} from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import {ClientSettings_License} from "../../../../../_DtoModels/ClientSettingsLicense/ClientSettings_License";
import {Observable, Subscription} from "rxjs";
import {
  ClientSettings_LicenseUpdateOptions
} from "../../../../../_DtoModels/ClientSettingsLicense/ClientSettings_LicenseUpdateOptions";
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-product-overview',
  templateUrl: './product-overview.component.html',
  styleUrls: ['./product-overview.component.scss']
})
export class ProductOverviewComponent implements OnInit {

  @Input() ClientSettings_License: any;

  private licenseUpdateOptions: ClientSettings_LicenseUpdateOptions;
  public licenseForm: UntypedFormGroup;
  public mode: string;

  @Output()
  OnClientIdChangeEvent: EventEmitter<any> = new EventEmitter();
  @Output()
  OnActivationCodeChangeEvent: EventEmitter<any> = new EventEmitter();
  @Output()
  OnSaveClickedEvent: EventEmitter<any> = new EventEmitter();
  @Output()
  OnCancelEvent: EventEmitter<any> = new EventEmitter();

  displayColumns: string[] = ["Product Name", "Product Acronym", "Version & Release Date", "Does my company have this product?"];

  @Input() completeEvent: Observable<void>;

  private completeSubscription: Subscription;

  constructor(private clientService: ApiClientSettingsService, private fb: UntypedFormBuilder) {
  }

  ngOnInit(): void {
    this.mode = "read";
    this.licenseUpdateOptions = new ClientSettings_LicenseUpdateOptions(this.ClientSettings_License.activationCode);
    const self = this;
    self.getLicenseForm();
    self.licenseForm.disable();
    this.completeSubscription = this.completeEvent.subscribe((clientSettings_License:any) => {
      self.ClientSettings_License = clientSettings_License;
      self.mode = 'read'
      self.licenseForm.disable();
      self.getLicenseForm();
    })
  }

  getLicenseForm(){
    this.licenseForm = this.fb.group({
      activationCode:  new UntypedFormControl(this.ClientSettings_License.activationCode, [Validators.required])
    })
  }

  OnCancelButtonClick() {
    this.licenseForm.reset({
      activationCode: this.ClientSettings_License.activationCode
    });
    this.mode = 'read';
    this.licenseForm.disable();
  }

  OnSaveButtonClick() {
    this.licenseUpdateOptions.clientAccountNumber = this.ClientSettings_License.clientAccountNumber;
    this.OnSaveClickedEvent.emit(this.licenseUpdateOptions)
  }

  OnEditButtonClick() {
    this.mode = 'write';
    this.licenseForm.enable();
  }

  OnFieldUpdated(name: string, value: string) {
    this.licenseUpdateOptions.UpdateValue(name, value);
  }

}
