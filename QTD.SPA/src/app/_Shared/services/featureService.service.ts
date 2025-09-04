import { Injectable } from "@angular/core";
import { promises } from "dns";
import { ApiClientSettingsService } from "src/app/_Services/QTD/ClientSettings/api.clientsettings.service";
import { DataBroadcastService } from "./DataBroadcast.service";
import { PublicClassScheduleRequestService } from "src/app/_Services/QTD/public-class-schedule-request.service";
import { jwtAuthHelper } from "../Utils/jwtauth.helper";
import { ActivatedRouteSnapshot } from "@angular/router";


@Injectable({
  providedIn: 'root'
})
export class FeatureService{
  publicClassFeatures: any
    constructor(private publicClassScheduleRequestService: PublicClassScheduleRequestService,
                private dataBroadcastService: DataBroadcastService,
                private apiClientSettingsService: ApiClientSettingsService ){}
    async getFeaturesData(instance: any ) : Promise<any> {      
      if(jwtAuthHelper.SelectedInstance){
       const features = await this.apiClientSettingsService.getAllFeatureAsync();
       this.publicClassFeatures = features.find((x) => x.feature == 'Public Classes')
      }
      else{
        this.publicClassFeatures = await this.publicClassScheduleRequestService.getPublicClassFeaturesAsync(instance); 
      }

        
        if (this.publicClassFeatures) {
          this.dataBroadcastService.publicClassEnabled.next(this.publicClassFeatures.enabled);
        }
        return this.publicClassFeatures;
    }

}