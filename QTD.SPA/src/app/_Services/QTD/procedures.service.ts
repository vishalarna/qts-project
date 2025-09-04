import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Provider } from '@angular/core';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';
import { firstValueFrom, Observable, of } from 'rxjs';
import { Result } from '@models/result';
import {
  AddEmployeeToProcedureReviewCreationOptions,
  ProcedureReviewDeleteOptions,
  CreateProcedureReview,
  procedureReviewEmpExitoptions,
  procedureReviewEmpUpdateOptions,
} from '@models/Procedure/Procedure_review';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProcedureCreateOptions } from 'src/app/_DtoModels/Procedure/ProcedureCreateOptions';
import { ProcedureUpdateOptions } from 'src/app/_DtoModels/Procedure/ProcedureUpdateOptions';
import { Procedure_EnablingObjective_LinkOptions } from 'src/app/_DtoModels/Procedure_EnablingObjective_Link/Procedure_EnablingObjective_LinkOptions';
import { ProcedureOptions } from 'src/app/_DtoModels/Procedure/ProcedureOptions';
import { Procedure_Task_LinkOptions } from 'src/app/_DtoModels/Procedure_Task_Link/Procedure_Task_LinkOptions';
import { Procedure_StatusHistoryCreateOptions } from 'src/app/_DtoModels/Procedure_StatusHistory/Procedure_StatusHistoryCreateOptions';
import { Procedure_StatusHistory } from 'src/app/_DtoModels/Procedure_StatusHistory/Procedure_StatusHistory';
import { Procedure_SaftyHazard_Link } from 'src/app/_DtoModels/Procedure_SaftyHazard_Link/Procedure_SaftyHazard_Link';
import { RegulatoryRequirement } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import { Procedure_RegulatoryRequirement_LinkOptions } from 'src/app/_DtoModels/Procedure_RegulatoryRequirement_Link/Procedure_RegulatoryRequirement_LinkOptions';
import { ProcedureStatsVM } from 'src/app/_DtoModels/Procedure/ProcedureStatsVM';
import { ProcedureLatestActivityVM } from 'src/app/_DtoModels/Procedure/ProcedureLatestActivityVM';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { TaskWithCountOptions } from 'src/app/_DtoModels/Task/TaskWithCountOptions';
import { SafetyHazardWithLinkCount } from 'src/app/_DtoModels/SaftyHazard/SafetyHazardWithLinkCount';
import { RegulatoryRequirementWithLinkCount } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementWithLinkCount';
import { Procedure_ILA_LinkOptions } from 'src/app/_DtoModels/Procedure/Procedure_ILA_Link/Procedure_ILA_LinkOptions';
import { ILA_Topic } from 'src/app/_DtoModels/ILA_Topic/ILA_Topic';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { Procedure_EnablingObjective_Link } from 'src/app/_DtoModels/Procedure_EnablingObjective_Link/Procedure_EnablingObjective_Link';
import { EOWithCountOptions } from 'src/app/_DtoModels/EnablingObjective/EOWithCountOptions';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubmitProcedureReviewDto } from '@models/Procedure/Procedure_review/submitProcedureReviewDto';
import { ProcedureReviewOverviewVM } from '@models/Procedure/Procedure_review/ProcedureReviewOverviewVM';
import { ILAProviderDataVM } from '@models/Provider/ILAProviderDataVM';
import { ILATopicDataVM } from '@models/ILA_Topic/ILATopicDataVM';


@Injectable({
  providedIn: 'root',
})
export class ProceduresService {
  private readonly baseUrl = environment.QTD + 'procedures';
  private readonly baseUrl1 = environment.QTD;

