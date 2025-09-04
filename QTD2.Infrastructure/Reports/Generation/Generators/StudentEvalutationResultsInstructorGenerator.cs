using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;


namespace QTD2.Infrastructure.Reports.Generation.Generators
{
  public  class StudentEvalutationResultsInstructorGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IClassSchedule_Evaluation_RosterService _classScheduleEvaluationRosterService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public StudentEvalutationResultsInstructorGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IClassScheduleService classScheduleService,
        IClassSchedule_Evaluation_RosterService classScheduleEvaluationRosterService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _classScheduleService = classScheduleService;
            _classScheduleEvaluationRosterService = classScheduleEvaluationRosterService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "StudentEvalutationResultsInstructorLed.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            List<InstructorLedDataModel> evaluations = new List<InstructorLedDataModel>();
            var classScheduleIds = ExtractParameters<List<int>>(report.Filters, "TRAINING CLASSES");
            var formIDs = ExtractParameters<List<int>>(report.Filters, "SELECT FORM");
            var includeSummaryOfCommentsOnly = ExtractParameters<bool>(report.Filters, "SHOW SUMMARY OF COMMENTS ONLY");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var classScheduleRosters = await _classScheduleEvaluationRosterService.GetEvaluation_RostersByEvalId(classScheduleIds, formIDs);
            var groupedClassRosters = classScheduleRosters.GroupBy(x => new { x.StudentEvaluationInfo, x.ClassScheduleInfo });
            var classSchedules = await _classScheduleService.GetStudentEvalutationResultsInstructorAsync(classScheduleRosters.Select(r => r.ClassScheduleId.Value).Distinct().ToList());
            var allStudentWithoutEmps = classSchedules.SelectMany(x => x.StudentEvaluationWithoutEmps);
            foreach (var groupedClassRoster in groupedClassRosters)
            {
                var studentEvalWithoutEmps = allStudentWithoutEmps.Where(x => x.ClassScheduleId == groupedClassRoster.Key.ClassScheduleInfo.Id && x.StudentEvaluationId == groupedClassRoster.Key.StudentEvaluationInfo.Id).ToList();
                evaluations.Add(new InstructorLedDataModel(groupedClassRoster.Key.ClassScheduleInfo, groupedClassRoster.Key.StudentEvaluationInfo, groupedClassRoster.ToList(), studentEvalWithoutEmps));
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new StudentEvalutationResultsInstructorLed(report.InternalReportTitle, templatePath, displayColumns, companyLogo, evaluations, labelReplacement, defaultTimeZone, defaultDateFormat, includeSummaryOfCommentsOnly);
        }
    }
}
