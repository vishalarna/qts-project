using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA.CBTReleaseSetting;
using QTD2.Infrastructure.Model.ILA_AssessmentTool_Link;
using QTD2.Infrastructure.Model.ILA_EnablingObjective_Link;
using QTD2.Infrastructure.Model.ILA_NERCAudience_Link;
using QTD2.Infrastructure.Model.ILA_NercStandard_Link;
using QTD2.Infrastructure.Model.ILA_Position_Link;
using QTD2.Infrastructure.Model.ILA_PreRequisite_Link;
using QTD2.Infrastructure.Model.ILA_Procedure_Link;
using QTD2.Infrastructure.Model.ILA_RegRequirement_Link;
using QTD2.Infrastructure.Model.ILA_SafetyHazard_Link;
using QTD2.Infrastructure.Model.ILA_Segment_Link;
using QTD2.Infrastructure.Model.ILA_StudentEvaluation_Link;
using QTD2.Infrastructure.Model.ILA_TaskObjective_Link;
using QTD2.Infrastructure.Model.ILA_TrainingTopic_Link;
using QTD2.Infrastructure.Model.ILACollaborator;
using QTD2.Infrastructure.Model.ILACustomObjective_Link;
using IDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.IDutyAreaService;
using IILA_PreReq_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_PreRequisite_LinkService;
using IILA_StudentEvaluation_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_StudentEvaluation_LinkService;
using IILA_UploadDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_UploadService;
using IILACustomObjective_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILACustomObjective_LinkService;
using IIlaDomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IILAEnablingObjectiveLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_EnablingObjective_LinkService;
using IILASegmentLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_Segment_LinkService;
using IILATaskObjectiveLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService;
using ILAEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_EnablingObjective_LinkService;
using ILANercStandardDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_NercStandard_LinkService;
using ILAPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_Position_LinkService;
using ILAProceduresDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_Procedure_LinkService;
using ILARegRequirementDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_RegRequirement_LinkService;
using ILASafteyHazardDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_SafetyHazard_LinkService;
using ILATaskObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService;
using IProviderDomainService = QTD2.Domain.Interfaces.Service.Core.IProviderService;
using ISubDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.ISubdutyAreaService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using ITopicDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TopicService;
using ICBTDomainService = QTD2.Domain.Interfaces.Service.Core.ICBTService;
using QTD2.Infrastructure.Model.ILA.EvaluationReleaseSetting;
using IEvaluationReleaseEMPSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IEvaluationReleaseEMPSettingsService;
using ISelfRegistrationOptionsDomainService = QTD2.Domain.Interfaces.Service.Core.ISelfRegistrationOptionsService;
using ITQEvaluatorDomainService = QTD2.Domain.Interfaces.Service.Core.ITQILAEmpSettingService;
using QTD2.Infrastructure.JWT;
using QTD2.Infrastructure.Model.ILASelfRegistrationOptions;
using QTD2.Infrastructure.Model.TQEvaluatorILAEmpSettings;
using IILA_Evaluator_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_Evaluator_LinkService;
using QTD2.Domain.Validation;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;
using IClassScheduleEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using QTD2.Infrastructure.Model.ClassSchedule;
using QTD2.Infrastructure.Model.Task;
using ISegmentObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.ISegmentObjective_LinkService;
using IILA_PerformDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_PerformTraineeEvalService;
using IScormUploadService = QTD2.Domain.Interfaces.Service.Core.IScormUploadService;
using QTD2.Infrastructure.Model.ILA_Cbt_ScormUpload;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.HttpClients;
using QTD2.Infrastructure.Rustici.EngineApi;
using IVersion_ILADomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_ILAService;
using QTD2.Infrastructure.Model.Version_ILA;
using IIDPScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IIDPScheduleService;
using ICBT_ScormRegistrationService = QTD2.Domain.Interfaces.Service.Core.ICBT_ScormRegistrationService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using IEOTopicDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_TopicService;
using IEOSubCatDomainServce = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_SubCategoryService;
using IEOCatDomainServce = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_CategoryService;
using IDeliveryMethodDomainService = QTD2.Domain.Interfaces.Service.Core.IDeliveryMethodService;
using IEODomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using IILATraineeEvaluationLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using ISegmentDomainService = QTD2.Domain.Interfaces.Service.Core.ISegmentService;
using ICustomEODomainService = QTD2.Domain.Interfaces.Service.Core.ICustomEnablingObjectiveService;
using IILATrainingTopicDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TrainingTopic_LinkService;
using IILAAssessmentToolLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_AssessmentTool_LinkService;
using QTD2.Infrastructure.Model.CustomEnablingObjective;
using QTD2.Infrastructure.QTDSettings;
using IILACertificationLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILACertificationLinkService;
using IILACertificationSubReqLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILACertificationSubRequirementLinkService;
using IILANercAudienceLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_NERCAudience_LinkService;
using ILATopicLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_Topic_LinkService;
using System.IO;
using QTD2.Infrastructure.Model.IDP;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using QTD2.Infrastructure.Model.Test;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Infrastructure.Model.CertifyingBody;
using QTD2.Infrastructure.Model.ILA_Topic_Link;
using QTD2.Domain.Exceptions;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using QTD2.Domain.Helpers;
using IInstructorWorkbook_ILADesign_ResourcesDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructorWorkbook_ILADesign_ResourcesService;
using QTD2.Infrastructure.Model.Segment;
using QTD2.Infrastructure.Model.ILA_Certification_Link;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.CBT;
using ITaskQualificationDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService;
using IClassScheduleRosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_RosterService;
using IClassScheduleEvaluationRosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluation_RosterService;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.Task_Requalification;
using Sustainsys.Saml2.Configuration;
using QTD2.Infrastructure.Model.Certification;
using IInstructorWorkbook_ProspectiveILADomainService = QTD2.Domain.Interfaces.Service.Core.IInstructorWorkbook_ProspectiveILAService;

namespace QTD2.Application.Services.Shared
{
    public class ILAService : IILAService
    {
        private readonly IDeliveryMethodDomainService _deliveryMethodService;
        private readonly IScormUploadService _scormUploadService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ILAService> _localizer;
        private readonly IIlaDomainService _ilaService;
        private readonly IAssessmentToolService _assessmentToolService;
        private readonly IEmployeeService _employeeService;
        private readonly ICollaboratorInvitationService _invitationService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IProcedureService _procedureService;
        private readonly ISaftyHazardService _saftyHazardService;
        private readonly ITaskService _taskService;
        private readonly IIDPScheduleDomainService _iDPScheduleDomainService;
        private readonly INercStandardService _nercStandardService;
        private readonly INercStandardMemberService _nercStandardMemberService;
        private readonly INERCTargetAudienceService _nercTargetAudienceService;
        private readonly IPositionService _positionService;
        private readonly IRegulatoryRequirementService _regulatoryRequirementService;
        private readonly ISegmentService _segmentService;
        private readonly IILACertificationLinkDomainService _ilaCertificationLinkService;
        private readonly IILACertificationSubReqLinkDomainService _ilaCertificationSubReqLinkService;
        private readonly ITrainingTopicService _trTopicService;
        private readonly IStudentEvaluationService _studentEvaluationFormService;
        private readonly IStudentEvaluationAvailabilityService _studentEvaluationAvailabilityService;
        private readonly IStudentEvaluationAudienceService _studentEvaluationAudienecService;
        private readonly ILAPositionDomainService _position_Link_Service;
        private readonly ILAEnablingObjectiveDomainService _enablingObjective_Link_Service;
        private readonly ICustomEnablingObjectiveService _custom_eo_Service;
        private readonly IILACustomObjective_LinkDomainService _custom_eo_link_Service;
        private readonly IILA_StudentEvaluation_LinkDomainService _student_evaluation_link_Service;
        private readonly ILATaskObjectiveDomainService _taskObjective_link_service;
        private readonly IProviderDomainService _providerDomainService;
        private readonly ITopicDomainService _topicDomainService;
        private readonly IDutyAreaDomainService _dutyAreaService;
        private readonly ISubDutyAreaDomainService _subdutyAreaService;
        private readonly IILATaskObjectiveLinkDomainService _ilaTaskObjectiveLinkService;
        private readonly IILAEnablingObjectiveLinkDomainService _ilaEnablingObjectiveLinkService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILA _ila;
        private readonly Domain.Entities.Core.Task _task;
        private readonly DutyArea _dutyArea;
        private readonly SubdutyArea _subDutyArea;
        private readonly ILA_TaskObjective_Link _ilaTaskObjectiveLink;
        private readonly ILA_SafetyHazard_Link _ilaSafetyHazardLink;
        private readonly ILA_Segment_Link _ilaSegmentLink;
        private readonly ITaskDomainService _taskDomainService;
        private readonly IHasher _hasher;
        private readonly IILASegmentLinkDomainService _ilaSegmentLinkService;
        private readonly IILA_PreReq_LinkDomainService _preReq_linkService;
        private readonly IILA_UploadDomainService _ilaUploadService;
        private readonly ILANercStandardDomainService _ilaNercStandard_link_service;
        private readonly ILAProceduresDomainService _ila_procedure_linkService;
        private readonly ILASafteyHazardDomainService _ila_safetyHazard_linkService;
        private readonly ILARegRequirementDomainService _ila_regRequirement_linkService;
        private readonly ICBTDomainService _cbtService;
        private readonly IEvaluationReleaseEMPSettingDomainService _eval_release_settingService;
        private readonly ISelfRegistrationOptionsDomainService _selfRegistrationOptionsService;
        private readonly ITQEvaluatorDomainService _tqEvalSettings;
        private readonly IILA_Evaluator_LinkDomainService _ila_eval_linkService;
        private readonly IClassScheduleDomainService _classScheduleDomainService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly ILA_StudentEvaluation_Link _ila_studentEvaluation;
        private readonly IClassScheduleEmployeeDomainService _classScheduleEmployeeService;
        private readonly ISegmentObjectiveDomainService _seg_objService;
        private readonly IILA_PerformDomainService _ila_performService;
        private readonly ScormEngineService _scormHttpClient;
        private readonly IVersion_ILADomainService _version_ilaService;
        private readonly IPersonDomainService _personService;
        private readonly IEOTopicDomainService _eo_topicService;
        private readonly IEOSubCatDomainServce _eo_subCatService;
        private readonly IEOCatDomainServce _eo_CatService;
        private readonly ICBT_ScormRegistrationService _cbt_ScormRegistrationService;
        private readonly QTD2.Infrastructure.Database.Interfaces.IInstanceFetcher _instanceFetcher;
        private readonly IEODomainService _enablingObjectiveDomainService;
        private readonly IILATraineeEvaluationLinkDomainService _ilaTraineeEvalService;
        private readonly ISegmentDomainService _segmentDomainService;
        private readonly ICustomEODomainService _custom_eo_domainService;
        private readonly QTDSettings _qtdSettings;
        private readonly IILATrainingTopicDomainService _ilaTrainingTopicService;
        private readonly IILAAssessmentToolLinkDomainService _ilaassessmentToolLinkService;
        private readonly IILANercAudienceLinkDomainService _ilaNercAudienceLinkService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ICertificationService _certificationService;
        private readonly ILATopicLinkDomainService _ilaTopicLinkService;
        private readonly IVersioningService _versioningService;
        private readonly IInstructorWorkbook_ILADesign_ResourcesDomainService _instructorWorkbook_ILADesign_ResourcesDomainService;
        private readonly ITaskQualificationDomainService _taskQualificationDomainService;
        private readonly IClassScheduleRosterDomainService _classScheduleRosterDomainService;
        private readonly IClassScheduleEvaluationRosterDomainService _classScheduleEvaluationRosterDomainService;
        private readonly IInstructorWorkbook_ProspectiveILADomainService _instructorWorkbook_ProspectiveILADomainService;
        public ILAService(
            IDeliveryMethodDomainService deliveryMethodService,
            IClassScheduleEmployeeDomainService classScheduleEmployeeService,
            IClassScheduleService classScheduleService,
            IHttpContextAccessor httpContextAccessor,
            IClassScheduleDomainService classScheduleDomainService,
            IAuthorizationService authorizationService,
            IStringLocalizer<ILAService> localizer,
            IIlaDomainService ilaService,
            UserManager<AppUser> userManager,
            IAssessmentToolService assessmentToolService,
            IEmployeeService employeeService,
            QTD2.Domain.Interfaces.Service.Core.ICertificationService certificationService,
            IEnablingObjectiveService enablingObjectiveService,
            IProcedureService procedureService,
            ISaftyHazardService saftyHazardService,
            ITaskService taskService,
            INercStandardService nercStandardService,
            IPositionService positionService,
            IRegulatoryRequirementService regulatoryRequirementService,
            ISegmentService segmentService,
            ITrainingTopicService trTopicService,
            ILAPositionDomainService position_Link_Service,
            IIDPScheduleDomainService iDPScheduleDomainService,
            ICollaboratorInvitationService invitationService,
            IStudentEvaluationFormService studentEvaluationFormService,
            IStudentEvaluationAvailabilityService studentEvaluationAvailabilityService,
            INERCTargetAudienceService nercTargetAudienceService,
            ICustomEnablingObjectiveService custom_eo_Service,
            IILACustomObjective_LinkDomainService custom_eo_link_Service,
            INercStandardMemberService nercStandardMemberService,
            IDutyAreaDomainService dutyAreaService,
            ISubDutyAreaDomainService subdutyAreaService,
            IILATaskObjectiveLinkDomainService ilaTaskObjectiveLinkService,
            ITaskDomainService taskDomainService,
            IILAEnablingObjectiveLinkDomainService ilaEnablingObjectiveLinkService,
            ILAEnablingObjectiveDomainService enablingObjective_Link_Service,
            IProviderDomainService providerDomainService,
            ITopicDomainService topicDomainService,
            ILATaskObjectiveDomainService taskObjective_link_service,
            IILASegmentLinkDomainService ilaSegmentLinkService,
            IHasher hasher,
            IILA_PreReq_LinkDomainService preReq_linkService,
            IILA_UploadDomainService ilaUploadService,
            IStudentEvaluationAudienceService studentEvaluationAudienecService,
            IILA_StudentEvaluation_LinkDomainService student_evaluation_link_Service,
            ILANercStandardDomainService ilaNercStandard_link_service,
            ILAProceduresDomainService ila_procedure_linkService,
            ILARegRequirementDomainService ila_regRequirement_linkService,
            ILASafteyHazardDomainService ila_safetyHazard_linkService,
            ICBTDomainService cbtService,
            IEvaluationReleaseEMPSettingDomainService eval_release_settingService,
            ISelfRegistrationOptionsDomainService selfRegistrationOptionsService,
            ITQEvaluatorDomainService tqEvalSettings,
            IILA_Evaluator_LinkDomainService ila_eval_linkService,
            IStudentEvaluationService studentEvaluationFormServices,
            ISegmentObjectiveDomainService seg_objService,
            IILA_PerformDomainService ila_performService,
            IScormUploadService scormUploadService,
            ScormEngineService scormHttpClient,
            IVersion_ILADomainService version_ilaService,
            IPersonDomainService personService,
            IEOTopicDomainService eo_topicService,
            IEOSubCatDomainServce eo_subCatService,
            IEOCatDomainServce eo_CatService,
            IILATraineeEvaluationLinkDomainService ilaTraineeEvalService,
            ISegmentDomainService segmentDomainService,
            ICBT_ScormRegistrationService cbt_ScormRegistrationService,
            QTD2.Infrastructure.Database.Interfaces.IInstanceFetcher instanceFetcher,
            IEODomainService enablingObjectiveDomainService,
            IOptions<QTDSettings> qtdSettings,
            ICustomEODomainService custom_eo_domainService,
            IILATrainingTopicDomainService ilaTrainingTopicService,
            IILAAssessmentToolLinkDomainService ilaassessmentToolLinkService,
            IILANercAudienceLinkDomainService ilaNercAudienceLinkService,
            IILACertificationLinkDomainService ilaCertificationLinkService,
             IILACertificationSubReqLinkDomainService ilaCertificationSubReqLinkService,
            ILATopicLinkDomainService ilaTopicLinkService,
            IVersioningService versioningService,
            IInstructorWorkbook_ILADesign_ResourcesDomainService instructorWorkbook_ILADesign_ResourcesDomainService,
            ITaskQualificationDomainService taskQualificationDomainService, IClassScheduleRosterDomainService classScheduleRosterDomainService,
            IClassScheduleEvaluationRosterDomainService classScheduleEvaluationRosterDomainService, IInstructorWorkbook_ProspectiveILADomainService instructorWorkbook_ProspectiveILADomainService
             )
        {
            _deliveryMethodService = deliveryMethodService;
            _classScheduleDomainService = classScheduleDomainService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _ilaService = ilaService;
            _userManager = userManager;
            _assessmentToolService = assessmentToolService;
            _employeeService = employeeService;
            _enablingObjectiveService = enablingObjectiveService;
            _procedureService = procedureService;
            _saftyHazardService = saftyHazardService;
            _taskService = taskService;
            _nercStandardService = nercStandardService;
            _positionService = positionService;
            _regulatoryRequirementService = regulatoryRequirementService;
            _segmentService = segmentService;
            _trTopicService = trTopicService;
            _position_Link_Service = position_Link_Service;
            _invitationService = invitationService;
            _studentEvaluationFormService = studentEvaluationFormServices;
            _studentEvaluationAvailabilityService = studentEvaluationAvailabilityService;
            _nercTargetAudienceService = nercTargetAudienceService;
            _custom_eo_Service = custom_eo_Service;
            _custom_eo_link_Service = custom_eo_link_Service;
            _ila = new ILA();
            _providerDomainService = providerDomainService;
            _topicDomainService = topicDomainService;
            _enablingObjective_Link_Service = enablingObjective_Link_Service;
            _nercStandardMemberService = nercStandardMemberService;
            _dutyArea = new DutyArea();
            _subDutyArea = new SubdutyArea();
            _ilaTaskObjectiveLink = new ILA_TaskObjective_Link();
            _task = new Domain.Entities.Core.Task();
            _dutyAreaService = dutyAreaService;
            _iDPScheduleDomainService = iDPScheduleDomainService;
            _subdutyAreaService = subdutyAreaService;
            _ilaTaskObjectiveLinkService = ilaTaskObjectiveLinkService;
            _taskDomainService = taskDomainService;
            _ilaEnablingObjectiveLinkService = ilaEnablingObjectiveLinkService;
            _taskObjective_link_service = taskObjective_link_service;
            _certificationService = certificationService;
            _hasher = hasher;
            _ilaSegmentLinkService = ilaSegmentLinkService;
            _preReq_linkService = preReq_linkService;
            _ilaUploadService = ilaUploadService;
            _studentEvaluationAudienecService = studentEvaluationAudienecService;
            _student_evaluation_link_Service = student_evaluation_link_Service;
            _ilaNercStandard_link_service = ilaNercStandard_link_service;
            _ila_procedure_linkService = ila_procedure_linkService;
            _ila_regRequirement_linkService = ila_regRequirement_linkService;
            _ila_safetyHazard_linkService = ila_safetyHazard_linkService;
            _ilaSafetyHazardLink = new ILA_SafetyHazard_Link();
            _cbtService = cbtService;
            _eval_release_settingService = eval_release_settingService;
            _selfRegistrationOptionsService = selfRegistrationOptionsService;
            _tqEvalSettings = tqEvalSettings;
            _ila_eval_linkService = ila_eval_linkService;
            _ila_studentEvaluation = new ILA_StudentEvaluation_Link();
            _classScheduleService = classScheduleService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _seg_objService = seg_objService;
            _ila_performService = ila_performService;
            _scormUploadService = scormUploadService;
            _scormHttpClient = scormHttpClient;
            _version_ilaService = version_ilaService;
            _personService = personService;
            _eo_topicService = eo_topicService;
            _eo_subCatService = eo_subCatService;
            _eo_CatService = eo_CatService;
            _cbt_ScormRegistrationService = cbt_ScormRegistrationService;
            _instanceFetcher = instanceFetcher;
            _enablingObjectiveDomainService = enablingObjectiveDomainService;
            _ilaTraineeEvalService = ilaTraineeEvalService;
            _segmentDomainService = segmentDomainService;
            _custom_eo_domainService = custom_eo_domainService;
            _qtdSettings = qtdSettings.Value;
            _ilaTrainingTopicService = ilaTrainingTopicService;
            _ilaassessmentToolLinkService = ilaassessmentToolLinkService;
            _ilaNercAudienceLinkService = ilaNercAudienceLinkService;
            _ilaCertificationLinkService = ilaCertificationLinkService;
            _ilaCertificationSubReqLinkService = ilaCertificationSubReqLinkService;
            _ilaTopicLinkService = ilaTopicLinkService;
            _versioningService = versioningService;
            _instructorWorkbook_ILADesign_ResourcesDomainService = instructorWorkbook_ILADesign_ResourcesDomainService;
            _taskQualificationDomainService = taskQualificationDomainService;
            _classScheduleRosterDomainService = classScheduleRosterDomainService;
            _classScheduleEvaluationRosterDomainService = classScheduleEvaluationRosterDomainService;
            _instructorWorkbook_ProspectiveILADomainService = instructorWorkbook_ProspectiveILADomainService;
        }

