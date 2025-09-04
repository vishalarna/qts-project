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
    public class TaskRequalificationByEmployeeGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITaskService _taskService;
        private readonly IILAService _iLAService;
        private readonly IEmployeeService _employeeService;
        public TaskRequalificationByEmployeeGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         ITaskQualificationService taskQualificationService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
         ITaskService taskService,
         IILAService iLAService,
         IEmployeeService employeeService
         )
        {
            _generalSettingService = generalSettingService;
            _taskQualificationService = taskQualificationService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
            _iLAService = iLAService;
            _employeeService = employeeService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TaskRequalificationByEmployee.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var employeeIds = ExtractParameters<List<int>>(report.Filters, "EMPLOYEE");
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE");
            var isRRTasks = ExtractParameters<bool>(report.Filters, "R-R TASKS ONLY");
            var includeTrainees = ExtractParameters<bool>(report.Filters, "INCLUDE TRAINEES");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var employees = await _employeeService.GetEmployeesByIdsAsync(employeeIds, includeTrainees);
            var distinctTaskIds = employees.SelectMany(p => p.EmployeePositions).SelectMany(ep => ep.Position?.Position_Tasks).Select(pt => pt.TaskId).Distinct().ToList();
            var tasks = await _taskService.GetTasksByIdsAndDatesAsync(distinctTaskIds, dateRange[0], dateRange[1], isRRTasks);
            var taskQualifications = await _taskQualificationService.GetTaskQualificationByTaskIdAsync(distinctTaskIds);
            var distinctILAIds = tasks.SelectMany(task => task.ILA_TaskObjective_Links).Select(link => link.ILAId).Distinct().ToArray();
            var ilas = await _iLAService.GetByListOfIdsAsync(distinctILAIds);
            var ilaDict = ilas.ToDictionary(ila => ila.Id);
            var taskQualificationDict = taskQualifications.GroupBy(tq => tq.TaskId).ToDictionary(g => g.Key, g => g.ToList());
            var taskDict = tasks.ToDictionary(t => t.Id);


            foreach (var task in tasks)
            {
                foreach (var link in task.ILA_TaskObjective_Links)
                {
                    if (ilaDict.TryGetValue(link.ILAId, out var ila))
                    {
                        link.ILA = ila;
                    }
                }

                if (taskQualificationDict.TryGetValue(task.Id, out var qualifications))
                {
                    task.TaskQualifications = qualifications;
                }
            }

            foreach (var emp in employees)
            {

                foreach (var empPos in emp.EmployeePositions)
                {
                    foreach (var posTask in empPos?.Position?.Position_Tasks)
                    {
                        if (taskDict.TryGetValue(posTask.TaskId, out var task))
                        {
                            posTask.Task = task;
                        }
                    }
                }
            }


            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TaskRequalificationByEmployee(report.InternalReportTitle, templatePath, displayColumns, companyLogo, employees, dateRange[0], dateRange[1], labelReplacement, defaultTimeZone);
        }
    }
}
