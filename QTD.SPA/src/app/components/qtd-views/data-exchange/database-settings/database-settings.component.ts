import { Component, OnInit } from '@angular/core';
import { ApiClientSettingsService } from "../../../../_Services/QTD/ClientSettings/api.clientsettings.service";
import {
  ClientSettings_LabelReplacement_UpdateOptions
} from "../../../../_DtoModels/ClientSettingsLabelReplacement/ClientSettings_LabelReplacement_UpdateOptions";
import { SweetAlertService } from "../../../../_Shared/services/sweetalert.service";
import { TranslateService } from "@ngx-translate/core";
import { Subject } from "rxjs";
import {
  ClientSettings_Notification
} from "../../../../_DtoModels/ClientsSettingsNotification/ClientSettings_Notification";
import { ApiClientUserSettingsService } from "../../../../_Services/QTD/ClientUserSettings/api.clientusersettings.service"
import { CustomizeDashboardUpdateOptions } from 'src/app/_DtoModels/ClientUserSettingsDashboard/CustomizeDashboardUpdateOptions';
import { ClientSettings_FeatureUpdateOptions } from '@models/ClientSettingsFeature/ClientSettings_FeatureUpdateOptions';
import { MatLegacyTabChangeEvent as MatTabChangeEvent } from '@angular/material/legacy-tabs';
import { InstanceService } from 'src/app/_Services/Auth/instance.service';
import { PublicUrlInstanceSettingOptions } from '@models/Instance/PublicUrlInstanceSettingOptions';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';

@Component({
  selector: 'app-database-settings',
  templateUrl: './database-settings.component.html',
  styleUrls: ['./database-settings.component.scss']
})
export class DatabaseSettingsComponent implements OnInit {

  public labelReplacementData;
  public customizeDashboardData;
  public licenseSettingData;
  public generalSettingsData;
  public dasboardsettingData;
  public isPanelOpen: Boolean;
  public featureData;
  private currentIndex = 0;
  completeGeneralSettingsSubject: Subject<void> = new Subject<void>();
  completeLicenseSubject: Subject<void> = new Subject<void>();
  completeLabelReplacementSubject: Subject<void> = new Subject<void>();
  completeCustomizeDashboardSubject: Subject<void> = new Subject<void>();
  publicUrlSettingSubject: Subject<string> = new Subject<string>();
  publicUrl:string;

  constructor(
    private clientSettingsService: ApiClientSettingsService,
    private alertService: SweetAlertService,
    private translate: TranslateService,
    private clientUserSettingsService: ApiClientUserSettingsService,
    private instanceService: InstanceService) {
      const browserLang = localStorage.getItem('lang') ?? 'en';
      this.translate.use(browserLang);
  }

  async ngOnInit(): Promise<void> {
    this.getInstanceSetting();
    this.labelReplacementData = await this.clientSettingsService.GetLabelReplacementsAsync(true);
    this.licenseSettingData = await this.clientSettingsService.GetCurrentLicenseVMAsync();
    this.generalSettingsData = await this.clientSettingsService.GetGeneralSettingsAsync();
    this.dasboardsettingData = await this.clientUserSettingsService.GetDashboardSettingsAsync();
    this.featureData = await this.clientSettingsService.getAllFeatureAsync();
    this.currentIndex = 0;
  }

  async handleLicenseSaveClick(licenseUpdateOptions): Promise<void> {
    try {
      await this.clientSettingsService.UpdateLicenseAsync(licenseUpdateOptions);
      this.licenseSettingData = await this.clientSettingsService.GetCurrentLicenseVMAsync();
      this.alertService.notificationSuccessToast(this.translate.instant("Saved Successfully"));
      this.completeLicenseSubject.next(this.licenseSettingData);
    } catch (e) {

    }
  }

  async handleGeneralSettingsSaveClick(obj) : Promise<void> {
    try {
      var publicUrlOptions  = new PublicUrlInstanceSettingOptions();
      publicUrlOptions.publicUrl = obj.url;
      await this.clientSettingsService.UpdateGeneralSettings(obj.options);
      this.generalSettingsData = await this.clientSettingsService.GetGeneralSettingsAsync();
      localStorage.removeItem('dateFormat');
      localStorage.setItem('dateFormat',this.generalSettingsData?.dateFormat)
      var publicUrl = (await this.instanceService.updateInstanceSettingsPublicUrlAsync(jwtAuthHelper.SelectedInstance, publicUrlOptions)).publicUrl;
      this.alertService.notificationSuccessToast(this.translate.instant("Saved Successfully"));
      this.completeGeneralSettingsSubject.next(this.generalSettingsData);
      this.publicUrlSettingSubject.next(publicUrl); 
    } catch (e) {
      
    }
  }

  async handleLabelReplacementSaveClick(labelReplacementUpdateOptions: ClientSettings_LabelReplacement_UpdateOptions): Promise<void> {
    try {
      await this.clientSettingsService.UpdateLabelReplacementsAsync(labelReplacementUpdateOptions);
      this.labelReplacementData = await this.clientSettingsService.GetLabelReplacementsAsync(true);
      this.alertService.notificationSuccessToast(this.translate.instant("Saved Successfully"));
      this.completeLabelReplacementSubject.next(this.labelReplacementData);
    } catch (e) {

    }
  }

  async handleCustomizeDashboardClickEvent(customizeDashboardUpdateOptions: CustomizeDashboardUpdateOptions): Promise<void> {
    try{
      
      await this.clientUserSettingsService.UpdateDashboardSettingsAsync(customizeDashboardUpdateOptions);
      this.customizeDashboardData = await this.clientUserSettingsService.GetDashboardSettingsAsync();
      this.alertService.notificationSuccessToast(this.translate.instant("Saved Successfully"));
      this.completeCustomizeDashboardSubject.next(this.customizeDashboardData);
    } catch (e) {

    }
  }
  togglePannel(val: boolean) {
    this.isPanelOpen = val;
  }

   async handleFeatureSaveClick(options:any): Promise<void>{
    var updateOption:ClientSettings_FeatureUpdateOptions = new ClientSettings_FeatureUpdateOptions() ;
    updateOption.featureList = options;
    try{
      await this.clientSettingsService.updateFeatureAsync(updateOption);
      this.alertService.notificationSuccessToast(this.translate.instant("Updated Successfully"))
      setTimeout(()=>{
        window.location.reload();
      },1500)
    }catch(e){

    }
  }

  getSelectedTabIndex(): number {
    return this.currentIndex;
  }
  onTabChange(event: MatTabChangeEvent) {
    this.currentIndex = event.index;
  }
  onFeatureUrlClicked(index: number) {
    this.currentIndex = index;
  }

  async getInstanceSetting(){
    this.publicUrl = await this.instanceService.getPublicURLInstanceSettingsAsync(jwtAuthHelper.SelectedInstance);
  }
}
