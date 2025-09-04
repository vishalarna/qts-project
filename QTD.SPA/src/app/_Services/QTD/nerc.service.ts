import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { CehUploadGetOptions } from "src/app/_DtoModels/NERC/CEHUploadGetOptions";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
  })
export class NercService {
    baseUrl = environment.QTD + "nerc";
    constructor(private http:HttpClient) { }

    getCehUploadAsync(options: CehUploadGetOptions){
      return firstValueFrom(this.http.post<CehUploadGetOptions>(this.baseUrl + "/cehupload", options)
      .pipe(
        map((res) => {
          return res["result"];
        })
      ));
    }
}