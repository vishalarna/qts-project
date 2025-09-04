import { Injectable } from '@angular/core';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';

@Injectable({
  providedIn: 'root'
})
export class LicenseHelperService {
  private localStorageKey = 'licenseData';

  constructor(
    private clientSettingService: ApiClientSettingsService,
  ) { }

  async setLicenseData(): Promise<any> {
    var licenseSettingData = await this.clientSettingService.GetCurrentLicenseAsync();
    localStorage.setItem(this.localStorageKey, JSON.stringify(licenseSettingData));
    return licenseSettingData;
  }

  getLicenseData(): any {
    const data = localStorage.getItem(this.localStorageKey);
    return data ? JSON.parse(data) : null;
  }
  removeLicenseData(){
    localStorage.removeItem(this.localStorageKey);
  }
}

