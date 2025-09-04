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
    public class EOPHoursForDesignatedYearsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly IEmployeeService _employeeService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EOPHoursForDesignatedYearsGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IPositionService positionService,
        IEmployeeService employeeService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _employeeService = employeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EOPHoursForDesignatedYears.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            List<Position> positions = new List<Position>();
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var positionIDs = ExtractParameters<List<int>>(report.Filters, "Positions");
            var isSummaryReport = ExtractParameters<bool>(report.Filters, "Summary Report");
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "Date Range");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            positions = await _positionService.GetPositionsByIdsAsync(positionIDs);
            var employeeIds = positions.SelectMany(x => x.EmployeePositions.Select(s => s.EmployeeId)).ToList();
            var employees = await _employeeService.GetEmployeesWithEOPHoursAsync(employeeIds);
            foreach(var employee in employees)
            {
                employee.ClassSchedule_Employee = employee.ClassSchedule_Employee.Where(x => dateRange[0] < x.ClassSchedule.EndDateTime && dateRange[1] > x.ClassSchedule.EndDateTime).ToList();
            }
            foreach(var position in positions)
            {
                foreach(var employeePosition in position.EmployeePositions)
                {
                    employeePosition.Employee = employees.Where(x => x.Id == employeePosition.EmployeeId).FirstOrDefault();
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new EOPHoursForDesignatedYears(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions,isSummaryReport, labelReplacement, defaultTimeZone);
        }
    }
}
