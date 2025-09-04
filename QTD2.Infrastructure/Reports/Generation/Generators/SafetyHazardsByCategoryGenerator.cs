using DocumentFormat.OpenXml.Wordprocessing;
using QTD2.Domain.Entities.Core;
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
    public class SafetyHazardsByCategoryGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ISaftyHazardService _saftyHazardService;
        public SafetyHazardsByCategoryGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          ISaftyHazardService saftyHazardService
          )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _saftyHazardService = saftyHazardService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "SafetyHazardsByCategory.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }

            var safetyHazardCategoryIds = ExtractParameters<List<int>>(report.Filters, "Safety Hazard Category");
            var includeSafetyHazardDetails = ExtractParameters<bool>(report.Filters, "Include Safety Hazard Details");
            var includeInactiveSafetyHazards = ExtractParameters<bool>(report.Filters, "Include Inactive Safety Hazards");

            var safetyHazards = await _saftyHazardService.GetForSafetyHazardsByCategory(safetyHazardCategoryIds, includeInactiveSafetyHazards);

            return new SafetyHazardsByCategoryModel(report.InternalReportTitle,templatePath,displayColumns,companyLogo,labelReplacement,defaultTimeZone,safetyHazards,includeSafetyHazardDetails, defaultDateFormat);
        }
    }
}
