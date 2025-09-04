using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class TasksByEnablingObjectivesGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly ITaskService _taskService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TasksByEnablingObjectivesGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IEnablingObjectiveService enablingObjectiveService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            ITaskService taskService
            )
        {
            _generalSettingService = generalSettingService;
            _enablingObjectiveService = enablingObjectiveService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TasksByEnablingObjectives.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var enablingObjectiveIds = ExtractParameters<List<int>>(report.Filters, "SELECT ENABLING OBJECTIVE");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var onlyShowEosWithTasksLinked = ExtractParameters<bool>(report.Filters, "ONLY SHOW EOS WITH TASKS LINKED");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var enablingObjectives = await _enablingObjectiveService.GetTasksByEnablingObjectivesAsync(enablingObjectiveIds);
            var distinctTaskIds = enablingObjectives.SelectMany(eo => eo.Task_EnablingObjective_Links).Select(link => link.TaskId).Distinct().ToList();
            var distinctTaskMetaIds = enablingObjectives.SelectMany(x => x.EnablingObjective_MetaEO_Links).SelectMany(x => x.EnablingObjective.Task_EnablingObjective_Links.Select(x => x.TaskId)).Distinct().ToList();
            var tasksWithDutySubDutyArea = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds);
            var tasksWithDutySubdutyAreaForMeta = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskMetaIds);

            if (!includePseudoTasks)
            {
                tasksWithDutySubDutyArea = tasksWithDutySubDutyArea.Where(x => x.SubdutyArea?.DutyArea?.Letter.ToUpper() != "P").ToList();
                tasksWithDutySubdutyAreaForMeta = tasksWithDutySubdutyAreaForMeta.Where(x => x.SubdutyArea?.DutyArea?.Letter.ToUpper() != "P").ToList();
            }
            if (onlyShowEosWithTasksLinked)
            {
                enablingObjectives = enablingObjectives.Where(enablingObjective => enablingObjective.Task_EnablingObjective_Links.Any(r => r.Active)).ToList();
            }

            foreach (var enablingObjective in enablingObjectives)
            {
                
                foreach (var taskLink in enablingObjective.Task_EnablingObjective_Links)
                {
                    var task = tasksWithDutySubDutyArea.FirstOrDefault(t => t.Id == taskLink.TaskId);
                    taskLink.Task = task;
                }
                if (onlyShowEosWithTasksLinked)
                {
                    enablingObjective.EnablingObjective_MetaEO_Links = enablingObjective.EnablingObjective_MetaEO_Links.Where(x => x.EnablingObjective.Task_EnablingObjective_Links.Count() > 0).ToList();
                }
                foreach (var metaLink in enablingObjective.EnablingObjective_MetaEO_Links)
                {
                    
                    foreach (var taskLinks in metaLink.EnablingObjective.Task_EnablingObjective_Links)
                    {
                        var tasks = tasksWithDutySubdutyAreaForMeta.FirstOrDefault(t => t.Id == taskLinks.TaskId);
                        taskLinks.Task = tasks;
                    }
                }
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            return new TasksByEnablingObjectivesModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, enablingObjectives.ToList(), includePseudoTasks, labelReplacement, defaultTimeZone);
        }
    }
}
