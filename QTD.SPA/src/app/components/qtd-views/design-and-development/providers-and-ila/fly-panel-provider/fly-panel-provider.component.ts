import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { Subject } from 'rxjs';
import { Provider } from 'src/app/_DtoModels/Provider/Provider';
import { ProviderCreateOptions } from 'src/app/_DtoModels/Provider/ProviderCreateOptions';
import { ProviderUpdateOptions } from 'src/app/_DtoModels/Provider/ProviderUpdateOptions';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ProviderLevelService } from 'src/app/_Services/QTD/provider-level.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-provider',
  templateUrl: './fly-panel-provider.component.html',
  styleUrls: ['./fly-panel-provider.component.scss'],
})
export class FlyPanelProviderComponent implements OnInit {
  @Input() provider_edit_mode: boolean;
  @Input() provider_change_mode: boolean;
  @Input() provider_edit: boolean;
  @Input() oldProvider: any;
  @Input() ilaIdForChange: any;
  @Input() provider_copy_mode: boolean;
  @Input() providerRow:any;
  @Output() closed = new EventEmitter<any>();
  imageProvider: string = '';
  ProviderName: UntypedFormControl;
  providerInfo: string = 'Provider Information';
  NERCChecked: boolean = false;
  csvLoaded: boolean = false;
  providerLevel: any[] = [];
  provider_dropdown: Provider[] = [];
  showSpinner = false;
  imageInBase64: string | undefined = '';
  allowedTypes = ['image/jpg', 'image/jpeg', 'image/png'];
  uploadedImage: string;
  newProviderId: any;
  providerForm: UntypedFormGroup = new UntypedFormGroup({
    ProviderName: new UntypedFormControl('', Validators.required),
    ProviderNumber: new UntypedFormControl('', [Validators.required]),
    ContactName: new UntypedFormControl('', [Validators.required]),
    ContactTitle: new UntypedFormControl(''),
    ContactPhone: new UntypedFormControl('0'),
    ContactExt: new UntypedFormControl('0',[this.validatePositiveInteger]),
    ContactMobile: new UntypedFormControl('0'),
    ContactEmail: new UntypedFormControl('', [Validators.email]),
    CompanyWebsite: new UntypedFormControl(''),
    nercChecked: new UntypedFormControl(''),
    nercProvider: new UntypedFormControl(false),
    priority: new UntypedFormControl(false),
    RepEmail: new UntypedFormControl('', [Validators.email]),
    RepPhone: new UntypedFormControl('0'),
    isSameAsContact: new UntypedFormControl(false),
    RepName: new UntypedFormControl('',[Validators.required]),
    RepTitle: new UntypedFormControl(''),
  });
  edit_provider_array: any = [];
  provider_Check: boolean = false;
  anotherCheck:boolean=false;

  public Editor = ckcustomBuild;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private service: ProviderService,
    private alert: SweetAlertService,
    private dataBroadcast: DataBroadcastService,
    private plService: ProviderLevelService,
    private providerSrvc: ProviderService,
    private ilaSrvc: IlaService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.readyProviderLevel();
    this.NERCChecked = false;
    if(this.provider_change_mode === true){
      this.getProviders();
    }
    if((this.provider_copy_mode === true || this.provider_edit_mode === true) && this.oldProvider !== null && this.oldProvider !== undefined){
      this.getProviderForCopyOrEdit()
    }

