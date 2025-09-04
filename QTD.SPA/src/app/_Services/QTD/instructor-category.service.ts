import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Instructor_Category } from 'src/app/_DtoModels/Instructor_Category/Instructor_Category';
import { Instructor_CategoryOptions } from 'src/app/_DtoModels/Instructor_Category/Instructor_CategoryOptions';
import { Instructor_CategoryHistoryCreateOptions } from 'src/app/_DtoModels/Instructor_Category/Instructor_CategoryHistoryCreateOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InstructorCategoryService {
  baseUrl = environment.QTD + 'instructors/categories';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {

        return res['ins_CatList'] as Instructor_Category[];
      })
    ));
  }

  create(options: Instructor_Category)
  {
    
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['result'] as Instructor_Category;
      })
    ));
  }

  update(id:any,options:Instructor_Category){
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res["result"];
      })
    ));
  }

  get(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res: any) => {

        return res['sh_cat'] as Instructor_Category;
      })
    ));
  }

  saveSHCatHistory(options:Instructor_Category){
    return firstValueFrom(this.http.post(this.baseUrl + '/history',options).pipe(
      map((res:any)=>{
        return res["result"] as Instructor_Category;
      })
    ));
  }

  delete(options:Instructor_CategoryOptions){
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
  makeActiveInactiveOrDelete(id:any,options:Instructor_CategoryHistoryCreateOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  
}