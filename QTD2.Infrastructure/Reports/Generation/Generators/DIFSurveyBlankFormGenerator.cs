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
  public  class DIFSurveyBlankFormGenerator : ReportModelGenerator 
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IDIFSurveyService _dIFSurveyService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public DIFSurveyBlankFormGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IDIFSurveyService dIFSurveyService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _dIFSurveyService = dIFSurveyService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "DIFSurveyBlankForm.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";

            var difSurveyId = ExtractParameters<List<int>>(report.Filters, "SELECT DIF SURVEY");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var difSurveys = await _dIFSurveyService.GetDifSurveyByIdsAsync(difSurveyId);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new DIFSurveyBlankForm(report.InternalReportTitle, templatePath, displayColumns, companyLogo, difSurveys, labelReplacement, defaultTimeZone);
        }
    }
}
