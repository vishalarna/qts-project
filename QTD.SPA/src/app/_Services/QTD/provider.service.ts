import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { FilterByOptions } from 'src/app/_DtoModels/ILA/FilterByOptions';
import { ILA_TopicCreateOptions } from 'src/app/_DtoModels/ILA_Topic/ILA_TopicCreateOptions';
import { ILA_ProviderVM } from 'src/app/_DtoModels/Provider/ILA_ProviderVM';
import { Provider } from 'src/app/_DtoModels/Provider/Provider';
import { ProviderCreateOptions } from 'src/app/_DtoModels/Provider/ProviderCreateOptions';
import { ProviderOptions } from 'src/app/_DtoModels/Provider/ProviderOptions';
import { ProviderUpdateOptions } from 'src/app/_DtoModels/Provider/ProviderUpdateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProviderService {
  baseUrl = environment.QTD + 'providers';
  constructor(private http: HttpClient) {}

  create(options: ProviderCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res) => {
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
          return res['providers'] as Provider[];
        })
      )
      );
  }

  getWihtoutIncludes() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/withoutIncludes`)
      .pipe(
        map((res: any) => {
          return res['result'] as Provider[];
        })
      )
      );
  }

  getActiveProviders() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/active`)
      .pipe(
        map((res: any) => {
          return res['providers'] as Provider[];
        })
      )
      );
  }

  getAllWithILACount() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/ilascount')
      .pipe(
        map((res: any) => {
          return res['providers'] as ILA_ProviderVM[];
        })
      )
      );
  }

  getAllWithFilterAndILACount(filterOptions:FilterByOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/ilascount/withFilter`,filterOptions).pipe(
      map((res:any)=>{
        return res['result'] as ILA_ProviderVM[];
      })
    ));
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/' + id)
      .pipe(
        map((res: any) => {
          return res['provider'] as Provider;
        })
      )
      );
  }

  getOnlyProviderData(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/onlyprov`).pipe(
      map((res:any)=>{
        return res['result'] as Provider;
      })
    ));
  }

  delete(id: any,options: ProviderOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`,{body:options})
      .pipe(
        map((res) => {
          return res;
        })
      )
      );
  }

  update(id: any, options: ProviderUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + '/' + id, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  changeStatus(id:any,options:ProviderOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ))
  }

  getAllWithILAs() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/ilas')
      .pipe(
        map((res: any) => {
          return res['providers'] as Provider[];
        })
      )
      );
  }
}
