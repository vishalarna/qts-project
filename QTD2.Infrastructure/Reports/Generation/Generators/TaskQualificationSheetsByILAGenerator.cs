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
  public  class TaskQualificationSheetsByILAGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IILAService _ilaService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TaskQualificationSheetsByILAGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IPositionService positionService,
         ITaskService taskService,
         IEnablingObjectiveService enablingObjectiveService,
         IILAService ilaService,
          ITaskQualificationService taskQualificationService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _taskService = taskService;
            _enablingObjectiveService = enablingObjectiveService;
            _ilaService = ilaService;
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
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var positions = await _positionService.GetPositionsAsync();
            var iLAID = ExtractParameters<int>(report.Filters, "ILA");
            var isRRPositions = ExtractParameters<bool>(report.Filters, "ONLY R-R* FOR ANY OF THE POSITIONS");
            var isPseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var taskStatus = ExtractParameters<string>(report.Filters, "TASK STATUS");
            var iLA = await _ilaService.GetAllILAsByILAIdAsync(iLAID);
            var taskIds = iLA.SelectMany(r => r.ILA_TaskObjective_Links.Select(s => s.TaskId)).Distinct().ToList();
            var tasks = await getPositionTasksAsync(taskIds, positions.Select(r => r.Id).ToList(), isPseudoTasks, isRRPositions, taskStatus);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TaskQualificationSheets(report.InternalReportTitle, templatePath, displayColumns, companyLogo, tasks.ToList(), labelReplacement, "ILA", defaultTimeZone);

        }
        private async System.Threading.Tasks.Task<List<Domain.Entities.Core.Task>> getPositionTasksAsync(List<int> taskIds, List<int> positionIds, bool isPseudoTasks = false, bool isRRPositions = false, string taskStatus = "")
        {
            var tasks = await _taskService.GetTasksByTaskIdsAsync(taskIds);
            if (taskStatus == "Active Only")
            {
                tasks = tasks.Where(x => x.Active).ToList();
            }
            else if (taskStatus == "Inactive Only")
            {
                tasks = tasks.Where(x => !x.Active).ToList();
            }
            var eoIDsLinkedToTasks = tasks.SelectMany(r => r.Task_EnablingObjective_Links.Select(t => t.EnablingObjectiveId)).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByEOIDs(eoIDsLinkedToTasks);
            List<Domain.Entities.Core.Task> tasksToExclude = new List<Domain.Entities.Core.Task>();
            var taskLoopData = new Dictionary<Domain.Entities.Core.Task, string>();
            foreach (var task in tasks)
            {
                if (!isPseudoTasks && task.SubdutyArea.DutyArea.Letter == "P")
                {
                    tasksToExclude.Add(task);
                }
                if (isRRPositions && !task.IsReliability)
                {
                    tasksToExclude.Add(task);
                }

                foreach (var eo in task.Task_EnablingObjective_Links)
                {
                    eo.EnablingObjective = enablingObjectives.Where(r => r.Id == eo.EnablingObjectiveId && r.Active).FirstOrDefault();
                }

                task.Task_EnablingObjective_Links = task.Task_EnablingObjective_Links.OrderBy(eo => eo.EnablingObjective?.FullNumber, new AlphaNumericSortHelper()).ToList();
                task.Position_Tasks = task.Position_Tasks.Where(r => positionIds.Contains(r.PositionId)).ToList();

                taskLoopData.Add(task, task.SubdutyArea.DutyArea.Letter + task.SubdutyArea.DutyArea.Number + "." + task.SubdutyArea.SubNumber + "." + task.Number);
            }

            taskLoopData = taskLoopData.OrderBy(str => str.Value, new AlphaNumericSortHelper()).ToDictionary(task => task.Key, task => task.Value);
            tasks = taskLoopData.Keys.Except(tasksToExclude).ToList();

            return tasks.ToList();
        }
    }
}
