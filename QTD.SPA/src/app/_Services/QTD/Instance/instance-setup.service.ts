import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PersonWithUserDataVm } from "@models/Person/PersonWithUserDataVm";
import { environment } from "src/environments/environment";
import { map } from 'rxjs/operators';
import { PersonCreateOption } from "@models/Person/PersonCreateOption";
import { Person } from "@models/Person/Person";
import { ClientUserCreateOptions } from "@models/ClientUser/ClientUserCreateOptions";
import { ClientUser } from "@models/ClientUser/ClientUser";
import { QtdUserVM } from "@models/QtdUser/QtdUserVM";
import { EmployeeCreateOptions } from "@models/Employee/EmployeeCreateOptions";
import { Employee } from "@models/Employee/Employee";
import { Instructor_CreateOptions } from "@models/Instructors/Instructor_CreateOptions";
import { Instructor } from "@models/Instructors/Instructor";
import { ClientSettings_AnalyzeLicenseOptions } from "@models/ClientSettingsLicense/ClientSettings_AnalyzeLicenseOptions";
import { ClientSettings_LicenseUpdateOptions } from "@models/ClientSettingsLicense/ClientSettings_LicenseUpdateOptions";
import { Instructor_Category } from "@models/Instructor_Category/Instructor_Category";
import { PersonUpdateOption } from "@models/Person/PersonUpdateOption";
import { EmployeeUpdateOptions } from "@models/Employee/EmployeeUpdateOptions";
import { Instructor_UpdateByEmailOptions } from "@models/Instructors/Instructor_UpdateByEmailOptions";
import { firstValueFrom } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class InstanceSetupService {
  baseUrl = environment.QTD + `instance`;
  baseUrlAuth = environment.APIAuth;
  constructor(private http: HttpClient) {}

  getLicenseHistoryAsync = (instanceName: string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${instanceName}/clientSettings/license/history`)
      .pipe(
        map((res: any) => {
          return res.licenseHistory;
        })
      )
      );

  }

  analyzeLicenseAsync(options: ClientSettings_AnalyzeLicenseOptions, instanceName: string ) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${instanceName}/clientSettings/license/analyze`, options)
      .pipe(
        map((res: any) => {
          return res.license;
        })
      )
      );
  }

  updateLicenseAsync (options: ClientSettings_LicenseUpdateOptions, instanceName: string) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${instanceName}/clientSettings/license`, options)
      .pipe(
        map((res: any) => {
          return res.license;
        })
      ));
  }

  getPersonsWithUserDataAsync = (instanceName: string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${instanceName}/persons/userData`)
      .pipe(
        map((res: any) => {
          return res['persons'] as PersonWithUserDataVm[];
        })
      )
      );
  }

  getUserDetailAsync = (id: string, instanceName: string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${instanceName}/persons/userData/${id}`)
      .pipe(
        map((res: any) => {
          return res['persons'] as PersonWithUserDataVm;
        })
      )
      );
  }

  getCategoriesByDatabaseAsync(instanceName : string) {
    return firstValueFrom(this.http
    .get(this.baseUrl + `/${instanceName}/instructors/categories`)
    .pipe(
      map((res: any) => {
        return res['ins_CatList'] as Instructor_Category[];
      })
    ));
  }

  getUserDetailByUserNameAsync(instanceName : string, userName : string) {
    return firstValueFrom(this.http
    .get(this.baseUrl + `/${instanceName}/persons/userdata/byUsername/${userName}`)
    .pipe(
      map((res: any) => {
        return res['persons'] as PersonWithUserDataVm;
      })
    ));
  }

  createPersonAsync(options: PersonCreateOption, instanceName: string ) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${instanceName}/persons`, options, { observe: 'response' })
      .pipe(
        map((res: any) => {
          return res.body['person'] as Person;
        })
      )
      );
  }

  updatePersonAsync(options: PersonUpdateOption, instanceName: string, id: string ) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/persons/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['person'] as Person;
        })
      )
      );
  }
  
  activatePersonAsync(instanceName: string, id: string ) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/persons/${id}/activate`,{})
      .pipe(
        map((res: any) => {
          return res as any
        })
      )
      );
  }
  
  deactivatePersonAsync(instanceName: string, id: string ) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/persons/${id}/deactivate`,{})
      .pipe(
        map((res: any) => {
          return res as any
        })
      )
      );
  }


  createClientUserAsync(options: ClientUserCreateOptions, instanceName: string ) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${instanceName}/clientUsers`, options, { observe: 'response' })
      .pipe(
        map((res: any) => {
          return res.body['clientUser'] as ClientUser;
        })
      )
      );
  }
  
  activateClientUserAsync(instanceName: string, personId: string ) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/clientUsers/${personId}/activate`,{})
      .pipe(
        map((res: any) => {
          return res as any
        })
      )
      );
  }

  deactivateClientUserAsync(instanceName: string, personId: string ) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/clientUsers/${personId}/deactivate`,{})
      .pipe(
        map((res: any) => {
          return res as any
        })
      )
      );
  }

  createQTDUserAsync(options: QtdUserVM, instanceName: string ){
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${instanceName}/qtdUsers`, options, { observe: 'response' })
      .pipe(
        map((res: any) => {
          return  res.body['qtdUser'] as QtdUserVM;
        })
      )
      );
  }

  updateQTDUserAsync(options: QtdUserVM, instanceName: string, id:string ){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/qtdUsers/${id}`, options)
      .pipe(
        map((res: any) => {
          return  res['qtdUser'] as QtdUserVM;
        })
      )
      );
  }

  activateQTDUserAsync(instanceName: string, id:string ){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/qtdUsers/${id}/activate`,null)
      .pipe(
        map((res: any) => {
          return  res['qtdUser'] as QtdUserVM;
        })
      )
      );
  }

  deactivateQTDUserAsync(instanceName: string, id:string ){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/qtdUsers/${id}/deactivate`,{})
      .pipe(
        map((res: any) => {
          return  res['qtdUser'] as QtdUserVM;
        })
      )
      );
  }

  getEmployeeByNumberAsync(employeeNumber: string, instanceName: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${instanceName}/employee/byEmployeeNumber/${employeeNumber}`)
      .pipe(
        map((res: any) => {
          return res['employee'] as Employee;
        })
      )
      );
  }

  createEmployeeAsync(option: EmployeeCreateOptions, instanceName: string) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${instanceName}/employees`, option, { observe: 'response' })
      .pipe(
        map((res: any) => {

          return res.body['employees'] as Employee;
        })
      )
      );
  }

  updateEmployeeAsync(options: EmployeeUpdateOptions, instanceName: string, id:string ){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/employees/${id}`, options)
      .pipe(
        map((res: any) => {
          return  res['employee'] as Employee;
        })
      )
      );
  }

  activateEmployeeAsync(instanceName: string, id:string ){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/employees/${id}/activate`,null)
      .pipe(
        map((res: any) => {
          return  res.message;
        })
      )
      );
  }

  deactivateEmployeeAsync(instanceName: string, id:string ){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/employees/${id}/deactivate`,null)
      .pipe(
        map((res: any) => {
          return  res as any;
        })
      )
      );
  }

  createInstructorAsync(option: Instructor_CreateOptions, instanceName: string) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${instanceName}/instructors`, option, { observe: 'response' })
      .pipe(
        map((res: any) => {

          return res.body['instructor'] as Instructor;
        })
      )
      );
  }

  updateInstructorAsync(options: Instructor_CreateOptions, instanceName: string, id:string ){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/instructors/${id}`, options)
      .pipe(
        map((res: any) => {
          return  res['instructor'] as Instructor;
        })
      )
      );
  }

  updateInstructorByEmailAsync(options: Instructor_UpdateByEmailOptions, instanceName: string){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/instructors/byEmail`, options)
      .pipe(
        map((res: any) => {
          return  res['instructor'] as Instructor;
        })
      )
      );
  }

  activateInstructorAsync(instanceName: string, id:string ){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/instructors/${id}/activate`,null)
      .pipe(
        map((res: any) => {
          return  res.message;
        })
      )
      );
  }

  deactivateInstructorAsync(instanceName: string, id:string ){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${instanceName}/instructors/${id}/deactivate`,null)
      .pipe(
        map((res: any) => {
          return  res as any;
        })
      )
      );
  }
  

}
