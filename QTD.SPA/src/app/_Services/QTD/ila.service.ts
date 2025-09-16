import {
  ILAEnablingObjective_LinkOptions
} from './../../_DtoModels/ILAEnablingObjective_Link/ILAEnablingObjective_LinkOptions';
import {ILANercStandard_LinkOptions} from 'src/app/_DtoModels/ILANercStandard_Link/ILANercStandard_LinkOptions';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map} from 'rxjs/operators';
import {DutyArea} from 'src/app/_DtoModels/DutyArea/DutyArea';
import {ILACreateOptions} from 'src/app/_DtoModels/ILA/ILACreateOptions';
import {Task} from 'src/app/_DtoModels/Task/Task';
import {ILAOptions} from 'src/app/_DtoModels/ILA/ILAOptions';
import {ILAStatsVM} from 'src/app/_DtoModels/ILA/ILAStatsVM';
import {ILAUpdateOptions} from 'src/app/_DtoModels/ILA/ILAUpdateOptions';
import {
  ILACustomObjective_LinkOptions
} from 'src/app/_DtoModels/ILACustomObjective_Link/ILACustomObjective_LinkOptions';
import {
  ILA_EnablingObjective_LinkOptions
} from 'src/app/_DtoModels/ILA_EnablingObjective_Link/ILA_EnablingObjective_LinkOptions';
import {ILAPositionLinkOption} from 'src/app/_DtoModels/ILA_Position_Link/ILA_Position_LinkOptions';
import {ILATaskObjectiveLinkOption} from 'src/app/_DtoModels/ILA_TaskObjective_Link/ILA_TaskObjective_LinkOptions';
import {Position} from 'src/app/_DtoModels/Position/Position';
import {ILAPrerequisitesLinkOptions} from 'src/app/_DtoModels/ILA_Prerequisites_Link/ILA_Prerequisites_LinkOptions';
import {ILA_Procedure_LinkOptions} from 'src/app/_DtoModels/ILA_Procedure_Link/ILA_Procedure_LinkOptions';
import {
  ILARegRequirementsLinkOptions
} from 'src/app/_DtoModels/ILA_RegRequirements_Link/ILA_RegRequirements_LinkOptions';
import {ILASafetyHazardLinkOptions} from 'src/app/_DtoModels/ILA_SafetyHazard_Link/ILA_SafetyHazard_LinkOptions';
import {environment} from 'src/environments/environment';
import {CustomEnablingObjective} from 'src/app/_DtoModels/CustomEnablingObjective/CustomEnablingObjective';
import {EnablingObjective} from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import {ILA} from 'src/app/_DtoModels/ILA/ILA';
import {ILA_Segment_LinkOptions} from 'src/app/_DtoModels/ILA_Segment_Link/ILA_Segment_LinkOptions';
import {Segment} from 'src/app/_DtoModels/Segment/Segment';
import {ILA_UploadOptions} from 'src/app/_DtoModels/ILA/ILA_UploadOptions';
import {ILAHistory} from 'src/app/_DtoModels/ILA/ILAHistory';
import {NercStandard} from 'src/app/_DtoModels/NercStandard/NercStandard';
import {Procedure} from 'src/app/_DtoModels/Procedure/Procedure';
import {
  EMPSettingCBTCreationOptions,
  EMPSettingEvaluationCreationOptions,
  EMPSettingsTQTaskEvaluation,
  EMPSettingStudentEval,
  EMPSettingTestReleaseCreationOptions,
  EMPSettingTQCreationOptions,
  TrainingEnrollStudentCreationOptions
} from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import {StudentEvaluation} from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation';
import {
  Schedule
} from 'src/app/components/qtd-views/implementation/schedulingclasses/scheduling-classes-overview/scheduling-classes-overview.component';
import {EmployeeEnrollOptions} from 'src/app/_DtoModels/ILA/EmployeeEnrollOptions';

import {CBTCreateOptions} from 'src/app/_DtoModels/ILA_CBT/CBTCreateOptions';
import {CBTUpdateOptions} from 'src/app/_DtoModels/ILA_CBT/CBTUpdateOptions';
import {ScormUploadAddOptions} from 'src/app/_DtoModels/Scorm/ScormUploadAddOptions';
import {ScormUploadDeleteOptions} from 'src/app/_DtoModels/Scorm/ScormUploadDeleteOptions';
import {ScormUpload} from 'src/app/_DtoModels/Scorm/ScormUpload';