  constructor(
    private readonly http: HttpClient,
    private readonly alert: SweetAlertService) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['procedures'] as Procedure[];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['procedure'] as Procedure;
        })
      )
      );
  }

  getStatsCount() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/stats')
      .pipe(
        map((res: any) => {
          return res['stats'] as ProcedureStatsVM;
        })
      )
      );
  }

  getNotLinkedWith(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/notlinked`)
      .pipe(
        map((res: any) => {
          return res['result'] as Procedure_IssuingAuthority[];
        })
      )
      );
  }

  //active inactive list of proc and IA
  getActiveInactiveIA(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/listia`)
      .pipe(
        map((res: any) => {
          return res['result'] as Procedure_IssuingAuthority[];
        })
      )
      );
  }

  getActiveInactiveProc(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/listproc`)
      .pipe(
        map((res: any) => {
          return res['result'] as any[];
        })
      )
      );
  }

  create(options: ProcedureCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res['procedure'] as Procedure;
        })
      )
      );
  }

  copy(id: any, options: ProcedureCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/copy`, options).pipe(
      map((res: any) => {
        return res['result'] as Procedure;
      })
    ));
  }

  update(id: any, options: ProcedureUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['procedure'] as Procedure;
        })
      )
      );
  }

  delete(id: any, options: ProcedureOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`, httpOptions)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  linkEnablingObjective(id: any, options: Procedure_EnablingObjective_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/enablingObjectives`, options)
      .pipe(
        map((res: any) => {
          return res['procedure'] as Procedure;
        })
      )
      );
  }

  unlinkEnablingObjective(procedureid: any, enablingObjectiveid: any) {
    return firstValueFrom(this.http
      .delete(
        this.baseUrl +
        `/${procedureid}/enablingObjectives/${enablingObjectiveid}`
      )
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  linkSafetyHazard(id: any, options: Procedure_SaftyHazard_Link) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/saftyHazards`, options)
      .pipe(
        map((res: any) => {
          return res['procedure'] as Procedure;
        })
      )
      );
  }

  unlinkSafetyHazard(procId: any, options: Procedure_SaftyHazard_Link) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${procId}/saftyHazards`, { body: options })
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  getLinkedSafetyHazard(procId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${procId}/saftyHazards`).pipe(
      map((res: any) => {
        return res['result'] as SafetyHazardWithLinkCount[];
      })
    ));
  }

  getLinkedEnablingObjectives(procId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${procId}/enablingObjectives`).pipe(
      map((res: any) => {
        return res['result'] as EOWithCountOptions[];
      })
    ));
  }

  LinkTasks(procId: any, options: Procedure_Task_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${procId}/task`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  UnlinkTasks(procId: any, options: Procedure_Task_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${procId}/task/`, { body: options })
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getLinkedTasks(id: any) {

    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/task`).pipe(
      map((res: any) => {
        return res['result'] as TaskWithCountOptions[];
      })
    ));
  }

  saveStatusHistory(options: Procedure_StatusHistoryCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/history', options)
      .pipe(
        map((res: any) => {
          return res['result'] as Procedure_StatusHistory;
        })
      )
      );
  }

  getStatusHistory(getLatest: boolean = false) {

    return firstValueFrom(this.http
      .get(this.baseUrl + `/history/latest/${getLatest}`)
      .pipe(
        map((res: any) => {
          return res['history'] as ProcedureLatestActivityVM[];
        })
      )
      );
  }

  linkRR(id: any, options: Procedure_RegulatoryRequirement_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/rr`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as RegulatoryRequirement;
        })
      )
      );
  }

  unlinkRR(procId: any, options: Procedure_RegulatoryRequirement_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${procId}/rr`, { body: options })
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getLinkedRR(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/rr`).pipe(
      map((res: any) => {
        return res['result'] as RegulatoryRequirementWithLinkCount[];
      })
    ));
  }

  getProceduresLinkedToSH(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/saftyHazards/${id}`).pipe(
      map((res: any) => {
        return res['result'] as Procedure[]
      })
    ));
  }

  getProceduresLinkedToEO(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/enablingObjectives/${id}`).pipe(
      map((res: any) => {
        return res['result'] as Procedure[]
      })
    ));
  }

  getProceduresLinkedToTask(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/task/${id}`).pipe(
      map((res: any) => {
        return res['result'] as Procedure[];
      })
    ));
  }

  getProceduresLinkedToILA(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/ila/${id}`).pipe(
      map((res: any) => {
        return res['result'] as Procedure[];
      })
    ));
  }

  getProceduresLinkedToRR(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/rr/${id}`).pipe(
      map((res: any) => {
        return res['result'] as Procedure[];
      })
    ));
  }

  LinkILAs(procId: any, options: Procedure_ILA_LinkOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${procId}/ila`, options).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  UnlinkILAs(procId: any, options: Procedure_ILA_LinkOptions) {
    return firstValueFrom(this.http.delete(this.baseUrl + `/${procId}/ila/`, { body: options }).pipe(
      map((res: any) => {
        return res["message"];
      })
    ));
  }

  LinkEOs(procId: any, options: Procedure_EnablingObjective_Link) {

    return firstValueFrom(this.http.post(this.baseUrl + `/${procId}/enablingObjectives`, options).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  UnlinkEOs(procId: any, options: Procedure_EnablingObjective_Link) {
    return firstValueFrom(this.http.delete(this.baseUrl + `/${procId}/enablingObjectives/`, { body: options }).pipe(
      map((res: any) => {
        return res["message"];
      })
    ));
  }

  getProviderWithILAs() {

    return firstValueFrom(this.http.get(this.baseUrl + `/provider/ila`).pipe(
      map((res: any) => {
        return res["result"] as ILAProviderDataVM[];
      })
    ));
  }

  getTopicWithILAs() {
    return firstValueFrom(this.http.get(this.baseUrl + `/topic/ila`).pipe(
      map((res: any) => {
        return res['result'] as ILATopicDataVM[];
      })
    ));
  }

  getLinkedILA(procId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${procId}/ila`).pipe(
      map((res: any) => {
        return res['result'] as ILAWithCountOptions[];
      })
    ));
  }

  addProcedureReview(options: CreateProcedureReview): Observable<Result<any>> {
    return this.http
      .post<Result<any>>(`${this.baseUrl1}procedureReview`, options)
      .pipe(catchError((err) => this.handleError(err)));
  }

  updateProcedureReview(procedureId: string, options: CreateProcedureReview): Observable<Result> {
    return this.http
      .put<Result>(`${this.baseUrl1}procedureReview/${procedureId}`, options)
      .pipe(catchError((err) => this.handleError(err)));
  }

  getProcedureReviews = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl1 + 'procedureReviews')
      .pipe(
        map((res: any) => {
          return res.result as ProcedureReviewOverviewVM[];
        })
      )
      );
  };

  getProcedureReviewById(procedureId: string): Observable<Result<any>> {
    return this.http
      .get<Result<any>>(`${this.baseUrl1}procedureReview/${procedureId}`)
      .pipe(catchError((err) => this.handleError(err)));
  }

  deleteProcedureReviewById(procedureReviewId: string, option: ProcedureReviewDeleteOptions) {
    return firstValueFrom(this.http.delete(`${this.baseUrl1}procedureReview/${procedureReviewId}`, { body: option }).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  linkEmpProcedureReview(procedureReviewId: string, option: AddEmployeeToProcedureReviewCreationOptions) {
    return firstValueFrom(this.http.post(`${this.baseUrl1}procedureReview/${procedureReviewId}/emp`, option).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  getLinkProcedureReviewEmp(procedureReviewId: string) {
    return firstValueFrom(this.http.get(`${this.baseUrl1}procedureReview/${procedureReviewId}/emp`).pipe(
      map((res: any) => {
        return res['result'];
      })
    ));
  }

  getProcedureReviewStatus() {
    return firstValueFrom(this.http.get(`${this.baseUrl1}procedureReview/stats`).pipe(
      map((res: any) => {
        return res['stats'];
      })
    ));
  }

  unLinkProcedureReviewEmp(procedureReviewId: string, option: AddEmployeeToProcedureReviewCreationOptions) {
    return firstValueFrom(this.http.delete(`${this.baseUrl1}procedureReview/${procedureReviewId}/emp`, { body: option }).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  publishProcedureReview(procedureReviewId: string) {
    return firstValueFrom(this.http.put(`${this.baseUrl1}procedureReview/${procedureReviewId}/publish`, null).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  updateProcedureReviewComments(empId: any, options: procedureReviewEmpUpdateOptions) {

    return firstValueFrom(this.http.put(this.baseUrl1 + `procedureReview/${empId}/comments/emp`, options).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }
  updateProcedureReviewResponse(empId: any, options: procedureReviewEmpUpdateOptions) {

    return firstValueFrom(this.http.put(this.baseUrl1 + `procedureReview/${empId}/response/emp`, options).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  getProcedureReviewDraft() {

    return firstValueFrom(this.http.get(this.baseUrl1 + `procedureReview/draft`).pipe(
      map((res: any) => {
        return res['result'];
      })
    ));
  }

  getPublishedList() {

    return firstValueFrom(this.http.get(this.baseUrl1 + `procedureReview/published`).pipe(
      map((res: any) => {
        return res['stats'];
      })
    ));
  }

  getProcedureReviewPending() {

    return firstValueFrom(this.http.get(this.baseUrl1 + `procedureReview/pending`).pipe(
      map((res: any) => {
        return res['result'];
      })
    ));
  }

  // Procedure Review EMP Side

  getProcedureReviewEmpSide() {

    return firstValueFrom(this.http.get(this.baseUrl1 + `procedureReviewEmp/procedureReviews`).pipe(
      map((res: any) => {
        return res['result'];
      })
    ));
  }

  getProcedureInfoByIdInEmp(procedureReviewId: any) {

    return firstValueFrom(this.http.get(this.baseUrl1 + `procedureReviewEmp/${procedureReviewId}`).pipe(
      map((res: any) => {
        return res['result'];
      })
    ));
  }
  submitProcedureReviewInEmp(options: SubmitProcedureReviewDto) {
    return firstValueFrom(this.http.post(this.baseUrl1 + `procedureReviewEmp/submit`, options).pipe(
      map((res: any) => {
        return res['result'];
      })
    ));
  }

  cancelProcedureReviewInEmp(procedureReviewObj: procedureReviewEmpExitoptions) {

    return firstValueFrom(this.http.post(this.baseUrl1 + `procedureReviewEmp/exit`, procedureReviewObj).pipe(
      map((res: any) => {
        return res['result'];
      })
    ));
  }

  IsProcedureReleasedToEmp(procedureId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/procedure/${procedureId}/releaseToEmp`).pipe(
      map((res) => {
        return res['result'];
      })));
  }

  IsIssuingAuthorityReleasedToEmp(issuingAuthorityId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/issuingAuthority/${issuingAuthorityId}/releaseToEmp`).pipe(
      map((res) => {
        return res['result'];
      })));
  }

  private handleError(responseData: any): Observable<Result<any>> {
    const errorMessage = responseData.error?.error ?? responseData.error;
    console.error(errorMessage ?? responseData);
    this.alert.errorAlert(errorMessage ?? responseData);
    return of({error: errorMessage, isSuccess: false});
  }
}
