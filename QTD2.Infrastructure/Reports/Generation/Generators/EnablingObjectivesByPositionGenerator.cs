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
    public class EnablingObjectivesByPositionGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IILAService _ilaService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EnablingObjectivesByPositionGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IPositionService positionService,
         ITaskService taskService,
         IEnablingObjectiveService enablingObjectiveService,
         IILAService ilaService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _taskService = taskService;
            _enablingObjectiveService = enablingObjectiveService;
            _ilaService = ilaService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EnablingObjectivesByPosition.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var positionIds = ExtractParameters<List<int>>(report.Filters, "POSITIONS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var includeObjectivesLinkIds = ExtractParameters<List<int>>(report.Filters, "SELECT OBJECTIVES TO INCLUDE");
            var positionsWithTask = await _positionService.GetPositionTasksByIdAsync(positionIds);
            var positionWithSq = new List<Position>();
            if (includeObjectivesLinkIds.Contains(3))
            {
                positionWithSq = await _positionService.GetPositionSqsWithEOAsync(positionIds);
            }
            var positions = positionsWithTask.Concat(positionWithSq).Distinct().ToList();
            List<QTD2.Infrastructure.Model.Position.PositionEOsOptions> positionEOsOptions = new List<Model.Position.PositionEOsOptions>();
            List<EnablingObjective> enablingObjectives = new List<EnablingObjective>();
            foreach (var position in positions)
            {
                var taskIds = position.Position_Tasks.Select(t => t.TaskId).Distinct().ToList();
                var tasks = await _taskService.GetEnablingObjectivesMetaEosSQsByTaskAsync(taskIds, includeObjectivesLinkIds);
                var eosWithTask = tasks.SelectMany(s => s.Task_EnablingObjective_Links.Select(t => t.EnablingObjective)).Distinct().ToList();
                var eoWithMetaEO = tasks.SelectMany(s => s.Task_EnablingObjective_Links).SelectMany(m => m.EnablingObjective.EnablingObjective_MetaEO_Links).Select(t => t.EnablingObjective).Distinct().ToList();
                var eoSQs = position.Position_SQs.Select(m => m.EnablingObjective).Distinct().ToList();
                var eos = eosWithTask.Concat(eoSQs).Concat(eoWithMetaEO).Distinct().ToList();
                Model.Position.PositionEOsOptions positionEOsOption = new Model.Position.PositionEOsOptions(position, eos);
                positionEOsOptions.Add(positionEOsOption);
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new EnablingObjectivesByPosition(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positionEOsOptions.ToList(), includeObjectivesLinkIds, labelReplacement, defaultTimeZone);
        }

    }
}
