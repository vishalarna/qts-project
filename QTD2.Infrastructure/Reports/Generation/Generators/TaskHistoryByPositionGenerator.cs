using QTD2.Domain.Entities.Core;
using QTD2.Domain.Helpers;
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
    public class TaskHistoryByPositionGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITaskService _taskService;

        public TaskHistoryByPositionGenerator(IClientUserSettings_GeneralSettingService generalSettingService,
          IPositionService positionService, IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService, ITaskService taskService
        )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TaskHistoryByPosition.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            List<Position> positions = new List<Position>();
            var positionIds = ExtractParameters <List<int>>(report.Filters, "Positions");
            var excludePseudoTasks = ExtractParameters <bool>(report.Filters, "Exclude Pseudo Tasks");
            var includeTaskModificationDetails = ExtractParameters<bool>(report.Filters, "Include Task Modification Details");
            var includeRRTasks = ExtractParameters<bool>(report.Filters, "Include *R-R Tasks Only");
            var allTasksChange = ExtractParameters<DateTime>(report.Filters, "ALL TASKS CHANGED SINCE");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            positions = await _positionService.GetPositionTaskHistoryAsync(positionIds);
            var distinctTaskIds = positions.SelectMany(e => e.Position_Tasks).Select(pt => pt.TaskId).Distinct().ToList();
            var tasks = await _taskService.GetTasksHistoryByTaskIdsAsync(distinctTaskIds);

            foreach (var position in positions)
            {
                foreach (var posTask in position.Position_Tasks)
                {
                    posTask.Task = tasks.FirstOrDefault(x => x.Id == posTask.TaskId);
                }

                position.Position_Tasks = position.Position_Tasks.Where(r => r.Task.Version_Tasks.Any(s => s.EffectiveDate > DateOnly.FromDateTime(allTasksChange))).ToList();

                if (includeRRTasks)
                {
                    position.Position_Tasks = position.Position_Tasks.Where(r => r.Task.IsReliability).ToList();
                }

                if (!excludePseudoTasks)
                {
                    position.Position_Tasks = position.Position_Tasks.Where(r => r.Task.SubdutyArea.DutyArea.Letter != "P").ToList();
                }

                foreach (var positionTask in position.Position_Tasks)
                {
                    positionTask.Task.Version_Tasks = positionTask.Task.Version_Tasks.Where(r => r.EffectiveDate > DateOnly.FromDateTime(allTasksChange)).ToList();
                }
            }

            foreach (var position in positions)
            {
                var taskLoopData = new Dictionary<Domain.Entities.Core.Position_Task, string>();
                foreach(var positionTask in position.Position_Tasks)
                {
                    var task = positionTask.Task;
                    taskLoopData.Add(positionTask, task.SubdutyArea.DutyArea.Letter + task.SubdutyArea.DutyArea.Number + "." + task.SubdutyArea.SubNumber + "." + task.Number);
                }
                taskLoopData = taskLoopData.OrderBy(str => str.Value, new AlphaNumericSortHelper()).ToDictionary(task => task.Key, task => task.Value);
                position.Position_Tasks = taskLoopData.Keys.ToList();
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            positions = positions.Where(x => x.Position_Tasks != null && x.Position_Tasks.Count > 0).ToList();
            return new TaskHistoryByPositionModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions, labelReplacement, defaultTimeZone);
        }

    }
}
