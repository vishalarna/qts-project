import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { QuestionBankCreateOptions } from 'src/app/_DtoModels/QuestionBank/QuestionBankCreateOptions';
import { QuestionBankHistoryCreateOptions } from 'src/app/_DtoModels/QuestionBankHistory/QuestionBankHistoryCreateOptions';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class QuestionBankService {
  baseUrl = environment.QTD + 'questionBank';
  constructor(private http: HttpClient) {}
  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {

        return res['questions'] as any[];
      })
    ));
  }
  create(options: QuestionBankCreateOptions)
  {
    
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }
  makeActiveInactiveOrDelete(id:any,options:QuestionBankHistoryCreateOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  update(id:any,options:QuestionBankCreateOptions)
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

        return res['question'] as any;
      })
    ));
  }
  createAndLinkQuestion(options: QuestionBankCreateOptions)
  {
    
    return firstValueFrom(this.http.post(this.baseUrl+'/createAndLinkQuestion',options).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }
}
