import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {firstValueFrom, Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {
  ClientSettings_NotificationUpdateOptions
} from 'src/app/_DtoModels/ClientsSettingsNotification/ClientSettings_NotificationUpdateOptions';
import {pascalToCamel} from 'src/app/_Shared/Utils/PascalToCamel';
import {environment} from 'src/environments/environment';

import {IClientSettingsService} from './iclientsettings-service';
import * as data from '../../../../assets/qtd-docs/clientSettings_notifications.json'
import { ClientSettings_LicenseUpdateOptions } from 'src/app/_DtoModels/ClientSettingsLicense/ClientSettings_LicenseUpdateOptions';
import {
  ClientSettings_LabelReplacement
} from "../../../_DtoModels/ClientSettingsLabelReplacement/ClientSettings_LabelReplacement";
import {
  ClientSettings_LabelReplacement_UpdateOptions
} from "../../../_DtoModels/ClientSettingsLabelReplacement/ClientSettings_LabelReplacement_UpdateOptions";
import {
  ClientSettings_GeneralSettings_UpdateOptions
} from "../../../_DtoModels/ClientSettingsGeneralSetting/ClientSettings_GeneralSettings_UpdateOptions";

import {jwtAuthHelper} from "../../../_Shared/Utils/jwtauth.helper";
import { ClientSettings_FeatureVM } from '@models/ClientSettingsFeature/ClientSettings_FeatureVM';

@Injectable({
  providedIn: 'root',
})

export class ApiClientSettingsService implements IClientSettingsService {
  baseUrl = environment.QTD;

  LabelReplacements: ClientSettings_LabelReplacement[];
  LabelReplacementsLoading: boolean;
  ClientSettingFeatures:ClientSettings_FeatureVM[];
  private pendingRequests: Array<{ resolve: (value: ClientSettings_LabelReplacement[]) => void, reject: (reason?: any) => void }> = [];

  constructor(private http: HttpClient) {
  }

  getNotifications = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `clientSettings/notifications`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getNotificationByName = (name: string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `clientSettings/notifications/` + name)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  updateNotification(
    notification: string,
    options: ClientSettings_NotificationUpdateOptions) {
    return firstValueFrom(this.http.put<ClientSettings_NotificationUpdateOptions>(this.baseUrl + `clientSettings/notifications/${notification}`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  // client database General settings services

  GetGeneralSettingsAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `clientSettings/general`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  UpdateGeneralSettings (
    options: ClientSettings_GeneralSettings_UpdateOptions) {
    return firstValueFrom(this.http.put<ClientSettings_NotificationUpdateOptions>(this.baseUrl + `clientSettings/general`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  //client label replacements service

  GetLabelReplacementsAsync = (refresh = false) => {

    if(!jwtAuthHelper.SelectedInstance)
    {
      this.LabelReplacements = [];
      return new Promise<ClientSettings_LabelReplacement[]>((resolve, reject) => {
        resolve(this.LabelReplacements);
      });
    }

    if (this.LabelReplacementsLoading) {
      return new Promise<ClientSettings_LabelReplacement[]>((resolve, reject) => {
        this.pendingRequests.push({ resolve, reject });
      });
    }

    if(this.LabelReplacements && !refresh)
      return new Promise<ClientSettings_LabelReplacement[]>((resolve, reject) => {
        resolve(this.LabelReplacements);
      });

    this.LabelReplacementsLoading = true;
    return firstValueFrom(this.http
      .get(this.baseUrl + `clientSettings/labelReplacements`)
      .pipe(
        map((res: any) => {
          this.LabelReplacements = res.locList as ClientSettings_LabelReplacement[];
          this.LabelReplacementsLoading = false;
          this.resolvePendingRequests(this.LabelReplacements);
          return this.LabelReplacements;
        })
      )
      );
  }

  private resolvePendingRequests(value: ClientSettings_LabelReplacement[]): void {
    this.pendingRequests.forEach(request => request.resolve(value));
    this.pendingRequests = [];
  }

  UpdateLabelReplacementsAsync (
    options: ClientSettings_LabelReplacement_UpdateOptions) {
    return firstValueFrom(this.http.put<ClientSettings_NotificationUpdateOptions>(this.baseUrl + `clientSettings/labelReplacements`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  //client license settings

  GetCurrentLicenseAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `clientSettings/license`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  GetCurrentLicenseVMAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `clientSettings/licenseVM`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  UpdateLicenseAsync (
    options: ClientSettings_LicenseUpdateOptions) {
    return firstValueFrom(this.http.put<ClientSettings_NotificationUpdateOptions>(this.baseUrl + `clientSettings/license`, options)
      .pipe(
        map((res: any) => {
          return res.license;
        })
      ));
  }

  getAllFeatureAsync(){

    if(!jwtAuthHelper.SelectedInstance)
    {
      this.ClientSettingFeatures = [];
      return new Promise<ClientSettings_FeatureVM[]>((resolve, reject) => {
        resolve(this.ClientSettingFeatures);
      });
    }  

    return firstValueFrom(this.http
      .get(this.baseUrl + `clientSettings/features`)
      .pipe(
        map((res: any) => {
          return res.featureList;          
        })
      )
      );
  }

  updateFeatureAsync (
    options: any) {
    return firstValueFrom(this.http.put<any>(this.baseUrl + `clientSettings/features`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  getAllTimeZones(){
    return firstValueFrom(this.http
      .get(this.baseUrl + `clientSettings/general/timezones`)
      .pipe(
        map((res:any) => {
          return res.timeZones ;          
        })
      ))
  }
}
