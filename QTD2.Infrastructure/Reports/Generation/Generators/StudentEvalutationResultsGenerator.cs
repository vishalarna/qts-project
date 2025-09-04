using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class StudentEvalutationResultsGenerator : ReportModelGenerator
    {

        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IStudentEvaluationService _studentEvaluationService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IILA_StudentEvaluation_LinkService _iLA_StudentEvaluation_LinkService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IClassSchedule_Evaluation_RosterService _classSchedule_Evaluation_RosterService;
        private readonly IStudentEvaluationWithoutEmpService _studentEvaluationWithoutEmpService;
        

        public StudentEvalutationResultsGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IStudentEvaluationService studentEvaluationService,
          IClassScheduleService classScheduleService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          IILA_StudentEvaluation_LinkService iLA_StudentEvaluation_LinkService,
          IClassSchedule_Evaluation_RosterService classSchedule_Evaluation_RosterService,
          IStudentEvaluationWithoutEmpService studentEvaluationWithoutEmpService
          )
        {
            _generalSettingService = generalSettingService;
            _studentEvaluationService  = studentEvaluationService;
            _classScheduleService = classScheduleService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _iLA_StudentEvaluation_LinkService = iLA_StudentEvaluation_LinkService;
            _classSchedule_Evaluation_RosterService = classSchedule_Evaluation_RosterService;
            _studentEvaluationWithoutEmpService = studentEvaluationWithoutEmpService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "StudentEvaluationResults.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var iLAIDs = ExtractParameters<List<int>>(report.Filters, "SELECT ILA");
            var dateRangefilter = report.Filters.FirstOrDefault(x => x.Name == "Date Range" && !String.IsNullOrEmpty(x.Value));
            var dateRange = dateRangefilter != null ? ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE") : new List<DateTime>();
            var formIDs = ExtractParameters<List<int>>(report.Filters, "SELECT FORM");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var classEvalRosters = await _classSchedule_Evaluation_RosterService.GetEvaluationsRosterByEvalIdILAIdAsync(iLAIDs, formIDs,dateRange);
            classEvalRosters = classEvalRosters.Where(x => x.ClassScheduleId != null).Distinct().ToList();
            var classscheduleIds = classEvalRosters.Where(c=>c.ClassScheduleId!=null).Select(c => c.ClassScheduleId.Value).Distinct().ToList();
            var studentEvalWithoutEmps = await _studentEvaluationWithoutEmpService.GetStudentEvaluationWithoutEmpByClassandEvalId(classscheduleIds, formIDs);
            foreach (var classEvalRoster in classEvalRosters)
            {
                classEvalRoster.StudentEvaluationInfo.StudentEvaluationWithoutEmps = new List<StudentEvaluationWithoutEmp>();
                var filterEvalWithoutEmp = studentEvalWithoutEmps.Where(t => t.ClassScheduleId == classEvalRoster.ClassScheduleId.Value && t.StudentEvaluationId == classEvalRoster.StudentEvaluationId).ToList();
                classEvalRoster.StudentEvaluationInfo.StudentEvaluationWithoutEmps = filterEvalWithoutEmp;
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new StudentEvaluationResults(report.InternalReportTitle, templatePath, displayColumns, companyLogo, classEvalRosters, dateRange, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}