using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Helpers;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class TaskQualificationSheetsByTaskGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TaskQualificationSheetsByTaskGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IPositionService positionService,
         ITaskService taskService,
         IEnablingObjectiveService enablingObjectiveService,
         ITaskQualificationService taskQualificationService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _taskService = taskService;
            _enablingObjectiveService = enablingObjectiveService;
            _taskQualificationService = taskQualificationService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TaskQualificationSheets.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var taskIds = ExtractParameters<List<int>>(report.Filters, "TASK");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var positions = (await _positionService.GetPositionsAsync()).Where(x => x.Active);
            taskIds = positions.SelectMany(r => r.Position_Tasks.Where(s => taskIds.Contains(s.TaskId)).Select(t => t.TaskId)).Distinct().ToList();
            var tasks = await getPositionTasksAsync(taskIds, positions.Select(r => r.Id).ToList());
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            
            return new TaskQualificationSheets(report.InternalReportTitle, templatePath, displayColumns, companyLogo, tasks.ToList(), labelReplacement, "Task", defaultTimeZone);

        }
        private async System.Threading.Tasks.Task<List<Domain.Entities.Core.Task>> getPositionTasksAsync(List<int> taskIds, List<int> positionIds)
        {
            var tasks = await _taskService.GetTasksByTaskIdsAsync(taskIds);
            var eoIDsLinkedToTasks = tasks.SelectMany(r => r.Task_EnablingObjective_Links.Select(t => t.EnablingObjectiveId)).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByEOIDs(eoIDsLinkedToTasks);
            var taskLoopData = new Dictionary<Domain.Entities.Core.Task, string>();
            foreach (var task in tasks)
            {
                foreach (var eo in task.Task_EnablingObjective_Links)
                {
                    eo.EnablingObjective = enablingObjectives.Where(r => r.Id == eo.EnablingObjectiveId).FirstOrDefault();
                }

                task.Task_EnablingObjective_Links = task.Task_EnablingObjective_Links.OrderBy(eo => eo.EnablingObjective?.FullNumber, new AlphaNumericSortHelper()).ToList();
                task.Position_Tasks = task.Position_Tasks.Where(r => positionIds.Contains(r.PositionId)).ToList();

                taskLoopData.Add(task, task.SubdutyArea.DutyArea.Letter + task.SubdutyArea.DutyArea.Number + "." + task.SubdutyArea.SubNumber + "." + task.Number);
            }

            taskLoopData = taskLoopData.OrderBy(str => str.Value, new AlphaNumericSortHelper()).ToDictionary(task => task.Key, task => task.Value);
            tasks = taskLoopData.Keys.ToList();

            return tasks.ToList();
        }
    }
}
