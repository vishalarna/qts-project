import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { IDPScheduleUpdateOption } from 'src/app/_DtoModels/IDP/IDPScheduleUpdateOption';
import { IDPVM } from 'src/app/_DtoModels/IDP/IDPVM';
import { IDP_Review } from 'src/app/_DtoModels/IDP/IDP_Review';
import { IDP_ReviewCreateOptions } from 'src/app/_DtoModels/IDP/IDP_ReviewCreateOptions';
import { IDP_ScoreOptions } from 'src/app/_DtoModels/IDP/IDP_ScoreOptions';
import { IlaLinkEmployeesOptions } from 'src/app/_DtoModels/ILA/IlaLinkEmployeesOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IdpService {
  baseUrl = environment.QTD + 'idp';
  constructor(private http:HttpClient) { }

  getAllIdps(empId:any, year: number){
    return firstValueFrom(this.http.get(this.baseUrl + `/${empId}/${year}`).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }

  getAllIDPReviews(empId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/review/${empId}`).pipe(
      map((res:any)=>{
        return res['result'] as IDP_Review[];
      })
    ));
  }

  createIDPReview(options:IDP_ReviewCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/review`,options).pipe(
      map((res:any)=>{
        return res['result'] as IDP_Review;
      })
    ));
  }

  updateIDPScore(options:IDP_ScoreOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/score`,options).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }

  
  updateIDPDate(options:IDPScheduleUpdateOption){
    return firstValueFrom(this.http.put(this.baseUrl + `/date`,options).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }

  linkILA(id, options:IlaLinkEmployeesOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/linkEmployee/ILA/${id}`,options).pipe(
      map((res:any)=>{
        return res['result'] as IDP_Review;
      })
    ));
  }

  getAllLinkedSchedulingClassesIdps(id: any,empId: any ) {
    return firstValueFrom(this.http.get(`${this.baseUrl}/${id}/IDPSchedule/${empId}`).pipe(
        map((res: any) => {
            return res['result'];
        })
    ));
  }

  deleteAsync(id:string){
    return firstValueFrom(this.http.delete(`${this.baseUrl}/${id}`).pipe(
      map((res: any) => {
          return res['result'];
      })
  ));
  }
}
