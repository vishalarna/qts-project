import { ILA_Procedure_LinkOptions } from 'src/app/_DtoModels/ILA_Procedure_Link/ILA_Procedure_LinkOptions';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IlaProcedureLinkService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'ila/';

  /*to send data to ILA_Procedures_Link Table*/
  create(id: any, options: ILA_Procedure_LinkOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + id + "/procedure", options).pipe(
      map((res: any) => {
        return res
      })
    ))
  }

  /*to delete data from ILA_Procedures_Link Table*/
  delete(id:any,linkIds:ILA_Procedure_LinkOptions){
    
    return firstValueFrom(this.http.delete(this.baseUrl + id + "/procedure", { body:linkIds }).pipe(
      map((res:any)=>{
        return res
      })
    ))
  }
}
