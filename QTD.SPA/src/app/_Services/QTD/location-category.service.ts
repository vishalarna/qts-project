import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Location_Category } from 'src/app/_DtoModels/Location_Category/Location_Category';
import { Location_CategoryOptions } from 'src/app/_DtoModels/Location_Category/Location_CategoryOptions';
import { Location_HistoryCreateOptions } from 'src/app/_DtoModels/Location_History/Location_HistoryCreateOptions';
import { Location_CategoryHistoryCreateOptions } from 'src/app/_DtoModels/Location_Category/Location_CategoryHistoryCreateOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LocationCategoryService {
  baseUrl = environment.QTD + 'locations/categories';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['loc_CatList'] as Location_Category[];
        })
      )
      );
  }

  create(options: any) {
    
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res['result'] as Location_Category;
        })
      )
      );
  }

  update(id: any, options: any) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['loc_cat'] as Location_Category;
        })
      )
      );
  }

  saveSHCatHistory(options: Location_Category) {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/history', options)
      .pipe(
        map((res: any) => {
          return res['result'] as Location_Category;
        })
      )
      );
  }

  delete(options: Location_CategoryOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getCount() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/count`)
      .pipe(
        map((res: any) => {
          return res['result'] as number;
        })
      )
      );
  }
  makeActiveInactiveOrDelete(
    id: any,
    options: Location_CategoryHistoryCreateOptions
  ) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`, { body: options })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }
}
