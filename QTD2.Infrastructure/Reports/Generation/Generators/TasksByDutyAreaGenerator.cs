using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class TasksByDutyAreaGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskService _taskService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IRR_Task_LinkService _rR_Task_LinkService;
        public TasksByDutyAreaGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          ITaskService taskService,
          IEnablingObjectiveService enablingObjectiveService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          IRR_Task_LinkService rR_Task_LinkService
          )
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _enablingObjectiveService = enablingObjectiveService;
            _rR_Task_LinkService = rR_Task_LinkService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "MyData/TasksByDutyArea.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";

            var taskIds = ExtractParameters<List<int>>(report.Filters, "SELECT TASKS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var tasks = await _taskService.GetTasksByTaskIdsAsync(taskIds);
            var uniquesTaskIds = tasks.Select(m => m.Id).Distinct().ToList();
            var rrTaskLinks = await _rR_Task_LinkService.GetRRTaskLinksByTaskIdsAsync(uniquesTaskIds);
            var uniqueEoIds = tasks.SelectMany(task => task.Task_EnablingObjective_Links.Select(link => link.EnablingObjective.Id)).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByIDs(uniqueEoIds);

            var enablingObjectivesDict = enablingObjectives.ToDictionary(eo => eo.Id);

            foreach (var task in tasks)
            {
                task.RR_Task_Links = rrTaskLinks.Where(x => x.TaskId == task.Id).ToList();
                foreach (var eoLink in task.Task_EnablingObjective_Links)
                {
                    if (enablingObjectivesDict.TryGetValue(eoLink.EnablingObjectiveId, out var enablingObjective))
                    {
                        eoLink.EnablingObjective = enablingObjective;
                    }
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TasksByDutyAreaModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, tasks.ToList(), labelReplacement, defaultTimeZone);
        }
    }
}
