import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ILAPrerequisitesLinkOptions } from 'src/app/_DtoModels/ILA_Prerequisites_Link/ILA_Prerequisites_LinkOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IlaPreRequisitesLinkService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'ila/';

  /*to send data to ILA_Procedures_Link Table*/
  create(id: any, options: ILAPrerequisitesLinkOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + id + "/preReq", options).pipe(
      map((res: any) => {
        return res
      })
    ))
    }
}
