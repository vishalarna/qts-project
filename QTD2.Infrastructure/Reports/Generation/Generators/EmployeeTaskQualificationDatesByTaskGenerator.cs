using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Wordprocessing;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class EmployeeTaskQualificationDatesByTaskGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITaskService _taskService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IEmployeeService _employeeService;

        public EmployeeTaskQualificationDatesByTaskGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            ITaskService taskService,
            ITaskQualificationService taskQualificationService,
            IEmployeeService employeeService
        )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
            _taskQualificationService = taskQualificationService;
            _employeeService = employeeService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EmployeeTaskQualificationDatesByTask.cshtml";
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

            var taskIds = ExtractParameters<List<int>>(report.Filters, "SELECT TASKS");
            var rrTasksOnly = ExtractParameters<bool>(report.Filters, "RR TASKS ONLY");
            var dateRangefilter = report.Filters.FirstOrDefault(x => x.Name == "Date Range" && !String.IsNullOrEmpty(x.Value));
            var dateRange = dateRangefilter != null ? ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE") : new List<DateTime>();
            var activeInactiveAllEmployees = ExtractParameters<string>(report.Filters, "ACTIVE ONLY/INACTIVE ONLY/ALL EMPLOYEES");
            var includeTrainees = ExtractParameters<bool>(report.Filters, "INCLUDE TRAINEES");
            var tasks = (await _taskService.GetTasksForEmployeeTaskQualificationDatesByTaskGenerator(taskIds, rrTasksOnly)).ToList();
            var taskQualifications = (await _taskQualificationService.GetTaskQualificationsForEmployeeTaskQualificationDatesByTaskGenerator(tasks.Select(t => t.Id).ToList())).ToList();
            var employees = (await _employeeService.GetEmployeesForEmployeeTaskQualificationDatesByTaskGenerator(taskQualifications.Select(tq => tq.EmpId).Distinct().ToList(), activeInactiveAllEmployees, includeTrainees)).ToList();

            //Limit to date range if given
            if (dateRange.Any())
            {
                taskQualifications = taskQualifications.Where(t => t.TaskQualificationDate >= dateRange[0] && t.TaskQualificationDate <= dateRange[1]).ToList();
            }

            // Limit TQs by the filtered set of Employees 
            taskQualifications = taskQualifications.Where(tq => employees.Select(e => e.Id).Contains(tq.EmpId)).ToList();

            // Patch TaskQualifications and Employees for each Task
            foreach (var taskQualification in taskQualifications)
            {
                taskQualification.Employee = employees.First(e => e.Id == taskQualification.EmpId);
            }
            foreach (var task in tasks) 
            {
                task.TaskQualifications = taskQualifications
                    .Where(tq => tq.TaskId == task.Id)
                    .GroupBy(tq => tq.EmpId)
                    .Select(g => g.OrderByDescending(tq => tq.TaskQualificationDate).First())
                    .ToList();
            }

            return new EmployeeTaskQualificationDatesByTask(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, dateRange,labelReplacement, tasks, defaultDateFormat);
        }
    }
}
