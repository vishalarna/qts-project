import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { DIFSurveyTaskLinkOptions } from '@models/DIFSurvey/DIFSurveyTaskLinkOptions';
import { DIFSurveyTaskVM } from '@models/DIFSurvey/DIFSurveyTaskVM';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiDifSurveyTaskService  {
  baseUrlWithNav = environment.QTD + 'difSurvey/tasks';
  baseUrl = environment.QTD;

  constructor(private http: HttpClient) {}

 
  linkTasksAsync = (options: DIFSurveyTaskLinkOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrlWithNav, options)
      .pipe(
        map((res: any) => {
          return res.result as DIFSurveyTaskVM[];
        })
      )
      );
  };
  unlinkTasksAsync = (difSurveyTaskId:string) => {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `difSurveyTask/${difSurveyTaskId}`,{ observe: 'response' })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  };

}
