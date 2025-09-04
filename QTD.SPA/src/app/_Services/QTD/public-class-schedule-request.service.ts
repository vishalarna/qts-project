import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreatePublicClassScheduleRequestModel } from "@models/PublicClasses/CreatePublicClassScheduleRequestModel";
import { PublicClassScheduleRequestVM } from "@models/PublicClasses/PublicClassScheduleRequestVM";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root',
})
export class PublicClassScheduleRequestService{
        baseUrl = environment.QTD + 'public';
        baseUrl1 = environment.QTD + 'publicClassScheduleRequest'
                constructor(private http: HttpClient,
                    ) { }

        createpublicClassScheduleRequestAsync(instanceName: string,classScheduleId:string, options: CreatePublicClassScheduleRequestModel){
            return firstValueFrom(this.http.post(this.baseUrl + `/publicClassScheduleRequest/${instanceName}/${classScheduleId}` , options)
            .pipe(
                map((res:any) => {
                    return res as any;
                })
            ));
        }

        getPublicRequestsAsync(){
          return firstValueFrom(this.http.get(this.baseUrl1 )
          .pipe(
              map((res:any)=>{
                return res["result"] as PublicClassScheduleRequestVM[];
              })
            ));
      }
      getRequestStatsAsync(){
        return firstValueFrom(this.http.get(this.baseUrl1 + `/stats`)
          .pipe(
              map((res:any)=>{
                return res["result"] as any;
              })
            ));
      }

      updatePublicClassScheduleRequestAsync(id: string, options: PublicClassScheduleRequestVM){
        return firstValueFrom(this.http
      .put(this.baseUrl1 + `/${id}`, options)
      .pipe(
        map((res: any) => {
           return res["result"] as any;
        })
      )
      );
      }
      
    getNercCertList(instanceName: string) {
      return firstValueFrom(this.http
        .get(this.baseUrl + `/nerc/${instanceName}`)
        .pipe(
          map((res: any) => {
            return res['result'] as any;
          })
        )
        );
    }
    
    getAllPublicOrganization(instanceName:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/organizations/${instanceName}`)
      .pipe(
        map((res: any) => {

          return res['organizations'] as any[];
        })
      )
      );
  }

  getPublicClassFeaturesAsync(instanceName:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${instanceName}/clientSetting/feature`)
      .pipe(
        map((res: any) => {

          return res['publicClassFeature'] as any;
        })
      )
      );
  }

  getPublicClassCompanyLogoAsync(instanceName:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${instanceName}/clientSetting/companyLogo`)
      .pipe(
        map((res: any) => {

          return {
          companyName: res.companyName,
          companyLogo: res.companyLogo
        };
        })
      )
      );
  }

  GetILACompletionRequirementAsync(instanceName:string, ilaId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${instanceName}/ila/${ilaId}`)
      .pipe(
        map((res: any) => {

          return res['iLACompletionRequirement'] as any[];
        })
      )
      );
  }
}
