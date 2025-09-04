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
    public class RecordOfTaskEOQualificationGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public RecordOfTaskEOQualificationGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IEmployeeService employeeService,
        IPositionService positionService,
         ITaskService taskService,
         IEnablingObjectiveService enablingObjectiveService,
         ITaskQualificationService taskQualificationService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _employeeService = employeeService;
            _positionService = positionService;
            _taskService = taskService;
            _enablingObjectiveService = enablingObjectiveService;
            _taskQualificationService = taskQualificationService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "RecordOfTaskEOQualifications.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var employeeIds = ExtractParameters<List<int>>(report.Filters, "Employee");
            var includeEvaluatorDetails = ExtractParameters<bool>(report.Filters, "Include Evaluator and Mode of Qualification");
            var includeTrainees = ExtractParameters<bool>(report.Filters, "Include Trainees");
            var printAllCompletedTasksFirst = ExtractParameters<bool>(report.Filters, "Print All Completed Tasks First");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var taskQualifications = await _taskQualificationService.GetTaskQualificationRecordsByEmployeeIdsAsync(employeeIds, includeEvaluatorDetails);
            taskQualifications = taskQualifications.Where(tq => tq.TaskQualificationDate.HasValue).ToList();
            var eoIDsLinkedToTasks = taskQualifications.SelectMany(t => t.Task.Task_EnablingObjective_Links.Select(t => t.EnablingObjectiveId)).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByIDs(eoIDsLinkedToTasks);
            foreach (var taskQualification in taskQualifications)
            {
                foreach (var eo in taskQualification.Task.Task_EnablingObjective_Links)
                {
                    eo.EnablingObjective = enablingObjectives.Where(r => r.Id == eo.EnablingObjectiveId).FirstOrDefault();
                }
            }
            List<Employee> employees = await _employeeService.GetEmployeesByListOfEmpIds(employeeIds);
            var positionIds = employees.SelectMany(r => r?.EmployeePositions?.Select(s => s.PositionId)).ToList();
            var positions = await _positionService.GetPositionTasksByIdAsync(positionIds);
            
            foreach (var emp in employees)
            {
                emp.EmployeePositions = emp.EmployeePositions.Where(p => p.Active).ToList();
                foreach (var empPosition in emp.EmployeePositions)
                {
                    empPosition.Position = positions.Where(r => r.Id == empPosition.PositionId).FirstOrDefault();
                }
            }

            if (generalSettings != null)
            {
                defaultTimeZone = generalSettings.DefaultTimeZone;
                companyLogo = generalSettings.CompanyLogo;
            }
            return new RecordOfTaskEOQualifications(report.InternalReportTitle, templatePath, displayColumns, companyLogo, taskQualifications.ToList(), employees.ToList(),printAllCompletedTasksFirst,includeEvaluatorDetails, labelReplacement, defaultTimeZone);
        }
    }
}
