import { MetaILAConfigurationPublishOptionCreateOptions } from './../../_DtoModels/MetaILAConfigurationPublishOption/MetaILAConfigurationPublishOptionCreateOptions';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class MetaILAConfigPublishOptionService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'metaILAConfigPublishOption';

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['result'] as MetaILAConfigurationPublishOptionCreateOptions[];
        })
      ));
  }

  
}
