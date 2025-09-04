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
    public class ReliabilityRelatedTaskImpactMatrixR5Generator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IPosition_TaskService _position_TaskService;
        private readonly ITaskService _taskService;

        public ReliabilityRelatedTaskImpactMatrixR5Generator(
            IPosition_TaskService position_TaskService,
            ITaskService taskService,
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService)
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _position_TaskService = position_TaskService;
            _taskService = taskService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ReliabilityRelatedTaskImpactMatrixR5.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var positionTaskIds = ExtractParameters<List<int>>(report.Filters, "SELECT TASKS");
            var companyLogo = "";
            var defaultTimeZone = "";

            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var positionTasks = await _position_TaskService.GetPositionTasksForReliabilityRelatedTaskImpactMatrixR5(positionTaskIds);
            var distinctTaskIds = positionTasks.Select(pt => pt.TaskId).ToList();
            distinctTaskIds.AddRange(positionTasks.SelectMany(pt => pt.R5ImpactedTasks.Select(r5 => r5.ImpactedTaskId)));
            distinctTaskIds = distinctTaskIds.Distinct().ToList();
            var tasks = await _taskService.GetTasksWithDutySubDutyAreaPositionTaskPositionsByTaskIdsAsync(distinctTaskIds.ToList());
            foreach (var positionTask in positionTasks)
            {
                positionTask.Task = tasks.Where(t => t.Id == positionTask.TaskId).FirstOrDefault();

                foreach (var r5ImpactedTask in positionTask.R5ImpactedTasks)
                {
                    r5ImpactedTask.ImpactedTask = tasks.Where(x => x.Id == r5ImpactedTask.ImpactedTaskId).FirstOrDefault();
                }
            }

            return new ReliabilityRelatedTaskImpactMatrixR5(report.InternalReportTitle, templatePath, displayColumns, companyLogo, labelReplacement, defaultTimeZone, positionTasks);
        }
    }
}
