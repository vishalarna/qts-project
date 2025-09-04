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
   public class MyDataPositionDetailsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public MyDataPositionDetailsGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IPositionService positionService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "MyData/PositionDetails.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var positionIDs = ExtractParameters<List<int>>(report.Filters, "POSITIONS");
            var filterOption = ExtractParameters<string>(report.Filters, "ACTIVE ONLY/INACTIVE ONLY/ALL POSITIONS");
            var positionVersion = ExtractParameters<string>(report.Filters, "SELECT POSITION VERSION");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var positions = await _positionService.GetForMyDataPositionDetailsAsync(positionIDs, filterOption, positionVersion);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new MyDataPositionDetails(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions, labelReplacement, defaultTimeZone);
        }
    }
}
