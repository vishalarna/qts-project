import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { NercStandard } from 'src/app/_DtoModels/NercStandard/NercStandard';
import { NercStandardCreateOptions } from 'src/app/_DtoModels/NercStandard/NercStandardCreateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NercStandardService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'nercStandards';

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['result'] as NercStandard[];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res['result'] as NercStandard;
        })
      )
      );
  }

  create(option: NercStandardCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {

          return res;
        })
      )
      );
  }

  update(id: any, options: NercStandardCreateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + '/' + id,options)
      .pipe(
        map((res:any)=>{
          return res;
        })
      ));
  }
}
