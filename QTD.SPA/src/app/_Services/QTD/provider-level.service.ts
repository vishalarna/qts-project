import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { ProviderCreateOptions } from 'src/app/_DtoModels/Provider/ProviderCreateOptions';
import { ProviderLevelUpdateOptions } from 'src/app/_DtoModels/ProviderLevel/ProviderLevelUpdateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProviderLevelService {
  baseUrl = environment.QTD + 'providerlevels';
  constructor(private http: HttpClient) {}

  create(options: ProviderCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res['providers'];
        })
      )
      );
  }

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['providers'];
        })
      )
      );
  }

  getAllWithILA() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/ilas')
      .pipe(
        map((res: any) => {
          return res['providers'];
        })
      )
      );
  }

  get(id: any) {
    return this.http.get(this.baseUrl + '/' + id).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + '/' + id).pipe(
      map((res) => {
        return res;
      })
    );
  }

  update(id: any, options: ProviderLevelUpdateOptions) {
    this.http.put(this.baseUrl + '/' + id, options).pipe(
      map((res: any) => {
        return res;
      })
    );
  }
}
