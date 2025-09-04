import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { EvaluationMethod } from 'src/app/_DtoModels/EvaluationMethod/EvaluationMethod';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EvaluationMethodService {
  baseUrl = environment.QTD + 'evalMethod';
  constructor(private http:HttpClient) { }

  getAll(){
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as EvaluationMethod[];
      })
    ));
  }
}
