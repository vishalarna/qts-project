import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { DeliveryMethod } from 'src/app/_DtoModels/DeliveryMethod/DeliveryMethod';
import { DeliveryMethodeCreateOption } from 'src/app/_DtoModels/DeliveryMethod/DeliveryMethodeCreateOption';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DeliveryMethodeService {
  constructor(private http: HttpClient) {}

  baseUrl = environment.QTD + 'deliveryMethods';

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['result'] as DeliveryMethod[];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as DeliveryMethod;
        })
      )
      );
  }

  create(option: DeliveryMethodeCreateOption) {
    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getIsNerc(id: boolean) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/isNerc`)
      .pipe(
        map((res: any) => {
          return res['result'] as DeliveryMethod[];
        })
      )
      );
  }

}
