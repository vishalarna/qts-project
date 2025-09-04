import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ILA_StudentEvaluation_LinkOption, ILA_StudentEvaluation_LinkOptions } from 'src/app/_DtoModels/ILA_StudentEvaluation_Link/ILA_StudentEvaluation_LinkOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IlaStudentEvaluationLinkService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'ila/';

  create(id: any, options: ILA_StudentEvaluation_LinkOptions) {
    
    return firstValueFrom(this.http.post(this.baseUrl + id + "/studentEvaluation", options).pipe(
      map((res: any) => {
        return res;
      })
    ))
  }


  createLinks(id: any, options: ILA_StudentEvaluation_LinkOption) {
    
    return firstValueFrom(this.http.post(this.baseUrl + id + "/LinkstudentEvaluation", options).pipe(
      map((res: any) => {
        return res;
      })
    ))
  }




   delete(id:any,options:ILA_StudentEvaluation_LinkOptions){
    
    return firstValueFrom(this.http.delete(this.baseUrl + id + "/studentEvaluation", { body:options }).pipe(
      map((res:any)=>{
        return res
      })
    ))
  }

  unlinkAll(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `${id}/studentEvaluation/all`).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getLinkedStudentEvaluations(ilaId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `${ilaId}/studentEvaluation`).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }
  getLinkedStudentEvaluationsData(ilaId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `${ilaId}/LinkstudentEvaluation`).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }
}
