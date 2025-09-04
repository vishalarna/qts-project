import { MetaILAStatusCreateOptions } from './../../_DtoModels/MetaILAStatus/MetaILAStatusCreateOptions';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MetaILAStatus } from 'src/app/_DtoModels/MetaILAStatus/MetaILAStatus';
import { firstValueFrom } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class MetaILAStatusService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'metaILAStatus';

  create(option: MetaILAStatusCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {

          return res;
        })
      )
      );
  }

  getAll(){
    return firstValueFrom(this.http
    .get(this.baseUrl)
    .pipe(
      map((res: any) => {

        return res['result'] as MetaILAStatus[];
      })
    )
    );
  }
}
