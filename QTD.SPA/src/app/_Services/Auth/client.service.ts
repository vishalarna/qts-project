import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { Client } from 'src/app/_DtoModels/Client/ClientViewModel';
import { CreateClientOption } from 'src/app/_DtoModels/Client/CreateClientOption';
import { Instance } from 'src/app/_DtoModels/Instance/Instance';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class ClientService {
  baseUrl = environment.APIAuth;

  constructor(private http: HttpClient) {}

  getClients() {
    return firstValueFrom(this.http
      .get(this.baseUrl + 'clients')
      .pipe(
        map((res: any) => {
          
          return res['clients'] as Client[];
        })
      )
      );
  }

  createClient(option: CreateClientOption) {
    return firstValueFrom(this.http
      .post(this.baseUrl + 'clients', option)
      .pipe(
        map((res: any) => {
          
          return res.createdClient;
        })
      )
      );
  }

  updateClient(name: string, updatedName: string) {
    

    return firstValueFrom(this.http
      .put(this.baseUrl + 'clients/' + name + '/' + updatedName, {})
      .pipe(
        map((res: any) => {
          
          return res.message;
        })
      )
      );
  }
  getClient(name: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + 'clients/' + name)
      .pipe(
        map((res: any) => {
          
          return res['client'] as Client;
        })
      )
      );
  }

  deactivate(name: string) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + 'clients/' + name)
      .pipe(
        map((res: any) => {
          
          return res.message;
        })
      )
      );
  }

  getClientInstances(name: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `clients/${name}/instances`)
      .pipe(
        map((res: any) => {
          return res['instances'] as Instance[];
        })
      )
      );
  }

  getUserClientInstances(name: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `users/${name}/instances`)
      .pipe(
        map((res: any) => {
          return res['instances'] as Instance[];
        })
      )
      );
  }
  getAllClients() {
    return firstValueFrom(this.http
      .get(this.baseUrl + 'clients/all')
      .pipe(
        map((res: any) => {
          
          return res['clients'] as Client[];
        })
      )
      );
  }
  
}
