import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PublicallyAvailableClassesVm } from "@models/PublicClasses/PublicallyAvailableClasses";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root',
})
export class PublicPortalClassScheduleService{
     baseUrl = environment.QTD + 'public/classSchedules';
        constructor(private http: HttpClient,
            ) { }

            getAvailableClassSchedules(instanceName: string){
                    return firstValueFrom(this.http.get(this.baseUrl + `/${instanceName}`)
                    .pipe(
                        map((res:any)=>{
                          return res["classSchedules"] as PublicallyAvailableClassesVm[];
                        })
                      ));
                }
}