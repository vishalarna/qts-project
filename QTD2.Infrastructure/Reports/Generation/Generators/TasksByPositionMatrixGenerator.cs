
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
    public class TasksByPositionMatrixGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TasksByPositionMatrixGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
         ITaskService taskService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
         IPositionService positionService
          )
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _positionService = positionService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TasksByPositionMatrix.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";

            var positionIds = ExtractParameters<List<int>>(report.Filters, "POSITIONS");
            var includeInactiveTasks = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE TASKS");
            var rrTasks = ExtractParameters<bool>(report.Filters, "RELIABILITY RELATED TASKS ONLY");
            var includeMetaTask = ExtractParameters<bool>(report.Filters, "INCLUDE META TASKS");
            var includePsuedoTask = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var positions = await _positionService.GetPositionTasksByIdsAsync(positionIds);
            var taskIds = positions.SelectMany(x => x.Position_Tasks.Select(p => p.TaskId)).Distinct().ToList();

            var tasks = await _taskService.GetTasksByIdsAsync(taskIds, rrTasks, includeMetaTask, includeInactiveTasks, includePsuedoTask);

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new TasksbyPositionMatrixModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions, tasks, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}