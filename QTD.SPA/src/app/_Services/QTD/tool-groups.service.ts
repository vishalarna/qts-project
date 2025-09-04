import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { ToolGroupCreateOptions } from 'src/app/_DtoModels/ToolGroup/ToolGroupCreateOptions';
import { ToolGroup } from 'src/app/_DtoModels/ToolGroup/ToolGroup';
import { ToolAddOptions } from 'src/app/_DtoModels/Tool/ToolAddOptions';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ToolGroupsService {
  baseUrl = environment.QTD + 'toolGroups';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          
          return res['tgList'] as ToolGroup[];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          
          return res['tg'] as ToolGroup;
        })
      )
      );
  }

  create(options: ToolGroupCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          
          return res['tg'] as ToolGroup;
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

  addTool(toolGroupid: any, options: ToolAddOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${toolGroupid}/tools`, options)
      .pipe(
        map((res: any) => {
          
          return res['tool'] as Tool;
        })
      )
      );
  }

  removeTool(toolGroupid: any, toolid: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${toolGroupid}/tools/${toolid}`)
      .pipe(
        map((res: any) => {
          
          return res.message;
        })
      )
      );
  }
}
