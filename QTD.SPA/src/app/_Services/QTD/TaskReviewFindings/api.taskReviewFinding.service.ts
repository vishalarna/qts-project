import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { TaskReviewFinding_VM } from "@models/Task_Review/TaskReviewFinding_VM";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";


@Injectable({
    providedIn: 'root',
  })

  export class ApiTaskReviewFindingService {
    baseUrl = environment.QTD + 'taskReviewFindings';
  
    constructor(private http: HttpClient) {}
  
    getAllAsync = () => {
      return firstValueFrom(this.http
        .get(this.baseUrl)
        .pipe(
          map((res: any) => {
            return res.result as TaskReviewFinding_VM[];
          })
        ));
    };
  }