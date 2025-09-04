import { Injectable } from '@angular/core';
import { IClientUserSettingsService } from './iclientusersettings-service';
import { pascalToCamel } from 'src/app/_Shared/Utils/PascalToCamel';
import {
  ClientUserSettings_Dashboard
} from "../../../_DtoModels/ClientUserSettingsDashboard/ClientUserSettings_Dashboard";
import { ClientUserSettings_Dashboard_UpdateOptions } from 'src/app/_DtoModels/ClientUserSettingsDashboard/ClientUserSettings_Dashboard_UpdateOptions';
import * as dasboardsettingdata from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/dashboard_settings.json';
import *  as clientusersettingdata from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientUserSettings_dashboard.json';
import { CustomizeDashboardUpdateOptions } from 'src/app/_DtoModels/ClientUserSettingsDashboard/CustomizeDashboardUpdateOptions';


@Injectable({
  providedIn: 'root',
})
export class StubClientUserSettingsService implements IClientUserSettingsService {
  constructor() {
  }

  GetDashboardSettingsAsync = () => {
    return new Promise<ClientUserSettings_Dashboard>((resolve, reject) => {
      setTimeout(() => {
        const userdata = pascalToCamel(clientusersettingdata);
        const dashboardSettings = pascalToCamel(dasboardsettingdata);
        userdata.forEach(element => {
          element.dashboardSetting = dashboardSettings.filter(r => r.id === element.dashboardSettingId)[0];
        });
        resolve(userdata);
      }, 500);
    });
  }

  UpdateDashboardSettingsAsync = (options: CustomizeDashboardUpdateOptions) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve('ok');
      }, 500);
    });
  }
}
