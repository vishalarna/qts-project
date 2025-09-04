import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {firstValueFrom, Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {EmployeeDashboardStats} from '@models/Employee';
import {Result} from '@models/result';
import {ClassInfoVM} from 'src/app/_DtoModels/Dashboard/ClassInfoVM';
import {DetailsEventVM} from 'src/app/_DtoModels/Dashboard/DetailsEventVM';
import {GetDueTrainingOptions} from 'src/app/_DtoModels/Dashboard/GetDueTrainingOptions';
import {environment} from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private readonly baseUrl = environment.QTD + 'dashboard';
  private readonly baseUrlEmp = environment.QTD + 'emp/dashboard';

  constructor(private readonly http: HttpClient) {}

  getStats(): Observable<Result<EmployeeDashboardStats>> {
    return this.http.get<Result<EmployeeDashboardStats>>(`${this.baseUrlEmp}/statistics`).pipe(
      map((res: any)=>{
        return res['result'] as any;
      })
    );;
  }

  getEventsForDate(date: any): Observable<any> {
    return this.http.get(`${this.baseUrl}/trainingDue/${date}`).pipe(
      map((res: any)=>{
        return res['result'] as any;
      })
    );
  }

  getFinalReminderEvents(options:GetDueTrainingOptions){
    return firstValueFrom(this.http.post(this.baseUrl + `/trainingSchedule/final`,options).pipe(
      map((res:any)=>{
        return res['result'] as DetailsEventVM;
      })
    ));
  }

  getInProgressEvents(startDate:any,endDate:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/trainingSchedule/inProgress/${startDate}/${endDate}`).pipe(
      map((res:any)=>{
        return res['result'] as DetailsEventVM;
      })
    ));
  }

  getTodaysScheduleEvents(date:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/trainingSchedule/today/${date}`).pipe(
      map((res:any)=>{
        return res['result'] as DetailsEventVM;
      })
    ));
  }

  getClassInfo(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/class/${id}/info`).pipe(
      map((res:any)=>{
        return res['result'] as ClassInfoVM;
      })
    ));
  }

  getCourseInfo(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/ila/${id}/info`).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }

  checkSelfRegCourseAvailable(){
    return firstValueFrom(this.http.get(this.baseUrl + `/checkCourseAvailability`).pipe(
      map((res:any)=>{
        return res['result'] as boolean;
      })
    ));
  }

  getCurrentUserName(){
    return firstValueFrom(this.http.get(this.baseUrlEmp + `/curr/name`).pipe(
      map((res:any)=>{
        return res['result'] as string;
      })
    ));
  }
}
