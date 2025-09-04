import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { firstValueFrom, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.APIAuth;
  constructor(private http: HttpClient) { }
  createUser(option: any) {
    return firstValueFrom(this.http
      .post(this.baseUrl + 'users', option, { observe: 'response' })
      .pipe(
        map((res: any) => {
          return res as any;
        })
      )
      );
  }

  updateUser(option: any) {
    return firstValueFrom(this.http
      .put(this.baseUrl + 'users/update', option, { observe: 'response' })
      .pipe(
        map((res: any) => {

          return res as any;
        })
      )
      );
  }

  removeUserByInstanceAsync(userName: string, instanceName: string) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `users/${userName}/instance/${instanceName}`, {observe : "response"})
      .pipe(
        map((res: any) => {
          return res as any;
        })
      )
      );
  }

  removeUserAsync(userName: string) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `users/${userName}`, {observe : "response"})
      .pipe(
        map((res: any) => {
          return res as any;
        })
      )
      );
  }

  getUserIsAdminClaim(name : string){
    return firstValueFrom(this.http
      .get(this.baseUrl + 'users/' + name + '/isAdmin')
      .pipe(
        map((res: any) => {
          return res['isAdmin'] as boolean;
        })
      )
      )
  }
}
