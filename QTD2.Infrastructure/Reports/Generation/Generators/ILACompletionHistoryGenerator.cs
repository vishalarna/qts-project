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
    public class ILACompletionHistoryGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IILAService _ilaService;
        private readonly IEmployeeService _employeeService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public ILACompletionHistoryGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
         IILAService ilaService,
         IEmployeeService employeeService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _ilaService = ilaService;
            _employeeService = employeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ILACompletionHistory.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var iLAIDs = ExtractParameters<List<int>>(report.Filters, "ILA");
            var completedStatus = ExtractParameters<string>(report.Filters, "COMPLETION TYPE");
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE");
            var activeStatus = ExtractParameters<string>(report.Filters, "ACTIVE/INACTIVE EMPLOYEES");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var iLAs = await _ilaService.GetILACompletionHistoryAsync(iLAIDs, completedStatus, dateRange[0], dateRange[1], activeStatus);
            var employees = await _employeeService.GetEmployeesWithCertificationsAsync();

            foreach (var ila in iLAs)
            {
                List<ClassSchedule> classSchedulesexclude = new List<ClassSchedule>();
                foreach (var classSchedule in ila.ClassSchedules)
                {
                    foreach (var employee in classSchedule.ClassSchedule_Employee.Where(r => r.Active && r.IsEnrolled && !r.Deleted))
                    {
                        employee.Employee.EmployeeCertifications = employees
                                .Where(r => r.Id == employee.EmployeeId)
                                .SelectMany(r => r.EmployeeCertifications)
                                .ToList();
                    }
                    if (classSchedule.ClassSchedule_Employee.Count() == 0)
                    {
                        classSchedulesexclude.Add(classSchedule);
                    }
                }
                ila.ClassSchedules = ila.ClassSchedules.Except(classSchedulesexclude).ToList();
            }
            iLAs = iLAs.Where(r => r.ClassSchedules.Count() > 0).ToList();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new ILACompletionHistory(report.InternalReportTitle, templatePath, displayColumns, companyLogo, iLAs, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}
