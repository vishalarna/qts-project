import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map} from 'rxjs/operators';
import {environment} from 'src/environments/environment';
import {IClientUserSettingsService} from './iclientusersettings-service';
import { ClientUserSettings_Dashboard_UpdateOptions } from 'src/app/_DtoModels/ClientUserSettingsDashboard/ClientUserSettings_Dashboard_UpdateOptions';
import { CustomizeDashboardUpdateOptions } from 'src/app/_DtoModels/ClientUserSettingsDashboard/CustomizeDashboardUpdateOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class ApiClientUserSettingsService implements IClientUserSettingsService {
  baseUrl = environment.QTD;

  constructor(private http: HttpClient) {
  }


  // client user dashboard settings services

  GetDashboardSettingsAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `clientUserSettings/dashboard`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  UpdateDashboardSettingsAsync (
    options: CustomizeDashboardUpdateOptions) {
    return firstValueFrom(this.http.put<CustomizeDashboardUpdateOptions>(this.baseUrl + `clientUserSettings/dashboard`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }
}
