import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ScormUploadAddOptions } from 'src/app/_DtoModels/Scorm/ScormUploadAddOptions';
import { ScormUploadDeleteOptions } from 'src/app/_DtoModels/Scorm/ScormUploadDeleteOptions';
import { environment } from 'src/environments/environment';
import { IscormService } from './iscorm.service';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiScormService implements IscormService {
  baseUrl = environment.QTD;
  constructor(private http: HttpClient) {
  }

  getScormUploadsAsync = (ilaId: number, current: Boolean) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/ila/${ilaId}/scorm`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  addScormUploadAsync = (cbtId: number, options: ScormUploadAddOptions) => {
    return firstValueFrom(this.http.post<any>(this.baseUrl + `cbt/${cbtId}/scorm`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  disconnectScormUploadAsync = (cbtId: number, options: ScormUploadDeleteOptions) => {
    return firstValueFrom(this.http.delete<any>(this.baseUrl + `cbt/${cbtId}/scorm/delete`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  getScormUploadAsync = (ilaId: number, scormUploadId: number) => {
    return firstValueFrom(this.http
      .get(this.baseUrl +`/ila/${ilaId}/scorm/${scormUploadId}` + scormUploadId)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getAllCbtScormUploadsAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `cbt/scormUploads/all`)
      .pipe(
        map((res: any) => {
          return res["result"];
        })
      )
      );
  }

}
