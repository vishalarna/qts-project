using QTD2.Domain.Entities.Core;
using QTD2.Domain.Helpers;
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
    public class EmployeeTaskQualificationRecordsForGivenPositionGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly ITaskQualificationService _taskQualificationService;

        public EmployeeTaskQualificationRecordsForGivenPositionGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            IEmployeeService employeeService,
            IPositionService positionService,
            ITaskService taskService,
            ITaskQualificationService taskQualificationService
        )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _employeeService = employeeService;
            _positionService = positionService;
            _taskService = taskService;
            _taskQualificationService = taskQualificationService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EmployeeTaskQualificationRecordsForGivenPosition.cshtml";
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

            var employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES");
            var positionIds = ExtractParameters<List<int>>(report.Filters, "SELECT POSITION");
            var dateRangefilter = report.Filters.FirstOrDefault(x => x.Name == "Date Range" && !String.IsNullOrEmpty(x.Value));
            var dateRange = dateRangefilter != null ? ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE") : new List<DateTime>();
            var reliabilityRelatedTasksOnly = ExtractParameters<bool>(report.Filters, "RELIABILITY RELATED TASKS ONLY"); ;
            var includeInactiveTasks = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE TASKS");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var includeTrainees = ExtractParameters<bool>(report.Filters, "INCLUDE TRAINEES");

            var employees = (await _employeeService.GetEmployeesForEmployeeTaskQualificationRecordsForGivenPositionGenerator(employeeIds, includeTrainees)).ToList();
            // Filter EmployeePositions for each Employee based on if the Position is in the input set + the link is Active
            foreach (var employee in employees)
            {
                employee.EmployeePositions = employee.EmployeePositions.Where(ep => ep.Active && positionIds.Contains(ep.PositionId)).ToList();
            }
            // Remove Employees without any relevant EmployeePositions
            employees = employees.Where(e => e.EmployeePositions.Count > 0).ToList();
            employeeIds = employees.Select(e => e.Id).ToList();

            // Limit PositionIds to relevant Positions based on existing EmployeePositions
            var employeePosition_PositionIds = employees.SelectMany(e => e.EmployeePositions.Select(ep => ep.PositionId)).Distinct().ToList();
            positionIds = positionIds.Where(p => employeePosition_PositionIds.Contains(p)).ToList();

            var positions = (await _positionService.GetPositionTasksByIdsAsync(positionIds)).ToList();

            // Patch Position onto EmployeePositions
            foreach (var employee in employees)
            {
                foreach (var employeePosition in employee.EmployeePositions)
                {
                    employeePosition.Position = positions.First(p => p.Id == employeePosition.PositionId);
                }
            }

            var taskIds = positions.SelectMany(p => p.Position_Tasks.Select(pt => pt.TaskId)).Distinct().ToList();

            var tasks = (await _taskService.GetTasksForEmployeeTaskQualificationRecordsForGivenPositionGenerator(taskIds, reliabilityRelatedTasksOnly, includeInactiveTasks, includePseudoTasks)).ToList();
            
            // Limit TaskIds to relevant Tasks based on filtered set of returned Tasks
            taskIds = tasks.Select(t => t.Id).ToList();

            var taskQualifications = (await _taskQualificationService.GetTaskQualificationsForEmployeeTaskQualificationRecordsForGivenPositionGenerator(employeeIds, taskIds, dateRange)).ToList();

            // Patch Task onto TaskQualifications
            foreach (var taskQualification in taskQualifications)
            {
                taskQualification.Task = tasks.First(t => t.Id == taskQualification.TaskId);
            }

            // Get Employee/Person data for TaskQualification Evaluators
            var evaluatorEmployeeIds = taskQualifications.SelectMany(tq => tq.TaskQualification_Evaluator_Links.Select(tqel => tqel.EvaluatorId)).Distinct().ToList();
            var evaluator_employees = (await _employeeService.GetEmployeesPersonDetailsByEmpIds(evaluatorEmployeeIds)).ToList();

            // Order TaskQualifications by full number with AlphaNumericHelper compare method
            taskQualifications = taskQualifications.OrderBy(t => t.Task?.FullNumber, new AlphaNumericSortHelper()).ToList();

            return new EmployeeTaskQualificationRecordsForGivenPosition(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, employees, taskQualifications, evaluator_employees,dateRange,defaultDateFormat);
        }
    }
}
