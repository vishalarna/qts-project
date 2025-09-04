using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class SCORMTestCompletionStatisticsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IEmployeeService _employeeService;

        public SCORMTestCompletionStatisticsGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            IClassScheduleService classScheduleService,
            IEmployeeService employeeService
        )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _classScheduleService = classScheduleService;
            _employeeService = employeeService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "SCORMTestCompletionStatistics.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var companyLogo = "";
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat ?? "MM/dd/yyyy";
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var classScheduleIds = ExtractParameters<List<int>>(report.Filters, "SELECT CLASS");

            var classSchedules = (await _classScheduleService.GetForSCORMTestCompletionStatistics(classScheduleIds)).ToList();

            var employeeIds = classSchedules.SelectMany(cs =>
            {
                var cbt = cs.ILA?.CBTs?.FirstOrDefault(c => c.Active == true);
                if (cbt == null) return Enumerable.Empty<int>();

                var scormUpload = cbt.ScormUploads?.FirstOrDefault(su => su.Active == true && su.ScormStatus == "Uploaded");
                if (scormUpload == null) return Enumerable.Empty<int>();

                var questionChoices = scormUpload.CBT_ScormUpload_Question?.SelectMany(q => q.CBT_ScormUpload_Question_Choices.SelectMany(c => c.CBT_ScormRegistration_Responses.Where(r => r.CBT_ScormRegistration.ClassScheduleEmployee != null)  // Ensure ClassScheduleEmployee is not null
                            .Select(r => r.CBT_ScormRegistration.ClassScheduleEmployee.EmployeeId)));
                return questionChoices ?? Enumerable.Empty<int>();
            }).Distinct().ToList();

            var employees = (await _employeeService.GetEmployeesPersonDetailsByEmpIds(employeeIds)).ToList();

            return new SCORMTestCompletionStatistics(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, classSchedules, employees, defaultDateFormat);

        }
    }
}
