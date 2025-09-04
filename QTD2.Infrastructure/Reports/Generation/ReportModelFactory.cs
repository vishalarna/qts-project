

using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Reports.Generation.Generators;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation
{
    public partial class ReportModelFactory : IReportModelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportModelFactory(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IReportModel> GenerateModelAsync(Report report)
        {
            var generatorType = GetGeneratorType(report.ReportSkeletonId);
            var generator = (IReportModelGenerator)_serviceProvider.GetService(generatorType);

            var reportModel = await generator.GenerateModel(report);

            return reportModel;
        }

        private Type GetGeneratorType(int reportSkeletonId)
        {
            switch (reportSkeletonId)
            {
                case 1:
                    return typeof(TaskByPositionModelGenerator);
                case 2:
                    return typeof(IDPReviewCompletionHistoryModelGenerator);
                case 3:
                    return typeof(ReportScheduleByClassModelGenerator);
                case 4:
                    return typeof(ILAByProvidersModelGenerator);
                case 5:
                    return typeof(TasksByTaskGroupModelGenerator);
                case 6:
                    return typeof(StudentEvaluationFormModelGenerator);
                case 7:
                    return typeof(MyDataPositionDetailsGenerator);
                case 8:
                    return typeof(EmployeeByPositionGenerator);
                case 9:
                    return typeof(MyDataPositionLinkagesGenerator);
                case 10:
                    return typeof(EmployeeByOrganizationGenerator);
                case 11:
                    return typeof(EmployeePositionHistoryGenerator);
                case 12:
                    return typeof(EmployeeCertificationHistoryGenerator);
                case 13:
                    return typeof(ILACompletionHistoryGenerator);
                case 14:
                    return typeof(ClassesByILAGenerator);
                case 15:
                    return typeof(ILALessonPlanGenerator);
                case 16:
                    return typeof(TaskQualificationEvaluatorsGenerator);
                case 17:
                    return typeof(StudentEvalutationResultsGenerator);
                case 18:
                    return typeof(TaskRequalificationByPositionGenerator);
                case 19:
                    return typeof(TrainingQualificationRecordsGenerator);
                case 20:
                    return typeof(TestItemsGenerator);
                case 21:
                    return typeof(TestSpecificationsGenerator);
                case 22:
                    return typeof(TaskRequalificationByEmployeeGenerator);
                case 23:
                    return typeof(ListOfTaskEvaluatorsGenerator);
                case 24:
                    return typeof(StudentEvalutationResultsInstructorGenerator);
                case 25:
                    return typeof(ListOfCertifiedOperatorsGenerator);
                case 27:
                    return typeof(TrainingSummaryByPositionGenerator);
                case 28:
                    return typeof(OJTGuideByPositionGenerator);
                case 29:
                    return typeof(OJTGuideByTaskGenerator);
                case 30:
                    return typeof(OJTGuideByILAGenerator);
                case 31:
                    return typeof(TaskQualificationSheetsByPositionGenerator);
                case 32:
                    return typeof(TaskQualificationSheetsByTaskGenerator);
                case 33:
                    return typeof(TaskQualificationSheetsByILAGenerator);
                case 34:
                    return typeof(ProcedureReviewCompletionHistoryGenerator);
                case 35:
                    return typeof(TrainingProgramReviewGenerator);
                case 36:
                    return typeof(EnablingObjectivesByTaskGenerator);
                case 37:
                    return typeof(EnablingObjectivesByPositionGenerator);
                case 38:
                    return typeof(EmployeeTrainingNeedsAssessmentGenerators);
                case 39:
                    return typeof(ClassRosterGenerator);
                case 40:
                    return typeof(AnnualPositionsTaskListReviewGenerator);
                case 41:
                    return typeof(EOPHoursForDesignatedYearsGenerator);
                case 42:
                    return typeof(TaskandEOByILAGenerator);
                case 43:
                    return typeof(RecordOfTaskEOQualificationGenerator);
                case 44:
                    return typeof(TaskHistoryByPositionGenerator);
                case 45:
                    return typeof(TrainingMaterialForTaskEOByPositionsGenerator);
                case 46:
                    return typeof(InitialTrainingByPositionGenerator);
                case 47:
                    return typeof(EmployeeTrainingStatusGenerator);
                case 48:
                    return typeof(TrainingModuleCompletionHistoryByEmployeeGenerator);
                case 49:
                    return typeof(TrainingModuleCompletionHistoryGenerator);
                case 50:
                    return typeof(TrainingProgramCompletionHistoryGenerator);
                case 51:
                    return typeof(ClassSignInSheetGenerator);
                case 53:
                    return typeof(DIFSurveyBlankFormGenerator);
                case 54:
                    return typeof(DIFSurveyIndividualFeedbackGenerator);
                case 55:
                    return typeof(DIFSurveyAggregateResultsGenerator);
                case 56:
                    return typeof(YearToDateHoursForCertifiedEmployeesGenerator);
                case 57:
                    return typeof(TotalNERCCEHsForTheYearToDateGenerator);
                case 58:
                    return typeof(EmployeeTrainingTowardNercRecertificationGenerator);
                case 59:
                    return typeof(EmployeeTrainingTowardCertAndAllRequiredTrainingGenerator);
                case 60:
                    return typeof(EmployeeDelinquencyReportGenerator);   
                case 61:
                    return typeof(ProcedureReviewCompletionHistorybyEmployeeGenerator);
                case 62:
                    return typeof(TasksMetbyPositionGenerator);
                case 63:
                    return typeof(TasksMetbyEmployeeGenerator);
                case 64:
                    return typeof(EmployeeActiveInactiveHistoryGenerator);
                case 65:
                    return typeof(ReliabilityRelatedTaskImpactMatrixR5Generator);
                case 66:
                    return typeof(EMPTestSummarybyClassesGenerator);
                case 67:
                    return typeof(EmployeeTrainingTowardCertAndAllRequiredTrainingSummaryGenerator);
                case 68:
                    return typeof(EMPTaskQualificationDetailsGenerator);
                case 69:
                    return typeof(EmployeeCourseScheduleforGivenYearGenerator);
                case 70:
                    return typeof(EmployeeIDPCompletionStatusReportGenerator);  
                case 71:
                    return typeof(TaskHistoryByTaskGenerator);
                case 72:
                    return typeof(EmployeeTrainingTowardProceduresAndRegulatoryRequirementsGenerator);
                case 73:
                    return typeof(ProcedureAndRegulatoryRequirementTrainingSummarybyPositionGenerator);
                case 74:
                    return typeof(SummaryOfTaskQualificationBySubDutyAreaGenerator);
                case 75:
                    return typeof(EmployeeTaskQualificationDatesByTaskGenerator);
                case 76:
                    return typeof(EmployeeTaskQualificationRecordsForGivenPositionGenerator);
                case 77:
                    return typeof(EMPTestStatisticsGenerator);
                case 78:
                    return typeof(SCORMCompletionSummaryByClassesGenerator);
                case 79:
                    return typeof(SCORMTestCompletionStatisticsGenerator);
                case 80:
                    return typeof(OJTTrainingLogGenerator);
                case 81:
                    return typeof(ProceduresByIssuingAuthorityGenerator);
                case 82:
                    return typeof(ProceduresByTaskGenerator);
                case 83:
                    return typeof(EnablingObjectivesNotLinkedToTaskGenerator);
                case 84:
                    return typeof(EnablingObjectivesNotLinkedToILAGenerator);
                case 85:
                    return typeof(TasksByDutyAreaGenerator);
                case 86:
                    return typeof(EnablingObjectivesByCategoryGenerator);
                case 87:
                    return typeof(TestReportPaperBasedVersionGenerator);
                case 88:
                    return typeof(TasksByEnablingObjectivesGenerator);
                case 89:
                    return typeof(IlasByTaskGenerator);
                case 90:
                    return typeof(IlasByEnablingObjectiveGenerator);
                case 91:
                    return typeof(IlasByPositionGenerator);
                case 92:
                    return typeof(SkillQualificationTrainingGuideByPositionOrSkillGenerator);
                case 93:
                    return typeof(SkillQualificationAssessmentByPositionOrTaskGenerator);
                case 94:
                    return typeof(SafetyHazardsByPositionMatrixGenerator);
                case 95:
                    return typeof(SafetyHazardsByCategoryGenerator);
                case 96:
                    return typeof(ClassCertificatesGenerator);
                case 97:
                    return typeof(PretestAndFinalTestComparisonGenerator);
                case 98:
                    return typeof(CertifiedEmployeesforGivenCertificateGenerator);
                case 99:
                    return typeof(TasksNotLinkedToILAGenerator);
                case 100:
                    return typeof(TasksNotLinkedToPositionGenerator);
                case 101:
                    return typeof(TasksByProcedureGenerator);
                case 102:
                    return typeof(ILAsByMetaILAGenerator);
                case 103:
                    return typeof(TasksByPositionMatrixGenerator);
                case 104:
                    return typeof(TestListGenerator); 
                case 105:
                    return typeof(SafetyHazardsByTaskGenerator);
                case 106:
                    return typeof(EnablingObjectivesByPositionMatrixGenerator);
                case 107:
                    return typeof(EnablingObjectivesbySafetyHazardGenerator);
                case 108:
                    return typeof(NERCILAApplicationReportVersionGenerator);
                case 26:
                    return typeof(TrainingIssuesListGenerator);
                case 109:
                    return typeof(TrainingIssueDetailsGenerator);
                case 110:
                    return typeof(TrainingIssuesActionItemsGenerator);
                case 111:
                    return typeof(TrainingProgramQualificationCardGenerator);
                case 112:
                    return typeof(ProceduresByEnablingObjectivesGenerator);
                default:
                    throw new QTDServerException("No report skeleton found with id " + reportSkeletonId);
            }
        }
    }
}