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
    public class OJTGuideByILAGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IILAService _ilaService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public OJTGuideByILAGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IPositionService positionService,
         ITaskService taskService,
         IEnablingObjectiveService enablingObjectiveService,
         IILAService ilaService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _taskService = taskService;
            _enablingObjectiveService = enablingObjectiveService;
            _ilaService = ilaService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "OJTGuideTemplate.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var positions = await _positionService.GetPositionsAsync();
            var iLAID = ExtractParameters<int>(report.Filters, "ILA");
            var iLA = await _ilaService.GetAllILAsByILAIdAsync(iLAID);
            var iLAName = iLA.FirstOrDefault(x => x.Id == iLAID)?.Name;
            var taskIds = iLA.SelectMany(r => r.ILA_TaskObjective_Links.Select(s => s.TaskId)).Distinct().ToList();
            var isRRPositions = ExtractParameters<bool>(report.Filters, "SHOW RR ONLY");
            var taskStatus = ExtractParameters<string>(report.Filters, "TASK STATUS");
            var tasks = await getPositionTasksAsync(taskIds, isRRPositions, taskStatus);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new OJTGuide(report.InternalReportTitle, templatePath, displayColumns, companyLogo, tasks, "ILA", iLAName, labelReplacement, defaultTimeZone);
        }
        private async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Task>> getPositionTasksAsync(List<int> taskIds, bool isRRPositions, string taskStatus,string isPseudoTasks = "")
        {
            var tasks = await _taskService.GetTasksByTaskIdsAsync(taskIds);
            var eoIDsLinkedToTasks = tasks.SelectMany(r => r.Task_EnablingObjective_Links.Select(t => t.EnablingObjectiveId)).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByEOIDs(eoIDsLinkedToTasks);
            var taskLoopData = new Dictionary<QTD2.Domain.Entities.Core.Task, string>();
            List<QTD2.Domain.Entities.Core.Task> tasksToExclude = new List<QTD2.Domain.Entities.Core.Task>();
            foreach (var task in tasks)
            {
                foreach (var eo in task.Task_EnablingObjective_Links)
                {
                    eo.EnablingObjective = enablingObjectives.Where(r => r.Id == eo.EnablingObjectiveId).FirstOrDefault();
                }
                task.Task_EnablingObjective_Links = task.Task_EnablingObjective_Links.OrderBy(eo => eo.EnablingObjective?.FullNumber, new AlphaNumericSortHelper()).ToList();
                taskLoopData.Add(task, task.SubdutyArea.DutyArea.Letter + task.SubdutyArea.DutyArea.Number + "." + task.SubdutyArea.SubNumber + "." + task.Number);
                if (isPseudoTasks != "" && !Convert.ToBoolean(isPseudoTasks) && task.SubdutyArea.DutyArea.Letter == "P")
                {
                    tasksToExclude.Add(task);
                }
                if (isRRPositions && !task.IsReliability)
                {
                    tasksToExclude.Add(task);
                }
                if (taskStatus == "Active Only" && !task.Active)
                {
                    tasksToExclude.Add(task);
                }
                if (taskStatus == "Inactive Only" && task.Active)
                {
                    tasksToExclude.Add(task);
                }
            }
            taskLoopData = taskLoopData.OrderBy(str => str.Value, new AlphaNumericSortHelper()).ToDictionary(task => task.Key, task => task.Value);
            tasks = taskLoopData.Keys.Except(tasksToExclude).ToList();
            return tasks.ToList();
        }
    }
}
