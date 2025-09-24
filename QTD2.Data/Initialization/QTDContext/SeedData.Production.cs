using Microsoft.EntityFrameworkCore.Migrations;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace QTD2.Data.Initialization.QTDContext
{
    public partial class SeedData
    {
        protected void Production_AddInstructor_Category()
        {
            _migrationBuilder.InsertData(
             table: "Instructor_Categories",
             columns: new[] { "ICategoryTitle", "ICategoryDescription", "IEffectiveDate", "Active" },
             values: new object[,]
              {
                    { "Default", "The Default Instructor Category", System.DateTime.Now, true },
              });
        }

        protected void Production_AddInstructorTable()
        {
        }

        protected void Production_AddPersonsTable()
        {

        }

        protected void Production_AddEmployeeTable()
        {

        }

        protected void Production_AddCertificationsHistoryTable()
        {

        }

        protected void Production_AddEmployeeCertificationsTable()
        {

        }

        protected void Production_AddEmployeePositionTable()
        {
        }

        protected void Production_AddPositionTable()
        {
        }

        protected void Production_AddClientUsers()
        {
        }

        protected void Production_AddVersion_EmployeeTable()
        {
        }

        protected void Production_AddEmployeeOrganizationTable()
        {
        }

        protected void Production_AddOrganizationTable()
        {
        }

        protected void Production_AddTrainingProgramTable()
        {
        }

        protected void Production_AddCertifyingBodyTable()
        {
            _migrationBuilder.InsertData(
                 table: "CertifyingBodies",
                 columns: new[] { "Id", "Name", "IsNERC", "Active", "Deleted" },
                 values: new object[,]
                {
                    { 1, "NERC", true, true, false },
                    { 2, "Regional", false, true, false },
                });
        }

        protected void Production_AddMetaILAAssessment()
        {
            _migrationBuilder.InsertData(
                   table: "MetaILAAssessments",
                   columns: new[] { "Description", "Active" },
                   values: new object[,]
                   {
                        { "Create Meta ILA Summary Test",true },
                        {"Create Meta ILA Student Evaluation",true }
                   });
        }


        protected void Production_AddMetaILA_Status()
        {
            _migrationBuilder.InsertData(
                 table: "MetaILA_Statuses",
                 columns: new[] { "Name", "Active" },
                 values: new object[,]
                 {
                        {"Published",true },
                        {"Draft",true }
                 });
        }

        protected void Production_AddCertificationTable()
        {
            _migrationBuilder.InsertData(
                table: "Certifications",
                columns: new[] { "Id", "CertAcronym", "Name", "CertifyingBodyId", "RenewalTimeFrame", "RenewalInterval", "CreditHrsReq", "CreditHrs", "CertSubReq", "CertSubReqName", "CertSubReqHours", "CertMemberNum", "CertifiedDate", "RenewalDate", "ExpirationDate", "AllowRolloverHours", "AdditionalHours", "EffectiveDate", "Active", "Deleted" },
                values: new object[,]
                {
                    { 1,  "Other", "Other", 2, false, 1, false, 1, false, "CertSubReqName", 1.00, false, false, false, false, false, 0.00, System.DateTime.Now, true, false },
                });
        }

        protected void Production_AddDutyAreaTable()
        {
        }

        protected void Production_AddSubdutyAreaTable()
        {
        }

        protected void Production_AddTaskTable()
        {
        }

        protected void Production_AddEnablingObjectiveTable()
        {
        }

        protected void Production_AddEnablingObjective_TopicTable()
        {
        }

        protected void Production_AddEnablingObjective_CategoryTable()
        {
        }

        protected void Production_AddEnablingObjective_SubCategoryTable()
        {
        }

        protected void Production_AddProcedure_IssuingAuthorityTable()
        {
        }

        protected void Production_AddProcedureTable()
        {
        }

        protected void Production_AddSaftyHazard_categoryTable()
        {
        }

        protected void Production_AddSaftyHazardTable()
        {
        }

        protected void Production_AddSaftyHazard_ControlTable()
        {
        }

        protected void Production_AddSaftyHazard_AbatementTable()
        {
        }

        protected void Production_AddProcedure_SaftyHazard_LinkTable()
        {
        }

        protected void Production_AddProcedure_EnablingObjective_LinkTable()
        {
        }

        protected void Production_AddEnablingObjective_SaftyHazard_LinkTable()
        {
        }

        protected void Production_AddEnablingObjective_Procedure_LinkTable()
        {
        }

        protected void Production_AddTask_EnablingObjective_LinkTable()
        {
        }

        protected void Production_AddTask_Procedure_LinkTable()
        {
        }

        protected void Production_AddTask_SaftyHazard_LinkTable()
        {
        }

        protected void Production_AddTask_StepTable()
        {
        }

        protected void Production_AddTask_ToolTable()
        {
        }

        protected void Production_AddToolGroupsTable()
        {
            _migrationBuilder.InsertData(
              table: "ToolGroups",
              columns: new[] { "Id", "Description", "Active" },
              values: new object[,]
                {
                    { 1, "Default", true },
                });
        }

        protected void Production_AddToolTable()
        {
        }

        protected void Production_AddToolGroup_ToolsTable()
        {
        }

        protected void Production_AddVersion_TaskTable()
        {
        }

        protected void Production_AddVersion_Task_StepTable()
        {
        }

        protected void Production_AddVersion_Task_QuestionTable()
        {
        }

        protected void Production_AddVersion_ProcedureTable()
        {
        }

        protected void Production_AddVersion_Task_Procedure_LinkTable()
        {
        }

        protected void Production_AddVersion_ToolTable()
        {
        }

        protected void Production_AddVersion_Task_Tool_LinkTable()
        {
        }

        protected void Production_AddVersion_Procedure_Tool_LinkTable()
        {
        }

        protected void Production_AddVersion_Task_EnablingObjective_LinkTable()
        {
        }

        protected void Production_AddVersion_EnablingObjective_Tool_LinkTable()
        {
        }

        protected void Production_AddVersion_SaftyHazardTable()
        {
        }

        protected void Production_AddVersion_SaftyHazard_AbatementTable()
        {
        }

        protected void Production_AddVersion_SaftyHazard_ControlTable()
        {
        }

        protected void Production_AddVersion_Task_SaftyHazard_LinkTable()
        {
        }

        protected void Production_AddVersion_Procedure_SaftyHazard_LinkTable()
        {
        }

        protected void Production_AddVersion_EnablingObjective_SaftyHazard_LinkTable()
        {
        }

        protected void Production_AddVersion_EnablingObjectiveTable()
        {
        }

        protected void Production_AddVersion_EnablingObjective_Procedure_LinkTable()
        {
        }

        protected void Production_AddVersion_Procedure_EnablingObjective_LinkTable()
        {
        }

        protected void Production_AddEmployee_TaskTable()
        {
        }

        protected void Production_AddTimesheetTable()
        {
        }

        protected void Production_AddTask_QuestionTable()
        {
        }

        protected void Production_AddTask_PositionTable()
        {
        }

        protected void Production_AddProviderLevelTable()
        {
            _migrationBuilder.InsertData(
                   table: "ProviderLevels",
                   columns: new[] { "Name", "Active", "Deleted" },
                   values: new object[,]
                   {
                        { "New Provider - Status Pending", true, false },
                        { "Level 1 - Transcript Reviewer", true, false },
                        { "Level 2 - ILA Provider", true, false },
                        { "Level 3 - NERC Continuing Education Provider", true, false }
                   }
               );
        }

        protected void Production_AddProviderTable()
        {
        }

        protected void Production_AddILA_TopicTable()
        {
        }

        protected void Production_AddDeliveryMethodTable()
        {
            _migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Name", "DisplayName", "IsNerc", "Active", "CreatorIlaId" },
                values: new object[,]
                  {
                    { "Classroom", "Classroom", true, true,0 },
                    { "Self-Study (e.g., paperbased)", "Self-Study (e.g., paperbased)", true, true,0 },
                    { "Computer Based Training (CBT)", "Computer Based Training (CBT)", true, true,0 },
                    { "Backup Drill Facility", "Backup Drill Facility", false, true,0 },
                    { "External Conference/Seminar/Workshop", "External Conference/Seminar/Workshop", false, true,0 },
                    { "Field Visit", "Field Visit", false, true,0 },
                    { "On-the-Job Training (OJT)", "On-the-Job Training (OJT)", true, true ,0},
                    { "One-on-One Training", "One-on-One Training", false, true,0 },
                    { "Simulator Training", "Simulator Training", false, true,0 },
                    { "Table Top Simulation", "Table Top Simulation", false, true,0 },
                    { "Task Qualification", "Task Qualification", false, true,0 },
                    { "Vendor Supplied Training", "Vendor Supplied Training", false, true,0 },
                    { "Virtual Instructor Led Training", "Virtual Instructor Led Training", false, true,0 },
                    { "Workshop", "Workshop", true, true ,0},
                    { "Traditional Classroom Activities", "Traditional Classroom Activities", true, true,0 },
                    { "Conferences", "Conferences", true, true,0 },
                    { "Seminars", "Seminars", true, true,0 },
                    { "In-house training curricula", "In-house training curricula", true, true,0 },
                    { "Structured self-study activities", "Structured self-study activities", true, true,0 },
                    { "Online and distance - learning activities", "Online and distance - learning activities", true, true,0 },
                    { "Operator Training Simulations", "Operator Training Simulations", true, true,0 },
                    { "Computer- based training activities", "Computer- based training activities", true, true,0 },
                  });
        }

        protected void Production_AddTrainingTopicTable()
        {
            _migrationBuilder.InsertData(
                    table: "TrainingTopics",
                    columns: new[] { "TrainingTopic_CategoryId", "Name", "Active" },
                    values: new object[,]
                    {
                        { 1,"Capacitance", true },
                        { 1,"Inductance", true },
                        { 1,"Impedance", true },
                        { 1,"Real reactive power", true },
                        { 1,"Electrical circuits", true },
                        { 1,"Magnetism", true },
                        { 2,"Basic trigonometry", true },
                        { 2,"Ratios" , true},
                        { 2,"Per unit values", true },
                        { 2,"Pythagorean Theorem", true },
                        { 2,"Ohm’s Law" , true},
                        { 2,"Kirchoff’s Laws" , true},
                        { 3,"Transmission lines", true },
                        { 3,"Transformers", true },
                        { 3,"Substations" , true},
                        { 3,"Power plants" , true},
                        { 3,"Protection", true },
                        { 3,"Introduction to power system operations and interconnected operations" , true},
                        { 3,"Frequency" , true},
                        { 4,"Transmission lines", true },
                        { 4,"Transformers", true },
                        { 4,"Busses" , true},
                        { 4,"Generators" , true},
                        { 4,"Relays and protection schemes", true},
                        { 4,"Power system faults", true },
                        { 4,"Synchronizing equipment", true },
                        { 4,"Under-frequency load shedding" , true},
                        { 4,"Under-voltage load shedding", true },
                        { 4,"Communication systems utilized", true },
                        { 5,"Voltage control", true },
                        { 5,"Frequency control", true },
                        { 5,"Power system stability", true },
                        { 5,"Facility outage both planned and unplanned" , true},
                        { 5,"Energy accounting", true },
                        { 5,"Inadvertent energy" , true},
                        { 5,"Time error control", true },
                        { 5,"Balancing of load and resources", true },
                        { 6,"Loss of generation resource(s)", true },
                        { 6,"Loss of transmission element(s)", true },
                        { 6,"Operating reserves", true },
                        { 6,"Contingency reserves", true },
                        { 6,"Line loading relief", true },
                        { 6,"Load shedding", true },
                        { 6,"Voltage and reactive flows during emergencies", true },
                        { 6,"Loss of EMS", true },
                        { 6,"Loss of primary control center", true },
                        { 7,"Restoration philosophies", true },
                        { 7,"Facility restoration priorities", true },
                        { 7,"Blackstart restoration", true },
                        { 7,"Stability (angle and voltage)", true },
                        { 7,"Islanding and Synchronizing", true },
                        { 8,"NAESB standards", true },
                        { 8,"Standards of Conduct", true },
                        { 8,"Tariffs", true },
                        { 8,"OASIS applications (Transmission Reservations)", true },
                        { 8,"E-Tag application", true },
                        { 8,"Transaction scheduling", true },
                        { 8,"Market applications", true },
                        { 8,"Interchange", true },
                        { 9,"Supervisory Control and Data Acquisition (SCADA)", true },
                        { 9,"Automatic Generation Control (AGC) application", true },
                        { 9,"Power flow application", true },
                        { 9,"State Estimator application", true },
                        { 9,"Contingency analysis application", true },
                        { 9,"P-V Curves", true },
                        { 9,"Load forecasting application", true },
                        { 9,"Energy accounting application", true },
                        { 9,"Voice and data communication systems", true },
                        { 9,"Demand-side management programs" , true},
                        { 10,"Identifying loss of facilities", true},
                        { 10,"Recognizing loss of communication facilities", true },
                        { 10,"Recognizing telemetry problems", true },
                        { 10,"Recognizing and identifying contingency problems", true },
                        { 10,"Proper communications (three-part)", true },
                        { 10,"Communication with appropriate entities including the RC", true },
                        { 10,"Cyber and physical security and threats", true },
                        { 10,"Reducing System Operator errors through the use of HPI Tools (self-checking, peer checking, Place Keeping and Procedure Use", true },
                        { 11,"ISO/RTO operational and emergency policies and procedures", true },
                        { 11,"Regional operational and emergency policies and procedures", true },
                        { 11,"Company-specific operational and emergency policies and procedures", true },
                        { 12,"Application and/or implementation of NERC Reliability Standards", true },
                    }
                );
        }

        protected void Production_AddNercStandardTable()
        {
        }

        protected void Production_AddNercStandardMemberTable()
        {
        }

        protected void Production_AddTraineeEvaluationTypeTable()
        {
            _migrationBuilder.InsertData(
                 table: "TraineeEvaluationTypes",
                 columns: new[] { "Name", "Active" },
                 values: new object[,]
                   {
                            { "Written", true },
                            { "Perform", true },
                            { "Discuss", true },
                            { "Simulations", true },
                   });
        }

        protected void Production_AddMetaILATable()
        {
        }

        protected void Production_AddSegmentTable()
        {
        }

        protected void Production_AddAssessmentToolTable()
        {
        }

        protected void Production_AddRR_IssuingAuthorityTable()
        {
            _migrationBuilder.InsertData(
            table: "RR_IssuingAuthorities",
            columns: new[] { "Title", "Description", "Active" },
            values: new object[,]
             {
                    { "NERC", "NERC", true },
             });
        }

        protected void Production_AddRegulatoryRequirementTable()
        {
        }

        protected void Production_AddRR_SH_LinkTable()
        {
        }

        protected void Production_AddILATable()
        {
        }

        protected void Production_AddTrainingTopic_CategoryTable()
        {
            _migrationBuilder.InsertData(
                table: "TrainingTopic_Categories",
                columns: new[] { "Name", "Active" },
                values: new object[,]
                {
                    { "Basic AC/DC Electricity", true },
                    { "Basic Power System Mathematic Concepts", true },
                    { "Characteristics of the Bulk Electric System", true },
                    { "System Protection Principles", true },
                    { "Interconnected Power System Operations" , true},
                    { "Emergency Operations" , true},
                    { "Power System Restoration", true },
                    { "Market Operations", true },
                    { "Tools", true },
                    { "System Operator Situational Awareness", true },
                    { "Policies and Procedures", true },
                    { "NERC Reliability Standards", true },
                }
                );
        }

        protected void Production_AddNERCTargetAudienceTable()
        {
            _migrationBuilder.InsertData(
                table: "NERCTargetAudiences",
                columns: new[] { "Name", "IsOther", "Active" },
                values: new object[,]
                {
                    { "Transimission Operator", false, true },
                    { "Market Operator", false, true },
                    { "Reliability Operator", false, true },
                    { "Operator And Planning Engineers", false, true },
                    { "Balancing and Interchange Operator", false, true},
                    { "Control Room Supervision/Management and Support Staff", false, true},
                    { "Generator Operator", false, true }
                });
        }

        protected void Production_AddRatingScaleTable()
        {
        }

        protected void Production_AddStudentEvaluationTable()
        {
        }

        protected void Production_AddStudentEvaluationAvailabilityTable()
        {
        }

        protected void Production_AddILA_NERCAudience_LinkTable()
        {
        }

        protected void Production_AddStudentEvaluationFormsTable()
        {
        }

        protected void Production_AddCoverSheetTypeTable()
        {
        }

        protected void Production_AddStudentEvaluationQuestionTable()
        {
        }

        protected void Production_AddCollaboratorInvitationTable()
        {
        }

        protected void Production_AddCoversheetTable()
        {
        }

        protected void Production_AddCustomEnablingObjectiveTable()
        {
        }

        protected void Production_AddStudentEvaluationAudienceTable()
        {
            _migrationBuilder.InsertData(
                 table: "StudentEvaluationAudiences",
                 columns: new[] { "Name", "Active" },
                 values: new object[,]
                 {
                        { "All Enrolled Employees",true },
                        { "First Class Only (Pilot Class)",true },
                 });
        }

        protected void Production_AddTaxonomyLevelTable()
        {

            _migrationBuilder.InsertData(
                    table: "TaxonomyLevels",
                    columns: new[] { "Description", "Active", "Deleted" },
                    values: new object[,]
                    {
                             { "Recall", true, false },
                        { "Application", true, false },
                        { "Analysis", true, false },
                        { "Evaluation", true, false },
                        { "Create", true, false },
                    });
        }

        protected void Production_AddTestStatusTable()
        {
            _migrationBuilder.InsertData(
                 table: "TestStatuses",
                 columns: new[] { "Description", "Active" },
                 values: new object[,]
                 {
                        { "In Development", true },
                        { "Published", true },
                        { "Inactive", true },
                        { "Draft", true },
                 });
        }

        protected void Production_AddTestTable()
        {
        }

        protected void Production_AddTestTypeTable()
        {
            _migrationBuilder.InsertData(
                   table: "TestTypes",
                   columns: new[] { "Description", "Active" },
                   values: new object[,]
                   {
                        { "Pretest", true },
                        { "Final Test", true },
                        { "Retake", true },
                        { "CBT", true },
                        { "StudentEvaluation", true },
                   });
        }

        protected void Production_AddTestSettingTable()
        {
        }

        protected void Production_AddTestItemTypeTable()
        {
            _migrationBuilder.InsertData(
                    table: "TestItemTypes",
                    columns: new[] { "Description", "Active" },
                    values: new object[,]
                    {
                        { "Multiple Choice Questions", true },
                        { "Fill in the Blank", true },
                        { "True / False", true },
                        { "Short Answers", true },
                        { "Match the Column", true },
                        { "Multiple Correct Answers", true },
                    });
        }

        protected void Production_AddMetaILAConfigurationPublishOption()
        {
            _migrationBuilder.InsertData(
                  table: "MetaILAConfigurationPublishOptions",
                  columns: new[] { "Description", "Active" },
                  values: new object[,]
                  {
                        { "Upon Clicking Start",true },
                        { "Upon Passing Previous ILA",true },
                        { "Upon Completion of Previous ILA",true },
                  });
        }

        protected void Production_AddTestItemTable()
        {
        }

        protected void Production_AddILATraineeEvaluationTable()
        {
        }

        protected void Production_AddTestItemMatchTable()
        {
        }

        protected void Production_AddTestItemMCQTable()
        {
        }

        protected void Production_AddTestItemTrueFalseTable()
        {
        }

        protected void Production_AddTestItemFillBlankTable()
        {
        }

        protected void Production_AddTestItemShortAnswerTable()
        {
        }

        protected void Production_AddDiscussionQuestionTable()
        {
        }

        protected void Production_AddToolCategoryTable()
        {
            _migrationBuilder.InsertData(
                 table: "ToolCategories",
                 columns: new[] { "Title", "Description", "Notes", "Active" },
                 values: new object[,]
                 {
                        { "Default", "Default", "Default", true },
                        { "From Migration", "From Migration", "These are from the initial EMP4 -> QTD2 Migration", true }
                 });
        }

        protected void Production_AddSimulatorScenarioDifficultyLevelTable()
        {
            _migrationBuilder.InsertData(
                table: "SimulatorScenarioDifficultyLevels_Old",
                columns: new[] { "SimulatorScenarioDiffLevel", "Active" },
                values: new object[,]
                {
                        { "Unknown", true },
                });
        }

        protected void Production_AddSimulationScenarioSpecLookUpTable()
        {
        }

        protected void Production_AddSimulatorScenarioTable()
        {
        }

        protected void Production_AddTrainingGroup_CategoryTable()
        {
            _migrationBuilder.InsertData(
                 table: "TrainingGroup_Categories",
                 columns: new[] { "Title", "Description", "EffectiveDate", "Note", "Active" },
                 values: new object[,]
                 {
                        { "Migration","The training group from the migration",System.DateTime.Now, System.String.Empty, true },
                 });
        }
        protected void Production_RatingScaleExpandedTable()
        {
            _migrationBuilder.InsertData(
                table: "RatingScaleExpanded",
                columns: new[] { "RatingScaleNId", "Ratings", "Description", "Active" },
                values: new object[,]
                {
                                { 1,1, "Strongly Disagree", true },
                                { 1,2, "Disagree", true },
                                { 1,3, "Neutral", true },
                                { 1,4, "Agree", true },
                                { 1,5, "Strongly Agree", true },

                                { 2,1,"Poor", true },
                                { 2,2,"Fair", true },
                                { 2,3,"Good", true },
                                { 2,4,"Very Good", true },
                                { 2,5,"Excellent", true },

                                { 3,1,"Poor", true},
                                { 3,2,"Unsatisfactory", true },
                                { 3,3,"Satisfactory", true },
                                { 3,4,"Very Satisfactory", true },
                                { 3,5,"Outstanding", true },

                                { 4,1,"Very Poor", true },
                                { 4,2,"Poor", true },
                                { 4,3,"Average", true },
                                { 4,4,"Good", true },
                                { 4,5,"Very Good", true },

                                { 5,1,"Strongly Disagree", true },
                                { 5,2,"Disagree", true },
                                { 5,3,"Agree", true },
                                { 5,4,"Strongly Agree", true },


                                { 6,1,"Poor", true },
                                { 6,2,"Fair" , true},
                                { 6,3,"Very Good", true },
                                { 6,4,"Excellent", true },

                                { 7,1,"Poor", true },
                                { 7,2,"Unsatisfactory", true },
                                { 7,3,"Very Satisfactory", true },
                                { 7,4,"Outstanding", true },

                                { 8,1,"Very Poor", true },
                                { 8,2,"Poor", true },
                                { 8,3,"Good", true },
                                { 8,4,"Very Good", true },

                                { 9,1,"Disagree", true },
                                { 9,2,"Neutral", true },
                                { 9,3,"Agree", true },

                                { 10,1,"Poor" , true},
                                { 10,2,"Good", true },
                                { 10,3,"Excellent", true },

                                { 11,1,"Poor", true },
                                { 11,2,"Satisfactory", true },
                                { 11,3,"Outstanding", true },

                                { 12,1,"Very Poor", true },
                                { 12,2,"Average", true },
                                { 12,3,"Very Good", true },

                                { 13,1,"Yes",true },
                                { 13,2,"No" ,true}
                });
        }


        protected void Production_AddTrainingGroupTable()
        {
        }

        protected void Production_AddTask_TrainingGroupTable()
        {

        }

        protected void Production_AddActivityNotificationTable()
        {
        }
        protected void Production_AddVersion_TrainingGroupTable()
        {

        }

        protected void Production_AddSettings_EmailNotifications()
        {
            string clientSettingsJsonString = System.IO.File.ReadAllText(_path + "\\clientsettings_notifications.json");
            List<ClientSettings_Notification> clientSettings = JsonSerializer.Deserialize<List<ClientSettings_Notification>>(clientSettingsJsonString);

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notifications",
              columns: new[] { "Name", "Enabled", "TimingText", "Active" },
              values: toRectangular(clientSettings.Select(r => new object[] { r.Name, r.Enabled, r.TimingText, true }).ToArray()));

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_AvailableCustomSettings",
              columns: new[] { "ClientSettingsNotificationId", "Setting", "Active" },
              values: toRectangular(clientSettings
                    .SelectMany(r => r.AvailableCustomSettings)
                    .Select(r => new object[] { r.ClientSettingsNotificationId, r.Setting, true })
                    .ToArray()));

            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_CustomSettings",
                columns: new[] { "ClientSettingsNotificationId", "Key", "Value", "Active" },
                values: toRectangular(clientSettings
                    .SelectMany(r => r.CustomSettings)
                    .Select(r => new object[] { r.ClientSettingsNotificationId, r.Key, r.Value, true })
                    .ToArray()));

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Active" },
              values: toRectangular(clientSettings
                .SelectMany(r => r.Steps)
                .Select(r => new object[] { r.ClientSettingsNotificationId, r.Template, r.Order, true })
                .ToArray()));

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Step_AvailableCustomSettings",
              columns: new[] { "ClientSettingsNotificationStepId", "Setting", "Active" },
              values: toRectangular(clientSettings
               .SelectMany(r => r.Steps)
                .SelectMany(r => r.AvailableCustomSettings)
                .Select(r => new object[] { r.ClientSettingsNotificationStepId, r.Setting, true }).ToArray()));

            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: toRectangular(clientSettings
                .SelectMany(r => r.Steps)
                .SelectMany(r => r.CustomSettings)
                .Select(r => new object[] { r.ClientSettingsNotificationStepId, r.Key, r.Value, true }).ToArray()));
        }

        protected void Production_AddTrainingProgramTypeTable()
        {
            _migrationBuilder.InsertData(
                 table: "TrainingProgramTypes",
                 columns: new[] { "TrainingProgramTypeTitle", "Active", "Deleted" },
                 values: new object[,]
                 {
                        { "Initial Training Program", true, false},
                        { "Continuing Training Program", true, false},
                        { "Cycle Training Program", true, false},
                 });
        }

        protected void Production_UpdateTrainingProgramTypeTable_VersionIsYear()
        {
            var trainingProgramTypes = new object[,]
            {
                { "Initial Training Program", true},
                { "Continuing Training Program", false},
                { "Cycle Training Program", true},
            };

            for (int i = 0; i < trainingProgramTypes.GetLength(0); i++)
            {
                _migrationBuilder.UpdateData(
                    table: "TrainingProgramTypes",
                    keyColumn: "TrainingProgramTypeTitle",
                    keyValue: trainingProgramTypes[i, 0],
                    column: "VersionIsYear",
                    value: trainingProgramTypes[i, 1]);
            }
        }

        protected void Production_AddTaskQualificationStatusTable()
        {
            _migrationBuilder.InsertData(
               table: "TaskQualificationStatuses",
               columns: new[] { "Name", "Description", "Active" },
               values: new object[,]
               {
                        {"Trainee Initial Qualification","Employee is a Trainee for the position, and the qualification is his/her Initial Qualification record to indicate successful performance on the Task", true },
                        {"On Time","Employee qualified on the Task within the requalification due date window", true },
                        {"Pending","Employee has not qualified on the Task, but the current date is still within the requalification due date window", true },
                        {"Delayed","Employee qualified on the Task outside of the requalification due date window", true },
                        {"Overdue","The 6-month window has passed, Employee has not qualified", true },
                        {"Requalification Not Required","Employee was flagged a Trainee for the position at the time of the Task change. Employee qualified on revised task as part of initial training", true },
                        {"No Position Qual Date","The Employee is not flagged as a Trainee and there is no Position Qual Date in the Employee window to use to confirm the task qual against", true },
                        {"Completed","Task Requalification is completed on Emp side", true },
                        {"Unmappable","Task Requalification status was unmappable from legacy", true }
               }
               );
        }

        protected void Production_AddRatingScaleNTable()
        {
            _migrationBuilder.InsertData(
                 table: "RatingScaleNs",
                 columns: new[] { "RatingScaleDescription", "Active" },
                 values: new object[,]
                 {
                        { "1 = Strongly Disagree, 2 = Disagree, 3 = Neutral, 4 = Agree, 5 = Strongly Agree", true },
                        { "1 = Poor, 2 = Fair, 3 = Good, 4 = Very Good, 5 = Excellent", true },
                        { "1 = Poor, 2 = Unsatisfactory, 3 = Satisfactory, 4 = Very Satisfactory, 5 = Outstanding", true },
                        { "1 = Very Poor, 2 = Poor, 3 = Average, 4 = Good, 5 = Very Good", true },

                        { "1 = Strongly Disagree, 2 = Disagree, 3 = Agree, 4 = Strongly Agree", true },
                        { "1 = Poor, 2 = Fair, 3 = Very Good, 4 = Excellent", true },
                        { "1 = Poor, 2 = Unsatisfactory, 3 = Very Satisfactory, 4 = Outstanding", true },
                        { "1 = Very Poor, 2 = Poor, 3 = Good, 4 = Very Good", true },

                        { "1 = Disagree, 2 = Neutral, 3 = Agree", true },
                        { "1 = Poor, 2 = Good, 3 = Excellent", true },
                        { "1 = Poor, 2 = Satisfactory, 3 = Outstanding", true },
                        { "1 = Very Poor, 2 = Average, 3 = Very Good", true },

                        { "1 = Yes, 2 = No", true }
                 });
        }

        protected void Production_AddIDP_ReviewTable()
        {
        }

        protected void Production_AddClassSchedule_Roster_StatusesTable()
        {
            _migrationBuilder.InsertData(
               table: "ClassSchedule_Roster_Statuses",
               columns: new[] { "Name", "Active" },
               values: new object[,]
               {
                    { "Not Started", true },
                    { "In Progress", true },
                    { "Completed", true },
                    { "Released", true },
               });
        }

        protected void Production_AddEvaluationMethodTable()
        {
            _migrationBuilder.InsertData(
              table: "EvaluationMethods",
              columns: new[] { "Description", "Active" },
              values: new object[,]
              {
                    { "ILA", true },
                    { "OJT", true },
                    { "SIM", true },
                    { "Other", true },
              });
        }

        protected void Production_AddIDP_ReviewStatusTable()
        {
        }

        protected void Production_AddDatabaseSettings()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_GeneralSettings",
              columns: new[] { "CompanyName", "CompanyLogo", "DateFormat", "ClassStartEndTimeFormat", "CompanySpecificCoursePassingScore", "Active" },
              values: new object[] { "Company Name", "data:image/jpeg;base64,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", "mm/dd/yyyy", "24-hour clock", ".93", true });


            List<ClientSettings_LabelReplacement> clientSettingsLabelReplacement = new List<ClientSettings_LabelReplacement>();

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Employee"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Position"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Task"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Enabling Objectives"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Certifications"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Procedures"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Safety Hazards"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Tools"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Regulatory Requirements"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Definitions"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Instructions"
            });

            clientSettingsLabelReplacement.Add(new ClientSettings_LabelReplacement()
            {
                DefaultLabel = "Locations"
            });


            _migrationBuilder.InsertData(
                 table: "ClientSettings_LabelReplacements",
                 columns: new[] { "DefaultLabel", "LabelReplacement", "Active" },
                 values: toRectangular(clientSettingsLabelReplacement.Select(r => new object[] { r.DefaultLabel, r.LabelReplacement, true }).ToArray()));
        }

        protected void Production_AddClassScheduleTable()
        {
        }

        protected void Production_AddLocationCategories()
        {
            _migrationBuilder.InsertData(
                  table: "Location_Categories",
                  columns: new[] { "LocCategoryTitle", "LocCategoryDesc", "EffectiveDate", "Active" },
                  values: new object[,]
                  {
                        { "Legacy", "Locations that came over from migration.", System.DateTime.Now, true     }
                  });
        }

        protected void Production_AddEnablingObjective_Employee_LinksTable()
        {

        }

        protected void Production_AddTaskQualificationTable()
        {

        }

        protected void Production_AddPositionSQsTable() { }

        protected void Production_AddPositionHistoriesTable() { }


        protected void Production_AddLocation()
        {
        }

        protected void Production_AddILA_Evaluator_LinksTable() { }

        protected void Production_AddILA_Position_LinksTable() { }

        protected void Production_AddClassScheduleEmployeesTable() { }

        protected void Production_AddSelfRegistrationOptionsTable() { }

        protected void Production_AddClassSchedule_Evaluation_RosterTable() { }

        protected void Production_AddILA_SafetyHazard_LinksTable() { }

        protected void Production_AddILA_Procedure_LinksTable() { }

        protected void Production_AddILA_Segment_LinksTable() { }

        protected void Production_AddILA_EnablingObjective_LinksTable() { }

        protected void Production_AddILA_TaskObjective_LinksTable() { }

        protected void Production_AddTestItemLinksTable() { }

        protected void Production_AddQuestionBankTable() { }

        protected void Production_AddStudentEvaluationWithoutEmpTable() { }

        protected void Production_AddTrainingPrograms_ILA_LinksTable() { }

        protected void Production_AddTaskQualification_Evaluator_LinksTable() { }

        protected void Production_AddPosition_TasksTable() { }

        protected void Production_AddILA_PreRequisite_LinksTable() { }

        protected void Production_AddILA_UploadsTable() { }

        protected void Production_AddILA_NERCAudience_LinksTable() { }

        protected void Production_AddStudentEvaluation_QuestionTable() { }

        protected void Production_AddDocumentTypesTable()
        {
            _migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Name", "LinkedDataType", "Deleted", "Active" },
                values: new object[,]
                  {
                       {"Training Program Reviews","TrainingProgramReviews",false,true},
                       {"Employee Details","Employees",false,true},
                       {"DIF Survey","Positions",true,true},
                       {"OJT","Employees",true,true},
                       {"Other Employee Course Completion Info","Employees",false,true},
                       {"Sign In Sheet","ClassSchedules",false,true},
                       {"Student Evaluation","ClassSchedules",false,true},
                       {"Task Qualification","TaskQualifications",false,true},
                       {"Test","ClassScheduleRosters",false,true},
                       {"Tool","ToolCategory",false,true}
                  });
        }

        protected void Production_AddILA_TrainingTopic_LinksTable() { }

        protected void Production_AddInstructorWorkbook()
        {
            _migrationBuilder.InsertData(
                table: "InstructorWorkbook_Phases",
                columns: new[] { "CoursePhaseDescription", "Deleted", "Active" },
                values: new object[,]
                 {
                                   { "Analysis", false, true },
                                   { "Design", false, true },
                                   { "Develop", false, true },
                                   { "Implement", false, true},
                                   { "Evaluate", false, true}
                 });
        }

        protected void Production_UpdateMetaILAConfigurationPublishOption()
        {
            _migrationBuilder.UpdateData(
                table: "MetaILAConfigurationPublishOptions",
                keyColumn: "Description",
                keyValue: "Upon Clicking Start",
                column: "Description",
                value: "On Demand");
        }

        protected void Production_UpdateDocumentTypeTable()
        {
            _migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Name", "LinkedDataType", "Deleted", "Active" },
                values: new object[,]
                  {
                      {"Tool","ToolCategorys",false,true}
                  });
        }

        protected void Production_AddMetaIlaEmployees()
        {
            _migrationBuilder.InsertData(
               table: "ClientSettings_Notifications",
               columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
               values: new object[,]
                   {
                      {"Meta ILA - Self Paced Released", true, "Sent after a Self Paced class is released due to completion of a step in a Meta ILA", false, true},
                      {"Meta ILA - Employee - Self Registration Needed", true, "Sent to employee when they need to enroll in a self-registration enabled class after completing a step in an Meta ILA", false, true},
                      {"Meta ILA - Admin - Self Registration Needed", true, "Sent to an admin when they may need to assist an employee in enrolling in a self-registration enabled class after completing a step in an Meta ILA", false, true},
                      {"Meta ILA - Employee - Registration Needed", true, "Sent to employee when an admin needs to enroll them in a self-registration enabled class after completing a step in an Meta ILA", false, true},
                      {"Meta ILA - Admin - Registration Needed", true, "Sent to an admin when may need to assist an employee in enrolling in a self-registration enabled class after completing a step in an Meta ILA", false, true},
                      {"Meta ILA - Coursework Complete", true, "Sent to an employee when have completed the coursework for a Meta ILA.  Informs the employee about tests and evaluations to complete.", false, true},
                   });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
                      {16, @"
Hello @Model.EmployeeFirstName @Model.EmployeeLastName

Congraulations on completing the course @Model.PreviousILATitle.

Because you are enrolled in a Meta ILA, your course @Model.ILATitle has been already released.

Please log into your EMP Dashboard to complete the course.
                              
If you are unable to complete the course by the due date listed, notify your Training Administrator as soon as possible. 

Thank you!", 1, false, true },
                      {17, @"Hello @Model.EmployeeFirstName @Model.EmployeeLastName

Congraulations on completing the course @Model.PreviousILATitle.

Because you are enrolled in a Meta ILA, you need to enroll yourself in the course @Model.ILATitle.

@if(Model.RegistrationsAvailable)
{

Please log into your EMP Dashboard and use the self registration portal to enroll in a class.

If you are unable to attend the classes listed notify your Training Administrator as soon as possible.

}
else
{
	There are no upcoming classes for the ILA.Please notification your Training Administrator as soon as possible
}


Thank you!", 1, false, true },
                      {18, @"Hello,

Please be aware that @Model.EmployeeFirstName @Model.EmployeeLastName has completed the ILA. @Model.PreviousILATitle.

As part of the Meta ILA @Model.MetaILATitle they are now required to take the ILA @Model.ILATitle.

@if(Model.RegistrationsAvailable)
{
The employee has been notified that they need to self register in the ILA.  However, it is possible that the employee may request a different time.  They have been instructed to reach out to their Training Administrator if the times do not work for them.
}
else
{
The employee has been notified that they need to self register in the ILA.  However currently there are no available classes for the employee to register in.  You will need to create an additional class for the employee to continue.
}

Thank you.", 1, false, true },
                      {19, @"Hello @Model.EmployeeFirstName @Model.EmployeeLastName

Congraulations on completing the course @Model.PreviousILATitle.

Because you are enrolled in a Meta ILA, you will be enrolled in the next course @Model.ILATitle.

Please cooridinate your Training Administrator as soon as possible                              

Thank you!", 1, false, true },
                      {20, @"
Hello,

Please be aware that @Model.EmployeeFirstName @Model.EmployeeLastName has completed the ILA. @Model.PreviousILATitle.

As part of the Meta ILA @Model.MetaILATitle they are now required to take the ILA @Model.ILATitle.

The employee has been notified that they need to be enrolled in the next ILA however that ILA is not configured to allow self-registration.  You will need to assist the employee for them to continue.

Thank you.", 1, false, true },
                      {21, @"
Hello,

Congraulations on completing the course @Model.PreviousILATitle.

You have now completed the coursework for the your Meta ILA.

To complete your training on this Meta ILA you need to complete the associated test.  It has been released to your EMP portal.  

Thank you.", 1, false, true }
                  });
        }

        protected void Production_AddMetaIlaTestType()
        {
            _migrationBuilder.InsertData(
                  table: "TestTypes",
                  columns: new[] { "Description", "Active", "Deleted" },
                  values: new object[,]
                  {
                        { "Meta ILA Summary", true, false }
                  });
        }

        protected void Production_AddReports_EmployeeTrainingNeedsAssessment()
        {
            _migrationBuilder.InsertData(
              table: "ReportSkeletons",
              columns: new[] { "Id", "DefaultTitle", "Deleted", "Active" },
              values: new object[,]
                {
                 {38, "Employee Training Needs Assessment", false, true }
                }
              );

            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active" },
                values: new object[,]
                {
                  {38, "Employee", "Int", "Array", DateTime.MinValue,DateTime.MinValue, "employees", false, true},
                  {38, "Include score option", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true }
                }
              );

            _migrationBuilder.InsertData(
                table: "ReportSkeleton_Subcategory_Reports",
                columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Deleted", "Active" },
                values: new object[,]
                {
                  {13, 38, false, true },
                  {24, 38, false, true },
                  {28, 38, false, true },
                  {30, 38, false, true },
                }
              );
        }

        protected void Production_AddReports_ClassRoster()
        {
            _migrationBuilder.InsertData(
              table: "ReportSkeletons",
              columns: new[] { "Id", "DefaultTitle", "Deleted", "Active" },
              values: new object[,]
                {
                 {39, "Class Roster", false, true }
                }
              );

            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active" },
                values: new object[,]
                {
                  {39, "Training Classes", "Int", "Array", DateTime.MinValue,DateTime.MinValue, "ClassSchedule", false, true},
                  {39, "Include Employee Phone and Email", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true}
                }
              );

            _migrationBuilder.InsertData(
                table: "ReportSkeleton_Subcategory_Reports",
                columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Deleted", "Active" },
                values: new object[,]
                {
                  {17, 39, false, true },
                  {22, 39, false, true },
                  {30, 39, false, true },
                  {33, 39, false, true }
                }
              );
        }

        protected void Production_AddReports_AnnualPositionsTaskListReview()
        {
            _migrationBuilder.InsertData(
              table: "ReportSkeletons",
              columns: new[] { "Id", "DefaultTitle", "Deleted", "Active" },
              values: new object[,]
                {
                 {40, "Annual Positions Task List Review", false, true }
                }
              );

            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active" },
                values: new object[,]
                {
                  {40, "Positions", "String", "Single", DateTime.MinValue,DateTime.MinValue, "Positions", false, true},
                  {40, "Date Range", "Date", "Range", DateTime.MinValue,DateTime.MinValue, "", false, true},
                  {40, "Show All Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true },
                  {40, "Include Psuedo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true },
                  {40, "Only R-R* for any of the Positions", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true }
                }
              );

            _migrationBuilder.InsertData(
                table: "ReportSkeleton_Subcategory_Reports",
                columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Deleted", "Active" },
                values: new object[,]
                {
                  {14, 40, false, true },
                  {15, 40, false, true },
                  {19, 40, false, true },
                  {20, 40, false, true },
                }
              );
        }

        protected void Production_UpdateClientSettings_GeneralSettings_DateFormat()
        {
            _migrationBuilder.UpdateData(
                table: "ClientSettings_GeneralSettings",
                keyColumn: "CompanyName",
                keyValue: "QTD",
                column: "DateFormat",
                value: "MM/dd/yyyy");
        }

        protected void Production_UpdateTaskReQualificationEmpSignOffData()
        {
            _migrationBuilder.UpdateData(
                table: "TaskReQualificationEmp_SignOffs",
                keyColumn: "IsCompleted",
                keyValue: true,
                columns: new[] { "IsTraineeSignOff", "IsEvaluatorSignOff" },
                values: new object[] { true, true }
                );
        }

        protected void Production_AddClassSchedule_SelfRegistrationOptionsData()
        {
            //      _migrationBuilder.Sql(@"INSERT INTO ClassSchedule_SelfRegistrationOptions
            //   (
            //    ClassScheduleId,
            //    MakeAvailableForSelfReg,
            //    RequireAdminApproval,
            //    AcknowledgeRegDisclaimer,
            //    RegDisclaimer,
            //    LimitForLinkedPositions,
            //    CloseRegOnStartDate,
            //    ClassSize,
            //    EnableWaitlist,
            //    SendApprovedEmail,
            //    Deleted,
            //    Active,
            //    CreatedBy,
            //    CreatedDate
            //)
            //      SELECT  cs.Id,
            //    MakeAvailableForSelfReg,
            //    RequireAdminApproval,
            //    AcknowledgeRegDisclaimer,
            //    RegDisclaimer,
            //    LimitForLinkedPositions,
            //    CloseRegOnStartDate,
            //    ClassSize,
            //    EnableWaitlist,
            //    SendApprovedEmail,
            //    o.Deleted,
            //    o.Active,
            //    o.CreatedBy,
            //    o.CreatedDate
            //      FROM [ILA_SelfRegistrationOptions] o
            //   inner join [ClassSchedules] cs on cs.ILAID = o.Ilaid");
        }

        protected void Production_ModifySelfRegistrationOptions()
        {
            //_migrationBuilder.Sql(@"
            //    update ilas set ClassSize = ILA_SelfRegistrationOptions.classSize 
            //    from ILA_SelfRegistrationOptions
            //    where ILA_SelfRegistrationOptions.ILAId = ilas.id

            //     update ilas set ClassSize = IsNull(ClassSize, 30)

            //    update ClassSchedules set ClassSize = ILA_SelfRegistrationOptions.classSize 
            //    from ILA_SelfRegistrationOptions
            //    where ILA_SelfRegistrationOptions.ILAId = ClassSchedules.ILAId");
        }

        protected void Production_UpdateCertifyingBodiesTableData()
        {
            _migrationBuilder.UpdateData(
            table: "CertifyingBodies",
            keyColumn: "Name",
            keyValue: "NERC",
            column: "EnableCertifyingBodyLevelCEHEditing",
            value: true
            );
        }

        protected void Production_ModifyClientSettingNotificationTemplates()
        {
            string clientSettingsJsonString = System.IO.File.ReadAllText(_path + "\\clientsettings_notifications.json");
            List<ClientSettings_Notification> clientSettings = JsonSerializer.Deserialize<List<ClientSettings_Notification>>(clientSettingsJsonString);
            foreach (var clientSettingNotification in clientSettings)
            {
                foreach (var step in clientSettingNotification.Steps)
                {
                    var query = @" UPDATE ClientSettings_Notification_Steps SET Template = '" + step.Template.Replace("'", "''") + @"'  
                                WHERE ClientSettingsNotificationId IN (SELECT id FROM ClientSettings_Notifications WHERE name = '" + clientSettingNotification.Name + @"') 
                                AND [Order] = " + step.Order + @";";
                    _migrationBuilder.Sql(query);
                }
            }
        }
        protected void Production_UpdateILATaskObjectiveLinksSequenceNumber()
        {
            _migrationBuilder.Sql(@" WITH TaskSequence AS (
                                    SELECT ilaid,taskid,ROW_NUMBER() OVER (PARTITION BY ilaid ORDER BY id) AS new_sequence FROM ILA_TaskObjective_Links where Deleted=0
                                    )
                                    UPDATE ILA_TaskObjective_Links
                                    SET SequenceNumber = TaskSequence.new_sequence
                                    FROM ILA_TaskObjective_Links AS ilaTask
                                    INNER JOIN TaskSequence ON ilaTask.taskid = TaskSequence.taskid and ilaTask.ILAId=TaskSequence.ILAId; "
                                );
        }

        protected void Production_AddDIFSurvey_DevStatus()
        {

            _migrationBuilder.InsertData(
                table: "DIFSurvey_DevStatus",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Draft", true },
                  { "Published", true },
                });
        }

        protected void Production_AddDIFSurvey_Employee_Status()
        {

            _migrationBuilder.InsertData(
                table: "DIFSurvey_Employee_Status",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Not Started", true },
                  { "In Progress", true },
                  { "Completed", true },
                });
        }
        protected void Production_AddDIFSurvey_Task_Status()
        {

            _migrationBuilder.InsertData(
                table: "DIFSurvey_Task_Status",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Initial Training", true },
                  { "Continuous Training", true },
                  { "No Training Required", true },
                  { "No Responses Yet", true }
                });
        }

        protected void Production_AddDIFSurvey_Task_TrainingFrequency()
        {

            _migrationBuilder.InsertData(
                table: "DIFSurvey_Task_TrainingFrequency",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Quarterly", true },
                  { "Annually", true },
                  { "Every 2 Years", true },
                });
        }

        protected void Production_AddDIFSurvey()
        {
        }

        protected void Production_AddDIFSurvey_Employee()
        {
        }

        protected void Production_AddDIFSurvey_Task()
        {
        }

        protected void Production_AddDIFSurvey_Employee_Response()
        {
        }
        protected void Production_AddEmailNotification_AdminEmployeePortalCompletions()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_Notifications",
              columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
              values: new object[,]
                  {
                      {"Admin - Employee Portal Completions", true, "To Do Admin - Employee Portal Completions Timing Text", false, true}
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
                      {22, "This email is in testing", 1, false, true }
                  });
        }

        protected void Production_UpdateInstructorWorkbook_ILADesign_Segments()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[InstructorWorkbook_ILADesign_Segments]
            SET SegmentTitle = CONCAT('Segment', Id)");
        }

        protected void Production_AddClientSettings_Feature()
        {

            _migrationBuilder.InsertData(
                table: "ClientSettings_Features",
                columns: new[] { "Feature", "Enabled", "Active", "Deleted" },
                 values: new object[,]
                {
                  { "RSAW", true,true,false },
                  { "CEH Upload", true,true,false }
                });
        }

        protected void Production_UpdateClientSettings_LabelReplacementsEnhanced()
        {
            _migrationBuilder.InsertData(
             table: "ClientSettings_LabelReplacements",
             columns: new[] { "DefaultLabel", "Active" },
              values: new object[,]
                 {
                      {"ILA", true },
                      {"My Data", true },
                      {"Instructor",true }
                 });

            _migrationBuilder.UpdateData(
             table: "ClientSettings_LabelReplacements",
             keyColumns: new[] { "Id" },
             keyValues: new object[] { 4 },
             columns: new[] { "DefaultLabel" },
             values: new object[] { "Enabling Objective" }
             );

            _migrationBuilder.UpdateData(
               table: "ClientSettings_LabelReplacements",
               keyColumns: new[] { "Id" },
               keyValues: new object[] { 5 },
               columns: new[] { "DefaultLabel" },
               values: new object[] { "Certification" }
               );

            _migrationBuilder.UpdateData(
              table: "ClientSettings_LabelReplacements",
              keyColumns: new[] { "Id" },
              keyValues: new object[] { 6 },
              columns: new[] { "DefaultLabel" },
              values: new object[] { "Procedure" }
              );

            _migrationBuilder.UpdateData(
              table: "ClientSettings_LabelReplacements",
              keyColumns: new[] { "Id" },
              keyValues: new object[] { 7 },
              columns: new[] { "DefaultLabel" },
              values: new object[] { "Safety Hazard" }
              );

            _migrationBuilder.UpdateData(
             table: "ClientSettings_LabelReplacements",
             keyColumns: new[] { "Id" },
             keyValues: new object[] { 8 },
             columns: new[] { "DefaultLabel" },
             values: new object[] { "Tool" }
             );

            _migrationBuilder.UpdateData(
             table: "ClientSettings_LabelReplacements",
             keyColumns: new[] { "Id" },
             keyValues: new object[] { 9 },
             columns: new[] { "DefaultLabel" },
             values: new object[] { "Regulatory Requirement" }
             );

            _migrationBuilder.UpdateData(
              table: "ClientSettings_LabelReplacements",
              keyColumns: new[] { "Id" },
              keyValues: new object[] { 10 },
              columns: new[] { "DefaultLabel" },
              values: new object[] { "Definition" }
              );

            _migrationBuilder.UpdateData(
             table: "ClientSettings_LabelReplacements",
             keyColumns: new[] { "Id" },
             keyValues: new object[] { 11 },
             columns: new[] { "DefaultLabel" },
             values: new object[] { "Instruction" }
             );

            _migrationBuilder.UpdateData(
                table: "ClientSettings_LabelReplacements",
                keyColumns: new[] { "Id" },
                keyValues: new object[] { 12 },
                columns: new[] { "DefaultLabel" },
                values: new object[] { "Location" }
                );

        }

        protected void Production_Update_TblClientSettings_GeneralSettings()
        {
            _migrationBuilder.Sql("UPDATE ClientSettings_GeneralSettings SET CompanySpecificCoursePassingScore = '65'");
        }

        protected void Production_UpdateActivityNotificationsTable()
        {
            _migrationBuilder.Sql("DELETE FROM ActivityNotifications");
            _migrationBuilder.InsertData(
                 table: "ActivityNotifications",
                 columns: new[] { "Title", "Active" },
                 values: new object[,]
                 {
                        { "Tests", true },
                        { "Online Courses", true },
                        { "Procedure Reviews", true },
                        { "Task & Skill Qualifications", true },
                        { "DIF Surveys", true },
                        { "Gap Surveys", true },
                        { "Student Evaluations", true },
                 });
        }

        protected void Production_AddAdminEmailNotificationStepsSettings()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Step_AvailableCustomSettings",
              columns: new[] { "ClientSettingsNotificationStepId", "Setting", "Active" },
              values: new object[,] { { 24, "Email Frequency", true } }
              );
            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: new object[,] { { 24, "Email Frequency", "Weekly", true } }
              );
        }

        protected void Production_UpdateAdminEmailNotificationTimingText()
        {
            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notifications",
             keyColumns: new[] { "Id", "Name" },
             keyValues: new object[] { 22, "Admin - Employee Portal Completions" },
             column: "TimingText",
             value: "Once enabled, this email will be sent to all QTD Users based on the intervals set below");
        }

        protected void Production_UpdateClientSettingsNotification_Admin()
        {
            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notifications",
             keyColumns: new[] { "Id", "Name" },
             keyValues: new object[] { 22, "Admin - Employee Portal Completions" },
             column: "Enabled",
             value: false);
        }

        protected void Production_UpdateAdminEmailNotificationStepsSettings()
        {
            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 4, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods; Hello @Model.EmployeeFirstName @Model.EmployeeLastName In order to receive
                        credit for completion of @Model.CourseTitle, you must also complete the online test.You can access the test through the
                        Employee Portal(EMP).Please review the table below for further details. <table>
                            <tr>
                                <td> Course Title </td>
                                <td> @Model.CourseTitle </td>
                            </tr>
                            <tr>
                                <td> Instructor / Location </td>
                                <td> @Model.Instructor / @Model.Location </td>
                            </tr>
                            <tr>
                                <td> Class Dates </td>
                                <td> @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) -
                                    @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                            </tr>
                            <tr>
                                <td> Test Due Date </td>
                                <td> @Model.TestDueDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                            </tr>
                        </table>
                        If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon
                        as possible.Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 2, 3 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                        Hello  @Model.EmployeeFirstName @Model.EmployeeLastName
                        Your immediate attention is required.This is an urgent reminder from the Training Department that your
                        @Model.CertificateName certificate @Model.CertificateNumber
                        will expire in @Model.DaysUntilCertificationExpiration days.Your expiration date
                        is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date,
                        we have not received a copy of your updated @Model.CertificateName certificate.
                        After multiple notifications regarding the renewal of your @Model.CertificateName Credential,
                        your management has also received a copy of this notification.You and your management will continue
                        to receive daily reminders until the System Operations Training Team receives an updated copy of your
                        @Model.CertificateName certificate.  If you received this message in error or are no longer maintaining this
                        certificate, we ask that you let us know so we can update our records.If you have any questions,
                        please reach out to your Training Administrator.    
                        Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 2, 2 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                        Hello  @Model.EmployeeFirstName @Model.EmployeeLastName 
                        Your immediate attention is required.This is an urgent reminder from the Training Department that your
                        @Model.CertificateName certificate @Model.CertificateNumber
                        will expire in @Model.DaysUntilCertificationExpiration days.Your expiration date
                        is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, 
                        we have not received a copy of your updated @Model.CertificateName certificate.   
                        After multiple notifications regarding the renewal of your @Model.CertificateName Credential, 
                        your management has also received a copy of this notification.You and your management will continue
                        to receive daily reminders until the System Operations Training Team receives an updated copy of your
                        @Model.CertificateName certificate.  If you received this message in error or are no longer maintaining this
                        certificate, we ask that you let us know so we can update our records.If you have any questions,
                        please reach out to your Training Administrator.    
                        Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 2, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                        Hello @Model.EmployeeFirstName @Model.EmployeeLastName
                           This is a reminder from the Training Department that your
                        @Model.CertificateName certificate @Model.CertificateNumber will expire in
                        @Model.DaysUntilCertificationExpiration
                        days.Your expiration date is 
                        @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). 
                        To date, we have not received a copy of your updated @Model.CertificateName certificate.   
                                        If you received this message in error or are no longer maintaining this certificate, 
                        we ask that you let us know so we can update our records.If you have any questions,
                        please reach out to your Training Administrator.    
                               Thank you!");

            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notification_Steps",
            keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
            keyValues: new object[] { 5, 1 },
            column: "Template",
            value: @"@using QTD2.Infrastructure.ExtensionMethods;
                        Hello @Model.EmployeeFirstName @Model.EmployeeLastName
                        You have been enrolled in a course which requires you to complete a pretest prior to the course
                        start date.You can access the pretest through the Employee Portal(EMP).Please review the table below for further
                        details.
                        <table>
                            <tr>
                                <td>Course Title</td>
                                <td>@Model.CourseTitle</td>
                            </tr>
                            <tr>
                                <td>Instructor/Location</td>
                                <td>@Model.Instructor/@Model.Location</td>
                            </tr>
                            <tr>
                                <td>Class Dates </td>
                                <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)
                                    - @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                            </tr>
                            <tr>
                                <td>Pretest ID/Name </td>
                                <td>@Model.PretestId/@Model.PretestTitle</td>
                            </tr>
                            <tr>
                                <td>Pretest Available Date</td>
                                <td>@Model.PretestAvailableDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                            </tr>
                            <tr>
                                <td>Pretest Due Date </td>
                                <td>
                                    @Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                            </tr>
                        </table>
                        If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as
                        possible.
                        
                        Thank you!");

            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notification_Steps",
            keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
            keyValues: new object[] { 6, 1 },
            column: "Template",
            value: @"@using QTD2.Infrastructure.ExtensionMethods;
                     Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned an online training course to complete.You
                     can access the course through the Employee Portal(EMP).Please review the table below for further details.
                      <table>
                         <tr>
                             <td>Course Title</td>
                             <td>@Model.ILATitle</td>
                         </tr>
                         <tr>
                             <td>Instructor/Location</td>
                             <td>@Model.Instructor/@Model.Location</td>
                         </tr>
                         <tr>
                             <td>Class Dates </td>
                             <td>
                                 @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) -
                                 @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                         </tr>
                         <tr>
                             <td>Course Available Date</td>
                             <td>
                                 @Model.CourseStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                         </tr>
                         <tr>
                             <td>Course Due Date</td>
                             <td>
                                 @Model.CourseEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                         </tr>
                     </table>
                     If you are unable to complete the course by the due date listed,
                     notify your Training Administrator as soon as possible.Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 7, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                 Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned to complete an evaluation for a course you
                 recently completed.
                 You can access the evaluation through the Employee Portal(EMP).Please review the table below for further details.
                 <table>
                     <tr>
                         <td>Course Title</td>
                         <td>@Model.ILATitle</td>
                     </tr>
                     <tr>
                         <td>Instructor/Location</td>
                         <td>@Model.Instructor/@Model.Location</td>
                     </tr>
                     <tr>
                         <td>Class Dates </td>
                         <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)
                             - @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                     </tr>
                     <tr>
                         <td>Eval Available Date</td>
                         <td>@Model.CourseEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                     </tr>
                     <tr>
                         <td>Eval Due Date </td>
                         <td>
                             @Model.EvalDueDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                     </tr>
                 </table>
                 If you are unable to complete the evaluation by the due date listed, notify your Training Administrator.
                 Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 8, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                    Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned to complete a procedure review.You can
                    complete the procedure review through the
                    Employee Portal(EMP).Please review the table below for further details.
                    <table>
                        <tr>
                            <td>Procedure Title</td>
                            <td>@Model.ProcedureTitle</td>
                        </tr>
                        <tr>
                            <td>Review Available Date</td>
                            <td>
                                @Model.ReviewStartdate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                        </tr>
                        <tr>
                            <td>Review Due Date</td>
                            <td>
                                @Model.ReviewEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                        </tr>
                    </table>
                    If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon
                    as possible.
                    Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 9, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                    Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned to complete an IDP review.You can complete
                    the IDP review through the Employee
                    Portal(EMP).Please review the table below for further details.
                    <table>
                        <tr>
                            <td>IDP Title</td>
                            <td>@Model.IDPTitle</td>
                        </tr>
                        <tr>
                            <td>Review Available Date</td>
                            <td>
                                @Model.ReviewStartdate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                        </tr>
                        <tr>
                            <td>Review Due Date</td>
                            <td>
                                @Model.ReviewEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                        </tr>
                    </table>
                    If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon
                    as possible.
                    Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 12, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                Hello @Model.EmployeeFirstName @Model.EmployeeLastName Your self - registration request to enroll in @Model.CourseTitle on
                @@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) and
                @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) has been approved.
                Thank you!");


            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 13, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                Hello @Model.EmployeeFirstName @Model.EmployeeLastName Your self - registration request to enroll in @Model.CourseTitle on
                @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) and
                @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) has been denied.
                Please contact your Training Administrator for additional information.
                Thank you");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 14, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                           Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned to complete a GAP Survey.You can access
                           the GAP Survey through the Employee
                           Portal(EMP).Please review the table below for further details. This survey is being conducted to help us improve and
                           provide on target training for
                           @Model.PositionTitle position and your input is an important part of this process. 
                           <table>
                               <tr>
                                   <td>Position Title</td>
                                   <td>@Model.PositionTitle</td>
                               </tr>
                               <tr>
                                   <td>Survey Title</td>
                                   <td>@Model.SurveyTitle</td>
                               </tr>
                               <tr>
                                   <td>Survey Available Date</td>
                                   <td>
                                       @Model.SurveyStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                               </tr>
                               <tr>
                                   <td>Survey Due Date </td>
                                   <td>@Model.SurveyEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                               </tr>
                           </table>
                           If you are unable to complete the GAP Survey by the due date listed, notify your Training Administrator as soon as
                           possible.
                           Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 15, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                           Hello @Model.EmployeeFirstName @Model.EmployeeLastNam You have been assigned to complete a GAP Survey.You can access the
                           GAP Survey through the Employee
                           Portal(EMP).Please review the table below for further details. This survey is being conducted to help us improve and
                           provide on target training for
                           @Model.PositionTitle position and your input is an important part of this process.
                            <table>
                               <tr>
                                   <td>Position Title</td>
                                   <td>@Model.PositionTitle</td>
                               </tr>
                               <tr>
                                   <td>Survey Title</td>
                                   <td>@Model.SurveyTitle</td>
                               </tr>
                               <tr>
                                   <td>Survey Available Date</td>
                                   <td>
                                       @Model.SurveyStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                               </tr>
                               <tr>
                                   <td>Survey Due Date </td>
                                   <td>
                                       @Model.SurveyEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                               </tr>
                           </table> 
                           If you are unable to complete the GAP Survey by the due date listed,
                           notify your Training Administrator as soon as possible. Thank you!");


        }

        protected void Production_AddEmpSettingsReleaseTypes()
        {

            _migrationBuilder.InsertData(
                table: "EmpSettingsReleaseTypes",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "Days", true },
                  { "Weeks", true },
                  { "Months", true },
                });
        }

        protected void Production_UpdateEmpSettingsReleaseTypes()
        {

            _migrationBuilder.Sql(
               @" UPDATE TestReleaseEMPSettings
	                SET EmpSettingsReleaseTypeId = 1 ; "
            );

            _migrationBuilder.Sql(
               @" UPDATE EvaluationReleaseEMPSettings
                    SET EmpSettingsReleaseTypeId = 1 ; "
            );
        }

        protected void Production_UpdateEmailNotificationTemplate()
        {

            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notification_Steps",
            keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
            keyValues: new object[] { 22, 1 },
            column: "Template",
            value: @"@using QTD2.Infrastructure.ExtensionMethods;
                    <style>
                        .emp-completion-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .emp-completion-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .emp-completion-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                    <p>Hello,</p>
                    <p>Below please find the Employee Portal Training Completion Report. This report lists the training that has been completed and submitted by Employees via the Employee Portal.</p>
                    <p>Refer to applicable QTD reports for detailed training completion information. </p>
                    <p>To change the frequency of this report, navigate to <b>Templates and Forms > Email Notifications > Employee Portal Completions</b></p>
                    <p>If you have questions, please reach out to your training administrator.</p>
                    <table class='emp-completion-table'>
                        <thead>
                            <tr>
                                <th style='width: 30%;'>Name</th>
                                <th style='width: 30%;'>Completion Type</th>
                                <th style='width: 40%;'>Completion Date</th>
                            </tr>
                        </thead>
                        <tbody>
                            @foreach (var item in Model.Items)
                            {
                                <tr>
                                    <td>@item.Title</td>
                                    <td>@item.CompletionType</td>
                                    <td>@item.CompletionDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                </tr>
                            }
                        </tbody>
                    </table>");
        }

        protected void Production_AddTaskListReview_Type()
        {

            _migrationBuilder.InsertData(
                table: "TaskListReview_Type",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "Annual", true },
                  { "Quarterly", true },
                  { "Special Meeting", true }
                });
        }

        protected void Production_AddTaskListReview_Status()
        {

            _migrationBuilder.InsertData(
                table: "TaskListReview_Status",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "Draft", true },
                  { "Published", true }
                });
        }

        protected void Production_AddTaskReview_Status()
        {

            _migrationBuilder.InsertData(
                table: "TaskReview_Status",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "In Progress", true },
                  { "Completed", true }
                });
        }
        protected void Production_AddTaskReview_Finding()
        {

            _migrationBuilder.InsertData(
                table: "TaskReview_Finding",
                columns: new[] { "Finding", "Active" },
                 values: new object[,]
                {
                  { "No Changes Required", true },
                  { "Changes Required - No Training/Requal Required", true },
                  { "Changes Required - Training/Requalification Required", true },
                  { "Make Task Inactive", true }
                });
        }

        protected void Production_AddActionItem_Priority()
        {

            _migrationBuilder.InsertData(
                table: "ActionItem_Priority",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "Low", true },
                  { "Medium", true },
                  { "High", true }
                });
        }

        protected void Production_AddActionItem_OperationTypes()
        {

            _migrationBuilder.InsertData(
                table: "ActionItem_OperationTypes",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "CreateLink", true },
                  { "RemoveLink", true },
                  { "CreateRecord", true },
                  { "UpdateRecord", true },
                });
        }

        protected void Production_AddActionItem_OperationType_Links()
        {

            _migrationBuilder.InsertData(
                table: "ActionItem_OperationType_Links",
                columns: new[] { "ActionItemOperationName", "OperationTypeId", "Active" },
                 values: new object[,]
                {
                  { "ActionItem_SubDuty_Operation",4, true },
                  { "ActionItem_Step_Operation",2 ,true },
                  { "ActionItem_Step_Operation",3 ,true },
                  { "ActionItem_Step_Operation",4,true },
                  { "ActionItem_QuestionAndAnswer_Operation",2, true },
                  { "ActionItem_QuestionAndAnswer_Operation",3, true },
                  { "ActionItem_QuestionAndAnswer_Operation",4, true },
                  { "ActionItem_Suggestion_Operation",2, true },
                  { "ActionItem_Suggestion_Operation",3, true },
                  { "ActionItem_Suggestion_Operation",4, true },
                  { "ActionItem_Tool_Operation",1, true },
                  { "ActionItem_Tool_Operation",2, true },
                  { "ActionItem_EnablingObjective_Operation",1, true },
                  { "ActionItem_EnablingObjective_Operation",2, true },
                  { "ActionItem_Procedure_Operation",1, true },
                  { "ActionItem_Procedure_Operation",2, true },
                  { "ActionItem_RegulatoryRequirement_Operation",1, true },
                  { "ActionItem_RegulatoryRequirement_Operation",2, true },
                  { "ActionItem_SafetyHazard_Operation",1, true },
                  { "ActionItem_SafetyHazard_Operation",2, true },
                });
        }
        protected void Production_AddTaskListReviewDocumentType()
        {
            _migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Name", "LinkedDataType", "Deleted", "Active" },
                values: new object[,]
                  {
                       {"Task List Review Supporting Document","TaskListReview",false,true},
                  });
        }


        protected void Production_AddSimulatorScenarioDifficulty()
        {

            _migrationBuilder.InsertData(
                table: "SimulatorScenario_Difficultys",
                columns: new[] { "Difficulty", "Active" },
                 values: new object[,]
                {
                  { "High", true },
                  { "Medium", true },
                  { "Low", true},
                });
        }

        protected void Production_AddSimulatorScenario_CollaboratorPermission()
        {

            _migrationBuilder.InsertData(
            table: "SimulatorScenario_CollaboratorPermissions",
            columns: new[] { "Permission", "Active", "Deleted" },
             values: new object[,]
            {
                   { "Editor", true, false },
                   { "Viewer", true, false }
             });
        }

        protected void Production_AddSimulatorScenario_Status()
        {
            _migrationBuilder.InsertData(
                table: "SimulatorScenario_Status",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Draft", true },
                  { "Published", true }

                });
        }

        protected void Production_AddEmailNotification_SimulatorScenarioCollaboration()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_Notifications",
              columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
              values: new object[,]
                  {
                      {"Simulator Scenario Collaboration", false, "Sent to the QTD user when they are added to the Simulator Scenario as a Collaborator.", false, true}
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
                      {23, @"@using RazorEngine.Text
                            <p>
                                Hello @Model.CollaboratorFirstName @Model.CollaboratorLastName you've been invited to participate in Simulator Scenario - @Model.SimulatorScenarioTitle .  <a href='@Model.SimulatorScenarioLink'>Click here to view it</a>.
                            </p>
                            <p>@(new RawString(Model.SimulatorScenarioMessage))</p>",
                          1, false, true }
                  });
        }

        protected void Production_CopySimulatorScenarioOldToNew()
        {
            _migrationBuilder.Sql(@"
                CREATE TABLE #ScenarioMapping (
                    OldScenarioID INT,
                    NewScenarioID INT
                );

                MERGE INTO SimulatorScenarios AS TGT
                USING (SELECT * FROM SimulatorScenarios_Old ) AS SRC ON 1 = 2
                WHEN NOT MATCHED THEN
                INSERT (Title, Description, DurationHours, DurationMinutes, Active, Deleted) 
                VALUES(SRC.SimScenarioTitle, 
                    SRC.SimScenarioDesc, 
                    SRC.SimScenarioDurationHours, 
                    SRC.SimScenarioDurationMins,
                    SRC.Active,
                    SRC.Deleted)
                OUTPUT SRC.id, inserted.id
                INTO #ScenarioMapping(OldScenarioID, NewScenarioID);

                INSERT INTO SimulatorScenario_ILAs (SimulatorScenarioID, ILAID, Active, Deleted)
                SELECT 
                    map.NewScenarioID AS SimulatorScenarioID, 
                    oldILA.ILAID,
                    oldILA.Active,
                    oldILA.Deleted
                FROM 
                    SimulatorScenarioILA_Links_Old oldILA
                JOIN 
                    #ScenarioMapping map ON oldILA.SimulatorScenarioID = map.OldScenarioID;

                UPDATE SimulatorScenarios
                SET Title = 'Simulator Scenario ' + CAST(SimulatorScenarios.Id AS VARCHAR(255))
                FROM SimulatorScenarios
                INNER JOIN #ScenarioMapping ON SimulatorScenarios.Id = #ScenarioMapping.NewScenarioID
                WHERE SimulatorScenarios.Title IS NULL OR SimulatorScenarios.Title = '';

                INSERT INTO SimulatorScenario_Tasks (SimulatorScenarioID, TaskId, Active, Deleted)
                SELECT 
                    map.NewScenarioID AS SimulatorScenarioID, 
                    oldLink.TaskID,
                    oldLink.Active,
                    oldLink.Deleted
                FROM 
                    SimulatorScenarioTaskObjectives_Links_Old oldLink
                JOIN 
                   #ScenarioMapping map ON oldLink.SimulatorScenarioID = map.OldScenarioID;

                INSERT INTO SimulatorScenario_Positions (SimulatorScenarioID, PositionID, Active, Deleted)
                SELECT 
                    map.NewScenarioID AS SimulatorScenarioID, 
                    oldPosition.PositionID,
                    oldPosition.Active,
                    oldPosition.Deleted
                FROM 
                    SimulatorScenarioPositon_Links_Old oldPosition
                JOIN 
                    #ScenarioMapping map ON oldPosition.SimulatorScenarioID = map.OldScenarioID;
               

                 INSERT INTO  SimulatorScenario_EnablingObjectives (SimulatorScenarioID, EnablingObjectiveID, Active, Deleted)
                SELECT 
                    map.NewScenarioID AS SimulatorScenarioID, 
                    oldEo.EnablingObjectiveID,
                    oldEo.Active,
                    oldEo.Deleted
                FROM 
                    SimulatorScenario_EnablingObjectives_Links_Old oldEo
                JOIN 
                    #ScenarioMapping map ON oldEo.SimulatorScenarioID = map.OldScenarioID;
            ");
        }

        protected void Production_AddTrainingIssue_Status()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueStatuses",
                columns: new[] { "Status", "Active" },
                values: new object[,]
                {
                    {"Open",true},
                    {"Closed",true }
                }
              );
        }

        protected void Production_AddTrainingIssue_Severity()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueSeverities",
                columns: new[] { "Severity", "Active" },
                values: new object[,]
                {
                    {"Low",true},
                    {"Medium",true },
                    {"High",true }
                }
              );
        }

        protected void Production_AddTrainingIssue_DriverType()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueDriverTypes",
                columns: new[] { "Type", "Active" },
                values: new object[,]
                {
                    {"Other",true},
                    {"Survey Results",true },
                    {"Team Feedback",true }
                }
              );
        }

        protected void Production_AddTrainingIssue_DriverSubType()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueDriverSubTypes",
                columns: new[] { "SubType", "DriverTypeId", "Active" },
                values: new object[,]
                {
                    {"DIF",2,true},
                    {"GAP",2,true},
                    {"RRT Analysis",2,true},
                    {"Student/Employee",3,true},
                    {"Instructor",3,true},
                    {"Training Dept. Personnel",3,true},
                    {"Manager",3,true},
                    {"Performance Problem",3,true}
                }
              );
        }

        protected void Production_AddTrainingIssue_ActionItemPriority()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueActionItemPriorities",
                columns: new[] { "Priority", "Active" },
                values: new object[,]
                {
                    {"Low",true},
                    {"Medium",true },
                    {"High",true }
                }
              );
        }

        protected void Production_AddTrainingIssue_ActionItemStatus()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueActionItemStatuses",
                columns: new[] { "Status", "Active" },
                values: new object[,]
                {
                    {"Not Started",true},
                    {"In Progress",true },
                    {"Completed",true }
                }
              );
        }

        protected void Production_UpdateDuplicateInstructorEmails()
        {
            _migrationBuilder.Sql(@"
               WITH DuplicateEmails AS (
                    SELECT 
                        InstructorEmail,
                        COUNT(*) AS EmailCount
                    FROM Instructors
                    WHERE InstructorEmail IS NOT NULL AND InstructorEmail <> ''
                    GROUP BY InstructorEmail
                    HAVING COUNT(*) > 1
                )

                UPDATE I
                SET InstructorEmail =  I.InstructorEmail + CAST(I.Id AS NVARCHAR(10)) 
                FROM Instructors I
                INNER JOIN DuplicateEmails D
                ON I.InstructorEmail = D.InstructorEmail
                WHERE I.InstructorEmail IS NOT NULL AND I.InstructorEmail <> ''; 
            ");
        }

        protected void Production_UpdateStudentEvaluation()
        {
            _migrationBuilder.DeleteData(
                table: "StudentEvaluationAudiences",
                keyColumns: new[] { "Name" },
                keyValues: new object[,]
                {
                       {"All Enrolled Employees" },
                       {"First Class Only (Pilot Class)" },
                }
                );

            _migrationBuilder.InsertData(
                 table: "StudentEvaluationAudiences",
                 columns: new[] { "Name", "Active" },
                 values: new object[,]
                 {
                        { "All Employees and Classes",true },
                 });

            _migrationBuilder.Sql(@"
                WITH RankedEvaluations AS (
	                SELECT
		                ILAId,
		                id,
		                ROW_NUMBER() OVER (PARTITION BY ILAId ORDER BY id) AS rn
	                FROM
		                ILA_StudentEvaluation_Links
                )

                update 
	                ILA_StudentEvaluation_Links 
                set 
	                Deleted = 1
                where
	                id IN (
		                SELECT id
		                FROM RankedEvaluations
		                WHERE rn > 1
	                );
                ");
        }
        protected void Production_UpdateEvaluationReleaseEMPSettings()
        {
            _migrationBuilder.Sql(@"UPDATE EvaluationReleaseEMPSettings
                    SET 
                        [EvaluationAvailableOnStartDate] = 0,
                        [EvaluationAvailableOnEndDate] = 1,
                        [FinalGradeRequired] = 1,
                        [ReleaseOnSpecificTimeAfterClassEndDate] = 0,
                        [ReleaseAfterEndTime] = 0,
                        [ReleasePrior] = 0,
                        [ReleaseAfterGradeAssigned] = 0
                    WHERE 
                        ([EvaluationAvailableOnStartDate] = 1 AND [EvaluationAvailableOnEndDate] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [FinalGradeRequired] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [ReleaseOnSpecificTimeAfterClassEndDate] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [ReleaseAfterEndTime] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [ReleasePrior] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [FinalGradeRequired] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [ReleaseOnSpecificTimeAfterClassEndDate] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [ReleaseAfterEndTime] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [ReleasePrior] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([FinalGradeRequired] = 1 AND [ReleaseOnSpecificTimeAfterClassEndDate] = 1) OR
                        ([FinalGradeRequired] = 1 AND [ReleaseAfterEndTime] = 1) OR
                        ([FinalGradeRequired] = 1 AND [ReleasePrior] = 1) OR
                        ([FinalGradeRequired] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([ReleaseOnSpecificTimeAfterClassEndDate] = 1 AND [ReleaseAfterEndTime] = 1) OR
                        ([ReleaseOnSpecificTimeAfterClassEndDate] = 1 AND [ReleasePrior] = 1) OR
                        ([ReleaseOnSpecificTimeAfterClassEndDate] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([ReleaseAfterEndTime] = 1 AND [ReleasePrior] = 1) OR
                        ([ReleaseAfterEndTime] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([ReleasePrior] = 1 AND [ReleaseAfterGradeAssigned] = 1);
                                 ");
        }
        protected void Production_UpdateRecord_IlaStudentEvalLinks()
        {
            _migrationBuilder.Sql(@"UPDATE ILA_StudentEvaluation_Links
                                SET studentEvalAudienceID = (SELECT Id FROM StudentEvaluationAudiences)");
        }

        protected void Production_UpdateVersionNumberinExistingVersionILAs()
        {
            _migrationBuilder.Sql(@"
				                    UPDATE main
                    SET main.[VersionNumber] = v.rn
                    FROM Version_ILAs main
                    INNER JOIN (
                        SELECT Id, ILAId, ROW_NUMBER() OVER (PARTITION BY ILAId ORDER BY Id) AS rn
                        FROM Version_ILAs
                    ) AS v
                    ON main.Id = v.Id;
            ");
        }


        protected void Production_AddData_EmployeeHistoryTableandReport()
        {
            _migrationBuilder.Sql(@"UPDATE eh
                                   SET eh.OperationType = 2, 
                                       eh.CurrentActiveStatus = CASE
                                           WHEN e.Active = 1 THEN 1
                                           ELSE 0
                                       END
                                   FROM EmployeeHistories eh
                                   JOIN Employees e ON eh.EmployeeId = e.Id
                                   WHERE e.Deleted = 1;
                                   
                                   UPDATE eh
                                   SET eh.OperationType = CASE
                                           WHEN e.Active = 1 THEN 0 
                                           ELSE 1 
                                       END,
                                       eh.CurrentActiveStatus = CASE
                                           WHEN e.Active = 1 THEN 1
                                           ELSE 0
                                       END
                                   FROM EmployeeHistories eh
                                   JOIN Employees e ON eh.EmployeeId = e.Id
                                   WHERE e.Deleted = 0; ");

        }

        protected void Production_UpdateVersionILAsToPublishedStateBasedOnEffectiveDate()
        {
            _migrationBuilder.Sql(@"
                    UPDATE [dbo].[Version_ILAs]
                    SET [State] = 4
                    WHERE CAST([EffectiveDate] AS TIME) = '00:00:00'
                ");
        }

        protected void Production_AddTableData_ClassScheduleEmpSettings()
        {
            _migrationBuilder.Sql(@"INSERT INTO ClassSchedule_TestReleaseEMPSettings
               (
                   ClassScheduleId,
                   UsePreTestAndTest,
                   PreTestRequired,
                   FinalTestId,
                   PreTestId,
                   jobDetails,
                   PreTestAvailableOnEnrollment,
                   PreTestAvailableOneStartDate,
                   PreTestScore,
                   ShowStudentSubmittedPreTestAnswers,
                   ShowCorrectIncorrectPreTestAnswers,
                   MakeAvailableBeforeDays,
                   MakeAvailableBeforeWeeks,
                   DaysOrWeeks,
                   FinalTestPassingScore,
                   MakeFinalTestAvailableImmediatelyAfterStartDate,
                   MakeFinalTestAvailableOnClassEndDate,
                   MakeFinalTestAvailableAfterCBTCompleted,
                   MakeFinalTestAvailableOnSpecificTime,
                   FinalTestSpecificTimePrior,
                   FinalTestDueDate,
                   ShowStudentSubmittedFinalTestAnswers,
                   ShowStudentSubmittedRetakeTestAnswers,
                   ShowCorrectIncorrectFinalTestAnswers,
                   ShowCorrectIncorrectRetakeTestAnswers,
                   AutoReleaseRetake,
                   RetakeEnabled,
                   NumberOfRetakes,
                   EmpSettingsReleaseTypeId,
                   Deleted,
                   Active
               )
               SELECT 
                   cs.Id,
                   emp.UsePreTestAndTest,
                   emp.PreTestRequired,
                   emp.FinalTestId,
                   emp.PreTestId,
                   emp.jobDetails,
                   emp.PreTestAvailableOnEnrollment,
                   emp.PreTestAvailableOneStartDate,
                   emp.PreTestScore,
                   emp.ShowStudentSubmittedPreTestAnswers,
                   emp.ShowCorrectIncorrectPreTestAnswers,
                   emp.MakeAvailableBeforeDays,
                   emp.MakeAvailableBeforeWeeks,
                   emp.DaysOrWeeks,
                   emp.FinalTestPassingScore,
                   emp.MakeFinalTestAvailableImmediatelyAfterStartDate,
                   emp.MakeFinalTestAvailableOnClassEndDate,
                   emp.MakeFinalTestAvailableAfterCBTCompleted,
                   emp.MakeFinalTestAvailableOnSpecificTime,
                   emp.FinalTestSpecificTimePrior,
                   emp.FinalTestDueDate,
                   emp.ShowStudentSubmittedFinalTestAnswers,
                   emp.ShowStudentSubmittedRetakeTestAnswers,
                   emp.ShowCorrectIncorrectFinalTestAnswers,
                   emp.ShowCorrectIncorrectRetakeTestAnswers,
                   emp.AutoReleaseRetake,
                   emp.RetakeEnabled,
                   emp.NumberOfRetakes,
                   emp.EmpSettingsReleaseTypeId,
                   emp.Deleted,
                   emp.Active
               FROM TestReleaseEMPSettings emp
               JOIN ClassSchedules cs ON emp.ILAId = cs.ILAId
               WHERE NOT EXISTS (
                   SELECT 1
                   FROM ClassSchedule_TestReleaseEMPSettings cs_emp
                   WHERE cs_emp.ClassScheduleId = cs.Id
               );");

            _migrationBuilder.Sql(@"INSERT INTO ClassSchedule_TestReleaseEMPSetting_Retake_Links (ClassSchedule_TestReleaseSettingId, RetakeTestId,Deleted,Active)
                    SELECT cs_emp.Id, trl.RetakeTestId,trl.Deleted,trl.Active
                    FROM ClassSchedule_TestReleaseEMPSettings cs_emp
                    INNER JOIN ClassSchedules cs ON cs_emp.ClassScheduleId = cs.Id 
                    INNER JOIN TestReleaseEMPSettings tr ON tr.ILAId = cs.ILAId 
                    INNER JOIN TestReleaseEMPSetting_Retake_Links trl ON trl.TestReleaseSettingId = tr.Id
                    WHERE NOT EXISTS (
                        SELECT 1
                        FROM ClassSchedule_TestReleaseEMPSetting_Retake_Links existing_links
                        WHERE existing_links.ClassSchedule_TestReleaseSettingId = cs_emp.Id
                        AND existing_links.RetakeTestId = trl.RetakeTestId
                    );");
        }
        protected void Production_AddTableData_ClassScheduleTQEMPSettings()
        {
            _migrationBuilder.Sql(@"WITH FilteredTQILAEmpSettings AS (
                                    SELECT t1.ILAId, t1.TQRequired, t1.Deleted, t1.Active
                                    FROM TQILAEmpSettings t1
                                    WHERE t1.Id = (
                                        SELECT TOP 1 t2.Id
                                        FROM TQILAEmpSettings t2
                                        WHERE t2.ILAId = t1.ILAId AND t2.Active = 1
                                        ORDER BY t2.Id DESC
                                    )
                                )
                                INSERT INTO ClassSchedule_TQEMPSettings (ClassScheduleId, TQRequired, Deleted, Active)
                                SELECT DISTINCT 
                                    cs.Id,         
                                    t.TQRequired, 
                                    t.Deleted,     
                                    t.Active       
                                FROM ClassSchedules cs
                                INNER JOIN FilteredTQILAEmpSettings t ON t.ILAId = cs.ILAId
                                WHERE NOT EXISTS (
                                    SELECT 1
                                    FROM ClassSchedule_TQEMPSettings cs_tq
                                    WHERE cs_tq.ClassScheduleId = cs.Id
                                );
                            ");

            _migrationBuilder.Sql(@"INSERT INTO ClassSchedule_Evaluator_Links (ClassScheduleId, EvaluatorId, Deleted, Active)
                        SELECT 
                            cs.Id, 
                            e.EvaluatorId,
                            e.Deleted,
                            e.Active
                        FROM ClassSchedules cs
                        INNER JOIN ILA_Evaluator_Links e ON e.ILAId = cs.ILAId
                        WHERE NOT EXISTS (
                            SELECT 1
                            FROM ClassSchedule_Evaluator_Links existing_links
                            WHERE existing_links.ClassScheduleId = cs.Id
                            AND existing_links.EvaluatorId = e.EvaluatorId
                        );");
        }

        protected void Production_ApplyDatabaseUpdateScripts()
        {
            _migrationBuilder.Sql(@"
            UPDATE TrainingProgramReviews
            SET ReviewDate = DATEADD(hour, 6, ReviewDate)
            WHERE DATEPART(hour, ReviewDate) = 0
              AND DATEPART(minute, ReviewDate) = 0
              AND DATEPART(second, ReviewDate) = 0;
        ");

            _migrationBuilder.Sql(@"
            UPDATE TrainingProgramReviews
            SET StartDate = DATEADD(hour, 6, StartDate)
            WHERE DATEPART(hour, StartDate) = 0
              AND DATEPART(minute, StartDate) = 0
              AND DATEPART(second, StartDate) = 0;
        ");

            _migrationBuilder.Sql(@"
            UPDATE TrainingProgramReviews
            SET EndDate = DATEADD(hour, 6, EndDate)
            WHERE DATEPART(hour, EndDate) = 0
              AND DATEPART(minute, EndDate) = 0
              AND DATEPART(second, EndDate) = 0;
        ");

            _migrationBuilder.Sql(@"
                UPDATE TaskQualifications
                SET TaskQualificationDate = DATEADD(hour, 6, TaskQualificationDate)
                WHERE DATEPART(hour, TaskQualificationDate) = 0
                AND DATEPART(minute, TaskQualificationDate) = 0
                AND DATEPART(second, TaskQualificationDate) = 0;
            ");

            _migrationBuilder.Sql(@"
                    ;WITH CTE_Data  AS (
                    SELECT
                        b.Id AS [ClassScheduleId],
                        a.studentEvalFormID AS [StudentEvaluationId]
                    FROM
                        dbo.ILA_StudentEvaluation_Links a
                    INNER JOIN dbo.ClassSchedules b
                        ON a.ILAId = b.ILAID AND b.Deleted = 0 AND b.Active = 1
                    LEFT JOIN dbo.ClassSchedule_StudentEvaluations_Links c
                        ON a.studentEvalFormID = c.StudentEvaluationId AND b.Id = c.ClassScheduleId AND c.Deleted = 0 AND c.Active = 1
                    WHERE
                        a.Deleted = 0
                        AND a.Active = 1
                        AND c.Id IS NULL
		                )

                        insert into ClassSchedule_StudentEvaluations_Links (ClassScheduleId, StudentEvaluationId, Active, Deleted)
                        select 
                        ClassScheduleId
                        ,StudentEvaluationId
                        ,1
                        ,0 
                        from CTE_Data
                        ");

            _migrationBuilder.Sql(@"update TQEmpSettings set MultipleSignOff = 1 where MultipleSignOff is null and ReleaseToAllSingleSignOff = 0");
        }

        protected void Production_UpdateDocumentTypeForCourseCompletionInfo()
        {
            _migrationBuilder.UpdateData(
               table: "DocumentTypes",
               keyColumns: new[] { "Name", "LinkedDataType" },
               keyValues: new object[] { "Other Employee Course Completion Info", "Employees" },
               columns: new[] { "LinkedDataType" },
               values: new object[] { "ClassScheduleEmployees" });
        }

        protected void Production_UpdateLaunchLinkInCbtScormRegistration()
        {
            _migrationBuilder.Sql(@"
                   UPDATE CBT_ScormRegistration
                    SET LaunchLink = CONCAT(
                                        LEFT(LaunchLink, LEN(LaunchLink) - CHARINDEX('=', REVERSE(LaunchLink))),
                                        '=', 
                                        CAST([CBTScormUploadId] AS NVARCHAR(MAX)),
                                        '.', 
                                        CAST([ClassScheduleEmployeeId] AS NVARCHAR(MAX))
                                    )
                    WHERE ISNULL(LaunchLink, '') <> ''
                    ");
        }

        protected void Production_AddScormRegistrationsForActiveClassScheduleEmployees()
        {
            _migrationBuilder.Sql(@"
                with CTE_DATA as (
                        select  
                          a.Id as [CBT_ScormUploadId]
                        , a.ConnectedDate
                        , d.id as [ClassScheduleEmployeesId]
                        from
                        dbo.CBT_ScormUpload a
                        inner join dbo.CBTs b
                             on a.CbtId = b.Id and b.Active = 1 and b.Deleted = 0
                        inner join dbo.ClassSchedules c
                            on b.ILAId = c.ILAID and c.Deleted = 0 and c.Active = 1
                        inner join dbo.ClassScheduleEmployees d
                            on c.Id = d.ClassScheduleId and d.Active = 1 and d.Deleted = 0 and d.CBTStatusId <> 3
                        left join dbo.CBT_ScormRegistration e
                            on d.Id = e.ClassScheduleEmployeeId
                        where  a.Active = 1
                        and a.Deleted = 0
                        and a.ScormStatus = 'Uploaded'
                        and e.Id is null
                    )
                    ,CTE_PartitionedByCSE as (
                        select
                        *
                        , ROW_NUMBER() OVER(PARTITION BY a.ClassScheduleEmployeesId ORDER BY a.ConnectedDate DESC) AS RowNum
                        from CTE_DATA a
                    )

                    insert into
                        dbo.CBT_ScormRegistration
                        (CBTScormUploadId, ClassScheduleEmployeeId, RegistrationCompletion, RegistrationSuccess, Deleted, Active, PassingScoreSource)
                    select
                        a.CBT_ScormUploadId, a.ClassScheduleEmployeesId, 0, 0, 0, 1, 1
                    from
                        CTE_PartitionedByCSE a
                    where
                        RowNum = 1
                 ");
        }

        protected void Production_UpdateInactiveScormRegistrations()
        {

            _migrationBuilder.Sql(@"
                    with CTE_DATA as (
	                                select 
	                                a.Id
	                                ,d.EndDateTime
	                                ,e.DueDateAmount
	                                ,f.Type
	                                ,case 
		                                when f.Type = 'Days' then DATEADD(DAY, e.DueDateAmount, d.EndDateTime)
		                                when f.Type = 'Weeks' then DATEADD(WEEK, e.DueDateAmount, d.EndDateTime)
		                                when f.Type = 'Months' then DATEADD(MONTH, e.DueDateAmount, d.EndDateTime)
		                                else '9999-12-31'
	                                end as [AdjustedDate]
	                                from 
	                                dbo.CBT_ScormRegistration a
	                                inner join dbo.CBT_ScormUpload b
		                                on a.CBTScormUploadId = b.Id and b.ScormStatus = 'Disconnected' and b.Active = 1 and b.Deleted = 0
	                                inner join dbo.ClassScheduleEmployees c
		                                on a.ClassScheduleEmployeeId = c.Id and c.Active = 1 and c.CBTStatusId <> 3 and c.Deleted = 0
	                                inner join dbo.ClassSchedules d
		                                on c.ClassScheduleId = d.Id and d.Deleted = 0 and d.Active = 1
	                                inner join dbo.CBTs e
		                                on b.CbtId = e.id and e.Deleted = 0 and e.Active = 1
	                                inner join dbo.EmpSettingsReleaseTypes f
		                                on e.EmpSettingsReleaseTypeId = f.Id and f.Deleted = 0 and f.Active = 1
                                where
                                a.Active = 1
                                and a.Deleted = 0
                                )

                                update dbo.CBT_ScormRegistration set Active = 0 where id in (select Id from CTE_DATA where AdjustedDate > GETDATE())
                                ");
        }

        protected void Production_UpdateApiRegistrationIdsForScormRegistrationsAndScormApiRegToLearner()
        {
            _migrationBuilder.Sql(@"
                ALTER TABLE dbo.ScormRegistration NOCHECK CONSTRAINT FK_ScormRegistration_reglearn;
                WITH ValidScormApiReg AS (
                    SELECT sar.api_registration_id
                    FROM dbo.ScormApiRegToLearner sar
                    WHERE TRY_CONVERT(INT, sar.api_registration_id) IS NOT NULL
                )
                UPDATE dbo.ScormApiRegToLearner
                SET api_registration_id = CONCAT(CAST(csr.CBTScormUploadId AS VARCHAR), '.', CAST(csr.ClassScheduleEmployeeId AS VARCHAR))
                FROM dbo.ScormApiRegToLearner sar
                JOIN ValidScormApiReg v
                    ON sar.api_registration_id  = v.api_registration_id
                  Join CBT_ScormRegistration csr
                On  v.api_registration_id  =csr.ClassScheduleEmployeeId
               ALTER TABLE dbo.ScormRegistration CHECK CONSTRAINT FK_ScormRegistration_reglearn;
                    ");
            _migrationBuilder.Sql(@"
               WITH ValidScormRegistration AS(
                    SELECT sr.api_registration_id
                    FROM dbo.ScormRegistration sr
                    WHERE TRY_CONVERT(INT, sr.api_registration_id) IS NOT NULL
                )
                UPDATE dbo.ScormRegistration
                SET api_registration_id = CONCAT(CAST(csr.CBTScormUploadId AS VARCHAR), '.', CAST(csr.ClassScheduleEmployeeId AS VARCHAR))
                FROM dbo.ScormRegistration sr
                JOIN ValidScormRegistration v
                    ON sr.api_registration_id = v.api_registration_id
                    Join CBT_ScormRegistration csr
                On  v.api_registration_id = csr.ClassScheduleEmployeeId
                ");
        }

        protected void Production_UpdateTaskQualificationsLinkedToDeletedTasks()
        {
            _migrationBuilder.Sql(@"
                     DECLARE @TempTQIds TABLE (
                         Id INT
                     );

                     INSERT INTO @TempTQIds
                     SELECT 
                         a.Id
                     FROM
                         dbo.TaskQualifications a
                     INNER JOIN 
                         dbo.Tasks b
                         ON a.TaskId = b.Id
                         AND b.Deleted = 1
                     WHERE 
                         a.Deleted = 0;

                     select * from @TempTQIds

                     UPDATE a
                     SET a.Deleted = 1
                     FROM dbo.TaskQualifications a
                     INNER JOIN @TempTQIds t
                         ON a.Id = t.Id; 
              ");
        }

        protected void Production_UpdateTaskActiveStatusFromHistory()
        {
            _migrationBuilder.Sql(@"
                  with CTE_Ordered_VersionTasks as (
	                        select 
	                        a.id
	                        ,a.TaskId
	                        ,cast(VersionNumber as decimal(10,2)) as [CastVersionNumber]
	                        ,VersionNumber
	                        ,ROW_NUMBER() OVER (PARTITION BY a.TaskId ORDER BY cast(a.VersionNumber as decimal(10, 2))) as [RowNumber] 
	                        from 
	                        dbo.Version_Tasks a 
                        ), CTE_Ordered_VersionTasks_With_ActivationInactivation as (
	                        select
	                        a.Id
	                        ,a.TaskId
	                        ,a.RowNumber
	                        ,InactivationActivationOccurred = 
		                        case 
			                        when exists (select 1 from dbo.Task_Histories b where a.id = Version_TaskId and b.ChangeNotes like 'Inactive Task True%') then 0
			                        when exists (select 1 from dbo.Task_Histories b where a.id = Version_TaskId and b.ChangeNotes like 'Inactive Task False%') then 1
			                        else null
		                        end
	                        from
	                        CTE_Ordered_VersionTasks a
                        ), CTE_MostRecent_VersionTask_ActivationInactivation as (
	                        select 
	                        a.TaskId
	                        ,a.Id
	                        ,a.RowNumber
	                        ,max(b.RowNumber) as [MostRecentActivationInactivation_RowNumber]
	                        from CTE_Ordered_VersionTasks a
	                        left join CTE_Ordered_VersionTasks_With_ActivationInactivation b
		                        on a.TaskId = b.TaskId and b.RowNumber <= a.RowNumber and b.InactivationActivationOccurred is not null
	                        group by
		                        a.TaskId
		                        ,a.Id
		                        ,a.RowNumber
                        )

                        update a
                        set 
                        TaskActive = 
                        case 
	                        when c.id is null then 1
	                        else IsNull(c.InactivationActivationOccurred, 0)
                        end 
                        from 
                        dbo.Version_Tasks a
                        inner join CTE_MostRecent_VersionTask_ActivationInactivation b
	                        on a.Id = b.Id
                        left join CTE_Ordered_VersionTasks_With_ActivationInactivation c
	                        on b.TaskId = c.TaskId and c.RowNumber = b.MostRecentActivationInactivation_RowNumber
                    ");
        }

        protected void Production_UpdateDocumentTypesTable_Tool()
        {
            _migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumns: new[] { "Id", "Name" },
                keyValues: new object[] { 10, "Tool" },
                column: "LinkedDataType",
                value: "Tool"
                );
        }

        protected void Production_UpdateILACertificationLinksForDeletedCertifications()
        {
            _migrationBuilder.Sql(@" update a set deleted = 1 from dbo.ILACertificationLinks a inner join dbo.Certifications b on a.CertificationId = b.id and b.Deleted = 1 where a.Deleted = 0 ");
        }

        protected void Production_UpdateDeletedTaskQualificationEvaluatorLinksForDeletedEmployees()
        {
            _migrationBuilder.Sql(@"update a set Deleted = 1 from dbo.TaskQualification_Evaluator_Links a inner join dbo.Employees b on a.EvaluatorId = b.Id and b.Deleted = 1 where a.Deleted = 0 ");
        }

        protected void Production_MarkDeletedPositionTasksBasedOnTasks()
        {
            _migrationBuilder.Sql(@"update a set Deleted = 1 from dbo.Position_Tasks a inner join dbo.Tasks b on a.TaskId = b.id and b.Deleted = 1 where a.Deleted = 0");
        }
        protected void Production_UpdateSegmentObjectiveLinksData()
        {
            _migrationBuilder.Sql(@"UPDATE SegmentObjective_Links
                                    SET Deleted = 1
                                    WHERE 
                                        TaskId IS NOT NULL 
                                        AND (
                                            NOT EXISTS (
                                                SELECT 1
                                                FROM ILA_TaskObjective_Links ila
                                                INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                                WHERE ila.TaskId = SegmentObjective_Links.TaskId
                                                  AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                  AND ila.Deleted = 0 
                                            )
                                            OR 
                                            EXISTS (
                                                SELECT 1
                                                FROM ILA_TaskObjective_Links ila
                                                INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                                WHERE ila.TaskId = SegmentObjective_Links.TaskId
                                                  AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                  AND ila.Deleted = 1 
                                            )
                                        );
            ");

            _migrationBuilder.Sql(@"UPDATE SegmentObjective_Links
                                  SET Deleted = 1
                                  WHERE 
                                      EnablingObjectiveId IS NOT NULL 
                                      AND (
                                          NOT EXISTS (
                                              SELECT 1
                                              FROM ILA_EnablingObjective_Links ila
                                              INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                              WHERE ila.EnablingObjectiveId = SegmentObjective_Links.EnablingObjectiveId
                                                AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                AND ila.Deleted = 0 
                                          )
                                          OR 
                                          EXISTS (
                                              SELECT 1
                                              FROM ILA_EnablingObjective_Links ila
                                              INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                              WHERE ila.EnablingObjectiveId = SegmentObjective_Links.EnablingObjectiveId
                                                AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                AND ila.Deleted = 1 
                                          )
                                      );"
                                  );

            _migrationBuilder.Sql(@"UPDATE SegmentObjective_Links
                                    SET Deleted = 1
                                    WHERE 
                                        CustomEOId IS NOT NULL 
                                        AND (
                                            NOT EXISTS (
                                                SELECT 1
                                                FROM ILACustomObjective_Links ila
                                                INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                                WHERE ila.CustomObjId = SegmentObjective_Links.CustomEOId
                                                  AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                  AND ila.Deleted = 0 
                                            )
                                            OR 
                                            EXISTS (
                                                SELECT 1
                                                FROM ILACustomObjective_Links ila
                                                INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                                WHERE ila.CustomObjId = SegmentObjective_Links.CustomEOId
                                                  AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                  AND ila.Deleted = 1 
                                            )
                                        );"
                                );
        }

        protected void Production_AddTrainingTopicsTableData()
        {
            _migrationBuilder.InsertData(
                    table: "TrainingTopics",
                    columns: new[] { "TrainingTopic_CategoryId", "Name", "Active" },
                    values: new object[,]
                    {
                        {3,"Emergency technologies/equipment" , true }
                    }
            );

            _migrationBuilder.Sql(@"WITH OrderedLinks AS (
                        SELECT 
                            SOL.Id AS SegmentObjectiveLinkId,
                            ROW_NUMBER() OVER (
                                PARTITION BY ISeg.ILAId 
                                ORDER BY S.Id, SOL.Id
                            ) AS RowOrder
                        FROM SegmentObjective_Links SOL
                        INNER JOIN Segments S ON S.Id = SOL.SegmentId
                        INNER JOIN ILA_Segment_Links ISeg ON ISeg.SegmentId = S.Id
                        WHERE ISeg.ILAId IS NOT NULL
                    )
                    UPDATE SOL
                    SET SOL.[Order] = OL.RowOrder
                    FROM SegmentObjective_Links SOL
                    INNER JOIN OrderedLinks OL ON SOL.Id = OL.SegmentObjectiveLinkId;"
            );
        }

        protected void Production_AddData_AdminEMPCompletionNotificationAndSettings()
        {
            _migrationBuilder.Sql(@"UPDATE AdminEMPCompletionNotificationItem
                                    SET CompletionEntityName = CASE 
                                    
                                        WHEN CompletionType = 'test' THEN (
                                            SELECT t.TestTitle 
                                            FROM ClassSchedule_Roster csr
                                            INNER JOIN Tests t ON csr.TestId = t.Id
                                            WHERE csr.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                        
                                        WHEN CompletionType = 'online courses' THEN (
                                            SELECT ila.[Name] 
                                            FROM ClassScheduleEmployees cse
                                            INNER JOIN ClassSchedules cs ON cse.ClassScheduleId = cs.Id
                                            INNER JOIN ILAs ila ON cs.ILAId = ila.Id
                                            WHERE cse.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                        
                                        WHEN CompletionType = 'Stude' THEN (
                                            SELECT se.Title
                                            FROM ClassSchedule_Evaluation_Roster er
                                            INNER JOIN StudentEvaluations se ON er.StudentEvaluationId = se.Id
                                            WHERE er.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                        
                                        WHEN CompletionType = 'Procedure Review' THEN (
                                            SELECT pr.ProcedureReviewTitle
                                            FROM ProcedureReview_Employees pre
                                            INNER JOIN ProcedureReviews pr ON pre.ProcedureReviewId = pr.Id
                                            WHERE pre.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                        
                                        WHEN CompletionType = 'Task & Skill Qualifications' THEN (
                                        SELECT 
                                            da.Letter + CAST(da.Number AS VARCHAR) + '.' + CAST(sd.SubNumber AS VARCHAR) + '.' + CAST(t.Number AS VARCHAR)
                                        FROM 
                                            TaskQualifications tq
                                        INNER JOIN 
                                            Tasks t ON tq.TaskId = t.Id 
                                        INNER JOIN 
                                            SubdutyAreas sd ON sd.Id = t.SubdutyAreaId 
                                        INNER JOIN 
                                            DutyAreas da ON da.Id = sd.DutyAreaId
                                        WHERE 
                                            tq.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                    )
                                        
                                        WHEN CompletionType = 'DIF Surveys' THEN (
                                            SELECT ds.Title
                                            FROM DIFSurvey_Employee dse
                                            INNER JOIN DifSurvey ds ON dse.DifSurveyId = ds.Id
                                            WHERE dse.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                    END
                                    WHERE CompletionType IN ('test', 'online courses', 'Stude', 'Procedure Review', 'Task & Skill Qualifications', 'DIF Surveys');"
            );

            _migrationBuilder.InsertData(
                 table: "ClientSettings_Notification_Step_AvailableCustomSettings",
                 columns: new[] { "ClientSettingsNotificationStepId", "Setting", "Active" },
                 values: new object[,]
                 {
                     { 24, "Time of Day", true } ,
                     { 24, "Day of Week", true } ,
                     { 24, "Day # of Month", true }
                 }
                 );

            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: new object[,]
                {
                    { 24, "Time of Day", "00:00", true },
                    { 24, "Day of Week", "Sunday", true },
                    { 24, "Day # of Month", "01", true },
                }
              );

            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notification_Steps",
            keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
            keyValues: new object[] { 22, 1 },
            column: "Template",
            value: @"@using QTD2.Infrastructure.ExtensionMethods;
                    <style>
                        .emp-completion-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .emp-completion-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .emp-completion-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                    <p>Hello,</p>
                    <p>Below please find the Employee Portal Training Completion Report. This report lists the training that has been completed and submitted by Employees via the Employee Portal.</p>
                    <p>Refer to applicable QTD reports for detailed training completion information. </p>
                    <p>To change the frequency of this report, navigate to <b>Templates and Forms > Email Notifications > Employee Portal Completions</b></p>
                    <p>If you have questions, please reach out to your training administrator.</p>
                    @if (Model.Items.Count > 0){
                        <table class='emp-completion-table'>
                            <thead>
                                <tr>
                                    <th style='width: 15%;'>Name</th>
                                    <th style='width: 15%;'>Completion Type</th>
                                    <th style='width: 30%;'>Completion Name </th>
                                    <th style='width: 40%;'>Completion Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                @foreach (var item in Model.Items)
                                {
                                    <tr>
                                        <td>@item.Title</td>
                                        <td>@item.CompletionType </td>
                                        <td>@item.CompletionEntityName</td>
                                        <td>@item.CompletionDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                    </tr>
                                }
                            </tbody>
                        </table>
                    } else {
                        <i>No EMP completion records for this time period</i>
                    }");
        }

        protected void Production_NotificationTemplateAdjustments()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                      set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p> You have been assigned to complete a Task Qualification as part of your position specific training program. An evaluator has been assigned to sign - off on this task qualification.This will be completed using the Employee Portal(EMP). Please review the table below for further details. To help you prepare for the task qualification, the task(s) below are available in a read - only format in EMP.</p><figure class=""table""><table><tbody><tr><td>Task #</td><td>@Model.TaskNumber</td></tr><tr><td>Task Statement</td><td>@Model.TaskStatement</td></tr><tr><td>Evaluator Name</td><td>@Model.EvaluatorName</td></tr></tbody></table></figure><p>If for any reason you cannot complete the assigned Task Qualification, notify your Task Qualification Evaluator and/or Training Administrator as soon as possible. Thank you!</p>'

                                      where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Task Qualification - Trainee')"
           );
        }

        protected void Production_Update_NotificationTemplate()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p>Attached find a schedule of courses you are currently registered for. Please review this schedule and let us know if you have any questions.</p><p> Thank you!!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Class Schedule')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p>This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days.Your expiration date is @Model.CertificateExpirationDate. To date, we have not received a copy of your updated @Model.CertificateName certificate.</p><p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records.If you have any questions, please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 2"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your attention is required. This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate. To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p> <p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 3"
             );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your immediate attention is required. This is an urgent reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate. To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>After multiple notifications regarding the renewal of your @Model.CertificateName credential, your management has also received a copy of this notification. You and your management will continue to receive daily reminders until the System Operations Training Team receives an updated copy of your @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 4"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Welcome to the Employee Portal (EMP)! EMP is your gateway to complete critical training activities such as computer training classes, tests, procedure reviews, and more.</p><p>Your Training Administrator has created an account for you in EMP, which you can access via the link and username below:</p><p>Link: @Model.EMPWebsite<br>Username: @Model.EmployeeUserName</p><p>For security reasons, this email does not contain your account password. Please reset your password by clicking on the “Forgot your Password?” link on the EMP login page.</p><p><strong>Important!</strong> The following internet browsers are supported: Internet Explorer, Google Chrome, Microsoft Edge. We encourage you to log in as soon as possible to ensure everything is working correctly. If you have any questions, please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Login')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>In order to receive credit for completion of @Model.CourseTitle, you must also complete the online test. You can access the test through the Employee Portal (EMP). Please review the table below for further details.</p> <figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate/@Model.EndDate</td></tr><tr><td>Test ID/Name</td><td>@Model.TestId/@Model.TestTitle</td></tr><tr><td>Test Available Date</td><td>@Model.ClassEndDate</td></tr><tr><td>Test Due Date</td><td>@Model.TestDueDate</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p> <p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Test')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been enrolled in a course which requires you to complete a pretest prior to the course start date. You can access the pretest through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate/@Model.EndDate</td></tr><tr><td>Pretest ID/Name</td><td>@Model.PretestId/@Model.PretestTitle</td></tr><tr><td>Pretest Available Date</td><td>@Model.PretestAvailableDate</td></tr><tr><td>Pretest Due Date</td><td>@Model.ClassStartDate</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Pretest')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned an online training course to complete. You can access the course through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.ILATitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate/@Model.EndDate</td></tr><tr><td>Course Available Date</td><td>@Model.CourseStartDate</td></tr><tr><td>Course Due Date</td><td>@Model.CourseEndDate</td></tr></table></figure><p>If you are unable to complete the course by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Online Course')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete an evaluation for a course you recently completed. You can access the evaluation through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.ILATitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate/@Model.EndDate</td></tr><tr><td>Eval Available Date</td><td>@Model.CourseEndDate</td></tr><tr><td>Eval Due Date</td><td>@Model.EvalDueDate</td></tr></table></figure><p>If you are unable to complete the evaluation by the due date listed, notify your Training Administrator.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Student Evaluation')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete a procedure review. You can complete the procedure review through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Procedure Title</td><td>@Model.ProcedureTitle</td></tr><tr><td>Review Available Date</td><td>@Model.ReviewStartdate</td></tr><tr><td>Review Due Date</td><td>@Model.ReviewEndDate</td></tr></table></figure><p>If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Procedure Review')"
            );


            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete an IDP review. You can complete the IDP review through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>IDP Title</td><td>@Model.IDPTitle</td></tr><tr><td>Review Available Date</td><td>@Model.ReviewStartdate</td></tr><tr><td>Review Due Date</td><td>@Model.ReviewEndDate</td></tr></table></figure><p>If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP IDP Review')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned as an Evaluator to sign off on a Task Qualification. This will be completed using the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Task #</td><td>@Model.TaskNumber</td></tr><tr><td>Task Statement</td><td>@Model.TaskStatement</td></tr><tr><td>Trainee Name</td><td>@Model.TraineeName</td></tr></table></figure><p>If for any reason you cannot complete the assigned Task Qualification(s), notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Task Qualification - Evaluator')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Your self-registration request to enroll in @Model.CourseTitle on @Model.StartDate and @Model.EndDate has been approved.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Self-Registration Approval')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Your self-registration request to enroll in @Model.CourseTitle on @Model.StartDate and @Model.EndDate has been denied.</p><p>Please contact your Training Administrator for additional information.</p><p>Thank you</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Self-Registration Denial')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete a DIF Survey. You can access the DIF Survey through the Employee Portal (EMP). Please review the table below for further details.</p><p>This survey is being conducted to help us improve both the initial and continuing training for @Model.PositionTitle position, and your input is an important part of this process. You will be asked to review and rate the tasks you perform in terms of difficulty, importance, and frequency. Further instructions are provided once you start the survey.</p><figure class=""table""> <table><tr><td>Position Title</td><td>@Model.PositionTitle</td></tr><tr><td>Survey Title</td><td>@Model.SurveyTitle</td></tr><tr><td>Survey Available Date</td><td>@Model.SurveyStartDate</td></tr><tr><td>Survey Due Date</td><td>@Model.SurveyEndDate</td></tr></table></figure><p>If you are unable to complete the DIF Survey by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP DIF Survey')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete a GAP Survey. You can access the GAP Survey through the Employee Portal (EMP). Please review the table below for further details.</p><p>This survey is being conducted to help us improve and provide on-target training for @Model.PositionTitle position, and your input is an important part of this process.</p><figure class=""table""><table><tr><td>Position Title</td><td>@Model.PositionTitle</td></tr><tr><td>Survey Title</td><td>@Model.SurveyTitle</td></tr><tr><td>Survey Available Date</td><td>@Model.SurveyStartDate</td></tr><tr><td>Survey Due Date</td><td>@Model.SurveyEndDate</td></tr></table></figure><p>If you are unable to complete the GAP Survey by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP GAP Survey')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>Because you are enrolled in a Meta ILA, your course @Model.ILATitle has been already released.</p><p>Please log into your EMP Dashboard to complete the course.</p><p>If you are unable to complete the course by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Self Paced Released')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>Because you are enrolled in a Meta ILA, you need to enroll yourself in the course @Model.ILATitle.</p>@if(Model.RegistrationsAvailable) {<p>Please log into your EMP Dashboard and use the self-registration portal to enroll in a class.</p><p>If you are unable to attend the classes listed, notify your Training Administrator as soon as possible.</p>}else{<p>There are no upcoming classes for the ILA. Please notify your Training Administrator as soon as possible.</p>}<p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Employee - Self Registration Needed')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>Because you are enrolled in a Meta ILA, you need to enroll yourself in the course @Model.ILATitle.</p>@if(Model.RegistrationsAvailable) {<p>Please log into your EMP Dashboard and use the self-registration portal to enroll in a class.</p><p>If you are unable to attend the classes listed, notify your Training Administrator as soon as possible.</p>}else{<p>There are no upcoming classes for the ILA. Please notify your Training Administrator as soon as possible.</p>}<p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Employee - Self Registration Needed')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello,<p>Please be aware that @Model.EmployeeFirstName @Model.EmployeeLastName has completed the ILA @Model.PreviousILATitle.</p><p>As part of the Meta ILA @Model.MetaILATitle, they are now required to take the ILA @Model.ILATitle.</p>@if(Model.RegistrationsAvailable) {<p>The employee has been notified that they need to self-register in the ILA. However, it is possible that the employee may request a different time. They have been instructed to reach out to their Training Administrator if the times do not work for them.</p>} else {<p>The employee has been notified that they need to self-register in the ILA. However, currently, there are no available classes for the employee to register in. You will need to create an additional class for the employee to continue.</p>}<p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Admin - Self Registration Needed')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>Because you are enrolled in a Meta ILA, you will be enrolled in the next course @Model.ILATitle.</p><p>Please coordinate with your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Employee - Registration Needed')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello,<p>Please be aware that @Model.EmployeeFirstName @Model.EmployeeLastName has completed the ILA @Model.PreviousILATitle.</p><p>As part of the Meta ILA @Model.MetaILATitle, they are now required to take the ILA @Model.ILATitle.</p><p>The employee has been notified that they need to be enrolled in the next ILA. However, that ILA is not configured to allow self-registration. You will need to assist the employee for them to continue.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Admin - Registration Needed')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>You have now completed the coursework for your Meta ILA.</p><p>To complete your training on this Meta ILA, you need to complete the associated test. It has been released to your EMP portal.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Coursework Complete')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '@using RazorEngine.Text <p>Hello @Model.CollaboratorFirstName @Model.CollaboratorLastName, you have been invited to participate in Simulator Scenario - @Model.SimulatorScenarioTitle. <a href=""@Model.SimulatorScenarioLink"">Click here to view it</a>.</p><p>@(new RawString(Model.SimulatorScenarioMessage))</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Simulator Scenario Collaboration')"
            );
        }

        protected void Production_Update_NotifcationEmailTemplate()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p>This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days.Your expiration date is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, we have not received a copy of your updated @Model.CertificateName certificate.</p><p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records.If you have any questions, please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 2"
            );


            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your attention is required. This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p> <p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 3"
             );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your immediate attention is required. This is an urgent reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>After multiple notifications regarding the renewal of your @Model.CertificateName credential, your management has also received a copy of this notification. You and your management will continue to receive daily reminders until the System Operations Training Team receives an updated copy of your @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 4"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = ' @using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>In order to receive credit for completion of @Model.CourseTitle, you must also complete the online test. You can access the test through the Employee Portal (EMP). Please review the table below for further details.</p> <figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Test Due Date</td><td>@Model.TestDueDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p> <p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Test')"
             );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been enrolled in a course which requires you to complete a pretest prior to the course start date. You can access the pretest through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest ID/Name</td><td>@Model.PretestId/@Model.PretestTitle</td></tr><tr><td>Pretest Available Date</td><td>@Model.PretestAvailableDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest Due Date</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Pretest')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned an online training course to complete. You can access the course through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.ILATitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Course Available Date</td><td>@Model.CourseStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Course Due Date</td><td>@Model.CourseEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the course by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Online Course')"
            );


            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete an evaluation for a course you recently completed. You can access the evaluation through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.ILATitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Eval Available Date</td><td>@Model.CourseEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Eval Due Date</td><td>@Model.EvalDueDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the evaluation by the due date listed, notify your Training Administrator.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Student Evaluation')"
             );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete a procedure review. You can complete the procedure review through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Procedure Title</td><td>@Model.ProcedureTitle</td></tr><tr><td>Review Available Date</td><td>@Model.ReviewStartdate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Review Due Date</td><td>@Model.ReviewEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Procedure Review')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete an IDP review. You can complete the IDP review through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>IDP Title</td><td>@Model.IDPTitle</td></tr><tr><td>Review Available Date</td><td>@Model.ReviewStartdate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Review Due Date</td><td>@Model.ReviewEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP IDP Review')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your self-registration request to enroll in @Model.CourseTitle on @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) and @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) has been approved.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Self-Registration Approval')"
             );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your self-registration request to enroll in @Model.CourseTitle on @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) and @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) has been denied.</p><p>Please contact your Training Administrator for additional information.</p><p>Thank you</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Self-Registration Denial')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete a DIF Survey. You can access the DIF Survey through the Employee Portal (EMP). Please review the table below for further details.</p><p>This survey is being conducted to help us improve both the initial and continuing training for @Model.PositionTitle position, and your input is an important part of this process. You will be asked to review and rate the tasks you perform in terms of difficulty, importance, and frequency. Further instructions are provided once you start the survey.</p><figure class=""table""> <table><tr><td>Position Title</td><td>@Model.PositionTitle</td></tr><tr><td>Survey Title</td><td>@Model.SurveyTitle</td></tr><tr><td>Survey Available Date</td><td>@Model.SurveyStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Survey Due Date</td><td>@Model.SurveyEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the DIF Survey by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP DIF Survey')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete a GAP Survey. You can access the GAP Survey through the Employee Portal (EMP). Please review the table below for further details.</p><p>This survey is being conducted to help us improve and provide on-target training for @Model.PositionTitle position, and your input is an important part of this process.</p><figure class=""table""><table><tr><td>Position Title</td><td>@Model.PositionTitle</td></tr><tr><td>Survey Title</td><td>@Model.SurveyTitle</td></tr><tr><td>Survey Available Date</td><td>@Model.SurveyStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Survey Due Date</td><td>@Model.SurveyEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the GAP Survey by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP GAP Survey')"
             );
        }

        protected void Production_UpdateTaskEoLinksOfDeletedEO()
        {
            _migrationBuilder.Sql(@"UPDATE Task_EnablingObjective_Links SET Deleted = 1 WHERE EnablingObjectiveId IN ( SELECT Id FROM EnablingObjectives WHERE Deleted = 1 );");
        }

        protected void Production_Update_NotificationEmailTemplatesWithUsing()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your immediate attention is required. This is an urgent reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>After multiple notifications regarding the renewal of your @Model.CertificateName credential, your management has also received a copy of this notification. You and your management will continue to receive daily reminders until the System Operations Training Team receives an updated copy of your @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 4"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been enrolled in a course which requires you to complete a pretest prior to the course start date. You can access the pretest through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.ClassEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest ID/Name</td><td>@Model.PretestId/@Model.PretestTitle</td></tr><tr><td>Pretest Available Date</td><td>@Model.PretestAvailableDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest Due Date</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Pretest')"
            );
        }

        protected void Production_UndeleteCompletedClassesAndClearIDPInfo()
        {
            _migrationBuilder.Sql(@"DECLARE @ClassScheduleIds TABLE (Id INT);
                INSERT INTO @ClassScheduleIds (Id)
                SELECT a.Id
                FROM dbo.ClassSchedules a
                WHERE a.Deleted = 1
                AND EXISTS (
                    SELECT 1
                    FROM dbo.ClassScheduleEmployees aa
                    WHERE a.Id = aa.ClassScheduleId
                      AND aa.FinalGrade IS NOT NULL
                )
                update dbo.ClassSchedules set Deleted = 0 where id in (select * from @ClassScheduleIds)
                update dbo.ClassScheduleEmployees set Deleted = 0 where ClassScheduleId in (select * from @ClassScheduleIds)
                ");

            _migrationBuilder.Sql(@"update a
                set deleted = 1
                from
                dbo.IDPSchedules a
                inner join dbo.ClassSchedules b
	                on a.ClassScheduleId = b.Id and b.Deleted = 1 and not exists(
		                select 1
		                from dbo.ClassScheduleEmployees aa
		                where 
		                b.Id = aa.ClassScheduleId
		                and aa.FinalGrade is not null
	                )
                where
                a.Deleted = 0");
        }
        protected void Production_UpdateEMPPretestNotificationEmailTemplate()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been enrolled in a course which requires you to complete a pretest prior to the course start date. You can access the pretest through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) - @Model.ClassEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest ID/Name</td><td>@Model.PretestId/@Model.PretestTitle</td></tr><tr><td>Pretest Available Date</td><td>@Model.PretestAvailableDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest Due Date</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Pretest')"
           );
        }

        protected void Production_AddProfessionlaHoursToCertificationTable()
        {
            _migrationBuilder.InsertData(
            table: "Certifications",
            columns: new[] { "Name", "CertAcronym", "CertifyingBodyId", "RenewalTimeFrame", "CreditHrsReq", "AllowRolloverHours", "AdditionalHours", "EffectiveDate", "Active", "Deleted", "RenewalInterval" },
            values: new object[,]
            {
                 { "Professional", "PROF", 2, true, false, false, 0.00, DateTime.Now, true, false, 1 },
            });
        }

        protected void Production_UpdateVersionTasksIsInUseFlag()
        {
            _migrationBuilder.Sql(@"
                WITH CTE_Ordered_VersionTasks_For_Tasks_Without_IsInUse_VersionTask AS (
                    SELECT 
                        a.Id,
                        ROW_NUMBER() OVER (PARTITION BY a.TaskId ORDER BY a.Id DESC) AS rn
                    FROM 
                        dbo.Version_Tasks a
                    WHERE 
                        a.Deleted = 0
                        AND NOT EXISTS (
                            SELECT 1 
                            FROM dbo.Version_Tasks bb 
                            WHERE a.TaskId = bb.TaskId 
                                AND bb.Deleted = 0 
                                AND bb.IsInUse = 1
                        )
                )

                UPDATE dbo.Version_Tasks 
                SET IsInUse = 1
                FROM dbo.Version_Tasks a
                INNER JOIN CTE_Ordered_VersionTasks_For_Tasks_Without_IsInUse_VersionTask b
                    ON a.Id = b.Id 
                    AND b.rn = 1;
            ");
        }

        protected void Production_MigrateDataFromProcedure_ILA_LinksToILA_Procedure_Links()
        {
            _migrationBuilder.Sql(@"INSERT INTO ILA_Procedure_Links
                                    ([ILAId]
                                    ,[ProcedureId]
                                    ,[Deleted]
                                    ,[Active]
                                    ,[CreatedBy]
                                    ,[CreatedDate]
                                    ,[ModifiedBy]
                                    ,[ModifiedDate])
                                         SELECT p.[ILAId]
                                               ,p.[ProcedureId]
                                               ,p.[Deleted]
                                               ,p.[Active]
                                               ,p.[CreatedBy]
                                               ,p.[CreatedDate]
                                               ,p.[ModifiedBy]
                                               ,p.[ModifiedDate]
                                         FROM Procedure_ILA_Links p
                                         WHERE NOT EXISTS (
                                             SELECT 1 
                                             FROM [ILA_Procedure_Links] i
                                             WHERE i.[ILAId] = p.[ILAId]
                                               AND i.[ProcedureId] = p.[ProcedureId]
                                         );");
        }

        protected void Production_AddNewReportFilters_AccordingToRRTTypes()
        {
            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
                values: new object[,]
                {
                    {1,"Select RRT's", "String", "Single", DateTime.MinValue, DateTime.MinValue, "rrttypes", false, true,null,null  }
                }
            );
        }

        protected void Production_UpdateReportFilters_ILAByProvider()
        {
            _migrationBuilder.DeleteData(
                table: "ReportSkeletonFilters",
                keyColumns: new[] { "ReportSkeletonId", "Name" },
                keyValues: new object[,]
                {
                 {4, "Delivery Method" },
                 {4,"ILAs with Approvals expiring within Months" }
                }
            );

            _migrationBuilder.UpdateData(
               table: "ReportSkeletonFilters",
               keyColumns: new[] { "ReportSkeletonId", "Name" },
               keyValues: new object[] { 4, "ILA Status" },
               column: "DefaultValue",
               value: "Active Only"
            );

            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
                values: new object[,]
                {
                    {4,"Show ILA Application Dates", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null  }
                }
            );
        }

        protected void Production_UpdateReport_ILAByCompletionHistory()
        {
            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue" },
                values: new object[,] {
                          {13, "Active/Inactive Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true,"Active Only"},
                            }
                );

            _migrationBuilder.UpdateData(
            table: "ReportSkeletonFilters",
            keyColumns: new[] { "ReportSkeletonId", "Name" },
            keyValues: new object[] { 13, "Completion Type" },
            column: "FilterOption",
            value: "coursecompletionstatus"
         );

        }
        
        protected void Production_UpdateReport_TaskByPosition()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletonFilters",
            keyColumns: new[] { "ReportSkeletonId", "Name" },
            keyValues: new object[] { 1, "Show Specific Tasks" },
            column: "DefaultValue",
            value: "All Active Tasks"
           );
        }

        protected void Production_UpdateReport_TrainingIssueList()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletons",
            keyColumns: new[] { "DefaultTitle" },
            keyValues: new object[] { "Issues List" },
            column: "DefaultTitle",
            value: "Training Issues List"
           );

            _migrationBuilder.DeleteData(
                table: "ReportSkeletonFilters",
                keyColumns: new[] { "ReportSkeletonId", "Name" },
                keyValues: new object[,]
                {
                     {26, "Program Type" },
                     {26, "ILA" },
                     {26, "OJT / Task Qualification" },
                     {26, "Open Only" },
                }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                  {26, "Select Training Component", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingissuecomponent", false, true,"listAllSelected",null},
                  {26, "Severity Level", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingissueseveritylevel", false, true,"listAllSelected",null},
                  {26, "Open/Closed Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingissuestatus", false, true,"All",null}
              }
            );

            _migrationBuilder.DeleteData(
            table: "ReportSkeletonColumns",
            keyColumn: "ReportSkeletonId",
            keyValue: 26
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                   {26, "Issue ID",  false, true },
                   {26, "Created Date",  false, true },
                   {26, "Due Date",  false, true },
                   {26, "Training Issue Title",  false, true },
                   {26, "Severity",  false, true },
                   {26, "Driver",  false, true },
                   {26, "Training Component",  false, true },
                   {26, "Pending Action Items",  false, true },
                   {26, "Open/Closed Status",  false, true },
                   {26, "Active/Inactive Status",  false, true },
             });
        }

        protected void Production_AddReport_TrainingIssueDetails()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"Training Issue Details", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {42, 109, 6, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                  {109, "Select Training Issue", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingissue", false, true,"listAllSelected",null}
               }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                   {109, "Issue ID",  false, true },
                   {109, "Training Issue Title",  false, true },
                   {109, "Created Date",  false, true },
                   {109, "Due Date",  false, true },
                   {109, "Severity",  false, true },
                   {109, "Issue Status",  false, true },
                   {109, "Driver",  false, true },
                   {109, "Data Element",  false, true },
                   {109, "Description",  false, true },
                   {109, "Planned Response",  false, true },
                   {109, "Action Items",  false, true }
             }
            );

            _migrationBuilder.UpdateData(
            table: "ReportSkeleton_Subcategory_Reports",
            keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
            keyValues: new object[] { 42, 6 },
            column: "Order",
            value: "7");
        }

        protected void Production_AddReport_TrainingIssuesActionItems()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"Training Issue Action Items", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {42, 110, 7, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                  {110, "Select Training Issue", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingissue", false, true,"listAllSelected",null},
                  {110, "Action Step Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "actionitemstepstatus", false, true,"All",null},
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                   {110, "Issue ID",  false, true },
                   {110, "Training Issue Title",  false, true },
                   {110, "Created Date",  false, true },
                   {110, "Due Date",  false, true },
                   {110, "Severity",  false, true },
                   {110, "Driver",  false, true },
                   {110, "Data Element",  false, true },
                   {110, "Description",  false, true },
                   {110, "Planned Response",  false, true },
                   {110, "Assigned To",  false, true },
                   {110, "Priority",  false, true },
                   {110, "Date Assigned",  false, true },
                   {110, "Action Step Due Date",  false, true },
                   {110, "Date Completed",  false, true },
                   {110, "Status",  false, true },
                   {110, "Notes",  false, true }
             }
            );

            _migrationBuilder.UpdateData(
            table: "ReportSkeleton_Subcategory_Reports",
            keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
            keyValues: new object[] { 42, 6 },
            column: "Order",
            value: "8");
        }

        protected void Production_UpdateReport_TasksByTrainingTaskGroup()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletons",
            keyColumns: new[] { "Id", "DefaultTitle" },
            keyValues: new object[] { 5, "Tasks by Task Group" },
            column: "DefaultTitle",
            value: "Tasks by Training Task Group"
           );

            _migrationBuilder.DeleteData(
            table: "ReportSkeletonColumns",
            keyColumn: "ReportSkeletonId",
            keyValue: 5
            );

            _migrationBuilder.DeleteData(
            table: "ReportSkeletonFilters",
            keyColumn: "ReportSkeletonId",
            keyValue: 5
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue" },
              values: new object[,]
              {
                  {5, "Select Task Group", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingtasksgroup", false, true,null},
                  {5,"Include Tasks not Assigned to a Task Group", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false"},
                  {5,"Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
                  {5,"Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
                  {5,"Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
                {
                      {5, "Duty Area",  false, true },
                      {5, "Sub-Duty Area",  false, true },
                      {5, "Linked Positions",  false, true },
                }
            );
        }

        protected void Production_AddNewReportFilters_EmployeeTrainingTowardNERCRecertification()
        {
            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
                values: new object[,]
                {
                    {58, "Include Failed Grade", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
                }
            );
        }

        protected void Production_UpdateReportFilters_TasksByPosition()
        {
            _migrationBuilder.DeleteData(
                table: "ReportSkeletonFilters",
                keyColumns: new[] { "ReportSkeletonId", "Name" },
                keyValues: new object[,]
                {
                     {1, "Show Specific Tasks" },
                     {1, "Include Pseudo Tasks" },
                     {1, "Select RRT's" },
                }
            );

            _migrationBuilder.UpdateData(
            table: "ReportSkeletonFilters",
            keyColumns: new[] { "ReportSkeletonId", "Name" },
            keyValues: new object[] { 1, "Group By Training Task Groups" },
            column: "Name",
            value: "Group by Training Task Group"
           );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue" },
              values: new object[,]
              {
                  {1, "Select Task Type", "String", "Single", DateTime.MinValue, DateTime.MinValue, "tasktype", false, true,"Tasks (Regular only)"},
                  {1,"RR Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
                  {1,"All/Active/Inactive Tasks", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true,"Active Only" },
                  {1,"Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
              }
            );
        }

        protected void Production_AddClientSettings_Feature_PublicClasses()
        {

            _migrationBuilder.InsertData(
                table: "ClientSettings_Features",
                columns: new[] { "Feature", "Enabled", "Active", "Deleted" },
                 values: new object[,]
                {
                  { "Public Classes", false,true,false },
                });


        }

        protected void Production_UpdateReportFilters_TrainingIssuesActionItems()
        {
            _migrationBuilder.UpdateData(
              table: "ReportSkeletonFilters",
              keyColumns: new[] { "ReportSkeletonId", "Name" },
              keyValues: new object[] { 110, "Select Training Issue" },
              column: "DefaultValue",
              value: null
             );
        }

        protected void Production_UpdateReportFilters_TrainingIssuesDetails()
        {
            _migrationBuilder.UpdateData(
              table: "ReportSkeletonFilters",
              keyColumns: new[] { "ReportSkeletonId", "Name" },
              keyValues: new object[] { 109, "Select Training Issue" },
              column: "DefaultValue",
              value: null
             );
        }

        protected void Production_AddReportFilters_TrainingProgramCompletionHistory()
        {
            _migrationBuilder.InsertData(
             table: "ReportSkeletonFilters",
             columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue" },
             values: new object[,]
             {
                  {50,"Current Position Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"true" },
                  {50,"All/Active/Inactive Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true,"Active Only" },
             }
           );
        }

        protected void Production_UpdateReportFilters_TrainingProgramCompletionHistory()
        {
            _migrationBuilder.DeleteData(
                 table: "ReportSkeletonFilters",
                 keyColumn: "Name",
                 keyValue: "Current Position Only"
                 );
        }
        protected void Production_AddDataToTrainingIssueActionItemsAssigneeName()
        {
            _migrationBuilder.Sql(@"UPDATE a
                SET AssigneeName = CONCAT(p.FirstName, ' ', p.LastName)
                FROM TrainingIssueActionItems a
                JOIN QTDUsers u ON a.AssignedToId = u.Id
                JOIN Persons p ON u.PersonId = p.Id
                WHERE a.AssignedToId IS NOT NULL;"
            );
        }

        protected void Production_SeedInitialReportData()
        {
            _migrationBuilder.InsertData(
             table: "ReportSkeleton_Categories",
             columns: new[] { "Name", "Deleted", "Active" },
             values: new object[,]
               {
                                   { "All Reports", false, true },
                                   { "Per 005", false, true },
                                   { "CEH Total 1", false, true },
                                   { "CEH Total 2", false, true},
               });

            _migrationBuilder.InsertData(
             table: "ReportSkeleton_Subcategories",
             columns: new[] { "Id", "Name", "ReportSkeleton_CategoryId", "Order", "Deleted", "Active" },
             values: new object[,]
             {
                    {  13, "R1", 2, 1, false, true },
                    {  14, "R1.1", 2, 2, false, true },
                    {  15, "R1.1.1", 2, 3, false, true },
                    {  16, "R1.2", 2, 4, false, true },
                    {  17, "R1.3", 2, 5, false, true },
                    {  18, "R1.4", 2, 6, false, true },
                    {  19 , "R2", 2, 7, false, true },
                    {  20, "R2.1", 2, 8, false, true },
                    {  21, "R2.2", 2, 9, false, true },
                    {  22, "R2.3", 2, 10, false, true },
                    {  23, "R2.4", 2, 11, false, true },
                    {  24 ,"R3", 2, 12, false, true },
                    {  25, "R3.1", 2, 13, false, true },
                    {  26, "R4", 2, 14, false, true },
                    {  27, "R4.1", 2, 15, false, true },
                    {  28, "R5", 2, 16, false, true },
                    {  29, "R5.1", 2, 17, false, true },
                    {  30, "R6", 2, 18, false, true },
                    {  31, "R6.1", 2, 19, false, true },
                    {  32, "Reports", 3, 1, false, true },
                    {  33, "Reports", 4, 1, false, true },
                    {  35, "Employee Data", 1, 1, false, true },
                    {  36, "Positions", 1, 2, false, true },
                    {  37, "Tasks", 1, 3, false, true },
                    {  38, "Enabling Objectives", 1, 4, false, true },
                    {  39, "Training Design & Development", 1, 8, false, true },
                    {  40, "Training Implementation & Delivery", 1, 9, false, true },
                    {  41, "Task Qualification & Requalification Records", 1, 10, false, true },
                    {  42, "Evaluations", 1, 11, false, true },
                    {  43, "NERC Reports", 1, 12, false, true },
                    {  44, "Meta ILAs", 1, 13, false, true },
                    {  45, "Employee Portal Completions", 1, 14, false, true },
                    {  47, "Surveys", 1, 7, false, true },
                    {  48, "Procedures", 1, 5, false, true },
                    {  49, "Data Quality Control", 1, 15, false, true },
                    {  50, "Safety Hazards", 1, 6, false, true }
             });

            _migrationBuilder.InsertData(
                table: "ReportSkeletons",
                columns: new[] { "Id", "DefaultTitle", "Deleted", "Active" },
                values: new object[,]
                {
                        { 1, "Tasks by Positions", false, true },
                        { 2, "IDP Review Completion History", false, true },
                        { 3, "Training Schedule By Class", false, true },
                        { 4, "ILA By Provider", false, true },
                        { 5, "Tasks by Task Group", false, true },
                        { 6, "Student Evaluation - Blank Form", false, true },
                        { 7, "Position Details", false, true },
                        { 8, "Employees By Position", false, true },
                        { 9, "Position Linkages", false, true },
                        { 10, "Employees By Organization", false, true },
                        { 11, "Employee Position History", false, true },
                        { 12, "Employee Certification History", false, true },
                        { 13, "ILA Completion History", false, true },
                        { 14, "Classes by ILA", false, true },
                        { 15, "ILA Design Specification", false, true },
                        { 16, "Task Qualification Evaluators", false, true },
                        { 17, "Student Evaluation Results - Self Paced", false, true },
                        { 18, "Task Requalification by Position", false, true },
                        { 19, "Task Qualification Records", false, true },
                        { 20, "Test Items", false, true },
                        { 21, "Test Specifications", false, true },
                        { 22, "Task Requalification by Employee", false, true },
                        { 23, "List of Task Evaluators", false, true },
                        { 24, "Student Evaluation Results - Instructor Led", false, true },
                        { 25, "List of NERC Certified Operators", false, true },
                        { 26, "Issues List", false, true },
                        { 27, "Training Summary by Position", false, true },
                        { 28, "OJT Guide By Position", false, true },
                        { 29, "OJT Guide By Task", false, true },
                        { 30, "OJT Guide By ILA", false, true },
                        { 31, "Task Qualification Sheets by Position", false, true },
                        { 32, "Task Qualification Sheets by Task", false, true },
                        { 33, "Task Qualification Sheets by ILA", false, true },
                        { 34, "Procedure Review Completion History", false, true },
                        { 35, "Training Program Review", false, true },
                        { 36, "Enabling Objectives By Task", false, true },
                        { 37, "Enabling Objectives By Position", false, true },
                        { 38, "Employee Training Needs Assessment", false, true },
                        { 39, "Class Roster", false, true },
                        { 40, "Annual Positions Task List Review", false, true },
                        { 41, "EOP Hours for Designated Years", false, true },
                        { 42, "Task and Enabling Objective By ILA", false, true },
                        { 43, "Record of Task/ EO Qualifications", false, true },
                        { 44, "Task History by Position", false, true },
                        { 45, "Training Material for Task and Associated EOs by Positions", false, true },
                        { 46, "Training Program By Positions", false, true },
                        { 47, "Employee Training Status", false, true },
                        { 48, "Training Module Completion History By Employee", false, true },
                        { 49, "Training Module Completion History", false, true },
                        { 50, "Training Program Completion History", false, true },
                        { 51, "Class Sign In Sheet", false, true },
                        { 53, "DIF Survey - Blank Form", false, true },
                        { 54, "DIF Survey - Individual Feedback", false, true },
                        { 55, "DIF Survey - Aggregate Results", false, true },
                        { 56, "Year To Date Hours For Certified Employees", false, true },
                        { 57, "Total NERC CEHS for the Year to Date", false, true },
                        { 58, "Employee Training Toward NERC Recertification", false, true },
                        { 59, "Employee Training Toward Cert and All Required Training", false, true },
                        { 60, "Employee Delinquency Report", false, true },
                        { 61, "Procedure Review Completion History by Employee", false, true },
                        { 62, "Tasks Met by Position", false, true },
                        { 63, "Tasks Met by Employee", false, true },
                        { 64, "Employee Active/Inactive History", false, true },
                        { 65, "Reliability Related Task Impact Matrix (R5)", false, true },
                        { 66, "EMP Test Summary by Classes", false, true },
                        { 67, "Employee Training Toward All Cert and Req. Training Summary", false, true },
                        { 68, "EMP Task Qualification Details", false, true },
                        { 69, "Employee Course Schedule for Given Year", false, true },
                        { 70, "Employee IDP Completion Status Report", false, true },
                        { 71, "Task History By Task", false, true },
                        { 72, "Employee Training Toward Procedures & Regulatory Requirements", false, true },
                        { 73, "Procedure & Regulatory Requirement Training Summary by Position", false, true },
                        { 74, "Summary of Task Qualification by Sub-Duty Area", false, true },
                        { 75, "Employee Task Qualification Dates by Task", false, true },
                        { 76, "Employee Task Qualification Records for Given Position", false, true },
                        { 77, "EMP Test Statistics", false, true },
                        { 78, "SCORM Completion Summary by Classes", false, true },
                        { 79, "SCORM Test Completion Statistics", false, true },
                        { 80, "OJT Training Log", false, true },
                        { 81, "Procedure by Issuing Authority", false, true },
                        { 82, "Procedures by Task", false, true },
                        { 83, "Enabling Objectives Not Linked to Tasks", false, true },
                        { 84, "Enabling Objectives Not Linked to ILA", false, true },
                        { 85, "Tasks by Duty Area", false, true },
                        { 86, "Enabling Objectives by Category", false, true },
                        { 87, "Test Report - Paper Based Version", false, true },
                        { 88, "Tasks by Enabling Objectives", false, true },
                        { 89, "ILAs by Task", false, true },
                        { 90, "ILAs by Enabling Objective", false, true },
                        { 91, "ILAs with EOs Required By Positions", false, true },
                        { 92, "Skills Qualification Training Guide by Position or Skill", false, true },
                        { 93, "Skill Qualification Assessment by Position or by Task", false, true },
                        { 94, "Safety Hazards by Position Matrix", false, true },
                        { 95, "Safety Hazards by Category", false, true },
                        { 96, "Class Certificates", false, true },
                        { 97, "Pretest & Final Test Comparison Report", false, true },
                        { 98, "Certified Employees for Given Certificate", false, true },
                        { 99, "Tasks Not Linked to ILA", false, true },
                        { 100, "Tasks Not Assigned to Position", false, true },
                        { 101, "Tasks by Procedure", false, true },
                        { 102, "ILAs by Meta ILA", false, true },
                        { 103, "Tasks by Position Matrix", false, true },
                        { 104, "Test List", false, true },
                        { 105, "Safety Hazards by Task", false, true },
                        { 106, "Enabling Objectives by Position Matrix", false, true },
                        { 107, "Enabling Objectives by Safety Hazard", false, true },
                        { 108, "NERC ILA Application - Report Version", false, true }
                });


            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Deleted", "Active", "Order", },
                  values: new object[,]
                  {

                        {13,  15,  false, true,   1   },
                        {14,  1,   false, true,   1   },
                        {15,  18, false, true,  1   },
                        {16 , 15  ,false, true, 1   },
                        {17 , 14  ,false, true, 1   },
                        {18 , 26  ,false, true, 1   },
                        {18 , 35  ,false, true, 2   },
                        {21 , 15  ,false, true, 1   },
                        {22 , 14  ,false, true, 1   },
                        {23 , 26  ,false, true, 1   },
                        {23 , 35  ,false, true, 2   },
                        {24 , 22  ,false, true, 1   },
                        {24 , 18  ,false, true, 2   },
                        {24 , 31  ,false, true, 3   },
                        {25 , 22  ,false, true, 1   },
                        {25 , 18  ,false, true, 2   },
                        {25 , 31  ,false, true, 3   },
                        {26 , 15  ,false, true, 1   },
                        {27 , 15  ,false, true, 1   },
                        {28 , 15  ,false, true, 1   },
                        {29 , 26  ,false, true, 1   },
                        {29 , 35  ,false, true, 2   },
                        {30 , 15  ,false, true, 1   },
                        {30 , 14  ,false, true, 2   },
                        {31 , 26  ,false, true, 1   },
                        {31 , 35  ,false, true, 2   },
                        {33 , 13  ,false, true, 1   },
                        {16 , 36  ,false, true, 2   },
                        {21 , 36  ,false, true, 2   },
                        {13 , 37  ,false, true, 2   },
                        {21 , 37  ,false, true, 3   },
                        {28 , 37  ,false, true, 2   },
                        {30 , 37  ,false, true, 3   },
                        {13 , 38  ,false, true, 3   },
                        {24 , 38  ,false, true, 4   },
                        {28 , 38  ,false, true, 3   },
                        {30 , 38  ,false, true, 4   },
                        {17 , 39  ,false, true, 2   },
                        {22 , 39  ,false, true, 2   },
                        {30 , 39  ,false, true, 5   },
                        {33 , 39  ,false, true, 2   },
                        {14 , 40  ,false, true, 2   },
                        {15 , 40  ,false, true, 2   },
                        {19 , 40  ,false, true, 1   },
                        {20 , 40  ,false, true, 1   },
                        {26 , 41  ,false, true, 2   },
                        {27 , 41  ,false, true, 2   },
                        {16 , 42  ,false, true, 3   },
                        {21 , 42  ,false, true, 4   },
                        {24 , 43  ,false, true, 5   },
                        {25 , 43  ,false, true, 4   },
                        {15 , 44  ,false, true, 3   },
                        {24 , 44  ,false, true, 6   },
                        {25 , 44  ,false, true, 5   },
                        {16 , 45  ,false, true, 4   },
                        {21 , 45  ,false, true, 5   },
                        {13 , 46  ,false, true, 4   },
                        {16 , 46  ,false, true, 5   },
                        {21 , 46  ,false, true, 6   },
                        {28 , 46  ,false, true, 4   },
                        {30 , 46  ,false, true, 6   },
                        {26 , 47  ,false, true, 3   },
                        {27 , 47  ,false, true, 3   },
                        {19 , 1   ,false, true, 2   },
                        {20 , 1   ,false, true, 2   },
                        {24 , 8   ,false, true, 7   },
                        {25 , 8   ,false, true, 6   },
                        {24 , 10  ,false, true, 8   },
                        {25 , 10  ,false, true, 7   },
                        {24 , 19  ,false, true, 9   },
                        {25 , 19  ,false, true, 8   },
                        {24 , 32  ,false, true, 10  },
                        {25 , 32  ,false, true, 9   },
                        {24 , 33  ,false, true, 11  },
                        {25 , 33  ,false, true, 10  },
                        {16 , 48  ,false, true, 6   },
                        {16 , 49  ,false, true, 7   },
                        {16 , 50  ,false, true, 8   },
                        {17 , 50  ,false, true, 3   },
                        {21 , 50  ,false, true, 7   },
                        {22 , 50  ,false, true, 3   },
                        {30 , 50  ,false, true, 7   },
                        {32 , 17  ,false, true, 1   },
                        {32 , 24  ,false, true, 2   },
                        {33 , 17  ,false, true, 3   },
                        {33 , 24  ,false, true, 4   },
                        {13 , 13  ,false, true, 5   },
                        {17 , 51  ,false, true, 4   },
                        {22 , 51  ,false, true, 4   },
                        {30 , 51  ,false, true, 8   },
                        {33 , 51  ,false, true, 5   },
                        {35 , 10  ,false, true, 1   },
                        {35 , 8   ,false, true, 2   },
                        {43 , 25  ,false, true, 10  },
                        {35 , 11  ,false, true, 4   },
                        {35 , 12  ,false, true, 5   },
                        {35 , 38  ,false, true, 7   },
                        {35 , 47  ,false, true, 8   },
                        {36 , 7   ,false, true, 1   },
                        {36 , 9   ,false, true, 2   },
                        {37 , 1   ,false, true, 1   },
                        {37 , 5   ,false, true, 3   },
                        {37 , 44  ,false, true, 6   },
                        {38 , 37  ,false, true, 2   },
                        {38 , 36  ,false, true, 3   },
                        {39 , 46  ,false, true, 1   },
                        {39 , 4   ,false, true, 2   },
                        {39 , 15  ,false, true, 3   },
                        {39 , 42  ,false, true, 4   },
                        {39 , 45  ,false, true, 5   },
                        {39 , 28  ,false, true, 6   },
                        {39 , 29  ,false, true, 7   },
                        {39 , 30  ,false, true, 8   },
                        {39 , 31  ,false, true, 10  },
                        {39 , 32  ,false, true, 11  },
                        {39 , 33  ,false, true, 12  },
                        {39 , 20  ,false, true, 15  },
                        {39 , 21  ,false, true, 18  },
                        {40 , 3   ,false, true, 1   },
                        {40 , 39  ,false, true, 2   },
                        {40 , 51  ,false, true, 3   },
                        {40 , 14  ,false, true, 4   },
                        {40 , 50  ,false, true, 5   },
                        {40 , 13  ,false, true, 6   },
                        {41 , 16  ,false, true, 1   },
                        {41 , 19  ,false, true, 2   },
                        {41 , 43  ,false, true, 6   },
                        {41 , 18  ,false, true, 8   },
                        {41 , 22  ,false, true, 9   },
                        {42 , 6   ,false, true, 6   },
                        {42 , 24  ,false, true, 1   },
                        {42 , 17  ,false, true, 2   },
                        {42 , 40  ,false, true, 3   },
                        {42 , 35  ,false, true, 4   },
                        {42 , 26  ,false, true, 5   },
                        {43 , 27  ,false, true, 8   },
                        {43 , 41  ,false, true, 9   },
                        {44 , 48  ,false, true, 2   },
                        {44 , 49  ,false, true, 3   },
                        {44 , 2   ,false, true, 4   },
                        {45 , 34  ,false, true, 4   },
                        {47 , 53  ,false, true, 1   },
                        {47 , 54  ,false, true, 2   },
                        {47 , 55  ,false, true, 3   },
                        {43 , 56  ,false, true, 7   },
                        {43 , 57  ,false, true, 6   },
                        {43 , 58  ,false, true, 2   },
                        {43 , 59  ,false, true, 3   },
                        {43 , 60  ,false, true, 5   },
                        {45 , 61  ,false, true, 5   },
                        {41 , 62  ,false, true, 5   },
                        {41 , 63  ,false, true, 4   },
                        {35 , 64  ,false, true, 6   },
                        {36 , 65  ,false, true, 3   },
                        {45 , 66  ,false, true, 1   },
                        {43 , 67  ,false, true, 4   },
                        {45 , 68  ,false, true, 6   },
                        {35 , 69  ,false, true, 9   },
                        {35 , 70  ,false, true, 10  },
                        {37 , 71  ,false, true, 7   },
                        {35 , 72  ,false, true, 11  },
                        {35 , 73  ,false, true, 12  },
                        {41 , 74  ,false, true, 7   },
                        {41 , 75  ,false, true, 10  },
                        {41 , 76  ,false, true, 3   },
                        {45 , 77  ,false, true, 3   },
                        {45 , 78  ,false, true, 7   },
                        {45 , 79  ,false, true, 8   },
                        {39 , 80  ,false, true, 9   },
                        {48 , 81  ,false, true, 1   },
                        {48 , 82  ,false, true, 2   },
                        {49 , 83  ,false, true, 3   },
                        {49 , 84  ,false, true, 4   },
                        {37 , 85  ,false, true, 2   },
                        {38 , 86  ,false, true, 1   },
                        {39 , 87  ,false, true, 17  },
                        {37 , 88  ,false, true, 5   },
                        {49 , 89  ,false, true, 5   },
                        {49 , 90  ,false, true, 6   },
                        {49 , 91  ,false, true, 7   },
                        {39 , 92  ,false, true, 13  },
                        {39 , 93  ,false, true, 14  },
                        {50 , 94  ,false, true, 4   },
                        {50 , 95  ,false, true, 1   },
                        {40 , 96  ,false, true, 7   },
                        {45 , 97  ,false, true, 2   },
                        {35 , 98  ,false, true, 3   },
                        {49 , 99  ,false, true, 2   },
                        {49 , 100 ,false, true, 1   },
                        {37 , 101 ,false, true, 4   },
                        {44 , 102 ,false, true, 1   },
                        {36 , 103 ,false, true, 4   },
                        {39 , 104 ,false, true, 16  },
                        {50 , 105 ,false, true, 2   },
                        {38 , 106 ,false, true, 4   },
                        {50 , 107 ,false, true, 3   },
                        { 43,  108, false, true,1  },
                  });


            _migrationBuilder.InsertData(
      table: "ReportSkeletonFilters",
      columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
      values: new object[,]
      {
        { 1, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 1, "Group By Training Task Groups", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 1, "Show Specific Tasks", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TaskOptions", false, true, null, null },
        { 1, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 2, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 2, "IDP Reviewers", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "IDP Reviewers", false, true, null, null },
        { 2, "Include Inactive Employees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 2, "IDP Status", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "IDP Status", false, true, null, null },
        { 3, "Employee Active Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 3, "Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "ClassSchedule", false, true, null, null },
        { 4, "Providers", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Providers", false, true, null, null },
        { 4, "ILA Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 4, "ILAs with Approvals expiring within Months", "String", "Single", DateTime.MinValue, DateTime.MinValue, "MonthNumber", false, true, null, null },
        { 4, "Delivery Method", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "DeliveryMethods", false, true, null, null },
        { 5, "Task Group Range", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TaskGroupDescriptions", false, true, null, null },
        { 5, "Task Group Id", "Int", "Single", DateTime.MinValue, DateTime.MinValue, "TaskGroups", false, true, null, null },
        { 6, "Select Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "allclassschedule", false, true, null, null },
        { 6, "Select Form", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "studentevaluationforms", false, true, null, null },
        { 7, "Active Only/Inactive Only/All Positions", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 7, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 7, "Select Position Version", "Int", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 8, "By Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 8, "Active Only/Inactive Only/All Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 8, "Include Current Position Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 8, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 9, "Select Positions", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "Tasks Linked to Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "Enabling Objectives Linked to Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "EOs flagged as Skills Linked to Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "Meta EOs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 9, "Employee", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "All", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 9, "Select Date Range", "Date", "Range", new DateTime(2000, 1, 1), DateTime.MinValue, null, false, true, null, null },
        { 10, "By Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 10, "Active Only/Inactive Only/All Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 11, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 12, "Employee Filter", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 13, "ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 13, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 13, "Completion Type", "String", "Single", DateTime.MinValue, DateTime.MinValue, "completiontype", false, true, null, null },
        { 14, "ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 14, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 14, "Select Instructor", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "instructor", false, true, null, null },
        { 14, "Select Location", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "location", false, true, null, null },
        { 15, "ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "courses", false, true, null, null },
        { 16, "Select Task Qualification Evaluators", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "taskqualification", false, true, null, null },
        { 16, "Show Assigned and Pending Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 16, "Show Completed Task Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 17, "Select ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "SelfpacedCourses", false, true, null, null },
        { 17, "Select Form", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "StudentEval", false, true, null, null },
        { 17, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 18, "Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 18, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 18, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 18, "R-R Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 19, "Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 19, "Current Position(s) only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 19, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 19, "Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 19, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 19, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 19, "Include trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 20, "Select Test Items", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TaskListReviewStatus", false, true, "Active", null },
        { 20, "Item Type", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "testitemtypes", false, true, "1, 2, 3, 4, 5, 6", null },
        { 20, "Taxonomy Level", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "taxonomylevel", false, true, "1, 2, 3, 4, 5", null },
        { 20, "Only Test Items with No EO linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 20, "Only Test Items not linked to Test", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 21, "Tests", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Tests", false, true, null, null },
        { 21, "Show Test Completion Statistics", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 21, "Show Correct Answer", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 22, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Employees", false, true, null, null },
        { 22, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 22, "R-R Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 22, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 23, "Evaluators To Filter", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 23, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 24, "Select Form", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "StudentEval", false, true, null, null },
        { 24, "Training Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingclasses", false, true, null, null },
        { 25, "Filter by Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 25, "All Company Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 26, "Program Type", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TrainingPrograms", false, true, null, null },
        { 26, "ILA", "String", "Single", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 26, "OJT / Task Qualification", "String", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 26, "Open Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 27, "Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 27, "Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 28, "Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 28, "Show RR Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 28, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 29, "Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 29, "Show RR Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 30, "ILA", "String", "Single", DateTime.MinValue, DateTime.MinValue, "courses", false, true, null, null },
        { 30, "Show RR Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 30, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 31, "Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 31, "Only R-R* for any of the Positions", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 31, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 31, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "true", null },
        { 32, "Task", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 33, "ILA", "String", "Single", DateTime.MinValue, DateTime.MinValue, "courses", false, true, null, null },
        { 33, "Only R-R* for any of the Positions", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 33, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 33, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "true", null },
        { 34, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 34, "Procedures", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "procedures", false, true, null, null },
        { 34, "Employee Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 34, "Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 34, "Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 34, "Completion Type", "String", "Single", DateTime.MinValue, DateTime.MinValue, "CompletionType", false, true, null, null },
        { 35, "Select Training Program Review", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TrainingProgramReviews", false, true, null, null },
        { 36, "Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 37, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 37, "Select Objectives to Include", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "enablingobjectivetypes", false, true, "listAllSelected", null },
        { 38, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 38, "Select Training Program", "String", "Single", DateTime.MinValue, DateTime.MinValue, "initialtrainingprograms", false, true, null, null },
        { 38, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 39, "Training Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "ClassRosterSchedule", false, true, null, null },
        { 40, "Task List Reviews", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "TaskListReview", false, true, null, null },
        { 40, "Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TaskListReviewStatus", false, true, "All", null },
        { 41, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 41, "Summary Report", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 41, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 42, "ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 42, "Include Task and EO Filter", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "TaskEoIncludeOption", false, true, "1, 2, 3", null },
        { 43, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 43, "Include Evaluator and Mode of Qualification", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 43, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 43, "Print All Completed Tasks First", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 44, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 44, "Exclude Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 44, "Include Task Modification Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 44, "Include *R-R Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 44, "All Tasks Changed Since", "Date", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 45, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 45, "Show Training Resources", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 45, "Include ILAs that cover the EO associated with the Task", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 46, "Select Training Program", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingprogramtype", false, true, null, null },
        { 46, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 47, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 47, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 48, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 48, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 48, "Training Module", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingmoduleoption", false, true, null, null },
        { 48, "Include Inactive Employees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 48, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 49, "Training Module", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingmodule", false, true, null, null },
        { 49, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 49, "Training Module Option", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingmoduleoption", false, true, null, null },
        { 49, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 49, "Include Inactive Employee", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 50, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 50, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 50, "Select Training Program", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingprogramtype", false, true, null, null },
        { 51, "Training Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "ClassSignInSchedule", false, true, null, null },
        { 53, "Select DIF Survey", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "DifSurvey", false, true, null, null },
        { 54, "Select Survey", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "DifSurvey", false, true, null, null },
        { 54, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 55, "Select Survey", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "DifSurvey", false, true, null, null },
        { 55, "Task Active Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 55, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 55, "Training Frequency", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingfrequency", false, true, "0", null },
        { 56, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 56, "Select Year", "String", "Single", DateTime.MinValue, DateTime.MinValue, "YearFilter", false, true, null, null },
        { 57, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 57, "Select Certificates & Training Requirements", "String", "Single", DateTime.MinValue, DateTime.MinValue, "certifications", false, true, null, null },
        { 58, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 58, "Include Scheduled ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 58, "Include NERC Provider ILAs only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 59, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 60, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 61, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 61, "Published Procedure Reviews", "String", "Single", DateTime.MinValue, DateTime.MinValue, "procedurereviewstatus", false, true, "Published", null },
        { 61, "Completion Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 61, "Completion Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "completionstatus", false, true, "Completed", null },
        { 62, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 62, "Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 62, "Current Positions only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 62, "Reliability-Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 62, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 62, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 62, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 63, "Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 63, "Current Positions Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 63, "Reliability Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 63, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 63, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 63, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 64, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 65, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "taskr5impacted", false, true, null, null },
        { 66, "Select Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "allclassschedule", false, true, null, null },
        { 66, "Select Test Type", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tests", false, true, null, null },
        { 66, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 66, "Include Test Answer Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 66, "Show only Failed Grades", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 67, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 68, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 68, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 68, "Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 68, "Task Qualification Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "onlycompletionstatus", false, true, "All", null },
        { 68, "Evaluator Completion Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "onlycompletionstatus", false, true, "All", null },
        { 69, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 69, "Select Year", "String", "Single", DateTime.MinValue, DateTime.MinValue, "YearFilter", false, true, null, null },
        { 69, "Active/Inactive/All ILAs", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 69, "ILA Completion Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "completedstatus", false, true, "All", null },
        { 70, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 70, "Select Year", "String", "Single", DateTime.MinValue, DateTime.MinValue, "YearFilter", false, true, null, null },
        { 70, "Active/Inactive/All ILAs", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 70, "ILA Completion Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "completedstatus", false, true, "All", null },
        { 71, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 71, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 72, "Select Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 72, "Select Procedures", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "procedures", false, true, null, null },
        { 72, "Select Regulatory Requirements", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "regulatoryrequirement", false, true, null, null },
        { 72, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 73, "Select Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 73, "Active Only/Inactive Only/All Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 73, "Current Position(s) Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 73, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 73, "Select up to 10 Procedures", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "procedures", false, true, null, 10 },
        { 73, "Select up to 10 Regulatory Requirements", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "regulatoryrequirement", false, true, null, 10 },
        { 73, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 74, "Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 74, "Select Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 74, "Reliability Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 74, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 74, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 74, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 74, "Include Task Qualification Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 75, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 75, "RR Tasks only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 75, "Active Only/Inactive Only/All Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 75, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 75, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 76, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 76, "Select Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 76, "Reliability Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 76, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 76, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 76, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 76, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 77, "Select Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "classsigninschedule", false, true, null, null },
        { 77, "Select Test Type", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tests", false, true, null, null },
        { 78, "Select Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "classschedulewithilaproviderlocation", false, true, null, null },
        { 78, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 78, "Include Test Answer Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 78, "Show only Failed Grades", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 79, "Select Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "classschedulewithilaproviderlocation", false, true, null, null },
        { 80, "Select Employee", "String", "Single", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 80, "Select Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 80, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 80, "R-R Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 80, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 81, "Issuing Authority", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "issuingauthority", false, true, null, null },
        { 81, "Include Inactive Procedures", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 82, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 82, "Include Inactive Procedures", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 83, "All/Active/Inactive Enabling Objectives", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 83, "Include Meta EOs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 83, "Include Skill Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 84, "All/Active/Inactive Enabling Objectives", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 84, "Include Meta EOs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 84, "Include Skill Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 85, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 86, "Select Enabling Objective Categories", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "enablingobjectivecategories", false, true, null, null },
        { 86, "Enabling Objective Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 86, "Show Meta EOs Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 86, "Show Skill Qualifications Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 86, "Show Category, Sub-Category, And Topic Labels only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 87, "Select Test", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tests", false, true, null, null },
        { 87, "Show Correct Answer", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 88, "Select Enabling Objective", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectenablingobjective", false, true, null, null },
        { 88, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 88, "Only show EOs with Tasks Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 89, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 89, "Include Tasks With No ILAs Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 90, "Select Enabling Objectives", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectenablingobjective", false, true, null, null },
        { 90, "Include Enabling Objectives With No ILAs Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 91, "Select Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 91, "Include Positions With No ILAs Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 92, "Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 92, "Select Skill Qualification", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectskillqualification", false, true, null, null },
        { 92, "Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "onlyactiveinactivestatus", false, true, "Active", null },
        { 92, "Include Employee Name and Certificate No", "String", "Single", DateTime.MinValue, DateTime.MinValue, "employeewithnerccertification", false, true, null, null },
        { 93, "Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 93, "Select Skill Qualification", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectskillqualification", false, true, null, null },
        { 93, "Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "onlyactiveinactivestatus", false, true, "Active", null },
        { 93, "Include Employee Name and Certificate No", "String", "Single", DateTime.MinValue, DateTime.MinValue, "employeewithnerccertification", false, true, null, null },
        { 94, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, 10 },
        { 94, "Include Inactive Safety Hazards", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 95, "Safety Hazard Category", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "safetyhazardscategories", false, true, null, null },
        { 95, "Include Safety Hazard Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 95, "Include Inactive Safety Hazards", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 96, "Select Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "ClassSignInSchedule", false, true, null, null },
        { 96, "Print for all registered students before grade is awarded", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 96, "Include Failed Students", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 97, "Select Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "classseswithilastatusproviderlocation", false, true, null, null },
        { 97, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 97, "Include Test Items Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 97, "Show only Failed Pretest Grades", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 98, "Select Certificate", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectcertificate", false, true, null, null },
        { 98, "Filter by Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 98, "All Company Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 99, "Active/Inactive/All Tasks", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 99, "Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 99, "Include Meta Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 99, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 100, "Active/Inactive/All Tasks", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 100, "Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 100, "Include Meta Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 100, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 101, "Procedure", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "procedures", false, true, null, null },
        { 101, "Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 101, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 102, "Select Meta ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "metaila", false, true, null, null },
        { 102, "Include Objectives linked to ILAs", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "objectivelinkedtoila", false, true, null, null },
        { 103, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, 10 },
        { 103, "Reliability Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 103, "Include Meta Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 103, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 103, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 104, "Select ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 104, "Test Type", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "testtype", false, true, "listAllSelected", null },
        { 104, "Status", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "teststatus", false, true, "listAllSelected", null },
        { 105, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 105, "Include Safety Hazard Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 105, "Include Inactive Safety Hazards", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 106, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, 10 },
        { 106, "Include Meta Enabling Objectives", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 106, "Include Skill Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 106, "Include Inactive Enabling Objectives", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 107, "Safety Hazards", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "safetyhazard", false, true, null, null },
        { 107, "Include Safety Hazard Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 107, "Include Meta Enabling Objectives", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 107, "Include Skill Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 107, "Include Inactive Enabling Objectives", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 108, "Select ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectilas", false, true, null, null }
      }
  );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
                {
                                            { 1, "Include Enabling Objectives", false, true },
                         { 1, "Positions Description", false, true },
                         { 1, "R-R Definition", false, true },
                         { 1, "Review Date", false, true },
                         { 1, "Safety Hazards", false, true },
                         { 1, "Task Details", false, true },
                         { 1, "Task Steps & Sub-Steps", false, true },
                         { 1, "Tools", false, true },
                         { 2, "Completed Date", false, true },
                         { 2, "Completion Status", false, true },
                         { 2, "IDP Review Response", false, true },
                         { 2, "Position Abbreviation", false, true },
                         { 2, "Release Date", false, true },
                         { 2, "Employee Name", false, true },
                         { 4, "Approval Date", false, true },
                         { 4, "ILA Application Date", false, true },
                         { 4, "ILA Number", false, true },
                         { 4, "ILA Title", false, true },
                         { 4, "Nerc - EOPs", false, true },
                         { 4, "Operational Topic", false, true },
                         { 4, "Other", false, true },
                         { 4, "Partial Credit?", false, true },
                         { 4, "Per-005 EOPs", false, true },
                         { 4, "Regional 2", false, true },
                         { 4, "Regional", false, true },
                         { 4, "Self-paced ?", false, true },
                         { 4, "Simulator", false, true },
                         { 4, "Standards", false, true },
                         { 4, "Total CEHs", false, true },
                         { 4, "Total", false, true },
                         { 5, "Duty Area", false, true },
                         { 5, "SubDutyArea", false, true },
                         { 5, "TaskDescription", false, true },
                         { 5, "TaskId", false, true },
                         { 5, "Topic", false, true },
                         { 6, "Class End Date & Time", false, true },
                         { 6, "Class Start Date & Time", false, true },
                         { 6, "Evaluation Rating Scale", false, true },
                         { 6, "Form Name", false, true },
                         { 6, "ILA Name", false, true },
                         { 6, "ILA Number", false, true },
                         { 6, "Include General Comments Section", false, true },
                         { 6, "Instructor", false, true },
                         { 6, "Location", false, true },
                         { 6, "Provider", false, true },
                         { 7, "Effective Date", false, true },
                         { 7, "Note", false, true },
                         { 7, "Position Abbreviation", false, true },
                         { 7, "Position Description", false, true },
                         { 8, "Employee Email Address", false, true },
                         { 8, "Employee Notes", false, true },
                         { 8, "Employee Number", false, true },
                         { 8, "NERC Cert #", false, true },
                         { 8, "NERC Cert Expiration Date", false, true },
                         { 8, "NERC Cert Issue Date", false, true },
                         { 8, "NERC Cert Recert Date", false, true },
                         { 8, "NERC Cert Type", false, true },
                         { 8, "Position Qualification Date", false, true },
                         { 8, "Task Qualification Evaluator", false, true },
                         { 9, "Position Qualification Date", false, true },
                         { 9, "Start Date", false, true },
                         { 9, "Trainee Status", false, true },
                         { 10, "Email Address", false, true },
                         { 10, "Employee Notes", false, true },
                         { 10, "Employee Number", false, true },
                         { 10, "NERC Cert #", false, true },
                         { 10, "NERC Cert Expiration Date", false, true },
                         { 10, "NERC Cert Issue Date", false, true },
                         { 10, "NERC Cert Recert Date", false, true },
                         { 10, "NERC Cert Type", false, true },
                         { 10, "Position", false, true },
                         { 10, "Task Qualification Evaluator", false, true },
                         { 11, "Current Positions", false, true },
                         { 11, "Employee", false, true },
                         { 11, "End Date", false, true },
                         { 11, "Organization", false, true },
                         { 11, "Positions", false, true },
                         { 11, "Qualification Date", false, true },
                         { 11, "Start Date", false, true },
                         { 12, "Cert Area", false, true },
                         { 12, "Certification #", false, true },
                         { 12, "Employee", false, true },
                         { 12, "Expiration Date", false, true },
                         { 12, "Issue Date", false, true },
                         { 12, "Organization", false, true },
                         { 12, "Positions", false, true },
                         { 12, "Renewal Date", false, true },
                         { 13, "Certification Area", false, true },
                         { 13, "Course Location", false, true },
                         { 13, "Employee ID", false, true },
                         { 13, "Employee", false, true },
                         { 13, "Grade", false, true },
                         { 13, "ILA ID", false, true },
                         { 13, "ILA Provider", false, true },
                         { 13, "ILA Title", false, true },
                         { 13, "Instructor", false, true },
                         { 13, "NERC Cert #", false, true },
                         { 13, "Organization", false, true },
                         { 13, "Positions", false, true },
                         { 13, "Score", false, true },
                         { 14, "Class End Date", false, true },
                         { 14, "Class Start Date", false, true },
                         { 14, "ILA", false, true },
                         { 14, "Instructor", false, true },
                         { 14, "Location", false, true },
                         { 14, "Prov. ID", false, true },
                         { 14, "Year", false, true },
                         { 15, "ILA Description", false, true },
                         { 15, "ILA Evaluation Methods", false, true },
                         { 15, "ILA Prerequisites", false, true },
                         { 15, "ILA Procedures", false, true },
                         { 15, "ILA Segment and Content", false, true },
                         { 15, "ILA Training Plan", false, true },
                         { 15, "ILA Training Resources", false, true },
                         { 15, "List of Objectives", false, true },
                         { 15, "Position", false, true },
                         { 15, "Regulatory Requirements", false, true },
                         { 15, "Safety Hazards", false, true },
                         { 15, "Topic", false, true },
                         { 15, "Use Sequenced List", false, true },
                         { 16, "Evaluator Name", false, true },
                         { 16, "Number of Pending Qualifications", false, true },
                         { 16, "Organization", false, true },
                         { 16, "Position Abbreviation", false, true },
                         { 17, "Date Range", false, true },
                         { 17, "Delivery Method", false, true },
                         { 17, "Evaluation Comments", false, true },
                         { 17, "Evaluation Rating Scale", false, true },
                         { 17, "Form Name", false, true },
                         { 17, "ILA", false, true },
                         { 17, "Provider", false, true },
                         { 18, "Employee", false, true },
                         { 18, "ILA Name/ID", false, true },
                         { 18, "Positions Qualification Date", false, true },
                         { 18, "Requalification Date", false, true },
                         { 18, "Requalification Status", false, true },
                         { 19, "Comments", false, true },
                         { 19, "Date", false, true },
                         { 19, "Evaluation Method", false, true },
                         { 19, "Evaluator", false, true },
                         { 19, "Met Criteria", false, true },
                         { 19, "Task Description", false, true },
                         { 19, "Task Number", false, true },
                         { 20, "EO #", false, true },
                         { 20, "EO Description", false, true },
                         { 20, "Item ID", false, true },
                         { 20, "Item Stem", false, true },
                         { 20, "Item Type", false, true },
                         { 20, "Status", false, true },
                         { 20, "Taxonomy Level", false, true },
                         { 21, "ILA Number", false, true },
                         { 21, "ILA Title", false, true },
                         { 21, "Provider", false, true },
                         { 21, "Status", false, true },
                         { 21, "Test Title", false, true },
                         { 21, "Test Type", false, true },
                         { 22, "ILA ID", false, true },
                         { 22, "Position", false, true },
                         { 22, "Qualification Date", false, true },
                         { 22, "Requalification Date", false, true },
                         { 22, "Requalification Status", false, true },
                         { 22, "Task Description", false, true },
                         { 22, "Task Number", false, true },
                         { 22, "Task Revision Date", false, true },
                         { 23, "Employee Name", false, true },
                         { 23, "Organizations", false, true },
                         { 23, "Positions", false, true },
                         { 23, "Show Assigned Task Qualifications", false, true },
                         { 24, "Class End Date & Time", false, true },
                         { 24, "Class Start Date & Time", false, true },
                         { 24, "Evaluation Comments", false, true },
                         { 24, "Form Name", false, true },
                         { 24, "ILA", false, true },
                         { 24, "Instructor", false, true },
                         { 24, "Location", false, true },
                         { 24, "Provider", false, true },
                         { 25, "Cert Area", false, true },
                         { 25, "Employee Name", false, true },
                         { 25, "Expiration Date", false, true },
                         { 25, "NERC Cert #", false, true },
                         { 25, "Organization", false, true },
                         { 25, "Position", false, true },
                         { 25, "Recert Date", false, true },
                         { 26, "Component Type", false, true },
                         { 26, "Issue Date", false, true },
                         { 26, "Issue ID", false, true },
                         { 26, "Issue Type", false, true },
                         { 26, "Severity", false, true },
                         { 26, "Status", false, true },
                         { 27, "Cert Area", false, true },
                         { 27, "Cert Date", false, true },
                         { 27, "Cert Number", false, true },
                         { 27, "Employee Name / Number", false, true },
                         { 27, "Met Annual Requirement", false, true },
                         { 27, "NERC Standards", false, true },
                         { 27, "Other", false, true },
                         { 27, "Prof", false, true },
                         { 27, "Regional - Met Annual Requirement", false, true },
                         { 27, "Regional 2", false, true },
                         { 27, "Regional", false, true },
                         { 27, "Simulation Hours", false, true },
                         { 27, "Total CEHs", false, true },
                         { 27, "Total Training Hours", false, true },
                         { 28, "Cover Sheet", false, true },
                         { 28, "Include Pseudo Tasks", false, true },
                         { 28, "Include Safety Hazards", false, true },
                         { 28, "Position", false, true },
                         { 28, "Show EO linked to the Tasks", false, true },
                         { 28, "Show Procedures linked to the Tasks", false, true },
                         { 28, "Show Task Details", false, true },
                         { 28, "Show Task Questions and Answers", false, true },
                         { 28, "Show Task Questions Only", false, true },
                         { 28, "Show Task Specific Suggestions", false, true },
                         { 28, "Sub-steps", false, true },
                         { 29, "Cover Sheet", false, true },
                         { 29, "Include Pseudo Tasks", false, true },
                         { 29, "Include Safety Hazards", false, true },
                         { 29, "Show EO linked to the Tasks", false, true },
                         { 29, "Show Procedures linked to the Tasks", false, true },
                         { 29, "Show Task Details", false, true },
                         { 29, "Show Task Questions and Answers", false, true },
                         { 29, "Show Task Questions Only", false, true },
                         { 29, "Show Task Specific Suggestions", false, true },
                         { 29, "Sub-steps", false, true },
                         { 30, "Cover Sheet", false, true },
                         { 30, "ILA", false, true },
                         { 30, "Include Pseudo Tasks", false, true },
                         { 30, "Include Safety Hazards", false, true },
                         { 30, "Show EO linked to the Tasks", false, true },
                         { 30, "Show Procedures linked to the Tasks", false, true },
                         { 30, "Show Task Details", false, true },
                         { 30, "Show Task Questions and Answers", false, true },
                         { 30, "Show Task Questions Only", false, true },
                         { 30, "Show Task Specific Suggestions", false, true },
                         { 30, "Sub-steps", false, true },
                         { 31, "EOs linked to Task", false, true },
                         { 31, "Positions", false, true },
                         { 31, "Procedures linked to Task", false, true },
                         { 31, "Safety Hazards", false, true },
                         { 31, "Show Task Questions and Answers", false, true },
                         { 31, "Show Task Questions Only", false, true },
                         { 31, "Show Task Specific Suggestions", false, true },
                         { 31, "Sub-steps", false, true },
                         { 31, "Task Details", false, true },
                         { 31, "Task Groups", false, true },
                         { 32, "EOs linked to Task", false, true },
                         { 32, "Positions", false, true },
                         { 32, "Procedures linked to Task", false, true },
                         { 32, "Safety Hazards", false, true },
                         { 32, "Show Task Questions and Answers", false, true },
                         { 32, "Show Task Questions Only", false, true },
                         { 32, "Show Task Specific Suggestions", false, true },
                         { 32, "Sub-steps", false, true },
                         { 32, "Task Details", false, true },
                         { 32, "Task Groups", false, true },
                         { 33, "EOs linked to Task", false, true },
                         { 33, "Procedures linked to Task", false, true },
                         { 33, "Safety Hazards", false, true },
                         { 33, "Show Task Questions and Answers", false, true },
                         { 33, "Show Task Questions Only", false, true },
                         { 33, "Show Task Specific Suggestions", false, true },
                         { 33, "Sub-steps", false, true },
                         { 33, "Task Details", false, true },
                         { 34, "Comments", false, true },
                         { 34, "Completed Date", false, true },
                         { 34, "Employee Name", false, true },
                         { 34, "Issuing Authority", false, true },
                         { 34, "Organizations", false, true },
                         { 34, "Positions Abbreviation", false, true },
                         { 34, "Positions", false, true },
                         { 34, "Procedure number", false, true },
                         { 34, "Procedure Review Response", false, true },
                         { 34, "Procedure Title", false, true },
                         { 34, "Release Date", false, true },
                         { 34, "Status", false, true },
                         { 35, "Conclusions", false, true },
                         { 35, "Description of Evaluation", false, true },
                         { 35, "Historical Background", false, true },
                         { 35, "Results and Recommendations", false, true },
                         { 35, "Summary of Evaluation", false, true },
                         { 35, "Supporting Documents", false, true },
                         { 38, "Completed", false, true },
                         { 38, "Grade", false, true },
                         { 38, "ILA#", false, true },
                         { 38, "Individual Learning Activity Title", false, true },
                         { 38, "Planned", false, true },
                         { 38, "Scheduled", false, true },
                         { 38, "Score", false, true },
                         { 39, "Email", false, true },
                         { 39, "Emp #", false, true },
                         { 39, "Employee Name - Details", false, true },
                         { 39, "Grade", false, true },
                         { 39, "Phone", false, true },
                         { 39, "Score", false, true },
                         { 40, "Action Items", false, true },
                         { 40, "History Date", false, true },
                         { 40, "Requal Reqd.", false, true },
                         { 40, "Review Date", false, true },
                         { 40, "Review Findings", false, true },
                         { 40, "Reviewed By", false, true },
                         { 40, "Signature/Conclusion", false, true },
                         { 40, "Task #", false, true },
                         { 40, "Task History", false, true },
                         { 40, "Task Statement", false, true },
                         { 43, "Date", false, true },
                         { 43, "Evaluation Method", false, true },
                         { 43, "Evaluator", false, true },
                         { 43, "ILA Id/Name", false, true },
                         { 43, "Task #", false, true },
                         { 43, "Task Description", false, true },
                         { 46, "Individual Learning Activity Title", false, true },
                         { 46, "NERC CEHs-Sim", false, true },
                         { 46, "NERC CEHs-Stand", false, true },
                         { 46, "NERC CEHs-Total", false, true },
                         { 46, "Other", false, true },
                         { 46, "PER-005-EOPs", false, true },
                         { 46, "Prof", false, true },
                         { 46, "Provider ID/Individual Learning A", false, true },
                         { 46, "Reg", false, true },
                         { 46, "Total", false, true },
                         { 47, "Completed", false, true },
                         { 47, "Grade", false, true },
                         { 47, "Individual Learning Activity T", false, true },
                         { 47, "Location/Instructor (Proctor if self-paced)", false, true },
                         { 47, "NERC CEHs* - Sim", false, true },
                         { 47, "NERC CEHs* - Stand", false, true },
                         { 47, "NERC CEHs* - Total", false, true },
                         { 47, "Other", false, true },
                         { 47, "PER-005-EOPS", false, true },
                         { 47, "PER-005-inc.sim.", false, true },
                         { 47, "Provider ID / ILA#", false, true },
                         { 47, "Reg", false, true },
                         { 47, "Reg2", false, true },
                         { 47, "Score", false, true },
                         { 47, "Total", false, true },
                         { 48, "Completed Date", false, true },
                         { 48, "Grade", false, true },
                         { 48, "ILA Title", false, true },
                         { 48, "Location", false, true },
                         { 48, "Scheduled Date", false, true },
                         { 48, "Score", false, true },
                         { 49, "Completed Date", false, true },
                         { 49, "Employee", false, true },
                         { 49, "Grade", false, true },
                         { 49, "Location", false, true },
                         { 49, "Scheduled Date", false, true },
                         { 49, "Score", false, true },
                         { 50, "Completed", false, true },
                         { 50, "Employee/ID", false, true },
                         { 50, "Grade", false, true },
                         { 50, "Individual Learning Activity Title", false, true },
                         { 50, "Instructor", false, true },
                         { 50, "Location", false, true },
                         { 50, "NERC CEHs-Sim", false, true },
                         { 50, "NERC CEHs-Stand", false, true },
                         { 50, "NERC CEHs-Total", false, true },
                         { 50, "NERC Cert.#", false, true },
                         { 50, "Other", false, true },
                         { 50, "PER-005-EOPs", false, true },
                         { 50, "Prof", false, true },
                         { 50, "Provider ID/Individual Learning A", false, true },
                         { 50, "Reg", false, true },
                         { 50, "Reg. Cert.#", false, true },
                         { 50, "Score", false, true },
                         { 50, "Score", false, true },
                         { 50, "Total", false, true },
                         { 51, "Date", false, true },
                         { 51, "Email", false, true },
                         { 51, "Emp #", false, true },
                         { 51, "Employee Name - Details", false, true },
                         { 51, "Phone", false, true },
                         { 51, "Signature", false, true },
                         { 53, "Include Survey Instructions", false, true },
                         { 54, "Additional Comments", false, true },
                         { 54, "Task Comments", false, true },
                         { 55, "Additional Comments", false, true },
                         { 55, "Override Status", false, true },
                         { 55, "Recommended Training", false, true },
                         { 55, "Task Comments", false, true },
                         { 55, "Training Frequency", false, true },
                         { 56, "Employee Name", false, true },
                         { 56, "Organization", false, true },
                         { 56, "Position Abbreviation", false, true },
                         { 56, "Position", false, true },
                         { 57, "Certificate Name", false, true },
                         { 57, "Certificate Number", false, true },
                         { 57, "Completed YTD", false, true },
                         { 57, "Employee Name", false, true },
                         { 57, "Organization", false, true },
                         { 57, "Planned for Current Year", false, true },
                         { 57, "Position Abbreviation", false, true },
                         { 57, "Scheduled to Complete YTD", false, true },
                         { 57, "Still to Complete YTD", false, true },
                         { 58, "Cert Area", false, true },
                         { 58, "Completed Date", false, true },
                         { 58, "Employee Name", false, true },
                         { 58, "Expiration Date", false, true },
                         { 58, "ILA #", false, true },
                         { 58, "ILA Title", false, true },
                         { 58, "NERC Cert #", false, true },
                         { 58, "Position", false, true },
                         { 58, "Recertification Date", false, true },
                         { 59, "Cert #", false, true },
                         { 59, "Cert Area", false, true },
                         { 59, "Completed Date", false, true },
                         { 59, "Employee Name", false, true },
                         { 59, "Expiration Date", false, true },
                         { 59, "ILA #", false, true },
                         { 59, "ILA Title", false, true },
                         { 59, "Position", false, true },
                         { 59, "Recertification Date", false, true },
                         { 60, "Cert #", false, true },
                         { 60, "Cert Area", false, true },
                         { 60, "Employee Name", false, true },
                         { 60, "Expiration Date", false, true },
                         { 60, "Position Abbreviation", false, true },
                         { 61, "Comments", false, true },
                         { 61, "Completed Date", false, true },
                         { 61, "Issuing Authority", false, true },
                         { 61, "Organization", false, true },
                         { 61, "Position", false, true },
                         { 61, "Procedure Number", false, true },
                         { 61, "Procedure Review Response", false, true },
                         { 61, "Procedure Review Title", false, true },
                         { 61, "Procedure Title", false, true },
                         { 61, "Release Date", false, true },
                         { 61, "Status", false, true },
                         { 62, "Completed Tasks", false, true },
                         { 62, "Employee", false, true },
                         { 62, "Position", false, true },
                         { 62, "Total Tasks", false, true },
                         { 63, "Completed Tasks", false, true },
                         { 63, "Employee", false, true },
                         { 63, "Position", false, true },
                         { 63, "Total Tasks", false, true },
                         { 64, "Effective Date", false, true },
                         { 64, "Employee Name", false, true },
                         { 64, "Employee Number", false, true },
                         { 64, "Employee Status", false, true },
                         { 64, "Organizations", false, true },
                         { 64, "Positions", false, true },
                         { 64, "Reason for Status Change", false, true },
                         { 65, "Linked Task #", false, true },
                         { 65, "Linked Task Description", false, true },
                         { 65, "Linked Tasks Positions", false, true },
                         { 65, "Positions", false, true },
                         { 66, "Class End DateTime", false, true },
                         { 66, "Class Start DateTime", false, true },
                         { 66, "Completed Date", false, true },
                         { 66, "Completion Statistics Graph", false, true },
                         { 66, "Correct/Incorrect Answer", false, true },
                         { 66, "Cut Score", false, true },
                         { 66, "Disclaimer", false, true },
                         { 66, "Employee Name", false, true },
                         { 66, "ILA Number", false, true },
                         { 66, "ILA Title", false, true },
                         { 66, "Interrupted", false, true },
                         { 66, "Organization", false, true },
                         { 66, "Position", false, true },
                         { 66, "Release Date", false, true },
                         { 66, "Restarted", false, true },
                         { 66, "Submitted Response", false, true },
                         { 66, "Test Grade", false, true },
                         { 66, "Test Item", false, true },
                         { 66, "Test Score", false, true },
                         { 66, "Test Time", false, true },
                         { 66, "Test Title", false, true },
                         { 66, "Test Type", false, true },
                         { 67, "Employee Name", false, true },
                         { 67, "Employee Number", false, true },
                         { 67, "Positions", false, true },
                         { 67, "Show Certificate Details", false, true },
                         { 67, "Show Employee Certification Details", false, true },
                         { 68, "Comments", false, true },
                         { 68, "Date", false, true },
                         { 68, "Evaluation Method", false, true },
                         { 68, "Evaluator", false, true },
                         { 68, "Met Criteria", false, true },
                         { 68, "Position", false, true },
                         { 68, "Step #", false, true },
                         { 68, "Step/Sub-Step", false, true },
                         { 68, "Step Comments", false, true },
                         { 68, "Step Evaluator", false, true },
                         { 68, "Step Met", false, true },
                         { 68, "Step Qualification Date", false, true },
                         { 68, "Task Description", false, true },
                         { 68, "Task Number", false, true },
                         { 69, "Completed Date", false, true },
                         { 69, "Delivery Method", false, true },
                         { 69, "Employee Name", false, true },
                         { 69, "Grade Notes", false, true },
                         { 69, "Grade", false, true },
                         { 69, "IDP Specific Information", false, true },
                         { 69, "ILA Number", false, true },
                         { 69, "ILA Title", false, true },
                         { 69, "Organization", false, true },
                         { 69, "Planned Date", false, true },
                         { 69, "Position", false, true },
                         { 69, "Scheduled Date", false, true },
                         { 69, "Score", false, true },
                         { 69, "Self-Paced", false, true },
                         { 70, "Certificate Information", false, true },
                         { 70, "Completed Date", false, true },
                         { 70, "Employee Name", false, true },
                         { 70, "Grade Notes", false, true },
                         { 70, "Grade", false, true },
                         { 70, "IDP Specific Information", false, true },
                         { 70, "ILA Number", false, true },
                         { 70, "ILA Title", false, true },
                         { 70, "Organization", false, true },
                         { 70, "Percent Completed per IDP", false, true },
                         { 70, "Position", false, true },
                         { 70, "Scheduled Date", false, true },
                         { 70, "Score", false, true },
                         { 70, "Total Training Hours", false, true },
                         { 71, "History Date", false, true },
                         { 71, "Notes", false, true },
                         { 71, "R-R", false, true },
                         { 71, "Requalification Due Date", false, true },
                         { 71, "Requalification Required", false, true },
                         { 71, "Revision Summary", false, true },
                         { 71, "Status (Active/Inactive)", false, true },
                         { 71, "Task #", false, true },
                         { 71, "Task Description", false, true },
                         { 71, "Username", false, true },
                         { 72, "Completion Date", false, true },
                         { 72, "Employee Name", false, true },
                         { 72, "Employee Number", false, true },
                         { 72, "Grade notes", false, true },
                         { 72, "Grade", false, true },
                         { 72, "ILA #", false, true },
                         { 72, "ILA Title", false, true },
                         { 72, "Organization", false, true },
                         { 72, "Position", false, true },
                         { 72, "Procedure/Regulatory requirement #", false, true },
                         { 72, "Procedure/Regulatory Requirement Title", false, true },
                         { 72, "Score", false, true },
                         { 73, "Employee Name", false, true },
                         { 73, "Employee Number", false, true },
                         { 73, "Position", false, true },
                         { 74, "Completed Tasks", false, true },
                         { 74, "Employee Name", false, true },
                         { 74, "Evaluation Method", false, true },
                         { 74, "Evaluator", false, true },
                         { 74, "Organization", false, true },
                         { 74, "Position", false, true },
                         { 74, "Total Tasks", false, true },
                         { 75, "Duty Area", false, true },
                         { 75, "Employee Name", false, true },
                         { 75, "Employee Status", false, true },
                         { 75, "Last Task Qualification Date", false, true },
                         { 75, "Sub-Duty Area", false, true },
                         { 75, "Task Description", false, true },
                         { 75, "Task Number", false, true },
                         { 76, "Comments", false, true },
                         { 76, "Date", false, true },
                         { 76, "Evaluation Method", false, true },
                         { 76, "Evaluator", false, true },
                         { 76, "Met Criteria", false, true },
                         { 76, "Task Description", false, true },
                         { 76, "Task Number", false, true },
                         { 77, "Class End DateTime", false, true },
                         { 77, "Class Start DateTime", false, true },
                         { 77, "Cut Score", false, true },
                         { 77, "Employee Name", false, true },
                         { 77, "Enabling Objective #", false, true },
                         { 77, "ILA Number", false, true },
                         { 77, "ILA Title", false, true },
                         { 77, "# Selected Answer", false, true },
                         { 77, "Taxonomy Level", false, true },
                         { 77, "Test Item #", false, true },
                         { 77, "Test Response Bar Chart", false, true },
                         { 77, "Test Title", false, true },
                         { 77, "Test Type", false, true },
                         { 78, "CBT Completion Graph", false, true },
                         { 78, "CBT Learning Instructions", false, true },
                         { 78, "Class Start and End Date", false, true },
                         { 78, "Class Start and End Time", false, true },
                         { 78, "Completed Date", false, true },
                         { 78, "Correct/Incorrect Answer", false, true },
                         { 78, "Employee Name", false, true },
                         { 78, "ILA Number", false, true },
                         { 78, "ILA Title", false, true },
                         { 78, "Last Access Date", false, true },
                         { 78, "Organization", false, true },
                         { 78, "Position", false, true },
                         { 78, "Release Date", false, true },
                         { 78, "Status", false, true },
                         { 78, "Submitted Response", false, true },
                         { 78, "Test Grade", false, true },
                         { 78, "Test Item", false, true },
                         { 78, "Test Score", false, true },
                         { 78, "Total Time", false, true },
                         { 79, "CBT Test Response Bar Chart", false, true },
                         { 79, "Class Start and End Date", false, true },
                         { 79, "Class Start and End Time", false, true },
                         { 79, "Employee Name", false, true },
                         { 79, "ILA Number", false, true },
                         { 79, "ILA Title", false, true },
                         { 79, "# Selected Answer", false, true },
                         { 80, "Position Description", false, true },
                         { 80, "R-R Definition", false, true },
                         { 80, "Review Date", false, true },
                         { 80, "Show EO linked to the Tasks", false, true },
                         { 80, "Show Procedures linked to the Tasks", false, true },
                         { 80, "Show Safety Hazards linked to the Tasks", false, true },
                         { 80, "Show Task Details", false, true },
                         { 80, "Show Task Questions and Answers", false, true },
                         { 80, "Show Task Questions Only", false, true },
                         { 80, "Show Task Specific Suggestions", false, true },
                         { 80, "Steps/Sub-Steps", false, true },
                         { 81, "Effective Date", false, true },
                         { 81, "Hyperlink Linked", false, true },
                         { 81, "PDF linked", false, true },
                         { 81, "Procedure Number", false, true },
                         { 81, "Procedure Title", false, true },
                         { 81, "Revision Number", false, true },
                         { 82, "Duty Area", false, true },
                         { 82, "Procedure Number", false, true },
                         { 82, "Procedure Title", false, true },
                         { 82, "Sub-Duty Area", false, true },
                         { 83, "Category", false, true },
                         { 83, "Sub-Category", false, true },
                         { 83, "Topic", false, true },
                         { 84, "Category", false, true },
                         { 84, "Sub-Category", false, true },
                         { 84, "Topic", false, true },
                         { 85, "Enabling Objectives", false, true },
                         { 85, "Procedures", false, true },
                         { 85, "Regulatory Requirements", false, true },
                         { 85, "Safety Hazards", false, true },
                         { 85, "Task Details", false, true },
                         { 85, "Task Steps & Sub-Steps", false, true },
                         { 85, "Tools", false, true },
                         { 86, "Category", false, true },
                         { 86, "Show EOs linked to Meta EO", false, true },
                         { 86, "Show Meta label", false, true },
                         { 86, "Show Skill Qualification label", false, true },
                         { 86, "Sub-Category", false, true },
                         { 86, "Topic", false, true },
                         { 87, "Certificate Number", false, true },
                         { 87, "Date", false, true },
                         { 87, "Employee Name", false, true },
                         { 87, "Enabling Objective #", false, true },
                         { 87, "ILA #", false, true },
                         { 87, "ILA Title", false, true },
                         { 87, "Test Instructions", false, true },
                         { 87, "Test Item ID", false, true },
                         { 87, "Test Time Limit", false, true },
                         { 87, "Test Type", false, true },
                         { 88, "Category", false, true },
                         { 88, "Show EOs linked to Meta", false, true },
                         { 88, "Show Meta label", false, true },
                         { 88, "Show Skill Qualification label", false, true },
                         { 88, "Sub-Category", false, true },
                         { 88, "Topic", false, true },
                         { 92, "Date", false, true },
                         { 92, "Evaluator Notes", false, true },
                         { 92, "Evaluator Signature", false, true },
                         { 92, "Position", false, true },
                         { 92, "Show Procedures", false, true },
                         { 92, "Show Regulatory Requirements", false, true },
                         { 92, "Show Safety Hazards", false, true },
                         { 92, "Show Skill Details", false, true },
                         { 92, "Show Skill Questions and Answers", false, true },
                         { 92, "Show Skill Questions Only", false, true },
                         { 92, "Show Skill Specific Suggestions", false, true },
                         { 92, "Show Tasks", false, true },
                         { 93, "Evaluator Final Signature & Date", false, true },
                         { 93, "Evaluator Notes", false, true },
                         { 93, "Final Result", false, true },
                         { 93, "Position", false, true },
                         { 93, "Show Procedures", false, true },
                         { 93, "Show Regulatory Requirements", false, true },
                         { 93, "Show Safety Hazards", false, true },
                         { 93, "Show Skill Details", false, true },
                         { 93, "Show Skill Questions and Answers", false, true },
                         { 93, "Show Skill Questions Only", false, true },
                         { 93, "Show Skill Specific Suggestions", false, true },
                         { 93, "Show Tasks", false, true },
                         { 93, "Trainee Final Signature & Date", false, true },
                         { 95, "Effective Date", false, true },
                         { 95, "Hyperlink Linked", false, true },
                         { 95, "PDF linked", false, true },
                         { 95, "Revision Number", false, true },
                         { 95, "Safety Hazard Number", false, true },
                         { 95, "Safety Hazard Title", false, true },
                         { 96, "Certificate No.", false, true },
                         { 96, "Grade", false, true },
                         { 96, "Instructor", false, true },
                         { 96, "Location", false, true },
                         { 96, "Provider Contact Info", false, true },
                         { 96, "Provider Contact No.", false, true },
                         { 96, "Provider ID", false, true },
                         { 96, "Score", false, true },
                         { 96, "Training Provider Name", false, true },
                         { 96, "Training Provider Signature", false, true },
                         { 97, "# of Employees enrolled", false, true },
                         { 97, "Certificate Number", false, true },
                         { 97, "Class Final Test Average", false, true },
                         { 97, "Class Pretest Average", false, true },
                         { 97, "Class Start and End Date", false, true },
                         { 97, "Class Start and End Time", false, true },
                         { 97, "Employee Name", false, true },
                         { 97, "Final Test Cut Score", false, true },
                         { 97, "Final Test Score", false, true },
                         { 97, "ILA Number", false, true },
                         { 97, "ILA Title", false, true },
                         { 97, "Organization", false, true },
                         { 97, "Position", false, true },
                         { 97, "Pretest & Final Test Completion Graph", false, true },
                         { 97, "Pretest Cut Score", false, true },
                         { 97, "Pretest Score", false, true },
                         { 98, "Certificate #", false, true },
                         { 98, "Employee Name", false, true },
                         { 98, "Expiration Date", false, true },
                         { 98, "Organization", false, true },
                         { 98, "Position", false, true },
                         { 98, "Renewal Date", false, true },
                         { 99, "Duty Area", false, true },
                         { 99, "Sub-Duty Area", false, true },
                         { 100, "Duty Area", false, true },
                         { 100, "Sub-Duty Area", false, true },
                         { 101, "Duty Area", false, true },
                         { 101, "Issuing Authority", false, true },
                         { 101, "Sub-Duty Area", false, true },
                         { 102, "Delivery Method", false, true },
                         { 102, "EMP Release Criteria", false, true },
                         { 102, "Meta ILA Description", false, true },
                         { 102, "Meta ILA Student Evaluation Linked", false, true },
                         { 102, "Meta ILA Summary Test Linked", false, true },
                         { 102, "Show EOs linked to Meta EO", false, true },
                         { 102, "Show Tasks Linked to Meta Task", false, true },
                         { 104, "ILA #", false, true },
                         { 104, "ILA Title", false, true },
                         { 104, "No. of Items", false, true },
                         { 104, "Status", false, true },
                         { 104, "Test ID", false, true },
                         { 104, "Test Title", false, true },
                         { 105, "Duty Area", false, true },
                         { 105, "Safety Hazard Number", false, true },
                         { 105, "Safety Hazard Title", false, true },
                         { 105, "Show Meta Task Label", false, true },
                         { 105, "Show Tasks Linked to Meta Task", false, true },
                         { 105, "Sub-Duty Area", false, true },
                         { 107, "Category", false, true },
                         { 107, "Safety Hazard No.", false, true },
                         { 107, "Safety Hazard Title", false, true },
                         { 107, "Show EOs Linked to Meta EO", false, true },
                         { 107, "Show Meta EO label", false, true },
                         { 107, "Show SQ Label", false, true }
                });

        }


        protected void Production_AddReport_TrainingProgramQualificationCard()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"Training Program Qualification Card", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {40, 111, 6, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                {111, "Select Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true,null,null},
                {111, "Training Program", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingprogramtype", false, true,null,null},
                {111, "Include Employee Name", "String", "Single", DateTime.MinValue, DateTime.MinValue, "employeename", false, true,null,null},
                {111,"Include Meta Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null},
                {111,"Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                 {111, "Position",  false, true },
                 {111, "Training Program Version",  false, true },
                 {111, "Training Program Date Range",  false, true },
                 {111, "Overall Completion Sign-offs",  false, true },
                 {111, "Task Qualification Sign-offs",  false, true },
                 {111, "Task Qualification Methods",  false, true },
                 {111, "Meta Task Label",  false, true },
                 {111, "Include Tasks linked to Meta Task",  false, true },

             }
            );

            _migrationBuilder.UpdateData(
            table: "ReportSkeleton_Subcategory_Reports",
            keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
            keyValues: new object[] { 40, 13 },
            column: "Order",
            value: "7");

            _migrationBuilder.UpdateData(
           table: "ReportSkeleton_Subcategory_Reports",
           keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
           keyValues: new object[] { 40, 96 },
           column: "Order",
           value: "8");
        }

        protected void Production_AddDataToClassScheduleTQEmpSettingsFromTQILAEmpSettings()
        {
            _migrationBuilder.Sql(@"UPDATE cste
                SET 
                    cste.ReleaseOnClassStart = ilaSet.ReleaseOnClassStart,
                    cste.ReleaseOnClassEnd = ilaSet.ReleaseOnClassEnd,
                    cste.SpecificTime = ilaSet.SpecificTime,
                    cste.PriorToSpecificTime = ilaSet.PriorToSpecificTime
                FROM ClassSchedule_TQEMPSettings cste
                JOIN ClassSchedules cs ON cs.Id = cste.ClassScheduleId
                JOIN TQILAEmpSettings ilaSet ON cs.ILAId = ilaSet.ILAId;
            ");
        }
        
        protected void Production_UpdateTaskByPositionReportFilters()
        {
            _migrationBuilder.DeleteData(
                table: "ReportSkeletonFilters",
                keyColumns: new[] { "ReportSkeletonId", "Name" },
                keyValues: new object[,]
                {
                     {1, "Group by Training Task Group" }
                }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonFilters",
             columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
             values: new object[,]
             {
                  {1, "Select Task Group", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingtasksgroup", false, true,null,null}
             }
           );
        }

        protected void Production_UpdateReportFilters_StudentEvaluationInstructorLed()
        {
            _migrationBuilder.InsertData(
             table: "ReportSkeletonFilters",
             columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
             values: new object[,]
             {
                   {24,"Show Summary of Comments Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null},
             }
           );
        }

        protected void Production_SeedDataToEnrolledPropertyOfMetaILAEmployees()
        {
            _migrationBuilder.Sql(@"UPDATE MetaILA_Employees SET Enrolled = 1");
        }

        protected void Production_AddEmailNotification_PublicClassScheduleRequest()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_Notifications",
              columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
              values: new object[,]
                  {
               {"Public Class Schedule Request", false, "Once enabled,this email will be sent to QTDUser when an employees request for public class request.", false, true}
                  });

            _migrationBuilder.InsertData(
             table: "ClientSettings_Notifications",
             columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
             values: new object[,]
                 {
               {"Public Class Schedule Request Accepted", false, "Once enabled,this email will be sent when a request is accepted for employees.", false, true}
                 });

            _migrationBuilder.InsertData(
            table: "ClientSettings_Notifications",
            columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
            values: new object[,]
                {
               {"Public Class Schedule Request Rejected", false, "Once enabled,this email will be sent when a request is rejected for employees.", false, true}
                });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
               {24, @"@using QTD2.Infrastructure.ExtensionMethods
                        <style>
                        .class-schedule-request-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .class-schedule-request-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .class-schedule-request-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                        <p>
                            Hello, You have requests for the public class schedule.
                        </p>
                        
                        <table class='class-schedule-request-table'>
                            <thead>
                                <tr>
                                    <th style=''width: 15%;''>User Name</th>
                                    <th style=''width: 15%;''>Start Date</th>
                                    <th style=''width: 15%;''>End Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>@(Model.FirstName + "" "" + Model.LastName)</td>
                                    <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                    <td>@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                </tr>
                            </tbody>
                        </table>",
                   1, false, true }
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
               {25, @"@using QTD2.Infrastructure.ExtensionMethods

                       <style>
                        .class-schedule-request-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .class-schedule-request-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .class-schedule-request-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                     <p>
                          Hello, @Model.FirstName @Model.LastName your request  has been <b>accepted</b> for class scheduled on.
                     </p>
                       <table class='class-schedule-request-table'>
                            <thead>
                                <tr>
                                    <th style=''width: 15%;''>User Name</th>
                                    <th style=''width: 15%;''>Start Date</th>
                                    <th style=''width: 15%;''>End Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>@(Model.FirstName + "" "" + Model.LastName)</td>
                                    <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                    <td>@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                </tr>
                            </tbody>
                        </table>
                    ",
                   1, false, true }
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
               {26, @"@using QTD2.Infrastructure.ExtensionMethods
                        <style>
                        .class-schedule-request-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .class-schedule-request-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .class-schedule-request-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                        
                     <p>
                         Hello, @Model.FirstName @Model.LastName your request has been <b>denied</b> for class scheduled on.
                     </p>
                        <table class='class-schedule-request-table'>
                            <thead>
                                <tr>
                                    <th style=''width: 15%;''>User Name</th>
                                    <th style=''width: 15%;''>Start Date</th>
                                    <th style=''width: 15%;''>End Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>@(Model.FirstName + "" "" + Model.LastName)</td>
                                    <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                    <td>@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                </tr>
                            </tbody>
                        </table>
                    ",
                   1, false, true }
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Step_AvailableCustomSettings",
              columns: new[] { "ClientSettingsNotificationStepId", "Setting", "Active" },
              values: new object[,] { { 26, "Email Frequency", true } }
              );
            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: new object[,] { { 26, "Email Frequency", "Weekly", true } }
              );
        }

        protected void Production_UpdateEmailNotificationPublicClassScheduleRequestTemplate()
        {
            _migrationBuilder.UpdateData(
              table: "ClientSettings_Notification_Steps",
              keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
              keyValues: new object[] { 24, 1 },
              column: "Template",
               value: (@"    
                     <p>
                         Hello, A registration request(s) has been submitted via the Public Course Registration Portal. Please log into the QTD Admin site to review the submitted request(s).
                     </p>
                        
                    "
            )
                  );

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 25, 1 },
             column: "Template",
              value:
               (@"@using QTD2.Infrastructure.ExtensionMethods
                                                
                   <div>  
                        <p>Congratulations! Your Public Course Portal Registration has been approved and you may now complete the following class in the Employee Portal.</p>     
                    
                    <p> <b>@Model.CourseTitle</b> on
                    <b>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</b> and <b>@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</b></p>
                    <p>To access the Employee Portal navigate to <a href=""@Model.Url"">@Model.Url</a> and use the Forgot Password button to create a password.</p>   
                    
                    <p>Thank you,<p/>
                    <p>Your Training Department</p>
                    </div> 
                        
                    ")
                 );

            _migrationBuilder.UpdateData(
              table: "ClientSettings_Notification_Steps",
              keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
              keyValues: new object[] { 26, 1 },
              column: "Template",
               value: (@"@using QTD2.Infrastructure.ExtensionMethods
                       <div>
                         <p>Hello, @Model.FirstName @Model.LastName </p>    
                         <p>Your Public Portal registration request has been denied.</p>
                         <p>Please contact your Training Administrator for additional information.</p>
                          <p>  Thank you..</p>
                       </div>
                        
                    "
            )
                  );

            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: new object[,] { { 26, "Send To Others", "", true } }
              );
        }

        protected void Production_UpdateReportFiltersForILACompletionHistory()
        {
            _migrationBuilder.Sql(@"
                UPDATE rf
                SET rf.Value = 'Completed & Not Completed within Date Range'
                FROM ReportFilters rf
                INNER JOIN Reports r ON rf.ReportId = r.Id
                WHERE r.ReportSkeletonId = 13
                AND rf.Name = 'Completion Type'
                AND rf.Value = 'ALL';
                       ");
        }

        protected void Production_AddReport_ProceduresByEnablingObjectives()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"Procedures By Enabling Objectives", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {48, 112, 3, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                {112, "Select Enabling Objective", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectenablingobjective", false, true,null,null},
                {112,"Only show EOs with Procedure Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                 {112, "Category",  false, true },
                 {112, "Sub-Category",  false, true },
                 {112, "Topic",  false, true },
                 {112, "Show EOs linked to Meta",  false, true },
                 {112, "Show Meta label",  false, true },
                 {112, "Show Skill Qualification label",  false, true },
             }
            );
        }

        protected void Production_AddDataToTaskListReviewPositionLinksTable()
        {
            _migrationBuilder.Sql(@"INSERT INTO TaskListReview_PositionLinks(TaskListReviewId, PositionId, Deleted, Active)
                        SELECT Id AS TaskListReviewId, PositionId, Deleted, Active
                        FROM TaskListReview
                        WHERE PositionId IS NOT NULL
                          AND NOT EXISTS (
                              SELECT 1
                              FROM TaskListReview_PositionLinks link
                              WHERE link.TaskListReviewId = TaskListReview.Id
                                AND link.PositionId = TaskListReview.PositionId
                        );"
            );
        }

        protected void Production_UpdateInternalIdentifiersForCertifications()
        {
            _migrationBuilder.Sql(@"  UPDATE Certifications SET InternalIdentifier = 'Other' WHERE Name = 'Other';
                                UPDATE Certifications SET InternalIdentifier = 'Reliability Coordinator'  WHERE Name = 'Reliability Coordinator';
                                UPDATE Certifications SET InternalIdentifier = 'Balancing and Interchange/Transmission Operator' WHERE Name = 'Balancing and Interchange/Transmission Operator';
                                UPDATE Certifications SET InternalIdentifier = 'Balancing and Interchange Operator' WHERE Name = 'Balancing and Interchange Operator';
                                UPDATE Certifications SET InternalIdentifier = 'Transmission Operator' WHERE Name = 'Transmission Operator';
                                UPDATE Certifications SET InternalIdentifier = 'Emergency Response' WHERE Name = 'Emergency Response';
                                UPDATE Certifications SET InternalIdentifier = 'Reg' WHERE Name = 'Reg';
                                UPDATE Certifications SET InternalIdentifier = 'Reg2' WHERE Name = 'Reg2';
                        ");
        }

        protected void Production_AddReportFilter_EmployeeDelinquencyReport()
        {
            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
                values: new object[,]
                {
                  { 60,"Sort Employees by Organization", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
                }
              );
        }

        protected void Production_UpdateReportSkeletonColumns_AnnualTaskListReviewReport()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletonColumns",
            keyColumns: new[] { "ReportSkeletonId", "ColumnName" },
            keyValues: new object[] { 40, "History Date" },
            column: "ColumnName",
            value: "Recent History Date");
        }

        protected void Production_Seed_TaskReviewStatus_NotStarted()
        {
            _migrationBuilder.InsertData(
                table: "TaskReview_Status",
                columns: new[] { "Status", "Active" },
                values: new object[,]
                {
                  { "Not Started",true }
                });
        }

        protected void Production_UpdateReportSkeletonFilter_TaskAndEnabingObjectiveByILA()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletonFilters",
            keyColumns: new[] { "ReportSkeletonId", "Name" },
            keyValues: new object[] { 42, "Include Task and EO Filter" },
            column: "DefaultValue",
            value: "1,2,3,4,5,6");
        }

        protected void Production_MigrateReviewedByFromTrainerName()
        {
            _migrationBuilder.Sql(@"  UPDATE tlr
                SET ReviewedBy = p.FirstName + ' ' + p.LastName
                FROM TaskListReview tlr
                JOIN QtdUsers q
                    ON tlr.TrainerId = q.Id
                JOIN Persons p
                    ON q.PersonId = p.Id
                        ");
        }
        protected void Production_Update_ClientSettings_Notification_StepsTemplate()
        {
            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 2, 1 },
                column: "Template",
                value: @"@using QTD2.Infrastructure.ExtensionMethods;
                <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p>
                <p>This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. 
                Your expiration date is @Model.CertificateExpirationDate.ToString(""MM/dd/yyyy""). 
                To date, we have not received a copy of your updated @Model.CertificateName certificate.</p>
                <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. 
                If you have any questions, please reach out to your Training Administrator.</p>
                <p>Thank you!</p>"
            );

            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 2, 2 },
                column: "Template",
                value: @"@using QTD2.Infrastructure.ExtensionMethods;
                <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p>
                <p>Your attention is required. This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. 
                Your expiration date is @Model.CertificateExpirationDate.ToString(""MM/dd/yyyy""). 
                To date, we have not received a copy of your updated @Model.CertificateName certificate.</p>
                <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. 
                If you have any questions, please reach out to your Training Administrator.</p>
                <p>Thank you!</p>"
            );

            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 2, 3 },
                column: "Template",
                value: @"@using QTD2.Infrastructure.ExtensionMethods;
                <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p>
                <p>Your immediate attention is required. This is an urgent reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. 
                Your expiration date is @Model.CertificateExpirationDate.ToString(""MM/dd/yyyy""). 
                To date, we have not received a copy of your updated @Model.CertificateName certificate.</p>
                <p>After multiple notifications regarding the renewal of your @Model.CertificateName credential, your management has also received a copy of this notification. 
                You and your management will continue to receive daily reminders until the System Operations Training Team receives an updated copy of your @Model.CertificateName certificate.</p>
                <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. 
                If you have any questions, please reach out to your Training Administrator.</p>
                <p>Thank you!</p>"
            );

        }

        protected void Production_AddSkillQualificationStatusTable()
        {
            _migrationBuilder.InsertData(
                table: "SkillQualificationStatus",
                columns: new[] { "Name", "Description", "Active" },
                values: new object[,]
                {
                        {"Trainee Initial Qualification","Employee is a Trainee for the position, and the qualification is his/her Initial Qualification record to indicate successful performance on the Skill", true },
                        {"On Time","Employee qualified on the Skill within the requalification due date window", true },
                        {"Pending","Employee has not qualified on the Skill, but the current date is still within the requalification due date window", true },
                        {"Delayed","Employee qualified on the Skill outside of the requalification due date window", true },
                        {"Overdue","The 6-month window has passed, Employee has not qualified", true },
                        {"Requalification Not Required","Employee was flagged a Trainee for the position at the time of the Skill change. Employee qualified on revised skill as part of initial training", true },
                        {"No Position Qual Date","The Employee is not flagged as a Trainee and there is no Position Qual Date in the Employee window to use to confirm the skill qual against", true },
                        {"Completed","Skill Requalification is completed on Emp side", true }
                });
        }
        protected void Production_UpdateClassRoasterReportSkeletonColumn()
        {
            _migrationBuilder.UpdateData(
                table: "ReportSkeletonColumns",
                        keyColumns: new[] { "ReportSkeletonId", "ColumnName" },
                        keyValues: new object[] { 39, "Employee Name - Details" },
                        column: "ColumnName",
                        value: "Employee Details"
            );
        }

        protected void Production_AddReport_ILAsBySafetyHazard()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"ILAs by Safety Hazard", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {50, 113, 4, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                {113, "Safety Hazards", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "safetyhazard", false, true,null,null},
                {113,"Include Safety Hazard Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null},
                {113,"Include Meta ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null},
                {113,"Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                 {113, "Category",  false, true },
                 {113, "Safety Hazard No.",  false, true },
                 {113, "Safety Hazard Title",  false, true },
                 {113, "ILA No.",  false, true },
                 {113, "ILA Title",  false, true },
                 {113, "Show Meta ILA Label",  false, true },
                 {113, "Shows ILAs linked to Meta ILA",  false, true },
             }
            );

            _migrationBuilder.UpdateData(
           table: "ReportSkeleton_Subcategory_Reports",
           keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
           keyValues: new object[] { 50, 94 },
           column: "Order",
           value: "5");
        }

        protected void Production_SeedSimulatorScenarioEventsAsync()
        {
            _migrationBuilder.Sql(@" INSERT INTO [dbo].[SimulatorScenario_Events]
                                       ([SimulatorScenarioId]
                                       ,[Order]
                                       ,[Title]
                                       ,[Description]
                                       ,[Deleted]
                                       ,[Active]
                                       ,[CreatedBy]
                                       ,[CreatedDate]
                                       ,[ModifiedBy]
                                       ,[ModifiedDate])
                            SELECT     [SimulatorScenarioId]
                                      ,[Order]
                                      ,[Title]
                                      ,[Description]
                                      ,[Deleted]
                                      ,[Active]
                                      ,[CreatedBy]
                                      ,[CreatedDate]
                                      ,[ModifiedBy]
                                      ,[ModifiedDate]
                            FROM [dbo].[SimulatorScenario_EventAndScripts];
                        ");

            _migrationBuilder.Sql(@" 
                        INSERT INTO [dbo].[SimulatorScenario_Scripts]
                                  ([Title]
                                   ,[Description]
                                   ,[InitiatorId]
                                   ,[Time]
                                   ,[EventId]  
                                   ,[Deleted]
                                   ,[Active]
                                   ,[CreatedBy]
                                   ,[CreatedDate]
                                   ,[ModifiedBy]
                                   ,[ModifiedDate])
                        SELECT     
                                  es.[Title]
                                 ,es.[Description]
                                 ,es.[InitiatorId]
                                 ,es.[Time]
                                 ,ev.[Id] AS [EventId]   
                                 ,es.[Deleted]
                                 ,es.[Active]
                                 ,es.[CreatedBy]
                                 ,es.[CreatedDate]
                                 ,es.[ModifiedBy]
                                 ,es.[ModifiedDate]
                        FROM [dbo].[SimulatorScenario_EventAndScripts] es
                        INNER JOIN [dbo].[SimulatorScenario_Events] ev
                            ON ev.[Id] = es.[Id]
                        WHERE NOT EXISTS (
                            SELECT 1
                            FROM [dbo].[SimulatorScenario_Scripts] s
                            WHERE s.[EventId] = ev.[Id]
                        );
                  ");

            _migrationBuilder.Sql(@" 
              INSERT INTO [dbo].[SimulatorScenario_Script_Criterias]
                       ([ScriptId]
                       ,[CriteriaId]
                       ,[Deleted]
                       ,[Active]
                       ,[CreatedBy]
                       ,[CreatedDate]
                       ,[ModifiedBy]
                       ,[ModifiedDate])
                SELECT     sc.[Id] AS [ScriptId]
                          ,esc.[CriteriaId]
                          ,esc.[Deleted]
                          ,esc.[Active]
                          ,esc.[CreatedBy]
                          ,esc.[CreatedDate]
                          ,esc.[ModifiedBy]
                          ,esc.[ModifiedDate]
                FROM [dbo].[SimulatorScenario_EventAndScript_Criterias] esc
                INNER JOIN [dbo].[SimulatorScenario_EventAndScripts] es
                    ON es.[Id] = esc.[EventAndScriptId]
                INNER JOIN [dbo].[SimulatorScenario_Scripts] sc
                    ON sc.[EventId] = es.[Id]
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM [dbo].[SimulatorScenario_Script_Criterias] ssc
                    WHERE ssc.[ScriptId] = sc.[Id]
                      AND ssc.[CriteriaId] = esc.[CriteriaId]
                );
            ");
        }

        protected void Production_UpdateSimulatorScenarioILAsAndPrerequisitesForDeletedILAs()
        {
            _migrationBuilder.Sql(@" UPDATE ssIla
                            SET ssIla.Deleted = 1
                            FROM [dbo].[SimulatorScenario_ILAs] ssIla
                            INNER JOIN [dbo].[ILAs] ila
                                ON ssIla.ILAID = ila.Id
                            WHERE ila.Deleted = 1 
                            ");

            _migrationBuilder.Sql(@" UPDATE ssPre
                SET ssPre.Deleted = 1
                FROM [dbo].[SimulatorScenario_Prerequisites] ssPre
                INNER JOIN [dbo].[ILAs] ila
                    ON ssPre.PrerequisiteId = ila.Id
                WHERE ila.Deleted = 1 ");
        }

        protected void Production_Update_ClientSettingsNotification_StepsTemplateForTaskQualification()
        {
            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 10, 1 },
                column: "Template",
                value: @"<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p> You have been assigned to complete a Task/Skill Qualification as part of your position specific training program. An evaluator has been assigned to sign - off on this task/skill qualification.This will be completed using the Employee Portal(EMP). Please review the table below for further details. To help you prepare for the task/skill qualification, the task(s)/skill(s) below are available in a read - only format in EMP.</p><figure class=""table""><table><tbody><tr><td>Task/Skill #</td><td>@Model.TaskNumber</td></tr><tr><td>Task/Skill Statement</td><td>@Model.TaskStatement</td></tr><tr><td>Evaluator Name</td><td>@Model.EvaluatorName</td></tr></tbody></table></figure><p>If for any reason you cannot complete the assigned Task/Skill Qualification, notify your TaskSkill Qualification Evaluator and/or Training Administrator as soon as possible. Thank you!</p>'"
            );

            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 11, 1 },
                column: "Template",
                value: @"Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned as an Evaluator to sign off on a Task/Skill Qualification. This will be completed using the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Task/Skill #</td><td>@Model.TaskNumber</td></tr><tr><td>Task/Skill Statement</td><td>@Model.TaskStatement</td></tr><tr><td>Trainee Name</td><td>@Model.TraineeName</td></tr></table></figure><p>If for any reason you cannot complete the assigned Task/Skill Qualification(s), notify your Training Administrator as soon as possible.</p><p>Thank you!</p>"
            );

            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notifications",
             keyColumns: new[] { "Id", "Name" },
             keyValues: new object[] { 10, "EMP Task Qualification - Trainee" },
             column: "Name",
             value: "EMP Task And Skill Qualification - Trainee");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notifications",
              keyColumns: new[] { "Id", "Name" },
              keyValues: new object[] { 11, "EMP Task Qualification - Evaluator" },
              column: "Name",
              value: "EMP Task And Skill Qualification - Evaluator");

        }
    }
}
