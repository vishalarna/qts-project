import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { TaxonomyLevel } from 'src/app/_DtoModels/TaxonomyLevel/TaxonomyLevel';
import { TaxonomyLevelCreateOptions } from 'src/app/_DtoModels/TaxonomyLevel/TaxonomyLevelCreateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaxonomyLevelService {
  baseUrl = environment.QTD + 'taxonomylevel';
  constructor(private http: HttpClient) { }

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {
        return res['result'] as TaxonomyLevel[];
      })
    ));
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as TaxonomyLevel;
      })
    ));
  }

  create(options:TaxonomyLevelCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  delete(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }
}
