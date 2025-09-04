import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { ClassSchedule_Evaluation_Roster } from 'src/app/_DtoModels/EmpEvaluation/ClassSchedule_Evaluation_Roster';
import { ClassRoasterUpdateOptions } from 'src/app/_DtoModels/SchedulesClassses/Rosters/ClassRosterUpdateOptions';
import { ClassSchedule_Rosters } from 'src/app/_DtoModels/SchedulesClassses/Rosters/ClassSchedule_Rosters';
import { RosterFetchOptions } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RosterFetchOptions';
import { RoastersModel } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RostersModel';
import { StudentEvaluationSubmitOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationSubmitOptions';
import { StudentEvaluationWithoutEmpCreateOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationWithoutEmpCreateOptions';
import { StudentEvaluation_SaveQuestion } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation_SaveQuestion';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RostersService {
  baseUrl = environment.QTD + 'schedules';
  constructor(private http: HttpClient) { }

  enrollStudents(options:RoastersModel){
    return firstValueFrom(this.http.post(this.baseUrl + `/roster`,options).pipe(
      map((res:any)=>{
        return res['result'] as boolean;
      })
    ));
  }

  getRosterData(options:RosterFetchOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/roster/fetch/type/`,options).pipe(
      map((res:any)=>{
        return res['result'] as RoastersModel[];
      })
    ));
  }

  updateGrade(id:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/grade/roster`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Rosters;
      })
    ));
  }

  updateScore(id:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/score/roster`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Rosters;
      })
    ));
  }

  updateCompDate(id:any, options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/compDate/roster`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Rosters;
      })
    ));
  }

  removeStudent(classId:any,testId:any,testType:string,empId:any){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${classId}/roster/${testId}/${testType}/employee/${empId}`).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  bulkUpdateGrade(classId:any, testId:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${classId}/bulkgrade/roster/${testId}`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Rosters[];
      })
    ));
  }
  releaseTest(empId:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${empId}/release`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Rosters[];
      })
    ));
  }

  reCallTest(empId:any,options:ClassRoasterUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${empId}/recall`,options).pipe(
      map((res:any)=>{
        return res['result'] as ClassSchedule_Rosters[];
      })
    ));
  }

  submitEvaluation(options:StudentEvaluationSubmitOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/emp/evaluation`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

}
