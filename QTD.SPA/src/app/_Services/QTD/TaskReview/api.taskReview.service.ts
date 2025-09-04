import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { TaskReviewActionItem_VM } from "@models/Task_Review/TaskReviewActionItem_VM";
import { TaskReviewStatusVM } from "@models/Task_Review/TaskReviewStatusVM";
import { TaskReview_ReviewerOption } from "@models/Task_Review/TaskReview_ReviewerOption";
import { TaskReview_Reviewer_VM } from "@models/Task_Review/TaskReview_Reviewer_VM";
import { TaskReview_VM } from "@models/Task_Review/TaskReview_VM";
import { TaskReviewOptions } from "@models/Task_Review/TaskReviewoptions";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";


@Injectable({
    providedIn: 'root',
  })

  export class ApiTaskReviewService{
    baseUrl = environment.QTD + 'taskReviews';

    constructor(private http: HttpClient) {}

    getAsync(id:string){
      return firstValueFrom(this.http
        .get(this.baseUrl + `/${id}`)
        .pipe(
          map((res: any) => {
            return res.result as TaskReview_VM;
          })
        )
        );
    }

    createTaskReviewReviewerAsync = (id:string,options: TaskReview_ReviewerOption) => {
      return firstValueFrom(this.http
        .post(this.baseUrl + `/${id}/reviewers`, options)
        .pipe(
          map((res: any) => {
            return res.result as TaskReview_Reviewer_VM;
          })
        )
        );
    };

    deleteTaskReviewReviewerAsync = (id:string,reviewerId:any) => {
      return firstValueFrom(this.http
        .delete(this.baseUrl + `/${id}/reviewers/${reviewerId}`)
        .pipe(
          map((res: any) => {
            return res;
          })
        )
        );
    };

    createTaskReviewActionItemAsync = (id:string,options: TaskReviewActionItem_VM) => {
      return firstValueFrom(this.http
        .post(this.baseUrl + `/${id}/taskReviewActionItems`, options)
        .pipe(
          map((res: any) => {
            return res.result as TaskReviewActionItem_VM;
          })
        )
        );
    }

    deleteAsync=(id:string)=>{
      return firstValueFrom(this.http
        .delete(this.baseUrl + `/${id}`)
        .pipe(
          map((res: any) => {
            return res.message;
          })
        )
        );
    }

    updateAsync=(id:string,options:TaskReview_VM)=>{
      return firstValueFrom(this.http
        .put(this.baseUrl + `/${id}`,options)
        .pipe(
          map((res: any) => {
            return res.message;
          })
        )
        );
    }

    getAllStatusAsync(){
      return firstValueFrom(this.http
        .get(this.baseUrl + `/status`)
        .pipe(
          map((res: any) => {
            return res.result as TaskReviewStatusVM[];
          })
        )
        );
    }

   unlinkTaskReviewsAsync = (options: TaskReviewOptions) => {
   return firstValueFrom(this.http
        .delete(this.baseUrl + `/unlink`, { body: options })
        .pipe(
          map((res: any) => {
            return res.message;
          })
        )
        );
   };

  }