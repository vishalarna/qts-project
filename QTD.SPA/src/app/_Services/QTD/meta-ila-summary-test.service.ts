import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MetaILA_SummaryTest_ViewModel } from 'src/app/_DtoModels/MetaILA_SummaryTest/MetaILA_SummaryTest_ViewModel';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { GetTestItemsByILAsOption } from 'src/app/_DtoModels/MetaILA_SummaryTest/GetTestItemsByILAsOption';
import { firstValueFrom } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class MetaILASummaryTestService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'metaIlaSummaryTest';

  createAsync(option: MetaILA_SummaryTest_ViewModel) {
    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {

          return res.result as MetaILA_SummaryTest_ViewModel;
        })
      )
      );
  }

  getAsync(id:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res['result'] as MetaILA_SummaryTest_ViewModel;
        })
      )
      );
  }

  updateAsync(id:string,option: MetaILA_SummaryTest_ViewModel) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, option)
      .pipe(
        map((res: any) => {
          return res.result as MetaILA_SummaryTest_ViewModel;
        })
      )
      );
  }

  getTestItemsFromILAs(option: GetTestItemsByILAsOption) {
    return firstValueFrom(this.http
      .post(this.baseUrl+"/ilas/testItems", option)
      .pipe(
        map((res: any) => {

          return res.result as TestItem[];
        })
      )
      );
  }
 
}
