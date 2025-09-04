import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { StudentEvaluationQuestion } from 'src/app/_DtoModels/StudentEvaluationQuestion/StudentEvaluationQuestion';
import { StudentEvaluationQuestionCreateOptions } from 'src/app/_DtoModels/StudentEvaluationQuestion/StudentEvaluationQuestionCreateOptions';
import { QuestionBank } from 'src/app/_DtoModels/QuestionBank/QuestionBank';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentEvaluationQuestionService {
  baseUrl = environment.QTD + 'studentEvaluationQuestion';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as StudentEvaluationQuestion[];
      })
    ));
  }

  get(id:any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
    map((res: any) => {
      return res['result'] as StudentEvaluationQuestion[];
    })
  ));
}

  create(options: StudentEvaluationQuestionCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {

        return res['result'] as StudentEvaluationQuestion;
      })
    ));
  }

  getStudentEvalQuestionsForEval(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/eval/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as QuestionBank[];
      })
    ));
  }
}
