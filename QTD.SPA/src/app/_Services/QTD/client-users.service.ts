import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { ClientUser } from 'src/app/_DtoModels/ClientUser/ClientUser';
import { ClientUserCreateOptions } from 'src/app/_DtoModels/ClientUser/ClientUserCreateOptions';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ClientUsersService {
  baseUrl = environment.QTD + 'clientsUsers';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          
          return res['clientUsers'] as ClientUser[];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          
          return res['clientUser'] as ClientUser;
        })
      )
      );
  }

  create(opt: ClientUserCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, opt)
      .pipe(
        map((res: any) => {
          
          return res['clientUser'] as ClientUser;
        })
      )
      );
  }

  delete(id: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          
          return res.message;
        })
      )
      );
  }
}
