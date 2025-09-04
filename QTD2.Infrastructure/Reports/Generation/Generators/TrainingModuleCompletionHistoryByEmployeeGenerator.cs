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
   public class TrainingModuleCompletionHistoryByEmployeeGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEmployeeService _employeeService;
        private readonly IMetaILAService _metaILAService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TrainingModuleCompletionHistoryByEmployeeGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         IMetaILAService metaILAService,
         IEmployeeService employeeService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
         )
        {
            _generalSettingService = generalSettingService;
            _metaILAService = metaILAService;
            _employeeService = employeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TrainingModuleCompletionHistoryByEmployee.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var employeeIds = ExtractParameters<List<int>>(report.Filters, "EMPLOYEE");
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE");
            var trainingOptions = ExtractParameters<string>(report.Filters, "TRAINING MODULE");
            var includeInActiveEmployee = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE EMPLOYEES");
            var includeInActiveILAs = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE ILAs");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var employees = await _employeeService.GetEmployeesByListOfEmpIds(employeeIds);
            var metaILAs = await _metaILAService.GetTrainingModuleCompletionHistoryByEmployeeAsync(employeeIds, trainingOptions, includeInActiveEmployee, dateRange[0], dateRange[1], includeInActiveILAs);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TrainingModuleCompletionHistoryByEmployee(report.InternalReportTitle, templatePath, displayColumns, companyLogo, metaILAs, employees, labelReplacement, defaultTimeZone);
        }
    }

}

