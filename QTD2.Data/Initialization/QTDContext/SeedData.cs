using System;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;
using QTD2.Domain.Entities.Core;
using System.Security.Claims;

namespace QTD2.Data.Initialization.QTDContext
{
    public partial class SeedData
    {
        private readonly MigrationBuilder _migrationBuilder;
        private readonly Environments _environment;
        private readonly string _path;

        private string prefix
        {
            get { return _environment.ToString() + "_"; }
        }

        public SeedData(string environment, MigrationBuilder migrationBuilder)
        {
            _environment = (Environments)Enum.Parse(typeof(Environments), environment);
            _migrationBuilder = migrationBuilder;
            _path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Initialization\\QTDContext\\Data\\" + _environment;
        }

        public void AddClientUsers()
        {
            callMethod(prefix + "AddClientUsers");
        }

        public void AddEmployeeTable()
        {
            callMethod(prefix + "AddEmployeeTable");
        }

        public void AddEmployeeCertificationsTable()
        {
            callMethod(prefix + "AddEmployeeCertificationsTable");
        }

        public void AddEmployeePositionTable()
        {
            callMethod(prefix + "AddEmployeePositionTable");
        }
        public void AddILA_Position_LinksTable()
        {
            callMethod(prefix + "AddILA_Position_LinksTable");
        }

        public void AddILA_PreRequisite_LinksTable()
        {
            callMethod(prefix + "AddILA_PreRequisite_LinksTable");
        }

        public void AddILA_UploadsTable()
        {
            callMethod(prefix + "AddILA_UploadsTable");
        }
        public void AddILA_NERCAudience_LinksTable()
        {
            callMethod(prefix + "AddILA_NERCAudience_LinksTable");
        }
        public void AddILA_TrainingTopic_LinksTable()
        {
            callMethod(prefix + "AddILA_TrainingTopic_LinksTable");
        }
        public void AddEmployeeOrganizationTable()
        {
            callMethod(prefix + "AddEmployeeOrganizationTable");
        }

        public void AddPositionTable()
        {
            callMethod(prefix + "AddPositionTable");
        }
        public void AddPositionSQsTable()
        {
            callMethod(prefix + "AddPositionSQsTable");
        }

        public void AddOrganizationTable()
        {
            callMethod(prefix + "AddOrganizationTable");
        }

        public void AddPositionHistoriesTable()
        {
            callMethod(prefix + "AddPositionHistoriesTable");
        }

        public void AddTrainingProgramTable()
        {
            callMethod(prefix + "AddTrainingProgramTable");
        }

        public void AddCertifyingBodyTable()
        {
            callMethod(prefix + "AddCertifyingBodyTable");
        }

        public void AddCertificationTable()
        {
            callMethod(prefix + "AddCertificationTable");
        }

        public void AddDutyAreaTable()
        {
            callMethod(prefix + "AddDutyAreaTable");
        }

        public void AddSubdutyAreaTable()
        {
            callMethod(prefix + "AddSubdutyAreaTable");
        }

        public void AddTaskTable()
        {
            callMethod(prefix + "AddTaskTable");
        }
        public void AddTaskQualificationTable()
        {
            callMethod(prefix + "AddTaskQualificationTable");
        }


        public void AddClassScheduleTable()
        {
            callMethod(prefix + "AddClassScheduleTable");
        }
        public void AddClassScheduleEmployeesTable()
        {
            callMethod(prefix + "AddClassScheduleEmployeesTable");
        }

        public void AddEnablingObjectiveTable()
        {
            callMethod(prefix + "AddEnablingObjectiveTable");
        }
        public void AddILA_EnablingObjective_LinksTable()
        {
            callMethod(prefix + "AddILA_EnablingObjective_LinksTable");
        }

        public void AddEnablingObjective_TopicTable()
        {
            callMethod(prefix + "AddEnablingObjective_TopicTable");
        }

        public void AddEnablingObjective_CategoryTable()
        {
            callMethod(prefix + "AddEnablingObjective_CategoryTable");
        }

        public void AddEnablingObjective_SubCategoryTable()
        {
            callMethod(prefix + "AddEnablingObjective_SubCategoryTable");
        }

        public void AddEnablingObjective_Employee_LinksTable()
        {
            callMethod(prefix + "AddEnablingObjective_Employee_LinksTable");
        }

        public void AddProcedure_IssuingAuthorityTable()
        {
            callMethod(prefix + "AddProcedure_IssuingAuthorityTable");
        }

        public void AddProcedureTable()
        {
            callMethod(prefix + "AddProcedureTable");
        }
        public void AddProcedureReviewsTable()
        {
            callMethod(prefix + "AddProcedureReviewsTable");
        }
        public void AddProcedureReview_EmployeesTable()
        {
            callMethod(prefix + "AddProcedureReview_EmployeesTable");
        }

        public void AddSaftyHazard_categoryTable()
        {
            callMethod(prefix + "AddSaftyHazard_categoryTable");
        }

