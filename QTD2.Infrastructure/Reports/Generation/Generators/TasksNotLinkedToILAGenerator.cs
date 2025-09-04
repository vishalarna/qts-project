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
    public class TasksNotLinkedToILAGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskService _taskService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TasksNotLinkedToILAGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
         ITaskService taskService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TasksNotLinkedToILA.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";

            var activeStatus = ExtractParameters<string>(report.Filters, "ACTIVE/INACTIVE/ALL TASKS");
            var rrTasks = ExtractParameters<bool>(report.Filters, "RELIABILITY RELATED TASKS");
            var includeMetaTask = ExtractParameters<bool>(report.Filters, "INCLUDE META TASKS");
            var includePsuedoTask = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var tasks = await _taskService.GetTasksNotLinkedToILAAsync(activeStatus, rrTasks, includeMetaTask, includePsuedoTask);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new TasksNotLinkedToILAModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, tasks, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}
