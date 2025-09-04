import { StudentEvaluationAudiences } from './../../_DtoModels/StudentEvaluationAudiences/StudentEvaluationAudiences';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { StudentEvaluationAudiencesCreateOptions } from 'src/app/_DtoModels/StudentEvaluationAudiences/StudentEvaluationAudiencesCreateOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentEvaluationAudiencesService {
  baseUrl = environment.QTD + 'studentevaluationaudience';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['studentEvaluationAudience'] as StudentEvaluationAudiences[];
      })
    ));
  }

  create(options: StudentEvaluationAudiencesCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {

        return res
      })
    ));
  }
}
