import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TrainingIssue_ActionItemStatus_VM } from '@models/TrainingIssues/TrainingIssue_ActionItemStatus_VM';
import { TrainingIssue_ActionItems_VM } from '@models/TrainingIssues/TrainingIssue_ActionItems_VM';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { TrainingIssue_DriverType_VM } from '@models/TrainingIssues/TrainingIssue_DriverType_VM';
import { TrainingIssueOverview_VM } from '@models/TrainingIssues/TrainingIssueOverview_VM';
import { TrainingIssue_DataElementCategory_VM } from '@models/TrainingIssues/TrainingIssue_DataElementCategory_VM';
import { TrainingIssue_ActionItemPriority_VM } from '@models/TrainingIssues/TrainingIssue_ActionItemPriority_VM';
import { TrainingIssue_Severity_VM } from '@models/TrainingIssues/TrainingIssue_Severity_VM';
import { TrainingIssue_Status_VM } from '@models/TrainingIssues/TrainingIssue_Status_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TrainingIssuesService {
  baseUrlTrainingIssues = environment.QTD + `trainingIssues`;
  baseUrl = environment.QTD;
  constructor(private http: HttpClient) { }

  getAsync(id: string) {
    return firstValueFrom(this.http
      .get(this.baseUrlTrainingIssues + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_VM;
        })
      )
      );
  }

  createAsync(options: TrainingIssue_VM) {
    return firstValueFrom(this.http
      .post(this.baseUrlTrainingIssues, options)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_VM;
        })
      )
      );
  }

  updateAsync(options: TrainingIssue_VM, id: string) {
    return firstValueFrom(this.http
      .put(this.baseUrlTrainingIssues + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_VM;
        })
      )
      );
  }

  getAllSeveritiesAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `trainingIssueSeverities`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_Severity_VM[];
        })
      )
      );
  }

  getAllStatusesAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `trainingIssueStatuses`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_Status_VM[];
        })
      )
      );
  }

  getAllActionItemPrioritiesAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `trainingIssueActionItemPriorities`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_ActionItemPriority_VM[];
        })
      )
      );
  }

  copyTrainingIssueByIdAsync(id: any) {
    return firstValueFrom(this.http
      .post(this.baseUrlTrainingIssues + `/${id}`, {})
      .pipe(
        map((res: any) => {
          return res.result;
        })
      )
      );
  }
  activeAsync(id: any) {
    return firstValueFrom(this.http
      .put(this.baseUrlTrainingIssues + `/${id}/activate`, {})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  inactiveAsync(id: any) {
    return firstValueFrom(this.http
      .put(this.baseUrlTrainingIssues + `/${id}/inactivate`, {})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }
  

  deleteTrainingIssueByIdAsync(id: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrlTrainingIssues + `/${id}`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getWithPendingActionItemsAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrlTrainingIssues + `/overview/withPendingActionItems`)
      .pipe(
        map((res: any) => {
          return res.result as TrainingIssue_VM[];
        })
      )
      );
  }

  getWithNoActionItemsAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrlTrainingIssues + `/overview/withNoActionItems`)
      .pipe(
        map((res: any) => {
          return res.result as TrainingIssue_VM[];
        })
      )
      );
  }

  updateActionItemsAsync(options: TrainingIssue_ActionItems_VM, id: string, isStatusCheck:boolean) {
    const params = new HttpParams().set('isStatusCheck', `${isStatusCheck}`);
    return firstValueFrom(this.http
      .put(this.baseUrlTrainingIssues + `/${id}/actionItems`, options,{params} )
      .pipe(
        map((res: any) => {
          return res.result;
        })
      )
      );
  }

  UpdateDataElementAsync(options: TrainingIssue_DataElement_VM, id: string) {
    return firstValueFrom(this.http
      .put(this.baseUrlTrainingIssues + `/${id}/dataElement`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_DataElement_VM;
        })
      )
      );
  }

  getAllTrainingIssueActionItemStatusAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `trainingIssueActionItemStatuses`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_ActionItemStatus_VM[];
        })
      )
      );
  }

  getAllWithSubTypesAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `trainingIssueDriverTypes/withSubTypes`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_DriverType_VM[];
        })
      )
      );
  }

  getOverviewAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrlTrainingIssues + `/overview`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssueOverview_VM;
        })
      )
      );
  }

  getAllDataElementsWithCategoriesAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrlTrainingIssues + `/dataElementsWithCategories`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_DataElementCategory_VM[];
        })
      )
      );
  }

  getTrainingissueByDataElementTypeAsync(id:string,type: string) {
    return firstValueFrom(this.http
      .get(this.baseUrlTrainingIssues + `/dataElement/${id}/Type/${type}`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_VM[];
        })
      )
      );
  }

   getTrainingIssueByTaskReviewIdAsync(taskReviewId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrlTrainingIssues + `/taskReview/${taskReviewId}`)
      .pipe(
        map((res: any) => {
          return res['result'] as TrainingIssue_VM;
        })
      )
      );
  }
}
