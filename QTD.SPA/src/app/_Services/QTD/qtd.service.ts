import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { QtdUserVM } from '@models/QtdUser/QtdUserVM';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class QTDService {
  baseUrl = environment.QTD + 'qtdUsers';
  constructor(private http: HttpClient) { }

  getAllActiveAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res.result as QtdUserVM[];
        })
      ));
  }

  getAllActiveWithEmployeeDataAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/withEmployeeData')
      .pipe(
        map((res: any) => {
          return res.result as QtdUserVM[];
        })
      ));
  }

  createAsync(qtdUserOption:QtdUserVM){
    return firstValueFrom(this.http
      .post(this.baseUrl, qtdUserOption)
      .pipe(
        map((res: any) => {
          return res.qtdUser as QtdUserVM;
        })
      ));
  }

}