        public void AddSaftyHazardTable()
        {
            callMethod(prefix + "AddSaftyHazardTable");
        }
        public void AddILA_SafetyHazard_LinksTable()
        {
            callMethod(prefix + "AddILA_SafetyHazard_LinksTable");
        }

        public void AddInstructor_Category()
        {
            callMethod(prefix + "AddInstructor_Category");
        }

        public void AddInstructorTable()
        {
            callMethod(prefix + "AddInstructorTable");
        }

        public void AddSaftyHazard_ControlTable()
        {
            callMethod(prefix + "AddSaftyHazard_ControlTable");
        }

        public void AddSaftyHazard_AbatementTable()
        {
            callMethod(prefix + "AddSaftyHazard_AbatementTable");
        }

        public void AddProcedure_SaftyHazard_LinkTable()
        {
            callMethod(prefix + "AddProcedure_SaftyHazard_LinkTable");
        }

        public void AddProcedure_EnablingObjective_LinkTable()
        {
            callMethod(prefix + "AddProcedure_EnablingObjective_LinkTable");
        }

        public void AddEnablingObjective_SaftyHazard_LinkTable()
        {
            callMethod(prefix + "AddEnablingObjective_SaftyHazard_LinkTable");
        }

        public void AddEnablingObjective_Procedure_LinkTable()
        {
            callMethod(prefix + "AddEnablingObjective_Procedure_LinkTable");
        }

        public void AddTask_EnablingObjective_LinkTable()
        {
            callMethod(prefix + "AddTask_EnablingObjective_LinkTable");
        }

        public void AddTask_Procedure_LinkTable()
        {
            callMethod(prefix + "AddTask_Procedure_LinkTable");
        }

        public void AddTask_SaftyHazard_LinkTable()
        {
            callMethod(prefix + "AddTask_SaftyHazard_LinkTable");
        }

        public void AddTask_StepTable()
        {
            callMethod(prefix + "AddTask_StepTable");
        }

        public void AddTask_ToolTable()
        {
            callMethod(prefix + "AddTask_ToolTable");
        }

        public void AddToolGroupsTable()
        {
            callMethod(prefix + "AddToolGroupsTable");
        }

        public void AddToolTable()
        {
            callMethod(prefix + "AddToolTable");
        }

        public void AddToolGroup_ToolsTable()
        {
            callMethod(prefix + "AddToolGroup_ToolsTable");
        }

        public void AddVersion_TaskTable()
        {
            callMethod(prefix + "AddVersion_TaskTable");
        }

        public void AddVersion_Task_StepTable()
        {
            callMethod(prefix + "AddVersion_Task_StepTable");
        }

        public void AddVersion_Task_QuestionTable()
        {
            callMethod(prefix + "AddVersion_Task_QuestionTable");
        }
        public void AddCertificationsHistoryTable()
        {
            callMethod(prefix + "AddCertificationsHistoryTable");

        }
        public void AddVersion_ProcedureTable()
        {
            callMethod(prefix + "AddVersion_ProcedureTable");
        }

        public void AddVersion_Task_Procedure_LinkTable()
        {
            callMethod(prefix + "AddVersion_Task_Procedure_LinkTable");
        }

        public void AddVersion_ToolTable()
        {
            callMethod(prefix + "AddVersion_ToolTable");
        }

        public void AddVersion_Task_Tool_LinkTable()
        {
            callMethod(prefix + "AddVersion_Task_Tool_LinkTable");
        }

        public void AddVersion_Procedure_Tool_LinkTable()
        {
            callMethod(prefix + "AddVersion_Procedure_Tool_LinkTable");
        }

        public void AddVersion_Task_EnablingObjective_LinkTable()
        {
            callMethod(prefix + "AddVersion_Task_EnablingObjective_LinkTable");
        }

        public void AddVersion_EnablingObjective_Tool_LinkTable()
        {
            callMethod(prefix + "AddVersion_EnablingObjective_Tool_LinkTable");
        }

        public void AddVersion_SaftyHazardTable()
        {
            callMethod(prefix + "AddVersion_SaftyHazardTable");
        }

        public void AddVersion_SaftyHazard_AbatementTable()
        {
            callMethod(prefix + "AddVersion_SaftyHazard_AbatementTable");
        }

        public void AddVersion_SaftyHazard_ControlTable()
        {
            callMethod(prefix + "AddVersion_SaftyHazard_ControlTable");
        }

        public void AddVersion_Task_SaftyHazard_LinkTable()
        {
            callMethod(prefix + "AddVersion_Task_SaftyHazard_LinkTable");
        }

        public void AddVersion_Procedure_SaftyHazard_LinkTable()
        {
            callMethod(prefix + "AddVersion_Procedure_SaftyHazard_LinkTable");
        }

        public void AddVersion_EnablingObjective_SaftyHazard_LinkTable()
        {
            callMethod(prefix + "AddVersion_EnablingObjective_SaftyHazard_LinkTable");
        }

        public void AddVersion_EnablingObjectiveTable()
        {
            callMethod(prefix + "AddVersion_EnablingObjectiveTable");
        }

