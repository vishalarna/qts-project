import { StudentEvaluationForms } from './../../_DtoModels/StudentEvaluationForms/StudentEvaluationForms';
import { RatingScaleCreateOptions } from '../../_DtoModels/RatingScale/RatingScaleCreateOptions';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { StudentEvaluationFormsCreateOptions } from 'src/app/_DtoModels/StudentEvaluationForms/StudentEvaluationFormsCreateOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentEvaluationFormService {
  baseUrl = environment.QTD + 'studentEvaluationForm';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as StudentEvaluationForms[];
      })
    ));
  }

  create(options: StudentEvaluationFormsCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {

        return res
      })
    ));
  }
}
