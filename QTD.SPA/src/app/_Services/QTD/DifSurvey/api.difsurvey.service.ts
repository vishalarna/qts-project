import { Injectable } from '@angular/core';
import { IDifSurveyService } from './idifsurvey-service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { DIFSurvey_CreateOptions } from '@models/DIFSurvey/DIFSurvey_CreateOptions';
import { DIFSurveyEmployeeVM } from '@models/DIFSurvey/DIFSurveyEmployeeVM';
import { DIFSurvey } from '@models/DIFSurvey/DIFSurvey';
import { DIFSurvey_UpdateOptions } from '@models/DIFSurvey/DIFSurvey_UpdateOptions';
import { DIFSurveyVM } from '@models/DIFSurvey/DIFSurveyVM';
import { DIFResult_UpdateOptions } from '@models/DIFSurvey/DIFResult_UpdateOptions';
import { DIFSurveyEmployeResponseOptions } from '@models/DIFSurvey/DIFSurveyEmployeResponseOptions';
import { DIFSurveyViewResponseVm } from '@models/DIFSurvey/DifSurveyViewResponseVm';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiDifSurveyService implements IDifSurveyService {
  baseUrl = environment.QTD + 'difsurvey';
  private baseUrlEmp = environment.QTD + 'emp/difSurvey';

  constructor(private http: HttpClient) {}

  getAllAsync = () => {
    return firstValueFrom(this.http
    .get(this.baseUrl + '/overview')
    .pipe(
      map((res: any) => {
        return res.result;
      })
    )
    );
  }

  getAsync = (id:string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res.result as DIFSurveyVM;
        })
      )
      );
  };

  editDIFSurveyAsync = (id:string,editType: string) =>  {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}` + '/edit' + `/${editType}`,null)
      .pipe(
        map((res: any) => {
          return res.result;
        })
      )
      );
  };

  createAsync = (options: DIFSurvey_CreateOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/create', options)
      .pipe(
        map((res: any) => {
          return res.result as DIFSurveyVM;
        })
      )
      );
  };

  updateAsync = (id:string,options: DIFSurvey_UpdateOptions) => {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res.result as DIFSurveyVM;
        })
      )
      );
  };


  getAllDIFSurveyCompleted(){
    return firstValueFrom(this.http
    .get(this.baseUrlEmp + '/completed')
    .pipe(
      map((res: any) => {
        return res['result'] as DIFSurveyEmployeeVM[];
      })
    )
    );
  }
  getAllDIFSurveyPending(){
    return firstValueFrom(this.http
    .get(this.baseUrlEmp + '/pending')
    .pipe(
      map((res: any) => {
        return res['result'] as DIFSurveyEmployeeVM[];
      })
    )
    );
  }

  getResultByIdAsync = (id:string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/results/${id}`)
      .pipe(
        map((res: any) => {
          return res.result as DIFSurvey;
        })
      )
      );
  };

  getTaskTrainingFrequency=()=>{
    return firstValueFrom(this.http
    .get(this.baseUrl + '/trainingFrequency')
    .pipe(
      map((res: any) => {
        return res['result'];
      })
    )
    );
  }

  getDifSurveyTaskStatus=()=>{
    return firstValueFrom(this.http
    .get(this.baseUrl + '/taskStatus')
    .pipe(
      map((res: any) => {
        return res['result'];
      })
    )
    );
  }

  updateDIFResultsAsync = (id:string,options: DIFResult_UpdateOptions) => {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/task/${id}`, options)
      .pipe(
        map((res: any) => {
          return res.result as any;
        })
      )
      );
  };

  getAllEmployeeResponses(id:string){
    return firstValueFrom(this.http
    .get(this.baseUrlEmp +`/${id}/employeeResponses`)
    .pipe(
      map((res: any) => {
        return res.result as DIFSurveyViewResponseVm;
      })
    )
    );
  }
  
  getEnrollmentsByIdAsync = (id:string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/enrollments`)
      .pipe(
        map((res: any) => {
          return res.result as DIFSurveyVM;
        })
      )
      );
  };

  updateDIFEmployeeResponsesAsync = (id:string,options: DIFSurveyEmployeResponseOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrlEmp + `/${id}/employeeResponses/update`, options)
      .pipe(
        map((res: any) => {
          return res as any;
        })
      )
      );
  };

  completeDIFEmployeeResponsesAsync = (id:string,options: DIFSurveyEmployeResponseOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrlEmp + `/${id}/employeeResponses/complete`, options)
      .pipe(
        map((res: any) => {
          return res as any;
        })
      )
      );
  };
}
