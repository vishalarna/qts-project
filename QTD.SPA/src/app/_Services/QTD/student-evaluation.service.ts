import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { StudentEvaluation } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation';
import { StudentEvaluationCreateOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationCreateOptions';
import { StudentEvaluationStatsVM } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationStatsVM';
import { StudentEvaluationWithoutEmp } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationWithoutEmp';
import { StudentEvaluation_SaveQuestion } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation_SaveQuestion';
import { StudentEvaluationHistoryCreateOptions } from 'src/app/_DtoModels/StudentEvaluationHistory/StudentEvaluationHistoryCreateOptions';
import { EMPSettingStudentEvaluationCreationOption, EMPSettingStudentEvaluationUpdateOption, StudentEvaluation_Question_LinkCreateOptions } from 'src/app/_DtoModels/StudentEvaluationQuestions/StudentEvaluation_Question_LinkCreateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StudentEvaluationService {
  baseUrl = environment.QTD + 'studentEvaluations';
  constructor(private http: HttpClient) {}
  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {

        return res['result'] as any[];
      })
    ));
  }

  getPublishedEvals(){
    return firstValueFrom(this.http.get(this.baseUrl + `/published`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }

  create(options: StudentEvaluationCreateOptions)
  {
    
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }
  makeActiveInactiveOrDelete(id:any,options:StudentEvaluationHistoryCreateOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  update(id:any,options:StudentEvaluationCreateOptions)
  {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res["result"];
      })
    ));
  }
  get(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res: any) => {

        return res['result'] as any;
      })
    ));
  }

  getWithScale(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/scale`).pipe(
      map((res: any) => {

        return res['result'] as any;
      })
    ));
  }

  getStatsCount() {
    return firstValueFrom(this.http.get(this.baseUrl + `/stats`).pipe(
      map((res: any) => {

        return res['stats'] as StudentEvaluationStatsVM;
      })
    ));
  }

  LinkQuestions(id: any, options: StudentEvaluation_Question_LinkCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/questions`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  UnLinkQuestions(id: any, options: StudentEvaluation_Question_LinkCreateOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/questions/unlink`, { body: options })
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }



  getLinkedQuestions(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/questions`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }
  publishEvaluation(id:any,options:StudentEvaluationHistoryCreateOptions)
  {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/publish`,options).pipe(
      map((res:any)=>{
        return res["publishEval"];
      })
    ));
  }


  studentEvaluationAddClasses(id: any, options: EMPSettingStudentEvaluationCreationOption) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/classes`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }
  studentEvaluationLinkUpdate(options: EMPSettingStudentEvaluationUpdateOption) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/link/classes`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }
  getStudentEvaluationClasses(ilaId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${ilaId}/classes`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }

  saveQuestions(options:StudentEvaluation_SaveQuestion){
    return firstValueFrom(this.http.post(this.baseUrl + `/SaveQuestion`,options).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }

  getSavedQuestions(evalId:any,classId:any,empId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${evalId}/class/${classId}/emp/${empId}`).pipe(
      map((res:any)=>{
        return res['result'] as StudentEvaluationWithoutEmp[];
      })
    ));
  }

  getList(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/list`)
      .pipe(
        map((res: any) => {
          return res['stats'] as any;
        })
      )
      );
  }
}
