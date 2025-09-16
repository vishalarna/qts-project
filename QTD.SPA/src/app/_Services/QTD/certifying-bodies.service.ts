import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CertifyingBodyWithSubRequirementsVM, SubRequirementVM } from '@models/CertifyingBody/CertifyingBodyWithSubRequirementsVM';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { CertificateUpdateOptions } from 'src/app/_DtoModels/Certification/CertificateUpdateOptions';
import { Certification } from 'src/app/_DtoModels/Certification/Certification';
import { CertificationCreateOptions } from 'src/app/_DtoModels/Certification/CertificationCreateOptions';
import { CertifyingBody } from 'src/app/_DtoModels/CertifyingBody/CertifyingBody';
import { CertifyingBodyCreateOptions } from 'src/app/_DtoModels/CertifyingBody/CertifyingBodyCreateOptions';
import { CertifyingBodyUpdateOptions } from 'src/app/_DtoModels/CertifyingBody/CertifyingBodyUpdateOptions';
import { CertifyingBody_HistoryCreateOptions } from 'src/app/_DtoModels/CertifyingBody/CertifyingBody_HistoryCreateOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CertifyingBodiesService {
  baseUrl = environment.QTD + 'certifyingBodies';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['certifyingBodies'] as CertifyingBody[];
        })
      )
      );
  }

  create(options: CertifyingBodyCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res['result'] as CertifyingBody;
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res['certifyingBody'] as CertifyingBody;
        })
      )
      );
  }

  getWithoutInclude(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/withoutInclude`).pipe(
      map((res:any)=>{
        return res['result'] as CertifyingBody;
      })
    ));
  }


  update(id: any, options: CertifyingBodyUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }

  delete(id: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }
  //#region certifications
  getAllCertifications(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/certifications`)
      .pipe(
        map((res: any) => {

          return res['certifications'] as Certification[];
        })
      )
      );
  }

  getCertification(id: any, certificationid: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/certifications/${certificationid}`)
      .pipe(
        map((res: any) => {

          return res['certification'] as Certification;
        })
      )
      );
  }

  createCertification(id: any, options: CertificationCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/certifications`, options)
      .pipe(
        map((res: any) => {

          return res['certification'] as Certification;
        })
      )
      );
  }

  updateCertification(
    id: any,
    certificationid: any,
    options: CertificateUpdateOptions
  ) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/certifications/${certificationid}`, options)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }

  deleteCertification(id: any, certificationid: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/certifications/${certificationid}`)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }
  //#endregion certifications

  makeActiveInactiveOrDelete(id: any, options: CertifyingBody_HistoryCreateOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`, { body: options })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  isEmployeeCertification(id:any){
    return firstValueFrom(this.http
    .get(this.baseUrl + `/${id}/isEmployeeCertification`)
    .pipe(
      map((res: any) => {
        return res['result'];
      })
    )
    );
  }
  getCertifyingBodiesByLevelEditingAsync(ilaId:any,isLevelEditing?:boolean){
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}/certifying/${isLevelEditing}`)
      .pipe(
        map((res: any) => {
          return res['result'] as CertifyingBodyWithSubRequirementsVM[];
        })
      )
      );
  }

  getCertifyingBodiesWithSubRequirementsAsync(isLevelEditing: boolean) {
    return firstValueFrom(
      this.http
        .get(this.baseUrl + `/subrequirements/${isLevelEditing}`)
        .pipe(
          map((res: any) => {
            return res['subReqs'] as SubRequirementVM[];
          })
        )
    );
  }

}