    this.dataBroadcast.providerSaved.subscribe((res) => {
      this.getProviders();
    });
  }
  validatePositiveInteger(control: UntypedFormControl): { [key: string]: any } | null {

    const value =Number(control.value) ;
    if (value < 0 || !Number.isInteger(value)) {
      return { 'invalidPositiveInteger': true };
    }
    return null;
  }
  async readyProviderLevel() {
    await this.plService.getAll().then((res) => {

      this.providerLevel = res;
    });
  }

  changeNERCStatus(checked: boolean) {

    if (checked == true) {
      this.providerInfo = 'NERC Provider Information';
      this.NERCChecked = true;
    } else {
      this.providerInfo = 'Provider Information';
      this.NERCChecked = false;
    }
  }

  uploadCSVFile(e: any) {
    this.csvLoaded = true;
    if (this.allowedTypes.includes(e.target.files[0].type.toLowerCase())) {
      let file = e.target.files[0];
      let reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onloadend = () => {
        this.imageInBase64 = reader.result?.toString();
        this.uploadedImage = file;
      };
      reader.onerror = function (error) {

      };
    } else {
      this.alert.errorAlert(
        'Invalid File Upload',
        'Uploaded file should be an image'
      );
    }
  }

  clearImage(){
    this.imageInBase64 = '';
    this.imageProvider = '';
    this.csvLoaded = false;
  }

  saveProvider() {
    //save provider

    if(this.provider_edit_mode===true && this.oldProvider === undefined){
      this.showSpinner = true;
      var options: ProviderCreateOptions = new ProviderCreateOptions();
      options.name = this.providerForm.get('ProviderName')?.value;
      options.number = this.providerForm.get('ProviderNumber')?.value;
      options.contactName = this.providerForm.get('ContactName')?.value;
      options.contactEmail = this.providerForm.get('ContactEmail')?.value;
      options.contactExt = this.providerForm.get('ContactExt')?.value;
      options.contactMobile = this.providerForm.get('ContactMobile')?.value;
      options.contactTitle = this.providerForm.get('ContactTitle')?.value;
      options.contactPhone = this.providerForm.get('ContactPhone')?.value;
      options.companyWebsite = this.providerForm.get('CompanyWebsite')?.value;
      options.isPriority = this.providerForm.get('priority')?.value;

      if (this.providerForm.get('isSameAsContact')?.value) {
        options.repName = this.providerForm.get('ContactName')?.value;
        options.repEmail = this.providerForm.get('ContactEmail')?.value;
        options.repPhone = this.providerForm.get('ContactPhone')?.value;
        options.repTitle = this.providerForm.get('ContactTitle')?.value;
      } else {
        options.repName = this.providerForm.get('RepName')?.value;
        options.repEmail = this.providerForm.get('RepEmail')?.value;
        options.repPhone = this.providerForm.get('RepPhone')?.value;
        options.repTitle = this.providerForm.get('RepTitle')?.value;
      }

      options.repSignature = this.imageInBase64 ? this.imageInBase64 : '';

      if (this.NERCChecked) {
        options.providerLevelId = this.providerForm.get('nercChecked')?.value;
        options.isNERC = this.NERCChecked;
      }
      // else {
      //   options.providerLevelId = 'xY';
      // }
      var response = this.service.create(options);
      response
        .then((res: any) => {
          if (res !== undefined && this.anotherCheck === true) {
            this.alert.successToast(res?.message);
            this.anotherCheck = false;
            this.providerForm.reset();
            this.changeNERCStatus(false);
            this.dataBroadcast.providerSaved.next(null);
          } else if(this.anotherCheck === false) {
            this.alert.successToast(res?.message);
            this.dataBroadcast.providerSaved.next(null);
            this.closed.emit(true);
          }
        })
        .catch((res) => {
          this.showSpinner = false;
        })
        .finally(() => {
          this.showSpinner = false;
        });

    }

    //this.flyPanelSrvc.close()

    //update provider
    else if(this.provider_edit_mode === true && this.oldProvider && this.provider_copy_mode === false){
      var updateOptions: ProviderUpdateOptions = new ProviderUpdateOptions();
      updateOptions.name = this.providerForm.get('ProviderName')?.value;
      updateOptions.number = this.providerForm.get('ProviderNumber')?.value;
      updateOptions.contactName = this.providerForm.get('ContactName')?.value;
      updateOptions.contactEmail = this.providerForm.get('ContactEmail')?.value;
      updateOptions.contactExt = this.providerForm.get('ContactExt')?.value;
      updateOptions.contactMobile =
        this.providerForm.get('ContactMobile')?.value;
      updateOptions.contactTitle = this.providerForm.get('ContactTitle')?.value;
      updateOptions.contactPhone = this.providerForm.get('ContactPhone')?.value;
      updateOptions.companyWebsite =
        this.providerForm.get('CompanyWebsite')?.value;
      updateOptions.isPriority = this.providerForm.get('priority')?.value;

      if (this.providerForm.get('isSameAsContact')?.value) {
        updateOptions.repName = this.providerForm.get('ContactName')?.value;
        updateOptions.repEmail = this.providerForm.get('ContactEmail')?.value;
        updateOptions.repPhone = this.providerForm.get('ContactPhone')?.value;
        updateOptions.repTitle = this.providerForm.get('ContactTitle')?.value;
      } else {
        updateOptions.repName = this.providerForm.get('RepName')?.value;
        updateOptions.repEmail = this.providerForm.get('RepEmail')?.value;
        updateOptions.repPhone = this.providerForm.get('RepPhone')?.value;
        updateOptions.repTitle = this.providerForm.get('RepTitle')?.value;
      }
        updateOptions.repSignature = this.imageInBase64 ? this.imageInBase64 : this.imageProvider;

   //   updateOptions.repSignature = this.imageInBase64 ? this.imageInBase64 : '';

      if (this.NERCChecked) {
        updateOptions.providerLevelId = this.providerForm.get('nercChecked')?.value;
        updateOptions.isNERC = this.NERCChecked;
      }
      this.providerSrvc
        .update(this.oldProvider, updateOptions)
        .then((res) => {
          if (res) {
            this.alert.successToast('Provider Updated successfully');
            this.flyPanelSrvc.close();
            this.closed.emit(true);
          }
        })
        .catch((err) => {

        });
    } else if (
      this.provider_edit_mode === true &&
      this.oldProvider !== undefined &&
      this.provider_copy_mode === true
    ) {
      var options: ProviderCreateOptions = new ProviderCreateOptions();
      var concatName = this.providerForm.get('ProviderName')?.value;
      options.name = concatName.concat('-Copy');
      options.number = this.providerForm.get('ProviderNumber')?.value;
      options.contactName = this.providerForm.get('ContactName')?.value;
      options.contactEmail = this.providerForm.get('ContactEmail')?.value;
      options.contactExt = this.providerForm.get('ContactExt')?.value;
      options.contactMobile = this.providerForm.get('ContactMobile')?.value;
      options.contactTitle = this.providerForm.get('ContactTitle')?.value;
      options.contactPhone = this.providerForm.get('ContactPhone')?.value;
      options.companyWebsite = this.providerForm.get('CompanyWebsite')?.value;
      options.isPriority = this.providerForm.get('priority')?.value;

      if (this.providerForm.get('isSameAsContact')?.value) {
        options.repName = this.providerForm.get('ContactName')?.value;
        options.repEmail = this.providerForm.get('ContactEmail')?.value;
        options.repPhone = this.providerForm.get('ContactPhone')?.value;
        options.repTitle = this.providerForm.get('ContactTitle')?.value;
      } else {
        options.repName = this.providerForm.get('RepName')?.value;
        options.repEmail = this.providerForm.get('RepEmail')?.value;
        options.repPhone = this.providerForm.get('RepPhone')?.value;
        options.repTitle = this.providerForm.get('RepTitle')?.value;
      }

      options.repSignature = this.imageInBase64 ? this.imageInBase64 : '';

      if (this.NERCChecked) {
        options.providerLevelId = this.providerForm.get('nercChecked')?.value;
        options.isNERC = this.NERCChecked;
      }
      // else {
      //   options.providerLevelId = 'xY';
      // }
      var response = this.service.create(options);
      response
        .then((res: any) => {
          if (res !== undefined) {
            this.showSpinner = false;
            this.alert.successAlert(
              'Provider Copy Created',
              concatName.concat('-Copy')
            );
            this.dataBroadcast.providerSaved.next(null);
            this.closed.emit(true);
          } else {
            this.showSpinner = false;
          }
        })
        .catch((res) => {
          this.showSpinner = false;
        })
        .finally(() => {
          this.showSpinner = false;
        });
    }
  }

  async getProviderForCopyOrEdit(){
    var provData = await this.providerSrvc.getOnlyProviderData(this.oldProvider)
    this.provider_dropdown.push(provData);
    this.getEditProvider();
  }

  getEditProvider() {
    this.provider_dropdown.forEach((i) => {
      if (i.id === this.oldProvider) {
        this.edit_provider_array.push(i);
        this.providerForm.patchValue({
          ProviderName: i.name,
          ProviderNumber: i.number,
          ContactName: i.contactName,
          ContactTitle: i.contactTitle,
          ContactPhone: i.contactPhone,
          ContactExt: i.contactExt,
          ContactMobile: i.contactMobile,
          ContactEmail: i.contactEmail,
          CompanyWebsite: i.companyWebsite,
          nercChecked: i.providerLevelId,
          nercProvider : i.isNERC,
          priority: i.isPriority,
          RepEmail: i.repEmail,
          RepPhone: i.repPhone,
          isSameAsContact: false,
          RepName: i.repName,
          RepTitle: i.repTitle,
        });
        this.NERCChecked = i.isNERC;
        this.imageProvider = i.repSignature
      }
    /*   if(i.isNERC === true){
        this.NERCChecked =  true;
      } */
    })
  }

  keyPressNumbersContact(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
    // Only Numbers 0-9
/*     var a = /(^[0-9]+[-]*[0-9]+$)/;
    if (a.test(charCode) == true)
    {
      return true;
    }
    else
    {
      return false;
    }
 */
     if (charCode >= 48 || charCode <= 57 && charCode === 45) {
      return true;
    } else {
      event.preventDefault();
      return false;
    }
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

  onReady(editor: any) {
    //
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  async OnChangeProvider() {


    if (this.newProviderId === this.oldProvider) {
      return this.alert.warningAlert(
        '',
        'Selected Provider is already assigned to this' + await this.labelPipe.transform('ILA') + ' .'
      );
    }
    this.showSpinner = true;
    this.ilaSrvc
      .changeProvider(this.ilaIdForChange, this.newProviderId)
      .then((res) => {
        if (res) {
          this.closed.emit('refreshtbl');
          this.alert.successToast('Provider Changed successfully');
          this.oldProvider = this.newProviderId;
        }
      })
      .finally(() => {
        this.showSpinner = false;
      });
    //this.flyPanelSrvc.close();
  }

  async getProviders() {
    await this.providerSrvc.getWihtoutIncludes().then((res) => {
      this.provider_dropdown = res;
      if (this.oldProvider) {
        this.newProviderId = this.oldProvider;
      }
    });

    if (this.provider_edit_mode === true && this.oldProvider !== undefined) {
      this.getEditProvider();
    }
  }

  OnAddNewProvider() {

    this.provider_edit_mode = true;
    this.provider_change_mode = false;
    this.provider_Check=true;
    this.oldProvider = undefined;
  }

  closeProvider() {
    if (this.newProviderId === undefined) {
      // this.flyPanelSrvc.close();
      this.closed.emit('fp-add-provider-closed');
    } else if (
      this.provider_edit_mode === true &&
      this.oldProvider !== undefined
    ) {
      this.closed.emit('fp-add-provider-closed');
    } else {
      this.provider_edit_mode = false;
      this.provider_change_mode = true;
    }
  }

  changeValidation(event:any){
    if(event.checked){
      this.providerForm.get('RepName')?.clearValidators();
      this.providerForm.get('RepName')?.setErrors(null);
    }
    else{
      this.providerForm.get('RepName')?.setValidators(Validators.required);
      this.providerForm.get('RepName')?.setErrors({required:true});
    }
    this.providerForm.updateValueAndValidity();
  }
}
