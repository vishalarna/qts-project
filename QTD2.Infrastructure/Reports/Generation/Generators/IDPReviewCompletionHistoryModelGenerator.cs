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
	public class IDPReviewCompletionHistoryModelGenerator: ReportModelGenerator
	{
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
		private readonly IIDP_ReviewService _reviewService;
		private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
		public IDPReviewCompletionHistoryModelGenerator(
			IClientUserSettings_GeneralSettingService generalSettingService,
			IIDP_ReviewService reviewService,
			IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
)
		{
			_generalSettingService = generalSettingService;
			_clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
			_reviewService = reviewService;
		}

		public override async Task<IReportModel> GenerateModel(Report report)
		{
			string templatePath = "IDPReviewCompletionHistory.cshtml";
			var generalSettings = await _generalSettingService.GetGeneralSettings();
			var companyLogo = "";
			var defaultTimeZone = "";
			List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

			//build data
			var includeInactiveEmployees = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE EMPLOYEES");
			var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

			var idpReview = await _reviewService.GetIDPReviewListAsync(includeInactiveEmployees);
			if (generalSettings != null)
			{
				companyLogo = generalSettings.CompanyLogo;
				defaultTimeZone = generalSettings.DefaultTimeZone;
			}
			return new IDPReviewCompletionHistory(report.InternalReportTitle, templatePath, displayColumns, idpReview.ToList(), companyLogo, labelReplacement, defaultTimeZone);
		}
    }
}
