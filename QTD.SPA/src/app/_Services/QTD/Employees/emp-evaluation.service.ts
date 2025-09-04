import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { EmpEvaluationVM } from 'src/app/_DtoModels/EmpEvaluation/EmpEvaluationVM';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmpEvaluationService {
  baseUrl = environment.QTD + 'emp/evaluations';
  constructor(private http:HttpClient) { }

  getAllEvaluations(){
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as EmpEvaluationVM[];
      })
    ));
  }

  startEvaluation(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/start/${id}`).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }
  // updateIDPScore(options:IDP_ScoreOptions){
  //   return this.http.put(this.baseUrl + `/score`,options).pipe(
  //     map((res:any)=>{
  //       return res['result'];
  //     })
  //   ).toPromise();
  // }



}
