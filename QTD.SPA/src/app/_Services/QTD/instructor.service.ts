import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Instructor } from 'src/app/_DtoModels/Instructors/Instructor';
import { InstructorLatestActivityVM } from 'src/app/_DtoModels/Instructors/InstructorLatestActivityVM';
import { InstructorStatsVM } from 'src/app/_DtoModels/Instructors/InstructorsStatsVM';
import { Instructor_CreateOptions } from 'src/app/_DtoModels/Instructors/Instructor_CreateOptions';
import { InstructorCategoryCompactOptions } from 'src/app/_DtoModels/Instructor_Category/InstructorCategoryCompactOptions';
import { Instructor_HistoryCreateOptions } from 'src/app/_DtoModels/Instructor_History/Instructor_HistoryCreateOptions';
import { environment } from 'src/environments/environment';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InstructorService {
  baseUrl = environment.QTD + 'instructors';
  constructor(private http: HttpClient) {}

  getInstructor(){
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res["insList"] as Instructor[];
      })
    ));
  }
  getInsCategoryWithIns()
  {
    return firstValueFrom(this.http.get(this.baseUrl + `/categories/nest`).pipe(
      map((res:any)=>{
        return res["result"] as InstructorCategoryCompactOptions[];
      })
    ));
  }
  create(options: Instructor_CreateOptions)
  {
    
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['ins'] as Instructor;
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
  get(id: any)
  {
    
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res: any) => {

        return res['ins'] as Instructor;
      })
    ));
  }
  makeActiveInactiveOrDelete(options:Instructor_HistoryCreateOptions){
    return firstValueFrom(this.http.delete(this.baseUrl,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  update(id:any,options:Instructor_CreateOptions)
  {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res["result"];
      })
    ));
  }
  getStatsCount()
  {
    
    return firstValueFrom(this.http
      .get(this.baseUrl + '/stats')
      .pipe(
        map((res: any) => {
          return res['stats'] as InstructorStatsVM;
        })
      ));
  }
  getStatusHistory()
  {
    
    return firstValueFrom(this.http
      .get(this.baseUrl + `/history`)
      .pipe(
        map((res: any) => {
          return res['history'] as InstructorLatestActivityVM[];
        })
      ));
  }

  getcatList(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/catlist`)
      .pipe(
        map((res: any) => {
          return res['result'] as any[];
        })
      )
      );
  }

  getinsList(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/inslist`)
      .pipe(
        map((res: any) => {
          return res['result'] as any[];
        })
      )
      );
  }
}
