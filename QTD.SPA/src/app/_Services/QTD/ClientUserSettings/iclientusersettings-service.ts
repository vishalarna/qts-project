import { ApiClientUserSettingsService } from './api.clientusersettings.service';
import { StubClientUserSettingsService } from './stub.clientusersettings.service';
import { environment } from 'src/environments/environment';
import {HttpClient} from "@angular/common/http";
import { CustomizeDashboardUpdateOptions } from 'src/app/_DtoModels/ClientUserSettingsDashboard/CustomizeDashboardUpdateOptions';


export interface IClientUserSettingsService {
  
  GetDashboardSettingsAsync:()=>Promise<any>;
  UpdateDashboardSettingsAsync : (options: CustomizeDashboardUpdateOptions)=>Promise<any>;
  }

function clientUserSettingsServiceFactory(http: HttpClient) {
  if(environment.Storybook_UseStub){
    return new StubClientUserSettingsService();
  }
  else{
    return new ApiClientUserSettingsService(http);
  }
}

export const clientUserSettingsServiceProvider = {
  provide: ApiClientUserSettingsService,
  useFactory: clientUserSettingsServiceFactory,
  deps: [HttpClient]
};