import {ILAObjectivesVM} from 'src/app/_DtoModels/ILA/ILAObjectivesVM';
import {IlaApplicationDetails} from 'src/app/_DtoModels/ILA/IlaApplicationDetails';
import {IlaApplicationSaveModel} from 'src/app/_DtoModels/ILA/IlaApplicationSaveModel';
import {TaskWithCountOptions} from 'src/app/_DtoModels/Task/TaskWithCountOptions';
import {ILA_Topic} from 'src/app/_DtoModels/ILA_Topic/ILA_Topic';
import {RegulatoryRequirement} from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import {SaftyHazard} from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import {
  ILA_PerformTraineeEvalCreateOptions
} from 'src/app/_DtoModels/ILA_PerformTraineeEval/ILA_PerformTraineeEvalCreateOptions';
import {ILA_PerformTraineeEval} from 'src/app/_DtoModels/ILA_PerformTraineeEval/ILA_PerformTraineeEval';
import {ILABulkOptions} from 'src/app/_DtoModels/ILA/ILABulkOptions';
import {Version_ILAModel} from 'src/app/_DtoModels/Version_ILA/Version_ILA';
import {ILAPublishOptions} from 'src/app/_DtoModels/ILA/ILAPublishOptions';
import {ILAEvalMethodVM} from 'src/app/_DtoModels/ILA/ILAEvalMethodVM';
import {ILACreditHourVM} from 'src/app/_DtoModels/ILA/ILACreditHourVM';
import {CustomEOWithNumVM} from 'src/app/_DtoModels/CustomEnablingObjective/CustomEOWithNumVM';
import {TestFilterOptions} from 'src/app/_DtoModels/Test/TestFilterOptions';
import {ILARequirementsDetailsVM} from 'src/app/_DtoModels/ILA/ILARequirementsDetailsVM';
import {ILAPrerequisitesOptions} from 'src/app/_DtoModels/ILA/ILAPrerequisitesOptions';
import {ILA_SelfRegistrationOption} from '@models/ILA/SelfRegistrationOption';
import { ILADetailsVM } from '@models/ILA/ILADetailsVM';
import { CertifyingBodyCEHUpdateOptions } from '@models/CertifyingBody/CertifyingBodyCEHUpdateOptions';
import { ILATaskObjectiveLinkUpdateOptions } from '@models/ILA_TaskObjective_Link/ILATaskObjectiveLinkUpdateOptions';
import { ILATopicLinkOption } from '@models/ILA_Topic_Link/ILA_Topic_LinkOptions';
import { ILAMetaTaskObjectiveLinkUpdateOptions } from '@models/ILA_TaskObjective_Link/ILAMetaTaskObjectiveLinkUpdateOptions';
import { ILAProviderVM } from '@models/ILA/ILAProviderVM';
import { ILAStatsDataVM } from '@models/ILA/ILAStatsDataVM';
import { ReportExportOptions } from '@models/Report/ReportExportOptions';
import { EmployeeIdsModel } from '@models/Employee/EmployeeIdsModel';
import { firstValueFrom } from 'rxjs';
import { ILABasicCreateOptions } from '@models/ILA/ILABasicCreateOptions';


@Injectable({
  providedIn: 'root',
})
export class IlaService {
  constructor(private http: HttpClient) {
  }

