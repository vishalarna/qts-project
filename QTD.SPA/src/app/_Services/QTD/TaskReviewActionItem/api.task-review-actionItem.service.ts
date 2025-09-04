import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Provider } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { TaskReviewActionItem_VM } from '@models/Task_Review/TaskReviewActionItem_VM';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TaskReviewActionItemService {
  baseUrl = environment.QTD ;
  baseUrlForActionItem = environment.QTD + "taskReviewActionItems" ;
  constructor(private http: HttpClient) {}

  getActionItemTypes = () => {
    return firstValueFrom(this.http
      .get(this.baseUrlForActionItem + "/types")
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }

  getOperationTypesAsync = (actionItemType:string) => {
    return firstValueFrom(this.http
      .get(this.baseUrlForActionItem + `/operationTypes/${actionItemType}`)
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }

  getAllActionItemPriorities = ()=>{
    return firstValueFrom(this.http
      .get(this.baseUrl + `taskReviewActionItemsPriorities`)
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }

  getAsync = (id:string) => {
    return firstValueFrom(this.http
      .get(this.baseUrlForActionItem + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as TaskReviewActionItem_VM;
        })
      )
      );
  }

  deleteAsync = (id:string) => {
    return firstValueFrom(this.http
      .delete(this.baseUrlForActionItem + `/${id}`)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  updateAsync = (id:string,options: TaskReviewActionItem_VM) => {
    return firstValueFrom(this.http
      .put(this.baseUrlForActionItem + `/${id}`,options)
      .pipe(
        map((res: any) => {
          return res['result'] as TaskReviewActionItem_VM;
        })
      )
      );
  }
}
