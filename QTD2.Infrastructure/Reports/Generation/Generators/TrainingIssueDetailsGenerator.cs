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
    public class TrainingIssueDetailsGenerator : ReportModelGenerator
    {

        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITrainingIssueService _trainingIssueService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TrainingIssueDetailsGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
         ITrainingIssueService trainingIssueService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _trainingIssueService = trainingIssueService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TrainingIssueDetails.cshtml";

            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var trainingIssueIds = ExtractParameters<List<int>>(report.Filters, "SELECT TRAINING ISSUE");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var trainingIssues = await _trainingIssueService.GetTrainingIssueDetailsByIdAsync(trainingIssueIds);

            var companyLogo = "";
            var defaultTimeZone = "";
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TrainingIssueDetailsModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, trainingIssues, labelReplacement, defaultTimeZone);
        }
    }
}
