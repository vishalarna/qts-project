import { MetaILACreateOptions } from './../../_DtoModels/MetaILA/MetaILACreateOptions';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MetaILAMembersLinkOptions, MetaILAMembersListOptions } from 'src/app/_DtoModels/MetaILAMembersLink/MetaILAMembersLinkOptions';
import { MetaILAILARequirements_VM } from 'src/app/_DtoModels/MetaILA/MetaILAILARequirements_VM';
import { MetaILAUpdateOptions } from 'src/app/_DtoModels/MetaILA/MetaILAUpdateOptions';
import { MetaILA } from 'src/app/_DtoModels/MetaILA/MetaILA';
import { MetaILAOptions } from 'src/app/_DtoModels/MetaILA/MetaILAOptions';
import { MetaILAEmployeesLinkOptions } from 'src/app/_DtoModels/MetaILAEmployeesLink/MetaILAEmployeesLinkOptions';
import { MetaILA_Employees } from 'src/app/_DtoModels/MetaILA/MetaILA_Employees';
import { EnablingObjective } from '@models/EnablingObjective/EnablingObjective';
import { MetaILAVM } from '@models/MetaILA/MetaILAVM';
import { MetaILAEmployeeVM } from '@models/MetaILAEmployeesLink/MetaILAEmployeeVM';
import { firstValueFrom } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class MetaILAService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'metailas';

  create(option: MetaILACreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {

          return res;
        })
      )
      );
  }

  updateAsync(id: string, option: MetaILAUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, option)
      .pipe(
        map((res: any) => {

          return res.result;
        })
      )
      );
  }

  getMetaILAAsync(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as MetaILAVM;
        })
      )
      );
  }

  createMetaILAMemeberLink(id: any, option: MetaILAMembersListOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/link`, option)
      .pipe(
        map((res: any) => {

          return res.result;
        })
      )
      );
  }

  updateMetaILAMemberLink(option: MetaILAMembersLinkOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/ilamember`, option)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['result'] as MetaILAVM;
        })
      )
      );
  }


  getAllMetaILAMemeberLink() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/ilamemeber`)
      .pipe(
        map((res: any) => {

          return res['result'] as MetaILAMembersLinkOptions;
        })
      )
      );
  }

  getMetaILAMemeberLink(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/ilamemeber`)
      .pipe(
        map((res: any) => {

          return res['result'] as MetaILAMembersLinkOptions;
        })
      )
      );
  }

  removeMetaILAMemeberLink(id: any, linkedId : any ) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/ilamemeber/${linkedId}/remove`,{ observe: 'response' })
      .pipe(
        map((res: any) => {

          return res as any;
        })
      )
      );
  }

  getMetaILAILARequirements(iLAId: string) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${iLAId}/requirements`)
      .pipe(
        map((res: any) => {
          return res['result'] as MetaILAILARequirements_VM;
        })
      )
      );
  }

  deleteMetaILAAsync(id: any,options: MetaILAOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`,{ body: options })
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  getMetaILAEmployees(metaILAId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${metaILAId}/employees`)
      .pipe(
        map((res: any) => {
          return res['result'] as MetaILAEmployeeVM[];
        })
      )
      );
  }

  linkMetaILAEmployee(option: MetaILAEmployeesLinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/employees/link`, option)
      .pipe(
        map((res: any) => {

          return res
        })
      )
      );
  }

  unlinkMetaILAEmployees(option: MetaILAEmployeesLinkOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/employees/unlink`, option)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getEnablingObjectivesForMetaILA(metaILAId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${metaILAId}/enabling-objectives`)
      .pipe(
        map((res: any) => {
            return res['result'] as EnablingObjective[];
        })
      )
      );
  }

  getMetaILANERCCertificationDetailsAsync(metaILAId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${metaILAId}/nerccertdetail`)
      .pipe(
        map((res: any) => {
            return res['result'] as any[];
        })
      )
      );
  }

  getAllMetaILAsLinkedToEmployeeForIDPAsync(empId:string){
    return firstValueFrom(this.http
      .get(this.baseUrl + `/employee/idp/${empId}`)
      .pipe(
        map((res: any) => {
            return res['result'] as any[];
        })
      )
      );
  }

  getLinkedMetaILAsMembersByMetaILAIdForIDPAsync(id:string,empId:string){
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/employee/${empId}/idp/ilamember`)
      .pipe(
        map((res: any) => {
            return res['result'] as any[];
        })
      )
      );
  }

}
