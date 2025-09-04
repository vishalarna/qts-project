import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ILA_EnablingObjective_Link } from 'src/app/_DtoModels/ILA_EnablingObjective_Link/ILA_EnablingObjective_Link';
import { RetakeStatusesVM } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RetakeStatusesVM';
import { RosterTestVM } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RosterTestVM';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { TestCreateOptions } from 'src/app/_DtoModels/Test/TestCreateOptions';
import { TestOptions } from 'src/app/_DtoModels/Test/TestOptions';
import { TestStatsVM } from 'src/app/_DtoModels/Test/TestStatsVM';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCopyOptions } from 'src/app/_DtoModels/TestItem/TestItemCopyOptions';
import { TestItemWithLinkCount } from 'src/app/_DtoModels/TestItem/TestItemWithLinkCount';
import { TestItemFilter } from 'src/app/_DtoModels/TestItemLink/TestItemFilter';
import { TestItemLink } from 'src/app/_DtoModels/TestItemLink/TestItemLink';
import { TestItemLinkCreateOptions } from 'src/app/_DtoModels/TestItemLink/TestItemLinkCreateOptions';
import { TestItemLinkeOptions } from 'src/app/_DtoModels/TestItemLink/TestItemLinkOptions';
import { Test_TestItem_LinkOptions } from 'src/app/_DtoModels/Test_TestItem_Link/Test_TestItem_LinkOptions';
import { ReorderTestItemOptions } from 'src/app/_DtoModels/TestItem/ReorderTestItemOptions';
import { environment } from 'src/environments/environment';
import { ILAWithTestVM } from 'src/app/_DtoModels/TreeVMs/ILAWithTestVM';
import { TestWithCountOptions } from 'src/app/_DtoModels/Test/TestWithCountOptions';
import { TestItemVM } from '@models/TestItem/TestItemVM';
import { TestDataVM } from '@models/Test/TestDataVM';
import { TestItemLinkVM } from '@models/TestItemLink/TestItemLinkVM';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TestsService {
  baseUrl = environment.QTD + 'test';
  constructor(private http: HttpClient) { }

  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {
        return res['result'] as Test[];
      })
    ));
  }

  get(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res: any) => {
        return res['result'] as Test;
      })
    ));
  }

  checkRandom(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/random`).pipe(
      map((res: any) => {
        return res['result'] as boolean;
      })
    ));
  }

  create(options: TestCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl, options).pipe(
      map((res: any) => {
        return res['result'] as Test;
      })
    ));
  }

  update(id: any, options: TestCreateOptions) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`, options).pipe(
      map((res: any) => {
        return res['result'] as Test;
      })
    ));
  }

  delete(options: TestOptions) {
    return firstValueFrom(this.http.delete(this.baseUrl, { body: options }).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
  }


  createTestItemLink(id: any, options: TestItemLinkCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/testItem`, options).pipe(
      map((res: any) => {
        return res['result'] as TestItemLink;
      })
    ));
  }

  getTestLinkedtoILA(ilaId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/ila/${ilaId}`).pipe(
      map((res: any) => {
        return res['result'] as TestWithCountOptions[];
      })
    ));
  }

  getAllTestLinkedtoILA() {
    return firstValueFrom(this.http.get(this.baseUrl + `/ila`).pipe(
      map((res: any) => {
        return res['result'] as any[];
      })
    ))
  }

  getMinimalDataForILAWithTest() {
    return firstValueFrom(this.http.get(this.baseUrl + `/ila/tree`).pipe(
      map((res: any) => {
        return res['result'] as ILAWithTestVM[];
      })
    ))
  }

  getStats() {
    return firstValueFrom(this.http.get(this.baseUrl + `/stats`).pipe(
      map((res: any) => {
        return res['result'] as TestStatsVM;
      })
    ));
  }

  LinkTestItem(id: any, options: Test_TestItem_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/testItem`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as Test;
        })
      )
      );
  }

  UnlinkAllTestItems(testId:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${testId}/testItem/all`).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }



  GetTestItem(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/testItem`)
      .pipe(
        map((res: any) => {
          return res['result'] as TestItemLinkVM[];
        })
      )
      );
  }

  filterTestItems(option: TestItemFilter) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/testItem/filter`, option)
      .pipe(
        map((res: any) => {
          return res['result'] as TestItem[];
        })
      ));
  }

  getLinkedEOs(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/testItem/eo/${id}`).pipe(
      map((res: any) => {
        return res['result'] as any[];
      })
    ));
  }

  UpdateTestItemSequence(id: any, options: Test_TestItem_LinkOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/sequence/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as Test;
        })
      )
      );
  }




  GetTestItemLinkedToTest(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/testItem/all`).pipe(
      map((res: any) => {
        return res['result'] as TestItem[];
      })
    ));
  }

  GetTestItemVMLinkedToTest(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/testItemVM/all`).pipe(
      map((res: any) => {
        return res['result'] as TestItemVM[];
      })
    ));
  }

  getTestItemsForCopyMode(options:TestItemCopyOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/testItem/copy`,options).pipe(
      map((res:any)=>{
        return res['result'] as TestItem[];
      })
    ));
  }

  unlinkTestItem(options: TestItemLinkeOptions) {
    return firstValueFrom(this.http.delete(this.baseUrl + `/${options.testId}/testItem/`, { body: options }).pipe(
      map((res: any) => {
        return res['message'] as string;
      })
    ));
  }

  getUnlinkedQuestions(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/unlinked/${id}`).pipe(
      map((res: any) => {
        return res['result'] as TestItem[];
      })
    ));
  }

  getEOsLinkedToILA(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/eo/${id}`).pipe(
      map((res: any) => {
        return res['result'] as any[];
      })
    ));
  }


  getTestDataByTestId(testId: any) {

    return firstValueFrom(this.http.get(this.baseUrl + `/${testId}/testItem/all`).pipe(
      map((res: any) => {

        return res['result'] as any[];
      })
    ));
  }

  getSpecificTests(classScheduleId: any, type: string) {
    return firstValueFrom(this.http.get(this.baseUrl + `/ila/${classScheduleId}/${type}`).pipe(
      map((res: any) => {
        return res['result'] as RosterTestVM[];
      })
    ));
  }

  getTestItemLinked(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/testItem`).pipe(
      map((res:any)=>{
        return res['result'] as TestItemLink;
      })
    ));
  }

  ReadyRetakeStatusData(empId:any,classId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/emp/${empId}/class/${classId}/statuses`).pipe(
      map((res:any)=>{
        return res['result'] as RetakeStatusesVM[];
      })
    ));
  }

  getLinkedEOForILAInTest(testId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${testId}/EOs`).pipe(
      map((res:any)=>{
        return res['result'] as ILA_EnablingObjective_Link[];
      })
    ));
  }

  getTestList(option:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${option}/list`).pipe(
      map((res: any) => {
        return res['result'] as any;
      })
    ));
  }

  ReorderItems(testId:any,reorderOptions:ReorderTestItemOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${testId}/reorder`,reorderOptions).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getAllTestsByTypeAsync(testType:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${testType}/testsList`).pipe(
      map((res: any) => {
        return res['result'] as TestDataVM[];
      })
    ));
  }
}
