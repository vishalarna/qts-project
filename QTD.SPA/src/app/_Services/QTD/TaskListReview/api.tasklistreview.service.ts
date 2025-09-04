import { Injectable } from '@angular/core';
import { ITaskListReviewService } from './itasklistreview.service';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { TaskListReviewOverview_VM } from '@models/TaskListReview/TaskListReviewOverview_VM';
import { TaskListReview_VM } from '@models/TaskListReview/TaskListReview_VM';
import { TaskReviewCreateOption } from '@models/Task_Review/TaskReviewCreateOption';
import { TaskListReview_TaskReview_VM } from '@models/TaskListReview/TaskListReview_TaskReview_VM';
import { ReportExportOptions } from '@models/Report/ReportExportOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiTaskListReviewService implements ITaskListReviewService {
  baseUrl = environment.QTD + 'taskListReviews';

  constructor(private http: HttpClient) {}

  getOverviewAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/overview')
      .pipe(
        map((res: any) => {
          return res.result as TaskListReviewOverview_VM;
        })
      )
      );
  };

  getAsync = (id : string ) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res.result as TaskListReview_VM;
        })
      )
      );
  };

  createAsync(options: TaskListReview_VM) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res.result as TaskListReview_VM;
        })
      )
      );
  }
  
  updateAsync(id : string, options: TaskListReview_VM) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`,options)
      .pipe(
        map((res: any) => {
          return res.result as TaskListReview_VM;
        })
      )
      );
  }
  
  copyAsync = (id : string ) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}`,{})
      .pipe(
        map((res: any) => {
          return res.result as string;
        })
      )
      );
  };

  deleteAsync = (id : string ) => {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  };

  activateAsync = (id : string ) => {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/activate`,{})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  };

  inactivateAsync = (id : string ) => {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/inactivate`,{})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  };

  createTaskReviewsAsync = (id : string, option : TaskReviewCreateOption) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/taskReviews`,option)
      .pipe(
        map((res: any) => {
          return res.result as TaskListReview_TaskReview_VM[];
        })
      )
      );
  };

  generateReportAsync = (options: ReportExportOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/generateReport', options, { observe: 'response', responseType: 'blob' })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  
}
