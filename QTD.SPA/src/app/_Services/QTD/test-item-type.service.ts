import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { TestItemTypeCreateOptions } from 'src/app/_DtoModels/TestItemType/TestItemTypeCreateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TestItemTypeService {
  baseUrl = environment.QTD + 'testitemtype';
  constructor(private http: HttpClient) { }

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {
        return res['testItemType'] as TestItemType[];
      })
    ));
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['testItemType'] as TestItemType;
      })
    ));
  }

  create(options:TestItemTypeCreateOptions){
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
