import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { TrainingGroup } from 'src/app/_DtoModels/TrainingGroup/TrainingGroup';
import { TrainingGroup_Category } from 'src/app/_DtoModels/TrainingGroup_Category/TrainingGroup_Category';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TrainingGroupService {
  baseUrl = environment.QTD + 'trainingGroups';
  constructor(
    private http : HttpClient
  ) { }

  getAll(){
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as TrainingGroup[];
      })
    ));
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as TrainingGroup;
      })
    ));
  }

  getWithCatId(catId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/cat/${catId}`).pipe(
      map((res:any)=>{
        return res['result'] as TrainingGroup;
      })
    ));
  }

  getGroupInCategories(){
    return firstValueFrom(this.http.get(this.baseUrl + `/cat`).pipe(
      map((res:any)=>{
        return res['result'] as TrainingGroup_Category[];
      })
    ));
  }
}
