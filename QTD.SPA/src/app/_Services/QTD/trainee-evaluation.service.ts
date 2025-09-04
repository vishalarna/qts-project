import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { TraineeEvaluationType } from 'src/app/_DtoModels/TraineeEvaluationType/TraineeEvaluationType';
import { TraineeEvaluationTypeCreateOptions } from 'src/app/_DtoModels/TraineeEvaluationType/TraineeEvaluationTypeCreateOptions';
import { TraineeEvaluationTypeOptions } from 'src/app/_DtoModels/TraineeEvaluationType/TraineeEvaluationTypeOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TraineeEvaluationService {
  baseUrl = environment.QTD + 'traineEvalTypes';
  constructor(private http:HttpClient) { }

  getAll(){
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as TraineeEvaluationType[];
      })
    ))
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as TraineeEvaluationType;
      })
    ));
  }

  create(options:TraineeEvaluationTypeCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ))
  }

  delete(id:any,options:TraineeEvaluationTypeOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }
}