        public async System.Threading.Tasks.Task UpdateDateAsync(ILA obj)
        {
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                var validationResult = await _ilaService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await _ilaService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _ilaService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<ILA> CreateAsync(ILACreateOptions options)
        {
            DeliveryMethod newMethod = null;
            var obj = (await _ilaService.FindAsync(x => x.Name == options.Name && x.Number == options.Number)).FirstOrDefault();
            if (obj == null)
            {
                if (!String.IsNullOrEmpty(options.ilaDeliveryMethod))
                {
                    newMethod = new DeliveryMethod
                    {
                        Name = options.ilaDeliveryMethod,
                        DisplayName = options.ilaDeliveryMethod,
                        IsNerc = false,
                        IsUserDefined = true,
                        IsAvailableForAllIlas = options.isAvailableForAllILA == true ? true : false,
                        CreatorIlaId = 0
                    };
                    var DeliverySaveResult = await _deliveryMethodService.AddAsync(newMethod);
                    if (DeliverySaveResult.IsValid)
                    {
                        options.DeliveryMethodId = newMethod.Id;
                    }
                }

                var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
                string input = options.Image;
                string pathToDisplay = "";
                if (!string.IsNullOrEmpty(input))
                {
                    string[] parts = input.Split(new string[] { "base64," }, StringSplitOptions.None);
                    string actualbase64 = parts[1];
                    string[] subparts = parts[0].Split(new string[] { ";data" }, StringSplitOptions.None);
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + subparts[0];
                    string base64Image = actualbase64;
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    pathToDisplay = "img/" + instanceName + "/" + fileName;
                    string outputPath = _qtdSettings.SaveFilePath + "img\\" + instanceName + "\\";
                    if (!Directory.Exists(outputPath))
                        Directory.CreateDirectory(outputPath);
                    outputPath += fileName;
                    File.WriteAllBytes(outputPath, imageBytes);
                }
                options.Image = pathToDisplay;

                obj = new ILA(options.Name, options.NickName, options.Number, options.Description, options.Image, options.TrainingPlan, options.ProviderId, options.IsSelfPaced ?? false, options.IsOptional, options.IsPublished, options.PublishDate, options.DeliveryMethodId, options.HasPilotData, options.IsProgramManual, options.SubmissionDate, options.ApprovalDate, options.ExpirationDate, options.EffectiveDate, false, string.Empty, options.PilotDataNA, options.DoesActivityConform);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["UnableToCreateILA"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _ilaService.AddAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    if (newMethod != null)
                    {
                        newMethod.CreatorIlaId = obj.Id;
                        await _deliveryMethodService.UpdateAsync(newMethod);

                    }
                    obj = (await _ilaService.FindAsync(x => x.Name == options.Name && x.Number == options.Number)).FirstOrDefault();
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task CopyAsync(int id)
        {
            var createdBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var obj = await _ilaService.GetForCopy(id);
            obj = obj.Copy<ILA>(createdBy);

            foreach (var segmentLink in obj.ILA_Segment_Links)
            {
                var segment = segmentLink.Segment.Copy<Segment>(_httpContextAccessor.HttpContext.User.Identity.Name);
                var segmentResult = await _segmentDomainService.AddAsync(segment);
                segmentLink.SegmentId = segment.Id;
            }

            var result = await _ilaService.AddAsync(obj);
            if (!result.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
            }
        }

        public async Task<ILA> CopyLinkedDetailsAsync(ILA ila, int id)
        {
            string[] includes = new string[] { "ILA_TaskObjective_Links.Task", "ILA_EnablingObjective_Links.EnablingObjective", "ILA_Segment_Links.Segment", "ILACustomObjective_Links.CustomEnablingObjective" };
            var ilaCopy = await _ilaService.GetWithIncludeAsync(id, includes);
            foreach (var include in includes)
            {
                switch (include)
                {
                    case "ILA_TaskObjective_Links.Task":
                        foreach (var taskLink in ilaCopy.ILA_TaskObjective_Links)
                        {
                            ila.LinkTaskObjective(taskLink.Task);
                        }
                        break;
                    case "ILA_EnablingObjective_Links.EnablingObjective":
                        foreach (var enablingObjectiveLink in ilaCopy.ILA_EnablingObjective_Links)
                        {
                            ila.LinkEnablingObjective(enablingObjectiveLink.EnablingObjective);
                        }
                        break;
                    case "ILACustomObjective_Links.CustomEnablingObjective":
                        foreach (var customEnablingObjectiveLink in ilaCopy.ILACustomObjective_Links)
                        {
                            ila.LinkCustomEnablingObjective(customEnablingObjectiveLink.CustomEnablingObjective);
                        }
                        break;
                    case "ILA_Segment_Links.Segment":
                        foreach (var segmentLink in ilaCopy.ILA_Segment_Links)
                        {
                            ila.LinkSegment(segmentLink.Segment, segmentLink.DisplayOrder.GetValueOrDefault());
                        }
                        break;
                }
            }
            return ila;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await _ilaService.GetAsync(id);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _ilaService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<List<ILADetailsVM>> GetAsync()
        {
            var obj_list = await _ilaService.FindQuery(x => !x.Deleted).ToListAsync();
            for (int i = 0; i < obj_list.Count(); i++)
            {
                obj_list[i].DeliveryMethod = await _deliveryMethodService.FindQuery(x => x.Id == obj_list[i].DeliveryMethodId).FirstOrDefaultAsync();
                obj_list[i].Provider = await _providerDomainService.FindQuery(x => x.Id == obj_list[i].ProviderId).FirstOrDefaultAsync();
            }
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return obj_list.Select(ila=>MapILADetailsVMByILA(ila)).OrderBy(x => x.Name).ToList();
        }

        public async Task<int> GetILANumberAsync()
        {
            var count = await _ilaService.AllQuery().CountAsync();
            return (count + 1);
        }

        public async Task<List<ILA>> GetAsync(Expression<Func<ILA, bool>> predicate)
        {
            var obj_list = await _ilaService.FindAsync(predicate);
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.OrderBy(x => x.Name).ToList();
        }

        public async Task<List<ILADetailsVM>> GetActiveILADetailsAsync()
        {
            var obj_list = await _ilaService.GetActiveILADetailsAsync();

            List<ILADetailsVM> iLADetailsVM = new List<ILADetailsVM>();

            foreach (var ilaData in obj_list)
            {
                ILADetailsVM data = new ILADetailsVM();
                data.Id = ilaData.Id;
                data.ProviderId = ilaData.ProviderId;
                data.DeliveryMethodId = ilaData.DeliveryMethodId;
                data.Number = ilaData?.Number;
                data.Name = ilaData.Name;
                data.NickName = ilaData?.NickName;
                data.ProviderName = ilaData.Provider?.Name;
                data.IsMeta = "No";
                var sortedCertificationLinks = ilaData.ILACertificationLinks?
                .OrderByDescending(link => link.CreatedDate)
                 .ToList();
                data.CreditHours = sortedCertificationLinks?.FirstOrDefault()?.CEHHours;
                data.DeliveryMethodName = ilaData.DeliveryMethod?.Name;
                data.Status = ilaData.IsPublished ? "Published" : "Draft";
                data.Active = ilaData.Active;

                iLADetailsVM.Add(data);
            }
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return iLADetailsVM.OrderBy(x => x.Name).ToList();
        }


        public async Task<object> BulkEditILAs(BulkEditOptions options)
        {
            switch (options.actionType.Trim().ToLower())
            {
                case "inactive":
                default:
                    foreach (var ilaId in options.iLaIds)
                    {
                        await InActiveAsync(ilaId);
                    }
                    //histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    //histOptions.ChangeNotes = options.ChangeNotes;
                    //histOptions.EnablingObjectiveId = id;
                    //histOptions.OldStatus = true;
                    //histOptions.NewStatus = false;


                    //var eo = await _enablingObjectiveService.GetAsync(id);
                    //var version = _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0).Result;
                    //histOptions.Version_EnablingObjectiveId = version.Id;
                    //await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
                    //message = "recinactive";
                    break;
                case "active":
                    foreach (var ilaId in options.iLaIds)
                    {
                        await ActiveAsync(ilaId);
                    }
                    //histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    //histOptions.ChangeNotes = options.ChangeNotes;
                    //histOptions.EnablingObjectiveId = id;
                    //histOptions.OldStatus = true;
                    //histOptions.NewStatus = false;

                    //var eo1 = await _enablingObjectiveService.GetAsync(id);
                    //var version1 = _versionEnablingObjectiveService.VersionAndCreateCopy(eo1, 2).Result;
                    //histOptions.Version_EnablingObjectiveId = version1.Id;
                    //await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
                    //message = "recactive";
                    break;
                case "delete":
                    foreach (var ilaId in options.iLaIds)
                    {
                        await DeleteAsync(ilaId);
                    }
                    break;
            }
            return new { message = "Completed Successfully" };
        }

        public async Task<List<Version_ILAModel>> GetAllVersionsForILAAsync(int id, bool all)
        {
            var versions = await _version_ilaService.FindQuery(x => x.ILAId == id).ToListAsync();

            if (!all)
            {
                var versionIlas = versions.GroupBy(x => x.EffectiveDate?.Date).OrderByDescending(m => m.Key).ToList();

                List<Version_ILAModel> versionModel = new List<Version_ILAModel>();

                for (int i = 0; i < versionIlas.Count; i++)
                {
                    var versionGroup = versionIlas[i];
                    Version_ILAModel temp = new Version_ILAModel();

                    temp.UserName = versionGroup.FirstOrDefault().CreatedBy;
                    temp.State = versionGroup.FirstOrDefault().State;
                    temp.EffectiveDate = versionGroup.FirstOrDefault().EffectiveDate?.Date;
                    temp.ChangeDescription = versionGroup.FirstOrDefault().ChangeReason;
                    temp.Id = versionGroup.FirstOrDefault().Id;
                    temp.VersionNumber = versionIlas.Count - i;
                    temp.ChangedBy = versionGroup.FirstOrDefault().ModifiedBy;
                    temp.ILANumber = versionGroup.FirstOrDefault().Number;
                    temp.ILATitle = versionGroup.FirstOrDefault().Name;

                    versionModel.Add(temp);
                }

                return versionModel;
            }
            else
            {
                List<Version_ILAModel> versionModel = new List<Version_ILAModel>();
                foreach (var version in versions)
                {
                    Version_ILAModel temp = new Version_ILAModel();
                    temp.UserName = version.CreatedBy;
                    temp.State = version.State;
                    temp.EffectiveDate = version.EffectiveDate?.Date;
                    temp.ChangeDescription = version.ChangeReason;
                    temp.Id = version.Id;
                    temp.VersionNumber = version.VersionNumber;
                    temp.ChangedBy = version.ModifiedBy;
                    temp.ILANumber = version.Number;
                    temp.ILATitle = version.Name;

                    versionModel.Add(temp);
                }
                return versionModel;
            }
        }


        public async Task<List<ILAStatDataVM>> GetPublishedILAs()
        {
            var ilas = await _ilaService.GetAllILAsWithDeliveryMethodAsync("Published");
            return ilas.Select(x => new ILAStatDataVM(x.Number,x.Name,x.NickName,x.DeliveryMethod?.Name)).OrderBy(m=>m.Name).ToList();
        }

        public async Task<bool> CheckIsProviderNercAsync(int id)
        {
            var ila = await _ilaService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (ila == null)
            {
                throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
            }
            else
            {
                var isNerc = await _providerDomainService.FindQuery(x => x.Id == ila.ProviderId).Select(s => s.IsNERC).FirstOrDefaultAsync();
                return isNerc;
            }
        }


        public async Task<List<ILAProviderVM>> GetByProviderId(int providerId, bool activeOnly)
        {
            var ilas = await _ilaService.FindQueryWithIncludeAsync(x => x.ProviderId == providerId && (!activeOnly || x.Active == true), new string[] { "Provider", "ILA_Topic_Links", "DeliveryMethod" }, true).ToListAsync();
            var listIla = new List<ILAProviderVM>();
            foreach (var ila in ilas)
            {
                var ilaCertificates = (await _ilaCertificationLinkService.FindAsync(x => x.ILAId == ila.Id)).ToList();

                var latestCertificationLink = ilaCertificates.OrderByDescending(a => a.CreatedDate).FirstOrDefault();

                var classSchedules = (await _classScheduleDomainService.FindWithIncludeAsync(x => x.ILAID == ila.Id, new string[] { "ClassSchedule_Employee" })).ToList();

                var classScheduleEmployees = classSchedules.SelectMany(x => x.ClassSchedule_Employee).Where(m => m.IsComplete).ToList();

                var ilaVm = new ILAProviderVM()
                {
                    Id = ila.Id,
                    ProviderId = ila.ProviderId,
                    Number = ila.Number,
                    Name = ila.Name,
                    NickName = ila.NickName,
                    ProviderName = ila.Provider?.Name,
                    TopicIds = ila.ILA_Topic_Links.Select(m => m.ILATopicId).ToList(),
                    TotalHours = latestCertificationLink?.CEHHours,
                    ClassScheduleEmpCount = classScheduleEmployees.Count,
                    DeliveryMethodName = ila.DeliveryMethod?.Name,
                    Image = ila.Image,
                    IsPublished = ila.IsPublished,
                    Active = ila.Active,
                    IsSelfPaced = ila.IsSelfPaced,
                    ClassSize = ila.ClassSize,
                    UseForEMP = ila.UseForEMP,
                    IsPubliclyAvailable = ila.IsPubliclyAvailable,
                    IsNerc = ila.Provider?.IsNERC
                };

                listIla.Add(ilaVm);
            }
            return listIla.OrderBy(o => o.Number).ThenBy(n => n.Name).ToList();
        }

        public async Task<List<ILAProviderVM>> GetByTopicId(int topicId)
        {
            var ilas = await _ilaService.FindQueryWithIncludeAsync(x => x.ILA_Topic_Links.Any(a => a.ILATopicId == topicId), new string[] { "Provider", "DeliveryMethod", "ILA_Topic_Links" }, true).ToListAsync();
            var listIla = new List<ILAProviderVM>();
            foreach (var ila in ilas)
            {
                var ilaCertificates = (await _ilaCertificationLinkService.FindAsync(x => x.ILAId == ila.Id)).ToList();

                var latestCertificationLink = ilaCertificates.OrderByDescending(a => a.CreatedDate).FirstOrDefault();

                var classSchedules = (await _classScheduleDomainService.FindWithIncludeAsync(x => x.ILAID == ila.Id, new string[] { "ClassSchedule_Employee" })).ToList();

                var classScheduleEmployees = classSchedules.SelectMany(x => x.ClassSchedule_Employee).Where(m => m.IsComplete).ToList();

                var ilaVm = new ILAProviderVM()
                {
                    Id = ila.Id,
                    ProviderId = ila.ProviderId,
                    Number = ila.Number,
                    Name = ila.Name,
                    NickName = ila.NickName,
                    ProviderName = ila.Provider?.Name,
                    TopicIds = ila.ILA_Topic_Links.Select(m => m.ILATopicId).ToList(),
                    TotalHours = latestCertificationLink?.CEHHours,
                    ClassScheduleEmpCount = classScheduleEmployees.Count,
                    DeliveryMethodName = ila.DeliveryMethod?.Name,
                    Image = ila.Image,
                    IsPublished = ila.IsPublished,
                    Active = ila.Active,
                    IsSelfPaced = ila.IsSelfPaced,
                    ClassSize = ila.ClassSize,
                    UseForEMP = ila.UseForEMP
                };

                listIla.Add(ilaVm);
            }
            return listIla.OrderBy(o => o.Number).ThenBy(n => n.Name).ToList();
        }

        public async Task<ILA> GetAsync(int id)
        {
            var obj = await _ilaService.GetWithProviderTopicDeliveryMethodAsync(id);
            var certifications = await _certificationService.GetByListOfIDsWithSubRequirementsAsync(obj.ILACertificationLinks.Select(r => r.CertificationId).Distinct().ToList());

            foreach (var certLink in obj.ILACertificationLinks)
            {
                certLink.Certification = certifications.Where(r => r.Id == certLink.CertificationId).First();
            }

            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<List<CollaboratorInvitation>> GetCollaboratorsAsync(int id)
        {
            var result = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILACollaborators) });
            List<CollaboratorInvitation> linked_list = new List<CollaboratorInvitation>();
            linked_list.AddRange(result.ILACollaborators.Where(x => x.ILAId == id).Select(x => x.CollaboratorInvitation));
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmployeeOperations.Read).Result.Succeeded).ToList();
            return linked_list;
        }

        public async Task<List<AssessmentTool>> GetLinkedAssessmentToolsAsync(int id)
        {
            var result = await _ilaService.GetAsync(id);
            List<AssessmentTool> linked_list = new List<AssessmentTool>();
            linked_list.AddRange(result.ILA_AssessmentTool_Links.Where(x => x.ILAId == id).Select(x => x.AssessmentTool));
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linked_list;
        }

        public async Task<List<EnablingObjective>> GetLinkedEnablingObjectivesAsync(int id)
        {
            // var result = await _ilaService.GetAsync(id);
            var result = await _enablingObjective_Link_Service.FindQuery(x => x.ILAId == id).ToListAsync();
            List<EnablingObjective> linked_list = new List<EnablingObjective>();
            List<int> eoIds = result.Select(s => s.EnablingObjectiveId).Distinct().ToList();

            linked_list = await _enablingObjectiveDomainService.GetEnablingObjectivesByEOIDs(eoIds);

            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EnablingObjectiveOperations.Read).Result.Succeeded).ToList();
            return linked_list.OrderBy(x => x.Number).ThenBy(x => x.Description).ToList();
        }

