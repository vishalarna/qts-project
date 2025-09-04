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
   public class TaskQualificationEvaluatorsGenerator : ReportModelGenerator
    {

        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskQualification_Evaluator_LinkService _taskQualification_Evaluator_LinkService;
        private readonly ITaskService _taskService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IEmployeeService _employeeService;

        public TaskQualificationEvaluatorsGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
         ITaskQualification_Evaluator_LinkService taskQualification_Evaluator_LinkService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService, ITaskService taskService,
         IEmployeeService employeeService
          )
        {
            _generalSettingService = generalSettingService;
            _taskQualification_Evaluator_LinkService = taskQualification_Evaluator_LinkService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskService = taskService;
            _employeeService = employeeService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "MyData/TaskQualificationEvaluators.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT TASK QUALIFICATION EVALUATORS");
            var showAssignedAndPendingQualifications = ExtractParameters<bool>(report.Filters, "SHOW ASSIGNED AND PENDING QUALIFICATIONS");
            var showCompletedTaskQualifications = ExtractParameters<bool>(report.Filters, "SHOW COMPLETED TASK QUALIFICATIONS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var evaluators = await _employeeService.GetListOfTaskEvalatorsAsync(employeeIds);
            var taskqualificationEvaluators = await _taskQualification_Evaluator_LinkService.GetTaskQualificationsEvalLinksByEmployeeId(employeeIds);
            var distinctTaskIds = taskqualificationEvaluators.Select(m => m.TaskQualification.TaskId).Distinct().ToList();
            var tasks = await _taskService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds);
            foreach(var eval in evaluators)
            {
                eval.TaskQualification_Evaluator_Links = taskqualificationEvaluators.Where(x => x.EvaluatorId == eval.Id).ToList();
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TaskQualificationEvaluators(report.InternalReportTitle, templatePath, displayColumns, companyLogo, evaluators, labelReplacement, defaultTimeZone, showAssignedAndPendingQualifications, showCompletedTaskQualifications);
        }
    }
}
