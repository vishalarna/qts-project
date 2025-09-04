import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { map } from 'rxjs/operators';
import { EmpSettingsReleaseTypeVM } from "@models/EmpSettingsReleaseType/EmpSettingsReleaseTypeVM";
import { firstValueFrom } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class EmpSettingsReleaseTypeService {
  baseUrl = environment.QTD + 'empSettingsReleaseType';
  constructor(private http: HttpClient) {}

  getEmpSettingsReleaseTypes(){
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as EmpSettingsReleaseTypeVM[];
      })
    ));
  }
}
