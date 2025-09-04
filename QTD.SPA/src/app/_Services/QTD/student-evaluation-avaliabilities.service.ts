import { StudentEvaluationAvaliabilities } from './../../_DtoModels/StudentEvaluationAvaliabilities/StudentEvaluationAvaliabilities';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { StudentEvaluationAvaliabilitiesCreateOptions } from 'src/app/_DtoModels/StudentEvaluationAvaliabilities/StudentEvaluationAvaliabilitiesCreateOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentEvaluationAvaliabilitiesService {
  baseUrl = environment.QTD + 'studentevaluationavailability';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['studentEvaluationAvailabilities'] as StudentEvaluationAvaliabilities[];
      })
    ));
  }

  create(options: StudentEvaluationAvaliabilitiesCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {

        return res
      })
    ));
  }
}
