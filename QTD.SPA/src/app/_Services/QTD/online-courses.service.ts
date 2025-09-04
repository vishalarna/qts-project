import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedQuery, PagedResult } from '@models/index';
import { ClassSchedule_Employee } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedule_Employee';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OnlineCoursesService {
  private baseUrl = environment.QTD + 'emp/onlinecourse';

  constructor(private readonly http: HttpClient) {}

  getCompletedCourses(query: PagedQuery): Observable<PagedResult<ClassSchedule_Employee>> {
    return this.http.get<PagedResult<ClassSchedule_Employee>>(
      `${this.baseUrl}/completed?orderBy=${query.orderBy ?? ''}&page=${query.page}&pageSize=${query.pageSize}`
    );
  }

  getPendingCourses(query: PagedQuery): Observable<PagedResult<ClassSchedule_Employee>> {
    return this.http.get<PagedResult<ClassSchedule_Employee>>(
      `${this.baseUrl}/pending?orderBy=${query.orderBy ?? ''}&page=${query.page}&pageSize=${query.pageSize}`
    );
  }
}
