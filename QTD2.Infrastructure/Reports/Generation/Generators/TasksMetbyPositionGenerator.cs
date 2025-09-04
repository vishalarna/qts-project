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
	public class TasksMetbyPositionGenerator : ReportModelGenerator
    {
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        public TasksMetbyPositionGenerator(
            IPositionService positionService, IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService, ITaskService taskService, ITaskQualificationService taskQualificationService
        )
        {
            _positionService = positionService;
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
            _taskQualificationService = taskQualificationService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TasksMetbyPosition.cshtml";
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
            var positionIds = ExtractParameters<List<int>>(report.Filters, "POSITIONS");
            var allActiveInactiveOnlyEmployees = ExtractParameters<String>(report.Filters, "EMPLOYEES");
            var currentPositionsOnly = ExtractParameters<bool>(report.Filters, "CURRENT POSITIONS ONLY");
            var reliabilityRelatedTasks = ExtractParameters<bool>(report.Filters, "RELIABILITY-RELATED TASKS ONLY");
            var includeInactiveTasks = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE TASKS");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var includeTrainees = ExtractParameters<bool>(report.Filters, "INCLUDE TRAINEES");

            var positions = await _positionService.GetPositionsForTasksMetbyPosition(positionIds, allActiveInactiveOnlyEmployees, currentPositionsOnly, includeTrainees);
            var distinctTaskIds = positions.SelectMany(m => m.Position_Tasks.Select(r => r.TaskId)).ToList().Distinct();
            var distinctEmpIds = positions.SelectMany(m => m.EmployeePositions.Select(r => r.EmployeeId)).ToList().Distinct();
            var taskQuals = await _taskQualificationService.GetTaskQualificationsByEmpIds(distinctEmpIds.ToList());
            var tasks = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds.ToList());
            foreach(var pos in positions)
            {
                foreach(var posTask in pos.Position_Tasks)
                {
                    posTask.Task = tasks.Where(x => x.Id == posTask.TaskId).FirstOrDefault();
                }

                foreach(var empPos in pos.EmployeePositions)
                {
                    empPos.Employee.TaskQualifications = taskQuals.Where(tq => tq.EmpId == empPos.EmployeeId).ToList();
                }
                    if (reliabilityRelatedTasks)
                    {
                        pos.Position_Tasks = pos.Position_Tasks.Where(pt => pt.Task.IsReliability).ToList();
                    }
                    if (!includeInactiveTasks)
                    {
                        pos.Position_Tasks = pos.Position_Tasks.Where(pt => pt.Task.Active).ToList();
                    }
                    if (!includePseudoTasks)
                    {
                        pos.Position_Tasks = pos.Position_Tasks.Where(pt => pt.Task.SubdutyArea.DutyArea.Letter != "P").ToList();
                    }
            }

            return new TasksMetbyPosition(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, positions, reliabilityRelatedTasks, includePseudoTasks, includeInactiveTasks);
        }
    }
}
