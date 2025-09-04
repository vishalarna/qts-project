import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { TaskListReviewType_VM } from '@models/TaskListReview/TaskListReviewType_VM';
import { firstValueFrom } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class ApiTaskListReviewTypeService {
  baseUrl = environment.QTD + 'taskListReviewTypes';

  constructor(private http: HttpClient) {}

  getAllAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res.result as TaskListReviewType_VM[];
        })
      )
      );
  };
}
