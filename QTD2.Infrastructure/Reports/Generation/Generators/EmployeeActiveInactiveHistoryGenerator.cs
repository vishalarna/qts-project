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
	public class EmployeeActiveInactiveHistoryGenerator : ReportModelGenerator
	{
        private readonly IEmployeeService _employeeService;
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EmployeeActiveInactiveHistoryGenerator(
           IEmployeeService employeeService, IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService)
        {
            _employeeService = employeeService;
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EmployeeActiveInactiveHistory.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            List<int> employeeIds = ExtractParameters<List<int>>(report.Filters, "EMPLOYEE");

            var employees = await _employeeService.GetEmployeesForEmployeeActiveInactiveHistory(employeeIds);

            return new EmployeeActiveInactiveHistory(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, employees);
        }
    }
}
