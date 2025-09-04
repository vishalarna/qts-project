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
  public  class MyDataPositionLinkagesGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _eoService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public MyDataPositionLinkagesGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        ITaskService taskService,
        IEnablingObjectiveService eoService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _eoService = eoService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "MyData/PositionLinkage.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var activePositions = ExtractParameters<string>(report.Filters, "SELECT POSITIONS");
            var taskStatus = ExtractParameters<string>(report.Filters, "TASKS LINKED TO POSITION");
            var eoStatus = ExtractParameters<string>(report.Filters, "ENABLING OBJECTIVES LINKED TO POSITION");
            var flaggedEOPosition = ExtractParameters<string>(report.Filters, "EOS FLAGGED AS SKILLS LINKED TO POSITION");
            var employeeStatus = ExtractParameters<string>(report.Filters, "EMPLOYEE");
            var useMetaEOs = ExtractParameters<bool>(report.Filters, "META EOS");
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "SELECT DATE RANGE");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var tasks = await _taskService.GetLinksForMyDataPositionLinkageAsync(activePositions, taskStatus, employeeStatus, dateRange[0], dateRange[1]);
            var enablingObjectives = await _eoService.GetLinksForMyDataPositionLinkage(activePositions, eoStatus, flaggedEOPosition, useMetaEOs, employeeStatus, dateRange[0], dateRange[1]);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new MyDataPositionLinkages(report.InternalReportTitle, templatePath, displayColumns, companyLogo, enablingObjectives, tasks, labelReplacement, defaultTimeZone);

        }

    }
}
