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
    public class EnablingObjectivesNotLinkedToTaskGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly ITaskService _taskService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IILAService _ilaService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EnablingObjectivesNotLinkedToTaskGenerator(
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
            string templatePath = "EnablingObjectivesNotLinkedToTasks.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var activeStatus = ExtractParameters<string>(report.Filters, "ALL/ACTIVE/INACTIVE ENABLING OBJECTIVES");
            var includeMetoEOs = ExtractParameters<bool>(report.Filters, "INCLUDE META EOS");
            var includeSkillQualifications = ExtractParameters<bool>(report.Filters, "INCLUDE SKILL QUALIFICATIONS");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            List<EnablingObjective> enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesNotLinkedToTaskAsync(activeStatus, includeMetoEOs, includeSkillQualifications);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new EnablingObjectivesNotLinkedToTasks(report.InternalReportTitle, templatePath, displayColumns, companyLogo, enablingObjectives, labelReplacement, defaultTimeZone);
        }
    }
}