  baseUrl = environment.QTD + 'ila';
  baseUrlForImage = environment.QTD;

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['result'] as any[];
        })
      )
      );
  }

  getAllDraft() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/draft')
      .pipe(
        map((res: any) => {
          return res['result'] as ILAStatsDataVM[];
        })
      )
      );
  }

  getILANumber() {
    return firstValueFrom(this.http.get(this.baseUrl + `/number`).pipe(
      map((res: any) => {
        return res['result'] as number;
      })
    ));
  }

  getAllActive() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/active')
      .pipe(
        map((res: any) => {
          return res['result'] as ILAStatsDataVM[];
        })
      )
      );
  }
  
  getAllActiveDetails() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/active/details')
      .pipe(
        map((res: any) => {
          return res['result'] as ILADetailsVM[];
        })
      )
      );
  }

  getAllilaApplicationData(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + "/" + id + '/application')
      .pipe(
        map((res: any) => {
          return res['result'] as IlaApplicationDetails[];
        })
      )
      );
  }

  CopyIlaById(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + "/copy/" + id)
      .pipe(
        map((res: any) => {
          return res['result'] as IlaApplicationDetails[];
        })
      )
      );
  }

  getAllPublished() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/published')
      .pipe(
        map((res: any) => {
          return res['result'] as ILAStatsDataVM[];
        })
      )
      );
  }

  getILAStats() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/stats')
      .pipe(
        map((res: any) => {
          return res['result'] as ILAStatsVM;
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res['result'] as ILADetailsVM;
        })
      )
      );
  }


  getScheduleClassesByILA(id: any, empId: any, idpId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/ScheduleClasses/${empId}/idp/${idpId}`)
      .pipe(
        map((res: any) => {
          return res['result'] as Schedule[];
        })
      )
      );
  }

  create(option: ILACreateOptions) {

    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  createILAHistory(options: ILAHistory) {
    return firstValueFrom(this.http.post(this.baseUrl + '/history', options).pipe(
      map((res: any) => {
        return res["result"] as ILAHistory;
      })
    ));
  }

  updateIlaApplicationData(options: IlaApplicationSaveModel) {
    return firstValueFrom(this.http.post(this.baseUrl + '/application', options).pipe(
      map((res: any) => {
        return res["result"] as any;
      })
    ));
  }

  update(id: any, options: ILACreateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + '/' + id, options)
      .pipe(
        map((res: any) => {
          return res['result'] as ILADetailsVM;
        })
      )
      );
  }

  EnrollEmployeeIDP(options: EmployeeEnrollOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/enroll`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }


  UnEnrollEmployeeIDP(ilaId: any, employeeId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/UnEnroll/${ilaId}/ScheduleClasses/${employeeId}`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  delete(id: any) {
    var options = new ILAOptions();
    options.actionType = 'delete';
    return firstValueFrom(this.http
      .delete(this.baseUrl + '/' + id, {body: options})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }


  bulkEditIlas(options: any) {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/bulkEdit', options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  changeStatus(id: any, options: ILAOptions) {
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`, {body: options}).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
  }

  linkCustomObjective(id: any, options: ILACustomObjective_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/customeo`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  unlinkCustomObjective(id: any, options: ILACustomObjective_LinkOptions) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/customeo/`, {body: options})
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  getLinkedCustomObjectives(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + "/" + id + "/customeo")
      .pipe(
        map((res: any) => {

          return res['result'] as CustomEnablingObjective[];
        })
      )
      );
  }

  getLinkedCustomObjectivesWithNumber(ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${ilaId}/customeo/withnum`).pipe(
      map((res: any) => {
        return res['result'] as CustomEOWithNumVM[];
      })
    ));
  }

  getLinkedEnablingObjectives(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + "/" + id + "/enablingObjective")
      .pipe(
        map((res: any) => {

          return res['result'] as EnablingObjective[];
        })
      )
      );
  }

  getLinkedTaskObjectives(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + "/" + id + "/taskObjective")
      .pipe(
        map((res: any) => {

          return res['result'] as TaskWithCountOptions[];
        })
      )
      );
  }

  linkEnablingObjective(id: any, options: ILA_EnablingObjective_LinkOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/enablingObjective`, options)
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  unlinkEnablingObjective(id: any, opt: ILA_EnablingObjective_LinkOptions) {

    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/enablingObjective`, {body: opt})
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }


  linkPosition(id: any, options: ILAPositionLinkOption) {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/' + id + '/position', options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }
  updateLinkedPosition(ilaId: string, options: ILAPositionLinkOption){
    return firstValueFrom(this.http
    .put(this.baseUrl + '/' + ilaId + '/position', options)
    .pipe(
      map((res: any) => {
        return res;
      })
    )
    );
  }
  unlinkPosition(ilaId: any, linkIds: ILAPositionLinkOption) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + '/' + ilaId + '/position', {body: linkIds})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getLinkedPositions(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/' + id + '/position')
      .pipe(
        map((res: any) => {
          return res['result'] as Position[];
        })
      )
      );
  }

  LinkProcedure(id: any, options: ILA_Procedure_LinkOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/procedure`, options).pipe(
      map((res: any) => {
        return res
      })
    ))
  }

  LinkSafetyHazard(id: any, options: ILASafetyHazardLinkOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/safetyHazard`, options).pipe(
      map((res: any) => {
        return res;
      })
    ))
  }


  LinkRegRequirement(id: any, options: ILARegRequirementsLinkOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/regRequirement`, options).pipe(
      map((res: any) => {
        return res
      })
    ))
  }


  LinkPrerequisites(id: any, options: ILAPrerequisitesLinkOptions) {

    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/preReq`, options).pipe(
      map((res: any) => {
        return res
      })
    ))
  }

  UnlinkPreRequisites(id:any, options: ILAPrerequisitesLinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/preReq`, {body: options}).pipe(
      map((res:any)=>{
        return res
      })
    ))
  }

  getByProvider(id: any, activeOnly?: any) {
    var queryString = activeOnly ? `?activeOnly=${activeOnly}` : '';
    return firstValueFrom(this.http
      .get(this.baseUrl + `/provider/${id}${queryString}`)
      .pipe(
        map((res: any) => {
          return res['result'] as ILAProviderVM[];
        })
      )
      );
  }

  getByTopic(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/topic/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as ILAProviderVM[];
        })
      )
      );
  }

  changeProvider(id: any, newProviderId: any) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/provider/${newProviderId}`, {})
      .pipe(
        map((res: any) => {
          return res['message'];
        })
      )
      );
  }

  LinkNercStandardLink(id: any, options: ILANercStandard_LinkOptions) {

    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/nercstandard`, options).pipe(
      map((res: any) => {
        return res
      })
    ))
  }

  UnLinkNercStandardLink(id: any, options: ILANercStandard_LinkOptions) {
    return firstValueFrom(this.http.delete(this.baseUrl + '/' + id + '/nercstandard', {body: options}).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  getNercStandardLink(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + "/" + id + "/nercstandard").pipe(
      map((res: any) => {
        return res['result'] as NercStandard[];
      })
    ))
  }

  getLinkedTaskObjectivesWithDutyAreas(ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + '/' + ilaId + '/taskObjective/DutyAreas').pipe(
      map((res: any) => {
        return res['result'] as DutyArea[];
      })
    ));
  }

  getLinkedEOWithEOCategories(ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + '/' + ilaId + '/enablingObjective/enablingObjective_Category')
      .pipe(
        map((res: any) => {
          return res['result']
        })
      ));
  }

  linkSegments(ilaId: any, options: ILA_Segment_LinkOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${ilaId}/segment`, options).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  getLinkedSegments(ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${ilaId}/segment`).pipe(
      map((res: any) => {
        return res['result'] as Segment[];
      })
    ));
  }

  updateLinkedSegmentsOrder(ilaId: any, linkedSegmentsIds: any[]) {
    var data = {
      segmentIds: linkedSegmentsIds
    };
    return firstValueFrom(this.http.post(this.baseUrl + `/${ilaId}/segment/reorder`, data).pipe(
      map((res: any) => {
        return res['result'] as Segment[];
      })
    ));
  }

  uploadFile(ilaId: any, formData: any) {
    return firstValueFrom(this.http.put(this.baseUrl + `/upload/${ilaId}`, formData).pipe(
      map((res: any) => {
        return res['message']
      })
    ));
  }

  getUploadedFiles(ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/upload/${ilaId}`).pipe(
      map((res: any) => {
        return res['result'] as ILA_UploadOptions[];
      })
    ));
  }

  deleteUploadedFile(id: any, uploadId: any) {
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/upload/${uploadId}`).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
  }

  getDownloadData(ilaId: any, id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${ilaId}/download/${id}`).pipe(
      map((res: any) => {
        return res['result'];
      })
    ));
  }

  saveTrainingPlan(ilaId: any, options: ILAUpdateOptions) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${ilaId}/trainingPlan`, options).pipe(
      map((res: any) => {
        return res['message'];
      })
    ));
  }

  getProceduresLink(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + "/" + id + "/procedure").pipe(
      map((res: any) => {
        return res['result'] as Procedure[];
      })
    ))
  }

  getPrerequisitesLink(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + "/" + id + "/preReq").pipe(
      map((res: any) => {
        return res['result'];
      })
    ))
  }

  getSafteyHazardLink(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + "/" + id + "/safetyHazard").pipe(
      map((res: any) => {
        return res['result'] as SaftyHazard[];
      })
    ))
  }

  getRegRequirementLink(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + "/" + id + "/regRequirement").pipe(
      map((res: any) => {
        return res['result'] as RegulatoryRequirement[];
      })
    ))
  }

  getStudentsForILAAsync(ilaId: number) {
    return firstValueFrom(this.http.get(environment.QTD + `ila/${ilaId}/students`).pipe(
      map((res: any) => {
        return res.locList;
      })
    ))
  }

  ///

  RegisterEmployeeToCbtAsync(classScheduleId: number, employeeId: number) {
    return firstValueFrom(this.http
      .post(environment.QTD + `classSchedule/${classScheduleId}/employees/${employeeId}`, null) //passing null as POST method requires object, but in this case we don't have details of object to be passed.
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  createCBTAsync(ilaId: number, options: CBTCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${ilaId}/cbt`, options)
      .pipe(
        map((res: any) => {
          return res.result;
        })
      )
      );
  }

  updateCBTAsync(ilaId: number, cbtId: number, options: CBTUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${ilaId}/cbt/${cbtId}`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getCBTAsync(ilaId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/cbt/${ilaId}`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  GetCBTForILAAsync(ilaId: any) {
    return firstValueFrom(this.http
      .get(environment.QTD + `ila/${ilaId}/cbt`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  GetCBTScormFormsForILAAsync(ilaId: any) {
    return firstValueFrom(this.http
      .get(environment.QTD + `ila/${ilaId}/cbtScormForms`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  GetScormUploadsAsync(cbtId: Number, current: boolean) {
    return firstValueFrom(this.http
      .get(environment.QTD + `cbt/${cbtId}/scorm`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  GetScormUploadAsync(cbtId: number, scormUploadId: number) {
    return firstValueFrom(this.http
      .get(environment.QTD + `cbt/${cbtId}/scorm/${scormUploadId}`)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  AddScormUploadAsync(cbtId: number, file: FormData) {
    return firstValueFrom(this.http
      .post(environment.QTD + `cbt/${cbtId}/scorm`, file)
      .pipe(
        map((res: any) => {
          return res.locList;
        })
      )
      );
  }

  disconnectScormUploadAsync = (cbtId: number, options: ScormUploadDeleteOptions) => {
    return firstValueFrom(this.http.put(environment.QTD + `cbt/${cbtId}/scorm/delete`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  failUploadAsync = (cbtId: number, options: ScormUploadDeleteOptions) => {
    return firstValueFrom(this.http.put(environment.QTD + `cbt/${cbtId}/scorm/fail`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  createIlaCBT(options: EMPSettingCBTCreationOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/setting/cbt', options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getIlaCBT(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/cbt`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  updateIlaCBT(id: any, options: EMPSettingCBTCreationOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/setting/cbt/${id}`, options)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }


  ////
  getAllTestRelease() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['schedules'] as any[];
        })
      )
      );
  }

  createTestRelease(options: EMPSettingTestReleaseCreationOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/setting/test', options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getTestRelease(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/setting/test`)
      .pipe(
        map((res: any) => {

          return res['result'] as any;
        })
      )
      );
  }

  updateTestRelease(id: any, options: EMPSettingTestReleaseCreationOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/setting/test/${id}`, options)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }


  ////
  getAllTestEvaluation() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['schedules'] as any[];
        })
      )
      );
  }

  createTestEvaluation(options: EMPSettingEvaluationCreationOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + "/setting/eval", options)
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }

  getTestEvaluation(ilaId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}/setting/eval`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  updateTestEvaluation(id: any, options: EMPSettingEvaluationCreationOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/setting/eval/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }


  ////
  getAllTQRelease() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['schedules'] as any[];
        })
      )
      );
  }

  createTQRelease(options: EMPSettingTQCreationOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + "/setting/tqEval", options)
      .pipe(
        map((res: any) => {
          return res['result'];
        })
      )
      );
  }

  getTQRelease(ilaId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}/setting/tqEval`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  updateTQRelease(id: any, options: EMPSettingTQCreationOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/setting/tqEval/${id} `, options)
      .pipe(
        map((res: any) => {

          return res['result'] as any;
        })
      )
      );
  }

  createStudentEvaluationRelease(id: any, options: EMPSettingStudentEval) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/studentEvaluation`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  createILA_SelfRegistrationAsync(options: TrainingEnrollStudentCreationOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + '/setting/selfReg', options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getStudentEvaluationRelease(ilaId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}/studentEvaluation`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  createTQTaskEvaluations(options: EMPSettingsTQTaskEvaluation) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${options.ilaId}/evals/link`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  removeTQTaskEvaluations(options: EMPSettingsTQTaskEvaluation) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${options.ilaId}/evals/unlink`, {body: options})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getTQTaskEvaluations(ilaId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}/evals`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  getLinkedStudentEvals(ilaId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}/studentEvaluation`)
      .pipe(
        map((res: any) => {
          return res['result'] as StudentEvaluation[];
        })
      ));
  }

  getAllObjectives(ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${ilaId}/objectives`).pipe(
      map((res: any) => {
        return res['result'] as ILAObjectivesVM[];
      })
    ));
  }

  GetPrerequisitesData(ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${ilaId}/prereq/data`).pipe(
      map((res: any) => {
        return res['result'] as ILADetailsVM[];
      })
    ));
  }

  checkIsProviderNerc(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/provider/isNerc`).pipe(
      map((res: any) => {
        return res['result'] as boolean;
      })
    ));
  }

  createPerformEval(id: any, options: ILA_PerformTraineeEvalCreateOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/perform`, options).pipe(
      map((res: any) => {
        return res['result'] as ILA_PerformTraineeEval;
      })
    ));
  }

  UpdateTObjUsedForTQ(id: any, options: ILATaskObjectiveLinkOption) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/taskObjective/useForTQ`, options).pipe(
      map((res: any) => {
        return res['message'] as string;
      })
    ));
  }

  getPerformEval(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/perform`).pipe(
      map((res: any) => {
        return res['result'] as ILA_PerformTraineeEval;
      })
    ));
  }

  getIlaVersions(id: any,all:boolean) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/versions/${all}`).pipe(
      map((res: any) => {
        return res['result'] as Version_ILAModel[];
      })
    ));
  }

  publisILA(id: any, options: ILAPublishOptions) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/publish`, options).pipe(
      map((res: any) => {
        return res['message'] as string;
      })
    ));
  }

  GetTQForILA(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/tqobjectives`).pipe(
      map((res: any) => {
        return res['result'] as TaskWithCountOptions[];
      })
    ));
  }

  getEvalMethodData(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/evalMethod`).pipe(
      map((res: any) => {
        return res['result'] as ILAEvalMethodVM;
      })
    ));
  }

  saveEvalMethodData(id: any, options: ILAEvalMethodVM) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/evalMethod`, options).pipe(
      map((res: any) => {
        return res['message'] as string;
      })
    ));
  }

  updateEvalMethod(id: any, options: ILAEvalMethodVM) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/evalMethodString`, options).pipe(
      map((res: any) => {
        return res['message'] as string;
      })
    ));
  }

  getWithLinks(testFilterOptions: TestFilterOptions) {
    return firstValueFrom(this.http.post(this.baseUrl + `/traineeEval/filter`, testFilterOptions).pipe(
      map((res: any) => {
        return res['result'] as ILADetailsVM[];
      })
    ));
  }

  UpdateTrainngHours(id: any, options: ILACreditHourVM) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/credHours`, options).pipe(
      map((res: any) => {
        return res['message'] as string;
      })
    ));
  }

  updatePrerequisitesAsync(id: any, options: ILAPrerequisitesOptions) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}/prerequisites`, options).pipe(
      map((res: any) => {
        return res['message'] as string;
      })
    ));
  }

  getTotalTrainingHours(ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${ilaId}/credHours`).pipe(
      map((res: any) => {
        return res['result'] as number;
      })
    ));
  }

  getILANotLinkedToTopic() {
    return firstValueFrom(this.http
      .get(this.baseUrl + '/notLinked/topic')
      .pipe(
        map((res: any) => {
          return res['result'] as ILAStatsDataVM[];
        })
      )
      );
  }

  exportAsCSVAsync(ilaId: number) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}/export`, {observe: 'response', responseType: 'blob'})
      .pipe(
        map((res: any) => {
          return res;
        })
      ));
  }

  getILADetailsByILAId(ilaId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}/requirements/details`)
      .pipe(
        map((res: any) => {
          return res['result'] as ILARequirementsDetailsVM;
        })
      ));
  }

  getILAAsync(ilaId: number) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      ));
  }

  getPreRequisitesAsync(ilaId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${ilaId}/prerequisites`)
      .pipe(
        map((res: any) => {
          return res['result'] as string;
        })
      ));
  }

  saveILACertificationByCertifyingBodyAsync(ilaId:string,certifyingBodyId:string,options:CertifyingBodyCEHUpdateOptions)
    {
      return firstValueFrom(this.http.post(this.baseUrl + `/${ilaId}/certifyingBodies/${certifyingBodyId}/certifications`,options).pipe(
        map((res:any)=>{
          return res;
        })
      ));
    }

    deleteILACertificationByCertifyingBodyAsync(ilaId:string,certifyingBodyId:string)
    {
      return firstValueFrom(this.http.delete(this.baseUrl + `/${ilaId}/certifyingBodies/${certifyingBodyId}`).pipe(
        map((res:any)=>{
          return res;
        })
      ));
    }

    updateILATaskObjectiveLinksAsync(ilaId: any, options: ILATaskObjectiveLinkUpdateOptions) {
      return firstValueFrom(this.http.put(this.baseUrl + `/${ilaId}/taskObjective`, options).pipe(
        map((res: any) => {
          return res;
        })
      ));
    }

    updateLinkedILATopicsAsync(ilaId: string, options: ILATopicLinkOption){
      return firstValueFrom(this.http
      .put(this.baseUrl + '/' + ilaId + '/ilatopic', options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
    }
    getLinkedILATopicsAsync(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl + '/' + id + '/ilatopic')
        .pipe(
          map((res: any) => {
            return res['result'] as ILA_Topic[];
          })
        )
        );
    }
    getTrainingPlanAsync(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl+ '/' + id + '/trainingplan')
        .pipe(
          map((res: any) => {
            return res.result;
          })
        )
        );
    }
    

    getILATrainingTopicsAsync(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl + '/' + id + '/trainingTopic')
        .pipe(
          map((res: any) => {
            return res['result'] as string[];
          })
        )
        );
    }

    getILAPreviewDetailAsync(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl + '/' + id + '/preview')
        .pipe(
          map((res: any) => {
            return res['result'] as any;
          })
        )
        );
    }

    getILANERCCertificationDetailAsync(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl + '/' + id + '/nerccertdetail')
        .pipe(
          map((res: any) => {
            return res['result'] as any[];
          })
        )
        );
    }

    getSelfRegistrationOptionsSettingByILAIdAsync(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl + '/' + id + '/selfreg')
        .pipe(
          map((res: any) => {
            return res['result'] as any;
          })
        )
        );
    }

    generateNERCILAApplicationReportAsync = (options: ReportExportOptions) => {
      return firstValueFrom(this.http
        .post(this.baseUrl + '/nercilaapplication/generateReport', options, { observe: 'response', responseType: 'blob' })
        .pipe(
          map((res: any) => {
            return res;
          })
        )
        );
    }

    canILAbeDeactivated(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl + '/' + id + '/candeactivate')
        .pipe(
          map((res: any) => {
            return res['result'] as any;
          })
        )
        );
    }

    canPopulateOJTBeDeactivateAsync(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl + '/' + id + '/deactivateojtpopulate')
        .pipe(
          map((res: any) => {
            return res['result'] as any;
          })
        )
        );
    }

    getPendingLinkedTaskObjectivesAsync(id: any,options:EmployeeIdsModel) {
      return firstValueFrom(this.http
        .post(this.baseUrl + '/' + id + '/emp/pendingtaskObjective',options)
        .pipe(
          map((res: any) => {
            return res['result'] as any;
          })
        )
        );
    }

    getILANERCCertificationSubRequirementNamesForPartialCreditAsync(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl + '/' + id + '/nerccertsubreqname')
        .pipe(
          map((res: any) => {
            return res['result'] as any[];
          })
        )
        );
    }

    isILACreatedFromInstructorWorkbook(id: any) {
      return firstValueFrom(this.http
        .get(this.baseUrl + '/' + id + '/iscreatedfromiwb')
        .pipe(
          map((res: any) => {
            return res['result'] as boolean;
          })
        )
        );
    }
    
  updateIspubliclyAvailableIla(id: any, options: ILAUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + '/' + id +'/isPubliclyAvailable', options)
      .pipe(
        map((res: any) => {
          return res['result'] as ILADetailsVM;
        })
      )
      );
  }
  
  createBasicILA(option: ILABasicCreateOptions) {
    return firstValueFrom(
      this.http.post<any>(`${this.baseUrl}/basic`, option).pipe(
        map((res: any) => {
          return res;
        })
      )
    );
  }

}
