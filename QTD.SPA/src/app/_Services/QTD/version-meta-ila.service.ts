import { Version_MetaILACreateOptions } from './../../_DtoModels/Version_MetaILA/Version_MetaILACreateOptions';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class VersionMetaILAService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'version_metailas';

  create(option: Version_MetaILACreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {

          return res;
        })
      ));
  }
}
