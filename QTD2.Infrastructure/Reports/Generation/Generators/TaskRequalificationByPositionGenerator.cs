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
   public class TaskRequalificationByPositionGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;
        private readonly IPositionService _positionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IILAService _iLAService;
        public TaskRequalificationByPositionGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          ITaskQualificationService taskQualificationService,
          ITaskService taskService,
          IPositionService positionService,
          IEmployeeService employeeService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          IILAService iLAService
          )
        {
            _generalSettingService = generalSettingService;
            _taskQualificationService = taskQualificationService;
            _taskService = taskService;
            _positionService = positionService;
            _employeeService = employeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _iLAService = iLAService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "MyData/TaskRequalificationByPosition.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var positionId = ExtractParameters<List<int>>(report.Filters, "POSITION");
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE");
            var includeRRTasks = ExtractParameters<bool>(report.Filters, "R-R TASKS ONLY");
            var includeTrainees = ExtractParameters<bool>(report.Filters, "INCLUDE TRAINEES");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var positions = await _positionService.GetTaskRequalificationByPositionAsync(positionId, includeTrainees);
            var distinctTaskIds = positions.SelectMany(p => p.Position_Tasks).Select(pt => pt.TaskId).Distinct().ToList();
            var tasks = await _taskService.GetTasksByIdsAndDatesAsync(distinctTaskIds, dateRange[0], dateRange[1], includeRRTasks);
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

            foreach (var position in positions)
            {
                foreach (var positionTask in position.Position_Tasks)
                {
                    if (taskDict.TryGetValue(positionTask.TaskId, out var task))
                    {
                        positionTask.Task = task;
                    }
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
        
            return new TaskRequalificationByPosition(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions, labelReplacement, defaultTimeZone);

        }

    }
}
