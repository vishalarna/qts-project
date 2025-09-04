import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { DIFSurveyEmployeeLinkUnlinkOptions } from '@models/DIFSurvey/DIFSurveyEmployeeLinkUnlinkOptions';
import { DIFSurvey_EmployeeVM } from '@models/DIFSurvey/DIFSurvey_EmployeeVM';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiDifSurveyEmployeeService  {
  baseUrlWithNav = environment.QTD + 'difSurvey/employees';
  baseUrl = environment.QTD;

  constructor(private http: HttpClient) {}

 
  linkEmployeesAsync = (options: DIFSurveyEmployeeLinkUnlinkOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrlWithNav, options)
      .pipe(
        map((res: any) => {
          return res.result as DIFSurvey_EmployeeVM[];
        })
      )
      );
  };
  unlinkEmployeesAsync = (options: DIFSurveyEmployeeLinkUnlinkOptions) => {
    return firstValueFrom(this.http
      .delete(this.baseUrlWithNav, { body: options, observe: 'response' })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  };
}