        public async Task<List<EnablingObjective>> GetLinkedEnablingObjectivesByIlaIdsAsync(List<int> ids)
        {
            var linkedEnablingObjectives = (await _enablingObjective_Link_Service.FindAsync(link => ids.Contains(link.ILAId))).ToList();
            List<int> eoIds = linkedEnablingObjectives.Select(s => s.EnablingObjectiveId).Distinct().ToList();
            var linkedList = await _enablingObjectiveDomainService.GetEnablingObjectivesByEOIDs(eoIds);
            linkedList = linkedList.Where(eo => eo.Active).ToList();
            linkedList = linkedList.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EnablingObjectiveOperations.Read).Result.Succeeded).ToList();
            return linkedList.OrderBy(eo => eo.FullNumber, new AlphaNumericSortHelper()).ThenBy(x => x.Description).ToList();
        }

        public async Task<object> GetLinkedSchedulingClasses(int? id, int empId, int idpId)
        {
            var ila = new ILA();
            var toReturn = new List<IDPClass>();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Read); ;
            if (authorization.Succeeded)
            {
                var classSchedules = await _classScheduleDomainService.FindQueryWithIncludeAsync(x => x.ILAID == id, new string[] { "Location", "Instructor" }).Select(x => new ClassSchedule
                {
                    Id = x.Id,
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,
                    Location = new Location { LocName = x.Location.LocName },
                    Instructor = x.Instructor,
                }).ToListAsync();
                foreach (var item in classSchedules)
                {
                    var IsEnrolled = (await _classScheduleEmployeeService.FindAsync(x => x.EmployeeId == empId && x.IsEnrolled == true && x.ClassScheduleId == item.Id)).FirstOrDefault() == null ? false : true;
                    toReturn.Add(new IDPClass
                    {
                        classScheduleId = item.Id,
                        startDate = item.StartDateTime,
                        endDate = item.EndDateTime,
                        location = item.Location?.LocName ?? String.Empty,
                        IsEnrolled = IsEnrolled,
                        InstructorName = item.Instructor?.InstructorName ?? String.Empty
                    });
                }


                return toReturn.ToList();
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }


        }
        public async Task<object> EnrollEmployee(ILAEmployeeEnrollOption options)
        {

            var ila = new ILA();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
            if (authorization.Succeeded)
            {
                var result = await _classScheduleService.LinkEmployee(options.classScheduleId, new ClassSchedule_EmployeeCreateOptions
                {
                    classScheduleId = options.classScheduleId,
                    employeeIds = new[] { options.empId },
                    PlannedDate = options.plannedDate
                });
                await _classScheduleService.EnrollStudentAsync(new ClassScheduleEnrollOptions
                {
                    EmployeeId = options.empId,
                    ClassId = options.classScheduleId,
                    PlannedDate = options.plannedDate
                },false);
                var obj = (await _classScheduleEmployeeService.FindAsync(x => x.EmployeeId == options.empId && x.ClassScheduleId == options.classScheduleId)).FirstOrDefault();
                return obj;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }


        }
        public async Task<object> UnEnrollEmployee(int ilaId, int empId)
        {
            var ila = new ILA();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Delete); ;
            if (authorization.Succeeded)
            {

                var ilaToRemoveFrom = await _ilaService.GetWithIncludeAsync(ilaId, new string[] { "ClassSchedules" });
                foreach (var item in ilaToRemoveFrom?.ClassSchedules)
                {
                    await _classScheduleService.UnlinkEmployee(item.Id, new int[] { empId });

                }

                return new { message = "Employee UnEnrolled  successfully from ILA" };


            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }

        }
        public async Task<List<EnablingObjective_Category>> GetLinkedEOWithCategories(int id)
        {
            var result = await _enablingObjectiveService.GetAsync();
            var linkedEO = await _ilaEnablingObjectiveLinkService.AllAsync();
            var linked = linkedEO.Where(x => x.ILAId == id).ToList();
            foreach (var cat in result)
            {
                foreach (var subCat in cat.EnablingObjective_SubCategories)
                {
                    subCat.EnablingObjectives = subCat.EnablingObjectives.Where(x => linked.Any(i => i.EnablingObjectiveId == x.Id)).ToList();
                    foreach (var topic in subCat.EnablingObjective_Topics)
                    {
                        topic.EnablingObjectives = topic.EnablingObjectives.Where(x => linked.Any(i => i.EnablingObjectiveId == x.Id)).ToList();
                    }
                }
            }

            return result;
        }

        public async Task<List<NercStandard>> GetLinkedNercStandardAsync(int id)
        {
            //var result = await _ilaService.GetAsync(id);
            var result = await _ilaNercStandard_link_service.FindWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(NercStandard) });
            List<NercStandard> linked_list = new List<NercStandard>();
            linked_list.AddRange(result.Select(x => x.NercStandard));
            linked_list = linked_list.Distinct().ToList();
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linked_list;
        }

        public async Task<List<Position>> GetLinkedPositionAsync(int id)
        {
            // var result = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_Position_Links) });
            var result = await _position_Link_Service.FindWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(Position) });
            List<Position> linkedPositions = new List<Position>();
            linkedPositions.AddRange(result.Select(x => x.Position));
            linkedPositions = linkedPositions.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, PositionOperations.Read).Result.Succeeded).ToList();
            return linkedPositions;
        }

        public async Task<List<ILA>> GetLinkedPreRequisitesAsync(int id)
        {
            var result = await _ilaService.GetAsync(id);
            List<ILA> linked_list = new List<ILA>();
            linked_list.AddRange(result.ILA_Procedure_Links.Where(x => x.ILAId == id).Select(x => x.ILA));
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linked_list;
        }

        public async Task<List<Procedure>> GetLinkedProceduresAsync(int id)
        {
            var result = await _ila_procedure_linkService.FindWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(Procedure) });
            List<Procedure> linkedPositions = new List<Procedure>();
            linkedPositions.AddRange(result.Select(x => x.Procedure));
            linkedPositions = linkedPositions.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ProcedureOperations.Read).Result.Succeeded).ToList();
            return linkedPositions;

            /* var result = await _ilaService.GetAsync(id);
             List<Procedure> linked_list = new List<Procedure>();
             linked_list.AddRange(result.ILA_Procedure_Links.Where(x => x.ILAId == id).Select(x => x.Procedure));
             linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ProcedureOperations.Read).Result.Succeeded).ToList();
             return linked_list; */
        }

        public async Task<List<RegulatoryRequirement>> GetLinkedRegulatoryRequirementsAsync(int id)
        {
            var result = await _ila_regRequirement_linkService.FindWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(RegulatoryRequirement) });
            List<RegulatoryRequirement> linkedPositions = new List<RegulatoryRequirement>();
            linkedPositions.AddRange(result.Select(x => x.RegulatoryRequirement));
            linkedPositions = linkedPositions.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linkedPositions;

            /*var result = await _ilaService.GetAsync(id);
            List<RegulatoryRequirement> linked_list = new List<RegulatoryRequirement>();
            linked_list.AddRange(result.ILA_RegRequirement_Links.Where(x => x.ILAId == id).Select(x => x.RegulatoryRequirement));
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linked_list;*/
        }

        public async Task<List<SaftyHazard>> GetLinkedSafetyHazardsAsync(int id)
        {
            /* var result = await _ila_safetyHazard_linkService.FindWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(SaftyHazard) });
             List<SaftyHazard> linkedPositions = new List<SaftyHazard>();
             linkedPositions.AddRange(result.Select(x => x.SafetyHazard));
             linkedPositions = linkedPositions.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
             return linkedPositions;*/

            var result = (await _ila_safetyHazard_linkService.FindWithIncludeAsync(x=>x.ILAId==id,new string[] { nameof(_ilaSafetyHazardLink.SafetyHazard) }));
            List<SaftyHazard> linked_list = new List<SaftyHazard>();
            foreach (var sh in result)
            {
                linked_list.Add(sh.SafetyHazard);
            }
            return linked_list;

            /* var result = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_SafetyHazard_Links) });
             List<SaftyHazard> linked_list = new List<SaftyHazard>();
             linked_list.AddRange(result.ILA_SafetyHazard_Links.Where(x => x.ILAId == id).Select(x => x.SafetyHazard));
             linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
             return linked_list;*/
        }

        public async Task<object> GetLinkedSegmentsAsync(int id)
        {
            var result = await _ilaSegmentLinkService.FindQuery(x => x.ILAId == id).OrderBy(o => o.DisplayOrder).ToListAsync();
            List<Segment> linked_list = new List<Segment>();
            foreach (var link in result)
            {
                var segment = await _segmentDomainService.FindQuery(x => x.Id == link.SegmentId).FirstOrDefaultAsync();
                if (segment != null)
                {
                    linked_list.Add(segment);
                }

            }
            //var links = result.Select(x => x.Segment).ToList();
            //linked_list.AddRange(links);

            // linked_list.AddRange(result.ILA_Segment_Links.Where(x => x.ILAId == id).Select(x => x.Segment));
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            var toReturnobj = new List<object>();
            foreach (var link in linked_list)
            {
                var allObjectives = await _seg_objService.FindQuery(x => x.SegmentId == link.Id).ToListAsync();
                var taskObjs = new List<Domain.Entities.Core.Task>();
                var eoObjs = new List<EnablingObjective>();
                var customEoObjs = new List<CustomEnablingObjective>();
                foreach (var obj in allObjectives.OrderBy(x => x.Order))
                {
                    if (obj.TaskId != null)
                    {
                        var task = (await _taskDomainService.FindWithIncludeAsync(x => x.Id == obj.TaskId, new string[] { "SubdutyArea.DutyArea" })).FirstOrDefault();
                        if (task != null)
                        {
                            taskObjs.Add(task);
                        }
                    }
                    if (obj.EnablingObjectiveId != null)
                    {
                        var eo = (await _enablingObjectiveDomainService.FindWithIncludeAsync(x => x.Id == obj.EnablingObjectiveId, new string[] { "EnablingObjective_Category", "EnablingObjective_SubCategory", "EnablingObjective_Topic" })).FirstOrDefault();
                        if (eo != null)
                        {
                            eoObjs.Add(eo);
                        }
                    }
                    if (obj.CustomEOId != null)
                    {
                        var customEo = (await _custom_eo_domainService.FindWithIncludeAsync(x => x.Id == obj.CustomEOId, new string[] { "EnablingObjective_Category", "EnablingObjective_SubCategory", "EnablingObjective_Topic" })).FirstOrDefault();
                        if (customEo != null)
                        {
                            customEoObjs.Add(customEo);
                        }
                    }
                }
                var objectives = taskObjs.Select(x =>
                new
                {
                    number = x.FullNumber.ToString(),
                    type = "Task",
                    description = x.Description
                }).ToList();

                objectives.AddRange(eoObjs.Select(x =>
                new
                {
                    number = x.FullNumber,
                    type = "EO",
                    description = x.Description
                }));

                objectives.AddRange(customEoObjs.Select(x =>
                new
                {
                    number = x.FullNumber,
                    type = "Custom",
                    description = x.Description
                }));
                //objectives?.RemoveAll(x => x.number == null);
                ; toReturnobj.Add(new
                {
                    Id = link.Id,
                    title = link.Title,
                    duration = link.Duration,
                    isPartialCredit = link.IsPartialCredit,
                    isNercStandard = link.IsNercStandard,
                    isNercOperatingTopics = link.IsNercOperatingTopics,
                    isNercSimulation = link.IsNercSimulation,
                    content = link.Content,
                    uploads = link.Uploads,
                    segmentObjective_Link = objectives
                });
            }
            return toReturnobj;
        }

        public async Task<List<TaskWithCountOptions>> GetLinkedTaskObjectivesAsync(int id)
        {
            List<TaskWithCountOptions> taskWithcount = new List<TaskWithCountOptions>();
            var taskLinks = await _taskObjective_link_service.FindQueryWithIncludeAsync(x => x.ILAId == id, new string[] { "Task" }).ToListAsync();
            //foreach (var task in result)
            //{
            //    linked_list.Add(task.Task);
            //}
            foreach (var link in taskLinks)
            {
                var sda = await _subdutyAreaService.FindQuery(x => x.Id == link.Task.SubdutyAreaId).FirstOrDefaultAsync();
                var da = await _dutyAreaService.FindQuery(x => x.Id == sda.DutyAreaId).FirstOrDefaultAsync();
                var withCount = new TaskWithCountOptions();
                withCount.Number = da.Number.ToString() + '.' + sda.SubNumber.ToString() + '.' + link.Task.Number.ToString();
                withCount.DANumber = da.Number;
                withCount.SDANumber = sda.SubNumber;
                withCount.Description = link.Task.Description;
                withCount.Id = link.Task.Id;
                withCount.Active = link.Task.Active;
                withCount.Letter = da.Letter;
                withCount.IsUsedForTQ = link.UseForTQ;
                taskWithcount.Add(withCount);
            }

            // linked_list.AddRange(result.ILA_TaskObjective_Links.Where(x => x.ILAId == id).Select(x => x.Task));
            // linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TaskOperations.Read).Result.Succeeded).ToList();
            return taskWithcount;
        }

        public async Task<List<Domain.Entities.Core.DutyArea>> getDutyAreasForLinkedTasks(int id)
        {
            var dutyAreas = await _dutyAreaService.AllWithIncludeAsync(new string[] { nameof(_dutyArea.SubdutyAreas) });
            var linkedTasks = await _ilaTaskObjectiveLinkService.AllAsync();
            var linked = linkedTasks.Where(x => x.ILAId == id).ToList();

            foreach (var da in dutyAreas)
            {
                foreach (var sd in da.SubdutyAreas)
                {
                    sd.Tasks = sd.Tasks.Where(x => linked.Any(i => i.TaskId == x.Id)).ToList();
                }
            }

            return dutyAreas.ToList();
        }




        public async Task<List<TrainingTopic>> GetLinkedTrainingTopicsAsync(int id)
        {
            var result = await _ilaService.GetAsync(id);
            List<TrainingTopic> linked_list = new List<TrainingTopic>();
            linked_list.AddRange(result.ILA_TrainingTopic_Links.Where(x => x.ILAId == id && x.TrainingTopic.Active == true).Select(x => x.TrainingTopic));
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linked_list;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await _ilaService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);
            
            if (result.Succeeded)
            {
                
                obj.Deactivate();

                var validationResult = await _ilaService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<ILADetailsVM> LinkAssessmentToolAsync(int id, ILA_AssessmentTool_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_AssessmentTool_Links) });
            var assessmentTool = await _assessmentToolService.GetAsync(options.AssessmentToolId);

            if (assessmentTool == null)
            {
                throw new QTDServerException(_localizer["AssessmentToolNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

            var atool_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, assessmentTool, AuthorizationOperations.Read);

            if (ilaResult.Succeeded && atool_Result.Succeeded)
            {
                var ila_at_link = ila.LinkAssessmentTool(assessmentTool);
                ila_at_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                ila_at_link.CreatedDate = DateTime.Now;

                await _ilaService.UpdateAsync(ila);
                return MapILADetailsVMByILA(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<ILADetailsVM> LinkCollaboratorAsync(int id, ILACollaboratorOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILACollaborators) });

            foreach (var cid in options.CollaboratorInviteIds)
            {
                var collaboratorInvitation = await _invitationService.GetAsync(cid);

                if (collaboratorInvitation == null)
                {
                    throw new QTDServerException(_localizer["InvitataionNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var invite_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, collaboratorInvitation, AuthorizationOperations.Read);

                if (ilaResult.Succeeded && invite_Result.Succeeded)
                {
                    var ila_emp_link = ila.LinkCollaborator(collaboratorInvitation);
                    ila_emp_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    ila_emp_link.CreatedDate = DateTime.Now;

                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            ila = await _ilaService.GetAsync(id);
            return MapILADetailsVMByILA(ila);
        }

        public async Task<ILA> LinkEnablingObjectiveAsync(int id, ILA_EnablingObjective_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_EnablingObjective_Links) });
            foreach (var eoId in options.EnablingObjectiveIds)
            {
                var eo = await _enablingObjectiveService.GetAsync(eoId);

                if (eo == null)
                {
                    throw new QTDServerException(_localizer["EnablingObjectiveNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var eo_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Read);

                if (ilaResult.Succeeded && eo_Result.Succeeded)
                {
                    var ila_eo_link = ila.LinkEnablingObjective(eo);
                    ila_eo_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    ila_eo_link.CreatedDate = DateTime.Now;
                    if (options.IsIncludeMetaEO.GetValueOrDefault())
                    {
                        var metaEO = (await _enablingObjectiveDomainService.FindWithIncludeAsync(x => x.Id == eoId && x.isMetaEO, new string[] { "EnablingObjective_MetaEO_Links.EnablingObjective" })).FirstOrDefault();
                        if (metaEO != null)
                        {
                            foreach (var linkedEO in metaEO.EnablingObjective_MetaEO_Links.Where(x => !options.EnablingObjectiveIds.Contains(x.EOID)))
                            {
                                var ila_meta_eo_link = ila.LinkEnablingObjective(linkedEO.EnablingObjective);
                                ila_meta_eo_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                                ila_meta_eo_link.CreatedDate = DateTime.Now;
                            }
                        }
                    }
                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return ila;
        }

        public async System.Threading.Tasks.Task LinkNercStandardAsync(int id, ILA_NercStandard_LinkOptions options)
        {
            var ila = await _ilaService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_ila.ILA_NercStandard_Links) }).FirstOrDefaultAsync();
            foreach (var nerc_data in options.NercStdValues)
            {
                var nerc_ila_link = await _ilaNercStandard_link_service.FindQuery(x => x.ILAId == id && x.StdId == options.StdId && x.NERCStdMemberId == nerc_data.NERCStdMemberId).ToListAsync();
                if (nerc_ila_link.Count < 1)
                {

                    ILA_NercStandard_Link link = new ILA_NercStandard_Link();
                    link.ILAId = id;
                    link.NERCStdMemberId = nerc_data.NERCStdMemberId;
                    link.StdId = options.StdId;
                    link.CreditHoursByStd = nerc_data.CreditHoursByStd;
                    link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    link.CreatedDate = DateTime.Now;
                    await _ilaNercStandard_link_service.AddAsync(link);
                }
                else
                {
                    foreach (var link in nerc_ila_link)
                    {
                        link.CreditHoursByStd = nerc_data.CreditHoursByStd;
                        link.NERCStdMemberId = nerc_data.NERCStdMemberId;
                        link.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                        link.ModifiedDate = DateTime.Now;
                        await _ilaNercStandard_link_service.UpdateAsync(link);
                    }
                }
            }
            //foreach (var ila_nerc_option in options.NercStdValues)
            //{
            //    //var memberId = int.Parse(_hasher.Decode(ila_nerc_option.ey));
            //    var nercStandard = await _nercStandardService.GetAsync(options.StdId);
            //    var nercStandardMember = await _nercStandardMemberService.GetAsync(options.StdId, ila_nerc_option.NERCStdMemberId);

            //    if (nercStandard == null)
            //    {
            //        throw new Exception(message: _localizer["NercNotFound"]);
            //    }

            //    if (nercStandardMember == null)
            //    {
            //        throw new Exception(message: _localizer["NercStandardMemberNotFound"]);
            //    }

            //    var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

            //    var nerc_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, nercStandard, AuthorizationOperations.Read);

            //    var nercStandardMember_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, nercStandardMember, AuthorizationOperations.Read);

            //    if (ilaResult.Succeeded && nerc_Result.Succeeded && nercStandardMember_Result.Succeeded)
            //    {
            //        var ila_nerc_link = ila.LinkNercStandard(nercStandard, nercStandardMember, ila_nerc_option.CreditHoursByStd);
            //        if()

            //        await _ilaService.UpdateAsync(ila);
            //    }
            //    else
            //    {
            //        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //    }
            //}
        }

        public async Task<ILADetailsVM> LinkPositionAsync(int id, ILA_Position_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_Position_Links) });

            foreach (var positionId in options.PositionIds)
            {
                var position = await _positionService.GetAsync(positionId);

                if (position == null)
                {
                    throw new QTDServerException(_localizer["PositionNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var position_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Read);

                if (ilaResult.Succeeded && position_Result.Succeeded)
                {
                    var ila_pos_link = ila.LinkPosition(position);
                    ila_pos_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    ila_pos_link.CreatedDate = DateTime.Now;

                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            ila = await GetAsync(id);
            return MapILADetailsVMByILA(ila);
        }

        public async Task<ILA_PositionChangeCount> UpdateLinkedPositionsAsync(int ilaId, ILA_Position_LinkOptions options)
        {
            ILA_PositionChangeCount ilaPositionChangeCount = new ILA_PositionChangeCount();
            var ila = await _ilaService.GetWithIncludeAsync(ilaId, new string[] { "ILA_Position_Links" });
            var currentPositionLinks = ila.ILA_Position_Links.ToList();
            foreach (var currentPositionLink in currentPositionLinks)
            {
                if (!options.PositionIds.Contains(currentPositionLink.PositionId))
                {
                    currentPositionLink.Delete();
                    currentPositionLink.Modify((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                    ilaPositionChangeCount.PositionsRemoved++;
                }
            }
            foreach (var newPositionId in options.PositionIds)
            {
                if (!currentPositionLinks.Select(x => x.PositionId).Contains(newPositionId))
                {
                    var position = await _positionService.GetAsync(newPositionId);
                    var ila_pos_link = ila.LinkPosition(position);
                    ila_pos_link.Create((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                    ilaPositionChangeCount.PositionsAdded++;
                }
            }
            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
            if (ilaResult.Succeeded)
            {
                await _ilaService.UpdateAsync(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
            ilaPositionChangeCount.ILA = await GetAsync(ilaId);
            return ilaPositionChangeCount;
        }

        public async System.Threading.Tasks.Task LinkPreRequisiteAsync(int id, ILA_PreRequisite_LinkOptions options)
        {
            var preRe_link = (await _preReq_linkService.FindAsync(x => x.ILAId == id).ConfigureAwait(false)).ToList();
            foreach (var item in preRe_link)
            {
                await _preReq_linkService.DeleteAsync(item);
            }

            //var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_PreRequisite_Links) });
            ILA ila = await _ilaService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            //ila.ILA_PreRequisite_Links = await _preReq_linkService.FindQuery(x => x.ILAId == id).ToListAsync();
            if (ila == null)
            {
                throw new BadHttpRequestException("ILANotFoundException");
            }
            else
            {
                //ila.UnlinkPreRequisites();
                //await _ilaService.UpdateAsync(ila);

                foreach (var prereqId in options.PreRequisiteIds)
                {
                    // var prereq = await _ilaService.GetAsync(prereqId);

                    //var prereq = (await _preReq_linkService.FindAsync(x => x.ILAId == id && x.PreRequisiteId == prereqId)).FirstOrDefault();

                    //if (prereq != null)
                    //{
                    //    throw new Exception(message: _localizer["PrerequisitesAlreadyLinked"]);
                    //}
                    var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                    //var prereq_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, prereq, AuthorizationOperations.Read);

                    if (ilaResult.Succeeded)
                    {
                        //var ila_pos_link = ila.LinkPreRequisite(prereq);
                        //ila_pos_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                        //ila_pos_link.CreatedDate = DateTime.Now;

                        //await _ilaService.UpdateAsync(ila);

                        ILA_PreRequisite_Link prereq = new ILA_PreRequisite_Link();
                        prereq.ILAId = id;
                        prereq.PreRequisiteId = prereqId;
                        prereq.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                        prereq.CreatedDate = DateTime.Now;
                        var validationResult = await _preReq_linkService.AddAsync(prereq);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }

                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task LinkProcedureAsync(int id, ILA_Procedure_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_Procedure_Links) });
            ila.UnlinkProcedures();
            foreach (var procedureId in options.ProcedureIds)
            {
                var procedure = await _procedureService.GetAsync(procedureId);

                if (procedure == null)
                {
                    throw new QTDServerException(_localizer["ProcedureNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var position_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, ProcedureOperations.Read);

                if (ilaResult.Succeeded && position_Result.Succeeded)
                {
                    var ila_pos_link = ila.LinkProcedure(procedure);
                    ila_pos_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    ila_pos_link.CreatedDate = DateTime.Now;

                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task LinkRegulatoryRequirementAsync(int id, ILA_RegRequirement_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_RegRequirement_Links) });
            ila.UnlinkRegRequirements();
            foreach (var regRequirementId in options.RegulatoryRequirementIds)
            {
                var regRequirement = await _regulatoryRequirementService.GetAsync(regRequirementId);

                if (regRequirement == null)
                {
                    throw new QTDServerException(_localizer["RegulatoryRequirementNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var position_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, regRequirement, AuthorizationOperations.Read);

                if (ilaResult.Succeeded && position_Result.Succeeded)
                {
                    var ila_pos_link = ila.LinkRegRequirement(regRequirement);
                    ila_pos_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    ila_pos_link.CreatedDate = DateTime.Now;

                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task LinkSafetyHazardAsync(int id, ILA_SafetyHazard_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_SafetyHazard_Links) });
            ila.UnlinkSafetyHazards();

            foreach (var sh_id in options.SafetyHazardIds)
            {
                var sh = await _saftyHazardService.GetAsync(sh_id);

                if (sh == null)
                {
                    throw new QTDServerException(_localizer["SafteyHazardNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var sh_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh, SaftyHazardOperations.Read);

                if (ilaResult.Succeeded && sh_Result.Succeeded)
                {
                    var ila_pos_link = ila.LinkSafetyHazards(sh);
                    ila_pos_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    ila_pos_link.CreatedDate = DateTime.Now;

                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<ILA> LinkSegmentAsync(int id, ILA_Segment_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_Segment_Links) });
            var segment = await _segmentService.GetAsync(options.SegmentId);

            if (segment == null)
            {
                throw new QTDServerException(_localizer["SegmentNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

            var segment_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, segment, AuthorizationOperations.Read);

            if (ilaResult.Succeeded && segment_result.Succeeded)
            {
                var order = 1;
                var lastLink = await _ilaSegmentLinkService.FindQuery(x => x.ILAId == ila.Id).OrderBy(o => o.DisplayOrder).Select(s => s.DisplayOrder).LastOrDefaultAsync();
                if (lastLink != null && lastLink != 0)
                {
                    order = ((int)lastLink) + 1;
                }

                var ila_seg_link = ila.LinkSegment(segment, order);
                ila_seg_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                ila_seg_link.CreatedDate = DateTime.Now;

                await _ilaService.UpdateAsync(ila);
                return ila;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<ILADetailsVM> LinkTrainingTopicAsync(int id, ILA_TrainingTopic_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_TrainingTopic_Links) });
            var trainingTopic = await _trTopicService.GetAsync(options.TrTopicId);

            if (trainingTopic == null)
            {
                throw new QTDServerException(_localizer["TrainingTopicNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

            var trTopic_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingTopic, AuthorizationOperations.Read);

            if (ilaResult.Succeeded && trTopic_result.Succeeded)
            {
                var ila_trTopic_link = ila.LinkTrainingTopic(trainingTopic);
                ila_trTopic_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                ila_trTopic_link.CreatedDate = DateTime.Now;

                await _ilaService.UpdateAsync(ila);
                return MapILADetailsVMByILA(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }
        public async Task<object> GetStudentEvaluationILAData(int id)
        {
            var result = await _student_evaluation_link_Service.FindQueryWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(_ila_studentEvaluation.StudentEvaluationForm) }, true).ToListAsync();
            return result;

        }

        public async System.Threading.Tasks.Task LinkStudentEvaluationILAData(int id, LinkStudentEvaluationIlaModel evaluation)
        {
            var ila = await GetAsync(id);

            var existingLinks = await _student_evaluation_link_Service.FindWithIncludeAsync(x => x.ILAId == id, new string[] { "StudentEvaluationForm" });
            var existingLinksList = existingLinks.ToList();
            var linksExcludingCurrentEvaluation = existingLinksList.Where(x => x.Id != evaluation.ilaStudenEvaluationLinkId);
            if (linksExcludingCurrentEvaluation.Count() > 0)
            {
                foreach (var item in linksExcludingCurrentEvaluation)
                {
                    var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, item, ILA_StudentEvaluation_LinkOperations.Delete);
                    if (authResult.Succeeded)
                    {
                        await _versioningService.VersionILAAsync(ila, "Student Evaluation Link Removed", DateTime.Now, 0);
                        item.Deleted = true;
                        item.RemoveClassScheduleEvalLinks();
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
                await _student_evaluation_link_Service.BulkUpdateAsync(linksExcludingCurrentEvaluation.ToList());
            }

            if (evaluation.ilaStudenEvaluationLinkId != null)
            {
                ILA_StudentEvaluation_Link linkToUpdate = existingLinksList.FirstOrDefault(x => x.Id == evaluation.ilaStudenEvaluationLinkId);

                if (evaluation.isEvalRemove)
                {
                    await _versioningService.VersionILAAsync(ila, "Student Evaluation Link Removed", DateTime.Now, 0);
                    linkToUpdate.Delete();
                    linkToUpdate.RemoveClassScheduleEvalLinks();
                }
                else
                {
                    await _versioningService.VersionILAAsync(ila, "Student Evaluation Link Updated", DateTime.Now, 2);
                    linkToUpdate.studentEvalAudienceID = evaluation.studentEvalAudienceID;
                    linkToUpdate.studentEvalFormID = evaluation.studentEvalFormID;
                    linkToUpdate.studentEvalAvailabilityID = evaluation.studentEvalAvailabilityID;
                    linkToUpdate.isAllQuestionMandatory = evaluation.isAllQuestionMandatory;
                    linkToUpdate.AddClassScheduleEvalLinks();

                }
                var authUpdateResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, linkToUpdate, ILA_StudentEvaluation_LinkOperations.Update);

                if (authUpdateResult.Succeeded)
                {
                    var updateResult = await _student_evaluation_link_Service.UpdateAsync(linkToUpdate);
                    if (!updateResult.IsValid)
                    {
                        throw new QTDServerException("Unable to Update Evaluation Forms");
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                var newLink = new ILA_StudentEvaluation_Link
                {
                    ILAId = id,
                    studentEvalAudienceID = evaluation.studentEvalAudienceID,
                    studentEvalFormID = evaluation.studentEvalFormID,
                    studentEvalAvailabilityID = evaluation.studentEvalAvailabilityID,
                    isAllQuestionMandatory = evaluation.isAllQuestionMandatory,
                };

                var authCreateResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, newLink, ILA_StudentEvaluation_LinkOperations.Create);

                if (authCreateResult.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    await _versioningService.VersionILAAsync(ila, "Student Evaluation Link Created", DateTime.Now, 1);
                    newLink.Create(userName.Id);
                    newLink.AddClassScheduleEvalLinks();
                    var createResult = await _student_evaluation_link_Service.AddAsync(newLink);
                    if (!createResult.IsValid)
                    {
                        throw new QTDServerException("Unable to Save Evaluation Forms");
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task LinkStudentEvaluationAsync(int id, ILA_StudentEvaluation_LinkOptions options)
        {
            var ila = await _ilaService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { "ILA_StudentEvaluation_Links" }).FirstOrDefaultAsync();

            if (ila != null)
            {
                foreach (var eval in options.studentEvalFormIDs)
                {
                    var studentEvalForm = await _studentEvaluationFormService.GetAsync(eval);
                    //var studentEvalAvailability = await _studentEvaluationAvailabilityService.GetAsync(options.studentEvalAvailabilityID);
                    //var studentEvalAudience = await _studentEvaluationAudienecService.GetAsync(options.studentEvalAudienceID);

                    if (studentEvalForm == null)
                    {
                        throw new QTDServerException(_localizer["StudentEvaluationFormNotFound"]);
                    }

                    var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                    var studentEvalForm_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvalForm, AuthorizationOperations.Read);

                    //var studentEvalAvailability_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvalAvailability, AuthorizationOperations.Read);

                    //var studentEvalAudience_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvalAudience, AuthorizationOperations.Read);

                    if (ilaResult.Succeeded && studentEvalForm_result.Succeeded)
                    {
                        var ila_trTopic_link = ila.LinkstudentEvaluation(studentEvalForm, null, null);
                        ila_trTopic_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                        ila_trTopic_link.CreatedDate = DateTime.Now;

                        await _ilaService.UpdateAsync(ila);
                        //return ila;
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }
            //return ila;

        }

        public async System.Threading.Tasks.Task UnlinkAssessmentToolAsync(int id, int assessmentToolId)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_AssessmentTool_Links) });
            var assessmentTool = await _assessmentToolService.GetAsync(assessmentToolId);

            if (assessmentTool == null)
            {
                throw new QTDServerException(_localizer["AssessmentToolNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

            var atool_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, assessmentTool, AuthorizationOperations.Read);

            if (ilaResult.Succeeded && atool_Result.Succeeded)
            {
                ila.UnlinkAssessmentTool(assessmentTool);

                await _ilaService.UpdateAsync(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task UnlinkCollaboratorAsync(int id, ILACollaboratorOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILACollaborators) });

            foreach (var cid in options.CollaboratorInviteIds)
            {
                var collaboratorInvitation = await _invitationService.GetAsync(cid);

                if (collaboratorInvitation == null)
                {
                    throw new QTDServerException(_localizer["InvitationNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var invite_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, collaboratorInvitation, AuthorizationOperations.Read);

                if (ilaResult.Succeeded && invite_Result.Succeeded)
                {
                    ila.UnlinkCollaboratorTool(collaboratorInvitation);
                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkEnablingObjectiveAsync(int id, ILA_EnablingObjective_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_EnablingObjective_Links) });

            foreach (var eoid in options.EnablingObjectiveIds)
            {
                var unlinkEo = ila.ILA_EnablingObjective_Links.FirstOrDefault(r => r.EnablingObjectiveId == eoid);

                if (unlinkEo != null)
                {
                    unlinkEo.Delete();
                    await _ilaEnablingObjectiveLinkService.UpdateAsync(unlinkEo);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkNercStandardAsync(int id, ILA_NercStandard_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_NercStandard_Links) });

            foreach (var ila_nerc_option in options.NercStdValues)
            {
                var nercStandard = await _nercStandardService.GetAsync(options.StdId);
                var nercStandardMember = await _nercStandardMemberService.GetAsync(options.StdId, ila_nerc_option.NERCStdMemberId);

                if (nercStandard == null)
                {
                    throw new QTDServerException(_localizer["NercNotFound"]);
                }

                if (nercStandardMember == null)
                {
                    throw new QTDServerException(_localizer["NercStandardMemberNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var nerc_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, nercStandard, AuthorizationOperations.Read);
                var nercStandardMember_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, nercStandardMember, AuthorizationOperations.Read);

                if (ilaResult.Succeeded && nerc_Result.Succeeded && nercStandardMember_Result.Succeeded)
                {
                    ila.UnlinkNercStandard(nercStandard, nercStandardMember);
                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkPositionAsync(int id, ILA_Position_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_Position_Links) });

            foreach (var posId in options.PositionIds)
            {
                var position = await _positionService.GetAsync(posId);

                if (position == null)
                {
                    throw new QTDServerException(_localizer["PositionNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var position_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Read);

                if (ilaResult.Succeeded && position_Result.Succeeded)
                {
                    ila.UnlinkPosition(position);
                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<ILADetailsVM>> GetPreReqDataAsync(int id)
        {
            var preReqLinks = await _preReq_linkService.FindQuery(x => x.ILAId == id).ToListAsync();
            List<ILADetailsVM> preReqs = new List<ILADetailsVM>();
            foreach (var link in preReqLinks)
            {
                var ila = await _ilaService.FindQuery(x => x.Id == link.PreRequisiteId).FirstOrDefaultAsync();
                if (ila != null)
                {
                    preReqs.Add(MapILADetailsVMByILA(ila));
                }
            }

            return preReqs;
        }

        public async System.Threading.Tasks.Task UnlinkPreRequisiteAsync(int id, ILA_PreRequisite_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new[] { "ILA_PreRequisite_Links" });
            if (ila == null)
            {
                throw new BadHttpRequestException("ILANotFoundException");
            }

            foreach (var preid in options.PreRequisiteIds)
            {
                var pre_result = await _ilaService.GetAsync(preid);

                if (pre_result == null)
                {
                    throw new QTDServerException(_localizer["PrerequisiteNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var pre_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, pre_result, AuthorizationOperations.Read);

                if (ilaResult.Succeeded && pre_Result.Succeeded)
                {
                    ila.UnlinkPreRequisite(pre_result);
                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkProcedureAsync(int id, ILA_Procedure_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_Procedure_Links) });

            foreach (var proid in options.ProcedureIds)
            {
                var pro_result = await _procedureService.GetAsync(proid);

                if (pro_result == null)
                {
                    throw new QTDServerException(_localizer["ProcedureNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var pro_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, pro_result, AuthorizationOperations.Read);

                if (ilaResult.Succeeded && pro_Result.Succeeded)
                {
                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkRegulatoryRequirementAsync(int id, ILA_RegRequirement_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_RegRequirement_Links) });
            ila.UnlinkRegRequirements();

            foreach (var rrid in options.RegulatoryRequirementIds)
            {
                var rr_result = await _regulatoryRequirementService.GetAsync(rrid);

                if (rr_result == null)
                {
                    throw new QTDServerException(_localizer["RegulatoryRequirementNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var rr_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, rr_result, AuthorizationOperations.Read);

                if (ilaResult.Succeeded && rr_Result.Succeeded)
                {
                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkSafetyHazardAsync(int id, ILA_SafetyHazard_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_SafetyHazard_Links) });

            foreach (var shid in options.SafetyHazardIds)
            {
                var sh_result = await _saftyHazardService.GetAsync(shid);

                if (sh_result == null)
                {
                    throw new QTDServerException(_localizer["ProcedureNotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

                var sh_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sh_result, AuthorizationOperations.Read);

                if (ilaResult.Succeeded && sh_Result.Succeeded)
                {
                    ila.UnlinkSafetyHazards(sh_result);
                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkSegmentAsync(int id, int segmentId)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_Segment_Links) });
            var segment = await _segmentService.GetAsync(segmentId);

            if (segment == null)
            {
                throw new QTDServerException(_localizer["SegmentNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

            var segment_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, segment, AuthorizationOperations.Read);

            if (ilaResult.Succeeded && segment_result.Succeeded)
            {
                ila.UnlinkSegment(segment);
                await _ilaService.UpdateAsync(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task UpdateTObjUsedForTQAsync(int id, ILA_TaskObjective_LinkOptions options)
        {
            var links = await _ilaTaskObjectiveLinkService.FindQuery(x => x.ILAId == id).ToListAsync();
            foreach (var link in links)
            {
                if (options.TaskIds.Contains(link.TaskId))
                {
                    link.UseForTQ = true;
                }
                else
                {
                    link.UseForTQ = false;
                }
                var validationResult = await _ilaTaskObjectiveLinkService.UpdateAsync(link);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkTrainingTopicAsync(int id, int trTopicId)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_TrainingTopic_Links) });
            var trainingTopic = await _trTopicService.GetAsync(trTopicId);

            if (trainingTopic == null)
            {
                throw new QTDServerException(_localizer["TrainingTopicNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

            var trTopic_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingTopic, AuthorizationOperations.Read);

            if (ilaResult.Succeeded && trTopic_result.Succeeded)
            {
                ila.UnlinkTrainingTopic(trainingTopic);
                await _ilaService.UpdateAsync(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task UnlinkstudentEvaluationAsync(int id, ILA_StudentEvaluation_LinkOptions options)
        {
            var ila = await GetAsync(id);
            var studentEvaluation = await _studentEvaluationFormService.GetAsync(options.studentEvalFormIDs.First());
            //var studentEvaluationAvailability = await _studentEvaluationAvailabilityService.GetAsync(options.studentEvalAvailabilityID);
            //var studentEvalAudience = await _studentEvaluationAudienecService.GetAsync(options.studentEvalAudienceID);

            if (studentEvaluation == null)
            {
                throw new QTDServerException(_localizer["SstudentEvaluationNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);

            var studentEvaluation_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, AuthorizationOperations.Read);

            //var studentEvaluationAvailability_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluationAvailability, AuthorizationOperations.Read);

            //var studentEvalAudience_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvalAudience, AuthorizationOperations.Read);

            if (ilaResult.Succeeded && studentEvaluation_result.Succeeded)
            {
                ila.UnlinkStudentEvaluation(studentEvaluation, null, null);
                await _ilaService.UpdateAsync(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<ILADetailsVM> UpdateAsync(int id, ILAUpdateOptions options)
        {
            var obj = await _ilaService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                if (!String.IsNullOrEmpty(options.ilaDeliveryMethod))
                {
                    var newMethod = new DeliveryMethod
                    {
                        Name = options.ilaDeliveryMethod,
                        DisplayName = options.ilaDeliveryMethod,
                        IsNerc = false,
                        IsUserDefined = true,
                        IsAvailableForAllIlas = options.isAvailableForAllILA == true ? true : false,
                        CreatorIlaId = id
                    };
                    var DeliverySaveResult = await _deliveryMethodService.AddAsync(newMethod);
                    if (DeliverySaveResult.IsValid)
                    {
                        options.DeliveryMethodId = newMethod.Id;
                    }
                }

                var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
                string input = options.Image;
                string pathToDisplay = "";
                if (!string.IsNullOrEmpty(input))
                {
                    string[] parts = input.Split(new string[] { "base64," }, StringSplitOptions.None);
                    string actualbase64 = parts[1];
                    string[] subparts = parts[0].Split(new string[] { ";data" }, StringSplitOptions.None);
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + subparts[0];
                    string base64Image = actualbase64;
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    pathToDisplay = "img/" + instanceName + "/" + fileName;
                    string outputPath = _qtdSettings.SaveFilePath + "img\\" + instanceName + "\\";
                    if (!Directory.Exists(outputPath))
                        Directory.CreateDirectory(outputPath);
                    outputPath += fileName;
                    File.WriteAllBytes(outputPath, imageBytes);
                }

                obj.Name = options.Name;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                obj.NickName = options.NickName;
                obj.Number = options.Number;
                obj.DeliveryMethodId = options.DeliveryMethodId;
                obj.ProviderId = options.ProviderId;
                obj.Description = options.Description;
                obj.IsSelfPaced = options.IsSelfPaced;
                obj.Image = string.IsNullOrEmpty(pathToDisplay) ? obj.Image : pathToDisplay;

                var data = (await _ilaService.FindAsync(x => x.Name == options.Name && x.Number == options.Number)).FirstOrDefault();
                if (data != null && data.Id != obj.Id)
                {
                    throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
                }

                var validationResult = await _ilaService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return MapILADetailsVMByILA(obj);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task UploadFile(int id, ILAUploadOptions file)
        {
            var obj = await _ilaService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);
            if (result.Succeeded)
            {
                foreach (var data in file.uploadFiles)
                {
                    obj.ILA_Uploads.Add(new ILA_Upload
                    {
                        FileAsBase64 = data.FileAsBase64,
                        FileName = data.FileName,
                        FileType = data.FileType,
                        ILAId = id,
                    });
                }

                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                await _ilaService.UpdateAsync(obj);
            }
        }

        public async Task<List<ILAObjectivesVM>> GetAllLinkedObjectivesAsync(int id)
        {
            List<ILAObjectivesVM> objectives = new List<ILAObjectivesVM>();

            var eoLinks = await _ilaEnablingObjectiveLinkService.FindQueryWithIncludeAsync(x => x.ILAId == id, new string[] { "EnablingObjective" }, true).ToListAsync();
            foreach (var eos in eoLinks)
            {
                var eoNum = eos.EnablingObjective?.Number;
                var orderNum = eos.EnablingObjective?.Number;
                if (eos.EnablingObjective.Number.Contains("."))
                {
                    orderNum = eos.EnablingObjective.Number.Split(".")[3];
                }
                else
                {
                    if (eos?.EnablingObjective?.TopicId == null)
                    {
                        var subCat = await _eo_subCatService.FindQuery(x => x.Id == eos.EnablingObjective.SubCategoryId).FirstOrDefaultAsync();
                        if (subCat != null)
                        {
                            var cat = await _eo_CatService.FindQuery(x => x.Id == eos.EnablingObjective.CategoryId).FirstOrDefaultAsync();
                            if (cat != null)
                            {
                                eoNum = cat.Number.ToString() + "." + subCat.Number.ToString() + ".0." + eos.EnablingObjective.Number;
                            }
                        }
                    }
                    else
                    {
                        var topic = await _eo_topicService.FindQuery(x => x.Id == eos.EnablingObjective.TopicId).FirstOrDefaultAsync();
                        if (topic != null)
                        {
                            var subCat = await _eo_subCatService.FindQuery(x => x.Id == eos.EnablingObjective.SubCategoryId).FirstOrDefaultAsync();
                            if (subCat != null)
                            {
                                var cat = await _eo_CatService.FindQuery(x => x.Id == eos.EnablingObjective.CategoryId).FirstOrDefaultAsync();
                                if (cat != null)
                                {
                                    eoNum = cat.Number.ToString() + "." + subCat.Number.ToString() + "." + (topic?.Number.ToString() ?? "0") + "." + eos.EnablingObjective.Number;
                                }
                            }
                        }
                    }
                }
                objectives.Add(new ILAObjectivesVM { Description = eos.EnablingObjective.Description, Number = eoNum, Type = "EO", Id = eos.EnablingObjective.Id, OrderNumber = orderNum.ToString(), Order = eos.ILAObjectiveOrder });
            }

            var customLinks = await _custom_eo_link_Service.FindQueryWithIncludeAsync(x => x.ILAId == id, new string[] { "CustomEnablingObjective" }).ToListAsync();
            foreach (var customLink in customLinks)
            {
                customLink.CustomEnablingObjective.EnablingObjective_Category = await _eo_CatService.FindQuery(x => x.Id == customLink.CustomEnablingObjective.EO_CatId).FirstOrDefaultAsync();
                customLink.CustomEnablingObjective.EnablingObjective_SubCategory = await _eo_subCatService.FindQuery(x => x.Id == customLink.CustomEnablingObjective.EO_SubCatId).FirstOrDefaultAsync();
                customLink.CustomEnablingObjective.EnablingObjective_Topic = await _eo_topicService.FindQuery(x => x.Id == customLink.CustomEnablingObjective.EO_TopicId).FirstOrDefaultAsync();
                objectives.Add(new ILAObjectivesVM { Description = customLink.CustomEnablingObjective.Description, Type = "Custom", Number = customLink.CustomEnablingObjective.FullNumber, Id = customLink.CustomEnablingObjective.Id, OrderNumber = customLink.CustomEnablingObjective.CustomEONumber.ToString(), Order = customLink.ILAObjectiveOrder });
            }


            var taskLinks = await _ilaTaskObjectiveLinkService.FindQueryWithIncludeAsync(x => x.ILAId == id, new string[] { "Task" }).OrderBy(x => x.SequenceNumber).ToListAsync();
            foreach (var task in taskLinks)
            {
                var sda = await _subdutyAreaService.FindQuery(x => x.Id == task.Task.SubdutyAreaId).FirstOrDefaultAsync();
                var da = await _dutyAreaService.FindQuery(x => x.Id == sda.DutyAreaId).FirstOrDefaultAsync();
                objectives.Add(new ILAObjectivesVM { Description = task.Task.Description, Number = da.Letter + da.Number.ToString() + "." + sda.SubNumber.ToString() + "." + task.Task.Number.ToString(), Id = task.Task.Id, Type = "Task", OrderNumber = task.Task.Number.ToString(), Order = task.ILAObjectiveOrder });
            }

            return objectives.OrderBy(o => o.OrderNumber).ToList();
        }

        public async Task<List<ILA_Upload>> getUploadedFiles(int id)
        {
            var obj = await _ilaUploadService.FindQuery(x => x.ILAId == id).Select(x => new { x.ILAId, x.FileName, x.FileSize, x.Id, x.FileType }).ToListAsync();
            var uploads = new List<ILA_Upload>();
            int count = 0;
            foreach (var data in obj)
            {
                uploads.Add(new ILA_Upload
                {
                    FileName = data.FileName,
                    ILAId = data.ILAId,
                    FileSize = data.FileSize,
                    FileType = data.FileType,
                });
                uploads[count].Set_Id(data.Id);
                count++;
            }

            return uploads;
        }

        public async Task<ILA_Upload> DownloadFile(int ilaId, int id)
        {
            var obj = _ilaUploadService.FindQuery(x => x.ILAId == ilaId).Where(x => x.Id == id).Select(x => new { x.FileAsBase64, x.FileName });
            var uploads = new List<ILA_Upload>();
            int count = 0;
            foreach (var data in obj)
            {
                uploads.Add(new ILA_Upload
                {
                    FileName = data.FileName,
                    FileAsBase64 = data.FileAsBase64,
                });
                count++;
            }

            return uploads[0];
        }

        public async System.Threading.Tasks.Task AddTrainingPlan(int ilaId, ILAUpdateOptions options)
        {
            var obj = await _ilaService.GetAsync(ilaId);
            if (obj != null)
            {
                obj.TrainingPlan = options.TrainingPlan;
                await _ilaService.UpdateAsync(obj);
            }
        }

        public async System.Threading.Tasks.Task DeleteUploadedFiles(int id, int uploadId)
        {
            var obj = _ilaUploadService.FindQuery(x => x.ILAId == id && x.Id == uploadId).FirstOrDefault();
            obj.Delete();
            await _ilaUploadService.UpdateAsync(obj);
        }

        public async System.Threading.Tasks.Task DeleteUploadedFileFromIwbResources(int uploadId)
        {
            var iwbResource = await _instructorWorkbook_ILADesign_ResourcesDomainService.GetUploadedFileFromIwbResourcesAsync(uploadId);
            if (iwbResource != null)
            {
                iwbResource.Delete();
                await _instructorWorkbook_ILADesign_ResourcesDomainService.UpdateAsync(iwbResource);
            }
        }

        public async Task<ILADetailsVM> LinkNERCAudienceAsync(int id, ILA_NERCAudience_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_NERCAudience_Links) });
            var nta = await _nercTargetAudienceService.GetAsync(options.NERCAudienceID);

            if (nta == null)
            {
                throw new QTDServerException(_localizer["NercNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
            var nerc_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, nta, AuthorizationOperations.Read);

            if (nerc_Result.Succeeded && ilaResult.Succeeded)
            {
                var ila_nerc_link = ila.LinkNERCAudience(nta);
                ila_nerc_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                ila_nerc_link.CreatedDate = DateTime.Now;

                await _ilaService.UpdateAsync(ila);
                return MapILADetailsVMByILA(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task UnlinkNERCAudienceAsync(int id, int ntaId)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_NERCAudience_Links) });
            var nta = await _nercTargetAudienceService.GetAsync(ntaId);

            if (nta == null)
            {
                throw new QTDServerException(_localizer["NercNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
            var nerc_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, nta, AuthorizationOperations.Read);

            if (ilaResult.Succeeded && nerc_Result.Succeeded)
            {
                ila.UnlinkNERCAudience(nta);
                await _ilaService.UpdateAsync(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<NERCTargetAudience>> GetLinkedNERCAudienceAsync(int id)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILA_NERCAudience_Links) });
            List<NERCTargetAudience> linked_list = new List<NERCTargetAudience>();
            linked_list.AddRange(ila.ILA_NERCAudience_Links.Where(x => x.ILAId == id).Select(x => x.NERCTargetAudience));
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linked_list;
        }

        public async Task<ILA> LinkCustomObjectiveAsync(int id, ILACustomObjective_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILACustomObjective_Links) });
            foreach (var customId in options.CustomObjIds)
            {
                var custom_eo = await _custom_eo_Service.GetAsync(customId);

                if (custom_eo == null)
                {
                    throw new QTDServerException(_localizer["CustomEONotFound"]);
                }

                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
                var nerc_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, custom_eo, AuthorizationOperations.Read);

                if (nerc_Result.Succeeded && ilaResult.Succeeded)
                {
                    var ila_cust_eo_link = ila.LinkCustomEnablingObjective(custom_eo);
                    ila_cust_eo_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    ila_cust_eo_link.CreatedDate = DateTime.Now;

                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return ila;
        }

        public async System.Threading.Tasks.Task UnlinkCustomObjectiveAsync(int id, ILACustomObjective_LinkOptions options)
        {
            var ila = await _ilaService.GetWithIncludeAsync(id, new string[] { nameof(_ila.ILACustomObjective_Links) });
            foreach (var custom_eoId in options.CustomObjIds)
            {
                var unlinkCustomEo = ila.ILACustomObjective_Links.FirstOrDefault(r => r.CustomObjId == custom_eoId);

                if (unlinkCustomEo != null)
                {
                    unlinkCustomEo.Delete();
                    await _custom_eo_link_Service.UpdateAsync(unlinkCustomEo);
                }
            }
        }

        public async Task<List<CustomEnablingObjective>> GetLinkedCustomObjectivesAsync(int id)
        {
            var result = await _custom_eo_link_Service.FindWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(CustomEnablingObjective) });
            List<CustomEnablingObjective> linkedPositions = new List<CustomEnablingObjective>();
            linkedPositions.AddRange(result.Select(x => x.CustomEnablingObjective));
            linkedPositions = linkedPositions.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linkedPositions;
        }

        public async Task<List<CustomEOWithNumber>> GetLinkedCustomEOWithNumberAsync(int id)
        {
            var links = await _custom_eo_link_Service.FindQuery(x => x.ILAId == id).ToListAsync();
            List<CustomEOWithNumber> toReturn = new List<CustomEOWithNumber>();
            foreach (var link in links)
            {
                var custEO = await _custom_eo_domainService.FindQuery(x => x.Id == link.CustomObjId).FirstOrDefaultAsync();
                if (custEO != null)
                {
                    custEO.EnablingObjective_Category = await _eo_CatService.FindQuery(x => x.Id == custEO.EO_CatId).FirstOrDefaultAsync();
                    custEO.EnablingObjective_SubCategory = await _eo_subCatService.FindQuery(x => x.Id == custEO.EO_SubCatId).FirstOrDefaultAsync();
                    custEO.EnablingObjective_Topic = await _eo_topicService.FindQuery(x => x.Id == custEO.EO_TopicId).FirstOrDefaultAsync();

                    var toRetCustEO = new CustomEOWithNumber();
                    toRetCustEO.Number = custEO.FullNumber;
                    toRetCustEO.Description = custEO.Description;
                    toRetCustEO.Id = custEO.Id;
                    toReturn.Add(toRetCustEO);
                }
            }
            return toReturn;
        }

        public async Task<ILAStatsVM> GetILAStatCounts()
        {
            var stats = new ILAStatsVM
            {
                Providers = await _providerDomainService.GetCount(x => x.Active == true),
                Topics = await _topicDomainService.GetCount(x => x.Active == true),
                ActiveILAs = await _ilaService.GetCount(x => x.Active == true),
                DraftILAs = await _ilaService.GetCount(x => x.IsPublished == false && x.Active == true),
                PublishedILAs = await _ilaService.GetCount(x => x.IsPublished == true && x.Active == true),
                UnlinkedTopicILAs = await _ilaService.GetCount(x => !x.ILA_Topic_Links.Any() && x.Active == true),
            };

            return stats;
        }

        public async System.Threading.Tasks.Task ChangeProviderAsync(int id, int newProviderId)
        {
            var ila = await _ilaService.GetAsync(id);
            var provider = await _providerDomainService.GetAsync(newProviderId);

            if (provider == null)
            {
                throw new QTDServerException(_localizer["ProviderNotFound"]);
            }

            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
            var provider_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, AuthorizationOperations.Read);

            if (ilaResult.Succeeded && provider_Result.Succeeded)
            {
                ila.ProviderId = newProviderId;
                var validationResult = await _ilaService.UpdateAsync(ila);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<StudentEvaluation>> GetLinkedStudentEvaluationAsync(int id)
        {
            var result = await _student_evaluation_link_Service.FindQueryWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(_ila_studentEvaluation.StudentEvaluationForm), "StudentEvaluationForm.ClassSchedule_StudentEvaluations_Links.ClassSchedule" }, true).ToListAsync();
            List<StudentEvaluation> linkedForms = new List<StudentEvaluation>();
            linkedForms.AddRange(result.Select(x => x.StudentEvaluationForm));
            linkedForms = linkedForms.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linkedForms;
        }

        public async System.Threading.Tasks.Task UnlinkAllEvaluationsAsync(int id)
        {
            var links = await _ila_eval_linkService.FindQuery(x => x.ILAId == id).ToListAsync();
            for (int i = 0; i < links.Count; i++)
            {
                await _ila_eval_linkService.DeleteAsync(links[i]);
            }
        }

        /* CBT Implementations Start */

        public async Task<CBT> GetCBTSettingAsync(int id)
        {
            var cbtSettings = await _cbtService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cbtSettings != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, cbtSettings, CBTOperations.Read);
                if (authResult.Succeeded)
                {
                    return cbtSettings;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EvaluationSettingNotFoundException"]);
            }
        }

        public async Task<CBT> GetCBTSettingForILAAsync(int ilaId, bool current)
        {
            var cbtSettings = (await _cbtService.FindWithIncludeAsync(x => x.ILAId == ilaId && (!current || x.Active), new string[] { "ScormUploads" })).FirstOrDefault();
            if (cbtSettings != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, cbtSettings, CBTOperations.Read);
                if (authResult.Succeeded)
                {
                    return cbtSettings;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<CBT>> GetCBTScormFormsForILAAsync(int ilaId, bool current)
        {
            var cbtSettings = (await _cbtService.FindWithIncludeAsync(x => x.ILAId == ilaId, new string[] { "ScormUploads.CBT_ScormRegistration.ClassScheduleEmployee" }));
            return cbtSettings.ToList();
        }


        public async Task<CBT> CreateCBTSettingAsync(int ilaId, CBTCreateOptions options)
        {
            var cbt = new CBT(ilaId, options.CBTLearningContractInstructions, options.DueDateAmount, options.EmpSettingsReleaseTypeId, options.Availablity);
            var ila = await _ilaService.FindQuery(x => x.Id == cbt.ILAId).FirstOrDefaultAsync();

            var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, cbt, CBTOperations.Create);
            if (authResult.Succeeded)
            {
                await UpdateCbtRequired(ila, options.CBTRequiredForCource);
                cbt.Create(_httpContextAccessor.HttpContext.User.Identity.Name);

                var validationResult = await _cbtService.AddAsync(cbt);
                if (validationResult.IsValid)
                {
                    return cbt;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }
        }

        public async System.Threading.Tasks.Task UpdateCbtRequired(ILA ila, bool cbtRequiredForCource)
        {
            if (ila != null)
            {
                ila.SetCBTRequiredForCourse(cbtRequiredForCource);
                await _ilaService.UpdateAsync(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["ILANotFoundException"]);
            }
        }

        public async Task<CBT> UpdateCBTSettingAsync(int id, CBTUpdateOptions options)
        {
            var cbt = await _cbtService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var ila = await _ilaService.FindQuery(x => x.Id == cbt.ILAId).FirstOrDefaultAsync();

            if (cbt != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, cbt, CBTOperations.Update);
                if (authResult.Succeeded)
                {
                    if (options.CBTRequiredForCource != ila.CBTRequiredForCourse)
                    {
                        await UpdateCbtRequired(ila, options.CBTRequiredForCource);
                    }
                    if (options.CBTRequiredForCource)
                    {
                        if (options.ChangeDueDate)
                        {
                            cbt.SetDueDate(options.DueDateAmount, options.EmpSettingsReleaseTypeId);
                            cbt.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);
                        }

                        if (options.Availablity != CBTAvailablity.NotSet)
                        {
                            cbt.SetAvailability(options.Availablity);
                            cbt.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);
                        }

                        if (!String.IsNullOrEmpty(options.CBTLearningContractInstructions))
                        {
                            cbt.setCBTLearningContractInstructions(options.CBTLearningContractInstructions);
                            cbt.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);
                        }
                    }
                    var validationResult = await _cbtService.UpdateAsync(cbt);
                    if (validationResult.IsValid)
                    {
                        return cbt;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }

            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["CBTReleaseEMPSettingNotFoundException"]);
            }
        }

        /* CBT Implementations End */

        /* Scorm Upload Implementations STARTS */
        public async System.Threading.Tasks.Task<CBT_ScormUpload> AttachScormUploadAsync(int cbtId, IFormFile file)
        {
            var cbt = await _cbtService.GetWithIncludeAsync(cbtId, new[] { "ScormUploads" });

            if (cbt != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, cbt, CBTOperations.Update);
                if (authResult.Succeeded)
                {
                    cbt.DisconnectAllScormUploads();
                    await _cbtService.UpdateAsync(cbt);

                    var newScormUpload = new CBT_ScormUpload(cbtId, file.FileName);
                    cbt.AttachScormUpload(newScormUpload);
                    var validationResult = await _cbtService.UpdateAsync(cbt);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }

                    var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
                    var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);

                    // Upload to Scorm
                    CreateUploadImportCourseSchema uploadSchema = new CreateUploadImportCourseSchema(newScormUpload.Id.ToString(), file, file.Name, instanceSettings.ScormTenant);

                    var response = await _scormHttpClient.CreateUploadAndImportCourseJobAsync(uploadSchema);
                    if (response == null)
                    {
                        throw new QTDServerException(_localizer["ScormServerError"]);
                    }

                    newScormUpload.MarkAsUploaded();
                    validationResult = await _cbtService.UpdateAsync(cbt);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }

                    return newScormUpload;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["CBTReleaseEMPSettingNotFoundException"]);
            }
        }

        public async System.Threading.Tasks.Task DisconnectScormUploadAsync(int scormId)
        {
            var scormUpload = await _scormUploadService.GetAsync(scormId);
            if (scormUpload != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, scormUpload, ScormUploadOperations.Update);
                if (result.Succeeded)
                {
                    var cbt = await _cbtService.GetWithIncludeAsync(scormUpload.CbtId, new[] { "ScormUploads" });

                    if (cbt != null)
                    {
                        cbt.DisconnectAllScormUploads();
                        await _cbtService.UpdateAsync(cbt);
                    }
                    var validationResult = await _scormUploadService.UpdateAsync(scormUpload);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<List<CBT_ScormUpload>> GetAllScormUploadAsync(int cbtId, bool current)
        {
            var scorms = await _scormUploadService.GetAllByCBTIdAsync(cbtId, current);
            return scorms;
        }

        public async Task<CBT_ScormUpload> GetCurrentScormUploadAsync(int scormId)
        {
            var currentScorm = await _scormUploadService.GetAsync(scormId);
            return currentScorm;
        }

        public async Task<CBT_ScormRegistration> RegisterEmployeeToCbtAsync(int classScheduleId, int employeeId)
        {
            var classScheduleEmployee = await _classScheduleEmployeeService.GetEmployeeForClassScheduleAsync(classScheduleId, employeeId);

            if (classScheduleEmployee == null) throw new KeyNotFoundException(_localizer["CBTNotFoundException"]);

            var scormRegistration = classScheduleEmployee.ScormRegistrations.FirstOrDefault(r => r.Active);

            //why is ILAId nullable?
            var scormUpload = await _scormUploadService.GetByIlaIdAsync(classScheduleEmployee.ClassSchedule.ILAID ?? -1);

            if (scormRegistration != null)
            {
                if (!String.IsNullOrEmpty(scormRegistration.LaunchLink)) return scormRegistration;
            }
            else
            {
                scormRegistration = new CBT_ScormRegistration(scormUpload.Id, classScheduleEmployee.Id);
            }

            var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, scormRegistration, CBT_ScormRegistrationOperations.Create);

            if (authResult.Succeeded)
            {
                if (scormRegistration.Id == 0)
                {
                    var validationResult = await _cbt_ScormRegistrationService.AddAsync(scormRegistration);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }

                var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
                var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
                CreateRegistrationWithLaunchLinkSchema request = new CreateRegistrationWithLaunchLinkSchema(scormRegistration.CBTScormUploadId, classScheduleEmployee.EmployeeId, classScheduleEmployee.Employee.Person.FirstName, classScheduleEmployee.Employee.Person.LastName, classScheduleEmployee.Id);

                var response = await _scormHttpClient.CreateRegistrationWithLaunchLinkAsync(request, instanceSettings.ScormTenant);
                if (response == null)
                {
                    throw new QTDServerException(_localizer["ScormServerError"]);
                }
                else
                {
                    scormRegistration.Register(response.LaunchLink);

                    var validationResult = await _cbt_ScormRegistrationService.UpdateAsync(scormRegistration);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }

                return scormRegistration;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }
        }

        /* Scorm Upload  Implementations End */

        public async Task<List<Person>> GetStudentsForILAAsync(int ilaId)
        {
            //var students = await _personService.AllAsync();
            var ilaWithStudents = await _ilaService.GetWithIncludeAsync(ilaId, new[] { "ClassSchedules.ClassSchedule_Employee.Employee.Person" });

            //TODO make this a not found
            if (ilaWithStudents == null) throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);

            var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilaWithStudents, ILAOperations.Read);
            if (authResult.Succeeded)
            {
                var persons = ilaWithStudents.ClassSchedules.SelectMany(r => r.ClassSchedule_Employee).Select(r => r.Employee).Select(r => r.Person).Distinct().ToList();
                persons.ForEach(p => p.Employee.ClassSchedule_Employee = null);

                return persons;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }
        }

        /* Eval Settings Implementations Start */

        public async Task<EvaluationReleaseEMPSettings> GetEvalSettingAsync(int id)
        {
            var evalSetting = await _eval_release_settingService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (evalSetting != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, evalSetting, EvaluationReleaseEMPSettingsOperations.Read);
                if (authResult.Succeeded)
                {
                    return evalSetting;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EvaluationSettingNotFoundException"]);
            }
        }

        public async Task<EvaluationReleaseEMPSettings> GetEvalSettingForILAAsync(int ilaId)
        {
            var evalSettings = await _eval_release_settingService.FindQuery(x => x.ILAId == ilaId).FirstOrDefaultAsync();
            if (evalSettings != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, evalSettings, EvaluationReleaseEMPSettingsOperations.Read);
                if (authResult.Succeeded)
                {
                    return evalSettings;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<EvaluationReleaseEMPSettings> CreateEvalSettingAsync(EvaluationReleaseEMPSettingCreateOptions options)
        {
            var exists = (await _eval_release_settingService.FindAsync(x => x.ILAId == options.ILAId)).FirstOrDefault();
            if (exists == null)
            {
                var evalSetting = new EvaluationReleaseEMPSettings(options.ILAId, options.EvaluationUsedToDeployStudentEvaluation, options.EvaluationRequired, options.EvaluationAvailableOnStartDate,
                                                               options.EvaluationAvailableOnEndDate, options.FinalGradeRequired, options.ReleaseOnSpecificTimeAfterClassEndDate,
                                                               options.ReleaseAfterEndTime, options.ReleasePrior, options.ReleaseAfterGradeAssigned, options.EvaluationDueDate, options.EmpSettingsReleaseTypeId);

                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, evalSetting, EvaluationReleaseEMPSettingsOperations.Create);
                if (authResult.Succeeded)
                {
                    evalSetting.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    evalSetting.CreatedDate = DateTime.Now;
                    var validationResult = await _eval_release_settingService.AddAsync(evalSetting);
                    if (validationResult.IsValid)
                    {
                        return evalSetting;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                var updateOptions = new EvaluationReleaseEMPSettingUpdateOptions(exists.ILAId, options.EvaluationRequired, options.EvaluationUsedToDeployStudentEvaluation, options.EvaluationAvailableOnStartDate, options.EvaluationAvailableOnEndDate,
                                                                                 options.FinalGradeRequired, options.ReleaseOnSpecificTimeAfterClassEndDate, options.ReleaseAfterEndTime, options.ReleasePrior, options.ReleaseAfterGradeAssigned, options.EvaluationDueDate, options.EmpSettingsReleaseTypeId);
                var evalSetting = await UpdateEvalSettingAsync(exists.Id, updateOptions);
                return evalSetting;
            }
        }

        public async Task<EvaluationReleaseEMPSettings> UpdateEvalSettingAsync(int id, EvaluationReleaseEMPSettingUpdateOptions options)
        {
            var evalSetting = await _eval_release_settingService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (evalSetting != null)
            {
                var authService = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, evalSetting, EvaluationReleaseEMPSettingsOperations.Update);
                if (authService.Succeeded)
                {
                    evalSetting.ILAId = options.ILAId;
                    evalSetting.EvaluationRequired = options.EvaluationRequired;
                    evalSetting.EvaluationAvailableOnStartDate = options.EvaluationAvailableOnStartDate;
                    evalSetting.EvaluationAvailableOnEndDate = options.EvaluationAvailableOnEndDate;
                    evalSetting.FinalGradeRequired = options.FinalGradeRequired;
                    evalSetting.ReleaseOnSpecificTimeAfterClassEndDate = options.ReleaseOnSpecificTimeAfterClassEndDate;
                    evalSetting.ReleaseAfterEndTime = options.ReleaseAfterEndTime;
                    evalSetting.ReleasePrior = options.ReleasePrior;
                    evalSetting.ReleaseAfterGradeAssigned = options.ReleaseAfterGradeAssigned;
                    evalSetting.EvaluationDueDate = options.EvaluationDueDate;
                    evalSetting.EvaluationUsedToDeployStudentEvaluation = options.EvaluationUsedToDeployStudentEvaluation;
                    evalSetting.EmpSettingsReleaseTypeId = options.EmpSettingsReleaseTypeId;

                    var validationResult = await _eval_release_settingService.UpdateAsync(evalSetting);
                    if (validationResult.IsValid)
                    {
                        return evalSetting;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EvaluationSettingNotFoundException"]);
            }
        }


        public async Task<ILA_PerformTraineeEval> GetPerformEvalAsync(int id)
        {
            var perform = await _ila_performService.FindQuery(x => x.ILAId == id).FirstOrDefaultAsync();
            if (perform == null)
            {
                return new ILA_PerformTraineeEval();
            }
            else
            {
                return perform;
            }
        }

        public async Task<ILA_PerformTraineeEvalVM> CreateOrUpdatePerformAsync(int id, ILA_PerformTraineeEvalCreateOptions options)
        {
            var perform = await _ila_performService.FindQuery(x => x.ILAId == id).FirstOrDefaultAsync();
            if (perform == null)
            {
                perform = new ILA_PerformTraineeEval();
                perform.Title = options.Title;
                perform.Description = options.Description;
                perform.ILAId = id;
                var validationResult = await _ila_performService.AddAsync(perform);
                if (validationResult.IsValid)
                {
                    var data = new ILA_PerformTraineeEvalVM();
                    data.ILA_PerformTraineeEval = perform;
                    data.State = 1;
                    return data;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                perform.Title = options.Title;
                perform.Description = options.Description;
                perform.ILAId = id;
                var validationResult = await _ila_performService.UpdateAsync(perform);
                if (validationResult.IsValid)
                {
                    var data = new ILA_PerformTraineeEvalVM();
                    data.ILA_PerformTraineeEval = perform;
                    data.State = 2;
                    return data;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
        }

        public async Task<List<TaskWithCountOptions>> GetTQForILAAsync(int id)
        {
            List<TaskWithCountOptions> taskList = new List<TaskWithCountOptions>();
            var taskLinks = await _ilaTaskObjectiveLinkService.FindQuery(x => x.ILAId == id).OrderBy(x => x.SequenceNumber).ToListAsync();
            foreach (var taskLink in taskLinks)
            {
                var task = await _taskDomainService.FindQuery(x => x.Id == taskLink.TaskId).FirstOrDefaultAsync();
                var sda = await _subdutyAreaService.FindQuery(x => x.Id == task.SubdutyAreaId).FirstOrDefaultAsync();
                var da = await _dutyAreaService.FindQuery(x => x.Id == sda.DutyAreaId).FirstOrDefaultAsync();
                TaskWithCountOptions withCount = new TaskWithCountOptions();
                withCount.Id = task.Id;
                withCount.Number = da.Letter + da.Number + "." + sda.SubNumber.ToString() + "." + task.Number.ToString();
                withCount.DANumber = da.Number;
                withCount.SDANumber = sda.SubNumber;
                withCount.Letter = da.Letter;
                withCount.Description = task.Description;
                withCount.IsUsedForTQ = taskLink.UseForTQ;
                taskList.Add(withCount);
            }

            return taskList;
        }
        public async System.Threading.Tasks.Task PublishILAAsync(int id, ILAPublshOptions options)
        {
            var ila = await _ilaService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (ila == null)
            {
                throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
            }
            else
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, ILAOperations.Update);
                if (authResult.Succeeded)
                {
                    ila.EffectiveDate = options.EffectiveDate ?? DateTime.Now;
                    ila.IsPublished = true;
                    var validationResult = await _ilaService.UpdateAsync(ila);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
        }

        public async Task<ILAEvalMethodVM> GetEvalMethodAndEMPAsync(int id)
        {
            var ila = (await _ilaService.FindAsync(x => x.Id == id)).Select(s => new ILAEvalMethodVM { EvaluationMethod = s.TrainingEvalMethods, UseForEMP = s.UseForEMP, IsPubliclyAvailableILA = s.IsPubliclyAvailable }).FirstOrDefault();
            if (ila == null)
            {
                throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
            }
            else
            {
                return ila;
            }
        }

        public async System.Threading.Tasks.Task UpdateUseForEMPAsync(int id, ILAEvalMethodVM options)
        {
            var ila = await _ilaService.GetAsync(id);
            if (ila == null)
            {
                throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
            }
            else
            {
                ila.UseForEMP = options.UseForEMP;
                ila.IsPubliclyAvailable = options.IsPubliclyAvailableILA;
                var validationResult = await _ilaService.UpdateAsync(ila);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
        }

        public async System.Threading.Tasks.Task<ILADetailsVM> UpdateIspubliclyAvailableIla(int id, ILAUpdateOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            if (ila == null)
            {
                throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
            }
            else
            {                
                ila.IsPubliclyAvailable = options.IsPubliclyAvailableILA;
                var validationResult = await _ilaService.UpdateAsync(ila);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
                return MapILADetailsVMByILA(ila);
            }
        }
        public async System.Threading.Tasks.Task UpdateEvalMethodAsync(int id, ILAEvalMethodVM options)
        {
            var ila = await _ilaService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (ila == null)
            {
                throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
            }
            else
            {
                ila.TrainingEvalMethods = options.EvaluationMethod;
                var validationResult = await _ilaService.UpdateAsync(ila);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
        }

        public async System.Threading.Tasks.Task EnrollStudentAsync(int cbtId, int employeeId)
        {
            throw new NotImplementedException();
        }

        /* Eval Settings Implementations End */

        #region Self Registration Options
        public async Task<ILA_SelfRegistrationOptions> GetSelfRegistrationOptionsSettingAsync(int id)
        {
            var selfRegistrationServiceSetting = await _selfRegistrationOptionsService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (selfRegistrationServiceSetting != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, selfRegistrationServiceSetting, SelfRegistrationOptionsOperations.Read);
                if (authResult.Succeeded)
                {
                    return selfRegistrationServiceSetting;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["SelfRegistrationSettingNotFoundException"]);
            }
        }

        public async Task<ILA_SelfRegistrationOptions> GetSelfRegistrationOptionsSettingForILAAsync(int ilaId)
        {
            var selfRegistrationServiceSetting = await _selfRegistrationOptionsService.FindQueryWithIncludeAsync(x => x.ILAId == ilaId, new string[] { "ILA.ILA_Position_Links.Position" }).FirstOrDefaultAsync();
            if (selfRegistrationServiceSetting != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, selfRegistrationServiceSetting, SelfRegistrationOptionsOperations.Read);
                if (authResult.Succeeded)
                {
                    return selfRegistrationServiceSetting;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<ILA_SelfRegistrationOptions> CreateSelfRegistrationOptionsSettingAsync(ILA_SelfRegistrationCreateOptions options)
        {
            var selfRegistrationServiceSetting = new ILA_SelfRegistrationOptions(options.ILAId, options.MakeAvailableForSelfReg,
                options.RequireAdminApproval, options.AcknowledgeRegDisclaimer, options.RegDisclaimer,
                options.LimitForLinkedPositions, options.CloseRegOnStartDate, options.EnableWaitlist, options.SendApprovedEmail);

            var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, selfRegistrationServiceSetting, EvaluationReleaseEMPSettingsOperations.Create);
            if (authResult.Succeeded)
            {
                var validationResult = new ValidationResult();
                var ila = await _ilaService.GetAsync(options.ILAId);
                var exists = await _selfRegistrationOptionsService.FindQuery(x => x.ILAId == options.ILAId).FirstOrDefaultAsync();
                if (exists != null)
                {
                    exists.MakeAvailableForSelfReg = selfRegistrationServiceSetting.MakeAvailableForSelfReg;
                    exists.RequireAdminApproval = selfRegistrationServiceSetting.RequireAdminApproval;
                    exists.AcknowledgeRegDisclaimer = selfRegistrationServiceSetting.AcknowledgeRegDisclaimer;
                    exists.RegDisclaimer = selfRegistrationServiceSetting.RegDisclaimer;
                    exists.LimitForLinkedPositions = selfRegistrationServiceSetting.LimitForLinkedPositions;
                    exists.CloseRegOnStartDate = selfRegistrationServiceSetting.CloseRegOnStartDate;
                    exists.EnableWaitlist = selfRegistrationServiceSetting.EnableWaitlist;
                    exists.SendApprovedEmail = selfRegistrationServiceSetting.SendApprovedEmail;
                    validationResult = await _selfRegistrationOptionsService.UpdateAsync(exists);
                }
                else
                {
                    validationResult = await _selfRegistrationOptionsService.AddAsync(selfRegistrationServiceSetting);
                }
                if (validationResult.IsValid)
                {
                    return selfRegistrationServiceSetting;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }
        }

        public async Task<ILA_SelfRegistrationOptions> UpdateSelfRegistrationOptionsSettingAsync(int id, ILA_SelfRegistrationCreateOptions options)
        {
            var selfRegistrationServiceSetting = await _selfRegistrationOptionsService.FindQuery(x => x.ILAId == id).FirstOrDefaultAsync();
            if (selfRegistrationServiceSetting != null)
            {
                var authService = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, selfRegistrationServiceSetting, SelfRegistrationOptionsOperations.Update);
                if (authService.Succeeded)
                {
                    selfRegistrationServiceSetting.ILAId = options.ILAId;
                    selfRegistrationServiceSetting.MakeAvailableForSelfReg = options.MakeAvailableForSelfReg;
                    selfRegistrationServiceSetting.RequireAdminApproval = options.RequireAdminApproval;
                    selfRegistrationServiceSetting.AcknowledgeRegDisclaimer = options.AcknowledgeRegDisclaimer;
                    selfRegistrationServiceSetting.RegDisclaimer = options.RegDisclaimer;
                    selfRegistrationServiceSetting.LimitForLinkedPositions = options.LimitForLinkedPositions;
                    selfRegistrationServiceSetting.CloseRegOnStartDate = options.CloseRegOnStartDate;
                    //selfRegistrationServiceSetting.ClassSize = options.ClassSize;
                    selfRegistrationServiceSetting.EnableWaitlist = options.EnableWaitlist;


                    var validationResult = await _selfRegistrationOptionsService.UpdateAsync(selfRegistrationServiceSetting);
                    if (validationResult.IsValid)
                    {
                        return selfRegistrationServiceSetting;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EvaluationSettingNotFoundException"]);
            }
        }

        public async Task<List<Employee>> GetTQEvaluatorsForILAAsync(int ilaId)
        {
            var evaluators = await _ila_eval_linkService.FindQueryWithIncludeAsync(x => x.ILAId == ilaId, new string[] { "Evaluator.Person" }).Select(m => m.Evaluator).ToListAsync();
            evaluators = evaluators.Where(w => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, w, EmployeeOperations.Read).Result.Succeeded).ToList();
            return evaluators;
        }

        public async Task<object> GetApplicationDetailsAsync(int id)
        {
            var ilaDetails = await _ilaService.FindQuery(x => x.Id == id).Select(s => new ILA
            {
                Id = s.Id,
                Active = s.Active,
                Deleted = s.Deleted,
                StartDate = s.StartDate,
                HasPilotData = s.HasPilotData,
                PilotDataNA = s.PilotDataNA,
                DoesActivityConform = s.DoesActivityConform,
                SubmissionDate = s.SubmissionDate,
                ApprovalDate = s.ApprovalDate,
                ExpirationDate = s.ExpirationDate,
                OtherNercTargetAudience = s.OtherNercTargetAudience,
                OtherAssesmentTool = s.OtherAssesmentTool,
                PerformAssessmentTool = s.PerformAssessmentTool,
                WriitenOrOnlineAssessmentTool = s.WriitenOrOnlineAssessmentTool
            }).FirstOrDefaultAsync();
            if (ilaDetails != null)
            {
                return new
                {
                    startDate = ilaDetails.StartDate,
                    hasPilotData = ilaDetails.HasPilotData,
                    hasPilotDataNA = ilaDetails.PilotDataNA,
                    doesActivityConform = ilaDetails.DoesActivityConform,
                    targetedAudienceIds = (await _ilaNercAudienceLinkService.GetActiveNercAudienceLinks(ilaDetails.Id)).Select(x => _hasher.Encode(x.NERCTargetAudience.Id.ToString())).ToList(),
                    applicationSubmissionDate = ilaDetails.SubmissionDate,
                    approvalDate = ilaDetails.ApprovalDate,
                    expirationDate = ilaDetails.ExpirationDate,
                    operatorsTrainingTopics = await _ilaTrainingTopicService.GetLinksForActiveTrTopics(ilaDetails.Id),
                    OtherNercTargetAudience = ilaDetails.OtherNercTargetAudience,
                    OtherAssesmentTool = ilaDetails.OtherAssesmentTool,
                    PerformAssessmentTool = ilaDetails.PerformAssessmentTool,
                    WriitenOrOnlineAssessmentTool = ilaDetails.WriitenOrOnlineAssessmentTool,
                };

            }
            return new { };
        }
        public async Task<object> LinkApplicationAsync(int id, IlaApplicationOptions options)
        {
            var ilaDetails = await _ilaService.GetWithIncludeAsync(id, new string[] { "ILA_TrainingTopic_Links.TrainingTopic.TrainingTopic_Category", "ILA_NERCAudience_Links.NERCTargetAudience" });
            if (ilaDetails != null)
            {
                ilaDetails.StartDate = options.startDate;
                ilaDetails.SubmissionDate = options.applicationSubmissionDatertDate;
                ilaDetails.ApprovalDate = options.approvalDate;
                ilaDetails.ExpirationDate = options.expirationDate;
                ilaDetails.HasPilotData = options.hasPilotData;
                ilaDetails.OtherAssesmentTool = options.OtherAssesmentTool;
                ilaDetails.OtherNercTargetAudience = options.OtherNercTargetAudience;
                ilaDetails.PilotDataNA = options.hasPilotDataNA;
                ilaDetails.DoesActivityConform = options.DoesActivityConform;
                ilaDetails.PerformAssessmentTool = options.PerformAssessmentTool;
                ilaDetails.WriitenOrOnlineAssessmentTool = options.WriitenOrOnlineAssessmentTool;
                //if (options.assesmentIds != null)
                //{
                //    ilaDetails.ILA_AssessmentTool_Links = new List<ILA_AssessmentTool_Link>();
                //    foreach (var item in options.assesmentIds)
                //    {
                //        ilaDetails.ILA_AssessmentTool_Links.Add(new ILA_AssessmentTool_Link
                //        {
                //            ILAId = options.ilaId,
                //            AssessmentToolId = item
                //        });
                //    }
                //}

                if (options.trainingTopicIds != null)
                {
                    ilaDetails.ILA_TrainingTopic_Links = new List<ILA_TrainingTopic_Link>();
                    foreach (var item in options.trainingTopicIds)
                    {
                        ilaDetails.ILA_TrainingTopic_Links.Add(new ILA_TrainingTopic_Link
                        {
                            ILAId = options.ilaId,
                            TrTopicId = item
                        });
                    }
                }
                if (options.nercTargetIds != null)
                {
                    ilaDetails.ILA_NERCAudience_Links = new List<ILA_NERCAudience_Link>();
                    foreach (var item in options.nercTargetIds)
                    {
                        ilaDetails.ILA_NERCAudience_Links.Add(new ILA_NERCAudience_Link
                        {
                            ILAId = options.ilaId,
                            NERCAudienceID = item
                        });
                    }
                }
                var validationResult = await _ilaService.UpdateAsync(ilaDetails);
                if (validationResult.IsValid)
                {
                    return new { message = "Ila Application Saved Successfully." };
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }

            }
            return new { message = "Ila not found" };

        }



        public async System.Threading.Tasks.Task LinkEvaluators(ILA_EvaluatorOptions options)
        {
            var ila = await _ilaService.FindQueryWithIncludeAsync(x => x.Id == options.ILAId, new string[] { "ILA_Evaluator_Links" }).FirstOrDefaultAsync();
            if (ila != null)
            {
                foreach (var evalId in options.EvaluatorIds)
                {
                    var eval = await _employeeService.GetAsync(evalId);
                    if (eval != null)
                    {
                        ila.LinkEvaluator(eval);
                        await _ilaService.UpdateAsync(ila);
                    }
                    else
                    {
                        throw new BadHttpRequestException(message: _localizer["EvaluatorNotFoundException"]);
                    }
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
            }
        }

        public async System.Threading.Tasks.Task UnlinkEvaluator(ILA_EvaluatorOptions options)
        {
            var ila = await _ilaService.FindQueryWithIncludeAsync(x => x.Id == options.ILAId, new string[] { "ILA_Evaluator_Links" }).FirstOrDefaultAsync();
            if (ila != null)
            {
                foreach (var evalId in options.EvaluatorIds)
                {
                    var eval = await _employeeService.GetAsync(evalId);
                    if (eval != null)
                    {
                        ila.UnlinkEvaluator(eval);
                        var result = await _ilaService.UpdateAsync(ila);
                        if (!result.IsValid)
                        {
                            throw new BadHttpRequestException(message: result.Errors.FirstOrDefault()?.Message);
                        }
                    }
                    else
                    {
                        throw new BadHttpRequestException(message: _localizer["EvaluatorNotFoundException"]);
                    }
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
            }
        }

        #endregion

        #region TQEvaluator Settings

        public async Task<TQILAEmpSetting> GetTQEvaluatorSettingForILAAsync(int ilaId)
        {
            var tQSetting = await _tqEvalSettings.FindQuery(x => x.ILAId == ilaId).FirstOrDefaultAsync();
            if (tQSetting != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tQSetting, TQILAEmpSettingOperations.Read);
                if (authResult.Succeeded)
                {
                    return tQSetting;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<TQILAEmpSetting> CreateTQEvaluatorSettingAsync(TQEvaluatorILAEmpSettings options)
        {
            var tQSetting = new TQILAEmpSetting(options.ILAId, options.TQRequired, options.ReleaseAtOnce, options.ReleaseOneAtTime, options.ReleaseOnClassStart, options.ReleaseOnClassEnd, options.SpecificTime, options.PriorToSpecificTime, options.OneSignOffRequired, options.TQDueDate, options.MultipleSignOffRequired, options.EmpSettingsReleaseTypeId,options.ShowTaskSuggestions,options.ShowTaskQuestions);

            var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tQSetting, TQILAEmpSettingOperations.Create);
            if (authResult.Succeeded)
            {
                var validationResult = await _tqEvalSettings.AddAsync(tQSetting);
                if (validationResult.IsValid)
                {
                    return tQSetting;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }
        }

        public async Task<TQILAEmpSetting> UpdateTQEvaluatorSettingAsync(int id, TQEvaluatorILAEmpSettings options)
        {
            var tQSetting = await _tqEvalSettings.FindQuery(x => x.ILAId == options.ILAId).FirstOrDefaultAsync();
            if (tQSetting != null)
            {
                var authService = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tQSetting, TQILAEmpSettingOperations.Update);
                if (authService.Succeeded)
                {
                    tQSetting.ILAId = options.ILAId;
                    tQSetting.TQRequired = options.TQRequired;
                    tQSetting.ReleaseAtOnce = options.ReleaseAtOnce;
                    tQSetting.ReleaseOneAtTime = options.ReleaseOneAtTime;
                    tQSetting.ReleaseOnClassStart = options.ReleaseOnClassStart;
                    tQSetting.ReleaseOnClassEnd = options.ReleaseOnClassEnd;
                    tQSetting.SpecificTime = options.SpecificTime;
                    tQSetting.PriorToSpecificTime = options.PriorToSpecificTime;
                    tQSetting.OneSignOffRequired = options.OneSignOffRequired;
                    tQSetting.MultipleSignOffRequired = options.MultipleSignOffRequired;
                    tQSetting.TQDueDate = options.TQDueDate;
                    tQSetting.EmpSettingsReleaseTypeId = options.EmpSettingsReleaseTypeId;
                    tQSetting.ShowTaskSuggestions = options.ShowTaskSuggestions;
                    tQSetting.ShowTaskQuestions = options.ShowTaskQuestions;

                    var validationResult = await _tqEvalSettings.UpdateAsync(tQSetting);
                    if (validationResult.IsValid)
                    {
                        return tQSetting;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EvaluationSettingNotFoundException"]);
            }
        }


        //public async Task<ClassSchedule> LinkTQEvaluatorEmployee(int ilaId, TQEvaluator_EmployeeCreateOptions options)
        //{

        //    var classSchedule = await _ilaService.GetWithIncludeAsync(ilaId, new string[] { nameof(_ila.TQILAEmpSettings) });
        //    foreach (var id in options.employeeIds)
        //    {
        //        var employee = await _employeeService.GetAsync(id);

        //        var classScheduleResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classSchedule, TQILAEmpSettingOperations.Update);
        //        var empResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read);
        //        if (classScheduleResult.Succeeded && empResult.Succeeded)
        //        {
        //            classSchedule.LinkEmployee(employee);
        //            classSchedule.UnlinkEmployee(employee);
        //            var validationResult = await _classScheduleService.UpdateAsync(classSchedule);
        //            if (!validationResult.IsValid)
        //            {
        //                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
        //            }
        //        }
        //        else
        //        {
        //            throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
        //        }
        //    }

        //    return classSchedule;
        //}

        //public async System.Threading.Tasks.Task UnlinkEmployee(int classScheduleId, int[] empIDs)
        //{
        //    var classSchedule = await _classScheduleService.GetWithIncludeAsync(classScheduleId, new string[] { nameof(_classSchedule.ClassSchedule_Employee) });
        //    foreach (var id in empIDs)
        //    {
        //        var employee = await _employeeService.GetAsync(id);

        //        var classScheduleResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classSchedule, ClassScheduleOperations.Update);
        //        var employeeResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read);
        //        if (classScheduleResult.Succeeded && employeeResult.Succeeded)
        //        {
        //            _classSchedule.UnlinkEmployee(employee);
        //            var validationResult = await _classScheduleService.UpdateAsync(classSchedule);
        //            if (!validationResult.IsValid)
        //            {
        //                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
        //            }
        //        }
        //        else
        //        {
        //            throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
        //        }
        //    }
        //}

        //public async Task<List<ClassSchedule>> GetClassSchedulesEmployeeIsLinkedTo(int id)
        //{
        //    var data = await _classScheduleEmployeeLinkService.AllQueryWithInclude(new string[] { nameof(_classScheduleEmployee.ClassSchedule) }).Where(x => x.EmployeeId == id).Select(x => x.ClassSchedule).ToListAsync();
        //    return data;
        //}

        //public async Task<List<EmployeesLinkedToSchedule>> GetLinkedEmployees(int id)
        //{
        //    var links = await _classScheduleEmployeeLinkService.FindWithIncludeAsync(x => x.ClassScheduleId == id, new string[] { nameof(_classScheduleEmployee.Employee) });
        //    List<Domain.Entities.Core.Employee> empList = new List<Domain.Entities.Core.Employee>();
        //    empList.AddRange(links.Select(x => x.Employee));
        //    List<EmployeesLinkedToSchedule> empWithCount = new List<EmployeesLinkedToSchedule>();
        //    foreach (var emp in empList)
        //    {
        //        empWithCount.Add(new EmployeesLinkedToSchedule(emp.Person.FirstName + " " + emp.Person.LastName, emp.Person.Username, emp.EmployeePositions.Select(x => x.Position.PositionTitle).FirstOrDefault(), emp.EmployeeOrganizations.Select(x => x.Organization.Name).FirstOrDefault()));
        //        //empWithCount.Add(new EmployeesLinkedToSchedule(l.Id,l.Employee.Person.FirstName+" "+ l.Employee.Person.LastName,l.Employee.Person.Username, l.Employee.EmployeePositions.Select(x => x.Position.PositionTitle).FirstOrDefault(),l.Employee.EmployeeOrganizations.Select(x=>x.Organization.Name).FirstOrDefault()));
        //    }

        //    return empWithCount;
        //}
        #endregion  TQEvaluator Settings

        public async Task<List<ILADetailsVM>> GetWithTraineeEvalLinks(TestFilterOptions filterOptions)
        {
            var ilas = await _ilaService.GetCompactedILAActiveOnly();
            var providers = await _providerDomainService.GetCompactedProvider();
            for (int i = 0; i < ilas.Count; i++)
            {
                ilas[i].Provider = providers.Find(x => x.Id == ilas[i].ProviderId);
                ilas[i].ILATraineeEvaluations = await _ilaTraineeEvalService.FindQuery(x => x.ILAId == ilas[i].Id && x.Test.TestTitle.Trim().ToLower().Contains(filterOptions.Filter.ToLower()), true).ToListAsync();
            }
            if (filterOptions.OnlyWithLink)
            {
                ilas = ilas.Where(x => x.ILATraineeEvaluations.Count > 0).ToList();
            }
            return ilas.OrderBy(x => x.Provider?.Name).Select(m=>MapILADetailsVMByILA(m)).ToList();
        }

        public async System.Threading.Tasks.Task UpdateTotalTrainingHoursAsync(int id, ILACreditHourVM hours)
        {
            var ila = await _ilaService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (ila != null)
            {
                ila.TotalTrainingHours = hours.TotalCreditHours;
                var validationResult = await _ilaService.UpdateAsync(ila);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["ILA Not Found Exception"]);
            }
        }

        public async Task<double?> GetTotalCredHoursAsync(int id)
        {
            var credHours = await _ilaService.FindQuery(x => x.Id == id).Select(s => s.TotalTrainingHours).FirstOrDefaultAsync();
            return credHours;
        }

        public async Task<List<ILAStatDataVM>> GetILAsNotLinkedToTopic()
        {
            var ilaNotLinkedToTopics = await _ilaService.GetILAsNotLinkedToTopic();
            return ilaNotLinkedToTopics.Select(m=>new ILAStatDataVM(m.Number,m.Name,m.NickName,m.DeliveryMethod?.Name)).OrderBy(x => x.Name).ToList();
        }

        public async Task<Byte[]> ExportILAAsCSV(int ilaId)
        {
            var ila = await _ilaService.GetFullILADetailsAsync(ilaId);

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, new UTF8Encoding(true)))
            using (var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var headers = getExportHeaders(ila.ILA_Segment_Links == null ? 0 : ila.ILA_Segment_Links.Count());

                foreach (var field in headers)
                {
                    csvWriter.WriteField(field);
                }
                csvWriter.NextRecord();

                var record = ilaToExportRow(ila);

                foreach (var field in record)
                {
                    csvWriter.WriteField(field.StripHTML());
                }

                csvWriter.Flush();
                memoryStream.Position = 0;

                return memoryStream.ToArray();
            }
        }

        protected List<string> ilaToExportRow(ILA ila)
        {
            List<string> strings = new List<string>();

            var nercCert = ila.ILACertificationLinks.Where(r => r.Certification.CertifyingBodyId == 1).FirstOrDefault();
            var standardsSub = nercCert?.ILACertificationSubRequirementLink.Where(r => r.CertificationSubRequirement.ReqName.ToUpper() == "Standards".ToUpper()).FirstOrDefault();
            var simulationsSub = nercCert?.ILACertificationSubRequirementLink.Where(r => r.CertificationSubRequirement.ReqName.ToUpper() == "Simulations".ToUpper()).FirstOrDefault();

            //A contact_user_id
            strings.Add(ila.Provider.Number);
            //B entity.client_course_id
            strings.Add(ila.Number);
            //C entity.course_title
            strings.Add(ila.Name);
            //D start_date
            strings.Add(ila.StartDate.GetValueOrDefault().ToString("yyyy-MM-dd"));
            //E course_description
            strings.Add(ila.TrainingPlan);
            //F entity.operating_topics_ceh
            strings.Add(nercCert == null ? "0" : nercCert.CEHHours.ToString());
            //G entity.standards_ceh
            strings.Add(standardsSub == null ? "0" : standardsSub.ReqHour.ToString());
            //H entity.simulations_ceh
            strings.Add(simulationsSub == null ? "0" : simulationsSub.ReqHour.ToString());
            //I criteria
            strings.Add(ila.DoesActivityConform.GetValueOrDefault() ? "Y" : "N");
            //J eor
            strings.Add(nercCert == null ? "N" : nercCert.IsEmergencyOpHours ? "Y" : "N");
            //K pilot_data
            strings.Add(ila.HasPilotData.GetValueOrDefault() ? "Y" : (ila.PilotDataNA.GetValueOrDefault() ? "NA" : "N"));
            //L exam
            strings.Add(ila.WriitenOrOnlineAssessmentTool.GetValueOrDefault() ? "Y" : "");
            //M performance
            strings.Add(ila.PerformAssessmentTool.GetValueOrDefault() ? "Y" : "N");
            //N other
            strings.Add(string.IsNullOrEmpty(ila.OtherAssesmentTool) ? "" : "Y");
            //O assessment_other
            strings.Add(ila.OtherAssesmentTool?.ToString());

            string learningObjectives = "";

            var taskObjectives = ila.ILA_TaskObjective_Links;
            var eoObjectives = ila.ILA_EnablingObjective_Links;
            var ceoObjectives = ila.ILACustomObjective_Links;

            int maxOrder = Math.Max(
                                    Math.Max(taskObjectives.Select(r => r.ILAObjectiveOrder).DefaultIfEmpty(0).Max(),
                                                eoObjectives.Select(r => r.ILAObjectiveOrder).DefaultIfEmpty(0).Max()),
                                    ceoObjectives.Select(r => r.ILAObjectiveOrder).DefaultIfEmpty(0).Max());

            for (int i = 1; i <= maxOrder; i++)
            {
                var taskObjective = taskObjectives.Where(r => r.ILAObjectiveOrder == i).FirstOrDefault();
                var eoObjective = eoObjectives.Where(r => r.ILAObjectiveOrder == i).FirstOrDefault();
                var ceoObjective = ceoObjectives.Where(r => r.ILAObjectiveOrder == i).FirstOrDefault();

                if (i > 1)
                {
                    learningObjectives += "; ";
                }

                learningObjectives += i.ToString() + "-";

                if (taskObjective != null)
                {
                    learningObjectives += taskObjective.Task?.Description + " (" + taskObjective.Task?.FullNumber + ")";
                }
                else if (eoObjective != null)
                {
                    learningObjectives += eoObjective.EnablingObjective.Description + " (" + eoObjective.EnablingObjective.FullNumber + ")";
                }
                else if (ceoObjective != null)
                {
                    learningObjectives += ceoObjective.CustomEnablingObjective.Description + " (" + ceoObjective.CustomEnablingObjective.FullNumber + ")";
                }
            }

            //for (int i = 0; i < 6; i++)
            //{
            //    var segment = ila.ILA_Segment_Links.ElementAtOrDefault(i);

            //    var enablingObjectives = segment == null ? new List<SegmentObjective_Link>() : segment.Segment.SegmentObjective_Links.Where(r => r.EnablingObjectiveId.HasValue).ToList();
            //    var tasks = segment == null ? new List<SegmentObjective_Link>() : segment.Segment.SegmentObjective_Links.Where(r => r.TaskId.HasValue).ToList();
            //    var customObjectives = segment == null ? new List<SegmentObjective_Link>() : segment.Segment.SegmentObjective_Links.Where(r => r.CustomEOId.HasValue).ToList();

            //    learningObjectives += (!String.IsNullOrEmpty(learningObjectives) && enablingObjectives.Count() > 0 ? ";" : "") + String.Join(';', enablingObjectives.Select(r => (learningObjectivesCount + enablingObjectives.IndexOf(r) + 1).ToString() + "-" + r.EnablingObjective.Description + "(" + r.EnablingObjective.FullNumber + ")"));
            //    learningObjectivesCount += enablingObjectives.Count();

            //    learningObjectives += (!String.IsNullOrEmpty(learningObjectives) && tasks.Count() > 0 ? ";" : "") + String.Join(';', tasks.Select(r => (learningObjectivesCount + tasks.IndexOf(r) + 1).ToString() + "-" + r.Task.Description + "(" + r.Task.FullNumber + ")"));
            //    learningObjectivesCount += tasks.Count();

            //    learningObjectives += (!String.IsNullOrEmpty(learningObjectives) && customObjectives.Count() > 0 ? ";" : "") + String.Join(';', customObjectives.Select(r => (learningObjectivesCount + customObjectives.IndexOf(r) + 1).ToString() + "-" + r.CustomEnablingObjective.Description + "(" + r.CustomEnablingObjective.FullNumber + ")"));
            //    learningObjectivesCount += customObjectives.Count();
            //}

            //P learning_objective
            strings.Add(learningObjectives);

            int segmentCount = ila.ILA_Segment_Links == null ? 0 : ila.ILA_Segment_Links.Count();

            for (int i = 0; i < segmentCount; i++)
            {
                var segment = ila.ILA_Segment_Links.ElementAtOrDefault(i);

                string relatedLearningObjectives = "";

                if (segment != null)
                {
                    var objectives = segment.Segment.SegmentObjective_Links.OrderBy(r => int.Parse(getOrder(r, taskObjectives, eoObjectives, ceoObjectives))).ToList();

                    var start = 0;
                    while (start < objectives.Count())
                    {
                        var end = start;
                        for (var next = start + 1; next < objectives.Count(); next++)
                        {
                            var endOrder = int.Parse(getOrder(objectives[end], taskObjectives, eoObjectives, ceoObjectives));
                            var nextOrder = int.Parse(getOrder(objectives[next], taskObjectives, eoObjectives, ceoObjectives));
                            if (endOrder + 1 != nextOrder)
                            {
                                break;
                            }
                            end = next;
                        }
                        relatedLearningObjectives += getOrder(objectives[start], taskObjectives, eoObjectives, ceoObjectives);
                        if (start != end)
                        {
                            relatedLearningObjectives += "-";
                            relatedLearningObjectives += getOrder(objectives[end], taskObjectives, eoObjectives, ceoObjectives);
                        }
                        if (end != objectives.Count() - 1)
                        {
                            relatedLearningObjectives += ";";
                        }
                        start = end + 1;
                    }
                }

                if (i == 0)
                {
                    //Q oper
                    strings.Add(segment == null ? "" : segment.Segment.IsNercOperatingTopics ? "Y" : "");
                    //R stds
                    strings.Add(segment == null ? "" : segment.Segment.IsNercStandard ? "Y" : "");
                    //S sim
                    strings.Add(segment == null ? "" : segment.Segment.IsNercSimulation ? "Y" : "");
                    //T partial_1
                    strings.Add(segment == null ? "" : segment.Segment.IsPartialCredit ? "Y" : "N");
                    //U related_learning_objectives_1
                    strings.Add(relatedLearningObjectives);
                    //V segment_duration_1
                    strings.Add(segment == null ? "" : segment.Segment.Duration.ToString());
                    //W segment_description_1
                    strings.Add(segment == null ? "" : segment.Segment.Content.ToString());
                }
                else
                {
                    //X, AE, AL, AS, AZ  Add_More_Learning_Activity_Content_Segments-[].segment_description
                    strings.Add(segment == null ? "" : segment.Segment.Content.ToString());
                    //Y, AF, AM, AT, BA Add_More_Learning_Activity_Content_Segments-[].oper_repeated
                    strings.Add(segment == null ? "" : segment.Segment.IsNercOperatingTopics ? "Y" : "");
                    //Z, AG, AN, AU, BB Add_More_Learning_Activity_Content_Segments-[].stds_repeated
                    strings.Add(segment == null ? "" : segment.Segment.IsNercStandard ? "Y" : "");
                    //AA, AH, AO, AV, BC Add_More_Learning_Activity_Content_Segments-[].sim_repeated
                    strings.Add(segment == null ? "" : segment.Segment.IsNercSimulation ? "Y" : "");
                    //AB, AI, AP, AW, BD Add_More_Learning_Activity_Content_Segments-[].partial
                    strings.Add(segment == null ? "" : segment.Segment.IsPartialCredit ? "Y" : "N");
                    //AC, AJ, AQ, AX, BE Add_More_Learning_Activity_Content_Segments-[].related_learning_objectives
                    strings.Add(relatedLearningObjectives);
                    //AD, AK, AR, AY, BF Add_More_Learning_Activity_Content_Segments-[].segment_duration
                    strings.Add(segment == null ? "" : segment.Segment.Duration.ToString());
                }
            }

            //BG capacitance
            strings.Add(getTrainingTopicValue(ila, "Capacitance"));
            //BH inductance
            strings.Add(getTrainingTopicValue(ila, "Inductance"));
            //BI impedance
            strings.Add(getTrainingTopicValue(ila, "Impedance"));
            //BJ real_reactive_power
            strings.Add(getTrainingTopicValue(ila, "Real reactive power"));
            //BK electrical_circuits
            strings.Add(getTrainingTopicValue(ila, "Electrical circuits"));
            //BL magnetism
            strings.Add(getTrainingTopicValue(ila, "Magnetism"));
            //BM trigonometry
            strings.Add(getTrainingTopicValue(ila, "Basic trigonometry"));
            //BN ratios
            strings.Add(getTrainingTopicValue(ila, "Ratios"));
            //BO per_unit
            strings.Add(getTrainingTopicValue(ila, "Per unit values"));
            //BP pythagorean
            strings.Add(getTrainingTopicValue(ila, "Pythagorean Theorem"));
            //BQ ohms_law
            strings.Add(getTrainingTopicValue(ila, "Ohm’s Law"));
            //BR kirchhoffs_law
            strings.Add(getTrainingTopicValue(ila, "Kirchoff’s Laws"));
            //BS transmission
            strings.Add(getTrainingTopicValue(ila, "Transmission lines", "Characteristics of the Bulk Electric System"));
            //BT transformers
            strings.Add(getTrainingTopicValue(ila, "Transformers", "Characteristics of the Bulk Electric System"));
            //BU substations
            strings.Add(getTrainingTopicValue(ila, "Substations"));
            //BV power_plants
            strings.Add(getTrainingTopicValue(ila, "Power plants"));
            //BW protection
            strings.Add(getTrainingTopicValue(ila, "Protection"));
            //BX introduction
            strings.Add(getTrainingTopicValue(ila, "Introduction to power system operations and interconnected operations"));
            //BY frequency
            strings.Add(getTrainingTopicValue(ila, "Frequency"));
            //BZ emerg_tech_equip
            strings.Add(getTrainingTopicValue(ila, "Emergency technologies/equipment"));
            //CA transmission_principle
            strings.Add(getTrainingTopicValue(ila, "Transmission lines", "System Protection Principles"));
            //CB transformers_principle
            strings.Add(getTrainingTopicValue(ila, "Transformers", "System Protection Principles"));
            //CC busses
            strings.Add(getTrainingTopicValue(ila, "Busses"));
            //CD generators
            strings.Add(getTrainingTopicValue(ila, "Generators"));
            //CE relays
            strings.Add(getTrainingTopicValue(ila, "Relays and protection schemes"));
            //CF power_system_faults
            strings.Add(getTrainingTopicValue(ila, "Power system faults"));
            //CG syncronizing_equipment
            strings.Add(getTrainingTopicValue(ila, "Synchronizing equipment"));
            //CH under_frequency
            strings.Add(getTrainingTopicValue(ila, "Under-frequency load shedding"));
            //CI under_voltage
            strings.Add(getTrainingTopicValue(ila, "Under-voltage load shedding"));
            //CJ comm_systems
            strings.Add(getTrainingTopicValue(ila, "Communication systems utilized"));
            //CK voltage_control
            strings.Add(getTrainingTopicValue(ila, "Voltage control"));
            //CL frequency_control
            strings.Add(getTrainingTopicValue(ila, "Frequency control"));
            //CM stability
            strings.Add(getTrainingTopicValue(ila, "Power system stability"));
            //CN outage
            strings.Add(getTrainingTopicValue(ila, "Facility outage both planned and unplanned"));
            //CO energy_accounting
            strings.Add(getTrainingTopicValue(ila, "Energy accounting"));
            //CP inadvertent_energy
            strings.Add(getTrainingTopicValue(ila, "Inadvertent energy"));
            //CQ time_error
            strings.Add(getTrainingTopicValue(ila, "Time error control"));
            //CR balancing_resources
            strings.Add(getTrainingTopicValue(ila, "Balancing of load and resources"));
            //CS generation_loss
            strings.Add(getTrainingTopicValue(ila, "Loss of generation resource(s)"));
            //CT transmission_loss
            strings.Add(getTrainingTopicValue(ila, "Loss of transmission element(s)"));
            //CU operating_reserves
            strings.Add(getTrainingTopicValue(ila, "Operating reserves"));
            //CV contingency_reserves
            strings.Add(getTrainingTopicValue(ila, "Contingency reserves"));
            //CW line_loading_relief
            strings.Add(getTrainingTopicValue(ila, "Line loading relief"));
            //CX load_shedding
            strings.Add(getTrainingTopicValue(ila, "Load shedding"));
            //CY emergencies
            strings.Add(getTrainingTopicValue(ila, "Voltage and reactive flows during emergencies"));
            //CZ ems_loss
            strings.Add(getTrainingTopicValue(ila, "Loss of EMS"));
            //DA primary_control_center_loss
            strings.Add(getTrainingTopicValue(ila, "Loss of primary control center"));
            //DB restoration_philosophies
            strings.Add(getTrainingTopicValue(ila, "Restoration philosophies"));
            //DC facility_restoration_priorities
            strings.Add(getTrainingTopicValue(ila, "Facility restoration priorities"));
            //DD blackstart
            strings.Add(getTrainingTopicValue(ila, "Blackstart restoration"));
            //DE stability_angle_voltage
            strings.Add(getTrainingTopicValue(ila, "Stability (angle and voltage)"));
            //DF islanding_and_synchronizing
            strings.Add(getTrainingTopicValue(ila, "Islanding and Synchronizing"));
            //DG naesb
            strings.Add(getTrainingTopicValue(ila, "NAESB standards"));
            //DH standards_of_conduct
            strings.Add(getTrainingTopicValue(ila, "Standards of Conduct"));
            //DI tariffs
            strings.Add(getTrainingTopicValue(ila, "Tariffs"));
            //DJ oasis
            strings.Add(getTrainingTopicValue(ila, "OASIS applications (Transmission Reservations)"));
            //DK e_tag
            strings.Add(getTrainingTopicValue(ila, "E-Tag application"));
            //DL transaction_scheduleing
            strings.Add(getTrainingTopicValue(ila, "Transaction scheduling"));
            //DM market_applications
            strings.Add(getTrainingTopicValue(ila, "Market applications"));
            //DN interchange
            strings.Add(getTrainingTopicValue(ila, "Interchange"));
            //DO scada
            strings.Add(getTrainingTopicValue(ila, "Supervisory Control and Data Acquisition (SCADA)"));
            //DP agc
            strings.Add(getTrainingTopicValue(ila, "Automatic Generation Control (AGC) application"));
            //DQ power_flow
            strings.Add(getTrainingTopicValue(ila, "Power flow application"));
            //DR state_estimator
            strings.Add(getTrainingTopicValue(ila, "State Estimator application"));
            //DS contingency_analysis
            strings.Add(getTrainingTopicValue(ila, "Contingency analysis application"));
            //DT pv_curves
            strings.Add(getTrainingTopicValue(ila, "P-V Curves"));
            //DU forecasting
            strings.Add(getTrainingTopicValue(ila, "Load forecasting application"));
            //DV energy_accounting_app
            strings.Add(getTrainingTopicValue(ila, "Energy accounting application"));
            //DW voice_and_data_comms
            strings.Add(getTrainingTopicValue(ila, "Voice and data communication systems"));
            //DX demand_side
            strings.Add(getTrainingTopicValue(ila, "Demand-side management programs"));
            //DY facilities_loss
            strings.Add(getTrainingTopicValue(ila, "Identifying loss of facilities"));
            //DZ communications_loss
            strings.Add(getTrainingTopicValue(ila, "Recognizing loss of communication facilities"));
            //EA telemetry_problems
            strings.Add(getTrainingTopicValue(ila, "Recognizing telemetry problems"));
            //EB contingency_problems
            strings.Add(getTrainingTopicValue(ila, "Recognizing and identifying contingency problems"));
            //EC proper_communications
            strings.Add(getTrainingTopicValue(ila, "Proper communications (three-part)"));
            //ED appropriate_communicationscyber_security 
            strings.Add(getTrainingTopicValue(ila, "Communication with appropriate entities including the RC"));
            //EE cyber_security
            strings.Add(getTrainingTopicValue(ila, "Cyber and physical security and threats"));
            //EF reducing_so_errors
            strings.Add(getTrainingTopicValue(ila, "Reducing System Operator errors through the use of HPI Tools (self-checking, peer checking, Place Keeping and Procedure Use"));
            //EG iso_rto
            strings.Add(getTrainingTopicValue(ila, "ISO/RTO operational and emergency policies and procedures"));
            //EH regional
            strings.Add(getTrainingTopicValue(ila, "Regional operational and emergency policies and procedures"));
            //EI company_policies
            strings.Add(getTrainingTopicValue(ila, "Company-specific operational and emergency policies and procedures"));
            //EJ reliability_standards
            strings.Add(getTrainingTopicValue(ila, "Application and/or implementation of NERC Reliability Standards"));

            return strings;
        }

        private string getOrder(SegmentObjective_Link segmentObjective_Link, ICollection<ILA_TaskObjective_Link> taskObjectives, ICollection<ILA_EnablingObjective_Link> eoObjectives, ICollection<ILACustomObjective_Link> ceoObjectives)
        {
            if (segmentObjective_Link.TaskId.HasValue)
            {
                var objective = taskObjectives.Where(r => r.TaskId == segmentObjective_Link.TaskId).FirstOrDefault();
                return objective == null ? "1" : objective.ILAObjectiveOrder.ToString();
            }
            else if (segmentObjective_Link.EnablingObjectiveId.HasValue)
            {
                var objective = eoObjectives.Where(r => r.EnablingObjectiveId == segmentObjective_Link.EnablingObjectiveId).FirstOrDefault();
                return objective == null ? "1" : objective.ILAObjectiveOrder.ToString();
            }
            else if (segmentObjective_Link.CustomEOId.HasValue)
            {
                var objective = ceoObjectives.Where(r => r.CustomObjId == segmentObjective_Link.CustomEOId).FirstOrDefault();
                return objective == null ? "1" : objective.ILAObjectiveOrder.ToString();
            }
            else
            {
                return "";
            }
        }

        private string getTrainingTopicValue(ILA ila, string topicName)
        {
            return getTrainingTopicValue(ila, topicName, null);
        }

        private string getTrainingTopicValue(ILA ila, string topicName, string categoryName)
        {
            ILA_TrainingTopic_Link link;

            if (string.IsNullOrEmpty(categoryName))
                link = ila.ILA_TrainingTopic_Links.Where(r => r.TrainingTopic.Name.ToUpper() == topicName.ToUpper()).FirstOrDefault();

            else
                link = ila.ILA_TrainingTopic_Links
                        .Where(r => r.TrainingTopic.Name.ToUpper() == topicName.ToUpper())
                        .Where(r => r.TrainingTopic.TrainingTopic_Category.Name.ToUpper() == categoryName.ToUpper())
                    .FirstOrDefault();

            if (link == null)
                return "";

            else return link.Active ? "Y" : "";
        }

        private List<string> getExportHeaders(int segmentCount)
        {
            List<string> strings = new List<string>();

            //A
            strings.Add("contact_user_id");
            //B
            strings.Add("entity.client_course_id");
            strings.Add("entity.course_title");
            strings.Add("start_date");
            strings.Add("course_description");
            strings.Add("entity.operating_topics_ceh");
            strings.Add("entity.standards_ceh");
            strings.Add("entity.simulations_ceh");
            strings.Add("criteria");
            strings.Add("eor");
            strings.Add("pilot_data");
            strings.Add("exam");
            strings.Add("performance");
            strings.Add("other");
            strings.Add("assessment_other");
            strings.Add("learning_objective");
            if (segmentCount > 0)
            {
                strings.Add("oper");
                strings.Add("stds");
                strings.Add("sim");
                strings.Add("partial_1");
                strings.Add("related_learning_objectives_1");
                strings.Add("segment_duration_1");
                strings.Add("segment_description_1");
            }
            for (int i = 0; i < segmentCount - 1; i++)
            {
                strings.Add($"Add_More_Learning_Activity_Content_Segments-{i.ToString()}.segment_description");
                strings.Add($"Add_More_Learning_Activity_Content_Segments-{i.ToString()}.oper_repeated");
                strings.Add($"Add_More_Learning_Activity_Content_Segments-{i.ToString()}.stds_repeated");
                strings.Add($"Add_More_Learning_Activity_Content_Segments-{i.ToString()}.sim_repeated");
                strings.Add($"Add_More_Learning_Activity_Content_Segments-{i.ToString()}.partial");
                strings.Add($"Add_More_Learning_Activity_Content_Segments-{i.ToString()}.related_learning_objectives");
                strings.Add($"Add_More_Learning_Activity_Content_Segments-{i.ToString()}.segment_duration");
            }
            strings.Add("capacitance");
            strings.Add("inductance");
            strings.Add("impedance");
            strings.Add("real_reactive_power");
            strings.Add("electrical_circuits");
            strings.Add("magnetism");
            strings.Add("trigonometry");
            strings.Add("ratios");
            strings.Add("per_unit");
            strings.Add("pythagorean");
            strings.Add("ohms_law");
            strings.Add("kirchhoffs_law");
            strings.Add("transmission");
            strings.Add("transformers");
            strings.Add("substations");
            strings.Add("power_plants");
            strings.Add("protection");
            strings.Add("introduction");
            strings.Add("frequency");
            strings.Add("emerg_tech_equip");
            strings.Add("transmission_principle");
            strings.Add("transformers_principle");
            strings.Add("busses");
            strings.Add("generators");
            strings.Add("relays");
            strings.Add("power_system_faults");
            strings.Add("syncronizing_equipment");
            strings.Add("under_frequency");
            strings.Add("under_voltage");
            strings.Add("comm_systems");
            strings.Add("voltage_control");
            strings.Add("frequency_control");
            strings.Add("stability");
            strings.Add("outage");
            strings.Add("energy_accounting");
            strings.Add("inadvertent_energy");
            strings.Add("time_error");
            strings.Add("balancing_resources");
            strings.Add("generation_loss");
            strings.Add("transmission_loss");
            strings.Add("operating_reserves");
            strings.Add("contingency_reserves");
            strings.Add("line_loading_relief");
            strings.Add("load_shedding");
            strings.Add("emergencies");
            strings.Add("ems_loss");
            strings.Add("primary_control_center_loss");
            strings.Add("restoration_philosophies");
            strings.Add("facility_restoration_priorities");
            strings.Add("blackstart");
            strings.Add("stability_angle_voltage");
            strings.Add("islanding_and_synchronizing");
            strings.Add("naesb");
            strings.Add("standards_of_conduct");
            strings.Add("tariffs");
            strings.Add("oasis");
            strings.Add("e_tag");
            strings.Add("transaction_scheduleing");
            strings.Add("market_applications");
            strings.Add("interchange");
            strings.Add("scada");
            strings.Add("agc");
            strings.Add("power_flow");
            strings.Add("state_estimator");
            strings.Add("contingency_analysis");
            strings.Add("pv_curves");
            strings.Add("forecasting");
            strings.Add("energy_accounting_app");
            strings.Add("voice_and_data_comms");
            strings.Add("demand_side");
            strings.Add("facilities_loss");
            strings.Add("communications_loss");
            strings.Add("telemetry_problems");
            strings.Add("contingency_problems");
            strings.Add("proper_communications");
            strings.Add("appropriate_communications");
            strings.Add("cyber_security");
            strings.Add("reducing_so_errors");
            strings.Add("iso_rto");
            strings.Add("regional");
            strings.Add("company_policies");
            strings.Add("reliability_standards");

            return strings;
        }

        public async Task<ILARequirementsDetailsVM> GetILARequirementsDetailsByILAId(int ilaId)
        {
            var ilaRequirementsDetails = await _ilaService.GetILARequirementsDetailsByILAId(ilaId);
            ILARequirementsDetailsVM iLARequirementsDetailsVM = new ILARequirementsDetailsVM(ilaRequirementsDetails.Provider?.Name, ilaRequirementsDetails.Number, ilaRequirementsDetails.Name, ilaRequirementsDetails.ClassSchedules.Select(cls=>new ClassScheduleILARequirementDetailVM(cls.Instructor?.InstructorName,cls.Location?.LocAddress,cls.Location?.LocCity,cls.Location?.LocState,cls.StartDateTime)).ToList(), ilaRequirementsDetails?.TotalTrainingHours);
            return iLARequirementsDetailsVM;
        }

        public async System.Threading.Tasks.Task UpdatePreRequisitesAsync(int ilaId, ILAPrerequisitesOptions options)
        {
            var ila = (await _ilaService.FindAsync(x => x.Id == ilaId)).FirstOrDefault();
            if (ila != null)
            {
                ila.Prerequisites = options.PreRequisites;
                var validationResult = await _ilaService.UpdateAsync(ila);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["ILA Not Found Exception"]);
            }
        }

        public async Task<string> GetPreRequisitesAsync(int ilaId)
        {
            var ila = (await _ilaService.FindAsync(x => x.Id == ilaId)).FirstOrDefault();
            return ila.Prerequisites;
        }

        public async System.Threading.Tasks.Task DeleteILACertLinksAsync(int id, int certifyingBodyId)
        {
            var ila = await _ilaService.GetILAWithILACertLinksAsync(id);
            if (ila != null)
            {
                var ilaCertificationLinks = ila.ILACertificationLinks.Where(r => r.Certification.CertifyingBodyId == certifyingBodyId).ToList();
                foreach (var certificationLink in ilaCertificationLinks)
                {
                    certificationLink.Delete();
                    var certSubReqLink = certificationLink.ILACertificationSubRequirementLink;
                    foreach (var subReqLink in certSubReqLink)
                    {
                        subReqLink.Delete();
                    }
                    await _ilaCertificationLinkService.UpdateAsync(certificationLink);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["ILA Not Found Exception"]);
            }
        }

        public async System.Threading.Tasks.Task SaveILACertLinksByCertifyingBodyAsync(int id, int certifyingBodyId, CertifyingBodyCEHUpdateOptions options)
        {
            var certifications = (await _certificationService.FindWithIncludeAsync(r => r.CertifyingBodyId == certifyingBodyId, new[] { "CertificationSubRequirements" })).ToList();
            var ila = await _ilaService.GetILAWithILACertLinksAsync(id);
            foreach (var certification in certifications)
            {
                var ilaCertificationLink = ila.ILACertificationLinks.Where(r => r.CertificationId == certification.Id).FirstOrDefault();
                if (ilaCertificationLink != null)
                {
                    ilaCertificationLink.UpdateCertificationLink(options.IsIncludeSimulation, options.IsEmergencyOpHours, options.IsPartialCreditHours, options.CEHHours);
                    var result = await _ilaCertificationLinkService.UpdateAsync(ilaCertificationLink);
                    if (!result.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
                    }
                }
                else
                {
                    ilaCertificationLink = new ILACertificationLink(certification.Id, id, options.IsIncludeSimulation, options.IsEmergencyOpHours, options.IsPartialCreditHours, options.CEHHours);
                    var result = await _ilaCertificationLinkService.AddAsync(ilaCertificationLink);
                    if (!result.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
                    }
                }

                foreach (var subReq in certification.CertificationSubRequirements)
                {
                    var subRequirementLink = ilaCertificationLink.ILACertificationSubRequirementLink.Where(x => x.CertificationSubRequirementId == subReq.Id).FirstOrDefault();
                    var reqHour = options.SubRequirements.Where(x => x.ReqName == subReq.ReqName).FirstOrDefault()?.ReqHour ?? 0;
                    if (subRequirementLink != null)
                    {
                        subRequirementLink.UpdateReqHour(reqHour);
                        var subReqResult = await _ilaCertificationSubReqLinkService.UpdateAsync(subRequirementLink);
                        if (!subReqResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', subReqResult.Errors));
                        }
                    }
                    else
                    {
                        subRequirementLink = new ILACertificationSubRequirementLink(ilaCertificationLink.Id, subReq.Id, reqHour);
                        var subReqResult = await _ilaCertificationSubReqLinkService.AddAsync(subRequirementLink);
                        if (!subReqResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', subReqResult.Errors));
                        }
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task<ILATaskChangeModel> UpdateILATaskObjectiveLinksAsync(int ilaId, ILATaskObjectiveLinkUpdateOptions options)
        {
            ILATaskChangeModel result = new ILATaskChangeModel();
            string[] includes = new string[] { "ILA_TaskObjective_Links.Task" };
            includes = options.IsIncludeEos ? includes.Append("ILA_EnablingObjective_Links").ToArray() : includes;
            includes = options.IsIncludeProcedures ? includes.Append("ILA_Procedure_Links").ToArray() : includes;
            var ila = await _ilaService.GetWithIncludeAsync(ilaId, includes);
            var currentTaskLinks = ila.ILA_TaskObjective_Links.ToList();
            bool isDataOrSequenceChanged = false;
            string userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name))?.Id;
            foreach (var currentTaskLink in currentTaskLinks)
            {
                if (!options.TaskLinks.Where(x => x.TaskId == currentTaskLink.TaskId).Any())
                {
                    currentTaskLink.Delete();
                    currentTaskLink.Modify(userName);
                    isDataOrSequenceChanged = true;
                    result.TasksRemoved.Add(currentTaskLink.Task);
                }
            }
            int j = 0;
            foreach (var taskLink in options.TaskLinks)
            {
                j++;
                var sequence = taskLink.Sequence == 0 ? j : taskLink.Sequence;
                var taskLinkToModify = currentTaskLinks.Where(x => x.TaskId == taskLink.TaskId).FirstOrDefault();
                if (taskLinkToModify == null)
                {
                    var task = await _taskDomainService.GetAsync(taskLink.TaskId);
                    var ila_task_link = ila.LinkTaskObjective(task, sequence);
                    ila_task_link.Create(userName);
                    result.TasksAdded.Add(task);
                    isDataOrSequenceChanged = true;
                    if (options.IsIncludeProcedures)
                    {
                        var proceduresLinkedToTasks = await _taskService.GetLinkedProceduresAsync(taskLink.TaskId);
                        proceduresLinkedToTasks = proceduresLinkedToTasks.Where(p => p.Active).ToList();
                        //link procedures to ila
                        foreach (var procedure in proceduresLinkedToTasks)
                        {
                            if (!ila.ILA_Procedure_Links.Any(x => x.ProcedureId == procedure.Id))
                            {
                                var procedureLink = ila.LinkProcedure(procedure);
                                procedureLink.Create(userName);
                            }
                        }
                    }
                    if (options.IsIncludeEos)
                    {
                        var eosLinkedToTasks = await _taskService.GetLinkedEnablingObjectivesAsync(taskLink.TaskId);
                        //link procedures to ila
                        foreach (var eo in eosLinkedToTasks)
                        {
                            if (eo.Active && !ila.ILA_EnablingObjective_Links.Any(x => x.EnablingObjectiveId == eo.Id))
                            {
                                var eoLink = ila.LinkEnablingObjective(eo);
                                eoLink.Create(userName);
                            }
                        }
                    }
                    if (options.IsIncludeMetaTask)
                    {
                        var metaTask = (await _taskDomainService.FindWithIncludeAsync(x => x.Id == taskLink.TaskId && x.IsMeta, new string[] { "Task_MetaTask_Links.Task" })).FirstOrDefault();
                        if (metaTask != null)
                        {
                            foreach (var linkedTask in metaTask.Task_MetaTask_Links.Where(x => !options.TaskLinks.Any(y => y.TaskId == x.TaskId)))
                            {
                                j++;
                                sequence = taskLink.Sequence == 0 ? j : taskLink.Sequence;
                                var ila_meta_task_link = ila.LinkTaskObjective(linkedTask.Task, sequence);
                                ila_meta_task_link.Create(userName);
                                result.TasksAdded.Add(task);
                                isDataOrSequenceChanged = true;
                            }
                        }
                    }
                }
                else if (taskLinkToModify.SequenceNumber != sequence)
                {
                    taskLinkToModify.UpdateSequenceNumber(sequence);
                    taskLinkToModify.Modify(userName);
                    isDataOrSequenceChanged = true;
                }

            }
            if (isDataOrSequenceChanged)
            {
                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
                if (ilaResult.Succeeded)
                {
                    await _ilaService.UpdateAsync(ila);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            return result;
        }


        public async Task<ILA_TopicChangeCount> UpdateLinkedILATopicsAsync(int ilaId, ILA_Topic_LinkOptions options)
        {
            ILA_TopicChangeCount ilaTopicChangeCount = new ILA_TopicChangeCount();
            var ila = await _ilaService.GetWithIncludeAsync(ilaId, new string[] { "ILA_Topic_Links" });
            var currentTopicLinks = ila.ILA_Topic_Links.ToList();
            foreach (var currentTopicLink in currentTopicLinks)
            {
                if (!options.TopicIds.Contains(currentTopicLink.ILATopicId))
                {
                    currentTopicLink.Delete();
                    currentTopicLink.Modify((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                    ilaTopicChangeCount.TopicsRemoved++;
                }
            }
            foreach (var newTopicId in options.TopicIds)
            {
                if (!currentTopicLinks.Select(x => x.ILATopicId).Contains(newTopicId))
                {
                    var ilaTopic = await _topicDomainService.GetAsync(newTopicId);
                    var ila_topic_link = ila.LinkILATopic(ilaTopic);
                    ila_topic_link.Create((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                    ilaTopicChangeCount.TopicsAdded++;
                }
            }
            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Update);
            if (ilaResult.Succeeded)
            {
                await _ilaService.UpdateAsync(ila);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
            ilaTopicChangeCount.ILA = await GetAsync(ilaId);
            return ilaTopicChangeCount;
        }
        public async Task<List<ILA_Topic>> GetLinkedILATopicsAsync(int id)
        {
            var result = await _ilaTopicLinkService.FindWithIncludeAsync(x => x.ILAId == id, new string[] { "ILA_Topic" });
            List<ILA_Topic> linkedTopics = new List<ILA_Topic>();
            linkedTopics.AddRange(result.Select(x => x.ILA_Topic));
            linkedTopics = linkedTopics.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ILA_TopicOperations.Read).Result.Succeeded).ToList();
            return linkedTopics;
        }

        public async Task<List<CBT_ScormUploadVM>> GetAllCBTScormUploads()
        {
            var cbtScormUploads = await _scormUploadService.GetAllCBTScormUploadsAsync();
            return cbtScormUploads.Select(m=>new CBT_ScormUploadVM(m.Id,m?.CbtId,m?.CBT?.ILAId,m?.CBT?.ILA?.ProviderId,m.Name,m.Active)).OrderBy(m=>m.Id).ToList();
        }

        public async System.Threading.Tasks.Task ReorderObjectiveLinks(int ilaId)
        {
            var ila = await _ilaService.GetWithIncludeAsync(ilaId, new string[]
            {
                "ILA_Segment_Links.Segment.SegmentObjective_Links",
                "ILA_TaskObjective_Links.Task",
                "ILA_EnablingObjective_Links.EnablingObjective",
                "ILACustomObjective_Links.CustomEnablingObjective",

                //"ILA_TaskObjective_Links.Task.SubdutyArea.DutyArea",
                //"ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_Topic",
                //"ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_SubCategory",
                //"ILA_EnablingObjective_Links.EnablingObjective.EnablingObjective_Category",
                //"ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_Topic",
                //"ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_SubCategory",
                //"ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_Category",
            });

            ila.OrderObjectives();

            await _ilaService.UpdateAsync(ila);
        }

        public bool HasILAChanges(ILA ila, ILAUpdateOptions options)
        {
            if (ila.Name != options.Name) return true;
            if (ila.Description != options.Description) return true;
            if (ila.NickName != options.NickName) return true;
            if (ila.Number != options.Number) return true;
            if (ila.DeliveryMethodId != options.DeliveryMethodId) return true;
            if (ila.ProviderId != options.ProviderId) return true;
            if (ila.Description != options.Description) return true;
            if (ila.IsSelfPaced != options.IsSelfPaced) return true;
            if (options.ilaDeliveryMethod != "") return true;
            if (options.Image != "") return true;

            return false;
        }

        public bool HasILASelfRegistrationOptionChanges(ILA_SelfRegistrationOptions ilaSelfRegOptions, ILA_SelfRegistrationCreateOptions options)
        {
            if (ilaSelfRegOptions.MakeAvailableForSelfReg != options.MakeAvailableForSelfReg) return true;
            if (ilaSelfRegOptions.RequireAdminApproval != options.RequireAdminApproval) return true;
            if (ilaSelfRegOptions.AcknowledgeRegDisclaimer != options.AcknowledgeRegDisclaimer) return true;
            if (ilaSelfRegOptions.RegDisclaimer != options.RegDisclaimer) return true;
            if (ilaSelfRegOptions.LimitForLinkedPositions != options.LimitForLinkedPositions) return true;
            if (ilaSelfRegOptions.CloseRegOnStartDate != options.CloseRegOnStartDate) return true;
            if (ilaSelfRegOptions.EnableWaitlist != options.EnableWaitlist) return true;
            if (ilaSelfRegOptions.SendApprovedEmail != options.SendApprovedEmail) return true;

            return false;
        }

        public async Task<List<ILAStatDataVM>> GetActiveILAs()
        {
            var ilas = await _ilaService.GetAllILAsWithDeliveryMethodAsync("Active");
            return ilas.Select(x => new ILAStatDataVM(x.Number, x.Name, x.NickName, x.DeliveryMethod?.Name)).OrderBy(m=>m.Name).ToList();
        }

        public async Task<List<ILAStatDataVM>> GetDraftILAs()
        {
            var ilas = await _ilaService.GetAllILAsWithDeliveryMethodAsync("Draft");
            return ilas.Select(x => new ILAStatDataVM(x.Number, x.Name, x.NickName, x.DeliveryMethod?.Name)).OrderBy(m => m.Name).ToList();
        }

        public async Task<List<string>> GetILALinkedTrainingTopicsNamesAsync(int id)
        {
            var ila = await _ilaService.GetILAWithTrainingTopics(id);
            return ila.ILA_TrainingTopic_Links.Select(m => m.TrainingTopic.Name).ToList();
        }

        public async Task<ILAPreviewVM> GetILAPreviewDetailsAsync(int id)
        {
            var ila = (await _ilaService.GetILAsWithCertificationInformationAsync(new List<int> { id})).FirstOrDefault();
            var nercILACertificationLink = ila?.ILACertificationLinks?.FirstOrDefault(x => x.Certification?.CertifyingBody?.Name == "NERC");
            var cehHour = nercILACertificationLink?.CEHHours;
            var standardReqHour = nercILACertificationLink?.ILACertificationSubRequirementLink?.FirstOrDefault(x => x.CertificationSubRequirement.ReqName == "Standards")?.ReqHour;
            var simulationReqHour = nercILACertificationLink?.ILACertificationSubRequirementLink?.FirstOrDefault(x => x.CertificationSubRequirement.ReqName == "Simulations")?.ReqHour;
            var ilaObjectives = await GetAllLinkedObjectivesAsync(id);
            var linkedSegments = await _ilaSegmentLinkService.GetILASegmentLinksWithSegments(id);
            var ilaLinkedSegments = linkedSegments.Select(x => new SegmentVM(x.Segment.Duration, x.Segment.IsNercStandard, x.Segment.IsNercOperatingTopics, x.Segment.IsNercSimulation, x.Segment.Content, x.Segment.SegmentObjective_Links.Count(),x.Segment.IsPartialCredit)).ToList();
            var ilaPreview = new ILAPreviewVM(ila.Provider?.Name,ila.Provider?.Number,ila.Provider?.ContactName,ila.Provider?.ContactPhone,ila.Provider?.ContactEmail,ila.Provider?.RepEmail,ila.Name,ila.Number,ila.StartDate,ila.Description, cehHour,standardReqHour,simulationReqHour, ilaObjectives, ilaLinkedSegments);
            return ilaPreview;
        }

        public async Task<List<ILACertificationLinkVM>> GetILANERCCertificationDetailsAsync(int id)
        {
            var ila = (await _ilaService.GetILAsWithCertificationInformationAsync(new List<int> { id })).FirstOrDefault();
            var nercILACertificationLink = ila?.ILACertificationLinks?.Where(x => x.Certification?.CertifyingBody?.Name == "NERC");
            return nercILACertificationLink.Select(m => new ILACertificationLinkVM(m.Certification.Name, m.CEHHours, m.ILACertificationSubRequirementLink.FirstOrDefault(m => m.CertificationSubRequirement.ReqName == "Standards").ReqHour, m.ILACertificationSubRequirementLink.FirstOrDefault(m => m.CertificationSubRequirement.ReqName == "Simulations").ReqHour, m.IsEmergencyOpHours)).ToList();
        }

        public async Task<ILA_SelfRegistrationOptions_ViewModel> GetSelfRegistrationOptionsSettingByILAIdAsync(int ilaId)
        {
            var ilaSelfRegistration = await _selfRegistrationOptionsService.GetSelfRegistrationWithILAByILAIdAsync(ilaId);
            if (ilaSelfRegistration == null) return null;
            return new ILA_SelfRegistrationOptions_ViewModel(ilaSelfRegistration?.MakeAvailableForSelfReg,ilaSelfRegistration?.RequireAdminApproval,ilaSelfRegistration?.SendApprovedEmail,ilaSelfRegistration?.AcknowledgeRegDisclaimer,ilaSelfRegistration?.RegDisclaimer,ilaSelfRegistration?.LimitForLinkedPositions,ilaSelfRegistration?.CloseRegOnStartDate,ilaSelfRegistration?.EnableWaitlist,ilaSelfRegistration?.ILA?.ClassSize);
        }

        public async Task<string> GetTrainingPlanByILAIdAsync(int id)
        {
            var ila= await _ilaService.GetTrainingPlanAsync(id);
            return ila.TrainingPlan; 
        }

        public async Task<ILADetailsVM> GetILAByIdAsync(int id)
        {
            var ila = await _ilaService.GetILAWithProviderAndDeliveryMethodAsync(id);
            return MapILADetailsVMByILA(ila);
        }

        public ILADetailsVM MapILADetailsVMByILA(ILA ila)
        {
            var ILADetailsVM = new ILADetailsVM(ila.Id,ila.Name,ila.Number,ila.NickName,ila.Image,ila.Description,ila.ProviderId,ila.DeliveryMethodId,ila.IsSelfPaced,ila.UseForEMP,ila.IsPublished,ila.CBTRequiredForCourse,ila.DeliveryMethod?.Name,ila.Provider?.Name,ila.Provider?.IsNERC,ila.ILATraineeEvaluations?.Count(),ila.Active, ila.IsPubliclyAvailable);
            return ILADetailsVM;
        }

        public async Task<List<string>> CanILABeDeactivateAsync(int ilaId)
        {
            var EmpReleaseList = new List<string>();
            var hasPendingCBT = (await _cbt_ScormRegistrationService.GetPendingCBTScormRegistrationByILAIdAsync(ilaId)).Any();
            var pendingTests = (await _classScheduleRosterDomainService.GetPendingClassScheduleRosterByILAIdAsync(ilaId));
            var hasPendingPretest = pendingTests.Where(x=>x.TestType.Description == "Pretest").Any();
            var hasPendingFinalTest = pendingTests.Where(x=>x.TestType.Description == "Final Test").Any();
            var hasPendingRetake = pendingTests.Where(x=>x.TestType.Description == "Retake").Any();
            var hasPendingEvaluation = (await _classScheduleEvaluationRosterDomainService.GetPendingClassScheduleEvaluationRosterByILAIdAsync(ilaId)).Any();
            var hasPendingTaskQualification = (await _taskQualificationDomainService.GetPendingTaskQualificationsByILAIdAsync(ilaId)).Any();
            if (hasPendingCBT) EmpReleaseList.Add("CBT/Online Course");
            if (hasPendingPretest) EmpReleaseList.Add("PreTest");
            if (hasPendingFinalTest) EmpReleaseList.Add("Final Test");
            if (hasPendingRetake) EmpReleaseList.Add("Retake");
            if (hasPendingEvaluation) EmpReleaseList.Add("Student Evaluation");
            if (hasPendingTaskQualification) EmpReleaseList.Add("Task Qualification");
            return EmpReleaseList.ToList();
        }

        public async Task<bool> CanPopulateOJTBeDeactivateAsync(int ilaId)
        {
            var performEval = await _ila_performService.GetILAPerformTraineeByILAIdAsync(ilaId);
            var ilaTaskObjectives = await _ilaTaskObjectiveLinkService.GetILATaskObjective_LinksAsync(ilaId);
            return performEval == null || ilaTaskObjectives.All(it => !it.UseForTQ);
        }

        public async Task<List<TQwithTask_EvalDetailVM>> GetPendingLinkedTaskObjectivesAsync(int id, EmployeeIdsModel options)
        {
            var pendingTaskQualifications = await _taskQualificationDomainService.GetPendingTaskQualificationsByILAIdAndEmpIdsAsync(id, options.EmployeeIds);
            var distinctTaskIds = pendingTaskQualifications.Select(m => m.TaskId).Distinct().ToList();
            var tasks = await _taskDomainService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds);
            pendingTaskQualifications.ForEach(x => x.Task = tasks.FirstOrDefault(m => m.Id == x.TaskId));

            var result = pendingTaskQualifications.Select(m =>
            {
                var allEvaluators = m.TaskQualification_Evaluator_Links.Select(link => new
                {
                    EvaluatorId = link.EvaluatorId,
                    FullName = $"{link.Evaluator.Person.FirstName} {link.Evaluator.Person.LastName}"
                }).ToList();
                
                var signedOffEvaluatorIds = m.TaskReQualificationEmp_SignOff.Select(sign => sign.EvaluatorId).ToHashSet();

                var pendingEvaluators = allEvaluators.Where(e => !signedOffEvaluatorIds.Contains(e.EvaluatorId)).Select(e => e.FullName).ToList();

                return new TQwithTask_EvalDetailVM(m.Task.FullNumber,m.Task.Description,pendingEvaluators);
            }).ToList();

            return result;
        }

        public async System.Threading.Tasks.Task UpdateClassSizeAsync(int? classSize, int ilaId)
        {
            var ila = await _ilaService.GetAsync(ilaId);
            if (ila.ClassSize == classSize) return;
            ila.ClassSize = classSize;
            var result = await _ilaService.UpdateAsync(ila);
        }
        
        public async Task<List<ILACertificationLink_SubRequirementVM>> GetILANERCCertificationSubRequirementNamesForPartialCreditAsync(int ilaId)
        {
            var ila = (await _ilaService.GetILAsWithCertificationSubRequirementsAsync(ilaId)).FirstOrDefault();
            var nercILACertificationLink = ila?.ILACertificationLinks?.Where(x => x.Certification?.CertifyingBody?.Name == "NERC" && x.IsPartialCreditHours);
            return nercILACertificationLink.Select(ic => new ILACertificationLink_SubRequirementVM(ic.Id,ic.CEHHours, ic.ILACertificationSubRequirementLink.Select(sr => new ILACertificationSubRequirementLinkVM(sr.Id,sr.CertificationSubRequirement.ReqName,sr.ReqHour)).ToList())).ToList();
        }

        public async Task<bool> IsILACreatedFromInstructorWorkbook(int ilaId)
        {
            var prospectiveILA = await _instructorWorkbook_ProspectiveILADomainService.GetIWBProspectiveILAByILAId(ilaId);
            return prospectiveILA != null;
        }

        public async Task<ILA> CreateBasicAsync(ILABasicCreateOptions options)
        {
            var obj = await _ilaService.GetILAByNameOrNumberAsync(options.Name, options.Number);
            if (obj != null)
            {
                throw new BadHttpRequestException(message: _localizer["ILANameOrNumberAlreadyExists"].Value);
            }

            var createdBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var ila = new ILA(options.Name, options.Number, options.TotalHours, options.ProviderId, options.DeliveryMethodId, options.IsSelfPacedILA ?? false, options.IsPublished ?? false, createdBy);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Create);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }

            var validationResult = await _ilaService.AddAsync(ila);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

            if (options.CEHUpdates != null && options.CEHUpdates.Any())
            {
                foreach (var ceh in options.CEHUpdates)
                {
                    var ilaCert = new ILACertificationLink(
                        ceh.CertificationId,
                        ila.Id,
                        ceh.IsIncludeSimulation,
                        ceh.IsEmergencyOpHours,
                        ceh.IsPartialCreditHours,
                        ceh.CEHHours,
                        createdBy
                    );

                    var certSaveResult = await _ilaCertificationLinkService.AddAsync(ilaCert);
                    if (!certSaveResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(string.Join(',', certSaveResult.Errors));
                    }

                    if (ceh.SubRequirements != null && ceh.SubRequirements.Any())
                    {
                        foreach (var subReq in ceh.SubRequirements)
                        {
                            var subRequirementLink = new ILACertificationSubRequirementLink(
                                ilaCert.Id,                
                                subReq.SubRequirementId,   
                                subReq.ReqHour            
                            );

                            var subReqResult = await _ilaCertificationSubReqLinkService.AddAsync(subRequirementLink);
                            if (!subReqResult.IsValid)
                            {
                                throw new System.ComponentModel.DataAnnotations.ValidationException(string.Join(',', subReqResult.Errors));
                            }
                        }
                    }
                }
            }

            return ila;
        }


    }
}
