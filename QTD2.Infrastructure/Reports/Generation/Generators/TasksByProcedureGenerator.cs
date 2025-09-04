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
    public class TasksByProcedureGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IProcedureService _procedureService;
        private readonly ITaskService _taskService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TasksByProcedureGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IProcedureService procedureService,
          ITaskService taskService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _procedureService = procedureService;
            _taskService = taskService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TasksByProcedure.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";

            var procedureIds = ExtractParameters<List<int>>(report.Filters, "PROCEDURE");
            var rrTasks = ExtractParameters<bool>(report.Filters, "RELIABILITY RELATED TASKS");
            var includeInactiveTask = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE TASKS");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var procedures = await _procedureService.GetProceduresByIDAsync(procedureIds);
            var distinctTaskIds = procedures.SelectMany(x => x.Procedure_Task_Links).Select(link => link.TaskId).Distinct().ToList();
            var tasks = await _taskService.GetTasksByProcedureAsync(distinctTaskIds, rrTasks, includeInactiveTask);
            var taskDictionary = tasks.ToDictionary(t => t.Id);
            foreach (var procedure in procedures)
            {
                if (rrTasks || !includeInactiveTask)
                {
                    procedure.Procedure_Task_Links = procedure.Procedure_Task_Links.Where(link => taskDictionary.ContainsKey(link.TaskId) && (!rrTasks || taskDictionary[link.TaskId].IsReliability) && (includeInactiveTask || taskDictionary[link.TaskId].Active)).ToList();
                }

                foreach (var link in procedure.Procedure_Task_Links)
                {
                    if (taskDictionary.TryGetValue(link.TaskId, out var task))
                    {
                        link.Task = task;
                    }
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new TasksByProcedureModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, procedures, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}
