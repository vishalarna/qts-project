import { ILA_TopicUpdateOptions } from './../../_DtoModels/ILA_Topic/ILA_TopicUpdateOptions';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { FilterByOptions } from 'src/app/_DtoModels/ILA/FilterByOptions';
import { ILA_Topic } from 'src/app/_DtoModels/ILA_Topic/ILA_Topic';
import { ILA_TopicCreateOptions } from 'src/app/_DtoModels/ILA_Topic/ILA_TopicCreateOptions';
import { ILA_TopicOptions } from 'src/app/_DtoModels/ILA_Topic/ILA_TopicOptions';
import { ILA_TopicVM } from 'src/app/_DtoModels/ILA_Topic/ILA_TopicVM';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TopicService {
  baseUrl = environment.QTD + 'ilatopics';
  constructor(private http: HttpClient) {}

  create(options: ILA_TopicCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['ilaTopics'] as ILA_Topic[];
        })
      )
      );
  }

  changeStatus(id:any,options:ILA_TopicOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ))
  }

  getAllWithFilterAndILACount(options:FilterByOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/withCount/filter`,options).pipe(
      map((res:any)=>{
        return res['result'] as ILA_TopicVM[];
      })
    ));
  }

  update(id: any, options: ILA_TopicUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + '/' + id, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  delete(id: any,options: ILA_TopicOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`,{body:options})
      .pipe(
        map((res) => {
          return res;
        })
      )
      );
  }

}
