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
    public class TaskByPositionModelGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskService _taskService;
        private readonly IPositionService _positionService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TaskByPositionModelGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            ITaskService taskService,
            IPositionService positionService,
            IEnablingObjectiveService enablingObjectiveService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
            )
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _positionService = positionService;
            _enablingObjectiveService = enablingObjectiveService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TaskByPosition.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var positionIds = ExtractParameters<List<int>>(report.Filters, "POSITIONS");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var tasksType = ExtractParameters<string>(report.Filters, "SELECT TASK TYPE");
            var rrTasksOnly = ExtractParameters<bool>(report.Filters, "RR TASKS ONLY");
            var activeInactiveTask = ExtractParameters<string>(report.Filters, "ALL/ACTIVE/INACTIVE TASKS");
            var taskGroupfilter = report.Filters.FirstOrDefault(x => x.Name == "Select Task Group" && !String.IsNullOrEmpty(x.Value));
            var taskGroupIds = taskGroupfilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT TASK GROUP") : new List<int>();

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var positions = await _positionService.GetPositionTasksByIdsAsync(positionIds);
            var taskIds = positions.SelectMany(x => x.Position_Tasks.Select(p => p.TaskId)).Distinct().ToList();
            var tasks = (await _taskService.GetTaskDetailsByTaskIdsAsync(positionIds,taskIds, includePseudoTasks, tasksType, activeInactiveTask,rrTasksOnly, taskGroupIds)).ToList();

            var eoIds = tasks.SelectMany(x => x.Task_EnablingObjective_Links.Select(a => a.EnablingObjectiveId)).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByIDs(eoIds);
            var activeEnablingObjectives = enablingObjectives.Where(eo => eo.Active).ToList();
            tasks.ForEach(x =>
            {
                x.Task_EnablingObjective_Links.ToList().ForEach(a =>
                {
                    a.EnablingObjective = activeEnablingObjectives.Where(s => s.Id == a.EnablingObjectiveId).FirstOrDefault();
                });
                x.Task_EnablingObjective_Links = x.Task_EnablingObjective_Links.Where(x => x.EnablingObjective != null).ToList();
            });

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            return new TaskByPosition(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions.ToList(), tasks.ToList(), labelReplacement, defaultTimeZone, taskGroupIds);
        }
    }
}
