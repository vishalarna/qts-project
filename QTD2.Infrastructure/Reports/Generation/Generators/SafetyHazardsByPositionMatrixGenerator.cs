using DocumentFormat.OpenXml.Wordprocessing;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
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
    public class SafetyHazardsByPositionMatrixGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IPositionService _positionService;
        private readonly ISaftyHazardService _saftyHazardService;
        public SafetyHazardsByPositionMatrixGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          IPositionService positionService,
          ISaftyHazardService saftyHazardService
          )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _positionService = positionService;
            _saftyHazardService = saftyHazardService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "SafetyHazardsByPositionMatrix.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            var positionIds = ExtractParameters<List<int>>(report.Filters, "POSITIONS");
            var includeInactiveSafetyHazards = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE SAFETY HAZARDS");

            if (positionIds != null && positionIds.Count > 10)
            {
                throw new QTDServerException("You can select up to 10 Positions only.",false);
            }

            var positions = await _positionService.GetForSafetyHazardsByPositionMatrix(positionIds);

            var safetyHazardIds = positions.SelectMany(p => p.Position_Tasks).SelectMany(pt => pt.Task.SafetyHazard_Task_Links).Select(shtl => shtl.SaftyHazardId).Distinct().ToList();

            var safetyHazards = await _saftyHazardService.GetForSafetyHazardsByPositionMatrix(safetyHazardIds, includeInactiveSafetyHazards);

            return new SafetyHazardsByPositionMatrixModel(report.InternalReportTitle,templatePath,displayColumns,companyLogo,labelReplacement,defaultTimeZone,positions,safetyHazards);
        }
    }
}
