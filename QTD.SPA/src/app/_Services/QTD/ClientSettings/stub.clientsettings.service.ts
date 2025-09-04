import {Injectable} from '@angular/core';
import {IClientSettingsService} from './iclientsettings-service';

import * as data
  from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientSettings_notifications.json'
import * as generalSettingsData
  from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientSettings_GeneralSettings.json'
import * as labelReplacementData
  from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientSettings_LabelReplacements.json'
import * as licenseData
  from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientSettings_License.json'
import * as licenseProductInfoData
  from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/clientSettings_License_ProductInfo.json'

import {pascalToCamel} from 'src/app/_Shared/Utils/PascalToCamel';
import {
  ClientSettings_NotificationUpdateOptions
} from "../../../_DtoModels/ClientsSettingsNotification/ClientSettings_NotificationUpdateOptions";

import {
  ClientSettings_LabelReplacement
} from "../../../_DtoModels/ClientSettingsLabelReplacement/ClientSettings_LabelReplacement";
import {ClientSettings_License} from "../../../_DtoModels/ClientSettingsLicense/ClientSettings_License";
import {
  ClientSettings_GeneralSettings
} from "../../../_DtoModels/ClientSettingsGeneralSetting/ClientSettings_GeneralSettings";
import {
  ClientSettings_LabelReplacement_UpdateOptions
} from "../../../_DtoModels/ClientSettingsLabelReplacement/ClientSettings_LabelReplacement_UpdateOptions";
import {
  ClientSettings_GeneralSettings_UpdateOptions
} from "../../../_DtoModels/ClientSettingsGeneralSetting/ClientSettings_GeneralSettings_UpdateOptions";

@Injectable({
  providedIn: 'root',
})
export class StubClientsettingsService implements IClientSettingsService {
  constructor() {
  }

  LabelReplacements: ClientSettings_LabelReplacement[];

  getNotifications = () => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve({locList: pascalToCamel(data)});
      }, 500);
    });
  }

  getNotificationByName = (name: string) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve({locList: pascalToCamel(data)});
      }, 500);
    });
  }

  updateNotification = (notification: string, options: ClientSettings_NotificationUpdateOptions) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        Promise.resolve();
      }, 500);
    });
  }

  //GeneralSettings stub service

  GetGeneralSettingsAsync = () => {
    return new Promise<ClientSettings_GeneralSettings>((resolve, reject) => {
      setTimeout(() => {
        resolve(pascalToCamel(generalSettingsData));
      }, 500);
    });
  }

  UpdateGeneralSettings = (options: ClientSettings_GeneralSettings_UpdateOptions) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve('ok');
      }, 500);
    });
  }

  //label replacement stub service

  GetLabelReplacementsAsync = () => {
    return new Promise<ClientSettings_LabelReplacement[]>((resolve, reject) => {
      setTimeout(() => {
        resolve(pascalToCamel(labelReplacementData) as ClientSettings_LabelReplacement[]);
      }, 500);
    });
  }

  UpdateLabelReplacementsAsync = (options: ClientSettings_LabelReplacement_UpdateOptions) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve('ok');
      }, 500);
    });
  }

  //license setting stub service

  GetCurrentLicenseAsync = () => {
    return new Promise<ClientSettings_License>((resolve, reject) => {
      setTimeout(() => {
        const license = pascalToCamel(licenseData).filter(r => r.active)[0];
        const productInfo = pascalToCamel(licenseProductInfoData).filter(r => r.clientSettingsLicenseId == license.id);
        license.products = productInfo;
        resolve(license);
      }, 500);
    });
  }

  UpdateLicenseAsync = () => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve('ok');
      }, 500);
    });
  }

  getAllFeatureAsync = ()=>{
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve('ok');
      }, 500);
    });
  }

  updateFeatureAsync = ()=>{
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve('ok');
      }, 500);
    });
  }

}
