import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { EnablingObjective } from '@models/EnablingObjective/EnablingObjective';
import { ReleasedTQAndSQUpdateOptions } from '@models/TaskQualification/TQAndSQReleasedUpdateOptions';
import { TQReleaseByTaskAndSkillOptions } from '@models/TaskQualification/TQReleaseByTaskAndSkillOptions';
import { map } from 'rxjs/operators';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EMPFilterOptions } from 'src/app/_DtoModels/FilterOptions/EMPFilterOptions';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskWithNumberVM } from 'src/app/_DtoModels/Task/TaskWithNumberVM';
import { ReassignTQVM } from 'src/app/_DtoModels/TaskQualification/ReassignTQVM';
import { TQEmpWithTasksVM } from 'src/app/_DtoModels/TaskQualification/TQEmpWithTasksVM';
import { TQEvaluatorWithCount } from 'src/app/_DtoModels/TaskQualification/TQEvaluatorWithCount';
import { TQReleasedToEMPVM } from 'src/app/_DtoModels/TaskQualification/TQReleasedToEMPVM';
import { TaskQuaificationWithoutPosDateVM } from 'src/app/_DtoModels/TaskQualification/TaskQuaificationWithoutPosDateVM';
import { TaskQualification } from 'src/app/_DtoModels/TaskQualification/TaskQualification';
import { TaskQualificationCreateOptions } from 'src/app/_DtoModels/TaskQualification/TaskQualificationCreateOptions';
import { TaskQualificationEmpVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationEmpVM';
import { TaskQualificationTabVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationTabVM';
import { TaskQualificationWithEvalsVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationWithEvalsVM';
import { TaskQualificationFilterOptions } from 'src/app/_DtoModels/TaskQualificationFilter/TaskQualificationFilterOptions';
import { TQEmpWithPosAndTaskVM } from 'src/app/_DtoModels/Task_Requalification/TQEmpWithPosAndTaskVM';
import { Task_RequalificationStatsVM } from 'src/app/_DtoModels/Task_Requalification/Task_RequalificationStatsVM';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskRequalificationService {
  baseUrl = environment.QTD + 'taskQualification';
  baseURL = environment.QTD + 'emp/empTaskQualification';
  baseurl = environment.QTD + 'empTaskQualification';

  constructor(
    private http: HttpClient,
  ) { }

  create(options:TaskQualificationCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['result'] as TaskQualification;
      })
    ));
  }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as TaskQualificationWithEvalsVM;
      })
    ));
  }

  update(id:any,options:TaskQualificationCreateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res['result'] as TaskQualification;
      })
    ));
  }

  delete(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  filterByPosition(options: TaskQualificationFilterOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + '/position', options).pipe(
      map((res: any) => {
        return res['result'] as TaskQualificationTabVM[];
      })
    ));
  }

  filterByTask(options:TaskQualificationFilterOptions){
    return firstValueFrom(this.http.post(this.baseUrl + '/task', options).pipe(
      map((res:any)=>{
        return res['result'] as TaskQualificationTabVM[];
      })
    ));
  }

  getTaskRequalWithNumber(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/withNumber/${id}`).pipe(
      map((res: any) => {
        return res['result'] as TaskQualificationTabVM;
      })
    ));
  }

  getEmpLinkedToTaskRequal(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/emp`).pipe(
      map((res: any) => {
        return res['result'] as TaskQualificationEmpVM[]
      })
    ));
  }

  filterBySQ(options: TaskQualificationFilterOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + '/sq', options).pipe(
      map((res: any) => {
        return res['result'] as TaskQualificationTabVM[];
      })
    ));
  }

  filterByGroup(options: TaskQualificationFilterOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + '/tg', options).pipe(
      map((res: any) => {
        return res['result'] as TaskQualificationTabVM[];
      })
    ));
  }

  filterByEMP(options: TaskQualificationFilterOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + '/emp', options).pipe(
      map((res: any) => {
        return res['result'] as TaskQualificationTabVM[];
      })
    ));
  }

  filterByEval(options: TaskQualificationFilterOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + '/eval', options).pipe(
      map((res: any) => {
        return res['result'] as TaskQualificationTabVM[];
      })
    ));
  }

  getEmpWithoutPositionQualDate() {
    return firstValueFrom(this.http.get(this.baseUrl + `/withoutPosDate`).pipe(
      map((res: any) => {
        return res['result'] as TaskQuaificationWithoutPosDateVM[];
      })
    ));
  }

  getStats(){
    return firstValueFrom(this.http.get(this.baseUrl + '/stats').pipe(
      map((res:any)=>{
        return res['result'] as Task_RequalificationStatsVM;
      })
    ));
  }

  getAllQualificationsForEMP(id:any,taskId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/allEmp/${id}/task/${taskId}`).pipe(
      map((res:any)=>{
        return res['result'] as TaskQualificationEmpVM[];
      })
    ));
  }

  getEvaluatorWithCount(){
    return firstValueFrom(this.http.get(this.baseUrl + '/eval').pipe(
      map((res:any)=>{
        return res['result'] as TQEvaluatorWithCount[];
      })
    ));
  }

  GetEmpWithTasksForTQEvaluator(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/eval/${id}/empwithtasks`).pipe(
      map((res:any)=>{
        return res['result'] as TQEmpWithTasksVM[];
      })
    ));
  }

  getRequalTasksForEMP(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/emp/${id}/tasks`).pipe(
      map((res:any)=>{
        return res['result'] as TaskWithNumberVM[];
      })
    ));
  }

  getPendingTaskRequalForEmp(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/emp/${id}/pending`).pipe(
      map((res:any)=>{
        return res['result'] as TaskWithNumberVM[];
      })
    ));
  }

  removeEvaluator(id:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/eval/${id}`).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getpendingTaskQualifications(){
    return firstValueFrom(this.http.get(this.baseUrl + `/pending`).pipe(
      map((res:any)=>{
        return res['result'] as TaskQuaificationWithoutPosDateVM[]
      })
    ));
  }

  getEmployeeDataWithoutTQRecords(){
    return firstValueFrom(this.http.get(this.baseUrl + `/without`).pipe(
      map((res:any)=>{
        return res['result'] as TaskQuaificationWithoutPosDateVM[];
      })
    ));
  }

  getAllTaskQualifications(){
    return firstValueFrom(this.http.get(this.baseUrl + `/recentTQ` ).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }

  GetTQReleasedToEMP(){
    return firstValueFrom(this.http.get(this.baseUrl + `/released`).pipe(
      map((res:any)=>{
        return res['result'] as TQReleasedToEMPVM[];
      })
    ));
  }

  getTaskTreeDataForPosition(option:EMPFilterOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/task/posFilter`,option).pipe(
      map((res:any)=>{
        return res['result'] as DutyArea[];
      })
    ));
  }

  getEOTreeDataForPosition(option:EMPFilterOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/eo/posFilter`,option).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective[];
      })
    ));
  }

  getEmpForFilter(options:EMPFilterOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/emp/filter`,options).pipe(
      map((res:any)=>{
        return res['result'] as Employee[]
      })
    ));
  }

  createAndRelease(options:TQReleaseByTaskAndSkillOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/release/byTaskAndSkill`,JSON.stringify(options)).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  updateReleasedTQAndSQ(options:ReleasedTQAndSQUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/released`,options).pipe(
      map((res:any)=>{
        return res['result'] as string;
      })
    ));
  }

  reassignEvaluator(options:ReassignTQVM){
    return firstValueFrom(this.http.delete(this.baseUrl + `/reassign`,{body: options}).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getTQByEMP(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/emp/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as TaskQualificationEmpVM[];
      })
    ));
  }

  getRecentTaskQuals(){
    return firstValueFrom(this.http.get(this.baseUrl + `/recent`).pipe(
      map((res:any)=>{
        return res['result'] as TaskQualificationEmpVM[];
      })
    ));
  }

  getCompletedTaskQualificationsAsTraineeAsync(){
    return firstValueFrom(this.http.get(this.baseURL + `/trainee/completed`).pipe(
      map((res:any)=>{
        return res['result'] as TQEmpWithPosAndTaskVM[];
      })
    ));
  }

  getPendingTaskQualificationsAsTraineeAsync(){
    return firstValueFrom(this.http.get(this.baseURL + `/trainee/pending`).pipe(
      map((res:any)=>{
        return res['result'] as TQEmpWithPosAndTaskVM[];
      })
    ));
  }

  getCompletedTaskQualificationsAsEvaluatorAsync(){
    return firstValueFrom(this.http.get(this.baseURL + `/evaluator/completed`).pipe(
      map((res:any)=>{
        return res['result'] as TQEmpWithPosAndTaskVM[];
      })
    ));
  }

  getTQEvaluatorBitAsync(){
    return firstValueFrom(this.http.get(this.baseURL + `/tqevaluator`).pipe(
      map((res:any)=>{
        return res['result'] as boolean
      })
    ));
  }

}
