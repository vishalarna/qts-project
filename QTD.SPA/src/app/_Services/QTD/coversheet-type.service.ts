import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { CoverSheetType } from 'src/app/_DtoModels/CoverSheetType/CoverSheetType';
import { CoverSheetTypeCreateOptions } from 'src/app/_DtoModels/CoverSheetType/CoverSheetTypeCreateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CoversheetTypeService {
  baseUrl = environment.QTD + 'coversheettype';
  constructor(private http: HttpClient) { }

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {
        return res['coversheetType'] as CoverSheetType[];
      })
    ));
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['coversheetType'] as CoverSheetType;
      })
    ));
  }

  create(options:CoverSheetTypeCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  delete(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }
}
