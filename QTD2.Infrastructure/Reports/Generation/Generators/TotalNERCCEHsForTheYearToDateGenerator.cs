using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using QTD2.Domain.Certifications.Factories.Interfaces;
using QTD2.Infrastructure.Reports.Generation.Generators.Helpers.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class TotalNERCCEHsForTheYearToDateGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ICertificationReportHelper _certificationReportHelper;

        public TotalNERCCEHsForTheYearToDateGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            ICertificationReportHelper certificationReportHelper
        )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _certificationReportHelper = certificationReportHelper;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TotalNERCCEHsfortheYeartoDate.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<int> employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES");
            int certificationId = ExtractParameters<int>(report.Filters, "SELECT CERTIFICATES & TRAINING REQUIREMENTS");
            var certificationFulfillmentStatuses = await _certificationReportHelper.GetCertificationFulfillmentStatuses(employeeIds, new List<int>() { certificationId });
            var companyLogo = "";
            var defaultTimeZone = "";
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
           
            return new TotalNERCCEHsForTheYearToDate(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, certificationFulfillmentStatuses);
        }
    }
}
