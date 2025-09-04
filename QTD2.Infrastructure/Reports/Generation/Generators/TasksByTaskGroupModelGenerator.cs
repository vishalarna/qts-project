using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
   public class TasksByTaskGroupModelGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskService _taskService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITask_TrainingGroupService _task_TrainingGroupService;

        public TasksByTaskGroupModelGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        ITaskService taskService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
        ITask_TrainingGroupService task_TrainingGroupService
        )
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _task_TrainingGroupService = task_TrainingGroupService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TasksByTaskGroup.cshtml";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var companyLogo = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var taskGroupfilter = report.Filters.FirstOrDefault(x => x.Name == "Select Task Group" && !String.IsNullOrEmpty(x.Value));
            var taskGroupIds = taskGroupfilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT TASK GROUP") : new List<int>();
            var tasksWithoutTaskGroup = ExtractParameters<bool>(report.Filters, "INCLUDE TASKS NOT ASSIGNED TO A TASK GROUP");
            var rrTasks = ExtractParameters<bool>(report.Filters, "RELIABILITY RELATED TASKS");
            var includeInactiveTasks = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE TASKS");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            if(!taskGroupIds.Any() && tasksWithoutTaskGroup == false)
            {
                throw new QTDServerException("Either Enable Include Tasks not Assigned to a Task Group or Select Task Group", false);
            }
            var tasks = new List<Domain.Entities.Core.Task>();
            if (taskGroupIds.Any())
            {
                var task_TrainingGroupsTasks = await _taskService.GetTasksByTrainingTaskGroupIdsAsync(taskGroupIds, rrTasks, includeInactiveTasks, includePseudoTasks);
                tasks.AddRange(task_TrainingGroupsTasks);
                foreach(var t in tasks)
                {
                    t.Task_TrainingGroups = t.Task_TrainingGroups.Where(r => taskGroupIds.Contains(r.TrainingGroupId)).ToList();
                }
            }
            if (tasksWithoutTaskGroup == true)
            {
                var notLinkedTasks = await _taskService.GetTasksWithoutTaskTrainingGroupsAsync(rrTasks, includeInactiveTasks, includePseudoTasks);
                tasks.AddRange(notLinkedTasks);
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            return new TasksByTaskGroup(report.InternalReportTitle, templatePath, displayColumns, tasks.ToList(), companyLogo, labelReplacement, defaultTimeZone, tasksWithoutTaskGroup);
        }
    }
}