        public void AddVersion_EnablingObjective_Procedure_LinkTable()
        {
            callMethod(prefix + "AddVersion_EnablingObjective_Procedure_LinkTable");
        }

        public void AddVersion_Procedure_EnablingObjective_LinkTable()
        {
            callMethod(prefix + "AddVersion_Procedure_EnablingObjective_LinkTable");
        }

        public void AddEmployee_TaskTable()
        {
            callMethod(prefix + "AddEmployee_TaskTable");
        }

        public void AddTimesheetTable()
        {
            callMethod(prefix + "AddTimesheetTable");
        }

        public void AddTask_QuestionTable()
        {
        }

        public void AddTask_PositionTable()
        {
            callMethod(prefix + "AddTask_PositionTable");
        }


        public void AddPersonsTable()
        {
            callMethod(prefix + "AddPersonsTable");
        }

        public void AddProviderLevelTable()
        {
            callMethod(prefix + "AddProviderLevelTable");
        }

        public void AddProviderTable()
        {
            callMethod(prefix + "AddProviderTable");
        }

        public void AddILA_TopicTable()
        {
            callMethod(prefix + "AddILA_TopicTable");
        }

        public void AddILA_Evaluator_LinksTable()
        {
            callMethod(prefix + "AddILA_Evaluator_LinksTable");
        }
        public void AddILA_Procedure_LinksTable()
        {
            callMethod(prefix + "AddILA_Procedure_LinksTable");
        }

        public void AddDeliveryMethodTable()
        {
            callMethod(prefix + "AddDeliveryMethodTable");
        }

        public void AddTrainingTopicTable()
        {
            callMethod(prefix + "AddTrainingTopicTable");
        }

        public void AddNercStandardTable()
        {
            callMethod(prefix + "AddNercStandardTable");
        }

        public void AddNercStandardMemberTable()
        {
            callMethod(prefix + "AddNercStandardMemberTable");
        }

        public void AddTraineeEvaluationTypeTable()
        {
            callMethod(prefix + "AddTraineeEvaluationTypeTable");
        }

        public void AddMetaILATable()
        {
            callMethod(prefix + "AddMetaILATable");
        }

        public void AddSegmentTable()
        {
            callMethod(prefix + "AddSegmentTable");
        }
        public void AddILA_Segment_LinksTable()
        {
            callMethod(prefix + "AddILA_Segment_LinksTable");
        }

        public void AddSelfRegistrationOptionsTable()
        {
            callMethod(prefix + "AddSelfRegistrationOptionsTable");
        }
        public void AddAssessmentToolTable()
        {
            callMethod(prefix + "AddAssessmentToolTable");
        }

        public void AddRR_IssuingAuthorityTable()
        {
            callMethod(prefix + "AddRR_IssuingAuthorityTable");
        }

        public void AddRegulatoryRequirementTable()
        {
            callMethod(prefix + "AddRegulatoryRequirementTable");
        }

        public void AddRR_SH_LinkTable()
        {
            callMethod(prefix + "AddRR_SH_LinkTable");
        }

        public void AddILATable()
        {
            callMethod(prefix + "AddILATable");
        }

        public void AddTrainingTopic_CategoryTable()
        {
            callMethod(prefix + "AddTrainingTopic_CategoryTable");
        }

        public void AddNERCTargetAudienceTable()
        {
            callMethod(prefix + "AddNERCTargetAudienceTable");
        }

        public void AddRatingScaleTable()
        {
            callMethod(prefix + "AddRatingScaleTable");
        }

        public void AddStudentEvaluationAvailabilityTable()
        {
            callMethod(prefix + "AddStudentEvaluationAvailabilityTable");
        }

        public void AddILA_NERCAudience_LinkTable()
        {
            callMethod(prefix + "AddILA_NERCAudience_LinkTable");
        }

        public void AddStudentEvaluationFormsTable()
        {
            callMethod(prefix + "AddStudentEvaluationFormsTable");
        }
        public void AddStudentEvaluationWithoutEmpTable()
        {
            callMethod(prefix + "AddStudentEvaluationWithoutEmpTable");
        }

        public void AddCoverSheetTypeTable()
        {
            callMethod(prefix + "AddCoverSheetTypeTable");
        }

        public void AddStudentEvaluationQuestionTable()
        {
            callMethod(prefix + "AddStudentEvaluationQuestionTable");
        }
        public void AddAddQuestionBankTable()
        {
            callMethod(prefix + "AddQuestionBankTable");
        }

        public void AddCollaboratorInvitationTable()
        {
            callMethod(prefix + "AddCollaboratorInvitationTable");
        }

        public void AddCoversheetTable()
        {
            callMethod(prefix + "AddCoversheetTable");
        }

        public void AddCustomEnablingObjectiveTable()
        {
            callMethod(prefix + "AddCustomEnablingObjectiveTable");
        }

        public void AddStudentEvaluationAudienceTable()
        {
            callMethod(prefix + "AddStudentEvaluationAudienceTable");
        }

