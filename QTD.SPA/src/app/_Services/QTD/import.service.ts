import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ImportData_DIFSurveyEmployeeResponse_VM } from '@models/DIFSurveyImport/ImportData_DIFSurveyEmployeeResponse_VM';
import { ValidateCSV_DIFSurveyEmployeeResponse_Results_VM } from '@models/DIFSurveyImport/ValidateCSV_DIFSurveyEmployeeResponse_Results_VM';
import { ImportData_ClassResponse_VM } from '@models/ExternalClassImport/ImportData_ClassResponse_VM';
import { ImportData_EmployeeResponse_VM } from '@models/ExternalClassImport/ImportData_EmployeeResponse_VM';
import { ImportData_ILAResponse_VM } from '@models/ExternalClassImport/ImportData_ILAResponse_VM';
import { ValidateCSV_Class_Results_VM } from '@models/ExternalClassImport/ValidateCSV_Class_Results_VM';
import { ValidateCSV_Employee_Results_VM } from '@models/ExternalClassImport/ValidateCSV_Employee_Results_VM';
import { ValidateCSV_ILA_Results_VM } from '@models/ExternalClassImport/ValidateCSV_ILA_Results_VM';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ImportService {
    baseUrl = environment.QTD;
    constructor(private http: HttpClient) {}

    getTemplateAsync(type:string){
      return firstValueFrom(this.http.get(this.baseUrl + 'import/template/' + type,  { observe: 'response', responseType: 'blob' }).pipe(
        map((res:any)=>{
          return res;
        })
      ));
    }

    validateCSVAsync(formData: any){
      return firstValueFrom(this.http.post(this.baseUrl + 'import/difSurveryEmployeeResponse/validate', formData).pipe(
        map((res:any)=>{
          return res.result as ValidateCSV_DIFSurveyEmployeeResponse_Results_VM;
        })
      ));
    }

    validateCSVILAAsync(formData: any){
      return firstValueFrom(this.http.post(this.baseUrl + 'import/ila/validate', formData).pipe(
        map((res:any)=>{
          return res.result as ValidateCSV_ILA_Results_VM;
        })
      ));
    }

    validateCSVEmployeeAsync(formData: any){
      return firstValueFrom(this.http.post(this.baseUrl + 'import/employee/validate', formData).pipe(
        map((res:any)=>{
          return res.result as ValidateCSV_Employee_Results_VM;
        })
      ));
    }

    validateCSVClassAsync(formData: any){
      return firstValueFrom(this.http.post(this.baseUrl + 'import/class/validate', formData).pipe(
        map((res:any)=>{
          return res.result as ValidateCSV_Class_Results_VM;
        })
      ));
    }

    importDIFSurveyEmployeeResponseAsync(options: ImportData_DIFSurveyEmployeeResponse_VM){
      return firstValueFrom(this.http.post(this.baseUrl + 'import/difSurveryEmployeeResponse', options,  { observe: 'response' }).pipe(
        map((res:any)=>{
          return res;
        })
      ));
    }

    importILAResponseAsync(options: ImportData_ILAResponse_VM){
      return firstValueFrom(this.http.post(this.baseUrl + 'import/ila', options,  { observe: 'response' }).pipe(
        map((res:any)=>{
          return res;
        })
      ));
    }

    importEmployeeResponseAsync(options: ImportData_EmployeeResponse_VM){
      return firstValueFrom(this.http.post(this.baseUrl + 'import/employee', options,  { observe: 'response' }).pipe(
        map((res:any)=>{
          return res;
        })
      ));
    }

    importClassResponseAsync(options: ImportData_ClassResponse_VM){
      return firstValueFrom(this.http.post(this.baseUrl + 'import/class', options,  { observe: 'response' }).pipe(
        map((res:any)=>{
          return res;
        })
      ));
    }


}
