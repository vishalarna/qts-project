import { Injectable } from '@angular/core';
import { ITrainingProgramReview } from './itrainingProgramReview-service'
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';
import { TrainingProgramReview_OverviewViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_OverviewViewModel';
import { TrainingProgramTrainingIssueLinkOption } from '@models/TrainingProgram/TrainingProgramTrainingIssueLinkOption';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiTrainingProgramReviewService implements ITrainingProgramReview {
  baseUrl = environment.QTD + 'trainingProgramReviews';
  baseUrl1 = environment.QTD + 'trainingProgram';

  constructor(private http: HttpClient) { }
  getOverviewAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/overview`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingProgramReview_OverviewViewModel;
        })
      )
      );
  }
  
  getTrainingProgramReviewAsync = (programReviewId:string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${programReviewId}`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingProgramReview_ViewModel;
        })
      )
      );
  }

  createAsync = (options:TrainingProgramReview_ViewModel) => {
    return firstValueFrom(this.http.post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res.result as TrainingProgramReview_ViewModel;
        })
      ));
  } 

  updateAsync = (options:TrainingProgramReview_ViewModel,programReviewId:string) => {
    return firstValueFrom(this.http.put(this.baseUrl +"/" + programReviewId, options)
      .pipe(
        map((res: any) => {
          return res.result as TrainingProgramReview_ViewModel;
        })
      ));
  }

  copyAsync = (id: string) => {
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}`, null)
      .pipe(
        map((res: any) => {
          return res.result as TrainingProgramReview_ViewModel;
        })
      ));
  }

  activateAsync = (id: any) => {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/activate`, null)
      .pipe(
        map((res: any) => {
          return res as any;
        })
      ));
  }

  inactivateAsync = (id: any) => {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/inactivate`, null)
      .pipe(
        map((res: any) => {
          return res as any;
        })
      ));
  }

  deleteAsync = (id: any) => {
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  getTrainingProgramTrainingIssuesLinks = (id:any)=>{
    return firstValueFrom(this.http.get(this.baseUrl1 + `/${id}/trainingissues`)
      .pipe(
        map((res: any) => {
          return res.result;
        })
      ));
  }

  createTrainingProgramTrainingIssuesLinks = (id:any,options:TrainingProgramTrainingIssueLinkOption)=>{
    return firstValueFrom(this.http.post(this.baseUrl1 + `/${id}/trainingissues`,options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  removeTrainingProgramTrainingIssuesLinks = (id: any, options: TrainingProgramTrainingIssueLinkOption) => {
    return firstValueFrom(this.http.put(this.baseUrl1 + `/${id}/trainingissues`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

}
