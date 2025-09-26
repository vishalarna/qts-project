using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA.CBTReleaseSetting;
using QTD2.Infrastructure.Model.ILA.EvaluationReleaseSetting;
using QTD2.Infrastructure.Model.ILA_AssessmentTool_Link;
using QTD2.Infrastructure.Model.ILA_EnablingObjective_Link;
using QTD2.Infrastructure.Model.ILA_NERCAudience_Link;
using QTD2.Infrastructure.Model.ILA_NercStandard_Link;
using QTD2.Infrastructure.Model.ILA_Position_Link;
using QTD2.Infrastructure.Model.ILA_PreRequisite_Link;
using QTD2.Infrastructure.Model.ILA_Procedure_Link;
using QTD2.Infrastructure.Model.ILA_RegRequirement_Link;
using QTD2.Infrastructure.Model.ILA_SafetyHazard_Link;
using QTD2.Infrastructure.Model.ILA_Cbt_ScormUpload;
using QTD2.Infrastructure.Model.ILA_Segment_Link;
using QTD2.Infrastructure.Model.ILA_StudentEvaluation_Link;
using QTD2.Infrastructure.Model.ILA_TaskObjective_Link;
using QTD2.Infrastructure.Model.ILA_TrainingTopic_Link;
using QTD2.Infrastructure.Model.ILACollaborator;
using QTD2.Infrastructure.Model.ILACustomObjective_Link;
using QTD2.Infrastructure.Model.ILASelfRegistrationOptions;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.TQEvaluatorILAEmpSettings;
using QTD2.Infrastructure.Model.Version_ILA;
using QTD2.Infrastructure.Model.CustomEnablingObjective;
using QTD2.Infrastructure.Model.Test;
using QTD2.Infrastructure.Model.CertifyingBody;
using QTD2.Infrastructure.Model.ILA_Topic_Link;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.ILA_Certification_Link;
using QTD2.Infrastructure.Model.CBT;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.Task_Requalification;


namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IILAService
    {
        public Task<List<ILADetailsVM>> GetAsync();

        public Task<int> GetILANumberAsync();

        public Task<List<ILA>> GetAsync(Expression<Func<ILA, bool>> predicate);
        public Task<List<ILADetailsVM>> GetActiveILADetailsAsync();

        public Task<List<Version_ILAModel>> GetAllVersionsForILAAsync(int id,bool all);

        public Task<List<ILAStatDataVM>> GetPublishedILAs();
        public Task<object> BulkEditILAs(BulkEditOptions options);

        public Task<bool> CheckIsProviderNercAsync(int id);

        public Task<ILA> GetAsync(int id);

        public Task<List<ILAProviderVM>> GetByProviderId(int providerId, bool activeOnly);

        public Task<List<ILAProviderVM>> GetByTopicId(int topicId);

        public Task<ILAStatsVM> GetILAStatCounts();

        public Task<ILA> CreateAsync(ILACreateOptions options);

        public Task<ILADetailsVM> UpdateAsync(int id, ILAUpdateOptions options);

        public System.Threading.Tasks.Task UploadFile(int id, ILAUploadOptions file);
        Task<object> GetLinkedSchedulingClasses(int? id, int empId, int idpId);


        public Task<List<ILA_Upload>> getUploadedFiles(int id);
        public Task<List<ILAObjectivesVM>> GetAllLinkedObjectivesAsync(int id);

        public Task<ILA_Upload> DownloadFile(int ilaId, int id);

        public System.Threading.Tasks.Task AddTrainingPlan(int ilaId, ILAUpdateOptions options);

        public System.Threading.Tasks.Task DeleteUploadedFiles(int id, int uploadId);
        
        public System.Threading.Tasks.Task DeleteUploadedFileFromIwbResources(int uploadId);
       
        public System.Threading.Tasks.Task ChangeProviderAsync(int id, int newProviderId);

        public System.Threading.Tasks.Task DeleteAsync(int id);
        public System.Threading.Tasks.Task CopyAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task UpdateDateAsync(ILA obj);


        public System.Threading.Tasks.Task InActiveAsync(int id);

        public System.Threading.Tasks.Task LinkRegulatoryRequirementAsync(int id, ILA_RegRequirement_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkRegulatoryRequirementAsync(int id, ILA_RegRequirement_LinkOptions rrid);

        public Task<List<RegulatoryRequirement>> GetLinkedRegulatoryRequirementsAsync(int id);

        public Task<ILADetailsVM> LinkAssessmentToolAsync(int id, ILA_AssessmentTool_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkAssessmentToolAsync(int id, int assessmentToolId);

        public Task<List<AssessmentTool>> GetLinkedAssessmentToolsAsync(int id);

        public System.Threading.Tasks.Task LinkNercStandardAsync(int id, ILA_NercStandard_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkNercStandardAsync(int id, ILA_NercStandard_LinkOptions options);

        public Task<List<NercStandard>> GetLinkedNercStandardAsync(int id);

        public System.Threading.Tasks.Task UpdateTObjUsedForTQAsync(int id, ILA_TaskObjective_LinkOptions options);

        public Task<List<TaskWithCountOptions>> GetLinkedTaskObjectivesAsync(int id);

        public Task<List<Domain.Entities.Core.DutyArea>> getDutyAreasForLinkedTasks(int id);

        public Task<ILA> LinkEnablingObjectiveAsync(int id, ILA_EnablingObjective_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkEnablingObjectiveAsync(int id, ILA_EnablingObjective_LinkOptions options);

        public Task<List<EnablingObjective>> GetLinkedEnablingObjectivesAsync(int id);
        public Task<List<EnablingObjective>> GetLinkedEnablingObjectivesByIlaIdsAsync(List<int> ids);

        public Task<List<EnablingObjective_Category>> GetLinkedEOWithCategories(int id);
        public Task<object> UnEnrollEmployee(int ilaId, int empId);
        public Task<object> EnrollEmployee(ILAEmployeeEnrollOption options);


        public Task<ILA> LinkCustomObjectiveAsync(int id, ILACustomObjective_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkCustomObjectiveAsync(int id, ILACustomObjective_LinkOptions options);

        public Task<List<CustomEnablingObjective>> GetLinkedCustomObjectivesAsync(int id);

        public Task<List<CustomEOWithNumber>> GetLinkedCustomEOWithNumberAsync(int id);

        public System.Threading.Tasks.Task LinkSafetyHazardAsync(int id, ILA_SafetyHazard_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkSafetyHazardAsync(int id, ILA_SafetyHazard_LinkOptions shId);

        public Task<List<SaftyHazard>> GetLinkedSafetyHazardsAsync(int id);

        public System.Threading.Tasks.Task LinkProcedureAsync(int id, ILA_Procedure_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkProcedureAsync(int id, ILA_Procedure_LinkOptions procId);

        public Task<List<Procedure>> GetLinkedProceduresAsync(int id);

        public Task<ILADetailsVM> LinkTrainingTopicAsync(int id, ILA_TrainingTopic_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkTrainingTopicAsync(int id, int trTopicId);

        public Task<List<TrainingTopic>> GetLinkedTrainingTopicsAsync(int id);

        public Task<ILA> LinkSegmentAsync(int id, ILA_Segment_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkSegmentAsync(int id, int segmentId);

        public Task<object> GetLinkedSegmentsAsync(int id);

        public Task<ILADetailsVM> LinkCollaboratorAsync(int id, ILACollaboratorOptions options);

        public System.Threading.Tasks.Task UnlinkCollaboratorAsync(int id, ILACollaboratorOptions options);

        public Task<List<CollaboratorInvitation>> GetCollaboratorsAsync(int id);

        public Task<ILADetailsVM> LinkPositionAsync(int id, ILA_Position_LinkOptions options);
        public Task<ILA_PositionChangeCount> UpdateLinkedPositionsAsync(int ilaId, ILA_Position_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkPositionAsync(int id, ILA_Position_LinkOptions positionId);

        public Task<List<Position>> GetLinkedPositionAsync(int id);

        public System.Threading.Tasks.Task LinkPreRequisiteAsync(int id, ILA_PreRequisite_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkPreRequisiteAsync(int id, ILA_PreRequisite_LinkOptions preReq_ilaId);

        public Task<List<ILADetailsVM>> GetPreReqDataAsync(int id);

        public Task<List<ILA>> GetLinkedPreRequisitesAsync(int id);

        /* NERCAudience starts*/

        public Task<ILADetailsVM> LinkNERCAudienceAsync(int id, ILA_NERCAudience_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkNERCAudienceAsync(int id, int ntaId);

        public Task<List<NERCTargetAudience>> GetLinkedNERCAudienceAsync(int id);

        /* NERCAudience ends*/

        public System.Threading.Tasks.Task LinkStudentEvaluationAsync(int id, ILA_StudentEvaluation_LinkOptions options);
        public System.Threading.Tasks.Task LinkStudentEvaluationILAData(int id, LinkStudentEvaluationIlaModel options);
        public Task<object> GetStudentEvaluationILAData(int id);

        public System.Threading.Tasks.Task UnlinkstudentEvaluationAsync(int id, ILA_StudentEvaluation_LinkOptions options);

        public Task<List<StudentEvaluation>> GetLinkedStudentEvaluationAsync(int id);

        public System.Threading.Tasks.Task UnlinkAllEvaluationsAsync(int id);

        /* CBT Setting Start */

        public Task<CBT> GetCBTSettingAsync(int id);

        public Task<CBT> GetCBTSettingForILAAsync(int ilaId, bool current);
        public Task<List<CbtVM>> GetCBTScormFormsForILAAsync(int ilaId, bool current);

        public Task<CBT> CreateCBTSettingAsync(int ilaId, CBTCreateOptions options);

        public Task<CBT> UpdateCBTSettingAsync(int id, CBTUpdateOptions options);

        /* CBT Setting End */

        /* Scorm Upload Setting Start */
        public System.Threading.Tasks.Task<CBT_ScormUpload> AttachScormUploadAsync(int cbtId, IFormFile file);
        public System.Threading.Tasks.Task DisconnectScormUploadAsync(int cbtId);
        public Task<List<CBT_ScormUpload>> GetAllScormUploadAsync(int courseId, bool current);
        public Task<CBT_ScormUpload> GetCurrentScormUploadAsync(int courseId);

        /* Scorm Upload Setting End */

        public Task<List<Person>> GetStudentsForILAAsync(int ilaId);

        /* Eval Setting Start */

        public Task<EvaluationReleaseEMPSettings> CreateEvalSettingAsync(EvaluationReleaseEMPSettingCreateOptions options);

        public Task<EvaluationReleaseEMPSettings> UpdateEvalSettingAsync(int id, EvaluationReleaseEMPSettingUpdateOptions options);

        public Task<EvaluationReleaseEMPSettings> GetEvalSettingAsync(int id);

        public Task<EvaluationReleaseEMPSettings> GetEvalSettingForILAAsync(int ilaId);

        public Task<ILA_PerformTraineeEval> GetPerformEvalAsync(int id);

        public Task<ILA_PerformTraineeEvalVM> CreateOrUpdatePerformAsync(int id, ILA_PerformTraineeEvalCreateOptions options);

        public System.Threading.Tasks.Task PublishILAAsync(int id, ILAPublshOptions options);

        public Task<List<TaskWithCountOptions>> GetTQForILAAsync(int id);

        public Task<ILAEvalMethodVM> GetEvalMethodAndEMPAsync(int id);

        public System.Threading.Tasks.Task UpdateUseForEMPAsync(int id, ILAEvalMethodVM options);
        public System.Threading.Tasks.Task UpdateEvalMethodAsync(int id, ILAEvalMethodVM options);

        public System.Threading.Tasks.Task EnrollStudentAsync(int cbtId, int employeeId);
        /* Eval Setting End */

        #region Self Registration Options
        public Task<ILA_SelfRegistrationOptions> GetSelfRegistrationOptionsSettingAsync(int id);

        public Task<ILA_SelfRegistrationOptions> GetSelfRegistrationOptionsSettingForILAAsync(int ilaId);
        public Task<ILA_SelfRegistrationOptions> CreateSelfRegistrationOptionsSettingAsync(ILA_SelfRegistrationCreateOptions options);

        public Task<ILA_SelfRegistrationOptions> UpdateSelfRegistrationOptionsSettingAsync(int id, ILA_SelfRegistrationCreateOptions options);
        #endregion

        #region TQEvaluatorILASettings
        public Task<TQILAEmpSetting> GetTQEvaluatorSettingForILAAsync(int ilaId);

        public Task<TQILAEmpSetting> CreateTQEvaluatorSettingAsync(TQEvaluatorILAEmpSettings options);

        public Task<TQILAEmpSetting> UpdateTQEvaluatorSettingAsync(int id, TQEvaluatorILAEmpSettings options);

        public Task<List<Employee>> GetTQEvaluatorsForILAAsync(int ilaId);

        public System.Threading.Tasks.Task LinkEvaluators(ILA_EvaluatorOptions options);

        public System.Threading.Tasks.Task UnlinkEvaluator(ILA_EvaluatorOptions options);
        public Task<object> GetApplicationDetailsAsync(int id);
        public Task<object> LinkApplicationAsync(int id, IlaApplicationOptions options);

        #endregion

        public Task<CBT_ScormRegistration> RegisterEmployeeToCbtAsync(int cbtId, int employeeId);

        public Task<List<ILADetailsVM>> GetWithTraineeEvalLinks(TestFilterOptions filterOptions);

        public System.Threading.Tasks.Task UpdateTotalTrainingHoursAsync(int id, ILACreditHourVM hours);

        public Task<double?> GetTotalCredHoursAsync(int id);

        public Task<List<ILAStatDataVM>> GetILAsNotLinkedToTopic();
        public Task<Byte[]> ExportILAAsCSV(int ilaId);
        public Task<ILARequirementsDetailsVM> GetILARequirementsDetailsByILAId(int ilaId);
        public System.Threading.Tasks.Task UpdatePreRequisitesAsync(int ilaId, ILAPrerequisitesOptions options);

        public Task<string> GetPreRequisitesAsync(int ilaId);
        public System.Threading.Tasks.Task DeleteILACertLinksAsync(int id, int certifyingBodyId);
        public System.Threading.Tasks.Task SaveILACertLinksByCertifyingBodyAsync(int id, int certifyingBodyId, CertifyingBodyCEHUpdateOptions options);
        public System.Threading.Tasks.Task<ILATaskChangeModel> UpdateILATaskObjectiveLinksAsync(int ilaId, ILATaskObjectiveLinkUpdateOptions options);
        public Task<ILA_TopicChangeCount> UpdateLinkedILATopicsAsync(int ilaId, ILA_Topic_LinkOptions options);
        public Task<List<ILA_Topic>> GetLinkedILATopicsAsync(int id);
        public Task<List<CBT_ScormUploadVM>> GetAllCBTScormUploads();
        public System.Threading.Tasks.Task ReorderObjectiveLinks(int ilaId);
        public bool HasILAChanges(ILA ila, ILAUpdateOptions options);
        public bool HasILASelfRegistrationOptionChanges(ILA_SelfRegistrationOptions ilaSelfRegOptions, ILA_SelfRegistrationCreateOptions options);
        public Task<List<ILAStatDataVM>> GetActiveILAs();
        public Task<List<ILAStatDataVM>> GetDraftILAs();
        public Task<List<string>> GetILALinkedTrainingTopicsNamesAsync(int id);
        public Task<ILAPreviewVM> GetILAPreviewDetailsAsync(int id);
        public Task<List<ILACertificationLinkVM>> GetILANERCCertificationDetailsAsync(int id);
        public Task<ILA_SelfRegistrationOptions_ViewModel> GetSelfRegistrationOptionsSettingByILAIdAsync(int ilaId);
        public Task<string> GetTrainingPlanByILAIdAsync(int id);
        public Task<ILADetailsVM> GetILAByIdAsync(int id);
        public ILADetailsVM MapILADetailsVMByILA(ILA ila);
        public Task<List<string>> CanILABeDeactivateAsync(int ilaId);
        public Task<bool> CanPopulateOJTBeDeactivateAsync(int ilaId);
        public Task<List<TQwithTask_EvalDetailVM>> GetPendingLinkedTaskObjectivesAsync(int id, EmployeeIdsModel options);
        public System.Threading.Tasks.Task UpdateClassSizeAsync(int? classSize, int ilaId);
        public Task<List<ILACertificationLink_SubRequirementVM>> GetILANERCCertificationSubRequirementNamesForPartialCreditAsync(int ilaId);
        public Task<bool> IsILACreatedFromInstructorWorkbook(int ilaId);
        public System.Threading.Tasks.Task<ILADetailsVM> UpdateIspubliclyAvailableIla(int id, ILAUpdateOptions options);
        public Task<ILA> CreateBasicAsync(ILABasicCreateOptions options);
    }
}
