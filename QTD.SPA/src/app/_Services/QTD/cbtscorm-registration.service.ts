import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClassRoasterUpdateOptions } from '@models/ClassRoasterUpdateOptions';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CBTScormRegistrationServiceService {
  baseUrl = environment.QTD + 'cbtScormRegistrations';

  constructor(private http: HttpClient) { }

  bulkUpdateCbtRegistrationsAsync = (classScheduleId:string, options: ClassRoasterUpdateOptions) =>  {
    return firstValueFrom(this.http.put(this.baseUrl + `/${classScheduleId}` + '/bulkUpdate',options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  };

  updateCbtRegistrationAsync = (employeeId:string, options: ClassRoasterUpdateOptions) =>  {
    return firstValueFrom(this.http.put(this.baseUrl + `/${employeeId}`,options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  };
}
