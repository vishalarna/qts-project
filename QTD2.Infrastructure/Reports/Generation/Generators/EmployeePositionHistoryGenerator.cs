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
   public class EmployeePositionHistoryGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEmployeePositionService _employeePositionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EmployeePositionHistoryGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IEmployeePositionService employeePositionService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _employeePositionService = employeePositionService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "MyData/EmployeePositionHistory.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var employees = ExtractParameters<List<int>>(report.Filters, "EMPLOYEE");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var empPositions = await _employeePositionService.GetEmployeesPositionsAsync("", employees);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new EmployeePositionHistory(report.InternalReportTitle, templatePath, displayColumns, companyLogo, empPositions, labelReplacement, defaultTimeZone);
        }
    }
}
