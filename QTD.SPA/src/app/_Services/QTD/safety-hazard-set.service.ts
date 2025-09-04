import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { SafetyHazard_Set } from 'src/app/_DtoModels/SaftyHazard_Set/SafetyHazard_Set';
import { SaftyHazard_SetCreateOptions } from 'src/app/_DtoModels/SaftyHazard_Set/SaftyHazard_SetCreateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SafetyHazardSetService {
  baseUrl = environment.QTD + 'saftyHazardSet';
  constructor(private http: HttpClient) { }

  createAndLink(id: any, options: SaftyHazard_SetCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}`, options).pipe(
      map((res: any) => {
        return res['result'] as SafetyHazard_Set;
      })
    ))
  }

  getForSpecificSH(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/sh/${id}`).pipe(
      map((res: any) => {
        return res['result'] as SafetyHazard_Set[];
      })
    ));
  }

  update(id: any, options: SaftyHazard_SetCreateOptions) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ))
  }
}
