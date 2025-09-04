using LegacyToQtd2Migrator.Mapping.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Releases
{
    public class MVP : IRelease
    {
        public List<IMigrationMap> MigrationMaps { get; set; }

        DbContext _source;
        DbContext _target;

        public MVP(DbContext source, DbContext target)
        {
            _source = source;
            _target = target;
        }

        public void LoadMigrationMaps()
        {
            MigrationMaps = new List<IMigrationMap>();

            MigrationMaps.Add(new Mapping.MVP.ClientSettingsLicenseMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.OrganizationsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.LocationsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.CoversheetsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.TaskSuggestionTypeMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.TaxonomyLevelsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.DeliveryMethodsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.LabelReplacementMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.ToolsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.NERCTargetAudienceMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.NERCTargetAudienceFromIlaMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.TDTImagesMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.GeneralSettingsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.RegulatoryRequirementsMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.MVP.CertificationsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.CertificationsFromLktblAnnualTrainingRequirementsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Certification_HistoryMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.MVP.PositionsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Positions_SQsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Position_HistoriesMap(_source, _target));

            //Combine
            MigrationMaps.Add(new Mapping.MVP.DutyAreasMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SubdutyAreasMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TasksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_SuggestionsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_StepsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_ReferencesMap(_source, _target));         //Not mapped with DutyArea(Needs to be discussed)
            //MigrationMaps.Add(new Mapping.MVP.Task_QuestionsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.TaskHistoriesMap(_source, _target));

            MigrationMaps.Add(new Mapping.MVP.TrainingGroupsMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.(_source, _target));

            //combine      
            MigrationMaps.Add(new Mapping.MVP.EmployeesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.PersonsMap(_source, _target));        /*not inserted*/
            //MigrationMaps.Add(new Mapping.MVP.EmployeeOrganizationsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.EmployeeCertifictaionHistoriesMap(_source, _target));
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
            MigrationMaps.Add(new Mapping.MVP.EnablingObjective_CategoriesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EnablingObjective_SubCategoriesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EnablingObjective_TopicsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EnablingObjective_QuestionsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EnablingObjectivesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_EnablingObjective_LinksMap(_source, _target));

            MigrationMaps.Add(new Mapping.MVP.EnablingObjective_BackPopulateMap(_source, _target));


            //combine
            MigrationMaps.Add(new Mapping.MVP.SaftyHazard_CategoriesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SaftyHazardsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SaftyHazard_AbatementsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SaftyHazard_ControlsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SafetyHazard_Task_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SafetyHazard_EO_LinksMap(_source, _target));


            //combine
            MigrationMaps.Add(new Mapping.MVP.InstructorsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.PersonsFromLkTblInstructorsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ClientUsersFromLkInstrutorsMap(_source, _target));


            //combine
            MigrationMaps.Add(new Mapping.MVP.ProvidersMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.IlasMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ILA_TopicsMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ILA_EnablingObjective_LinksMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ProviderLevelsFromlkTbllLAProviderStatusMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ProviderLevelsFromlkTblSupplierMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ClassSchedulesMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ClassScheduleHistoriesMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ClassScheduleEmployeesMap(_source, _target));
            // MigrationMaps.Add(new Mapping.MVP.ClassSchedule_RosterMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.ILA_PreRequisite_LinksMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.MVP.TestItemTypesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemTrueFalsesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemShortAnswersMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemMCQsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemMatchesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestItemFillBlanksMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.MVP.TestStatusesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TestsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Test_Item_LinksMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.TestReleaseEMPSettingsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.UpdateILAUseEmpForTests(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.MVP.Procedure_IssuingAuthoritiesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Proc_IssuingAuthority_HistoriesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ProceduresMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Procedure_EnablingObjective_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.EnablingObjective_Procedure_LinksMap(_source, _target)); 
            //MigrationMaps.Add(new Mapping.MVP.Procedure_Task_LinksMap(_source, _target));

            MigrationMaps.Add(new Mapping.MVP.RatingScalesMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.RatingScalesNMap(_source, _target));

            MigrationMaps.Add(new Mapping.MVP.QuestionBanksMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.MVP.StudentEvaluationsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.StudentEvaluationQuestionsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.StudentEvaluationFormsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.StudentEvaluation_QuestionsMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.StudentEvaluationWithoutEmpMap(_source, _target));


            //combine
            MigrationMaps.Add(new Mapping.MVP.SimulatorScenariosMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulatorScenarioTaskObjectivesLinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulatorScenarioPositonLinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulatorScenarioILALinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulatorScenarioEnablingObjectivesLinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.SimulationScenarioSpecLookUpsMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.MVP.IDPsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.IDP_ReviewMap(_source, _target));           

            //MigrationMaps.Add(new Mapping.MVP.EvaluationReleaseEMPSettingsMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.MVP.SegmentsMap(_source, _target));
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

            MigrationMaps.Add(new Mapping.MVP.ILA_TaskObjective_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_StudentEvaluation_LinksMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.ILA_SafetyHazard_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_Procedure_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_PreRequisite_LinksMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.NercStandardMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ILA_NercStandard_LinksMap(_source, _target));

            MigrationMaps.Add(new Mapping.MVP.Position_TasksMap(_source, _target));

            //combine
            MigrationMaps.Add(new Mapping.MVP.TaskQualificationsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TaskQualificationStatusesMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TaskQualification_Evaluator_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.TQEMPSettings_LinksMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.Task_SaftyHazard_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_Procedure_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_PositionsMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.Task_ILA_LinksMap(_source, _target));


            //MigrationMaps.Add(new Mapping.MVP.ClassSchedule_StudentEvaluations_LinksMap(_source, _target));
            //MigrationMaps.Add(new Mapping.MVP.ClientUsersFromLkInstrutorsMap(_source, _target));

            MigrationMaps.Add(new Mapping.MVP.TrainingProgramsMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.TrainingProgramsFromRsTblPositionTraining(_source, _target));

            MigrationMaps.Add(new Mapping.MVP.TrainingProgramReviewsMap(_source, _target));

            //MigrationMaps.Add(new Mapping.MVP.ProcedureReviewMap(_source, _target));

            MigrationMaps.Add(new Mapping.MVP.DifSurveysMap(_source, _target));
            MigrationMaps.Add(new Mapping.MVP.DifSurveys_ResponsesMap(_source, _target));

            MigrationMaps.Add(new Mapping.MVP.DocumentsMap(_source, _target));
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
