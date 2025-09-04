import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ClassScheduleTQEMPSettingsVM } from "@models/SchedulesClassses/ClassScheduleTQEMPSettingsVM";
import { ClassScheduleEvaluatorLinksVM, ClassScheduleTQEMPSettingsCreateOptions } from "@models/SchedulesClassses/training-creation-options";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";


@Injectable({
    providedIn: 'root',
  })

export class ApiClassScheduleTqReleaseSettingService{
    baseUrl = environment.QTD + 'classschedule';

    constructor(private http: HttpClient) {}

    getClassScheduleTQEMPSettings(id:string){
        return firstValueFrom(this.http
          .get(this.baseUrl + `/tqEMPSettings/${id}`)
          .pipe(
            map((res: any) => {
              return res.result as ClassScheduleTQEMPSettingsVM;
            })
          )
          );
      }

      createClassScheduleTQEMPSettings(id:string){
        return firstValueFrom(this.http
          .post(this.baseUrl + `/tqEMPSettings/${id}`,{})
          .pipe(
            map((res: any) => {
              return res.result as any;
            })
          )
          );
      }

      updateClassScheduleTQEMPSettings(id:string,options: ClassScheduleTQEMPSettingsCreateOptions){
        return firstValueFrom(this.http
          .put(this.baseUrl + `/tqEMPSettings/${id}`,options)
          .pipe(
            map((res: any) => {
              return res.result as ClassScheduleTQEMPSettingsVM;
            })
          )
          );
      }

      linkEvaluatorsFromClassSchedule(options : ClassScheduleEvaluatorLinksVM ){
        return firstValueFrom(this.http
          .post(this.baseUrl + `/${options.classScheduleId}/tqEMPSettings/linkEvaluators`,options)
          .pipe(
            map((res: any) => {
              return res.result as any;
            })
          )
          );
      }

      unlinkEvaluatorsFromClassSchedule(options :ClassScheduleEvaluatorLinksVM ) {
        return firstValueFrom(this.http
          .delete(this.baseUrl + `/${options.classScheduleId}/tqEMPSettings/unlinkEvaluators`,{body : options})
          .pipe(
            map((res: any) => {
              return res;
            })
          )
          );
      }

      linkEvaluatorsFromILAEvaluator(id:string){
        return firstValueFrom(this.http
          .post(this.baseUrl + `/tqEvaluators/${id}`,{})
          .pipe(
            map((res: any) => {
              return res.result as any;
            })
          )
          );
      }

      getClassScheduleTQEvaluators(id:string){
        return firstValueFrom(this.http
          .get(this.baseUrl + `/tqEvaluators/${id}`,{})
          .pipe(
            map((res: any) => {
              return res.result as any;
            })
          )
          );
      }

}