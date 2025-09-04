using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class TasksMetbyEmployeeGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEmployeeService _employeeService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITaskService _taskService;
        private readonly ITaskQualificationService _taskQualificationService;

        public TasksMetbyEmployeeGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IEmployeeService employeeService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            ITaskService taskService,
            ITaskQualificationService taskQualificationService
        )
        {
            _generalSettingService = generalSettingService;
            _employeeService = employeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
            _taskQualificationService = taskQualificationService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TasksMetByEmployee.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var employeeIds = ExtractParameters<List<int>>(report.Filters, "EMPLOYEES");
            var currentPositionsOnly = ExtractParameters<bool>(report.Filters, "CURRENT POSITIONS ONLY");
            var reliabilityRelatedTasks = ExtractParameters<bool>(report.Filters, "RELIABILITY RELATED TASKS ONLY");
            var includeInactiveTasks = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE TASKS");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var includeTrainees = ExtractParameters<bool>(report.Filters, "INCLUDE TRAINEES");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var employees = await _employeeService.GetEmployeessForTasksMetbyEmployee(employeeIds, currentPositionsOnly, includeTrainees);
            var taskQuals = await _taskQualificationService.GetTaskQualificationsByEmpIds(employeeIds.ToList());
            var distinctTaskIds = employees.SelectMany(e => e.EmployeePositions).SelectMany(ep => ep.Position.Position_Tasks).Select(pt => pt.TaskId).Distinct().ToList();
            var tasks = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds);

            foreach (var employee in employees)
            {
                employee.TaskQualifications = taskQuals.Where(tq => tq.EmpId == employee.Id).ToList();
                foreach (var employeePosition in employee.EmployeePositions)
                {
                    var updatedTasks = employeePosition.Position.Position_Tasks
                        .Select(pt =>
                        {
                            pt.Task = tasks.FirstOrDefault(t => t.Id == pt.TaskId);
                            return pt;
                        }).ToList();

                    if (reliabilityRelatedTasks)
                    {
                        updatedTasks = updatedTasks.Where(pt => pt.Task.IsReliability).ToList();
                    }

                    if (!includeInactiveTasks)
                    {
                        updatedTasks = updatedTasks.Where(pt => pt.Task.Active).ToList();
                    }

                    if (!includePseudoTasks)
                    {
                        updatedTasks = updatedTasks.Where(pt => pt.Task.SubdutyArea.DutyArea.Letter != "P").ToList();
                    }

                    employeePosition.Position.Position_Tasks = updatedTasks;
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TasksMetbyEmployee(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, employees,reliabilityRelatedTasks,includePseudoTasks,includeInactiveTasks);
        }
    }
}
