import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { TaskListReviewStatus_VM } from '@models/TaskListReview/TaskListReviewStatus_VM';
import { firstValueFrom } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class ApiTaskListReviewStatusService {
  baseUrl = environment.QTD + 'taskListReviewStatus';

  constructor(private http: HttpClient) {}

  getAllAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res.result as TaskListReviewStatus_VM[];
        })
      ));
  };
}
