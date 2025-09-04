import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ILARegRequirementsLinkOptions } from 'src/app/_DtoModels/ILA_RegRequirements_Link/ILA_RegRequirements_LinkOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IlaRegRequirementsLinkService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'ila/';

  /*to send data to ILA_RegRequirement_Link Table*/
  create(id: any, options: ILARegRequirementsLinkOptions) {
    
    return firstValueFrom(this.http.post(this.baseUrl + id + "/regRequirement", options).pipe(
      map((res: any) => {
        return res
      })
      
    ))
  }

  /*to delete data from ILA_RegRequirement_Link Table*/
  delete(id:any,linkIds:ILARegRequirementsLinkOptions){
    
    return firstValueFrom(this.http.delete(this.baseUrl + id + "/regRequirement", { body:linkIds }).pipe(
      map((res:any)=>{
        return res
      })
    ))
  }
}
