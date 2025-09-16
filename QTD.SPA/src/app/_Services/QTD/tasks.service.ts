import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskCreateOptions } from 'src/app/_DtoModels/Task/TaskCreateOptions';
import { TaskUpdateOptions } from 'src/app/_DtoModels/Task/TaskUpdateOptions';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { Task_Step } from 'src/app/_DtoModels/Task_Step/Task_Step';
import { Task_StepCreateOptions } from 'src/app/_DtoModels/Task_Step/Task_StepCreateOptions';
import { Task_StepUpdateOptions } from 'src/app/_DtoModels/Task_Step/Task_StepUpdateOptions';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { LinkSaftyHazardOptions } from 'src/app/_DtoModels/Task/LinkSaftyHazardOptions';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { ToolAddOptions } from 'src/app/_DtoModels/Tool/ToolAddOptions';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { QuestionCreateOptions } from 'src/app/_DtoModels/Task_Question/QuestionCreateOptions';
import { Task_Question } from 'src/app/_DtoModels/Task_Question/Task_Question';
import { EOWithCountOptions } from 'src/app/_DtoModels/EnablingObjective/EOWithCountOptions';
import { TaskPositionWithCount } from 'src/app/_DtoModels/Task/TaskPositionWithCount';
import { ProcedureWithLinkCount } from 'src/app/_DtoModels/Procedure/ProcedureWithLinkCount';
import { SafetyHazardWithLinkCount } from 'src/app/_DtoModels/SaftyHazard/SafetyHazardWithLinkCount';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { TaskRRWithCount } from 'src/app/_DtoModels/Task/TaskRRWithCount';
import { Task_RR_LinkOptions } from 'src/app/_DtoModels/Task_RR_Link/Task_RR_LinkOptions';
import { TaskStatsCount } from 'src/app/_DtoModels/Task/TaskStatsCount';

