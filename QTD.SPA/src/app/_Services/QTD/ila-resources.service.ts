import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { ILAResourceCreateOptions } from 'src/app/_DtoModels/ILA/ILAResourcesCreateOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class IlaResourcesService {
  constructor(private http: HttpClient) {}

  baseUrl = environment.QTD + 'ila';

  getAllAsync(ilaId:number) {
    return firstValueFrom(this.http
      .get(this.baseUrl+`/${ilaId}/resource`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }
  createAsync(ilaId:number, options: ILAResourceCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl+`/${ilaId}/resource`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }
  removeILAResourceByILAId(ilaResourceId:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl+`/${ilaResourceId}/removeResourceIla`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  updateAsync(ilaId:number, editILAResourceId:number, options: ILAResourceCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl+`/${ilaId}/${editILAResourceId}/resource/update`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }
}
