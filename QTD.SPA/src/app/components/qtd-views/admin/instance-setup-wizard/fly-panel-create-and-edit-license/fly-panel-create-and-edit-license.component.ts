import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { ClientSettings_AnalyzeLicenseOptions } from '@models/ClientSettingsLicense/ClientSettings_AnalyzeLicenseOptions';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { Instance } from '@models/Instance/Instance';
import { ClientSettings_LicenseUpdateOptions } from '@models/ClientSettingsLicense/ClientSettings_LicenseUpdateOptions';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { InstanceSetupService } from 'src/app/_Services/QTD/Instance/instance-setup.service';

@Component({
  selector: 'app-fly-panel-create-and-edit-license',
  templateUrl: './fly-panel-create-and-edit-license.component.html',
  styleUrls: ['./fly-panel-create-and-edit-license.component.scss']
})
export class FlyPanelCreateAndEditLicenseComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() newLicenseDetail: EventEmitter<any> = new EventEmitter<any>();
  @Input() licenseEditMode: string;
  @Input() licenseDetail: any;
  @Input() selectedInstance: Instance;
  productDetailDataSource = new MatTableDataSource<any>();
  clientSettings_LicenseUpdateOptions: ClientSettings_LicenseUpdateOptions;
  clientSettings_AnalyzeLicenseOptions: ClientSettings_AnalyzeLicenseOptions;
  licenseForm: UntypedFormGroup;
  loader: boolean = false;
  saveLoader: boolean = false;
  isLicenseAnalyzed: boolean = false;
  displayLicenseExpandColumns: string[] = [
    'productName',
    'productAcronym',
    'releaseDate',
    'companyProduct'
  ];

  constructor(private formBuilder: UntypedFormBuilder,
    private alert: SweetAlertService,
    private instanceSetupService : InstanceSetupService,
  ) { }

  ngOnInit(): void {
    this.setProductDataDetails();
    this.initializeLicenseForm();
  }

  setProductDataDetails() {
    const productDetailList = this.licenseDetail?.products;
    this.productDetailDataSource = new MatTableDataSource(productDetailList);
  }

  initializeLicenseForm() {
    this.licenseForm = this.formBuilder.group({
      clientId: [{ value: this.selectedInstance?.instanceSetting?.clientAccountNumber, disabled: true }],
      activationCode: this.licenseEditMode == 'Add' ? [{ value: null, disabled: false }] : [{ value: this.licenseDetail?.activationCode, disabled: false }]
    });
  }

  async analyzeLicenseAsync() {
    this.loader = true;
    this.licenseForm.disable();
    this.clientSettings_AnalyzeLicenseOptions = new ClientSettings_AnalyzeLicenseOptions();
    this.clientSettings_AnalyzeLicenseOptions.clientAccountNumber = this.selectedInstance?.instanceSetting?.clientAccountNumber;
    this.clientSettings_AnalyzeLicenseOptions.activationCode = this.licenseForm.get('activationCode').value;
    this.instanceSetupService.analyzeLicenseAsync(this.clientSettings_AnalyzeLicenseOptions,this.selectedInstance?.name).then((res) => {
      this.alert.successToast("License Successfully Analyzed");
      this.licenseDetail = res;
      this.productDetailDataSource = new MatTableDataSource(this.licenseDetail?.products);
      this.isLicenseAnalyzed = true;
      this.licenseForm.enable();
      this.licenseForm.get('clientId').disable();
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("License Analyze Failed");
      this.licenseForm.enable()
      this.licenseForm.get('clientId').disable();
    });
  }


  async createAndUpdateLicense() {
    this.saveLoader = true;
    this.licenseForm.disable();
    this.clientSettings_LicenseUpdateOptions = new ClientSettings_LicenseUpdateOptions(this.licenseDetail?.activationCode);
    this.clientSettings_LicenseUpdateOptions.clientAccountNumber = this.licenseDetail?.clientAccountNumber;
    this.instanceSetupService.updateLicenseAsync(this.clientSettings_LicenseUpdateOptions, this.selectedInstance?.name).then((res) => {
      this.alert.successToast("License Saved Successfully");
      this.licenseForm.enable();
      this.licenseForm.get('clientId').disable();
      this.newLicenseDetail.emit(res);
      this.closed.emit();
      this.saveLoader = false;
    }).catch((error) => {
      this.saveLoader = false;
      this.alert.errorToast("License Not Saved");
      this.licenseForm.enable()
      this.licenseForm.get('clientId').disable();
    });
  }

  resetActivationCodeDetails(event: any): void {
    this.licenseDetail = null;
    this.isLicenseAnalyzed = false;
  }

}