import { TaskRequalificationOptions } from 'src/app/_DtoModels/Task/TaskRequalificationOptions';
import { TaskLatestActivityVM } from 'src/app/_DtoModels/Task/TaskLatestActivity';
import { TaskSpecificUpdateOptions } from 'src/app/_DtoModels/Task/TaskSpecificUpdateOptions';
import { Task_Suggestion } from 'src/app/_DtoModels/Task_Suggestion/Task_Suggestion';
import { TaskSuggestionOptions } from 'src/app/_DtoModels/Task_Suggestion/Task_SuggestionOptions';
import { Task_StepNumberOptions } from 'src/app/_DtoModels/Task_Step/Task_StepNumberOptions';
import { EmployeeTaskVM } from 'src/app/_DtoModels/Task/EmployeeTaskVM';
import { TrainingGroup } from 'src/app/_DtoModels/TrainingGroup/TrainingGroup';
import { Task_TrainingGroupOptions } from 'src/app/_DtoModels/Task_TrainingGroup/Task_TrainingGroupOptions';
import { TaskNumberVM } from 'src/app/_DtoModels/Task/TaskNumberVM';
import { TaskMetaLinkVM } from 'src/app/_DtoModels/Task/TaskMetaLinkVM';
import { TaskCollaboratorInvitationOptions } from 'src/app/_DtoModels/Task_CollaboratorInvitaion/Task_CollaboratorInvitationOptions';
import { TaskWithNumberVM } from 'src/app/_DtoModels/Task/TaskWithNumberVM';
import { Task_CollaboratorInvitaiton } from 'src/app/_DtoModels/Task_CollaboratorInvitaion/Task_CollaboratorInvitaiton';
import { TaskVersionVM } from 'src/app/_DtoModels/Task/TaskVersionVM';
import { Task_QuestionNumberOptions } from 'src/app/_DtoModels/Task_Question/Task_QuestionNumberOptions';
import { Task_SuggestionNumberOptions } from 'src/app/_DtoModels/Task_Suggestion/Task_SuggestionNumberOptions';
import { Version_Task } from 'src/app/_DtoModels/Task/Version_Task';
import { GetTaskWithAllLinkData } from 'src/app/_DtoModels/Task/GetTaskWithAllLinkData';
import { MetaTask_OJTVM, MetaTask_QuestionsVM, MetaTask_SuggestionsVM } from 'src/app/_DtoModels/Task/MetaTask_OJTVM';
import { Version_TaskUpdateOptions } from 'src/app/_DtoModels/Version_Task/Version_TaskUpdateOptions';
import { TaskReQualificationCreateOption, TaskReQualificationQuestionsCreateOption, TaskReQualificationSignOffOption, TaskReQualificationStepsCreateOption } from 'src/app/_DtoModels/taskRequalfication/TaskReQualificationCreateOption';
import { MetaTaskOJTVM } from 'src/app/_DtoModels/Task/MetaTaskOJTVM';
import { TaskRequalVM } from 'src/app/_DtoModels/Task/TaskRequalVM';
import { TaskQualificationPengingEvaluatorVM } from '@models/TaskQualification/TaskQualificationPengingEvaluatorVM';
import { TaskWithPositionCompactVM } from '@models/Task/TaskWithPositionCompactVM';
import { ReportExportOptions } from '@models/Report/ReportExportOptions';
import { firstValueFrom } from 'rxjs';
import { VersionTaskModel } from '@models/Version_Task/VersionTaskModel';
import { TaskCopyOptions } from '@models/Task/TaskCopyOptions';

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  baseUrl = environment.QTD + 'tasks';
  baseUrl1 = environment.QTD;

  constructor(private http: HttpClient) { }

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['tasks'] as Task[];
        })
      )
      );
  }

  getAllData(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/all`)
      .pipe(
        map((res: any) => {
          return res['result'] as Task;
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['task'] as Task;
        })
      )
      );
  }

  create(options: TaskCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res['task'] as Task;
        })
      )
      );
  }

  createCopy(id: any, options: TaskCopyOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/copy`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as Task;
        })
      ));
  }

  update(id: any, options: TaskUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['task'] as Task;
        })
      )
      );
  }


  updateTaskRequalificationInfo(id: any, options: TaskRequalificationOptions) {
    return firstValueFrom(this.http
      .put(environment.QTD + `versionTask/requalification/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['versionTask'] as Version_Task;
        })
      )
      );
  }

  updateSpecific(id: any, options: TaskSpecificUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/specific`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  delete(options: TaskOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl, httpOptions)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  //Task Steps

  getSteps(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/steps`)
      .pipe(
        map((res: any) => {
          return res['steps'] as Task_Step[];
        })
      )
      );
  }

  createSteps(id: any, options: Task_StepCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/steps`, options)
      .pipe(
        map((res: any) => {
          return res['step'] as Task_Step;
        })
      )
      );
  }

  updateSteps(id: any, stepId: any, options: Task_StepUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/steps/${stepId}`, options)
      .pipe(
        map((res: any) => {
          return res['step'] as Task_Step;
        })
      )
      );
  }

  deleteSteps(id: any, stepId: any, options: TaskOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/steps/${stepId}`, httpOptions)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
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

  UpdateStepNumbers(options: Task_StepNumberOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/steps`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  updateQANumbers(options: Task_QuestionNumberOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/questions`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      ));
  }

  updateSuggestionNumbers(options: Task_SuggestionNumberOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/suggestion`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      ));
  }

  getSuggestions(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/suggestion`)
      .pipe(
        map((res: any) => {
          return res['result'] as Task_Suggestion[];
        })
      )
      );
  }

  createSuggestion(id: any, options: TaskSuggestionOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/suggestion`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as Task_Suggestion;
        })
      )
      );
  }

  getSuggestionNumber(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/suggestion/number`)
      .pipe(
        map((res: any) => {
          return res['result'] as number;
        })
      )
      );
  }

  deleteSuggestion(id: any, suggestionId: any, options: TaskOptions) {
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

  updateSuggestion(id: any, suggestionId: any, options: TaskSuggestionOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/suggestion/${suggestionId}`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  //Linked Enabling Objectives
  getEnablingObjectives(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/enablingObjectives`)
      .pipe(
        map((res: any) => {
          return res['tasksEO'] as EnablingObjective[];
        })
      )
      );
  }

  getLinkedEOWithCount(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/enablingObjectives/count`)
      .pipe(
        map((res: any) => {
          return res['result'] as EOWithCountOptions[];
        })
      )
      );
  }

  getLinkedTaskWithMetaEOCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/enablingObjectives/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as EOWithCountOptions[];
      })
    ));
  }

  LinkEnablingObjective(id: any, options: TaskOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/enablingObjectives`, options)
      .pipe(
        map((res: any) => {
          return res['eoList'] as EnablingObjective[];
        })
      )
      );
  }

  UnlinkEnablingObjective(id: any, options: TaskOptions) {
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

  getTasksLinkedTOEO(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/enablingObjectives/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as Task[];
        })
      )
      );
  }

  //Linked Procedures
  getLinkedProcedures(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/procedures`)
      .pipe(
        map((res: any) => {
          return res['tasksProc'] as Procedure[];
        })
      )
      );
  }

  getLinkedProcedureWithCount(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/procedures/count`)
      .pipe(
        map((res: any) => {
          return res['result'] as ProcedureWithLinkCount[];
        })
      )
      );
  }

  LinkProcedures(id: any, options: TaskOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/procedures`, options)
      .pipe(
        map((res: any) => {
          return res['message'] as string;
        })
      )
      );
  }

  UnlinkProcedures(id: any, options: TaskOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/procedures`, httpOptions)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getTasksLinkedToProc(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/procedures/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as Task[];
        })
      )
      );
  }

  //Linked SaftyHazards
  getLinkedSaftyHazards(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/saftyHazards`)
      .pipe(
        map((res: any) => {
          return res['shList'] as SaftyHazard[];
        })
      )
      );
  }

  LinkSaftyHazards(id: any, options: TaskOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/saftyHazards`, options)
      .pipe(
        map((res: any) => {
          return res['message'] as string;
        })
      )
      );
  }

  UnlinkSaftyHazards(id: any, options: TaskOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/saftyHazards`, httpOptions)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  GetLinkedSHWithCount(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/saftyHazards/count`)
      .pipe(
        map((res: any) => {
          return res['result'] as SafetyHazardWithLinkCount[];
        })
      )
      );
  }

  getLinkedSHWithMetaTaskCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/saftyHazards/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as SafetyHazardWithLinkCount[];
      })
    ));
  }


  GetTasksLinkedtoSH(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/saftyHazards/${id}`).pipe(
      map((res: any) => {
        return res['result'] as Task[];
      })
    ));
  }

  //Linked Positions
  getLinkedpositions(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/positions/count`)
      .pipe(
        map((res: any) => {
          return res['taskPositions'] as TaskPositionWithCount[];
        })
      )
      );
  }

  getLinkedPositionWithMetaTaskCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/positions/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as TaskPositionWithCount[];
      })
    ));
  }

  getTaskLinkedTopositions(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/positions`)
      .pipe(
        map((res: any) => {
          return res['result'] as Task[];
        })
      )
      );
  }

  Linkpositions(id: any, options: TaskOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/positions`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  LinkpositionsWithoutUnlink(id: any, options: TaskOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/positions/without`, options)
      .pipe(
        map((res: any) => {
          return res['positions'] as Position;
        })
      )
      );
  }

  Unlinkpositions(id: any, options: TaskOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/positions`, httpOptions)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  //Linked Regulatory Requirements
  getLinkedRRWithCount(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/rr/count`)
      .pipe(
        map((res: any) => {
          return res['result'] as TaskRRWithCount[];
        })
      )
      );
  }

  getLinkedRRWithMetaEOCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/rr/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as TaskRRWithCount[];
      })
    ));
  }

  getLinkedRR(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/rr`)
      .pipe(
        map((res: any) => {
          return res['result'] as Task[];
        })
      )
      );
  }
  LinkRR(id: any, options: Task_RR_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/rr`, options)
      .pipe(
        map((res: any) => {
          return res['message'] as any;
        })
      )
      );
  }

  UnlinkRR(id: any, options: TaskOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/rr`, httpOptions)
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

  UnlinkTool(id: any, toolName: string, options: TaskOptions) {
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

  UnlinkMultipleTools(id: any, options: TaskOptions) {
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

  updateTools(id: any, options: TaskOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/tools`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  linkILA(id: any, options: TaskOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/ila`, options)
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }

  unlinkILA(id: any, options: TaskOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/ila`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getLinkedILAWithCount(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/ila/count`)
      .pipe(
        map((res: any) => {
          return res['result'] as ILAWithCountOptions[];
        })
      )
      );
  }

  getLinkedILAWithMetaTaskCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/ila/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as ILAWithCountOptions[];
      })
    ));
  }

  getLinkedProceduresWithMetaTaskCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/procedures/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as ProcedureWithLinkCount[];
      })
    ));
  }

  getTasksLinkedToILA(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/ila/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as Task[];
        })
      )
      );
  }

  // Task Questions
  getTaskQuestions(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/questions`)
      .pipe(
        map((res: any) => {
          return res['questions'] as Task_Question[];
        })
      )
      );
  }

  addQuestion(id: any, options: QuestionCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/questions`, options)
      .pipe(
        map((res: any) => {
          return res['question'] as Task_Question;
        })
      )
      );
  }

  removeQuestion(taskid: any, questionid: any, options: TaskOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${taskid}/questions/${questionid}`, httpOptions)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getTaskQuestionNumber(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/questions/num`)
      .pipe(
        map((res: any) => {
          return res['result'] as number;
        })
      )
      );
  }

  updateQuestion(id: any, quesId: any, options: QuestionCreateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/questions/${quesId}`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getTaskWithSDA(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/sda/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as Task[];
        })
      )
      );
  }

  getTaskNumber(selectedDAId: any, selectedSubDAId: any) {
    let urlEO = this.baseUrl + `/number/${selectedDAId}/${selectedSubDAId}`;
    return firstValueFrom(this.http
      .get(urlEO)
      .pipe(
        map((res: any) => {
          return res['listEO'] as any[];
        })
      )
      );
  }

  getTaskNumberWithLetter(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/number`)
      .pipe(
        map((res: any) => {
          return res['result'] as TaskNumberVM;
        })
      )
      );
  }

  getLinkedStats(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/linkedStats`)
      .pipe(
        map((res: any) => {
          return res['stats'] as TaskStatsCount;
        })
      )
      );
  }

  getLinkedMetaStats(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/meta/linkedStats`).pipe(
      map((res:any)=>{
        return res['result'] as TaskStatsCount;
      })
    ));
  }

  getOverviewStats() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/stats`)
      .pipe(
        map((res: any) => {
          return res['stats'] as TaskStatsCount;
        })
      )
      );
  }

  getlatestActivity(getTrimmed:boolean = false) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/history/${getTrimmed}`)
      .pipe(
        map((res: any) => {
          return res['history'] as TaskLatestActivityVM[];
        })
      )
      );
  }

  restoreHistory(taskId: any, histId: any) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${taskId}/history/${histId}`, null).pipe(
      map((res: any) => {
        return res['message']
      })
    ));
  }

  GetPendingTasks() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/review')
      .pipe(
        map((res: any) => {
          return res['result'] as TaskWithNumberVM[];
        })
      ));
  }

  getTaskHistory(taskId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${taskId}/history`)
      .pipe(
        map((res: any) => {
          return res['history'] as TaskVersionVM[];
        })
      )
      );
  }

  getAllHistoryForTask(taskId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${taskId}/version`)
      .pipe(
        map((res: any) => {
          return res['result'] as Version_Task[];
        })
      ));
  }

  getTaskPositionEmployees(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/positions/employees`)
      .pipe(
        map((res: any) => {
          return res['employees'] as EmployeeTaskVM[];
        })
      )
      );
  }

  getLinkedEmployeesWithMetaTaskCount(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/positions/employees/allcount`).pipe(
      map((res:any)=>{
        return res['result'] as EmployeeTaskVM[];
      })
    ));
  }

  getLinkedIds(option: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/linkedIds/${option}`)
      .pipe(
        map((res: any) => {
          return res['result'] as number[];
        })
      )
      );
  }

  getTaskACtiveInactive(option: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/list/${option}`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  getLinkedMetaTask(metaTaskId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/meta/${metaTaskId}`)
      .pipe(
        map((res: any) => {
          return res['metaTaskVM'] as TaskMetaLinkVM[];
        })
      )
      );
  }

  LinkTaskToMetaTask(id: any, options: TaskOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/meta`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as Task;
        })
      )
      );
  }

  UnlinkTaskToMetaTask(id: any, options: TaskOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/meta`, httpOptions)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  // Training Groups
  getLinkedTrainingGroups(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/trainingGroups`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingGroup[];
        })
      )
      );
  }

  unlinkTrainingGroups(options: Task_TrainingGroupOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${options.taskId}/trainingGroups`, {
        body: options,
      })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  linkTrainingGroups(options: Task_TrainingGroupOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${options.taskId}/trainingGroups`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  CreateCollaborators(options: TaskCollaboratorInvitationOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/colabInvite`, options).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
  }

  getCollaborators(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/colabInvite/`)
      .pipe(
        map((res: any) => {
          return res['result'] as Task_CollaboratorInvitaiton[]
        })
      ));
  }

  GetAllTaskData(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/alldata`).pipe(
      map((res: any) => {
        return res['result'] as GetTaskWithAllLinkData;
      })
    ));
  }

  getTaskVersions(id: any) {
    return firstValueFrom(this.http.get(environment.QTD + `versionTask/task/${id}`).pipe(
      map((res: any) => {
        return res['result'] as Version_Task[];
      })
    ));
  }

  getMetaTrainingGroups(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/meta/${id}/tg`).pipe(
      map((res:any)=>{
        return res['result'] as TrainingGroup[];
      })
    ));
  }

  getMetaSuggestions(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/meta/${id}/suggestion`).pipe(
      map((res:any)=>{
        return res['result'] as MetaTask_SuggestionsVM[];
      })
    ));
  }

  getMetaQuestionsData(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/meta/${id}/question`).pipe(
      map((res:any)=>{
        return res['result'] as MetaTask_QuestionsVM[]
      })
    ));
  }

  getMetaToolsData(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/meta/${id}/tool`).pipe(
      map((res:any)=>{
        return res['result'] as Tool[]
      })
    ));
  }

  getMetaCondCritRefAsync(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/meta/${id}/extras`).pipe(
      map((res:any)=>{
        return res['result'] as MetaTaskOJTVM;
      })
    ));
  }

  getMetaSteps(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/meta/${id}/steps`).pipe(
      map((res:any)=>{
        return res['result'] as MetaTask_OJTVM[]
      })
    ));
  }


  /// Task EMP Services

  getEMPTask(){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/pending`).pipe(
      map((res:any)=>{

        return res['result'] as TaskQualificationPengingEvaluatorVM[]
      })
    ));
  }

  getSuggestionData(qualificationId:any,taskId:any,employeeId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/suggestions/${qualificationId}/task/${taskId}/emp/${employeeId}`).pipe(
      map((res:any)=>{

        return res['result'] as any[]
      })
    ));
  }

  getSuggestionSQData(skillqualificationId:any,skillId:any,employeeId:any){
    return firstValueFrom( this.http.get(this.baseUrl1 + `empTaskQualification/suggestions/${skillqualificationId}/skill/${skillId}/emp/${employeeId}`).pipe(
      map((res:any)=>{

        return res['result'] as any[]
      })
    ));
  } 

  getSuggestionBit(qualificationId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `taskQualification/showTaskSuggestion/${qualificationId}`).pipe(
      map((res:any)=>{
        return res['result'] as boolean
      })
    ));
  }

  getSuggestionSQBit(skillqualificationId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `taskQualification/showSkillSuggestion/${skillqualificationId}`).pipe(
      map((res:any)=>{
        return res['result'] as boolean
      })
    ));
  }


  getQuestionBit(qualificationId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `taskQualification/showTaskSuggestion/${qualificationId}`).pipe(
      map((res:any)=>{
        return res['result'] as boolean
      })
    ));
  }

  getQuestionSQBit(skillqualificationId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `taskQualification/showSkillQuestion/${skillqualificationId}`).pipe(
      map((res:any)=>{
        return res['result'] as boolean
      })
    ));
  }




  saveTaskSuggestionData(options: TaskReQualificationCreateOption) {
    return firstValueFrom(this.http.post(this.baseUrl1 + `empTaskQualification/suggestions`, options).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
  }

  getStepsData(qualificationId:any,taskId:any,employeeId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/steps/${qualificationId}/task/${taskId}/emp/${employeeId}`).pipe(
      map((res:any)=>{
        return res['result'] as any
      })
    ));
  }

   getStepsSQData(skillqualificationId:any,skillId:any,employeeId:any){
    return  firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/steps/${skillqualificationId}/skill/${skillId}/emp/${employeeId}`).pipe(
      map((res:any)=>{
        return res['result'] as any
      })
    ));
  }


  saveStepsData(options: TaskReQualificationStepsCreateOption) {
    return firstValueFrom(this.http.post(this.baseUrl1 + `empTaskQualification/steps`, options).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
  }

  getQuestionData(qualificationId:any,taskId:any,employeeId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/questions/${qualificationId}/task/${taskId}/emp/${employeeId}`).pipe(
      map((res:any)=>{

        return res['result'] as any[]
      })
    ));
  }

  getQuestionSQData(skillQualificationId:any,skillId:any,employeeId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/questions/${skillQualificationId}/skill/${skillId}/emp/${employeeId}`).pipe(
      map((res:any)=>{

        return res['result'] as any[]
      })
    ));
  }

  saveQuestionData(options: TaskReQualificationQuestionsCreateOption) {
    return firstValueFrom(this.http.post(this.baseUrl1 + `empTaskQualification/questions`, options).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
  }
  getTaskSignOffData(qualificationId:any,taskId:any,employeeId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/signoff/${qualificationId}/emp/${employeeId}`).pipe(
      map((res:any)=>{

        return res['result'] as any
      })
    ));
  }

  getSQSignOffData(skillQualificationId:any,taskId:any,employeeId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/signoff/${skillQualificationId}/emp/${employeeId}/skill`).pipe(
      map((res:any)=>{

        return res['result'] as any
      })
    ));
  }
  saveSingOffData(options: TaskReQualificationSignOffOption) {
    return firstValueFrom(this.http.post(this.baseUrl1 + `empTaskQualification/signOff`, options).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
  }

 saveSQSignOffData(options: TaskReQualificationSignOffOption){
   return firstValueFrom(this.http.post(this.baseUrl1 + `empTaskQualification/signOff/skill`, options).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
 }

  getCompletedTaskInEval(isEvaluator:boolean){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/completed/isEvaluator/${isEvaluator}`).pipe(
      map((res:any)=>{

        return res['result'] as any[]
      })
    ));
  }

  getTaskFeedback(qualificationId:any,traineeId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/feedback/qualification/${qualificationId}/trainee/${traineeId}`).pipe(
      map((res:any)=>{
        return res['result']
      })
    ));
  }

   getSQFeedback(skillQualificationId:any,traineeId:any){
    return firstValueFrom(this.http.get(this.baseUrl1 + `empTaskQualification/feedback/skillqualification/${skillQualificationId}/trainee/${traineeId}`).pipe(
      map((res:any)=>{
        return res['result']
      })
    ));
  }
  
  getRequalInfo(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/requal`).pipe(
      map((res:any)=>{
        return res['result'] as TaskRequalVM;
      })
    ));
  }

  getTasksBySDAId(sdaId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/sda/${sdaId}/withNum`).pipe(
      map((res:any)=>{
        return res['result'] as TaskWithNumberVM[];
      })
    ));
  }

  canMakeInactive(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/canDeactive`).pipe(
      map((res:any)=>{
        return res['result'] as boolean;
      })
    ));
  }

  // CanDeleteOJTDataForMetaTask(id:any){
  //   return(this.http.get(this.baseUrl + `/${id}/canDelete/OJT`).pipe(
  //     map((res:any)=>{
  //       return res['result'] as boolean;
  //     })
  //   ).toPromise();
  // }

  getAllTaskData() {  
    return firstValueFrom(this.http
      .get(this.baseUrl + '/getAllTasks')
      .pipe(
        map((res: any) => {
          return res['result'] as TaskWithPositionCompactVM[];
        })
      )
      );
  }

  generateTaskHistoryByTaskReportAsync = (options: ReportExportOptions) => {
        return firstValueFrom(this.http
          .post(this.baseUrl + '/historyByTasks/generateReport', options, { observe: 'response', responseType: 'blob' })
          .pipe(
            map((res: any) => {
              return res;
            })
          ));
      }

  updateTaskAndVersionTaskAsync(options: VersionTaskModel) {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/versionTask', options)
      .pipe(
       map((res: any) => {
        return res['message'];
      })
      ));
  }
}
