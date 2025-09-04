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
    public class SCORMCompletionSummaryByClassesGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;

        public SCORMCompletionSummaryByClassesGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            IClassScheduleService classScheduleService,
            IEmployeeService employeeService, IClassScheduleEmployeeService classScheduleEmployeeService
        )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _classScheduleService = classScheduleService;
            _employeeService = employeeService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "SCORMCompletionSummaryByClasses.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var companyLogo = "";
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat ?? "MM/dd/yyyy";
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var classScheduleIds = ExtractParameters<List<int>>(report.Filters, "SELECT CLASSES");
            var employeefilter = report.Filters.FirstOrDefault(x => x.Name == "Select Employees" && !String.IsNullOrEmpty(x.Value));
            var employeeIds = employeefilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES") : new List<int>();
            var includeTestAnswerDetails = ExtractParameters<bool>(report.Filters, "INCLUDE TEST ANSWER DETAILS"); 
            var showOnlyFailedGrades = ExtractParameters<bool>(report.Filters, "SHOW ONLY FAILED GRADES"); 

            var classSchedules = (await _classScheduleService.GetForSCORMCompletionSummaryByClasses(classScheduleIds, showOnlyFailedGrades)).ToList();
            var employees = new List<Employee>();

            if (employeeIds.Any())
            {
                employees = (await _employeeService.GetEmployeesPersonDetailsByEmpIds(employeeIds)).ToList();
            }
            else
            {
                var empIds = classSchedules.SelectMany(m => m.ClassSchedule_Employee).Select(r => r.EmployeeId).Distinct().ToList();
                employees = (await _employeeService.GetEmployeesPersonDetailsByEmpIds(empIds)).ToList();
            }

            foreach (var classSchedule in classSchedules)
            {
                if (employeeIds.Any())
                {
                    classSchedule.ClassSchedule_Employee = classSchedule.ClassSchedule_Employee.Where(cse => employeeIds.Contains(cse.EmployeeId)).ToList();
                }
                else
                {
                    classSchedule.ClassSchedule_Employee = classSchedule.ClassSchedule_Employee.ToList();
                }

                foreach (var classSchedule_Employee in classSchedule.ClassSchedule_Employee)
                {
                    classSchedule_Employee.Employee = employees.FirstOrDefault(e => e.Id == classSchedule_Employee.EmployeeId);
                }
            }

            return new SCORMCompletionSummaryByClasses(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, classSchedules, defaultDateFormat, includeTestAnswerDetails);
        }
    }
}
