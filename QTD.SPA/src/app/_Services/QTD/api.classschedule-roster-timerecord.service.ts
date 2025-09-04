import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ClassScheduleRosterTimeRecordVM } from "@models/ClassScheduleRoster-TimeRecord/classScheduleRosterTimeRecord_VM";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";


@Injectable({
    providedIn: 'root',
  })

export class ApiClassScheduleRosterTimeRecordService{

    baseUrl = environment.QTD + 'classscheduleroster-timerecord';
    
    constructor(private http: HttpClient) {}

    createAsync(options: ClassScheduleRosterTimeRecordVM) {
        return firstValueFrom(this.http
          .post(this.baseUrl, options)
          .pipe(
            map((res: any) => {
              return res
            })
          ));
      }
}