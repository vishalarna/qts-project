import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ClassSchedule_SelfRegCreateOptions } from "@models/SchedulesClassses/ClassSchedule_SelfRegCreateOptions";
import { TranslateService } from "@ngx-translate/core";
import { firstValueFrom, Observable, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { ClassSchedules } from "src/app/_DtoModels/SchedulesClassses/ClassSchedules";
import { SweetAlertService } from "src/app/_Shared/services/sweetalert.service";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root',
})

export class ClassScheduleService {
    baseUrl = environment.QTD + 'schedules';
    constructor(private http: HttpClient,
        private alertService: SweetAlertService,
        private translate: TranslateService,
        private router: Router) { }

    getCBTScormRegistrationLink(classScheduleId: number) {
        return firstValueFrom(this.http
            .post(environment.QTD + `emp/classSchedule/${classScheduleId}/startCourse`, {})
            .pipe(map((res => {
                return res;
            })),
            catchError((error: HttpErrorResponse) => {
                if (error.status < 200 || error.status > 299) {
                    this.router.navigate(['/auth']);
                } else {
                    this.alertService.errorAlert(error.message);
                }
                return throwError(error);
            })));
        }

    GetByILAIdAsync(ilaId: number){
        return firstValueFrom(this.http.get(this.baseUrl + '/byILA/' +ilaId)
        .pipe(
            map((res:any)=>{
              return res["schedules"] as ClassSchedules[];
            })
          ));
    }

    GetClassScheduleSelfRegistrationAsync(scheduleId:any){
        return firstValueFrom(this.http.get(this.baseUrl + `/${scheduleId}/selfReg`)
        .pipe(
            map((res:any) => {
                return res['result'] as any;
            })
        ));
    }

    CreateClassScheduleSelfRegistrationAsync(options: ClassSchedule_SelfRegCreateOptions){
        return firstValueFrom(this.http.post(this.baseUrl + `/selfReg`,options)
        .pipe(
            map((res:any) => {
                return res['result'] as any;
            })
        ));
    }

    GetByStartDateAndEndDate(startDate: Date, endDate: Date){
        return firstValueFrom(this.http.get(this.baseUrl + `/schedules/startDate/${startDate}/endDate/${startDate}`)
            .pipe( map((res:any) => {
                return res["schedules"] as ClassSchedules[];
            })
        ));
    }
}