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
    public class OJTTrainingLogGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public OJTTrainingLogGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IEmployeeService employeeService,
            ITaskService taskService,
            IEnablingObjectiveService enablingObjectiveService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
            )
        {
            _generalSettingService = generalSettingService;
            _employeeService = employeeService;
            _taskService = taskService;
            _enablingObjectiveService = enablingObjectiveService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "OJTTrainingLog.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var employeeId = ExtractParameters<string> (report.Filters, "SELECT EMPLOYEE");
            var positionId = ExtractParameters<string>(report.Filters, "SELECT POSITION");

            var taskStatus = ExtractParameters<string>(report.Filters, "TASK STATUS");
            var isRRPositions = ExtractParameters<bool>(report.Filters, "R-R TASKS ONLY");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            int.TryParse(employeeId, out var employeeIdInt);
            int.TryParse(positionId, out var positionIdInt);
            var employees = await _employeeService.GetEmployeesAndPositionsByIdAsync(employeeIdInt, positionIdInt);
            var taskIds = employees.SelectMany(employee => employee.EmployeePositions.SelectMany(empPosition => empPosition.Position.Position_Tasks.Select(task => task.TaskId))).Distinct().ToList();
            var tasks = await getPositionTasksAsync(taskIds, isRRPositions, taskStatus, includePseudoTasks);

            foreach (var employee in employees)
            {
                foreach (var empPosition in employee.EmployeePositions)
                {
                    foreach (var positionTask in empPosition.Position.Position_Tasks)
                    {
                        positionTask.Task = tasks.FirstOrDefault(x => x.Id == positionTask.TaskId);
                    }
                }
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            return new OJTTrainingLog(report.InternalReportTitle, templatePath, displayColumns, companyLogo, employees.ToList(), labelReplacement, defaultTimeZone);
        }

        private async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Task>> getPositionTasksAsync(List<int> taskIds, bool isRRPositions, string taskStatus, bool isPseudoTasks)
        {
            var tasks = await _taskService.GetTasksByTaskIdsAsync(taskIds);
            var eoIDsLinkedToTasks = tasks.SelectMany(r => r.Task_EnablingObjective_Links.Select(t => t.EnablingObjectiveId)).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByEOIDs(eoIDsLinkedToTasks);
            var taskLoopData = new Dictionary<QTD2.Domain.Entities.Core.Task, string>();
            List<QTD2.Domain.Entities.Core.Task> tasksToExclude = new List<QTD2.Domain.Entities.Core.Task>();
            foreach (var task in tasks)
            {
                foreach (var eo in task.Task_EnablingObjective_Links)
                {
                    eo.EnablingObjective = enablingObjectives.Where(r => r.Id == eo.EnablingObjectiveId).FirstOrDefault();
                }
                task.Task_EnablingObjective_Links = task.Task_EnablingObjective_Links.OrderBy(eo => eo.EnablingObjective?.FullNumber, new AlphaNumericSortHelper()).ToList();
                taskLoopData.Add(task, task.SubdutyArea.DutyArea.Letter + task.SubdutyArea.DutyArea.Number + "." + task.SubdutyArea.SubNumber + "." + task.Number);
                if (!isPseudoTasks && task.SubdutyArea.DutyArea.Letter == "P")
                {
                    tasksToExclude.Add(task);
                }
                if (isRRPositions && !task.IsReliability)
                {
                    tasksToExclude.Add(task);
                }
                if (taskStatus == "Active Only" && !task.Active)
                {
                    tasksToExclude.Add(task);
                }
                if (taskStatus == "Inactive Only" && task.Active)
                {
                    tasksToExclude.Add(task);
                }

            }
            taskLoopData = taskLoopData.OrderBy(str => str.Value, new AlphaNumericSortHelper()).ToDictionary(task => task.Key, task => task.Value);
            tasks = taskLoopData.Keys.Except(tasksToExclude).ToList();
            return tasks.ToList();
        }
    }
}
