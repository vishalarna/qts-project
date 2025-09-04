using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Helpers;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
   public class TrainingQualificationRecordsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IPositionService _positionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITaskService _taskService;

        public TrainingQualificationRecordsGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         ITaskQualificationService taskQualificationService,
         IPositionService positionService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
         ITaskService taskService
         )
        {
            _generalSettingService = generalSettingService;
            _taskQualificationService = taskQualificationService;
            _positionService = positionService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "MyData/TrainingQualificationRecords.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var employeeIds = ExtractParameters<List<int>>(report.Filters, "EMPLOYEES");
            var currentPositions = ExtractParameters<bool>(report.Filters, "CURRENT POSITION(S) ONLY");
            var dateRangefilter = report.Filters.FirstOrDefault(x => x.Name == "Date Range" && !String.IsNullOrEmpty(x.Value));
            var dateRange = dateRangefilter != null ? ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE") : new List<DateTime>();
            var reliabilityTasks = ExtractParameters<bool>(report.Filters, "RELIABILITY RELATED TASKS");
            var inActiveTasks = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE TASKS");
            var includePesudoTask = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var includeTrainees = ExtractParameters<bool>(report.Filters, "INCLUDE TRAINEES");
            var currentDate = DateTime.Now.Date;
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var TaskQualification = await _taskQualificationService.GetTaskQualificationRecordsAsync(employeeIds, dateRange,includePesudoTask,includeTrainees);
            var positionIds = TaskQualification.SelectMany(r => r.Employee.EmployeePositions.Select(s => s.PositionId)).Distinct().ToList();
            var positions = await _positionService.GetPositionTasksByIdAsync(positionIds);
            List<Employee> employees = TaskQualification.Select(r => r.Employee).Distinct().ToList();
            foreach (var position in positions)
            {
                var positionTask = position.Position_Tasks.Where(r => (inActiveTasks ? true : r.Task.Active) && (reliabilityTasks ? r.Task.IsReliability : true) && (includePesudoTask ? true : r.Task.SubdutyArea.DutyArea.Letter != "P")).OrderBy(t => t.Task?.FullNumber, new AlphaNumericSortHelper()).ToList();
                position.Position_Tasks = positionTask;
            }
            foreach (var emp in employees)
            {
                foreach (var empPosition in emp.EmployeePositions)
                {
                    empPosition.Position = positions.Where(r => r.Id == empPosition.PositionId).FirstOrDefault();
                }
                emp.EmployeePositions = emp.EmployeePositions.Where(p => currentPositions ? p.Active && (!p.EndDate.HasValue || p.EndDate > DateOnly.FromDateTime(currentDate)) : true).ToList();
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat ?? "MM/dd/yyyy";
            }
            return new TaskQualificationRecords(report.InternalReportTitle, templatePath, displayColumns, companyLogo, TaskQualification.ToList(), employees.ToList(), labelReplacement, defaultTimeZone,dateRange, defaultDateFormat);
        }
    }
}
