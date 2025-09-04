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
   public class ProcedureReviewCompletionHistoryGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IProcedureService _procedureService;
        private readonly IOrganizationService _organizationService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IPositionService _positionService;

        public ProcedureReviewCompletionHistoryGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IProcedureService procedureService,
          IPositionService positionService,
          IOrganizationService organizationService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _procedureService = procedureService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _positionService = positionService;
            _organizationService = organizationService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ProcedureReviewCompletionHistory.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var positionFilter = report.Filters.FirstOrDefault(x => x.Name == "Position" && !string.IsNullOrEmpty(x.Value));
            var organizationFilter = report.Filters.FirstOrDefault(x => x.Name == "Organization" && !string.IsNullOrEmpty(x.Value));

            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE");
            var employeeStatus = ExtractParameters<string>(report.Filters, "EMPLOYEE STATUS");
            var procedureIds = ExtractParameters<List<int>>(report.Filters, "PROCEDURES");
            var positionIDs = positionFilter != null ? ExtractParameters<List<int>>(report.Filters, "POSITION") : new List<int>();
            var organizationIDs = organizationFilter != null ? ExtractParameters<List<int>>(report.Filters, "ORGANIZATION") : new List<int>();
            var completedStatus = ExtractParameters<string>(report.Filters, "COMPLETION TYPE");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var positions = (await _positionService.GetByIdListAsync(positionIDs)).ToList();
            var organizations = (await _organizationService.GetByIdListAsync(organizationIDs)).ToList();

            var procedures = await _procedureService.GetAllProcedureCompletionHistoryAsync(employeeStatus,completedStatus, dateRange[0], dateRange[1], procedureIds, positionIDs, organizationIDs);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new ProcedureReviewCompletionHistory(report.InternalReportTitle, templatePath, displayColumns, companyLogo, procedures.ToList(), dateRange, labelReplacement, defaultTimeZone, positions, organizations);

        }
    }
}
