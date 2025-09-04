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
    public class EmployeeByOrganizationGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IOrganizationService _organizationService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EmployeeByOrganizationGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IOrganizationService organizationService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _organizationService = organizationService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EmployeeByOrganization.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";

            var organizationIDs = ExtractParameters<List<int>>(report.Filters, "BY ORGANIZATION");
            var activeStatus = ExtractParameters<string>(report.Filters, "ACTIVE ONLY/INACTIVE ONLY/ALL EMPLOYEES");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var organizations = await _organizationService.GetEmployeesByOrganizationAsync(organizationIDs, activeStatus);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new EmployeeByOrganization(report.InternalReportTitle, templatePath, displayColumns, companyLogo, organizations, labelReplacement, defaultTimeZone);
        }
    }
}
