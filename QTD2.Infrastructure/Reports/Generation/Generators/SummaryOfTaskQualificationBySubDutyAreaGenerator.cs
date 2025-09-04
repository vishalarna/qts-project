using DocumentFormat.OpenXml.Drawing.Diagrams;
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
    public class SummaryOfTaskQualificationBySubDutyAreaGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;
        private readonly ITaskReQualificationEmp_SignOffService _taskReQualificationEmp_SignOffService;
        private readonly ITaskQualificationService _taskQualificationService;
        public SummaryOfTaskQualificationBySubDutyAreaGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            IEmployeeService employeeService,
            ITaskService taskService,
            ITaskReQualificationEmp_SignOffService taskReQualificationEmp_SignOffService,
            ITaskQualificationService taskQualificationService
            )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _employeeService = employeeService;
            _taskService = taskService;
            _taskReQualificationEmp_SignOffService = taskReQualificationEmp_SignOffService;
            _taskQualificationService = taskQualificationService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "SummaryOfTaskQualificationBySubDutyArea.cshtml";
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
            List<int> employeeIds = ExtractParameters<List<int>>(report.Filters, "EMPLOYEES");
            List<int> positionIds = ExtractParameters<List<int>>(report.Filters, "SELECT POSITION");
            var includeTrainees = ExtractParameters<bool>(report.Filters, "INCLUDE TRAINEES");
            var includeInactiveTasks = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE TASKS");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var onlyRRTasks = ExtractParameters<bool>(report.Filters, "RELIABILITY RELATED TASKS ONLY");
            
            var includeTaskQualDetails = ExtractParameters<bool>(report.Filters, "INCLUDE TASK QUALIFICATION DETAILS");

            // Get Employees
            var employees = await _employeeService.GetEmployeesForSummaryOfTaskQualificationBySubDutyAreaGeneratorAsync(employeeIds);

            // Filter out EmployeePositions by PositionId
            employees.ForEach(e => e.EmployeePositions = e.EmployeePositions.Where(ep => positionIds.Contains(ep.PositionId)).ToList());

            // Filter out EmployeePositions if !includeTrainees and Trainee 
            if (!includeTrainees)
            {
                employees.ForEach(e => e.EmployeePositions = e.EmployeePositions.Where(ep => !ep.Trainee).ToList());
            }

            // Filter out Employees with no EmployeePositions
            employees = employees.Where(e => e.EmployeePositions.Any()).ToList();

            // Get unique TaskIds in remaining Employee.EmployeePositions.Position.Position_Tasks
            var taskIds = employees.SelectMany(e => e.EmployeePositions.SelectMany(ep => ep.Position.Position_Tasks.Select(pt => pt.TaskId))).Distinct().ToList();

            // Get Tasks by TaskIds
            // Include SubDutyArea.DutyArea
            var tasks = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(taskIds);

            // Filter out Tasks if !includeInactiveTasks
            if (!includeInactiveTasks)
            {
                tasks = tasks.Where(t => t.Active).ToList();
            }

            // Filter out Tasks if !includePseudoTasks
            if (!includePseudoTasks)
            {
                tasks = tasks.Where(t => t.SubdutyArea.DutyArea.Letter != "P").ToList();
            }

            // Filter out Tasks if onlyRRTasks
            if (onlyRRTasks)
            {
                tasks = tasks.Where(t => t.IsReliability).ToList();
            }


            // Get filtered EmployeeIds and TaskIds to be based on filtered lists
            var filteredEmployeeIds = employees.Select(e => e.Id).ToList();
            var filteredTaskIds = tasks.Select(t => t.Id).ToList();

            var taskQualifications = await _taskQualificationService.GetTaskQualificationsForSummaryOfTaskQualificationBySubDutyAreaGeneratorAsync(filteredEmployeeIds, filteredTaskIds);

            // Get Employee/Person data for TaskQualification Evaluators
            var evaluatorEmployeeIds = taskQualifications.SelectMany(tq => tq.TaskQualification_Evaluator_Links.Select(tqel => tqel.EvaluatorId)).Distinct().ToList();
            var evaluator_employees = (await _employeeService.GetEmployeesPersonDetailsByEmpIds(evaluatorEmployeeIds)).ToList();

            // Patch Evaluators onto TaskQualifications
            foreach (var taskQualification in taskQualifications)
            {
                foreach (var taskQualification_Evaluator_Link in taskQualification.TaskQualification_Evaluator_Links)
                {
                    taskQualification_Evaluator_Link.Evaluator = evaluator_employees.First(e => e.Id == taskQualification_Evaluator_Link.EvaluatorId);
                }
            }

            foreach (var employee in employees)
            {
                // Filter out Employee.EmployeePositions.Position.PositionTasks where TaskId not in Tasks.Id
                // -> Limits PositionTasks for each Employee based on the Tasks we actually want to show
                foreach (var employeePosition in employee.EmployeePositions)
                {
                    employeePosition.Position.Position_Tasks = employeePosition.Position.Position_Tasks.Where(pt => filteredTaskIds.Contains(pt.TaskId)).ToList();
                }

                // Patch TaskQualifications onto Employee
                employee.TaskQualifications = taskQualifications.Where(tq => tq.EmpId == employee.Id).ToList();
            }

            return new SummaryOfTaskQualificationBySubDutyArea(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, employees, tasks, includeTaskQualDetails, onlyRRTasks);
        }
    }
}
