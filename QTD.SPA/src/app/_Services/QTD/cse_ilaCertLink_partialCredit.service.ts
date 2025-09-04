import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CSE_ILACertPartialCreditCreateUpdateOption } from "@models/CSE_ILACertLink_PartialCredit/CSE_ILACertPartialCreditCreateUpdateOption";
import { EmployeeIdsModel } from "@models/Employee/EmployeeIdsModel";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";


@Injectable({
    providedIn: 'root',
  })

export class Cse_IlaCertPartialCreditService{

    baseUrl = environment.QTD + 'ilacertpartialcredit/cse';
    baseUrl1 = environment.QTD + 'cseilacertpartialcredit';
    
    constructor(private http: HttpClient) {}

    getByClassScheduleEmployeeIdAsync(option:EmployeeIdsModel) {
          return firstValueFrom(this.http.post(this.baseUrl,option).pipe(
            map((res: any)=> {
              return res['result'] as any[];
            })
          ));
        }

    addOrUpdateClassEmpILACertLinkPartialCreditHoursAsync(id:any,option:CSE_ILACertPartialCreditCreateUpdateOption) {
            return firstValueFrom(this.http
              .post(`${this.baseUrl1}/ila/${id}`,option)
              .pipe(
                map((res: any) => {
                  return res;
                })
              ));
    }
}