        public void AddTaxonomyLevelTable()
        {
            callMethod(prefix + "AddTaxonomyLevelTable");
        }

        public void AddStudentEvaluationTable()
        {
            callMethod(prefix + "AddStudentEvaluationTable");
        }

        public void AddTestStatusTable()
        {
            callMethod(prefix + "AddTestStatusTable");
        }

        public void AddTestTable()
        {
            callMethod(prefix + "AddTestTable");
        }

        public void AddTestTypeTable()
        {
            callMethod(prefix + "AddTestTypeTable");
        }

        public void AddTestSettingTable()
        {
            callMethod(prefix + "AddTestSettingTable");
        }

        public void AddTestItemTypeTable()
        {
            callMethod(prefix + "AddTestItemTypeTable");
        }

        public void AddTestItemTable()
        {
            callMethod(prefix + "AddTestItemTable");
        }

       
        public void AddILATraineeEvaluationTable()
        {
            callMethod(prefix + "AddILATraineeEvaluationTable");
        }

        public void AddTestItemMatchTable()
        {
            callMethod(prefix + "AddTestItemMatchTable");
        }

        public void AddTestItemMCQTable()
        {
            callMethod(prefix + "AddTestItemMCQTable");
        }

        public void AddTestItemTrueFalseTable()
        {
            callMethod(prefix + "AddTestItemTrueFalseTable");
        }

        public void AddTestItemFillBlankTable()
        {
            callMethod(prefix + "AddTestItemFillBlankTable");
        }

        public void AddTestItemShortAnswerTable()
        {
            callMethod(prefix + "AddTestItemShortAnswerTable");
        }

        public void AddDiscussionQuestionTable()
        {
            callMethod(prefix + "AddDiscussionQuestionTable");
        }

        public void AddToolCategoryTable()
        {
            callMethod(prefix + "AddToolCategoryTable");
        }

        public void AddSimulatorScenarioDifficultyLevelTable()
        {
            callMethod(prefix + "AddSimulatorScenarioDifficultyLevelTable");
        }

        public void AddSimulationScenarioSpecLookUpTable()
        {
            callMethod(prefix + "AddSimulationScenarioSpecLookUpTable");
        }

        public void AddSimulatorScenarioTable()
        {
            callMethod(prefix + "AddSimulatorScenarioTable");
        }

        public void AddTrainingGroup_CategoryTable()
        {
            callMethod(prefix + "AddTrainingGroup_CategoryTable");
        }

        public void AddTrainingGroupTable()
        {
            callMethod(prefix + "AddTrainingGroupTable");
        }

        public void AddTask_TrainingGroupTable()
        {
            callMethod(prefix + "AddTask_TrainingGroupTable");
        }
        public void AddTrainingPrograms_ILA_LinksTable()
        {
            callMethod(prefix + "AddTrainingPrograms_ILA_LinksTable");
        }

        public void AddVersion_EmployeeTable()
        {
            callMethod(prefix + "AddVersion_EmployeeTable");
        }

        public void AddActivityNotificationTable()
        {
            callMethod(prefix + "AddActivityNotificationTable");
        }

        public void AddVersion_TrainingGroupTable()
        {
            callMethod(prefix + "AddVersion_TrainingGroupTable");
        }
        public void AddRatingScaleNTable()
        {
            callMethod(prefix + "AddRatingScaleNTable");
        }

        public void AddRatingScaleExpandedTable()
        {
            callMethod(prefix + "RatingScaleExpandedTable");
        }

        public void AddEvaluationMethodTable()
        {
            callMethod(prefix + "AddEvaluationMethodTable");
        }

        public void AddTrainingProgramTypeTable()
        {
            callMethod(prefix + "AddTrainingProgramTypeTable");
        }

        public void UpdateTrainingProgramTypeTable_VersionIsYear()
        {
            callMethod(prefix + "UpdateTrainingProgramTypeTable_VersionIsYear");
        }

        public void AddSettings_EmailNotifications()
        {
            callMethod(prefix + "AddSettings_EmailNotifications");
        }

        public void AddTaskQualificationStatusTable()
        {
            callMethod(prefix + "AddTaskQualificationStatusTable");
        }

        public void AddIDP_ReviewStatusTable()
        {
            callMethod(prefix + "AddIDP_ReviewStatusTable");
        }

        public void AddIDP_ReviewTable()
        {
            callMethod(prefix + "AddIDP_ReviewTable");
        }

        public void AddClassSchedule_Roster_StatusesTable()
        {
            callMethod(prefix + "AddClassSchedule_Roster_StatusesTable");
        }
        public void AddClassSchedule_Evaluation_RosterTable()
        {
            callMethod(prefix + "AddClassSchedule_Evaluation_RosterTable");
        }

        public void AddDatabaseSettings()
        {
            callMethod(prefix + "AddDatabaseSettings");
        }

        public void AddMetaILAConfigurationPublishOption()
        {
            callMethod(prefix + "AddMetaILAConfigurationPublishOption");
        }

