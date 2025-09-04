import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { RRIssuingAuthority } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthority';
import { RR_IssuingAuthorityCompact } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityCompact';
import { RRIssuingAuthorityCreateOptions } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityCreateOptions';
import { RR_IssuingAuthorityOptions } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RRIssuingAuthorityService {
  baseUrl = environment.QTD + 'rr/issuingauthority';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as RRIssuingAuthority[];
      })
    ));
  }

  getAllWithoutIncludes() {
    return firstValueFrom(this.http.get(this.baseUrl + `/withoutInclude`).pipe(
      map((res:any)=>{
        return res['result'] as RRIssuingAuthority[];
      })
    ));
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as RRIssuingAuthority;
      })
    ));
  }

  create(options: RRIssuingAuthorityCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {

        return res['result'] as RRIssuingAuthority;
      })
    ));
  }

  update(id:any,options:RRIssuingAuthorityCreateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res['result'] as RRIssuingAuthority;
      })
    ));
  }

  delete(id:any, options:RR_IssuingAuthorityOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  GetRRWithIA(){
    return firstValueFrom(this.http.get(this.baseUrl + '/include').pipe(
      map((res:any)=>{
        return res['result'] as RR_IssuingAuthorityCompact[];
      })
    ));
  }
}
