import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { DutyAreaCreateOptions } from 'src/app/_DtoModels/DutyArea/DutyAreaCreateOption';
import { SubdutyAreaCreateOptions } from 'src/app/_DtoModels/SubdutyArea/SubdutyAreaCreateOptions';
import { SubdutyArea } from 'src/app/_DtoModels/SubdutyArea/SubdutyArea';
import { DutyAreaOptions } from 'src/app/_DtoModels/DutyArea/DutyAreaOptions';
import { DutyAreaUpdateOptions } from 'src/app/_DtoModels/DutyArea/DutyAreaUpdateOptions';
import { SubdutyAreaOptions } from 'src/app/_DtoModels/SubdutyArea/SubdutyAreaOptions';
import { SubdutyAreaUpdateOptions } from 'src/app/_DtoModels/SubdutyArea/SubdutyAreaUpdateOptions';
import { DutyAreaTreeVM } from 'src/app/_DtoModels/TreeVMs/TaskTreeVM';
import { PositionIdsModel } from '@models/Position/PositionIdsModel';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class DutyAreaService {
  baseUrl = environment.QTD + 'dutyAreas';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['listDA'] as DutyArea[];
        })
      )
      );
  }

  /// Add to the switch case on backend
  getAllOrderBy(order:'num'){
    return firstValueFrom(this.http.get(this.baseUrl + `/order/${order}`).pipe(
      map((res:any)=>{
        return res['result'] as DutyArea[];
      })
    ));
  }

  getWithSubdutyAreas() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/subDutyAreas')
      .pipe(
        map((res: any) => {
          return res['listDA'] as DutyArea[];
        })
      )
      );
  }

  getMinimizedDataForTree() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/subDutyAreas/tree')
      .pipe(
        map((res: any) => {
          return res['result'] as DutyAreaTreeVM[];
        })
      )
      );
  }

  getSubDutyAreasByDutyArea(dutyAreaId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${dutyAreaId}/subDutyAreas`)
      .pipe(
        map((res: any) => {
          return res['sda'] as SubdutyArea[];
        })
      )
      );
  }

  getSubDutyArea(Id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/subDutyArea/${Id}`)
      .pipe(
        map((res: any) => {
          return res['sda'] as SubdutyArea;
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['da'] as DutyArea;
        })
      )
      );
  }

  create(option: DutyAreaCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {
          return res['da'] as DutyArea;
        })
      )
      );
  }

  update(id: any, option: DutyAreaUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, option)
      .pipe(
        map((res: any) => {
          return res['da'] as DutyArea;
        })
      )
      );
  }

  updateSubDutyArea(id: any, option: SubdutyAreaUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/subdutyAreas`, option)
      .pipe(
        map((res: any) => {
          return res['sda'] as SubdutyArea;
        })
      )
      );
  }

  addSubDutyArea(id: any, option: SubdutyAreaCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/subdutyAreas`, option)
      .pipe(
        map((res: any) => {
          return res['sda'] as SubdutyArea;
        })
      )
      );
  }

  getDANumber(letter: string) {
    let encodedLetter = encodeURIComponent(letter);
    return firstValueFrom(this.http
      .get(this.baseUrl + `/number/${encodedLetter}`)
      .pipe(
        map((res: any) => {
          return res['da'] as number;
        })
      )
      );
  }

  getSDANumber(dutyAreaId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${dutyAreaId}/subdutyAreas/number`)
      .pipe(
        map((res: any) => {
          return res['number'] as number;
        })
      )
      );
  }

  changeStatus(id: any, options: DutyAreaOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  checkDAForTaskLinks(id:any){
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/links`)
      .pipe(
        map((res:any)=>{
          return res['result'] as boolean;
        })
      ));
  }

  checkSDAForTaskLinks(id:any){
    return firstValueFrom(this.http
      .get(this.baseUrl + `/subdutyArea/${id}/links`)
      .pipe(
        map((res:any)=>{
          return res['result'] as boolean;
        })
      ));
  }

  changeSubdutyAreaStatus(id: any, options: SubdutyAreaOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/subdutyAreas`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getAllSubDutyAreas(){
    return firstValueFrom(this.http.get(this.baseUrl + `/subdutyAreas/all`).pipe(
      map((res:any)=>{
        return res['result'] as SubdutyArea[]
      })
    ));
  }

  getSdasWithNumByDAID(daId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${daId}/sdas`).pipe(
      map((res:any)=>{
        return res['result'] as SubdutyArea[];
      })
    ));
  }

  getTaskTreeDataByPositionAsync(option:PositionIdsModel) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/subDutyAreas/taskTreeByPositions`,option)
      .pipe(
        map((res: any) => {
          return res['result'] as DutyAreaTreeVM[]; 
        })
      )
      );
  }
}
