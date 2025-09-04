import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthMessageCreateOptions } from "@models/AdminMessageAuth/AuthMessageCreateOptions";
import { AuthMessageVM } from "@models/AdminMessageAuth/AuthMessageVM";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root',
})
export class AuthAdminMesaageService{
baseUrl: string = environment.APIAuth + 'adminMessage';
  constructor(private http: HttpClient) {}

  createAdminMessage(option: AuthMessageCreateOptions ) {
      return firstValueFrom(this.http
      .post(this.baseUrl , option)
      .pipe(
        map((res: any) => {         
          return;
        })
      )
      );
    }

    getAllAdminMessageAuthAsync(){
      return firstValueFrom(this.http.get(this.baseUrl).pipe(
        map((res: any) => {
          return res['result'] as AuthMessageVM[]
        })
      )
      );
    }
}