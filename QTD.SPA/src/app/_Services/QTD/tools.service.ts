import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Result } from '@models/result';
import { UnlinkedTool } from '@models/Tool';
import { environment } from 'src/environments/environment';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { ToolCreateOptions } from 'src/app/_DtoModels/Tool/ToolCreateOptions';
import { ToolCategory } from 'src/app/_DtoModels/ToolCategory/ToolCategory';
import { ToolCategoryCreateOptions } from 'src/app/_DtoModels/ToolCategory/ToolCategoryCreateOptions';
import { ToolCategoryOptions } from 'src/app/_DtoModels/ToolCategory/ToolCategoryOptions';
import { Link_Tool_Options } from 'src/app/_DtoModels/Tool/Link_Tool_Options';
import { ToolBulkEdit_Options } from 'src/app/_DtoModels/Tool/ToolBulkEdit_Options';


@Injectable({
  providedIn: 'root',
})
export class ToolsService {
  private readonly toolsBaseUrl = environment.QTD + 'tools';
  private readonly toolBaseUrl = environment.QTD + 'tool';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return firstValueFrom(this.http
      .get(this.toolsBaseUrl)
      .pipe(
        map((res: any) => {
          return res['tools'] as Tool[];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.toolsBaseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['tool'] as any;
        })
      )
      );
  }

  create(options: ToolCreateOptions) {
    return firstValueFrom(this.http
      .post(this.toolsBaseUrl, options)
      .pipe(
        map((res: any) => {
          return res['tool'] as Tool;
        })
      )
      );
  }

  getAllToolCategories(includeTools: boolean = false) {
    let url = this.toolsBaseUrl + (includeTools ? '/categoryWithTools' : '/category');
    return firstValueFrom(this.http
      .get(url)
      .pipe(
        map((res: any) => {
          return res['result'] as ToolCategory[];
        })
      )
      );
  }

  getLinkedTasks(id:any) {
    return firstValueFrom(this.http
      .get(this.toolBaseUrl + `/tasks/${id}`)
      .pipe(
        map((res: any) => {
          return res['tasks'] as any;
        })
      )
      );
  }

  unlinkTasks(options: Link_Tool_Options) {
    return firstValueFrom(this.http
      .post(this.toolBaseUrl + `/tasks/unlink`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getLinkedSHs(id:any) {
    return firstValueFrom(this.http
      .get(this.toolBaseUrl + `/safetyHazards/${id}`)
      .pipe(
        map((res: any) => {
          return res['tasks'] as any;
        })
      )
      );
  }

  unlinkSHs(options: Link_Tool_Options) {
    return firstValueFrom(this.http
      .post(this.toolBaseUrl + `/safetyHazards/unlink`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getNumber(id:any){
    return firstValueFrom(this.http
      .get(this.toolsBaseUrl + `/${id}/number`)
      .pipe(
        map((res:any)=>{
          return res['result'] as number;
        })
      ));
  }

  createCategory(options: ToolCategoryCreateOptions) {
    return firstValueFrom(this.http
      .post(this.toolsBaseUrl + '/category', options)
      .pipe(
        map((res: any) => {
          return res['result'] as ToolCategory;
        })
      )
      );
  }

  getToolCategoryData(id: any) {
    return firstValueFrom(this.http.get(this.toolsBaseUrl + `/category/${id}`).pipe(
      map((res: any) => {

        return res['result'] as ToolCategory;
      })
    ));
  }

  updateToolCategory(id:any,options:ToolCategory){
    return firstValueFrom(this.http.put(this.toolsBaseUrl + `/category/${id}`,options).pipe(
      map((res:any)=>{
        return res["result"] as ToolCategory;
      })
    ));
  }

  updateTool(id:any,options:ToolCreateOptions){
    return firstValueFrom(this.http.put(this.toolsBaseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res["result"] as any;
      })
    ));
  }

  makeActiveInactiveOrDeleteCategory(id:any,options:ToolCategoryOptions){
    return firstValueFrom(this.http.delete(this.toolsBaseUrl + `/category/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  makeActiveInactiveOrDelete(id:any,options:ToolCategoryOptions){
    return firstValueFrom(this.http.delete(this.toolsBaseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  getAllToolStatus() {
    return firstValueFrom(this.http
      .get(this.toolsBaseUrl + `/history/all`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }
  getToolStatistics(){
    return firstValueFrom(this.http
      .get(this.toolsBaseUrl + `/statistics`)
      .pipe(
        map((res: any) => {
          return res['tools'] as any;
        })
      )
      );
  }

  getToolList(option:string){
    return firstValueFrom(this.http.get(this.toolBaseUrl + `/${option}/toollist`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }

  getCatList(option:string){
    return firstValueFrom(this.http.get(this.toolBaseUrl + `/${option}/catlist`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }
  
  getToolsNotLinkedToTask(): Observable<Result<UnlinkedTool[]>> {
    return this.http.get<Result<UnlinkedTool[]>>(`${this.toolsBaseUrl}/notLinkedToTask`);
  }

  getToolsNotLinkedToEo(): Observable<Result<UnlinkedTool[]>> {
    return this.http.get<Result<UnlinkedTool[]>>(`${this.toolsBaseUrl}/notLinkedToEo`);
  }

  LinkTasks(options: Link_Tool_Options) {
    return firstValueFrom(this.http
      .post(this.toolBaseUrl + `/tasks/link`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  LinkSHs(options: Link_Tool_Options) {
    return firstValueFrom(this.http
      .post(this.toolBaseUrl + `/safetyHazards/link`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  LinkEOs(options: Link_Tool_Options) {
    return firstValueFrom(this.http
      .post(this.toolBaseUrl + `/skills/link`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  unlinkEOs(options: Link_Tool_Options) {
    return firstValueFrom(this.http
      .post(this.toolBaseUrl + `/skills/unlink`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  bulkEditTools(options: ToolBulkEdit_Options) {
    return firstValueFrom(this.http
      .post(this.toolsBaseUrl + '/bulk/edit' , options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getLinkedEOs(id:any) {
    return firstValueFrom(this.http
      .get(this.toolBaseUrl + `/skills/${id}`)
      .pipe(
        map((res: any) => {
          return res['tasks'] as any;
        })
      )
      );
  }

  getToolsLinkedToTask(id:any){
    return firstValueFrom(this.http.get(this.toolBaseUrl + `/${id}/tasklist`).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }

  getToolsLinkedToSkill(id:any){
    return firstValueFrom(this.http.get(this.toolBaseUrl + `/skilllist/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }

  getToolsLinkedToSH(id:any){
    return firstValueFrom(this.http.get(this.toolBaseUrl + `/shlist/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }

}
