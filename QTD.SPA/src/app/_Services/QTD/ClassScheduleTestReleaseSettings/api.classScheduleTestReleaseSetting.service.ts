import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";


@Injectable({
    providedIn: 'root',
  })

export class ApiClassScheduleTestReleaseSettingService{
    baseUrl = environment.QTD + 'classschedule';

    constructor(private http: HttpClient) {}

    getClassScheduleTestEmpSettings(id:string){
        return firstValueFrom(this.http
          .get(this.baseUrl + `/testSettings/${id}`)
          .pipe(
            map((res: any) => {
              return res.result as any;
            })
          )
          );
      }

      createTestReleaseSettings(id:string){
        return firstValueFrom(this.http
          .post(this.baseUrl + `/testSettings/${id}`,{})
          .pipe(
            map((res: any) => {
              return res.result as any;
            })
          )
          );
      }

      updateTestReleaseSettings(id:string,options:any){
        return firstValueFrom(this.http
          .put(this.baseUrl + `/testSettings/${id}`,options)
          .pipe(
            map((res: any) => {
              return res.result as any;
            })
          )
          );
      }

}