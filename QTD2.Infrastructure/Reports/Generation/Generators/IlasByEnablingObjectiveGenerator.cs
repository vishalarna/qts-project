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
    public class IlasByEnablingObjectiveGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public IlasByEnablingObjectiveGenerator(
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
            string templatePath = "ILAsByEnablingObjective.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";

            var enablingObjectiveIds = ExtractParameters<List<int>>(report.Filters, "SELECT ENABLING OBJECTIVES");
            var includeUnlinkedEos = ExtractParameters<bool>(report.Filters, "INCLUDE ENABLING OBJECTIVES WITH NO ILAS LINKED");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var enablingObjectives = await _enablingObjectiveService.GetILAsByEnablingObjectiveAsync(enablingObjectiveIds, includeUnlinkedEos);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }

            return new ILAsByEnablingObjectiveModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, enablingObjectives, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}