        public void AddMetaILAAssessment()
        {
            callMethod(prefix + "AddMetaILAAssessment");
        }
        public void AddILA_TaskObjective_LinksTable()
        {
            callMethod(prefix + "AddILA_TaskObjective_LinksTable");
        }

        public void AddMetaILA_Status()
        {
            callMethod(prefix + "AddMetaILA_Status");
        }

        public void AddLocationCategories()
        {
            callMethod(prefix + "AddLocationCategories");
        }
        public void AddLocation()
        {
            callMethod(prefix + "AddLocation");
        }

        public void AddStudentEvaluation_QuestionTable()
        {
            callMethod(prefix + "AddStudentEvaluation_QuestionTable");
        }

        public void AddTestItemLinksTable()
        {
            callMethod(prefix + "AddTestItemLinksTable");
        }
        public void AddTaskQualification_Evaluator_LinksTable()
        {
            callMethod(prefix + "AddTaskQualification_Evaluator_LinksTable");
        }
        public void AddPosition_TasksTable()
        {
            callMethod(prefix + "AddPosition_TasksTable");
        }
        public void AddDocumentTypesTable()
        {
            callMethod(prefix + "AddDocumentTypesTable");
        }
        public void AddInstructorWorkbook()
        {
            callMethod(prefix + "AddInstructorWorkbook");
        }

        public void UpdateMetaILAConfigurationPublishOption()
        {
            callMethod(prefix + "UpdateMetaILAConfigurationPublishOption");
        }

        public void UpdateDocumentTypeTable()
        {
            callMethod(prefix + "UpdateDocumentTypeTable");
        }
        public void AddMetaIlaEmployees()
        {
            callMethod(prefix + "AddMetaIlaEmployees");
        }
        public void AddMetaIlaTestType()
        {
            callMethod(prefix + "AddMetaIlaTestType");
        }

        public void AddClassSchedule_SelfRegistrationOptionsData()
        {
            callMethod(prefix + "AddClassSchedule_SelfRegistrationOptionsData");
        }

        public void UpdateClientSettings_GeneralSettings_DateFormat()
        {
            callMethod(prefix + "UpdateClientSettings_GeneralSettings_DateFormat");
        }
        public void UpdateTaskReQualificationEmpSignOffData()
        {
            callMethod(prefix + "UpdateTaskReQualificationEmpSignOffData");
        }

        public void ModifySelfRegistrationOptions()
        {
            callMethod(prefix + "ModifySelfRegistrationOptions");
        }

        public void ModifyClientSettingNotificationTemplates()
        {
            callMethod(prefix + "ModifyClientSettingNotificationTemplates");
        }
        public void UpdateCertifyingBodiesTableData()
        {
            callMethod(prefix + "UpdateCertifyingBodiesTableData");
        }

        public void UpdateILATaskObjectiveLinksSequenceNumber()
        {
            callMethod(prefix + "UpdateILATaskObjectiveLinksSequenceNumber");
        }

        public void AddDIFSurvey_DevStatus()
        {
            callMethod(prefix + "AddDIFSurvey_DevStatus");
        }

        public void AddDIFSurvey_Employee_Status()
        {
            callMethod(prefix + "AddDIFSurvey_Employee_Status");
        }

        public void AddDIFSurvey_Task_Status()
        {
            callMethod(prefix + "AddDIFSurvey_Task_Status");
        }
        public void AddDIFSurvey_Task_TrainingFrequency()
        {
            callMethod(prefix + "AddDIFSurvey_Task_TrainingFrequency");
        }
        public void AddDIFSurvey()
        {
            callMethod(prefix + "AddDIFSurvey");
        }
        public void AddDIFSurvey_Employee()
        {
            callMethod(prefix + "AddDIFSurvey_Employee");
        }
        public void AddDIFSurvey_Task()
        {
            callMethod(prefix + "AddDIFSurvey_Task");
        }
        public void AddDIFSurvey_Employee_Response()
        {
            callMethod(prefix + "AddDIFSurvey_Employee_Response");
        }
        public void AddEmailNotification_AdminEmployeePortalCompletions()
        {
            callMethod(prefix + "AddEmailNotification_AdminEmployeePortalCompletions");
        }
        public void UpdateInstructorWorkbook_ILADesign_Segments()
        {
            callMethod(prefix + "UpdateInstructorWorkbook_ILADesign_Segments");
        }
        public void AddClientSettings_Feature()
        {
            callMethod(prefix + "AddClientSettings_Feature");
        }
        public void UpdateClientSettings_LabelReplacementsEnhanced()
        {
            callMethod(prefix + "UpdateClientSettings_LabelReplacementsEnhanced");
        }

