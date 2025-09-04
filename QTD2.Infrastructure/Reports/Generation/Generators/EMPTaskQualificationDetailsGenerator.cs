using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
	public class EMPTaskQualificationDetailsGenerator : ReportModelGenerator
    {
        private readonly ITaskQualificationService _taskQualificationService;
        private readonly ITaskService _taskService;
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        public EMPTaskQualificationDetailsGenerator(ITaskQualificationService taskQualificationService, ITaskService taskService, IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService)
        {
            _taskQualificationService = taskQualificationService;
            _taskService = taskService;
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EMPTaskQualificationDetails.cshtml";
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
            List<int> employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES");

            var positionsFilter = report.Filters.FirstOrDefault(x => x.Name == "Positions" && !String.IsNullOrEmpty(x.Value));
            var positionIds = positionsFilter != null ? ExtractParameters<List<int>>(report.Filters, "POSITIONS") : new List<int>();
            
            var tasksFilter = report.Filters.FirstOrDefault(x => x.Name == "Tasks" && !String.IsNullOrEmpty(x.Value));
            var taskIds = tasksFilter != null ? ExtractParameters<List<int>>(report.Filters, "TASKS") : new List<int>();
            
            if (positionsFilter == null && tasksFilter == null) { throw new QTDServerException("Either POSITIONS or TASKS should not be empty",false); }

			var taskQualificationStatus = ExtractParameters<String>(report.Filters, "TASK QUALIFICATION STATUS");

            var taskQualifications = await _taskQualificationService.GetTaskQualificationsForEMPTaskQualificationDetails(employeeIds, positionIds, taskIds, taskQualificationStatus);
            
            var evaluatorCompletionStatus = ExtractParameters<String>(report.Filters, "EVALUATOR COMPLETION STATUS");
            if (evaluatorCompletionStatus == "Completed Only")
            {
                foreach (var taskQualification in taskQualifications)
                {
                    taskQualification.TaskQualification_Evaluator_Links = taskQualification.TaskQualification_Evaluator_Links
                        .Where(tqe => taskQualification.TaskReQualificationEmp_SignOff
                            .Any(signoff => signoff.EvaluatorId == tqe.EvaluatorId && (signoff.IsCompleted ?? false) && signoff.Active)
                        ).ToList();
                }
            }
            else if (evaluatorCompletionStatus == "Not Completed Only")
            {
                foreach (var taskQualification in taskQualifications)
                {
                    taskQualification.TaskQualification_Evaluator_Links = taskQualification.TaskQualification_Evaluator_Links
                        .Where(tqe => !taskQualification.TaskReQualificationEmp_SignOff
                            .Any(signoff => signoff.EvaluatorId == tqe.EvaluatorId && (signoff.IsCompleted ?? false) && signoff.Active)
                        ).ToList();
                }
            }

            // Get Task.SubDutyArea/DutyArea information for all Tasks and Meta Task Tasks, optimization to load them all here once
            var distinctTaskIds = taskQualifications.Select(tq => tq.TaskId).ToList();
            distinctTaskIds.AddRange(taskQualifications.SelectMany(tq => tq.Task.Task_MetaTask_Links.Select(tmt => tmt.TaskId)));
            distinctTaskIds = distinctTaskIds.Distinct().ToList();

            var tasks = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds);

            foreach (var taskQualification in taskQualifications)
            {
                taskQualification.Task.SubdutyArea = tasks.Where(t => t.Id == taskQualification.TaskId).FirstOrDefault().SubdutyArea; // Set the SubDutyArea and not the Task so we don't override other Task loaded data

                foreach (var tmt in taskQualification.Task.Task_MetaTask_Links)
                {
                    tmt.Task.SubdutyArea = tasks.Where(t => t.Id == tmt.TaskId).FirstOrDefault().SubdutyArea;
                }
            }

            return new EMPTaskQualificationDetails(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, taskQualifications);
        }
    }
}
