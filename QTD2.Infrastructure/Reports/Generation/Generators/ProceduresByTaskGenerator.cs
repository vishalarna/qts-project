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
    public class ProceduresByTaskGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskService _taskService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        public ProceduresByTaskGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          ITaskService taskService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "MyData/ProceduresByTask.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";

            var taskIds = ExtractParameters<List<int>>(report.Filters, "SELECT TASKS");
            var includeInactive = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE PROCEDURES");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var tasks = await _taskService.GetProceduresByTaskAsync(taskIds, includeInactive);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            } 
            return new ProceduresByTaskModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, tasks, labelReplacement, defaultTimeZone, includeInactive);
        }
    }
}