        public void Update_TblClientSettings_GeneralSettings()
        {
            callMethod(prefix + "Update_TblClientSettings_GeneralSettings");
        }
        public void UpdateActivityNotificationsTable()
        {
            callMethod(prefix + "UpdateActivityNotificationsTable");
        }
        public void AddAdminEmailNotificationStepsSettings()
        {
            callMethod(prefix + "AddAdminEmailNotificationStepsSettings");
        }
        public void UpdateAdminEmailNotificationTimingText()
        {
            callMethod(prefix + "UpdateAdminEmailNotificationTimingText");
        }
        public void UpdateAdminEmailNotificationStepsSettings()
        {
            callMethod(prefix + "UpdateAdminEmailNotificationStepsSettings");
        }
        public void AddEmpSettingsReleaseTypes()
        {
            callMethod(prefix + "AddEmpSettingsReleaseTypes");
        }
        public void UpdateClientSettingsNotification_Admin()
        {
            callMethod(prefix + "UpdateClientSettingsNotification_Admin");
        }
        public void UpdateEmpSettingsReleaseTypes()
        {
            callMethod(prefix + "UpdateEmpSettingsReleaseTypes");
        }
        public void UpdateEmailNotificationTemplate()
        {
            callMethod(prefix + "UpdateEmailNotificationTemplate");
        }
        public void AddTaskListReview_Type()
        {
            callMethod(prefix + "AddTaskListReview_Type");
        }
        public void AddTaskListReview_Status()
        {
            callMethod(prefix + "AddTaskListReview_Status");
        }
        public void AddTaskReview_Status()
        {
            callMethod(prefix + "AddTaskReview_Status");
        }
        public void AddTaskReview_Finding()
        {
            callMethod(prefix + "AddTaskReview_Finding");
        }
        public void AddActionItem_Priority()
        {
            callMethod(prefix + "AddActionItem_Priority");
        }

        public void AddActionItem_OperationTypes()
        {
            callMethod(prefix + "AddActionItem_OperationTypes");
        }

        public void AddActionItem_OperationType_Links()
        {
            callMethod(prefix + "AddActionItem_OperationType_Links");
        }

        public void AddTaskListReviewDocumentType()
        {
            callMethod(prefix + "AddTaskListReviewDocumentType");
        }

        public void AddSimulatorScenarioDifficulty()
        {
            callMethod(prefix + "AddSimulatorScenarioDifficulty");
        }

        public void AddSimulatorScenario_CollaboratorPermission()
        {
            callMethod(prefix + "AddSimulatorScenario_CollaboratorPermission");
        }

        public void AddSimulatorScenario_Status()
        {
            callMethod(prefix + "AddSimulatorScenario_Status");
        }
        public void AddEmailNotification_SimulatorScenarioCollaboration()
        {
            callMethod(prefix + "AddEmailNotification_SimulatorScenarioCollaboration");
        }

        public void CopySimulatorScenarioOldToNew()
        {
            callMethod(prefix + "CopySimulatorScenarioOldToNew");
        }
        public void AddTrainingIssue_Status()
        {
            callMethod(prefix + "AddTrainingIssue_Status");
        }
        public void AddTrainingIssue_Severity()
        {
            callMethod(prefix + "AddTrainingIssue_Severity");
        }
        public void AddTrainingIssue_DriverType()
        {
            callMethod(prefix + "AddTrainingIssue_DriverType");
        }
        public void AddTrainingIssue_DriverSubType()
        {
            callMethod(prefix + "AddTrainingIssue_DriverSubType");
        }
        public void AddTrainingIssue_ActionItemPriority()
        {
            callMethod(prefix + "AddTrainingIssue_ActionItemPriority");
        }
        public void AddTrainingIssue_ActionItemStatus()
        {
            callMethod(prefix + "AddTrainingIssue_ActionItemStatus");
        }
        public void UpdateDuplicateInstructorEmails()
        {
            callMethod(prefix + "UpdateDuplicateInstructorEmails");
        }
        public void UpdateStudentEvaluation()
        {
            callMethod(prefix + "UpdateStudentEvaluation");
        }
        public void UpdateRecord_IlaStudentEvalLinks()
        {
            callMethod(prefix + "UpdateRecord_IlaStudentEvalLinks");
        }
        public void UpdateEvaluationReleaseEMPSettings()
        {
            callMethod(prefix + "UpdateEvaluationReleaseEMPSettings");
        }

        public void UpdateVersionNumberinExistingVersionILAs()
        {
            callMethod(prefix + "UpdateVersionNumberinExistingVersionILAs");
        }
        public void AddData_EmployeeHistoryTableandReport()
        {
            callMethod(prefix + "AddData_EmployeeHistoryTableandReport");
        }
       
        public void UpdateVersionILAsToPublishedStateBasedOnEffectiveDate()
        {
            callMethod(prefix + "UpdateVersionILAsToPublishedStateBasedOnEffectiveDate");
        }        public void AddTableData_ClassScheduleEmpSettings()
        {
            callMethod(prefix + "AddTableData_ClassScheduleEmpSettings");
        }

        public void AddTableData_ClassScheduleTQEMPSettings()
        {
            callMethod(prefix + "AddTableData_ClassScheduleTQEMPSettings");
        }

