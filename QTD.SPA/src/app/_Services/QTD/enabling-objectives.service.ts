import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjectiveCreateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveCreateOptions';
import { EnablingObjectiveUpdateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveUpdateOptions';
import { EO_Procedure_LinkOptions } from 'src/app/_DtoModels/EnablingObjective_Procedure_Link/EO_Procedure_LinkOptions';
import { EO_SaftyHazard_LinkOptions } from 'src/app/_DtoModels/EnablingObjective_SaftyHazard_Link/EO_SaftyHazard_LinkOptions';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import { EnablingObjectiveOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveOption';
import { EnablingObjectiveHistory } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveHistory';
import { EO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective/EO_LinkOptions';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskWithCountOptions } from 'src/app/_DtoModels/Task/TaskWithCountOptions';
import { ProcedureWithLinkCount } from 'src/app/_DtoModels/Procedure/ProcedureWithLinkCount';
import { SafetyHazardWithLinkCount } from 'src/app/_DtoModels/SaftyHazard/SafetyHazardWithLinkCount';
import { RegulatoryRequirementWithLinkCount } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementWithLinkCount';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { EOLinkStats } from 'src/app/_DtoModels/EnablingObjective/EOLinkStats';
import { EOLatestActivityVM } from 'src/app/_DtoModels/EnablingObjective/EOLatestActivityVM';
import { LinkedEOVM } from 'src/app/_DtoModels/EnablingObjective/LinkedEOVM';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { EO_MetaEO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective_MetaEO_Link/EO_MetaEO_LinkOptions';
import { TestItemWithLinkCount } from 'src/app/_DtoModels/TestItem/TestItemWithLinkCount';
import { EOWithAllDataVM } from 'src/app/_DtoModels/EnablingObjective/EOWithAllDataVM';
import { TestItemOptions } from 'src/app/_DtoModels/TestItem/TestItemOptions';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { EnablingObjectiveHistoryCreateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveHistoryCreateOptions';
import { TaskPositionWithCount } from 'src/app/_DtoModels/Task/TaskPositionWithCount';
import { EmployeeWithCountOptions } from 'src/app/_DtoModels/Employee/EmployeeWithCountOptions';
import { EnablingObjective_StepCreateOptions } from 'src/app/_DtoModels/EnablingObjective_Step/EnablingObjective_StepCreateOptions';
import { EnablingObjective_Step } from 'src/app/_DtoModels/EnablingObjective_Step/EnablingObjective_Step';
import { EnablingObjective_StepUpdateOptions } from 'src/app/_DtoModels/EnablingObjective_Step/EnablingObjective_StepUpdateOptions';
import { EnablingObjective_QuestionCreateOptions } from 'src/app/_DtoModels/EnablingObjective_Question/EnablingObjective_QuestionCreateOptions';
import { EnablingObjective_Question } from 'src/app/_DtoModels/EnablingObjective_Question/EnablingObjective_Question';
import { EnablingObjective_SuggestionNumberOptions } from 'src/app/_DtoModels/EnablingObjective_Suggestion/EnablingObjective_SuggestionNumberOptions';
import { EnablingObjective_Suggestion } from 'src/app/_DtoModels/EnablingObjective_Suggestion/EnablingObjective_Suggestion';
import { EnablingObjective_SuggestionOptions } from 'src/app/_DtoModels/EnablingObjective_Suggestion/EnablingObjective_SuggestionOptions';
import { EnablingObjectiveSpecificUpdateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveSpecificUpdateOptions';
import { ToolAddOptions } from 'src/app/_DtoModels/Tool/ToolAddOptions';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { Version_EnablingObjective } from 'src/app/_DtoModels/Version_EnablingObjective/Version_EnablingObjective';
import { SQWithNumberVM } from 'src/app/components/qtd-views/implementation/task-requalification/flypanel-task-requal-filter/flypanel-task-requal-filter.component';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EOCatTreeVM } from 'src/app/_DtoModels/TreeVMs/EOTreeVM';
import { firstValueFrom } from 'rxjs';
import { PositionIdsModel } from '@models/Position/PositionIdsModel';

@Injectable({
  providedIn: 'root',
})
export class EnablingObjectivesService {
  baseUrl = environment.QTD + 'enablingObjectives';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['listEO'] as EnablingObjective[];
        })
      )
      );
  }

  getMinimizedForTree() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/tree`)
      .pipe(
        map((res: any) => {

          return res['result'] as EOCatTreeVM[];
        })
      )
      );
  }



  getAllSQs() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/sq')
      .pipe(
        map((res: any) => {

          return res['listEO'] as EnablingObjective[];
        })
      )
      );
  }

  getAllSQSOrderBy(order:'num'){
    return firstValueFrom(this.http.get(this.baseUrl + `/sq/${order}`).pipe(
      map(((res:any)=>{
        return res['result'] as EnablingObjective_Category[];
      }))
    ));
  }

  getAllEOs() {
    let urlEO = environment.QTD + "eos";
    return firstValueFrom(this.http
      .get(urlEO)
      .pipe(
        map((res: any) => {

          return res['listEO'] as EnablingObjective[];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res['eo'] as EnablingObjective;
        })
      )
      );
  }

  getWithCatSubCatAndtopic(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/others`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective;
      })
    ));
  }

  create(options: EnablingObjectiveCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {

          return res['eo'] as EnablingObjective;
        })
      )
      );
  }

  saveEOHistory(options:EnablingObjectiveHistory){
    return firstValueFrom(this.http.post(this.baseUrl + '/history',options).pipe(
      map((res:any)=>{
        return res["result"] as EnablingObjectiveHistory;
      })
    ));
  }

  update(id: any, options: EnablingObjectiveCreateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {

          return res['eo'] as EnablingObjective;
        })
      )
      );
  }

  delete(id: any, options: EnablingObjectiveOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`, httpOptions)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }

  linkProcedure(options: EO_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${options.eoId}/procedures`, options)
      .pipe(
        map((res: any) => {
          return res['message'] as string;
        })
      )
      );
  }

  unlinkProcedure(
    options: EO_LinkOptions
  ) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(
        this.baseUrl + `/${options.eoId}/procedures/`,
        httpOptions
      )
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  getLinkedProceduresWithCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/procedures/count`).pipe(
      map((res:any)=>{
        return res['result'] as ProcedureWithLinkCount[];
      })
    ));
  }

  getLinkedProceduresWithMetaEOCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/procedures/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as ProcedureWithLinkCount[];
      })
    ));
  }

  linkSafetyHazard(options:EO_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${options.eoId}/saftyHazards`, options)
      .pipe(
        map((res: any) => {

          return res['message'] as string;
        })
      )
      );
  }

  unlinkSafetyHazard(
    options:EO_LinkOptions
  ) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(
        this.baseUrl + `/${options.eoId}/saftyHazards`, httpOptions
      )
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  getLinkedSHWithCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/saftyHazards/count`).pipe(
      map((res:any)=>{
        return res['result'] as SafetyHazardWithLinkCount[];
      })
    ));
  }

  getLinkedSHWithMetaEOCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/saftyHazards/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as SafetyHazardWithLinkCount[];
      })
    ));
  }

  getEONumberWithTopic(selectedCatId: any, selectedSubCatId : any, selectedTopicId: any) {
    let urlEO = environment.QTD + `eos/number/${selectedCatId}/${selectedSubCatId}/${selectedTopicId}`;
    return firstValueFrom(this.http
      .get(urlEO)
      .pipe(
        map((res: any) => {

          return res['listEO'] as EnablingObjective[];
        })
      )
      );
  }

  getEONumber(selectedCatId: any, selectedSubCatId : any) {
    let urlEO = environment.QTD + `eos/number/${selectedCatId}/${selectedSubCatId}`;
    return firstValueFrom(this.http
      .get(urlEO)
      .pipe(
        map((res: any) => {

          return res['listEO'] as EnablingObjective[];
        })
      )
      );
  }

  linkTask(options: EO_LinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${options.eoId}/task`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  unlinktasks(options : EO_LinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${options.eoId}/task`, { body : options }).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getLinkedTasks(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/task/count`).pipe(
      map((res:any)=>{
        return res['result'] as TaskWithCountOptions[];
      })
    ));
  }

  getLinkedTasksToMetaEO(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/task/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as TaskWithCountOptions[];
      })
    ));
  }

  linkRR(options: EO_LinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${options.eoId}/rr`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  unlinkRR(options : EO_LinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${options.eoId}/rr`, { body : options }).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getLinkedRRWithCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/rr`).pipe(
      map((res:any)=>{
        return res['result'] as RegulatoryRequirementWithLinkCount[];
      })
    ));
  }

  getLinkedRRWithMetaEOCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/rr/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as RegulatoryRequirementWithLinkCount[];
      })
    ));
  }

  linkILAs(options: EO_LinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${options.eoId}/ila`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  unlinkILAs(options : EO_LinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${options.eoId}/ila`, { body : options }).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getLinkedILAWithCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/ila/count`).pipe(
      map((res:any)=>{
        return res['result'] as ILAWithCountOptions[];
      })
    ));
  }

  getLinkedILAWithMetaEOCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/ila/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as ILAWithCountOptions[];
      })
    ));
  }

  copy(id:any,options:EnablingObjectiveCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/copy`,options).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }

  getStats(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/linked`).pipe(
      map((res:any)=>{
        return res['result'] as EOLinkStats;
      })
    ));
  }

  getMetaEOStats(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/meta/linked`).pipe(
      map((res:any)=>{
        return res['result'] as EOLinkStats;
      })
    ));
  }

  getNotLinkedStats(){
    return firstValueFrom(this.http.get(this.baseUrl + `/notlinked`).pipe(
      map((res:any)=>{
        return res['result'] as EOLinkStats;
      })
    ));
  }

  getLatestHistory(trim:boolean = false){
    return firstValueFrom(this.http.get(this.baseUrl + `/history/latest/${trim}`).pipe(
      map((res:any)=>{
        return res['result'] as EOLatestActivityVM[];
      })
    ));
  }

  getLatestHistoryStats(id : any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/history/latest`).pipe(
      map((res:any)=>{
        return res['result'] as EOLatestActivityVM[];
      })
    ));
  }

  getLinkedEOS(options : LinkedEOVM){
    return firstValueFrom(this.http.get(this.baseUrl + `/${options.id}/linked/${options.type}`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective[];
      })
    ));
  }

  getLinkedIds(name:string){
    return firstValueFrom(this.http.get(this.baseUrl + `/linkedIds/${name}`).pipe(
      map((res:any)=>{
        return res['result'] as number[]
      })
    ));
  }

  getEOsACtiveInactive(name:string){
    return firstValueFrom(this.http.get(this.baseUrl + `/list/${name}`).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }

  getTestItemsWithCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/test/count`).pipe(
      map((res:any)=>{
        return res['result'] as TestItemWithLinkCount[];
      })
    ));
  }

  getTestItemsWithMetaEOCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/test/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as TestItemWithLinkCount[];
      })
    ));
  }

  checkisMeta(id : any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/isMeta`).pipe(
      map((res:any)=>{
        return res['result'] as boolean;
      })
    ));
  }

  unlinkTests(id:any,options : TestItemOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/test`,{ body : options }).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getEOLinkedToMetaEO(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/meta`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective[];
      })
    ));
  }

  linkMetaEOtoEOS(options : EO_MetaEO_LinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${options.metaEOId}/meta`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  unlinkEOsFromMetaEO(options : EO_MetaEO_LinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${options.metaEOId}/meta`,{ body:options }).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  reorderMetaEoLinks(options : EO_MetaEO_LinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${options.metaEOId}/meta/order`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getAllEOData(id : any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/all`).pipe(
      map((res:any)=>{
        return res['result'] as EOWithAllDataVM;
      })
    ));
  }

  getTestsTestItemIsLinkedTo(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/test/${id}/linkedtest`).pipe(
      map((res:any)=>{
        return res['result'] as Test[];
      })
    ));
  }

  createHistory(options: EnablingObjectiveHistoryCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/history`,options).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }

  getStepNumber(id: any) {

    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/steps/num`)
      .pipe(
        map((res: any) => {
          return res['result'] as number;
        })
      )
      );
  }

  createSteps(id: any, options: EnablingObjective_StepCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/steps`, options)
      .pipe(
        map((res: any) => {
          return res['step'] as EnablingObjective_Step;
        })
      )
      );
  }

  getSteps(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/steps`)
      .pipe(
        map((res: any) => {
          return res['steps'] as EnablingObjective_Step[];
        })
      )
      );
  }

  updateSteps(eoId: any, stepId: any, options: EnablingObjective_StepUpdateOptions) {

    return firstValueFrom(this.http
      .put(this.baseUrl + `/${eoId}/steps/${stepId}`, options)
      .pipe(
        map((res: any) => {
          return res['step'] as EnablingObjective_Step;
        })
      )
      );
  }

  deleteSteps(eoId: any, stepId: any, options: EnablingObjectiveOptions) {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${eoId}/steps/${stepId}`, httpOptions)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  linkPositions(options : EO_LinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${options.eoId}/pos`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  unlinkPositions(options : EO_LinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${options.eoId}/pos`,{ body : options}).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getlinkedPositions(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/pos`).pipe(
      map((res:any)=>{
        return res['result'] as TaskPositionWithCount[];
      })
    ));
  }

  linkEmployees(options : EO_LinkOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/${options.eoId}/emp`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  unlinkEmployees(options : EO_LinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${options.eoId}/emp`,{ body : options}).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getLinkedEmployees(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/emp`).pipe(
      map((res:any)=>{
        return res['result'] as EmployeeWithCountOptions[];
      })
    ));
  }

  getEOQuestionNumber(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/questions/num`)
      .pipe(
        map((res: any) => {
          return res['result'] as number;
        })
      )
      );
  }

  addQuestion(id: any, options: EnablingObjective_QuestionCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/questions`, options)
      .pipe(
        map((res: any) => {
          return res['question'] as EnablingObjective_Question;
        })
      )
      );
  }

  removeQuestion(eoId: any, questionid: any, options: EnablingObjectiveOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${eoId}/questions/${questionid}`, httpOptions)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  updateQuestion(id: any, quesId: any, options: EnablingObjective_QuestionCreateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/questions/${quesId}`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getQuestions(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/questions`)
      .pipe(
        map((res: any) => {
          return res['questions'] as EnablingObjective_Question[];
        })
      )
      );
  }

  updateSuggestionNumbers(options:EnablingObjective_SuggestionNumberOptions){
    return firstValueFrom(this.http
      .put(this.baseUrl + `/suggestion`,options)
      .pipe(
        map((res:any)=>{
          return res['message'];
        })
      ));
  }

  getSuggestions(eoId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${eoId}/suggestion`)
      .pipe(
        map((res: any) => {
          return res['result'] as EnablingObjective_Suggestion[];
        })
      )
      );
  }

  createSuggestion(id: any, options: EnablingObjective_SuggestionOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/suggestion`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as EnablingObjective_Suggestion;
        })
      )
      );
  }

  getSuggestionNumber(eoId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${eoId}/suggestion/number`)
      .pipe(
        map((res: any) => {
          return res['result'] as number;
        })
      )
      );
  }

  deleteSuggestion(id: any, suggestionId: any, options: EnablingObjectiveOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/suggestion/${suggestionId}`, {
        body: options,
      })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  updateSuggestion(id: any, suggestionId: any, options: EnablingObjective_SuggestionOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/suggestion/${suggestionId}`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  updateSpecific(id: any, options: EnablingObjectiveSpecificUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/specific`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  //Link Tools
  LinkTool(id: any, options: ToolAddOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/tools`, options)
      .pipe(
        map((res: any) => {
          return res['tools'] as Tool[];
        })
      )
      );
  }

  UnlinkTool(id: any, toolName: string, options: EnablingObjectiveOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/tools/${toolName}`, httpOptions)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  UnlinkMultipleTools(id: any, options: ToolAddOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/tools`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getTools(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/tools`)
      .pipe(
        map((res: any) => {
          return res['result'] as Tool[];
        })
      )
      );
  }

  updateTools(id: any, options: EnablingObjectiveOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/tools`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }
  getVersions(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/version`).pipe(
      map((res:any)=>{
        return res['result'] as Version_EnablingObjective[];
      })
    ));
  }

  getAllSQS(){
    return firstValueFrom(this.http.get(this.baseUrl + `/sqs`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective[];
      })
    ));
  }

  createFromILA(options:EnablingObjectiveCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/ila`,options).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective;
      })
    ));
  }

  getSqWithNumber(options:SQWithNumberVM){
    return firstValueFrom(this.http.post(this.baseUrl + `/subCat/topic/number`,options).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective[];
      })
    ));
  }

  CheckDeactivePossible(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/canDeactivate`).pipe(
      map((res:any)=>{
        return res['result'] as boolean;
      })
    ));
  }

  getSQsByPositionIds(option:PositionIdsModel) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/sqByPositions`, option)
      .pipe(
        map((res: any) => {
 
          return res['listEO'] as EnablingObjective[];
        })
      )
      );
  }
}
