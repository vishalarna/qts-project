import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { SaftyHazard_CategoryHistory } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryHistory';
import { SaftyHazard_CategoryOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SafetyHazardCategoryService {
  baseUrl = environment.QTD + 'saftyHazards/categories';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {

        return res['sh_CatList'] as SaftyHazard_Category[];
      })
    ));
  }

  create(options: SaftyHazard_Category){
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['result'] as SaftyHazard_Category;
      })
    ));
  }

  update(id:any,options:SaftyHazard_Category){
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res["result"] as SaftyHazard_Category;
      })
    ));
  }

  get(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res: any) => {

        return res['sh_cat'] as SaftyHazard_Category;
      })
    ));
  }

  saveSHCatHistory(options:SaftyHazard_CategoryHistory){
    return firstValueFrom(this.http.post(this.baseUrl + '/history',options).pipe(
      map((res:any)=>{
        return res["result"] as SaftyHazard_CategoryHistory;
      })
    ));
  }

  delete(options:SaftyHazard_CategoryOptions){
    return firstValueFrom(this.http.delete(this.baseUrl,{ body: options }).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  getCount(){
    return firstValueFrom(this.http.get(this.baseUrl + `/count`).pipe(
      map((res:any)=>{
        return res['result'] as number;
      })
    ));
  }
}
