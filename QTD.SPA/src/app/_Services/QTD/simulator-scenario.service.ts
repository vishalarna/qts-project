import { Position } from './../../_DtoModels/Position/Position';
import { SimulatorScenarioTaskObjectives_LinkOptions } from './../../_DtoModels/SimulatorScenarioTaskObjectives_Link/SimulatorScenarioTaskObjectives_LinkOptions';
import { SimulatorScenarioPosition_LinkOptions } from './../../_DtoModels/SimulatorScenarioPosition_Link/SimulatorScenarioPosition_LinkOptions';
import { SimulatorScenarioCreateOptions } from './../../_DtoModels/SimulatorScenario/SimulatorScenarioCreateOptions';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { environment } from 'src/environments/environment';
import { SimulatorScenario } from 'src/app/_DtoModels/SimulatorScenario/SimulatorScenario';
import { SimulatorScenario_EnablingObjectives_LinkOptions } from 'src/app/_DtoModels/SimulatorScenario_EnablingObjectives_Link/SimulatorScenario_EnablingObjectives_LinkOptions';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { SimulatorScenarioILA_LinkOptions } from 'src/app/_DtoModels/SimulatorScenarioILA_Link/SimulatorScenarioILA_LinkOptions';
import { firstValueFrom } from 'rxjs';
import { SimulatorScenario_Script_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Script_VM';

@Injectable({
  providedIn: 'root'
})
export class SimulatorScenarioService {
  baseUrl = environment.QTD + 'simScenario';
  constructor(private http:HttpClient) { }

  create(options:SimulatorScenarioCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }

  getAll(){
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as SimulatorScenario[];
      })
    ));
  }

  getEOById(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/eo`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective;
      })
    ));
  }

  getTaskbyId(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/task`).pipe(
      map((res:any)=>{
        return res['result'] as Task;
      })
    ));
  }

  getPositionById(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/position`).pipe(
      map((res:any)=>{
        return res['result'] as Position;
      })
    ));
  }

  linkPositions(id: any, options: SimulatorScenarioPosition_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/position`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  linkTaskObjectives(id: any, options: SimulatorScenarioTaskObjectives_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/task`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  linkEnablingObjectives(id: any, options: SimulatorScenario_EnablingObjectives_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/eo`, options)
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }

  linkILA(id: any, options: SimulatorScenarioILA_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/ila`, options)
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }
  
  createScriptAsync(options: SimulatorScenario_Script_VM) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/scripts`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_Script_VM;
        })
      )
      );
  }

  copyScriptAsync(scritpId: string,eventId:string) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/scripts/${scritpId}/events/${eventId}/copy`, {})
      .pipe(
        map((res: any) => {
          return res.result;
        })
      )
      );
  }

 getScriptAsync(scriptId: string,eventId:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/scripts/${scriptId}/events/${eventId}`)
      .pipe(
        map((res: any) => {
          return res.result as SimulatorScenario_Script_VM;
        })
      )
      );
  }

  updateScriptAsync(scriptId: string,eventId:string, option : SimulatorScenario_Script_VM) {
      return firstValueFrom(this.http
        .put(this.baseUrl + `/scripts/${scriptId}/events/${eventId}/update`,option)
        .pipe(
          map((res: any) => {
            return res.result as SimulatorScenario_Script_VM;
          })
        )
      );
  }

  deleteScriptAsync(scriptId: string) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/scripts/events/${scriptId}/delete`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getAllScriptAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/scripts/all`)
      .pipe(
        map((res: any) => {
          return res.result as any ;
        })
      )
      );
  }

}
