import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { PositionCreateOptions } from 'src/app/_DtoModels/Position/PositionCreateOptions';
import { PositionUpdateOptions } from 'src/app/_DtoModels/Position/PositionUpdateOptions';
import { TrainingProgram } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram';
import { PositionCompact } from 'src/app/_DtoModels/Position/PositionCompact';
import { Position_Task_LinkOptions } from 'src/app/_DtoModels/Position_Task_Link/Position_Task_Link';
import { TaskWithCountOptions } from 'src/app/_DtoModels/Task/TaskWithCountOptions';
import { Position_StatsVM } from 'src/app/_DtoModels/Position/Position_StatsVM';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EOWithCountOptions } from 'src/app/_DtoModels/EnablingObjective/EOWithCountOptions';
import { PositionOptions } from 'src/app/_DtoModels/Position/PositionOptions'
import { PositionOption } from 'src/app/_DtoModels/Position/PositionOption';
import { EmployeeWithCountOptions } from 'src/app/_DtoModels/Employee/EmployeeWithCountOptions';
import { Position_Employee_LinkOptions } from 'src/app/_DtoModels/Position_Employee_Link/Position_Employee_LinkOptions';
import { PositionLatestActivityVM } from 'src/app/_DtoModels/Position/PositionLatestActivityVM';
import { TrainingGroup } from 'src/app/_DtoModels/TrainingGroup/TrainingGroup';
import { EmployeePosition } from 'src/app/_DtoModels/EmployeePosition/EmployeePosition';
import { TaskWithCountR5R6Options } from 'src/app/_DtoModels/Task/TaskWithCountR5R6Options';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PositionsService {
  baseUrl = environment.QTD + 'positions';
  baseURL = environment.QTD;
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['positions'] as Position[];
        })
      )
      );
  }

  getActiveAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/active')
      .pipe(
        map((res: any) => {
          return res['positions'] as Position[];
        })
      )
      );
  }

  getAllWithoutIncludes(){
    return firstValueFrom(this.http.get(this.baseUrl + `/withoutInclude`).pipe(
      map((res:any)=>{
        return res['result'] as Position[];
      })
    ));
  }

  getAllOrderBy(orderBy:'name'){
    return firstValueFrom(this.http.get(this.baseUrl + `/order/${orderBy}`).pipe(
      map((res:any)=>{
        return res['result'] as Position[];
      })
    ));
  }

  getStats(){
    return firstValueFrom(this.http.get(this.baseUrl + `/stats`).pipe(
      map((res:any)=>{
        return res['result'] as Position_StatsVM;
      })
    ))
  }

  GetPositions(){
    return firstValueFrom(this.http.get(this.baseUrl + '/include').pipe(
      map((res:any)=>{
        return res['result'] as PositionCompact[];
      })
    ));
  }

  GetPositionNumber(){
    return firstValueFrom(this.http.get(this.baseUrl + '/number').pipe(
      map((res:any)=>{
        return res['positions'] as any;
      })
    ));
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['position'] as Position;
        })
      )
      );
  }

  create(options: PositionCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res['position'] as Position;
        })
      )
      );
  }

  update(id: any, options: PositionUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['position'] as Position;
        })
      )
      );
  }

  copy(id: any, options: PositionCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/copy`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as Position;
        })
      )
      );
  }

  delete(options : PositionOption) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl +`/parent`, httpOptions)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  getTrainingPrograms(positionId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/trainingprograms/${positionId}`)
      .pipe(
        map((res: any) => {
          return res['trainingPrograms'] as TrainingProgram[];
        })
      )
      );
  }

  LinkTasks(id: any, options: Position_Task_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/task`, options)
      .pipe(
        map((res: any) => {
          return res as any;
        })
      )
      );
  }

  UnlinkTasks(posId: any, options: Position_Task_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${posId}/task/`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getLinkedTasks(id:any){

    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/task`).pipe(
      map((res:any)=>{
        return res['result'] as TaskWithCountR5R6Options[];
      })
    ));
  }

  LinkEnablingObjective(id: any, options: any)
   {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/enablingObjectives`, options)
      .pipe(
        map((res: any) => {
          return res['eoList'] as EnablingObjective[];
        })
      )
      );
  }
  getLinkedEOWithCount(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/enablingObjectives`)
      .pipe(
        map((res: any) => {
          return res['result'] as EOWithCountOptions[];
        })
      )
      );
  }
  UnlinkEnablingObjective(id: any, options: PositionOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/enablingObjectives`, httpOptions)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }


  getPositionsLinkedToTask(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/task/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as Position[];
      })
    ));
  }

  getNotLinkedWith(notlinkedWith: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/notlinked/${notlinkedWith}`)
      .pipe(
        map((res: any) => {
          return res['result'] as Position[];
        })
      )
      );
  }

  getActiveInactiveList(notlinkedWith: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/list/${notlinkedWith}`)
      .pipe(
        map((res: any) => {
          return res['result'] as any[];
        })
      )
      );
  }

  getEOLinkedWithPositions(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/enablingObjectives/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as Position[];
        })
      )
      );
  }

  //Employee with position start
  getLinkedEmployeeWithCount(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/emp`)
      .pipe(
        map((res: any) => {
          return res.result as EmployeePosition[];
        })
      )
      );
  }


  UnlinkEmployees(posId: any, options: Position_Employee_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${posId}/emp/`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }
  getEmployeeLinkedWithPositions(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/enablingObjectives/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as Position[];
        })
      )
      );
  }
  LinkEmployeesToPosition(id: any, options: any)
   {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/emp`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as Position;
        })
      )
      );
  }

  getStatusHistory(getLatest:boolean = false) {

    return firstValueFrom(this.http
      .get(this.baseUrl + `/history/latest/${getLatest}`)
      .pipe(
        map((res: any) => {
          return res['history'] as PositionLatestActivityVM[];
        })
    )
    );
  }

  getPositionEmployeeData(id: any)
  {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/enablingObjectives/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as Position[];
        })
      )
      );
  }

  getPositionsLinkedToTrainingGroup(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/task/${id}/traininggroup`).pipe(
      map((res:any)=>{
        return res['result'] as TrainingGroup[];
      })
    ));
  }

  getPositionTaskAsync(){
    return firstValueFrom(this.http
      .get(this.baseUrl + '/positionTask')
      .pipe(
        map((res: any) => {
          return res['positions'] as Position[];
        })
      )
      );
  }

  getPositionByNameAsync(positionName: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/positionByName/${positionName}`)
      .pipe(
        map((res: any) => {
          return res['position'] as Position;
        })
      )
      );
  }
}
