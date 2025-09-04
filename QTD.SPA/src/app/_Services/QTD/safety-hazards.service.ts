import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Provider } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { SaftyHazardUpdateOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardUpdateOptions';
import { SaftyHazardCreateOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCreateOptions';
import { SaftyHazardOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardOptions';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { SaftyHazard_CategoryCompactOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { SaftyHazard_Tool_LinkOptions } from 'src/app/_DtoModels/SaftyHazard_Tool/SaftyHazard_Tool_LinkOptions';
import { SafetyHazardWithLinkCount } from 'src/app/_DtoModels/SaftyHazard/SafetyHazardWithLinkCount';
import { RegulatoryRequirementWithLinkCount } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementWithLinkCount';
import { SaftyHazard_RR_LinkOptions } from 'src/app/_DtoModels/SaftyHazard_RR_Link/SaftyHazard_RR_LinkOptions';
import { SH_Task_LinkOptions } from 'src/app/_DtoModels/SH_Task_Link/SH_Task_LinkOptions';
import { SaftyHazardCompactOption } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCompactOptions';
import { ProcedureWithLinkCount } from 'src/app/_DtoModels/Procedure/ProcedureWithLinkCount';
import { SaftyHazard_ProcedureLinkOptions } from 'src/app/_DtoModels/SaftyHazard_ProcedureLink/SaftyHazard_ProcedureLinkOptions';
import { SaftyHazardWithSet } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardWithSet';
import { SH_EO_LinkOptions } from 'src/app/_DtoModels/SH_EO_Link/SH_EO_LinkOptions';
import { EOWithCountOptions } from 'src/app/_DtoModels/EnablingObjective/EOWithCountOptions';
import { SHLatestActivityVM } from 'src/app/_DtoModels/SafetyHazard_StatusHistory/SHLatestActivity';
import { SHStatsVM } from 'src/app/_DtoModels/SaftyHazard/SH_StatsVM';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { SH_ILA_LinkOptions } from 'src/app/_DtoModels/SH_ILA_Link/SH_ILA_LinkOptions';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { ILATopicDataVM } from '@models/ILA_Topic/ILATopicDataVM';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class SafetyHazardsService {
  baseUrl = environment.QTD + 'saftyHazards';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {

        return res['shList'] as SaftyHazard[];
      })
    ));
  }

  get(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res: any) => {

        return res['sh'] as any;
      })
    ));
  }

  create(options: SaftyHazardCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {

        return res['sh'] as any;
      })
    ));
  }

  update(id: any, options: SaftyHazardUpdateOptions) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`, options).pipe(
      map((res: any) => {
        return res['sh'] as any;
      })
    ));
  }

  updateOnlyDescription(id:any,description:SaftyHazardCreateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/desc`,description).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  delete(id: any, options: SaftyHazardOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`, httpOptions).pipe(
      map((res: any) => {

        return res.message;
      })
    ));
  }

  getNotLinked(option:string){
    return firstValueFrom(this.http.get(this.baseUrl + `/${option}/notLinked`).pipe(
      map((res:any)=>{
        return res['result'] as SaftyHazard_Category[];
      })
    ));
  }

  getcatList(option:string){
    return firstValueFrom(this.http.get(this.baseUrl + `/${option}/catlist`).pipe(
      map((res:any)=>{
        return res['result'] as SaftyHazard_Category[];
      })
    ));
  }

  getshList(option:string){
    return firstValueFrom(this.http.get(this.baseUrl + `/${option}/shlist`).pipe(
      map((res:any)=>{
        return res['result'] as SaftyHazard[];
      })
    ));
  }

  getLatestActivity(getLatest:boolean=false){
    return firstValueFrom(this.http.get(this.baseUrl + `/history/latest/${getLatest}`).pipe(
      map((res:any)=>{
        return res['result'] as SHLatestActivityVM[];
      })
    ));
  }

  getStats(){
    return firstValueFrom(this.http.get(this.baseUrl + `/stats`).pipe(
      map((res:any)=>{
        return res['result'] as SHStatsVM;
      })
    ))
  }

  getWithSet(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/withSet`).pipe(
      map((res:any)=>{
        return res['result'] as SaftyHazardWithSet;
      })
    ));
  }

  getSHCategoryWithSH(){
    return firstValueFrom(this.http.get(this.baseUrl + `/categories/nest`).pipe(
      map((res:any)=>{
        return res["result"] as SaftyHazard_CategoryCompactOptions[];
      })
    ));
  }

  getSHCategoryWithSHList(){
    return firstValueFrom(this.http.get(this.baseUrl + `/categories`).pipe(
      map((res:any)=>{
        return res["sh_CatList"];
      })
    ));
  }

  getShWithSHCatId(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/cat/${id}`).pipe(
      map((res:any)=>{
        return res["result"] as SaftyHazard_Category[];
      })
    ));
  }

  linkTools(id:any,options:SaftyHazard_Tool_LinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/tool`,options).pipe(
      map((res:any)=>{
        return res['result'] as SaftyHazard;
      })
    ));
  }

  getToolsLinkedToSH(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/tool/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as Tool[];
      })
    ));
  }

  linkRR(id:any,options:SaftyHazard_RR_LinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/rr`,options).pipe(
      map((res:any)=>{
        return res["result"] as SaftyHazard;
      })
    ));
  }

  unlinkRR(id:any,options:SaftyHazard_RR_LinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/rr`, { body: options }).pipe(
      map((res:any)=>{
        return res["message"] as string;
      })
    ));
  }

  getLinkedRR(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/rr/count`).pipe(
      map((res:any)=>{
        return res["result"] as RegulatoryRequirementWithLinkCount[];
      })
    ));
  }

  linkProcedures(id:any,options:SaftyHazard_ProcedureLinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/procedure`,options).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }

  unlinkProcedure(id:any,options:SaftyHazard_ProcedureLinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/procedure`,{ body: options }).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  getLinkedProcedures(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/procedure/count`).pipe(
      map((res:any)=>{
        return res['result'] as ProcedureWithLinkCount[];
      })
    ));
  }

  getLinkedTasks(shId: any) 
  {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${shId}/task/count`)
      .pipe(
        map((res: any) => {
          return res['result'] as SafetyHazardWithLinkCount[];
        })
      )
      );
  }

  LinkTasks(id: any, options: SH_Task_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/task`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SaftyHazard;
        })
      )
      );
  }

  copy(id:any,options:SaftyHazardCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/copy`,options).pipe(
      map((res:any)=>{
        return res['result'] as SaftyHazard;
      })
    ));
  }

  unlinkTasks(id: any, options: SH_Task_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/task`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getSHLinkedToTask(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/task/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as SaftyHazardCompactOption[];
        })
      )
      );
  }

  getSHLinkedToProcedure(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/procedure/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as SaftyHazardCompactOption[];
        })
      )
      );
  }

  getSHLinkedToRR(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/rr/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as SaftyHazardCompactOption[];
        })
      )
      );
  }

  LinkEOs(id: any, options: SH_EO_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/eo`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SaftyHazard;
        })
      )
      );
  }

  unlinkEOs(shId: any, options: SH_EO_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${shId}/eo`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getLinkedEOs(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/eo/count`).pipe(
      map((res:any)=>{
        return res['result'] as EOWithCountOptions[];
      })
    ));
  }

  getSHLinkedToEO(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/eo/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as SaftyHazardCompactOption[];
        })
      )
      );
  }

  getSHLinkedToILA(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/ila/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as SaftyHazardCompactOption[];
        })
      )
      );
  }

  getProviderWithILAs(){
    return firstValueFrom(this.http.get(this.baseUrl+ `/provider/ila`).pipe(
      map((res: any) =>{
        return res["result"] as Provider[];
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

  LinkILA(id: any, options: SH_ILA_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/ila`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  unlinkILA(id: any, options: SH_ILA_LinkOptions) {
    
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/ila`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getLinkedILA(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/ila/count`).pipe(
      map((res:any)=>{
        return res['result'] as ILAWithCountOptions[];
      })
    ));
  }
}
