import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { TestType } from 'src/app/_DtoModels/TestType/TestType';
import { TestTypeCreateOptions } from 'src/app/_DtoModels/TestType/TestTypeCreateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TestTypeService {
  baseUrl = environment.QTD + 'testtype';
  constructor(private http: HttpClient) { }

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {
        return res['testType'] as TestType[];
      })
    ));
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['testType'] as TestType;
      })
    ));
  }

  create(options:TestTypeCreateOptions){
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
