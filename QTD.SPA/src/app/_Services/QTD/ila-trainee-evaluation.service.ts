import { DiscussionQuestion } from './../../_DtoModels/DiscussionQuestion/DiscussionQuestion';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { DiscussionQuestionCreateOptions } from 'src/app/_DtoModels/DiscussionQuestion/DiscussionQuestionCreateOptions';
import { ILATraineeEvaluation, IlaTraineeEvaluationList } from 'src/app/_DtoModels/ILATraineeEvaluation/ILATraineeEvaluation';
import { ILATraineeEvaluationCreateOptions } from 'src/app/_DtoModels/ILATraineeEvaluation/ILATraineeEvaluationCreateOptions';
import { TraineeEvaluationTypeOptions } from 'src/app/_DtoModels/TraineeEvaluationType/TraineeEvaluationTypeOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IlaTraineeEvaluationService {
  baseUrl = environment.QTD + 'ilatraineeevaluation';
  constructor(private http: HttpClient) { }

  getAll() {
    return firstValueFrom(this.http
    .get(this.baseUrl)
    .pipe(
      map((res: any) => {

        return res['iLATraineeEvaluation'] as ILATraineeEvaluation[];
      })
    )
    );
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['iLATraineeEvaluation'] as ILATraineeEvaluation;
      })
    ));
  }

  getByIla(ilaId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/ila/${ilaId}`).pipe(
      map((res:any)=>{
        return res['result'] as IlaTraineeEvaluationList[];
      })
    ));
  }

  changeTraineeEvaluationStatus(data:any){
    return firstValueFrom(this.http.post(this.baseUrl + `/status`,data).pipe(
      map((res:any)=>{
        return res['result'] as boolean[];
      })
    ));
  }
  create(options:ILATraineeEvaluationCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['result'] as ILATraineeEvaluation;
      })
    ));
  }

  update(id:any,options:ILATraineeEvaluationCreateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  delete(id:any,options:TraineeEvaluationTypeOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  deletePerformType(ilaId:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/perform/ila/${ilaId}`).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }
  createDiscussion(options:DiscussionQuestionCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/discussionQuestion`,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  getDiscussionQuestions(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/discussionQuestion`).pipe(
      map((res:any)=>{
        return res['result'] as DiscussionQuestion[];
      })
    ));
  }

  DeleteAllQuestions(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/discussionQuestion`).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  copyEvaluation(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/copy`).pipe(
      map((res:any)=>{
        return res['result'] as ILATraineeEvaluation;
      })
    ));
    }

    //getDiscussion(id: any) {
    //    return this.http
    //        .get(this.baseUrl + `/discussionQuestion/${id}`)
    //        .pipe(
    //            map((res: any) => {

    //                return res['result'] as DiscussionQuestion;
    //            })
    //        )
    //        .toPromise();
    //}
}
