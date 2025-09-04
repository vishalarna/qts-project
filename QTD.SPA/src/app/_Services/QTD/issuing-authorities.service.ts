import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Procedure_IssuingAuthorityOptions } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthorityOptions';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { Procedure_IssuingAuthorityCreateOptions } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthorityCreateOptions';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class IssuingAuthoritiesService {
  baseUrl = environment.QTD + 'issuingAuthorities';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {

        return res['iaList'] as Procedure_IssuingAuthority[];
      })
    ));
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['ia'] as Procedure_IssuingAuthority;
      })
    ));
  }

  create(options: Procedure_IssuingAuthorityCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {

        return res['ia'] as Procedure_IssuingAuthority;
      })
    ));
  }

  update(id:any, options:Procedure_IssuingAuthorityCreateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res['result'] as Procedure_IssuingAuthority;
      })
    ));
  }

  delete(ilaId:any,linkIds:Procedure_IssuingAuthorityOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + '/' + ilaId, { body:linkIds }).pipe(
      map((res:any)=>{
        return res
      })
    ))
  }

  makeActiveInactiveOrDelete(id:any,options:Procedure_IssuingAuthorityOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }
}