        public void ApplyDatabaseUpdateScripts()
        {
            callMethod(prefix + "ApplyDatabaseUpdateScripts");
        }
        public void UpdateDocumentTypeForCourseCompletionInfo()
        {
            callMethod(prefix + "UpdateDocumentTypeForCourseCompletionInfo");
        }
        public void UpdateLaunchLinkInCbtScormRegistration()
        {
            callMethod(prefix + "UpdateLaunchLinkInCbtScormRegistration");
        }
        public void AddScormRegistrationsForActiveClassScheduleEmployees()
        {
            callMethod(prefix + "AddScormRegistrationsForActiveClassScheduleEmployees");
        }
        public void UpdateInactiveScormRegistrations()
        {
            callMethod(prefix + "UpdateInactiveScormRegistrations");
        }

        public void UpdateApiRegistrationIdsForScormRegistrationsAndScormApiRegToLearner()
        {
            callMethod(prefix + "UpdateApiRegistrationIdsForScormRegistrationsAndScormApiRegToLearner");
        }
        public void UpdateTaskQualificationsLinkedToDeletedTasks()
        {
            callMethod(prefix + "UpdateTaskQualificationsLinkedToDeletedTasks");
        }
        public void UpdateTaskActiveStatusFromHistory()
        {
            callMethod(prefix + "UpdateTaskActiveStatusFromHistory");
        }
        public void UpdateDocumentTypesTable_Tool()
        {
            callMethod(prefix + "UpdateDocumentTypesTable_Tool");
        }
        public void UpdateILACertificationLinksForDeletedCertifications()
        {
            callMethod(prefix + "UpdateILACertificationLinksForDeletedCertifications");
        }
        public void UpdateDeletedTaskQualificationEvaluatorLinksForDeletedEmployees()
        {
            callMethod(prefix + "UpdateDeletedTaskQualificationEvaluatorLinksForDeletedEmployees");
        }
        public void MarkDeletedPositionTasksBasedOnTasks()
        {
            callMethod(prefix + "MarkDeletedPositionTasksBasedOnTasks");
        }
        public void UpdateSegmentObjectiveLinksData()
        {
            callMethod(prefix + "UpdateSegmentObjectiveLinksData");
        }
        public void AddTrainingTopicsTableData()
        {
            callMethod(prefix + "AddTrainingTopicsTableData");
        }
        public void AddData_AdminEMPCompletionNotificationAndSettings()
        {
            callMethod(prefix + "AddData_AdminEMPCompletionNotificationAndSettings");
        }
        public void NotificationTemplateAdjustments()
        {
            callMethod(prefix + "NotificationTemplateAdjustments");
        }
        public void Update_NotificationTemplate()
        {
            callMethod(prefix + "Update_NotificationTemplate");
        }
        public void Update_NotifcationEmailTemplate()
        {
            callMethod(prefix + "Update_NotifcationEmailTemplate");
        }
        public void UpdateTaskEoLinksOfDeletedEO()
        {
            callMethod(prefix + "UpdateTaskEoLinksOfDeletedEO");
        }
        public void Update_NotificationEmailTemplatesWithUsing()
        {
            callMethod(prefix + "Update_NotificationEmailTemplatesWithUsing");
        }

        public void UndeleteCompletedClassesAndClearIDPInfo()
        {
            callMethod(prefix + "UndeleteCompletedClassesAndClearIDPInfo");
        }
        public void UpdateEMPPretestNotificationEmailTemplate()
        {
            callMethod(prefix + "UpdateEMPPretestNotificationEmailTemplate");
        }
        public void AddProfessionlaHoursToCertificationTable()
        {
            callMethod(prefix + "AddProfessionlaHoursToCertificationTable");
        }
        public void UpdateVersionTasksIsInUseFlag()
        {
            callMethod(prefix + "UpdateVersionTasksIsInUseFlag");
        }

        public void MigrateDataFromProcedure_ILA_LinksToILA_Procedure_Links()
        {
            callMethod(prefix + "MigrateDataFromProcedure_ILA_LinksToILA_Procedure_Links");
        }

