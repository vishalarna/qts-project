import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Certification } from 'src/app/_DtoModels/Certification/Certification';
import { CertificationLatestActivityVM } from 'src/app/_DtoModels/Certification/CertificationLatestActivityVM';
import { CertificationStatsVM } from 'src/app/_DtoModels/Certification/CertificationStatsVM';
import { CertificationCreateOptions } from 'src/app/_DtoModels/Certification/CertificationCreateOptions';
import { CertificationILAVM, CertifyingBodyCompactOptions } from 'src/app/_DtoModels/CertifyingBody/CertifyingBodyCompactOptions';
import { Certification_HistoryCreateOptions } from 'src/app/_DtoModels/Certification_History/Certification_HistoryCreateOptions';
import { environment } from 'src/environments/environment';
import { firstValueFrom, Observable } from 'rxjs';
import { SubRequirementVM } from '@models/CertifyingBody/CertifyingBodyWithSubRequirementsVM';



@Injectable({
    providedIn: 'root'
  })
  export class CertificationService {

    baseUrl = environment.QTD + 'certifications';
    constructor(private http: HttpClient) {}
    getCertCategoryWithCert()
    {
      return firstValueFrom(this.http.get(this.baseUrl + `/issuingauthority/nest`).pipe(
        map((res:any)=>{
          return res["result"] as CertifyingBodyCompactOptions[];
        })
      ));
    }
    getCertificationDataWithILA(certId :any,ilaId:any)
    {
      return firstValueFrom(this.http.get(this.baseUrl + `/${certId}/ila/${ilaId}`).pipe(
        map((res:any)=>{
          return res["loc"] as CertificationILAVM;
        })
      ));
    }

    getLinkedCertifications(ilaId:any)
    {
      return firstValueFrom(this.http.get(this.baseUrl + `/ila/${ilaId}`).pipe(
        map((res:any)=>{
          return res["result"] as string[];
        })
      ));
    }

    SaveCertificationDataWithILA(data:CertificationILAVM)
    {
      return firstValueFrom(this.http.post(this.baseUrl + `/ila/save`,data).pipe(
        map((res:any)=>{
          return res["loc"] as boolean;
        })
      ));
    }

    DeleteCertificationDataWithILA(ilaId:any,certId:any)
    {
      return firstValueFrom(this.http.delete(this.baseUrl + `/${certId}/ila/${ilaId}`).pipe(
        map((res:any)=>{
          return res["result"] as boolean;
        })
      ));
    }

    create(options: CertificationCreateOptions)
    {
      
      return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
        map((res:any)=>{
          return res['cer'] as Certification;
        })
      ));
    }
    getCount(){
      return firstValueFrom(this.http.get(this.baseUrl + `/count`).pipe(
        map((res:any)=>{
          return res['result'] as number;
        })
      ));
    }
    get(id: any)
    {
      
      return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
        map((res: any) => {

          return res['loc'] as Certification;

        })
      ));
    }
    makeActiveInactiveOrDelete(options:Certification_HistoryCreateOptions){
      return firstValueFrom(this.http.delete(this.baseUrl,{body:options}).pipe(
        map((res:any)=>{
          return res;
        })
      ));
    }

    update(id:any,options:CertificationCreateOptions)
    {
      
      return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
        map((res:any)=>{
          return res["cer"];
        })
      ));
    }
    getStatsCount()
    {
      
      return firstValueFrom(this.http
        .get(this.baseUrl + '/stats')
        .pipe(
          map((res: any) => {
            return res['stats'] as CertificationStatsVM;
          })
        ));
    }
    getStatusHistory(getLatest:boolean = false)
    {
      
      return firstValueFrom(this.http
        .get(this.baseUrl + `/history/latest/${getLatest}`)
        .pipe(
          map((res: any) => {
            return res['history'] as CertificationLatestActivityVM[];
          })
        ));
    }
    getAll() {
      return firstValueFrom(this.http
        .get(this.baseUrl)
        .pipe(
          map((res: any) => {
            return res['locList'] as any[];
          })
        )
        );
    }

    //subrequirement get api call
    getSubRequirement(id: any)
    {
      
      return firstValueFrom(this.http.get(this.baseUrl + `/${id}/subrequirement`).pipe(
        map((res: any) => {

          return res['result'];
        })
      ));
    }

    getcatList(notlinkedWith: string) {
      return firstValueFrom(this.http
        .get(this.baseUrl + `/${notlinkedWith}/catlist`)
        .pipe(
          map((res: any) => {
            return res['result'] as any;
          })
        )
        );
    }

    getcertList(notlinkedWith: string) {
      return firstValueFrom(this.http
        .get(this.baseUrl + `/${notlinkedWith}/certlist`)
        .pipe(
          map((res: any) => {
            return res['result'] as any;
          })
        )
        );
    }

    getNercCertList(){
      return firstValueFrom(this.http
        .get(this.baseUrl + '/nercCertList')
        .pipe(
          map((res: any) => {
            return res['result'] as any[];
          })
        )
        );
    }

    getSubRequirementsByCertId(certId: any) {
      return firstValueFrom(
        this.http.get(this.baseUrl + `/${certId}/subrequirements`).pipe(
          map((res: any) => {
            return res["subReqs"] as SubRequirementVM[];
          })
        )
      );
    }
  }


