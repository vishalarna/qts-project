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
    public class IlasByPositionGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;

        public IlasByPositionGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
         IPositionService positionService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
         IEnablingObjectiveService enablingObjectiveService
          )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _enablingObjectiveService = enablingObjectiveService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ILAsByPosition.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";

            var positionIds = ExtractParameters<List<int>>(report.Filters, "SELECT POSITIONS");
            var includeUnlinkedPositions = ExtractParameters<bool>(report.Filters, "INCLUDE POSITIONS WITH NO ILAS LINKED");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var positions = await _positionService.GetILAsByPositionAsync(positionIds);
            var eoIds = positions.SelectMany(x => x.Position_SQs.Select(m => m.EOId)).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByEOIDs(eoIds);

            foreach(var position in positions)
            {
                if (!includeUnlinkedPositions)
                {
                    position.Position_SQs = position.Position_SQs.Where(x => x.EnablingObjective.ILA_EnablingObjective_Links.Count() > 0).ToList();
                }
                foreach(var posSq in position.Position_SQs)
                {
                    posSq.EnablingObjective = enablingObjectives.FirstOrDefault(m => posSq.EOId == m.Id);
                }
            }

            if (!includeUnlinkedPositions)
            {
                positions = positions.Where(x => x.Position_SQs.Any()).ToList();
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }

            return new ILAsByPositionModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}
