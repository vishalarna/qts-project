import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, takeUntil } from 'rxjs/operators';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { ClassScheduleEnrollOptions } from 'src/app/_DtoModels/SchedulesClassses/ClassScheduleEnrollOptions';
import { ClassSchedule_Employee } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedule_Employee';
import { ClassSchedules } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedules';
import { EmployeesLinkedToSchedule } from 'src/app/_DtoModels/SchedulesClassses/EmployeesLinkedToSchedule';
import { ClassRoasterUpdateOptions } from 'src/app/_DtoModels/SchedulesClassses/Rosters/ClassRosterUpdateOptions';
import { ScheduleClassHistoryVM } from 'src/app/_DtoModels/SchedulesClassses/ScheduleClassHistory/ScheduleClassHistoryVM';
import { ScheduleClassesStats } from 'src/app/_DtoModels/SchedulesClassses/ScheduleClassesStats';
import { TrainingCreationOptions, TrainingEnrollStudentCreationOptions, TrainingStudentCreationOptions } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { environment } from 'src/environments/environment';
import { StudentEvaluationService } from './student-evaluation.service';
import { StudentEvaluationWithoutEmpCreateOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationWithoutEmpCreateOptions';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { ScheduleEvalVM } from 'src/app/_DtoModels/SchedulesClassses/ScheduleEvalVM';
import { StudentEvaluation } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation';
import { StudentEvaluationWithEMPVM } from 'src/app/_DtoModels/StudentEvaluation/StudentEvalWithEMPVM';
import { EvalReleaseOptions } from 'src/app/_DtoModels/StudentEvaluation/EvalReleaseOptions';
import { ReReleaseOptions } from 'src/app/components/qtd-views/implementation/schedulingclasses/scheduling-classes-overview/fly-panel-view-to-dos/fly-panel-view-to-dos.component';
import { RosterOverviewVM } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RosterOverviewVM';
import { ClassScheduleDetailVM } from '@models/SchedulesClassses/ClassScheduleDetailVM';
import { ReportExportOptions } from '@models/Report/ReportExportOptions';
import { firstValueFrom } from 'rxjs';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TrainingService {

  baseUrl = environment.QTD + 'schedules';
  baseUrl1 = environment.QTD + 'ila/setting/selfReg';
  baseUrl2 = environment.QTD + 'ila';


  constructor(private http: HttpClient) {}


  getAll(startDateTime: string, endDateTime: string): Promise<any[]> {
    return firstValueFrom(this.http
      .get<any[]>(`${this.baseUrl}/startDate/${startDateTime}/endDate/${endDateTime}`)
      .pipe(
        map((res: any) => res['schedules'] as any[])
      )
      );
  }
  create(options: TrainingCreationOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['schedule'] as ClassScheduleDetailVM;
        })
      )
      );
  }
  update(id: any, options: TrainingCreationOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  getAllEmployees() {
    return firstValueFrom(this.http
      .get(this.baseUrl1)
      .pipe(
        map((res: any) => {
          return res['schedules'] as any[];
        })
      )
      );
  }
  createEmployees(options: TrainingStudentCreationOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl +`/${options.classScheduleId}/emp`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getEmployees(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/emp`)
      .pipe(
        map((res: any) => {
          
          return res['result'] as any;
        })
      )
      );
  }

  getRecurrenceEmployees(classId:any,includeCurrentClass:any = true){
    return firstValueFrom(this.http.get(this.baseUrl + `/${classId}/recurrence/${includeCurrentClass}`)
      .pipe(
        map((res:any)=>{
          return res['result'] as ClassSchedules[]
        })
      ));
  }

  updateEmployees(id: any, options: TrainingStudentCreationOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl1 + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  GetStats(){
    return firstValueFrom(this.http.get(this.baseUrl + '/stats').pipe(
      map((res:any)=>{
        return res['result'] as ScheduleClassesStats;
      })
    ));
  }

  getPendingSelfReg(){
    return firstValueFrom(this.http.get(this.baseUrl + `/selfreg`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }

  getTestToReRelease(){
    return firstValueFrom(this.http.get(this.baseUrl + `/rerelease`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }

  readyRetakeData(){
    return firstValueFrom(this.http.get(this.baseUrl + `/retake/release`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }

  getLinkedEmployees(classId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${classId}/emp`).pipe(
      map((res:any)=>{
        return res['result'] as EmployeesLinkedToSchedule[]
      })
    ));
  }

  getOverViewData(classId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${classId}/roster/overview`).pipe(
      map((res:any)=>{
        return res['result'] as RosterOverviewVM[];
      })
    ));
  }

 unLinkedEmployees(id: any, options: TrainingStudentCreationOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/emp`,{body:options}).pipe(
      map((res:any)=>{
        return res['result'] as EmployeesLinkedToSchedule[]
      })
    ));
  }

  // Get all history
  getAllHistory(){
    return firstValueFrom(this.http.get(this.baseUrl + `/latestActivity`).pipe(
      map((res:any)=>{
        return res['result'] as ScheduleClassHistoryVM[];
      })
    ));
  }

  updateNotes(id:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/notes/emp`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Employee;
      })
    ));
  }

  updateGrade(id:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/grade/emp`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Employee;
      })
    ));
  }

  updateScore(id:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/score/emp`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Employee;
      })
    ));
  }

  bulkUpdate(id:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/bulkgrade/emp`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Employee;
      })
    ));
  }

  getILAsToSchedule(){
    return firstValueFrom(this.http.get(this.baseUrl + `/needsc`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }

  getWaitlistedData(){
    return firstValueFrom(this.http.get(this.baseUrl + `/waiting`).pipe(
      map((res:any)=>{
        return res['result'] as any[]
      })
    ));
  }

  enrollStudent(options: ClassScheduleEnrollOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/addto/enroll`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }
  enrollStudentWithClassSizeByPass(options: ClassScheduleEnrollOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/addto/enroll/extendclasssize`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  waitlistStudent(options: ClassScheduleEnrollOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/addto/waitlist`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  declineEmployee(options:ClassScheduleEnrollOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/decline/emp`,{ body:options }).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }


  getClassesByIla(ilaId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/class/${ilaId}`).pipe(
      map((res:any)=>{
        return res['schedule'] as any[]
      })
    ));
  }

  createEvalWithoutEMP(options:StudentEvaluationWithoutEmpCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/studentEvaluationWithoutEmp`,options).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }

  getEvaluationsForClass(classId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${classId}/studentEvaluationWithoutEmp`).pipe(
      map((res:any)=>{
        return res['result'] as StudentEvaluation[];
      })
    ));
  }

  getDataForEvaluation(classId:any,evalId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${classId}/studentEvaluationWithoutEmp/${evalId}`).pipe(
      map((res:any)=>{
        return res['result'] as StudentEvaluationWithEMPVM[];
      })
    ));
  }

  getAllDataForEvaluationForClass(classId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${classId}/studentEvaluationWithoutEmp/all`).pipe(
      map((res:any)=>{
        return res['result'] as StudentEvaluationWithEMPVM[];
      })
    ));
  }

  releaseOrRecallEvaluation(options:EvalReleaseOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/studentEvaluationWithoutEmp/`,{body:options}).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  updateDateTime(id:any,options: TrainingCreationOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/training`,options).pipe(
      map((res:any)=>{
        return res['result']
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

  getEnrolledStudents(classId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${classId}/enrolled/emp`).pipe(
      map((res:any)=>{
        return res['result'] as Employee[];
      })
    ));
  }
  getClassScheduleRecurrence(classId:any,includeCurrentClass:any = false){
    return firstValueFrom(this.http.get(this.baseUrl + `/${classId}/recurrence/${includeCurrentClass}`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }
  getLinkedStudentEvalsWithCompletedInfo(ilaId:any,classId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${ilaId}/eval/${classId}`).pipe(
      map((res:any)=>{
        return res['result'] as ScheduleEvalVM[];
      })
    ));
  }

  reReleaseTest(options:ReReleaseOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/rerelease`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  filterDataByILA(ilaId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/byILA/${ilaId}`).pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }

  getRevieWData(classId: any, ilaId:any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/class/${classId}/ila/${ilaId}`)
      .pipe(
        map((res: any) => {
          return res['schedule'] as any;
        })
      )
      );
  }

  updateEnrollment(id:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/enrollment/emp`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Employee;
      })
    ));
  }

  generateClassRosterReport(options: ReportExportOptions): Observable<HttpResponse<Blob>> {
    return this.http.post<Blob>(
      this.baseUrl + '/generateReport/classroster',
      options,
      { observe: 'response', responseType: 'blob' as 'json' } 
    );
  }

  generateClassInSheetReport(options: ReportExportOptions): Observable<HttpResponse<Blob>> {
    return this.http.post<Blob>(
      this.baseUrl + '/generateReport/classsigninsheet',
      options,
      { observe: 'response', responseType: 'blob' as 'json' } 
    );
  }
}
