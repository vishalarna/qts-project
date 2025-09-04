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
   public class DIFSurveyAggregateResultsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IDIFSurveyService _dIFSurveyService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public DIFSurveyAggregateResultsGenerator(
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
            string templatePath = "DIFSurveyAggregateResults.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";

            var difSurveyId = ExtractParameters<List<int>>(report.Filters, "SELECT SURVEY");
            var activeStatus = ExtractParameters<string>(report.Filters, "TASK ACTIVE STATUS");
            var includePseudoTasks = ExtractParameters<bool>(report.Filters, "INCLUDE PSEUDO TASKS");
            var trainingFrequencyId = ExtractParameters<int>(report.Filters, "TRAINING FREQUENCY");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var difSurveys = await _dIFSurveyService.GetDifSurveyAggregateResultsAsync(difSurveyId, activeStatus, includePseudoTasks, trainingFrequencyId);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new DIFSurveyAggregateResults(report.InternalReportTitle, templatePath, displayColumns, companyLogo, difSurveys, labelReplacement, defaultTimeZone);
        }
    }
}
