import { ILASafetyHazardLinkOptions } from 'src/app/_DtoModels/ILA_SafetyHazard_Link/ILA_SafetyHazard_LinkOptions';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IlaSafteyHazardLinkService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'ila/';

  /*to send data to ILA_Safety_Hazards_Link Table*/
  create(id: any, options: ILASafetyHazardLinkOptions) {
    
    return firstValueFrom(this.http.post(this.baseUrl + id + "/safetyHazard", options).pipe(
      map((res: any) => {
        return res;
      })
    ))
  }

   /*to delete data from ILA_Safety_Hazards_Link Table*/
   delete(id:any,linkIds:ILASafetyHazardLinkOptions){
    
    return firstValueFrom(this.http.delete(this.baseUrl + id + "/safetyHazard", { body:linkIds }).pipe(
      map((res:any)=>{
        return res
      })
    ))
  }
}
