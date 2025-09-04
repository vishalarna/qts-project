using LegacyToQtd2Migrator.Mapping.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Releases
{
    public class Vision : IRelease
    {
        public List<IMigrationMap> MigrationMaps { get; set; }

        DbContext _source;
        DbContext _target;

        int _projectId;

        public Vision(DbContext source, DbContext target, int projectId)
        {
            _source = source;
            _target = target;

            _projectId = projectId;
        }

        public void LoadMigrationMaps()
        {
            MigrationMaps = new List<IMigrationMap>();

            //MigrationMaps.Add(new Mapping.Vision.ClientSettingsLicenseMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.OrganizationsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.LocationsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.CoversheetsMap(_source, _target));
            MigrationMaps.Add(new Mapping.Vision.TaskSuggestionTypeMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.DeliveryMethodsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.LabelReplacementMap(_source, _target));
            MigrationMaps.Add(new Mapping.Vision.ToolCategoryMap(_source, _target, _projectId));
            MigrationMaps.Add(new Mapping.Vision.NERCTargetAudienceMap(_source, _target, _projectId));
            //MigrationMaps.Add(new Mapping.Vision.TDTImagesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.GeneralSettingsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.RegulatoryRequirementsMap(_source, _target));

            ////combine
            //MigrationMaps.Add(new Mapping.Vision.CertificationsMap(_source, _target));

            ////combine
            //MigrationMaps.Add(new Mapping.Vision.PositionsMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.Position_HistoriesMap(_source, _target));

            //Combine
            //MigrationMaps.Add(new Mapping.Vision.DutyAreasMap(_source, _target, _projectId));
            MigrationMaps.Add(new Mapping.Vision.DutyAreasFromTasksMap(_source, _target, _projectId));
            //MigrationMaps.Add(new Mapping.MVP.SubdutyAreasMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.TasksMap(_source, _target, _projectId));
            //MigrationMaps.Add(new Mapping.MVP.Task_SuggestionsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_StepsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_ReferencesMap(_source, _target));        
            //MigrationMaps.Add(new Mapping.MVP.Task_QuestionsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.TaskHistoriesMap(_source, _target));

            //MigrationMaps.Add(new Mapping.Vision.TrainingGroupsMap(_source, _target));

            ////combine
            MigrationMaps.Add(new Mapping.Vision.EnablingObjective_CategoriesMap(_source, _target, _projectId));
            ////MigrationMaps.Add(new Mapping.MVP.EnablingObjective_SubCategoriesMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.EnablingObjective_TopicsMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.EnablingObjective_QuestionsMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.EnablingObjectivesMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.Task_EnablingObjective_LinksMap(_source, _target));

            //MigrationMaps.Add(new Mapping.Vision.EnablingObjective_BackPopulateMap(_source, _target));


            ////combine
            //MigrationMaps.Add(new Mapping.Vision.SaftyHazard_CategoriesMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.SaftyHazardsMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.SaftyHazard_AbatementsMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.SaftyHazard_ControlsMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.SafetyHazard_Task_LinksMap(_source, _target));
            ////MigrationMaps.Add(new Mapping.MVP.SafetyHazard_EO_LinksMap(_source, _target));


            //MigrationMaps.Add(new Mapping.Vision.RatingScalesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.RatingScalesNMap(_source, _target));

            //MigrationMaps.Add(new Mapping.Vision.QuestionBanksMap(_source, _target));

            //MigrationMaps.Add(new Mapping.Vision.Position_TasksMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.Vision.TestItemTypesMap(_source, _target, _projectId));
            //MigrationMaps.Add(new Mapping.MVP.TestItemsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemTrueFalsesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemShortAnswersMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemMCQsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemMatchesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemFillBlanksMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.Vision.ProvidersMap(_source, _target, _projectId));
            // MigrationMaps.Add(new Mapping.MVP.IlasMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ILA_TopicsMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ILA_EnablingObjective_LinksMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ProviderLevelsFromlkTbllLAProviderStatusMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ProviderLevelsFromlkTblSupplierMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ClassSchedulesMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ClassScheduleHistoriesMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ClassScheduleEmployeesMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ClassSchedule_RosterMap(_source, _target));
            //MigrationMaps.Add(new Mapping.Vision.ILA_PreRequisite_LinksMap(_source, _target));


            MigrationMaps.Add(new Mapping.Vision.TestsMap(_source, _target, _projectId));

            //combine
            MigrationMaps.Add(new Mapping.Vision.Procedure_IssuingAuthoritiesMap(_source, _target, _projectId));
            //MigrationMaps.Add(new Mapping.MVP.Proc_IssuingAuthority_HistoriesMap(_source, _target));
            MigrationMaps.Add(new Mapping.Vision.ProceduresMap_FromAnalysisProcedures(_source, _target, _projectId));
            //MigrationMaps.Add(new Mapping.Vision.ProceduresMap_FromXRef(_source, _target, _projectId));
            //MigrationMaps.Add(new Mapping.MVP.Procedure_EnablingObjective_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EnablingObjective_Procedure_LinksMap(_source, _target)); 
            //MigrationMaps.Add(new Mapping.MVP.Procedure_Task_LinksMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.Vision.SegmentsMap(_source, _target, _projectId));
            //MigrationMaps.Add(new Mapping.MVP.SegmentObjectiveLinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_Segment_LinksMap(_source, _target));

            ////first stopping point

            /*
            //combine      
            MigrationMaps.Add(new Mapping.Vision.EmployeesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.PersonsMap(_source, _target));       
            //MigrationMaps.Add(new Mapping.MVP.EmployeeOrganizationsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EmployeeCertifictaionHistoriesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EmployeeCertificationsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Employee_TasksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EmployeeHistoriesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.PersonsMapFromTblEmployee(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ClientUsersFromTblEmployeeMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EmployeePositionsFromTblEmployeeAdditionalPositionMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EmployeePositionsFromTblEmployeeMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Position_EmployeesFromTblEmployeeAdditionalPositions(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Position_EmployeesFromTblEmployeeMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.Vision.InstructorsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.PersonsFromLkTblInstructorsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ClientUsersFromLkInstrutorsMap(_source, _target));


            //combine
            MigrationMaps.Add(new Mapping.Vision.TestStatusesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Test_Item_LinksMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.TestReleaseEMPSettingsMap(_source, _target));
            MigrationMaps.Add(new Mapping.Vision.UpdateILAUseEmpForTests(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.Vision.StudentEvaluationsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.StudentEvaluationQuestionsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.StudentEvaluationFormsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.StudentEvaluation_QuestionsMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.StudentEvaluationWithoutEmpMap(_source, _target));


            //combine
            MigrationMaps.Add(new Mapping.Vision.SimulatorScenariosMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulatorScenarioTaskObjectivesLinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulatorScenarioPositonLinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulatorScenarioILALinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulatorScenarioEnablingObjectivesLinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulationScenarioSpecLookUpsMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.Vision.IDPsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.IDP_ReviewMap(_source, _target));           

            //MigrationMaps.Add(new Mapping.MVP.EvaluationReleaseEMPSettingsMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.Vision.SegmentsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SegmentObjectiveLinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_Segment_LinksMap(_source, _target));


            //MigrationMaps.Add(new Mapping.MVP.Procedure_ILA_LinksMap(_source, _target));

            //combine
            //MigrationMaps.Add(new Mapping.MVP.CustomObjectivesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILACustomObjective_LinkMap(_source, _target));

            //combine
            //unmapped per SLL
            //MigrationMaps.Add(new Mapping.MVP.AssessmentToolMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_AssessmentTool_LinksMap(_source, _target));

            MigrationMaps.Add(new Mapping.Vision.ILA_TaskObjective_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_StudentEvaluation_LinksMap(_source, _target));
            MigrationMaps.Add(new Mapping.Vision.ILA_SafetyHazard_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_Procedure_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_PreRequisite_LinksMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.NercStandardMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_NercStandard_LinksMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.Vision.TaskQualificationsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TaskQualificationStatusesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TaskQualification_Evaluator_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TQEMPSettings_LinksMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.Task_SaftyHazard_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_Procedure_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_PositionsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_ILA_LinksMap(_source, _target));


            //MigrationMaps.Add(new Mapping.MVP.ClassSchedule_StudentEvaluations_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ClientUsersFromLkInstrutorsMap(_source, _target));

            MigrationMaps.Add(new Mapping.Vision.TrainingProgramsMap(_source, _target));
            MigrationMaps.Add(new Mapping.Vision.TrainingProgramsFromRsTblPositionTraining(_source, _target));

            MigrationMaps.Add(new Mapping.Vision.TrainingProgramReviewsMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.ProcedureReviewMap(_source, _target));

            MigrationMaps.Add(new Mapping.Vision.DifSurveysMap(_source, _target));
            MigrationMaps.Add(new Mapping.Vision.DifSurveys_ResponsesMap(_source, _target));

            MigrationMaps.Add(new Mapping.Vision.DocumentsMap(_source, _target));
            */
        }

        public void RunRelease()
        {
            LoadMigrationMaps();

            foreach (var map in MigrationMaps)
            {
                map.ConvertRecords();
            }
        }
    }
}
