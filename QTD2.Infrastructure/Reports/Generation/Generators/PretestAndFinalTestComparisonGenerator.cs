using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class PretestAndFinalTestComparisonGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly ITestService _testService;
        private readonly IClassSchedule_RosterService _classSchedule_RosterService;
        private readonly IEmployeeService _employeeService;

        public PretestAndFinalTestComparisonGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          IClassScheduleService classScheduleService,
          ITestService testService,
          IClassSchedule_RosterService classSchedule_RosterService, IEmployeeService employeeService
          )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _classScheduleService = classScheduleService;
            _testService = testService;
            _classSchedule_RosterService = classSchedule_RosterService;
            _employeeService = employeeService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        { 
            string templatePath = "PretestAndFinalTestComparison.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var classScheduleIds = ExtractParameters<List<int>>(report.Filters, "Select Classes");
            var employeeFilter = report.Filters.FirstOrDefault(x => x.Name == "Select Employees" && !String.IsNullOrEmpty(x.Value));
            var employeeIds = employeeFilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES") : new List<int>();
            var includeTestItemDetails = ExtractParameters<bool>(report.Filters, "Include Test Items Details");
            var showOnlyFailedPretestGrades = ExtractParameters<bool>(report.Filters, "Show only Failed Pretest Grades");

            var classSchedules = await _classScheduleService.GetForPretestAndFinalTestComparison(classScheduleIds);
            var distinctEmpIds = classSchedules.SelectMany(x => x.ClassSchedule_Employee).Select(m => m.EmployeeId).Distinct().ToList();
            var employees = await _employeeService.GetEmployeesPersonDetailsByEmpIds(distinctEmpIds);
            var employeeWithCertifications = await _employeeService.GetWithCertifications(distinctEmpIds);

            var certificationsLookup = employeeWithCertifications.ToDictionary(m => m.Id, m => m.EmployeeCertifications);

            foreach (var cls in classSchedules)
            {
                foreach (var clsEmp in cls.ClassSchedule_Employee)
                {
                    var employee = employees.FirstOrDefault(m => m.Id == clsEmp.EmployeeId);

                    clsEmp.Employee = employee;

                    if (employee != null && certificationsLookup.TryGetValue(clsEmp.EmployeeId, out var certifications))
                    {
                        clsEmp.Employee.EmployeeCertifications = certifications;
                    }
                }
            }
            List<int> testIds = new List<int>();
            testIds.AddRange(classSchedules.Where(cs => cs.ClassSchedule_TestReleaseEMPSettings != null && cs.ClassSchedule_TestReleaseEMPSettings.PreTestId != null).Select(cs => (int)cs.ClassSchedule_TestReleaseEMPSettings.PreTestId));
            testIds.AddRange(classSchedules.Where(cs => cs.ClassSchedule_TestReleaseEMPSettings != null && cs.ClassSchedule_TestReleaseEMPSettings.FinalTestId != null).Select(cs => (int)cs.ClassSchedule_TestReleaseEMPSettings.FinalTestId));
            testIds = testIds.Distinct().ToList();

            var tests = await _testService.GetForPretestAndFinalTestComparison(testIds);

            var classScheduleRosters = await _classSchedule_RosterService.GetForPretestAndFinalTestComparison(classScheduleIds, employeeIds);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new PretestAndFinalTestComparisonModel(report.InternalReportTitle,templatePath,displayColumns,companyLogo,defaultTimeZone,labelReplacement,employeeIds,classSchedules,tests,classScheduleRosters,showOnlyFailedPretestGrades,includeTestItemDetails,defaultDateFormat);
        }
    }
}
