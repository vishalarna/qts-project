using DocumentFormat.OpenXml.Wordprocessing;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Generators.Helpers.Interfaces;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class SafetyHazardsByTaskGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITaskService _taskService;
        private readonly ISaftyHazardService _saftyHazardService;
        public SafetyHazardsByTaskGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          ITaskService taskService,
          ISaftyHazardService saftyHazardService
          )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
            _saftyHazardService = saftyHazardService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "SafetyHazardsByTask.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            var taskIds = ExtractParameters<List<int>>(report.Filters, "SELECT TASKS");
            var includeSafetyHazardsDetail = ExtractParameters<bool>(report.Filters, "INCLUDE SAFETY HAZARD DETAILS");
            var includeInactiveSafetyHazards = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE SAFETY HAZARDS");

            var tasks = await _taskService.GetSafetyHazardsByTaskIdsAsync(taskIds);
            var safetyHazardIds = tasks.SelectMany(p => p.SafetyHazard_Task_Links).Select(shtl => shtl.SaftyHazardId).Distinct().ToList();
            var metaTaskSafetyHazardIds = tasks.SelectMany(x => x.Task_MetaTask_Links).SelectMany(m => m.Task.SafetyHazard_Task_Links).Select(shtl => shtl.SaftyHazardId).Distinct().ToList();
            var allSafetyHazardIds = safetyHazardIds.Concat(metaTaskSafetyHazardIds).Distinct().ToList();
            var safetyHazards = await _saftyHazardService.GetForSafetyHazardsByPositionMatrix(allSafetyHazardIds, includeInactiveSafetyHazards);
            foreach (var task in tasks)
            {
                foreach (var link in task.SafetyHazard_Task_Links)
                {
                    link.SaftyHazard = safetyHazards.FirstOrDefault(sh => sh.Id == link.SaftyHazardId);
                }
            }
            return new SafetyHazardsByTaskModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, tasks, labelReplacement, defaultTimeZone, includeSafetyHazardsDetail);
        }
    }
}
