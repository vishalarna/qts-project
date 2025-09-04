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
    public class TrainingMaterialForTaskEOByPositionsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        public TrainingMaterialForTaskEOByPositionsGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         IPositionService positionService,
         IEnablingObjectiveService enablingObjectiveService,
         ITaskService taskService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
         )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _taskService = taskService;
            _enablingObjectiveService = enablingObjectiveService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TrainingMaterialForTaskEOByPositions.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var positionIDs = ExtractParameters<List<int>>(report.Filters, "Positions");
            var showTrainingResources = ExtractParameters<bool>(report.Filters, "Show Training Resources");
            var includeILAsOfEOTask = ExtractParameters<bool>(report.Filters, "Include ILAs that cover the EO associated with the Task");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var positions = await _positionService.GetPositionTasksByIdsAsync(positionIDs);
            var taskIds = positions.SelectMany(x => x.Position_Tasks.Select(s => s.TaskId)).Distinct().ToList();
            var tasks = await _taskService.GetILAsLinkedToTaskAndEoAsync(taskIds, includeILAsOfEOTask,showTrainingResources);
            if (includeILAsOfEOTask)
            {
                var eoIds = tasks.SelectMany(x => x.Task_EnablingObjective_Links.Select(a => a.EnablingObjectiveId)).Distinct().ToList();
                var eos = await _enablingObjectiveService.GetILAsLinkedToEnablingObjectives(eoIds, showTrainingResources);
                tasks.ForEach(x =>
                {
                    x.Task_EnablingObjective_Links.ToList().ForEach(r => r.EnablingObjective = eos.Where(d => d.Id == r.EnablingObjectiveId).ToList().FirstOrDefault());
                });
            }
            tasks.ForEach(x =>
            {
                x.ILA_TaskObjective_Links = x.ILA_TaskObjective_Links.Where(x => x.ILA.Active && !x.ILA.Deleted).ToList();
                x.Task_EnablingObjective_Links = x.Task_EnablingObjective_Links.Where(x => x.EnablingObjective != null).ToList();
                x.Task_EnablingObjective_Links.ToList().ForEach(r => r.EnablingObjective.ILA_EnablingObjective_Links = r.EnablingObjective.ILA_EnablingObjective_Links.Where(d => d.ILA.Active && !d.ILA.Deleted).ToList());
            });
            foreach (var position in positions)
            {
                List<Position_Task> positionTaskToExclude = new List<Position_Task>();
                foreach (var positionTask in position.Position_Tasks)
                {
                    var filteredTasks = tasks.Where(x => x.Id == positionTask.TaskId);
                    filteredTasks = includeILAsOfEOTask ? filteredTasks.Where(x => x.ILA_TaskObjective_Links.Count() > 0 || x.Task_EnablingObjective_Links.Any(s => s.EnablingObjective.ILA_EnablingObjective_Links.Count() > 0)) : filteredTasks.Where(x => x.ILA_TaskObjective_Links.Count() > 0);
                    if (filteredTasks.Count() > 0)
                    {
                        positionTask.Task = filteredTasks.FirstOrDefault();
                    }
                    else
                    {
                        positionTaskToExclude.Add(positionTask);
                    }
                }
                position.Position_Tasks = position.Position_Tasks.Except(positionTaskToExclude).ToList();
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TrainingMaterialForTaskEOByPositions(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions, includeILAsOfEOTask,showTrainingResources, labelReplacement, defaultTimeZone);
        }

    }
}
