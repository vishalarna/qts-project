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
    public class ProceduresByIssuingAuthorityGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IProcedureService _procedureService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        public ProceduresByIssuingAuthorityGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IProcedureService procedureService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _procedureService = procedureService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "MyData/ProceduresByIssuingAuthority.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var issuingAuthorityIds = ExtractParameters<List<int>>(report.Filters, "ISSUING AUTHORITY");
            var includeInactiveProcedure = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE PROCEDURES");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var procedures = await _procedureService.GetAllProceduresByIssuingAuthoritiesAsync(issuingAuthorityIds, includeInactiveProcedure);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat ?? "MM/dd/yyyy";
            }
            return new ProceduresByIssuingAuthorityModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, procedures, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}
