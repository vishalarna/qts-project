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
    public class EnablingObjectivesByTaskGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IILAService _ilaService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EnablingObjectivesByTaskGenerator(
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
            string templatePath = "EnablingObjectivesByTask.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var taskIds = ExtractParameters<List<int>>(report.Filters, "TASKS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var tasks = await _taskService.GetEnablingObjectivesByTaskAsync(taskIds);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new EnablingObjectivesByTask(report.InternalReportTitle, templatePath, displayColumns, companyLogo, tasks.ToList(), labelReplacement, defaultTimeZone);
        }
    }
}
