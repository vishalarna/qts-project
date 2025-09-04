import { TestItemFillBlankCreateOptions } from './../../_DtoModels/TestItemFillBlank/TestItemFillBlankCreateOptions';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemMatch } from 'src/app/_DtoModels/TestItemMatch/TestItemMatch';
import { TestItemMatchCreateOPtions } from 'src/app/_DtoModels/TestItemMatch/TestItemMatchCreateOptions';
import { TestItemMcq } from 'src/app/_DtoModels/TestItemMcq/TestItemMcq';
import { TestItemMcqCreateOptions } from 'src/app/_DtoModels/TestItemMcq/TestItemMcqCreateOptions';
import { TestItemShortAnswer } from 'src/app/_DtoModels/TestItemShortAnswer/TestItemShortAnswer';
import { TestItemShortAnswerCreateOptions } from 'src/app/_DtoModels/TestItemShortAnswer/TestItemShortAnswerCreateOptions';
import { TestItemTrueFalse } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalse';
import { TestItemTrueFalseCreateOptions } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalseCreateOptions';
import { environment } from 'src/environments/environment';
import { TestItemFillBlank } from 'src/app/_DtoModels/TestItemFillBlank/TestItemFillBlank';
import { TestItem_HistoryCreateOptions } from 'src/app/_DtoModels/TestItem_History/TestItem_HistoryCreateOptions';
import { TestItem_History } from 'src/app/_DtoModels/TestItem_History/TestItem_History';
import { TestItemStatsVM } from 'src/app/_DtoModels/TestItem/TestItemStatsVM';
import { TestItemChangeOptions } from 'src/app/_DtoModels/TestItem/TestItemChangeOptions';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { TestItemByEoOptions } from 'src/app/_DtoModels/TestItem/TestItemByEoOptions';
import { TestItemByEoVM } from '@models/TestItem/TestItemByEoVM';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TestItemService {
  baseUrl = environment.QTD + 'testitem';
  constructor(private http: HttpClient) { }

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {
        return res['result'] as TestItem[];
      })
    ));
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as TestItem;
      })
    ));
  }

  create(options:TestItemCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['result'] as TestItem;
      })
    ));
  }

  update(id:any,options:TestItemCreateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res['result'] as TestItem;
      })
    ));
  }

  delete(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  changeStatus(id:any,options:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`, { body:options }).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  updateDescription(id:any,options:TestItemCreateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/description/${id}`,options).pipe(
      map((res:any)=>{
        return res['result'] as TestItem;
      })
    ));
  }

  createMcq(options:TestItemMcqCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/mcq`,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  getMCQ(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/mcq/${id}/byItem`).pipe(
      map((res:any)=>{
        return res['result'] as TestItemMcq[];
      })
    ));
  }

  removeMCQ(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/mcq/byItemId/${id}`).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  createMatchTheColumn(options:TestItemMatchCreateOPtions){
    return firstValueFrom(this.http.post(this.baseUrl + `/match`,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  getMatchTheColumn(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/match/byItemId/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as TestItemMatch[];
      })
    ));
  }

  deleteMatchColumnItems(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/match/byItemId/${id}`).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  createTrueFalse(options:TestItemTrueFalseCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/trueFalse`,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  getTrueFalse(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/trueFalse/${id}/byItem`).pipe(
      map((res:any)=>{
        return res['result'] as TestItemTrueFalse[];
      })
    ));
  }

  removeTrueFalseItems(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/trueFalse/byItemId/${id}`).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  createShortAnswer(options:TestItemShortAnswerCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/shortanswer`,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  getShortAnswers(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/shortanswer/${id}/byItem`).pipe(
      map((res:any)=>{
        return res['result'] as TestItemShortAnswer[];
      })
    ));
  }

  removeSA(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/shortanswer/byItemId/${id}`).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  createFillInBlank(options:TestItemFillBlankCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/fillBlank`,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  getFillInBlank(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/fillBlank/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as TestItemFillBlank[];
      })
    ));
  }

  getFillInBlankByTestItem(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/fillBlank/byItemId/${id}`).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  removeFillInBlank(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/fillBlank/byItemId/${id}`).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  createHistory(options : TestItem_HistoryCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/history`,options).pipe(
      map((res:any)=>{
        return res['result'] as TestItem_History;
      })
    ));
  }

  getAllWithFilterOption(option:string,id:any){

    
    return firstValueFrom(this.http.get(this.baseUrl + "/filter/" + option + (id===null?'':("/" + id))).pipe(
      map((res:any)=>{
        return res['result'] as TestItem[];
      })
    ));
  }



  getStats(){
    return firstValueFrom(this.http.get(this.baseUrl + '/stats').pipe(
      map((res:any)=>{
        return res['result'] as TestItemStatsVM;
      })
    ));
  }

  getTestItemList(option:string){
    return firstValueFrom(this.http.get(this.baseUrl + `/${option}/list`).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }

  changeEO(id:any,options:TestItemChangeOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/changeEO`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getItemsByEO(eoId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/eos/${eoId}`).pipe(
      map((res: any) => {
        return res['result'] as TestItem[];
      })
    ));
  }

  getItemsForEOs(options:TestItemByEoOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/eos/multiple`,options).pipe(
      map((res:any)=>{
        return res['result'] as TestItemByEoVM[];
      })
    ));
  }

  getItemWithData(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/individual`).pipe(
      map((res:any)=>{
        return res['result'] as TestItem;
      })
    ));
  }

  getTestItemNumber(){
    return firstValueFrom(this.http.get(this.baseUrl + `/number`).pipe(
      map((res:any)=>{
        return res['result'] as number;
      })
    ));
  }
}
