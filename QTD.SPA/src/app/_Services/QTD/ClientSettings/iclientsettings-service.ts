import { ApiClientSettingsService } from './api.clientsettings.service';
import { StubClientsettingsService } from './stub.clientsettings.service';
import { environment } from 'src/environments/environment';
import {HttpClient} from "@angular/common/http";

import {
  ClientSettings_LabelReplacement
} from "../../../_DtoModels/ClientSettingsLabelReplacement/ClientSettings_LabelReplacement";
import { ClientSettings_NotificationUpdateOptions } from 'src/app/_DtoModels/ClientsSettingsNotification/ClientSettings_NotificationUpdateOptions';
import {
  ClientSettings_LabelReplacement_UpdateOptions
} from "../../../_DtoModels/ClientSettingsLabelReplacement/ClientSettings_LabelReplacement_UpdateOptions";
import {
  ClientSettings_GeneralSettings_UpdateOptions
} from "../../../_DtoModels/ClientSettingsGeneralSetting/ClientSettings_GeneralSettings_UpdateOptions";

export interface IClientSettingsService {
  getNotifications: () => Promise<any>;
  getNotificationByName: (name :string ) => Promise<any>;
  updateNotification : (notification: string, options: ClientSettings_NotificationUpdateOptions)=>Promise<any>;
  GetGeneralSettingsAsync:()=>Promise<any>;
  UpdateGeneralSettings : (options: ClientSettings_GeneralSettings_UpdateOptions)=>Promise<any>;
  GetLabelReplacementsAsync: (refresh: boolean) => Promise<ClientSettings_LabelReplacement[]>;
  UpdateLabelReplacementsAsync : (options: ClientSettings_LabelReplacement_UpdateOptions)=>Promise<any>;
  GetCurrentLicenseAsync:()=>Promise<any>;
  getAllFeatureAsync:()=>Promise<any>;
  updateFeatureAsync:(options:any)=>Promise<any>;

  LabelReplacements: ClientSettings_LabelReplacement[];
}

function clientSettingsServiceFactory(http: HttpClient) {
  //here you can either inject params in to determine whic service to use OR detect an env var
  if(environment.Storybook_UseStub){
    return new StubClientsettingsService();
  }
  else{
    return new ApiClientSettingsService(http);
  }
}

export const clientSettingsServiceProvider = {
  provide: ApiClientSettingsService,
  useFactory: clientSettingsServiceFactory,
  deps: [HttpClient]
};
