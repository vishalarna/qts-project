import { HttpClient } from '@angular/common/http';
import { Injectable, Provider } from '@angular/core';
import { map } from 'rxjs/operators';
import { ProcedureWithLinkCount } from 'src/app/_DtoModels/Procedure/ProcedureWithLinkCount';
import { RegulatoryRequirement } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import { RegulatoryRequirementOptions } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementOptions';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RegulatoryRequirementWithLinkCount } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementWithLinkCount';
import { RRCreateOptions } from 'src/app/_DtoModels/RegulatoryRequirements/RR_CreateOptions';
import { RR_EO_LinkOptions } from 'src/app/_DtoModels/RR_EnablingObjective/RR_EO_LinkOptions';
import { RR_SaftyHazard_LinkOptions } from 'src/app/_DtoModels/RR_SaftyHazard_Link/RR_SaftyHazard_LinkOptions';
import { RR_Task_LinkOptions } from 'src/app/_DtoModels/RR_Task_Link/RR_Task_LinkOptions';
import { SafetyHazardWithLinkCount } from 'src/app/_DtoModels/SaftyHazard/SafetyHazardWithLinkCount';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskWithCountOptions } from 'src/app/_DtoModels/Task/TaskWithCountOptions';
import { RR_Procedure_LinkOptions } from 'src/app/_DtoModels/RegulatoryRequirements/RR_Procedure_LinkOptions';
import { environment } from 'src/environments/environment';
import { RR_StatsVM } from 'src/app/_DtoModels/RegulatoryRequirements/RR_StatsVM';
import { RRIssuingAuthority } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthority';
import { RRLatestActivityVM } from 'src/app/_DtoModels/RR_StatusHistory/RRLatestActivity';
import { ILA_Topic } from 'src/app/_DtoModels/ILA_Topic/ILA_Topic';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { RR_ILA_LinkOptions } from 'src/app/_DtoModels/RR_ILA/RR_ILA_LinkOptions';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { ILAProviderDataVM } from '@models/Provider/ILAProviderDataVM';
import { ILATopicDataVM } from '@models/ILA_Topic/ILATopicDataVM';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RegulatoryRequirementService {
  baseUrl = environment.QTD + 'rr';
  constructor(private http: HttpClient) {}

  create(options: RRCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirement;
        })
      )
      );
  }

  copy(id:any,options:RRCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/copy`,options).pipe(
      map((res:any)=>{
        return res['result'] as RegulatoryRequirement;
      })
    ));
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirement;
        })
      )
      );
  }

  update(id: any, options: RRCreateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirement;
        })
      )
      );
  }

  getCompactData(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/compact/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirementsCompact;
        })
      )
      );
  }

  delete(id: any, options: RegulatoryRequirementOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`, { body: options })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  linkProcedures(id: any, options: RR_Procedure_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/procedure`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  unlinkProcedures(id: any, options: RR_Procedure_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/procedure`, { body: options })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getLinkedProcedures(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/procedure`)
      .pipe(
        map((res: any) => {
          return res['result'] as ProcedureWithLinkCount[];
        })
      )
      );
  }

  getRRsLinkedProcedure(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/procedure/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirementsCompact[];
        })
      )
      );
  }

  LinkTasks(id: any, options: RR_Task_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/task`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirement;
        })
      )
      );
  }

  getLinkedTasks(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/task/count`)
      .pipe(
        map((res: any) => {
          return res['result'] as TaskWithCountOptions[];
        })
      )
      );
  }

  unlinkTasks(id: any, options: RR_Task_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/task`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  LinkSH(id: any, options: RR_SaftyHazard_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/safetyHazard`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SaftyHazard;
        })
      )
      );
  }

  unlinkSH(id: any, options: RR_SaftyHazard_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/safetyHazard`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getRRLinkedToSh(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/safetyHazard/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirementsCompact[];
        })
      )
      );
  }

  getLinkedSH(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/safetyHazard`)
      .pipe(
        map((res: any) => {
          return res['result'] as SafetyHazardWithLinkCount[];
        })
      )
      );
  }

  getRRLinkedToTask(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/task/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirementsCompact[];
        })
      )
      );
  }

  getStatsCount() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/stats')
      .pipe(
        map((res: any) => {
          return res['stats'] as RR_StatsVM;
        })
      )
      );
  }

  getNotLinkedWith(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/notLinked`)
      .pipe(
        map((res: any) => {
          return res['result'] as RRIssuingAuthority[];
        })
      )
      );
  }

  getcatList(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/catlist`)
      .pipe(
        map((res: any) => {
          return res['result'] as RRIssuingAuthority[];
        })
      )
      );
  }

  getrrList(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/rrlist`)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirement[];
        })
      )
      );
  }




  getStatusHistory() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/history`)
      .pipe(
        map((res: any) => {
          return res['history'] as RRLatestActivityVM[];
        })
      )
      );
  }

  LinkEOs(id:any,options:RR_EO_LinkOptions){
    
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/eo`,options).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  UnlinkEOs(id:any,options:RR_EO_LinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/eo/`, { body: options }).pipe(
      map((res:any)=>{
        return res["message"];
      })
    ));
  }

  getLinkedEnablingObjectives(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/eo/count`).pipe(
      map((res:any)=>{
        return res['result'] as RegulatoryRequirementWithLinkCount[];
      })
    ));
  }

  getRRLinkedToILA(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/ila/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as RegulatoryRequirementsCompact[];
      })
    ));
  }

  getRRLinkedToEO(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`+ `/eo`).pipe(
      map((res:any)=>{
        return res['result'] as RegulatoryRequirementsCompact[]
      })
    ));
  }

  getProviderWithILAs(){
    return firstValueFrom(this.http.get(this.baseUrl+ `/provider/ila`).pipe(
      map((res: any) =>{
        return res["result"] as ILAProviderDataVM[];
      })
    ));
  }

  getTopicWithILAs(){
    return firstValueFrom(this.http.get(this.baseUrl+ `/topic/ila`).pipe(
      map((res: any) =>{
        return res['result'] as ILATopicDataVM[];
      })
    ));
  }

  getLinkedILA(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/ila`).pipe(
      map((res:any)=>{
        return res['result'] as ILAWithCountOptions[];
      })
    ));
  }

  LinkILA(id: any, options: RR_ILA_LinkOptions) {
    
    
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/ila`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  unlinkILA(id: any, options: RR_ILA_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/ila`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }
}
