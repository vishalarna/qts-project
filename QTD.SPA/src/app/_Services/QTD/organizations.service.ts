import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Organization } from 'src/app/_DtoModels/Organization/Organization';
import { OrganizationCreateOptions } from 'src/app/_DtoModels/Organization/OrganizationCreateOptions';
import { OrganizationUpdateOptions } from 'src/app/_DtoModels/Organization/OrganizationUpdateOptions';
import { OrganizationIdAndNameVM } from 'src/app/_DtoModels/Organization/OrganizationIdAndNameVM';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class OrganizationsService {
  baseUrl = environment.QTD + 'organizations';
  constructor(private http: HttpClient) {}
  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['organizations'] as Organization[];
        })
      )
      );
  }

  getAllSimplified(){
    return firstValueFrom(this.http.get(this.baseUrl + `/simplified`).pipe(
      map((res:any)=>{
        return res['result'] as OrganizationIdAndNameVM[];
      })
    ));
  }

  getAllOrderBy(orderBy:'name'){
    return firstValueFrom(this.http.get(this.baseUrl + `/order/${orderBy}`).pipe(
      map((res:any)=>{
        return res['result'] as Organization[];
      })
    ));
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res['organization'] as Organization;
        })
      )
      );
  }

  create(options: OrganizationCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {

          return res['organization'] as Organization;
        })
      )
      );
  }
  update(id: any, options: OrganizationUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {

          return res['organization'] as Organization;
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

  getPublicOrganizationsList() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/publicOrganizations')
      .pipe(
        map((res: any) => {

          return res['result'] as any[];
        })
      )
      );
  }
}