        public void AddNewReportFilters_AccordingToRRTTypes()
        {
            callMethod(prefix + "AddNewReportFilters_AccordingToRRTTypes");
        }
        public void UpdateReportFilters_ILAByProvider()
        {
            callMethod(prefix + "UpdateReportFilters_ILAByProvider");
        }
        public void UpdateReport_ILAByCompletionHistory()
        {
            callMethod(prefix + "UpdateReport_ILAByCompletionHistory");
        }
        public void UpdateReport_TaskByPosition()
        {
            callMethod(prefix + "UpdateReport_TaskByPosition");
        }
        public void UpdateReport_TrainingIssueList()
        {
            callMethod(prefix + "UpdateReport_TrainingIssueList");
        }
        public void AddReport_TrainingIssueDetails()
        {
            callMethod(prefix + "AddReport_TrainingIssueDetails");
        }
        public void AddReport_TrainingIssuesActionItems()
        {
            callMethod(prefix + "AddReport_TrainingIssuesActionItems");
        }
        public void UpdateReport_TasksByTrainingTaskGroup()
        {
            callMethod(prefix + "UpdateReport_TasksByTrainingTaskGroup");
        }
        public void AddNewReportFilters_EmployeeTrainingTowardNERCRecertification()
        {
            callMethod(prefix + "AddNewReportFilters_EmployeeTrainingTowardNERCRecertification");
        }
        public void AddClientSettings_Feature_PublicClasses()
        {
            callMethod(prefix + "AddClientSettings_Feature_PublicClasses");
        }
        public void UpdateReportFilters_TasksByPosition()
        {
            callMethod(prefix + "UpdateReportFilters_TasksByPosition");
        }
        public void UpdateReportFilters_TrainingIssuesActionItems()
        {
            callMethod(prefix + "UpdateReportFilters_TrainingIssuesActionItems");
        }
        public void UpdateReportFilters_TrainingIssuesDetails()
        {
            callMethod(prefix + "UpdateReportFilters_TrainingIssuesDetails");
        }
        public void AddReportFilters_TrainingProgramCompletionHistory()
        {
            callMethod(prefix + "AddReportFilters_TrainingProgramCompletionHistory");
        }
        public void UpdateReportFilters_TrainingProgramCompletionHistory()
        {
            callMethod(prefix + "UpdateReportFilters_TrainingProgramCompletionHistory");
        }
        public void AddDataToTrainingIssueActionItemsAssigneeName()
        {
            callMethod(prefix + "AddDataToTrainingIssueActionItemsAssigneeName");
        }
        public void SeedInitialReportData()
        {
            callMethod(prefix + "SeedInitialReportData");
        }

        public void AddReport_TrainingProgramQualificationCard()
        {
            callMethod(prefix + "AddReport_TrainingProgramQualificationCard");
        }
        public void AddDataToClassScheduleTQEmpSettingsFromTQILAEmpSettings()
        {
            callMethod(prefix + "AddDataToClassScheduleTQEmpSettingsFromTQILAEmpSettings");
        }
        public void UpdateTaskByPositionReportFilters()
        {
            callMethod(prefix + "UpdateTaskByPositionReportFilters");
        }
        public void UpdateReportFilters_StudentEvaluationInstructorLed()
        {
            callMethod(prefix + "UpdateReportFilters_StudentEvaluationInstructorLed");
        }
        public void SeedDataToEnrolledPropertyOfMetaILAEmployees()
        {
            callMethod(prefix + "SeedDataToEnrolledPropertyOfMetaILAEmployees");
        }

        public void AddEmailNotification_PublicClassScheduleRequest()
        {
            callMethod(prefix + "AddEmailNotification_PublicClassScheduleRequest");
        }

        public void UpdateEmailNotification_PublicClassScheduleRequest()
        {
            callMethod(prefix + "UpdateEmailNotificationPublicClassScheduleRequestTemplate");
        }
        public void UpdateReportFiltersForILACompletionHistory()
        {
            callMethod(prefix + "UpdateReportFiltersForILACompletionHistory");
        }
        public void AddReport_ProceduresByEnablingObjectives()
        {
            callMethod(prefix + "AddReport_ProceduresByEnablingObjectives");
        }
        public void AddDataToTaskListReviewPositionLinksTable()
        {
            callMethod(prefix + "AddDataToTaskListReviewPositionLinksTable");
        }
        public void UpdateInternalIdentifiersForCertifications()
        {
            callMethod(prefix + "UpdateInternalIdentifiersForCertifications");
        }
        public void AddReportFilter_EmployeeDelinquencyReport()
        {
            callMethod(prefix + "AddReportFilter_EmployeeDelinquencyReport");
        }

        public void UpdateReportSkeletonColumns_AnnualTaskListReviewReport()
        {
            callMethod(prefix + "UpdateReportSkeletonColumns_AnnualTaskListReviewReport");
        }
        public void Seed_TaskReviewStatus_NotStarted()
        {
            callMethod(prefix + "Seed_TaskReviewStatus_NotStarted");
        }
        public void UpdateReportSkeletonFilter_TaskAndEnabingObjectiveByILA()
        {
            callMethod(prefix + "UpdateReportSkeletonFilter_TaskAndEnabingObjectiveByILA");
        }
        public void MigrateReviewedByFromTrainerName()
        {
            callMethod(prefix + "MigrateReviewedByFromTrainerName");
        }
        public void Update_ClientSettings_Notification_StepsTemplate()
        {
            callMethod(prefix + "Update_ClientSettings_Notification_StepsTemplate");
        }
        public void AddSkillQualificationStatusTable()
        {
            callMethod(prefix + "AddSkillQualificationStatusTable");
        }
        protected void callMethod(string method)
        {
            Type thisType = GetType();
            MethodInfo theMethod = thisType.GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance);
            theMethod.Invoke(this, null);
        }
        protected object[,] toRectangular(object[][] arrayOfArrays)
        {
            int height = arrayOfArrays.Length;
            int width = arrayOfArrays[0].Length;

            object[,] result = new object[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = arrayOfArrays[i][j];
                }
            }
            return result;
        }
    }
}
