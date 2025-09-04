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
    public class EnablingObjectivesByCategoryGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EnablingObjectivesByCategoryGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         IEnablingObjectiveService enablingObjectiveService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _enablingObjectiveService = enablingObjectiveService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EnablingObjectivesByCategory.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var categoryIds = ExtractParameters<List<int>>(report.Filters, "SELECT ENABLING OBJECTIVE CATEGORIES");
            var activeStatus = ExtractParameters<string>(report.Filters, "ENABLING OBJECTIVE STATUS");
            var showMetaEosOnly = ExtractParameters<bool>(report.Filters, "SHOW META EOS ONLY");
            var showSkillQualificationsOnly = ExtractParameters<bool>(report.Filters, "SHOW SKILL QUALIFICATIONS ONLY");
            var showLabelsOnly = ExtractParameters<bool>(report.Filters, "SHOW CATEGORY, SUB-CATEGORY, AND TOPIC LABELS ONLY");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            List<EnablingObjective> enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByCategoriesAsync(categoryIds, activeStatus, showMetaEosOnly, showSkillQualificationsOnly);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new EnablingObjectivesByCategoryModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, enablingObjectives, showLabelsOnly, labelReplacement, defaultTimeZone , showMetaEosOnly, showSkillQualificationsOnly);
        }

    }
}
