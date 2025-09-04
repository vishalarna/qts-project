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
   public class TrainingProgramReviewGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITrainingProgramService _trainingProgramService;
        private readonly ITrainingProgramReviewService _trainingProgramReviewService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TrainingProgramReviewGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          ITrainingProgramService trainingProgramService,
          ITrainingProgramReviewService trainingProgramReviewService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _trainingProgramService = trainingProgramService;
            _trainingProgramReviewService = trainingProgramReviewService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TrainingProgramReview.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var trainingProgramReviewId = ExtractParameters<int>(report.Filters, "SELECT TRAINING PROGRAM REVIEW");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var trainingProgramReview = await _trainingProgramReviewService.GetAsync(trainingProgramReviewId);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TrainingProgramReviewReportModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo,trainingProgramReview, labelReplacement, defaultTimeZone);
        }
    }
}
