using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class ILAsBySafetyHazardGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ISaftyHazardService _saftyHazardService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public ILAsBySafetyHazardGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          ISaftyHazardService saftyHazardService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _saftyHazardService = saftyHazardService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ILAsBySafetyHazard.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";

            var safetyHazardsIds = ExtractParameters<List<int>>(report.Filters, "SAFETY HAZARDS");
            var includeSafetyHazardDetails = ExtractParameters<bool>(report.Filters, "INCLUDE SAFETY HAZARD DETAILS");
            var includeMetaILAs = ExtractParameters<bool>(report.Filters, "INCLUDE META ILAS");
            var includeInactiveILA = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE ILAS");
            var saftyhazards = await _saftyHazardService.GetSafetyHazardsForILAsync(safetyHazardsIds, includeInactiveILA);
      
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new ILAsBySafetyHazardModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, saftyhazards, labelReplacement, defaultTimeZone, includeSafetyHazardDetails, includeMetaILAs);
        }
    }
}
