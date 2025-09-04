import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IdentityProviderVM } from "@models/IdentityProvider/IdentityProviderVM";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root',
})
export class IdentityProviderService {
  baseUrl: string = environment.APIAuth + 'identityprovider';
  constructor(private http: HttpClient) {}

  getIdentityProviderByName(name: string) {
      return firstValueFrom(this.http.get(this.baseUrl + `/${name}`).pipe(
        map((res: any) => {
          
          return res['identityProvider'] as IdentityProviderVM;
        })
      ));
    }
}