import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { ILAPositionLinkOption } from 'src/app/_DtoModels/ILA_Position_Link/ILA_Position_LinkOptions';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IlaPositionLinkService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.QTD + 'ila/';

  create(id: any, options: ILAPositionLinkOption) {
    return firstValueFrom(this.http.post(this.baseUrl + id + "/position", options).pipe(
      map((res: any) => {
        return res
      })
    ))
  }

  getAll(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + id + "/position").pipe(
      map((res: any) => {
        return res['result'] as Position[]
      })
    ))
  }

  delete(ilaId:any,linkIds:ILAPositionLinkOption){
    return firstValueFrom(this.http.delete(this.baseUrl + ilaId + "/position" , { body:linkIds }).pipe(
      map((res:any)=>{
        return res
      })
    ))
  }
}
