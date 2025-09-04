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
    public class InitialTrainingByPositionGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IILAService _iLAService;

        public InitialTrainingByPositionGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         IPositionService positionService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
         IILAService iLAService
         )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _iLAService = iLAService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "InitialTrainingByPosition.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var trainingProgramId = ExtractParameters<String>(report.Filters, "SELECT TRAINING PROGRAM");
            var includeInactiveILAs = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE ILAS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var positions = await _positionService.InitialTrainingByPositionAsync(trainingProgramId, includeInactiveILAs);
            var distinctILAIds = positions.SelectMany(position => position.TrainingPrograms).SelectMany(trainingProgram => trainingProgram.TrainingProgram_ILA_Links).Select(link => link.ILAId).Distinct().ToList();
            var ilas = await _iLAService.GetILAsWithCertificationInformationAsync(distinctILAIds);

            foreach (var position in positions)
            {
                foreach (var trainingProgram in position.TrainingPrograms)
                {
                    foreach (var trainingProgramILA in trainingProgram.TrainingProgram_ILA_Links)
                    {
                        var ila = ilas.FirstOrDefault(i => i.Id == trainingProgramILA.ILAId);
                        if (ila != null)
                        {
                            trainingProgramILA.ILA = ila;
                        }
                    }
                    if (!includeInactiveILAs)
                    {
                        trainingProgram.TrainingProgram_ILA_Links = trainingProgram.TrainingProgram_ILA_Links.Where(link => link.ILA != null && link.ILA.Active).ToList();
                    }
                }
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new InitialTrainingByPosition(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions, labelReplacement, defaultTimeZone);
        }

    }
}
