using DocumentFormat.OpenXml.Wordprocessing;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Generators.Helpers.Interfaces;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class EnablingObjectivesByPositionMatrixGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IPositionService _positionService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly ITaskService _taskService;
        public EnablingObjectivesByPositionMatrixGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          IPositionService positionService,
          IEnablingObjectiveService enablingObjectiveService, ITaskService taskService
          )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _positionService = positionService;
            _enablingObjectiveService = enablingObjectiveService;
            _taskService = taskService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EnablingObjectivesByPositionMatrix.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            var positionIds = ExtractParameters<List<int>>(report.Filters, "POSITIONS");
            var includeMetaEnablingObjectives = ExtractParameters<bool>(report.Filters, "INCLUDE META ENABLING OBJECTIVES");
            var includeSkillQualifications = ExtractParameters<bool>(report.Filters, "INCLUDE SKILL QUALIFICATIONS");
            var includeInactiveEnablingObjectives = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE ENABLING OBJECTIVES");
            
            var positions = await _positionService.GetForEnablingObjectivesByPositionMatrixAsync(positionIds);
            var taskIds = positions.SelectMany(m => m.Position_Tasks).Select(o => o.TaskId).ToList();
            var enablingObjectiveIds = new List<int>();
            var eoSqIds = positions.SelectMany(p => p.Position_SQs).Select(p => p.EOId).Distinct().ToList();
            var tasks = await _taskService.GetEnablingObjectivesMetaEosLinksByTaskIdsAsync(taskIds);
            var eoIds = tasks.SelectMany(m => m.Task_EnablingObjective_Links).Select(o => o.EnablingObjectiveId).ToList();
            enablingObjectiveIds.AddRange(eoIds);
            enablingObjectiveIds.AddRange(eoSqIds);
            enablingObjectiveIds = enablingObjectiveIds.Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByIdAsync(enablingObjectiveIds, includeMetaEnablingObjectives, includeSkillQualifications, includeInactiveEnablingObjectives);

            return new EnablingObjectivesByPositionMatrixModel(report.InternalReportTitle,templatePath,displayColumns,companyLogo,labelReplacement,defaultTimeZone,positions, enablingObjectives);
        }
    }
}
