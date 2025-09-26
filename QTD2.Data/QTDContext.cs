using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using QTD2.Data.Mapping.Core;
using QTD2.Domain.Entities.Common;
using QTD2.Domain.Entities.Core;
using System;
using System.Linq;
using System.Threading;

using MediatR;
using QTD2.Data.Mapping.Core.PublicClass;
using QTD2.Data.Mapping.Core.Notifications;

namespace QTD2.Data
{
    public class QTDContext : DbContext
    {
        private readonly string _currentUserId;
        private readonly IMediator _mediator;

        public QTDContext(DbContextOptions<QTDContext> options, string currentUserId, IMediator mediator)
         : base(options)
        {
            _currentUserId = currentUserId;
            _mediator = mediator;
        }


        public override async System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is Entity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (Entity)entry.Entity;
                var now = DateTime.UtcNow;
                var userName = _currentUserId ?? String.Empty;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = userName;
                }

                entity.ModifiedDate = now;
                entity.ModifiedBy = userName;
            }

            var result = await base.SaveChangesAsync(cancellationToken);


            var domainEntities = ChangeTracker.Entries<Entity>().Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());
            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);

            return result;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //apply configurations here
            modelBuilder.ApplyConfiguration(new ToolCategory_StatusHistoryMap());
            modelBuilder.ApplyConfiguration(new TaskReQualificationEmp_SignOffMap());
            modelBuilder.ApplyConfiguration(new TaskReQualificationEmp_QuestionAnswerMap());
            modelBuilder.ApplyConfiguration(new TaskReQualificationEmp_StepsMap());
            modelBuilder.ApplyConfiguration(new TaskReQualificationEmp_SuggestionMap());
            modelBuilder.ApplyConfiguration(new ILACertificationLinkMap());
            modelBuilder.ApplyConfiguration(new ILACertificationSubRequirementLinkMap());
            modelBuilder.ApplyConfiguration(new ProcedureReview_EmployeeMap());

            modelBuilder.ApplyConfiguration(new ILACertificationLinkMap());
            modelBuilder.ApplyConfiguration(new ILACertificationSubRequirementLinkMap());
            modelBuilder.ApplyConfiguration(new ProcedureReview_EmployeeMap());

            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new ClientUserMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new PositionMap());
            modelBuilder.ApplyConfiguration(new OrganizationMap());
            modelBuilder.ApplyConfiguration(new CertifyingBodyMap());
            modelBuilder.ApplyConfiguration(new CertificationMap());
            modelBuilder.ApplyConfiguration(new EmployeePositionMap());
            modelBuilder.ApplyConfiguration(new EmployeeOrganizationMap());
            modelBuilder.ApplyConfiguration(new EmployeeCertificationMap());
            modelBuilder.ApplyConfiguration(new TrainingProgramMap());
            modelBuilder.ApplyConfiguration(new DutyAreaMap());
            modelBuilder.ApplyConfiguration(new SubdutyAreaMap());
            modelBuilder.ApplyConfiguration(new TaskMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_CategoryMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_SubCategoryMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_TopicMap());
            modelBuilder.ApplyConfiguration(new EnablingObjectiveMap());
            modelBuilder.ApplyConfiguration(new Procedure_IssuingAuthorityMap());
            modelBuilder.ApplyConfiguration(new ProcedureMap());
            modelBuilder.ApplyConfiguration(new SaftyHazard_CategoryMap());
            modelBuilder.ApplyConfiguration(new SaftyHazardMap());
            modelBuilder.ApplyConfiguration(new SaftyHazard_AbatementMap());
            modelBuilder.ApplyConfiguration(new SaftyHazard_ControlMap());
            modelBuilder.ApplyConfiguration(new Procedure_EnablingObjective_LinkMap());
            modelBuilder.ApplyConfiguration(new Procedure_SaftyHazard_LinkMap());
            //modelBuilder.ApplyConfiguration(new EnablingObjective_SaftyHazard_LinkMap());
            //modelBuilder.ApplyConfiguration(new EnablingObjective_Procedure_LinkMap());
            modelBuilder.ApplyConfiguration(new Task_StepMap());
            modelBuilder.ApplyConfiguration(new Task_EnablingObjective_LinkMap());
            //modelBuilder.ApplyConfiguration(new Task_Procedure_LinkMap());
            //modelBuilder.ApplyConfiguration(new Task_SaftyHazard_LinkMap());
            modelBuilder.ApplyConfiguration(new ToolMap());
            modelBuilder.ApplyConfiguration(new ToolGroupMap());
            modelBuilder.ApplyConfiguration(new ToolGroup_ToolMap());
            modelBuilder.ApplyConfiguration(new Task_ToolMap());
            modelBuilder.ApplyConfiguration(new Version_TaskMap());
            modelBuilder.ApplyConfiguration(new Version_Task_StepMap());
            modelBuilder.ApplyConfiguration(new Version_Task_QuestionMap());
            modelBuilder.ApplyConfiguration(new Version_ProcedureMap());
            modelBuilder.ApplyConfiguration(new Version_ToolMap());
            modelBuilder.ApplyConfiguration(new Version_Task_Tool_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_Procedure_Tool_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjectiveMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_Tool_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_SaftyHazardMap());
            modelBuilder.ApplyConfiguration(new Version_SaftyHazard_AbatementMap());
            modelBuilder.ApplyConfiguration(new Version_SaftyHazard_ControlMap());
            modelBuilder.ApplyConfiguration(new Version_Task_EnablingObjective_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_Task_SaftyHazard_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_Procedure_SaftyHazard_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_SaftyHazard_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_Procedure_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_Procedure_EnablingObjective_LinkMap());
            modelBuilder.ApplyConfiguration(new Employee_TaskMap());
            modelBuilder.ApplyConfiguration(new TimesheetMap());
            modelBuilder.ApplyConfiguration(new Task_QuestionMap());
            modelBuilder.ApplyConfiguration(new Task_PositionMap());
            modelBuilder.ApplyConfiguration(new TrainingProgramReview_SupportingDocumentMap());
            modelBuilder.ApplyConfiguration(new ProviderLevelMap());
            modelBuilder.ApplyConfiguration(new ProviderMap());
            modelBuilder.ApplyConfiguration(new ILA_TopicMap());
            modelBuilder.ApplyConfiguration(new ILA_ResourceMap());
            modelBuilder.ApplyConfiguration(new DeliveryMethodMap());
            modelBuilder.ApplyConfiguration(new TrainingTopicMap());
            modelBuilder.ApplyConfiguration(new NercStandardMap());
            modelBuilder.ApplyConfiguration(new TraineeEvaluationTypeMap());
            modelBuilder.ApplyConfiguration(new MetaILAMap());
            modelBuilder.ApplyConfiguration(new SegmentMap());
            modelBuilder.ApplyConfiguration(new AssessmentToolMap());
            modelBuilder.ApplyConfiguration(new RR_IssuingAuthorityMap());
            modelBuilder.ApplyConfiguration(new RegulatoryRequirementMap());
            //modelBuilder.ApplyConfiguration(new RR_SafetyHazard_LinkMap());
            modelBuilder.ApplyConfiguration(new ILAMap());
            modelBuilder.ApplyConfiguration(new ILA_NercStandard_LinkMap());
            modelBuilder.ApplyConfiguration(new ILA_SafetyHazard_LinkMap());
            modelBuilder.ApplyConfiguration(new ILA_Segment_LinkMap());
            modelBuilder.ApplyConfiguration(new ILACollaboratorMap());
            modelBuilder.ApplyConfiguration(new ILA_Position_LinkMap());
            modelBuilder.ApplyConfiguration(new RR_Task_LinkMap());
            modelBuilder.ApplyConfiguration(new ILA_TaskObjective_LinkMap());
            modelBuilder.ApplyConfiguration(new ILA_EnablingObjective_LinkMap());
            modelBuilder.ApplyConfiguration(new ILA_Procedure_LinkMap());
            modelBuilder.ApplyConfiguration(new ILA_TrainingTopic_LinkMap());
            modelBuilder.ApplyConfiguration(new ILA_RegRequirement_LinkMap());
            modelBuilder.ApplyConfiguration(new ILA_AssessmentTool_LinkMap());
            modelBuilder.ApplyConfiguration(new RegRequirement_EO_LinkMap());
            modelBuilder.ApplyConfiguration(new Procedure_ILA_LinkMap());
            modelBuilder.ApplyConfiguration(new Procedure_RR_LinkMap());
            modelBuilder.ApplyConfiguration(new SaftyHazard_RR_LinkMap());
            //modelBuilder.ApplyConfiguration(new SafetyHazard_Procedure_LinkMap());
            modelBuilder.ApplyConfiguration(new ILA_PreRequisite_LinkMap());
            modelBuilder.ApplyConfiguration(new TrainingTopic_CategoryMap());
            modelBuilder.ApplyConfiguration(new NERCTargetAudienceMaps());
            modelBuilder.ApplyConfiguration(new RatingScaleMap());
            modelBuilder.ApplyConfiguration(new StudentEvaluationAvailabilityMap());
            modelBuilder.ApplyConfiguration(new ILA_NERCAudience_LinkMap());
            modelBuilder.ApplyConfiguration(new StudentEvaluationFormMap());
            modelBuilder.ApplyConfiguration(new ILA_StudentEvaluation_LinkMap());
            modelBuilder.ApplyConfiguration(new StudentEvaluationQuestionMap());
            modelBuilder.ApplyConfiguration(new Meta_ILAMembers_LinkMap());
            modelBuilder.ApplyConfiguration(new MetaILA_Employee_MemberLinkFufillmentMap());
            modelBuilder.ApplyConfiguration(new CoverSheetTypeMap());
            modelBuilder.ApplyConfiguration(new NercStandardMemberMap());
            modelBuilder.ApplyConfiguration(new CollaboratorInvitationMap());
            modelBuilder.ApplyConfiguration(new CoversheetMap());
            modelBuilder.ApplyConfiguration(new CustomEnablingObjectiveMap());
            modelBuilder.ApplyConfiguration(new SegmentObjective_LinkMap());
            modelBuilder.ApplyConfiguration(new StudentEvaluationAudienceMap());
            modelBuilder.ApplyConfiguration(new ILACustomObjective_LinkMap());
            modelBuilder.ApplyConfiguration(new TaxonomyLevelMap());
            modelBuilder.ApplyConfiguration(new TestStatusMap());
            modelBuilder.ApplyConfiguration(new ImageMap());
            modelBuilder.ApplyConfiguration(new TestMap());
            modelBuilder.ApplyConfiguration(new TestTypeMap());
            modelBuilder.ApplyConfiguration(new TestSettingMap());
            modelBuilder.ApplyConfiguration(new TestItemTypeMap());
            modelBuilder.ApplyConfiguration(new TestItemMap());
            modelBuilder.ApplyConfiguration(new TestItemTrueFalseMap());
            modelBuilder.ApplyConfiguration(new ILATraineeEvaluationMap());
            modelBuilder.ApplyConfiguration(new TestItemMatchMap());
            modelBuilder.ApplyConfiguration(new TestItemMCQMap());
            modelBuilder.ApplyConfiguration(new TestItemFillBlankMap());
            modelBuilder.ApplyConfiguration(new ILA_UploadMap());
            modelBuilder.ApplyConfiguration(new TestItemShortAnswerMap());
            modelBuilder.ApplyConfiguration(new Test_Item_LinkMap());
            modelBuilder.ApplyConfiguration(new Proc_IssuingAuthority_HistoryMap());
            modelBuilder.ApplyConfiguration(new Procedure_Task_LinkMap());
            modelBuilder.ApplyConfiguration(new Procedure_StatusHistoryMap());
            //modelBuilder.ApplyConfiguration(new RR_Procedure_LinkMap());
            modelBuilder.ApplyConfiguration(new RR_StatusHistoryMap());
            modelBuilder.ApplyConfiguration(new SafetyHazard_EO_LinkMap());
            modelBuilder.ApplyConfiguration(new SafetyHazard_Task_LinkMap());
            modelBuilder.ApplyConfiguration(new SafetyHazard_SetMap());
            modelBuilder.ApplyConfiguration(new SafetyHazard_Set_LinkMap());
            modelBuilder.ApplyConfiguration(new SafetyHazard_HistoryMap());
            modelBuilder.ApplyConfiguration(new SafetyHazard_CategoryHistoryMap());
            modelBuilder.ApplyConfiguration(new Task_ReferenceMap());
            modelBuilder.ApplyConfiguration(new Task_Reference_LinkMap());
            modelBuilder.ApplyConfiguration(new Task_ILA_LinkMap());
            //modelBuilder.ApplyConfiguration(new Task_RR_LinkMap());
            modelBuilder.ApplyConfiguration(new Task_CollaboratorInvitationMap());
            modelBuilder.ApplyConfiguration(new Task_Collaborator_LinkMap());
            modelBuilder.ApplyConfiguration(new DiscussionQuestionMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_CategoryHistoryMap());
            modelBuilder.ApplyConfiguration(new RR_IssuingAuthority_StatusHistoryMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_TopicHistoryMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_SubCategoryHistoryMap());
            modelBuilder.ApplyConfiguration(new EnablingObjectiveHistoryMap());
            modelBuilder.ApplyConfiguration(new ILAHistoryMap());
            modelBuilder.ApplyConfiguration(new ToolCategoryMap());
            modelBuilder.ApplyConfiguration(new SafetyHazard_Tool_LinkMap());
            modelBuilder.ApplyConfiguration(new Tool_StatusHistoryMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenarioDifficultyLevelMap_Old());
            modelBuilder.ApplyConfiguration(new SimulationScenarioSpecLookUpMap_Old());
            modelBuilder.ApplyConfiguration(new SimulatorScenarioMap_Old());
            modelBuilder.ApplyConfiguration(new SimulatorScenarioILA_LinkMap_Old());
            modelBuilder.ApplyConfiguration(new SimulatorScenarioPositon_LinkMap_Old());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_EnablingObjectives_LinkMap_Old());
            modelBuilder.ApplyConfiguration(new SimulatorScenarioTaskObjectives_LinkMap_Old());
            modelBuilder.ApplyConfiguration(new Instructor_CategoryMap());
            modelBuilder.ApplyConfiguration(new Instructor_CategoryHistoryMap());
            modelBuilder.ApplyConfiguration(new Instructor_Map());
            modelBuilder.ApplyConfiguration(new Instructor_HistoryMap());
            modelBuilder.ApplyConfiguration(new Location_CategoryMap());
            modelBuilder.ApplyConfiguration(new Location_CategoryHistoryMap());
            modelBuilder.ApplyConfiguration(new Location_Map());
            modelBuilder.ApplyConfiguration(new Location_HistoryMap());
            modelBuilder.ApplyConfiguration(new Task_HistoryMap());
            modelBuilder.ApplyConfiguration(new Task_MetaTask_LinkMap());
            modelBuilder.ApplyConfiguration(new Task_SuggestionMap());
            modelBuilder.ApplyConfiguration(new TrainingGroup_CategoryMap());
            modelBuilder.ApplyConfiguration(new TrainingGroupMap());
            modelBuilder.ApplyConfiguration(new Task_TrainingGroupMap());
            modelBuilder.ApplyConfiguration(new Version_ILAMap());
            modelBuilder.ApplyConfiguration(new Version_Task_ILA_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_RegulatoryRequirementMap());
            modelBuilder.ApplyConfiguration(new Version_Task_RR_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_TrainingGroupMap());
            modelBuilder.ApplyConfiguration(new Version_Task_TrainingGroupMap());
            modelBuilder.ApplyConfiguration(new Position_HistoryMap());
            modelBuilder.ApplyConfiguration(new Position_TaskMap());
            modelBuilder.ApplyConfiguration(new R5ImpactedTasksMap());
            modelBuilder.ApplyConfiguration(new Positions_SQMap());
            modelBuilder.ApplyConfiguration(new Position_EmployeeMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_MetaEO_LinkMap());
            modelBuilder.ApplyConfiguration(new TestItem_HistoryMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_TaskMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_ILALinkMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_RRLinkMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_MetaEOLinkMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_StepMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_Employee_LinkMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_Roster_ResponseMap());
            modelBuilder.ApplyConfiguration(new Version_PositionMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_Position_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_EmployeeMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_Employee_LinkMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_QuestionMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_QuestionMap());
            modelBuilder.ApplyConfiguration(new Version_TestItemsMap());
            modelBuilder.ApplyConfiguration(new Version_Task_SuggestionMap());
            modelBuilder.ApplyConfiguration(new Version_Task_Position_LinkMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_StepMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_SuggestionMap());
            modelBuilder.ApplyConfiguration(new EnablingObjective_ToolMap());
            modelBuilder.ApplyConfiguration(new Version_EnablingObjective_SuggestionsMap());
            modelBuilder.ApplyConfiguration(new EmployeeCertificationHistoryMap());
            modelBuilder.ApplyConfiguration(new CertificationIssuingAuthorityMap());
            modelBuilder.ApplyConfiguration(new ActivityNotificationMap());
            modelBuilder.ApplyConfiguration(new EmployeeDocumentMap());
            modelBuilder.ApplyConfiguration(new EmployeeHistoryMap());
            modelBuilder.ApplyConfiguration(new DocumentMap());
            modelBuilder.ApplyConfiguration(new DocumentTypeMap());

            modelBuilder.ApplyConfiguration(new ClientSettings_NotificationMap());
            modelBuilder.ApplyConfiguration(new ClientSettings_Notification_AvailableCustomSettingMap());
            modelBuilder.ApplyConfiguration(new ClientSettings_Notification_CustomSettingMap());
            modelBuilder.ApplyConfiguration(new ClientSettings_Notification_StepMap());
            modelBuilder.ApplyConfiguration(new ClientSettings_Notification_Step_AvailableCustomSettingMap());
            modelBuilder.ApplyConfiguration(new ClientSettings_Notification_Step_CustomSettingMap());
            modelBuilder.ApplyConfiguration(new ClientSettings_Notification_Step_RecipientMap());

            modelBuilder.ApplyConfiguration(new NotificationMap());
            modelBuilder.ApplyConfiguration(new MetaIlaSelfPacedReleasedNotificationMap());
            modelBuilder.ApplyConfiguration(new MetaIla_Employee_SelfRegistrationRequiredNotificationMap());
            modelBuilder.ApplyConfiguration(new MetaIla_Employee_RegistrationRequiredNotificationMap());
            modelBuilder.ApplyConfiguration(new MetaIla_CourseworkCompleteNotificationMap());
            modelBuilder.ApplyConfiguration(new MetaIla_Admin_SelfRegistrationRequiredNotificationMap());
            modelBuilder.ApplyConfiguration(new MetaIla_Admin_RegistrationRequiredNotificationMap());
            modelBuilder.ApplyConfiguration(new EMPTestNotificationMap());
            modelBuilder.ApplyConfiguration(new EMPTaskQualitificationEvaluatorNotificationMap());
            modelBuilder.ApplyConfiguration(new EMPTaskQualificationTraineeNotificationMap());
            modelBuilder.ApplyConfiguration(new EMPStudentEvaluationNoticationMap());
            modelBuilder.ApplyConfiguration(new EMPSelfRegistrationApprovalNotificationMap());
            modelBuilder.ApplyConfiguration(new EMPProcedureReviewNotificationMap());
            modelBuilder.ApplyConfiguration(new EMPPretestNotificiationMap());
            modelBuilder.ApplyConfiguration(new EMPOnlineCourseNotificationMap());
            modelBuilder.ApplyConfiguration(new EMPLoginNotificationMap());
            modelBuilder.ApplyConfiguration(new EMPIdpReviewNotificationMap());
            modelBuilder.ApplyConfiguration(new EmpCourseNotificationMap());
            modelBuilder.ApplyConfiguration(new ClassScheduleNotificationMap());
            modelBuilder.ApplyConfiguration(new CertificationExpiringNotificationMap());

            modelBuilder.ApplyConfiguration(new ClientSettings_GeneralSettingMap());
            modelBuilder.ApplyConfiguration(new ClientSettings_LicenseMap());
            modelBuilder.ApplyConfiguration(new ClientSettings_LabelReplacementsMap());
            modelBuilder.ApplyConfiguration(new ClientUserSettings_DashboardSettingMap());
            modelBuilder.ApplyConfiguration(new Dashboard_SettingMap());

            modelBuilder.ApplyConfiguration(new TrainingPrograms_ILA_LinkMap());
            modelBuilder.ApplyConfiguration(new TrainingProgramTypeMap());
            modelBuilder.ApplyConfiguration(new TrainingProgram_HistoryMap());
            modelBuilder.ApplyConfiguration(new Version_TrainingProgramMap());
            modelBuilder.ApplyConfiguration(new Version_TrainingProgram_ILA_LinkMap());

            modelBuilder.ApplyConfiguration(new RatingScaleNMap());
            modelBuilder.ApplyConfiguration(new StudentEvaluationMap());
            modelBuilder.ApplyConfiguration(new StudentEvaluationHistoryMap());
            modelBuilder.ApplyConfiguration(new QuestionBankHistoryMap());
            modelBuilder.ApplyConfiguration(new QuestionBankMap());
            modelBuilder.ApplyConfiguration(new StudentEvaluation_QuestionMap());
            modelBuilder.ApplyConfiguration(new Version_TestStausMap());
            modelBuilder.ApplyConfiguration(new Version_TestMap());
            modelBuilder.ApplyConfiguration(new Test_HistoryMap());
            modelBuilder.ApplyConfiguration(new DutyArea_HistoryMap());
            modelBuilder.ApplyConfiguration(new SubDutyArea_HistoryMap());
            modelBuilder.ApplyConfiguration(new EvaluationMethodMap());
            modelBuilder.ApplyConfiguration(new TaskQualificationMap());
            modelBuilder.ApplyConfiguration(new TaskQualification_Evaluator_LinkMap());
            modelBuilder.ApplyConfiguration(new TaskQualificationStatusMap());
            modelBuilder.ApplyConfiguration(new TQEmpSettingMap());

            modelBuilder.ApplyConfiguration(new ReportMap());
            modelBuilder.ApplyConfiguration(new ReportDisplayColumnMap());
            modelBuilder.ApplyConfiguration(new ReportFilterMap());
            modelBuilder.ApplyConfiguration(new ReportSkeletonColumnMap());
            modelBuilder.ApplyConfiguration(new ReportSkeletonFilterMap());
            modelBuilder.ApplyConfiguration(new ReportSkeletonMap());
            modelBuilder.ApplyConfiguration(new ReportSkeleton_CategoryMap());
            modelBuilder.ApplyConfiguration(new ReportSkeleton_SubcategoryMap());
            modelBuilder.ApplyConfiguration(new ReportSkeleton_Subcategory_ReportMap());

            modelBuilder.ApplyConfiguration(new ClassScheduleMap());
            modelBuilder.ApplyConfiguration(new ClassScheduleHistoryMap());
            modelBuilder.ApplyConfiguration(new CBTMap());
            modelBuilder.ApplyConfiguration(new EvaluationReleaseEMPSettingsMap());
            modelBuilder.ApplyConfiguration(new TestReleaseEMPSettingsMap());
            modelBuilder.ApplyConfiguration(new TestReleaseEMPSetting_Retake_LinkMap());
            modelBuilder.ApplyConfiguration(new SelfRegistrationOptionsMap());
            modelBuilder.ApplyConfiguration(new ClassScheduleEmployeeMap());
            modelBuilder.ApplyConfiguration(new TQILAEmpSettingMap());
            modelBuilder.ApplyConfiguration(new ILA_Evaluator_LinkMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_Roster_StatusesMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_RosterMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_RecurrenceMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_StudentEvaluations_LinkMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_Roster_SelectionMap());
            modelBuilder.ApplyConfiguration(new IDPMap());
            modelBuilder.ApplyConfiguration(new IDP_ReviewStatusMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_Evaluation_RosterMap());
            modelBuilder.ApplyConfiguration(new RatingScaleExpandedMap());
            modelBuilder.ApplyConfiguration(new Version_Task_MetaTask_LinkMap());
            modelBuilder.ApplyConfiguration(new ProcedureReviewMap());
            modelBuilder.ApplyConfiguration(new MetaILAConfigurationPublishOptionMap());
            modelBuilder.ApplyConfiguration(new Version_MetaILAMap());
            modelBuilder.ApplyConfiguration(new MetaILA_StatusMap());
            modelBuilder.ApplyConfiguration(new ILA_PerformTraineeEvalMap());
            modelBuilder.ApplyConfiguration(new CertificationSubRequirementMap());
            modelBuilder.ApplyConfiguration(new StudentEvaluationWithoutEmpMap());
            modelBuilder.ApplyConfiguration(new NotificationRecipietMap());
            modelBuilder.ApplyConfiguration(new MetaILA_SummaryTestMap());
            modelBuilder.ApplyConfiguration(new MetaILA_EmployeeMap());
            modelBuilder.ApplyConfiguration(new NotificationRecipietMap()); 
            modelBuilder.ApplyConfiguration(new TrainingProgramReviewsMap());
            modelBuilder.ApplyConfiguration(new TrainingProgramReview_Employee_LinkMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_DesignDefaultViewMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILA_DesignMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILA_DevelopMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILA_ImplementMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_DelieveryMethodsMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_EnablingObjectivesMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_NERCMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_PrerequistieMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_ProceduresMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_ResourcesMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_SafetyHazardsMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_SegmentsMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_TargetAudienceMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_TasksMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesign_TrainingTopicsMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADesignReviewersMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILADevelopReviewersMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILAEvaluationMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILAEvaluation_DefaultViewMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILAEvaluation_TestAnalysisMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILAImplement_ClassScheduleMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILAImplementReviewersMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_PhasesMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ProspectiveILAMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ProspectiveILA_ArchivesMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ProspectiveILA_SnapshotMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_Segments_NercStandardsMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_Segments_NercStandardsMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_TrainingTopicsMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_TrainingTopicsHeadingMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ILAEvaluation_TrainingIssuesMap());
            modelBuilder.ApplyConfiguration(new InstructorWorkbook_ProspectiveILA_ReviewersMap());
            modelBuilder.ApplyConfiguration(new ILA_Topic_LinkMap());

            modelBuilder.ApplyConfiguration(new DifSurveyMap());
            modelBuilder.ApplyConfiguration(new DIFSurvey_DevStatusMap());
            modelBuilder.ApplyConfiguration(new DIFSurvey_EmployeeMap());
            modelBuilder.ApplyConfiguration(new DIFSurvey_Employee_StatusMap());
            modelBuilder.ApplyConfiguration(new DIFSurvey_Employee_ResponseMap());
            modelBuilder.ApplyConfiguration(new DIFSurvey_TaskMap());
            modelBuilder.ApplyConfiguration(new DIFSurvey_Task_StatusMap());
            modelBuilder.ApplyConfiguration(new DIFSurvey_Task_TrainingFrequencyMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_Roster_ResponseMap());
            modelBuilder.ApplyConfiguration(new EmpSettingsReleaseTypeMap());

            modelBuilder.ApplyConfiguration(new TaskListReviewMap());
            modelBuilder.ApplyConfiguration(new TaskListReview_GeneralReviewerMap());
            modelBuilder.ApplyConfiguration(new TaskListReview_TypeMap());
            modelBuilder.ApplyConfiguration(new TaskListReview_StatusMap());
            modelBuilder.ApplyConfiguration(new TaskReviewMap());
            modelBuilder.ApplyConfiguration(new TaskReview_StatusMap());
            modelBuilder.ApplyConfiguration(new TaskReview_ReviewerMap());
            modelBuilder.ApplyConfiguration(new TaskReview_FindingMap());
            modelBuilder.ApplyConfiguration(new ActionItemMap());
            modelBuilder.ApplyConfiguration(new ActionItem_PriorityMap());
            modelBuilder.ApplyConfiguration(new ActionItem_SubDuty_OperationMap());
            modelBuilder.ApplyConfiguration(new ActionItem_Step_OperationMap());
            modelBuilder.ApplyConfiguration(new ActionItem_QuestionAndAnswer_OperationMap());
            modelBuilder.ApplyConfiguration(new ActionItem_Suggestion_OperationMap());
            modelBuilder.ApplyConfiguration(new ActionItem_Tool_OperationMap());
            modelBuilder.ApplyConfiguration(new ActionItem_EnablingObjective_OperationMap());
            modelBuilder.ApplyConfiguration(new ActionItem_Procedure_OperationMap());
            modelBuilder.ApplyConfiguration(new ActionItem_RegulatoryRequirement_OperationMap());
            modelBuilder.ApplyConfiguration(new ActionItem_SafetyHazard_OperationMap());
            modelBuilder.ApplyConfiguration(new ActionItem_OperationType_LinksMap());
            modelBuilder.ApplyConfiguration(new ActionItem_OperationTypesMap());

            modelBuilder.ApplyConfiguration(new SimulatorScenarioMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_DifficultyMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_PositionMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_TaskMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_EnablingObjectiveMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_ProcedureMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_Task_CriteriaMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_ILAMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_PrerequisiteMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_CollaboratorMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_CollaboratorPermissionMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_StatusMap());

            modelBuilder.ApplyConfiguration(new TrainingIssueMap());
            modelBuilder.ApplyConfiguration(new TrainingIssue_StatusMap());
            modelBuilder.ApplyConfiguration(new TrainingIssue_SeverityMap());
            modelBuilder.ApplyConfiguration(new TrainingIssue_DriverTypeMap());
            modelBuilder.ApplyConfiguration(new TrainingIssue_DriverSubTypeMap());
            modelBuilder.ApplyConfiguration(new TrainingIssue_DataElementMap());
            modelBuilder.ApplyConfiguration(new TrainingIssue_ActionItemStatusMap());
            modelBuilder.ApplyConfiguration(new TrainingIssue_ActionItemPriorityMap());
            modelBuilder.ApplyConfiguration(new TrainingIssue_ActionItemMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_Roster_TimeRecordMap());

            modelBuilder.ApplyConfiguration(new ClassSchedule_TestReleaseEMPSettingsMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_TestReleaseEMPSetting_Retake_LinksMap());

            modelBuilder.ApplyConfiguration(new ClassSchedule_TQEMPSettingMap());
            modelBuilder.ApplyConfiguration(new ClassSchedule_Evaluator_LinkMap());

            modelBuilder.ApplyConfiguration(new CBT_ScormRegistrationMap());
            modelBuilder.ApplyConfiguration(new CBT_ScormRegistration_ResponseMap());
            modelBuilder.ApplyConfiguration(new CBT_ScormUpload_QuestionMap());
            modelBuilder.ApplyConfiguration(new CBT_ScormUpload_Question_ChoiceMap());
            modelBuilder.ApplyConfiguration(new CBT_ScormUpload_Question_ChoiceMap());

            modelBuilder.ApplyConfiguration(new PersonActivityNotificationMap());

            modelBuilder.ApplyConfiguration(new TrainingProgramReview_TrainingIssue_LinkMap());
            modelBuilder.ApplyConfiguration(new PublicClassScheduleRequestMap());
            modelBuilder.ApplyConfiguration(new AdminMessageMap());
            modelBuilder.ApplyConfiguration(new AdminMessage_QTDUsersMap());
            modelBuilder.ApplyConfiguration(new PublicClassScheduleRequestNotificationMap());
            modelBuilder.ApplyConfiguration(new PublicClassScheduleRequestAcceptedNotificationMap());

            modelBuilder.ApplyConfiguration(new ClassScheduleEmployee_ILACertificationLink_PartialCreditMap());
            modelBuilder.ApplyConfiguration(new ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditMap());

            modelBuilder.ApplyConfiguration(new TaskListReview_PositionLinkMap());

            modelBuilder.ApplyConfiguration(new SkillQualificationMap());
            modelBuilder.ApplyConfiguration(new SkillQualificationStatusMap());
            modelBuilder.ApplyConfiguration(new SkillQualification_Evaluator_LinkMap());
            modelBuilder.ApplyConfiguration(new SkillQualificationEmpSettingMap());
            modelBuilder.ApplyConfiguration(new SkillQualificationEmp_SignOffMap());
            modelBuilder.ApplyConfiguration(new SkillReQualificationEmp_QuestionAnswerMap());
            modelBuilder.ApplyConfiguration(new SkillReQualificationEmp_SuggestionMap());
            modelBuilder.ApplyConfiguration(new SkillReQualificationEmp_StepMap());

            modelBuilder.ApplyConfiguration(new SimulatorScenario_ScriptMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_EventMap());
            modelBuilder.ApplyConfiguration(new SimulatorScenario_Script_CriteriaMap());

            modelBuilder.ApplyConfiguration(new EMPSkillQualitificationEvaluatorNotificationMap());
            modelBuilder.ApplyConfiguration(new EMPSkillQualificationTraineeNotificationMap());
        }

        //Add DBSet
        public DbSet<ToolCategory_StatusHistory> ToolCategory_StatusHistorys { get; set; }
        public DbSet<ILACertificationLink> ILACertificationLinks { get; set; }
        public DbSet<ILACertificationSubRequirementLink> ILACertificationSubRequirementLinks { get; set; }
        public DbSet<ProcedureReview_Employee> ProcedureReview_Employees { get; set; }
        public DbSet<TaskReQualificationEmp_SignOff> TaskReQualificationEmp_SignOffs { get; set; }
        public DbSet<TaskReQualificationEmp_QuestionAnswer> TaskReQualificationEmp_QuestionAnswers { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<ClientUser> ClientUsers { get; set; }

        public DbSet<QTDUser> QTDUsers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<RatingScaleExpanded> RatingScaleExpanded { get; set; }
        public DbSet<ClassSchedule_Evaluation_Roster> ClassSchedule_Evaluation_Roster { get; set; }

        public DbSet<CertifyingBody> CertifyingBodies { get; set; }

        public DbSet<Certification> Certifications { get; set; }

        public DbSet<EmployeePosition> EmployeePositions { get; set; }

        public DbSet<EmployeeOrganization> EmployeeOrganizations { get; set; }

        public DbSet<EmployeeCertification> EmployeeCertifications { get; set; }

        public DbSet<TrainingProgram> TrainingPrograms { get; set; }

        public DbSet<DutyArea> DutyAreas { get; set; }

        public DbSet<SubdutyArea> SubdutyAreas { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<EnablingObjective_Category> EnablingObjective_Categories { get; set; }

        public DbSet<EnablingObjective_SubCategory> EnablingObjective_SubCategories { get; set; }

        public DbSet<EnablingObjective_Topic> EnablingObjective_Topics { get; set; }

        public DbSet<EnablingObjective> EnablingObjectives { get; set; }

        public DbSet<Procedure_IssuingAuthority> Procedure_IssuingAuthorities { get; set; }

        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<SaftyHazard_Category> SaftyHazard_Categories { get; set; }

        public DbSet<SaftyHazard> SaftyHazards { get; set; }

        public DbSet<SaftyHazard_Abatement> SaftyHazard_Abatements { get; set; }

        public DbSet<SaftyHazard_Control> SaftyHazard_Controls { get; set; }

        public DbSet<Procedure_EnablingObjective_Link> Procedure_EnablingObjective_Links { get; set; }

        public DbSet<Procedure_SaftyHazard_Link> Procedure_SaftyHazard_Links { get; set; }

        //public DbSet<EnablingObjective_SaftyHazard_Link> EnablingObjective_SaftyHazard_Links { get; set; }

        //public DbSet<EnablingObjective_Procedure_Link> EnablingObjective_Procedure_Links { get; set; }

        public DbSet<Task_Step> Task_Steps { get; set; }

        public DbSet<Task_EnablingObjective_Link> Task_EnablingObjective_Links { get; set; }

        //public DbSet<Task_Procedure_Link> Task_Procedure_Links { get; set; }

        //public DbSet<Task_SaftyHazard_Link> Task_SaftyHazard_Links { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<ToolGroup> ToolGroups { get; set; }

        public DbSet<ToolGroup_Tool> ToolGroup_Tools { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Task_Tool> Task_Tools { get; set; }

        public DbSet<Version_Task> Version_Tasks { get; set; }

        public DbSet<Version_Task_Step> Version_Task_Steps { get; set; }

        public DbSet<Version_Task_Question> Version_Task_Questions { get; set; }

        public DbSet<Version_Procedure> Version_Procedures { get; set; }

        public DbSet<Version_Task_Procedure_Link> Version_Task_Procedure_Links { get; set; }

        public DbSet<Version_Tool> Version_Tools { get; set; }

        public DbSet<Version_Task_Tool_Link> Version_Task_Tool_Links { get; set; }

        public DbSet<Version_Procedure_Tool_Link> Version_Procedure_Tool_Links { get; set; }

        public DbSet<Version_EnablingObjective> Version_EnablingObjectives { get; set; }

        public DbSet<Version_EnablingObjective_Tool_Link> Version_EnablingObjective_Tool_Links { get; set; }

        public DbSet<Version_SaftyHazard> Version_SaftyHazards { get; set; }

        public DbSet<Version_SaftyHazard_Abatement> Version_SaftyHazard_Abatements { get; set; }

        public DbSet<Version_SaftyHazard_Control> Version_SaftyHazard_Controls { get; set; }

        public DbSet<Version_Task_EnablingObjective_Link> Version_Task_EnablingObjective_Links { get; set; }

        public DbSet<Version_Task_SaftyHazard_Link> Version_Task_SaftyHazard_Links { get; set; }

        public DbSet<Version_Procedure_SaftyHazard_Link> Version_Procedure_SaftyHazard_Links { get; set; }

        public DbSet<Version_EnablingObjective_SaftyHazard_Link> Version_EnablingObjective_SaftyHazard_Links { get; set; }

        public DbSet<Version_EnablingObjective_Procedure_Link> Version_EnablingObjective_Procedure_Links { get; set; }

        public DbSet<Version_Procedure_EnablingObjective_Link> Version_Procedure_EnablingObjective_Links { get; set; }

        public DbSet<Employee_Task> Employee_Tasks { get; set; }

        public DbSet<Timesheet> Timesheets { get; set; }

        public DbSet<Task_Question> Task_Questions { get; set; }

        public DbSet<Task_Position> Task_Positions { get; set; }

        public DbSet<ProviderLevel> ProviderLevels { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<ILA_Topic> ILA_Topics { get; set; }

        public DbSet<ILA_Resource> ILA_Resources { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        public DbSet<TrainingTopic_Category> TrainingTopic_Categories { get; set; }

        public DbSet<TrainingTopic> TrainingTopics { get; set; }

        public DbSet<NercStandard> NercStandards { get; set; }

        public DbSet<NercStandardMember> NercStandardMembers { get; set; }

        public DbSet<TraineeEvaluationType> TraineeEvaluationTypes { get; set; }

        public DbSet<MetaILA> MetaILAs { get; set; }

        public DbSet<Segment> Segments { get; set; }

        public DbSet<AssessmentTool> AssessmentTools { get; set; }

        public DbSet<RR_IssuingAuthority> RR_IssuingAuthorities { get; set; }

        public DbSet<RegulatoryRequirement> RegulatoryRequirements { get; set; }

        //public DbSet<RR_SafetyHazard_Link> RR_SafetyHazard_Links { get; set; }

        public DbSet<ILA> ILAs { get; set; }

        public DbSet<ILA_NercStandard_Link> ILA_NercStandard_Links { get; set; }

        public DbSet<ILA_SafetyHazard_Link> ILA_SafetyHazard_Links { get; set; }

        public DbSet<ILA_Segment_Link> ILA_Segment_Links { get; set; }

        public DbSet<ILACollaborator> ILACollaborators { get; set; }

        public DbSet<ILA_Position_Link> ILA_Position_Links { get; set; }

        public DbSet<RR_Task_Link> RR_Task_Links { get; set; }

        public DbSet<ILA_TaskObjective_Link> ILA_TaskObjective_Links { get; set; }

        public DbSet<ILA_EnablingObjective_Link> ILA_EnablingObjective_Links { get; set; }

        public DbSet<ILA_Procedure_Link> ILA_Procedure_Links { get; set; }

        public DbSet<ILA_TrainingTopic_Link> ILA_TrainingTopic_Links { get; set; }

        public DbSet<ILA_RegRequirement_Link> ILA_RegRequirement_Links { get; set; }


        public DbSet<ILA_AssessmentTool_Link> ILA_AssessmentTool_Links { get; set; }

        public DbSet<RegRequirement_EO_Link> RegRequirement_EO_Links { get; set; }

        public DbSet<Procedure_ILA_Link> Procedure_ILA_Links { get; set; }

        public DbSet<Procedure_RR_Link> Procedure_RR_Links { get; set; }

        public DbSet<SaftyHazard_RR_Link> SaftyHazard_RR_Links { get; set; }

        //public DbSet<SafetyHazard_Procedure_Link> SafetyHazard_Procedure_Links { get; set; }

        public DbSet<ILA_PreRequisite_Link> ILA_PreRequisite_Links { get; set; }

        public DbSet<NERCTargetAudience> NERCTargetAudiences { get; set; }

        public DbSet<RatingScale> RatingScales { get; set; }

        public DbSet<StudentEvaluationAvailability> StudentEvaluationAvailabilities { get; set; }

        public DbSet<ILA_NERCAudience_Link> ILA_NERCAudience_Links { get; set; }

        public DbSet<StudentEvaluationForm> StudentEvaluationForms { get; set; }

        public DbSet<ILA_StudentEvaluation_Link> ILA_StudentEvaluation_Links { get; set; }

        public DbSet<StudentEvaluationQuestion> StudentEvaluationQuestions { get; set; }

        public DbSet<Meta_ILAMembers_Link> Meta_ILAMembers_Links { get; set; }

        public DbSet<CoverSheetType> CoverSheetTypes { get; set; }

        public DbSet<CollaboratorInvitation> CollaboratorInvitations { get; set; }

        public DbSet<Coversheet> Coversheets { get; set; }

        public DbSet<CustomEnablingObjective> CustomEnablingObjectives { get; set; }

        public DbSet<SegmentObjective_Link> SegmentObjective_Links { get; set; }

        public DbSet<StudentEvaluationAudience> StudentEvaluationAudiences { get; set; }

        public DbSet<ILACustomObjective_Link> ILACustomObjective_Links { get; set; }

        public DbSet<TaxonomyLevel> TaxonomyLevels { get; set; }

        public DbSet<TestStatus> TestStatuses { get; set; }
        public DbSet<IDPSchedule> IDPSchedules { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestType> TestTypes { get; set; }

        public DbSet<TestSetting> TestSettings { get; set; }

        public DbSet<TestItemType> TestItemTypes { get; set; }

        public DbSet<TestItem> TestItems { get; set; }

        public DbSet<TestItemTrueFalse> TestItemTrueFalses { get; set; }

        public DbSet<ILATraineeEvaluation> ILATraineeEvaluations { get; set; }

        public DbSet<TestItemMatch> TestItemMatches { get; set; }

        public DbSet<TestItemMCQ> TestItemMCQs { get; set; }

        public DbSet<ILA_Upload> ILA_Uploads { get; set; }

        public DbSet<TestItemFillBlank> TestItemFillBlanks { get; set; }

        public DbSet<TestItemShortAnswer> TestItemShortAnswers { get; set; }

        public DbSet<Test_Item_Link> Test_Item_Links { get; set; }

        public DbSet<Proc_IssuingAuthority_History> Proc_IssuingAuthority_Histories { get; set; }

        public DbSet<Procedure_Task_Link> Procedure_Task_Links { get; set; }

        public DbSet<Procedure_StatusHistory> Procedure_StatusHistories { get; set; }

        //public DbSet<RR_Procedure_Link> RR_Procedure_Links { get; set; }

        public DbSet<RR_StatusHistory> RR_StatusHistories { get; set; }

        public DbSet<SafetyHazard_EO_Link> SafetyHazard_EO_Links { get; set; }

        public DbSet<SafetyHazard_Task_Link> SafetyHazard_Task_Links { get; set; }

        public DbSet<SafetyHazard_Set> SafetyHazard_Sets { get; set; }

        public DbSet<SafetyHazard_Set_Link> SafetyHazard_Set_Links { get; set; }

        public DbSet<SafetyHazard_History> SafetyHazard_Histories { get; set; }

        public DbSet<SafetyHazard_CategoryHistory> SafetyHazard_CategoryHistories { get; set; }

        public DbSet<Task_Reference> Task_References { get; set; }

        public DbSet<Task_Reference_Link> Task_Reference_Links { get; set; }

        public DbSet<Task_ILA_Link> Task_ILA_Links { get; set; }

        //public DbSet<Task_RR_Link> Task_RR_Links { get; set; }

        public DbSet<Task_CollaboratorInvitation> Task_CollaboratorInvitations { get; set; }

        public DbSet<Task_Collaborator_Link> Task_Collaborator_Links { get; set; }

        public DbSet<DiscussionQuestion> DiscussionQuestions { get; set; }

        public DbSet<EnablingObjective_CategoryHistory> EnablingObjective_CategoryHistories { get; set; }

        public DbSet<RR_IssuingAuthority_StatusHistory> RR_IssuingAuthority_StatusHistories { get; set; }

        public DbSet<EnablingObjective_TopicHistory> EnablingObjective_TopicHistories { get; set; }

        public DbSet<EnablingObjective_SubCategoryHistory> EnablingObjective_SubCategoryHistories { get; set; }

        public DbSet<EnablingObjectiveHistory> EnablingObjectiveHistories { get; set; }

        public DbSet<ILAHistory> ILAHistories { get; set; }

        public DbSet<ToolCategory> ToolCategories { get; set; }

        public DbSet<SafetyHazard_Tool_Link> SafetyHazard_Tool_Links { get; set; }

        public DbSet<Tool_StatusHistory> Tool_StatusHistories { get; set; }

        public DbSet<SimulatorScenarioDifficultyLevel_Old> SimulatorScenarioDifficultyLevels_Old { get; set; }

        public DbSet<SimulationScenarioSpecLookUp_Old> SimulationScenarioSpecLookUps_Old { get; set; }

        public DbSet<SimulatorScenario_Old> SimulatorScenarios_Old { get; set; }

        public DbSet<SimulatorScenarioILA_Link_Old> SimulatorScenarioILA_Links_Old { get; set; }

        public DbSet<SimulatorScenarioPositon_Link_Old> SimulatorScenarioPositon_Links_Old { get; set; }

        public DbSet<SimulatorScenario_EnablingObjectives_Link_Old> SimulatorScenario_EnablingObjectives_Links_Old { get; set; }

        public DbSet<SimulatorScenarioTaskObjectives_Link_Old> SimulatorScenarioTaskObjectives_Links_Old { get; set; }

        public DbSet<Instructor_Category> Instructor_Categories { get; set; }

        public DbSet<Instructor_CategoryHistory> Instructor_CategoryHistories { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Instructor_History> Instructor_Histories { get; set; }

        public DbSet<Location_Category> Location_Categories { get; set; }

        public DbSet<Location_CategoryHistory> Location_CategoryHistories { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Location_History> Location_Histories { get; set; }

        public DbSet<Task_History> Task_Histories { get; set; }

        public DbSet<Task_MetaTask_Link> Task_MetaTask_Links { get; set; }

        public DbSet<Task_Suggestion> Task_Suggestions { get; set; }

        public DbSet<TrainingGroup_Category> TrainingGroup_Categories { get; set; }

        public DbSet<TrainingGroup> TrainingGroups { get; set; }

        public DbSet<Task_TrainingGroup> Task_TrainingGroups { get; set; }

        public DbSet<Version_Task_ILA_Link> Version_Task_ILA_Links { get; set; }

        public DbSet<Version_ILA> Version_ILAs { get; set; }

        public DbSet<Version_RegulatoryRequirement> Version_RegulatoryRequirements { get; set; }

        public DbSet<Version_Task_RR_Link> Version_Task_RR_Links { get; set; }

        public DbSet<Version_TrainingGroup> Version_TrainingGroups { get; set; }

        public DbSet<Version_Task_TrainingGroup> Version_Task_TrainingGroups { get; set; }

        public DbSet<Position_History> Position_Histories { get; set; }

        public DbSet<Position_Task> Position_Tasks { get; set; }

        public DbSet<Positions_SQ> Positions_SQs { get; set; }

        public DbSet<Position_Employee> Position_Employees { get; set; }

        public DbSet<EnablingObjective_MetaEO_Link> EnablingObjective_MetaEO_Links { get; set; }

        public DbSet<TestItem_History> TestItem_Histories { get; set; }

        public DbSet<Version_EnablingObjective_Task> Version_EnablingObjective_Tasks { get; set; }

        public DbSet<Version_EnablingObjective_ILALink> Version_EnablingObjective_ILALinks { get; set; }

        public DbSet<Version_EnablingObjective_RRLink> Version_EnablingObjective_RRLinks { get; set; }

        public DbSet<Version_EnablingObjective_MetaEOLink> Version_EnablingObjective_MetaEOLinks { get; set; }

        public DbSet<EnablingObjective_Step> EnablingObjective_Steps { get; set; }

        public DbSet<EnablingObjective_Employee_Link> EnablingObjective_Employee_Links { get; set; }

        public DbSet<Version_Position> Version_Positions { get; set; }

        public DbSet<Version_EnablingObjective_Position_Link> Version_EnablingObjective_Position_Links { get; set; }

        public DbSet<Version_Employee> Version_Employees { get; set; }

        public DbSet<Version_EnablingObjective_Employee_Link> Version_EnablingObjective_Employee_Links { get; set; }

        public DbSet<EnablingObjective_Question> EnablingObjective_Questions { get; set; }

        public DbSet<Version_EnablingObjective_Question> Version_EnablingObjective_Questions { get; set; }

        public DbSet<Version_TestItems> Version_TestItems { get; set; }

        public DbSet<Version_Task_Suggestion> Version_Task_Suggestions { get; set; }

        public DbSet<Version_Task_Position_Link> Version_Task_Position_Links { get; set; }

        public DbSet<Version_EnablingObjective_Step> Version_EnablingObjective_Steps { get; set; }

        public DbSet<EnablingObjective_Suggestion> EnablingObjective_Suggestions { get; set; }

        public DbSet<EnablingObjective_Tool> EnablingObjective_Tools { get; set; }

        public DbSet<Version_EnablingObjective_Suggestions> Version_EnablingObjective_Suggestions { get; set; }

        public DbSet<EmployeeCertifictaionHistory> EmployeeCertifictaionHistories { get; set; }

        public DbSet<CertificationIssuingAuthority> CertificationIssuingAuthorities { get; set; }

        public DbSet<ActivityNotification> ActivityNotifications { get; set; }

        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }

        public DbSet<EmployeeHistory> EmployeeHistories { get; set; }


        public DbSet<ClientSettings_Notification> ClientSettings_Notifications { get; set; }
        public DbSet<ClientSettings_Notification_AvailableCustomSetting> ClientSettings_Notification_AvailableCustomSettings { get; set; }
        public DbSet<ClientSettings_Notification_CustomSetting> ClientSettings_Notification_CustomSettings { get; set; }
        public DbSet<ClientSettings_Notification_Step> ClientSettings_Notification_Steps { get; set; }
        public DbSet<ClientSettings_Notification_Step_AvailableCustomSetting> ClientSettings_Notification_Step_AvailableCustomSettings { get; set; }
        public DbSet<ClientSettings_Notification_Step_CustomSetting> ClientSettings_Notification_Step_CustomSettings { get; set; }
        public DbSet<ClientSettings_Notification_Step_Recipient> ClientSettings_Notification_Step_Recipients { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<ClientSettings_GeneralSettings> ClientSettings_GeneralSettings { get; set; }
        public DbSet<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public DbSet<ClientSettings_Feature> ClientSettings_Features { get; set; }
        public DbSet<ClientSettings_License> ClientSettings_Licenses { get; set; }


        public DbSet<TrainingProgramType> TrainingProgramTypes { get; set; }

        public DbSet<TrainingProgram_History> TrainingProgram_Histories { get; set; }

        public DbSet<TrainingPrograms_ILA_Link> TrainingPrograms_ILA_Links { get; set; }

        public DbSet<Version_TrainingProgram> Version_TrainingPrograms { get; set; }

        public DbSet<Version_TrainingProgram_ILA_Link> Version_TrainingProgram_ILA_Links { get; set; }

        public DbSet<RatingScaleN> RatingScaleNs { get; set; }
        public DbSet<StudentEvaluation> StudentEvaluations { get; set; }
        public DbSet<StudentEvaluationHistory> StudentEvaluationHistories { get; set; }
        public DbSet<QuestionBank> QuestionBanks { get; set; }
        public DbSet<QuestionBankHistory> QuestionBankHistories { get; set; }
        public DbSet<StudentEvaluation_Question> StudentEvaluation_Questions { get; set; }

        public DbSet<DutyArea_History> DutyArea_Histories { get; set; }

        public DbSet<Version_TestStaus> Version_TestStatuses { get; set; }

        public DbSet<Version_Test> Version_Tests { get; set; }

        public DbSet<Test_History> Test_Histories { get; set; }

        public DbSet<SubDutyArea_History> SubDutyArea_Histories { get; set; }

        public DbSet<EvaluationMethod> EvaluationMethods { get; set; }

        public DbSet<TaskQualification> TaskQualifications { get; set; }

        public DbSet<TaskQualification_Evaluator_Link> TaskQualification_Evaluator_Links { get; set; }

        public DbSet<TaskQualificationStatus> TaskQualificationStatuses { get; set; }

        public DbSet<TQEmpSetting> TQEmpSettings { get; set; }

        public DbSet<ClassSchedule> ClassSchedules { get; set; }

        public DbSet<ClassScheduleHistory> ClassScheduleHistories { get; set; }
        public DbSet<ClassSchedule_SelfRegistrationOptions> ClassSchedule_SelfRegistrationOptions { get; set; }

        public DbSet<CBT> CBTs { get; set; }

        public DbSet<EvaluationReleaseEMPSettings> EvaluationReleaseEMPSettings { get; set; }

        public DbSet<TestReleaseEMPSettings> TestReleaseEMPSettings { get; set; }

        public DbSet<TestReleaseEMPSetting_Retake_Link> TestReleaseEMPSetting_Retake_Links { get; set; }

        public DbSet<ILA_SelfRegistrationOptions> ILA_SelfRegistrationOptions { get; set; }

        public DbSet<ClassSchedule_Employee> ClassScheduleEmployees { get; set; }
        public DbSet<TQILAEmpSetting> TQILAEmpSettings { get; set; }
        public DbSet<ILA_Evaluator_Link> ILA_Evaluator_Links { get; set; }
        public DbSet<ClassSchedule_Roster_Statuses> ClassSchedule_Roster_Statuses { get; set; }
        public DbSet<ClassSchedule_Roster> ClassSchedule_Roster { get; set; }
        public DbSet<ClassSchedule_Recurrence> ClassSchedule_Recurrences { get; set; }
        public DbSet<ClassSchedule_StudentEvaluations_Link> ClassSchedule_StudentEvaluations_Links { get; set; }
        public DbSet<IDP> IDPs { get; set; }
        public DbSet<IDP_ReviewStatus> IDP_ReviewStatuses { get; set; }
        public DbSet<ClientUserSettings_DashboardSetting> ClientUserSettings_DashboardSettings { get; set; }
        public DbSet<DashboardSetting> DashboardSettings { get; set; }
        public DbSet<ClassSchedule_Roster_Response_Selection> ClassSchedule_Roster_Response_Selections { get; set; }
        public DbSet<ReportSkeleton> ReportSkeletons { get; set; }
        public DbSet<ReportSkeletonColumn> ReportSkeletonColumns { get; set; }
        public DbSet<ReportSkeletonFilter> ReportSkeletonFilters { get; set; }
        public DbSet<ReportSkeleton_Category> ReportSkeleton_Categories { get; set; }
        public DbSet<ReportSkeleton_Subcategory> ReportSkeleton_Subcategories { get; set; }
        public DbSet<ReportSkeleton_Subcategory_Report> ReportSkeleton_Subcategory_Reports { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportDisplayColumn> ReportDisplayColumns { get; set; }
        public DbSet<ReportFilter> ReportFilters { get; set; }
        public DbSet<ProcedureReview> ProcedureReviews { get; set; }
        public DbSet<Version_Task_MetaTask_Link> Version_Task_MetaTask_Links { get; set; }
        public DbSet<MetaILAConfigurationPublishOption> MetaILAConfigurationPublishOptions { get; set; }
        public DbSet<Version_MetaILA> Version_MetaILAs { get; set; }
        public DbSet<MetaILA_Status> MetaILA_Statuses { get; set; }
        public DbSet<MetaILA_Employee_MemberLinkFufillment> MetaILA_Employee_MemberLinkFufillments { get; set; }
        public DbSet<ILA_PerformTraineeEval> ILA_PerformTraineeEvals { get; set; }
        public DbSet<CertificationSubRequirement> CertificationSubRequirements { get; set; }
        public DbSet<CBT_ScormRegistration> CBT_ScormRegistration { get; set; }
        public DbSet<CBT_ScormUpload> CBT_ScormUpload { get; set; }
        public DbSet<TaskReQualificationEmp_Steps> TaskReQualificationEmp_Steps { get; set; }
        public DbSet<TaskReQualificationEmp_Suggestion> TaskReQualificationEmp_Suggestions { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<StudentEvaluationWithoutEmp> StudentEvaluationWithoutEmp { get; set; }
        public DbSet<NotificationRecipiet> NotificationRecipiets { get; set; }
        public DbSet<Task_SuggestionTypes> Task_SuggestionTypes { get; set; }
        public DbSet<MetaILA_SummaryTest> MetaILA_SummaryTests { get; set; }
        public DbSet<MetaILA_Employee> MetaILA_Employees { get; set; }
        public DbSet<TrainingProgramReview> TrainingProgramReviews { get; set; }
        public DbSet<TrainingProgramReview_Employee_Link> TrainingProgramReview_Employee_Links { get; set; }
        public DbSet<TrainingProgramReview_SupportingDocument> TrainingProgramReview_SupportingDocuments { get; set; }
        public DbSet<InstructorWorkbook_DesignDefaultView> InstructorWorkbook_DesignDefaultView { get; set; }
        public DbSet<InstructorWorkbook_ILA_Design> InstructorWorkbook_ILA_Design { get; set; }
        public DbSet<InstructorWorkbook_ILA_Develop> InstructorWorkbook_ILA_Develop { get; set; }
        public DbSet<InstructorWorkbook_ILA_Implement> InstructorWorkbook_ILA_Implement { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_DelieveryMethods> InstructorWorkbook_ILADesign_DelieveryMethods { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_EnablingObjectives> InstructorWorkbook_ILADesign_EnablingObjectives { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_NERC> InstructorWorkbook_ILADesign_NERC { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_Prerequistie> InstructorWorkbook_ILADesign_Prerequistie { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_Procedures> InstructorWorkbook_ILADesign_Procedures { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_Resources> InstructorWorkbook_ILADesign_Resources { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_SafetyHazards> InstructorWorkbook_ILADesign_SafetyHazards { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_Segments> InstructorWorkbook_ILADesign_Segments { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_TargetAudience> InstructorWorkbook_ILADesign_TargetAudience { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_Tasks> InstructorWorkbook_ILADesign_Tasks { get; set; }
        public DbSet<InstructorWorkbook_ILADesign_TrainingTopics> InstructorWorkbook_ILADesign_TrainingTopics { get; set; }
        public DbSet<InstructorWorkbook_ILADesignReviewers> InstructorWorkbook_ILADesignReviewers { get; set; }
        public DbSet<InstructorWorkbook_ILADevelopReviewers> InstructorWorkbook_ILADevelopReviewers { get; set; }
        public DbSet<InstructorWorkbook_ILAEvaluation> InstructorWorkbook_ILAEvaluation { get; set; }
        public DbSet<InstructorWorkbook_ILAEvaluation_DefaultView> InstructorWorkbook_ILAEvaluation_DefaultView { get; set; }
        public DbSet<InstructorWorkbook_ILAEvaluation_TestAnalysis> InstructorWorkbook_ILAEvaluation_TestAnalysis { get; set; }
        public DbSet<InstructorWorkbook_ILAImplement_ClassSchedule> InstructorWorkbook_ILAImplement_ClassSchedule { get; set; }
        public DbSet<InstructorWorkbook_ILAImplementReviewers> InstructorWorkbook_ILAImplementReviewers { get; set; }
        public DbSet<InstructorWorkbook_ILAPhases> InstructorWorkbook_ILAPhases { get; set; }
        public DbSet<InstructorWorkbook_Phases> InstructorWorkbook_Phases { get; set; }
        public DbSet<InstructorWorkbook_ProspectiveILA> InstructorWorkbook_ProspectiveILA { get; set; }
        public DbSet<InstructorWorkbook_ProspectiveILA_Archives> InstructorWorkbook_ProspectiveILA_Archives { get; set; }
        public DbSet<InstructorWorkbook_ProspectiveILA_Snapshot> InstructorWorkbook_ProspectiveILA_Snapshot { get; set; }
        public DbSet<InstructorWorkbook_Segments_LinkObjectives> InstructorWorkbook_Segments_LinkObjectives { get; set; }
        public DbSet<InstructorWorkbook_Segments_NercStandards> InstructorWorkbook_Segments_NercStandards { get; set; }
        public DbSet<InstructorWorkbook_TrainingTopics> InstructorWorkbook_TrainingTopics { get; set; }
        public DbSet<InstructorWorkbook_TrainingTopicsHeading> InstructorWorkbook_TrainingTopicsHeading { get; set; }
        public DbSet<InstructorWorkbook_ILAEvaluation_TrainingIssues> InstructorWorkbook_ILAEvaluation_TrainingIssues { get; set; }
        public DbSet<InstructorWorkbook_ProspectiveILA_Reviewers> InstructorWorkbook_ProspectiveILA_Reviewers { get; set; }
        public DbSet<ILA_Topic_Link> ILA_Topic_Links { get; set; }
        public DbSet<DIFSurvey> DIFSurvey { get; set; }
        public DbSet<DIFSurvey_DevStatus> DIFSurvey_DevStatus { get; set; }
        public DbSet<DIFSurvey_Employee> DIFSurvey_Employee { get; set; }
        public DbSet<DIFSurvey_Employee_Status> DIFSurvey_Employee_Status { get; set; }
        public DbSet<DIFSurvey_Employee_Response> DIFSurvey_Employee_Response { get; set; }
        public DbSet<DIFSurvey_Task> DIFSurvey_Task { get; set; }
        public DbSet<DIFSurvey_Task_Status> DIFSurvey_Task_Status { get; set; }
        public DbSet<EmpSettingsReleaseType> EmpSettingsReleaseTypes { get; set; }
        public DbSet<SimulatorScenario> SimulatorScenarios { get; set; }
        public DbSet<SimulatorScenario_Difficulty> SimulatorScenario_Difficultys { get; set; }
        public DbSet<SimulatorScenario_Position> SimulatorScenario_Positions { get; set; }
        public DbSet<SimulatorScenario_Task> SimulatorScenario_Tasks { get; set; }
        public DbSet<SimulatorScenario_EnablingObjective> SimulatorScenario_EnablingObjectives { get; set; }
        public DbSet<SimulatorScenario_Procedure> SimulatorScenario_Procedures { get; set; }
        public DbSet<SimulatorScenario_Task_Criteria> SimulatorScenario_Task_Criterias { get; set; }
        public DbSet<SimulatorScenario_ILA> SimulatorScenario_ILAs { get; set; }
        public DbSet<SimulatorScenario_Prerequisite> SimulatorScenario_Prerequisites { get; set; }
        public DbSet<SimulatorScenario_Collaborator> SimulatorScenario_Collaborators { get; set; }
        public DbSet<SimulatorScenario_CollaboratorPermission> SimulatorScenario_CollaboratorPermissions { get; set; }
        public DbSet<SimulatorScenario_Status> SimulatorScenario_Status { get; set; }
        public DbSet<TrainingIssue> TrainingIssues { get; set; }
        public DbSet<TrainingIssue_ActionItem> TrainingIssueActionItems { get; set; }
        public DbSet<TrainingIssue_ActionItemPriority> TrainingIssueActionItemPriorities { get; set; }
        public DbSet<TrainingIssue_ActionItemStatus> TrainingIssueActionItemStatuses { get; set; }
        public DbSet<TrainingIssue_DriverSubType> TrainingIssueDriverSubTypes { get; set; }
        public DbSet<TrainingIssue_DriverType> TrainingIssueDriverTypes { get; set; }
        public DbSet<TrainingIssue_Severity> TrainingIssueSeverities { get; set; }
        public DbSet<TrainingIssue_Status> TrainingIssueStatuses { get; set; }
        public DbSet<TrainingIssue_DataElement> TrainingIssueDataElements { get; set; }
        public DbSet<ClassSchedule_Roster_TimeRecord> ClassSchedule_Roster_TimeRecords { get; set; }
        public DbSet<ClassSchedule_TestReleaseEMPSetting> ClassSchedule_TestReleaseEMPSettings { get; set; }
        public DbSet<ClassSchedule_TestReleaseEMPSetting_Retake_Link> ClassSchedule_TestReleaseEMPSetting_Retake_Links { get; set; }
        public DbSet<ClassSchedule_TQEMPSetting> ClassSchedule_TQEMPSettings { get; set; }
        public DbSet<ClassSchedule_Evaluator_Link> ClassSchedule_Evaluator_Links { get; set; }
        public DbSet<CBT_ScormRegistration_Response> CBT_ScormRegistration_Responses { get; set; }
        public DbSet<CBT_ScormUpload_Question> CBT_ScormUpload_Questions { get; set; }
        public DbSet<CBT_ScormUpload_Question_Choice> cBT_ScormUpload_Question_Choices { get; set; }
        public DbSet<PersonActivityNotification> PersonActivityNotifications { get; set; }
        public DbSet<TrainingProgramReview_TrainingIssue_Link> TrainingProgramReview_TrainingIssue_Links { get; set; }
        public DbSet<PublicClassScheduleRequest> PublicClassScheduleRequests { get; set; }
        public DbSet<AdminMessage> AdminMessages { get; set; }
        public DbSet<AdminMessage_QTDUser> AdminMessage_QTDUsers { get; set; }
        public DbSet<ClassScheduleEmployee_ILACertificationLink_PartialCredit> ClassScheduleEmployee_ILACertificationLink_PartialCredits { get; set; }
        public DbSet<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit> ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits { get; set; }
        public DbSet<TaskListReview_PositionLink> TaskListReview_PositionLinks { get; set; }
        public DbSet<SkillQualification> SkillQualifications { get; set; }
        public DbSet<SkillQualificationStatus> SkillQualificationStatus { get; set; }
        public DbSet<SkillQualification_Evaluator_Link> SkillQualification_Evaluator_Links { get; set; }
        public DbSet<SkillQualificationEmpSetting> SkillQualificationEmpSettings { get; set; }
        public DbSet<SkillQualificationEmp_SignOff> SkillQualificationEmp_SignOffs { get; set; }
        public DbSet<SkillReQualificationEmp_QuestionAnswer> SkillReQualificationEmp_QuestionAnswers { get; set; }
        public DbSet<SkillReQualificationEmp_Suggestion> SkillReQualificationEmp_Suggestions { get; set; }
        public DbSet<SkillReQualificationEmp_Step> SkillReQualificationEmp_Steps { get; set; }
        public DbSet<SimulatorScenario_Script> SimulatorScenario_Scripts { get; set; }
        public DbSet<SimulatorScenario_Event> SimulatorScenario_Events { get; set; }
        public DbSet<SimulatorScenario_Script_Criteria> SimulatorScenario_Script_Criterias { get; set; }
    }
}
