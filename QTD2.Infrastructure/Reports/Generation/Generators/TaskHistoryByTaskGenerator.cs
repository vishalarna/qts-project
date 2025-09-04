using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class TaskHistoryByTaskGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IVersion_TaskService _version_TaskService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TaskHistoryByTaskGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
         IVersion_TaskService version_TaskService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _version_TaskService = version_TaskService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TaskHistoryByTask.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var taskIds = ExtractParameters<List<int>>(report.Filters, "SELECT TASKS");
            var dateRangefilter = report.Filters.FirstOrDefault(x => x.Name == "Date Range" && !String.IsNullOrEmpty(x.Value));
            var dateRange = dateRangefilter != null ? ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE") : new List<DateTime>();
            var version_Tasks = await _version_TaskService.GetTaskHistoryAsync(taskIds, dateRange);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TaskHistoryByTask(report.InternalReportTitle, templatePath, displayColumns, companyLogo, version_Tasks, dateRange,labelReplacement, defaultTimeZone);
        }
    }
}