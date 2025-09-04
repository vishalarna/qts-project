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
	public class ProcedureReviewCompletionHistorybyEmployeeGenerator : ReportModelGenerator
	{
		private readonly IProcedureReview_EmployeeService _procedureReview_EmployeeService;
		private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
		private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

		public ProcedureReviewCompletionHistorybyEmployeeGenerator(
			IProcedureReview_EmployeeService procedureReview_EmployeeService, IClientUserSettings_GeneralSettingService generalSettingService,
			IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
		)
		{
			_procedureReview_EmployeeService = procedureReview_EmployeeService;
			_generalSettingService = generalSettingService;
			_clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
		}

		public override async Task<IReportModel> GenerateModel(Report report)
		{
			string templatePath = "ProcedureReviewCompletionHistorybyEmployee.cshtml";
			List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
			var companyLogo = "";
			var defaultTimeZone = "";
			var generalSettings = await _generalSettingService.GetGeneralSettings();
			if (generalSettings != null)
			{
				companyLogo = generalSettings.CompanyLogo;
				defaultTimeZone = generalSettings.DefaultTimeZone;
			}
			var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
			List<int> employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES");
			string publishedProcedureReviews = ExtractParameters<String>(report.Filters, "PUBLISHED PROCEDURE REVIEWS");
			var dateRangefilter = report.Filters.FirstOrDefault(x => x.Name == "Completion Date Range" && !String.IsNullOrEmpty(x.Value));
			List<DateTime> completionDateRange = dateRangefilter != null ? ExtractParameters<List<DateTime>>(report.Filters, "COMPLETION DATE RANGE") : new List<DateTime>();
			string completionStatus = ExtractParameters<String>(report.Filters, "COMPLETION STATUS"); 

			var procedureReviewEmployees = await _procedureReview_EmployeeService.GetForProcedureReviewCompletionHistorybyEmployee(employeeIds, publishedProcedureReviews, completionDateRange, completionStatus);

			return new ProcedureReviewCompletionHistorybyEmployee(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, procedureReviewEmployees);
		}
	}
}
