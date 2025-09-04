import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IdentityProviderVM } from '@models/IdentityProvider/IdentityProviderVM';
import { PublicUrlInstanceSettingOptions } from '@models/Instance/PublicUrlInstanceSettingOptions';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { Instance } from 'src/app/_DtoModels/Instance/Instance';
import { InstanceCreateOptions } from 'src/app/_DtoModels/Instance/InstanceCreateOptions';
import { InstanceUpdateOptions } from 'src/app/_DtoModels/Instance/InstanceUpdateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class InstanceService {
  baseUrl: string = environment.APIAuth + 'instances';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {
        
        return res['instances'] as Instance[];
      })
    ));
  }

  get(name: string) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${name}`).pipe(
      map((res: any) => {
        
        return res['instance'] as Instance;
      })
    ));
  }

  create(options: InstanceCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {
        
        return res['instance'] as Instance;
      })
    ));
  }

  update(name: string, options: InstanceUpdateOptions) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${name}`, options).pipe(
      map((res: any) => {
        
        return res['instance'] as Instance;
      })
    ));
  }

  delete(name: string) {
    return firstValueFrom(this.http.delete(this.baseUrl + `/${name}`).pipe(
      map((res: any) => {
        
        return res.message;
      })
    ));
  }

  getIdentityProvidersByInstanceName(name: string) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${name}/identityprovider`).pipe(
      map((res: any) => {
        
        return res['identityProviders'] as IdentityProviderVM[];
      })
    ));
  }

  getPublicURLInstanceSettingsAsync(name: string): Promise<string | null> {
  return firstValueFrom(this.http.get<{ publicUrl: string }>(`${this.baseUrl}/${name}/settings/publicUrl`).pipe(
    map(res => res?.publicUrl ?? null)
  ));
}


  updateInstanceSettingsPublicUrlAsync(name: string, options:PublicUrlInstanceSettingOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${name}/settings/publicUrl`, options).pipe(
      map((res:any) => {
        return res['settings'] as any;
      })
    ));
  }
}
