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
	public class EmployeeCourseScheduleforGivenYearGenerator : ReportModelGenerator
	{
        private readonly IIDPScheduleService _idpScheduleService;
		private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
		private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

		public EmployeeCourseScheduleforGivenYearGenerator(IIDPScheduleService idpScheduleService, IClientUserSettings_GeneralSettingService generalSettingService,
			IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService)
		{
			_idpScheduleService = idpScheduleService;
			_generalSettingService = generalSettingService;
			_clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
		}

		public override async Task<IReportModel> GenerateModel(Report report)
		{
			string templatePath = "EmployeeCourseScheduleforGivenYear.cshtml";
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
			var employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES");
			var year = ExtractParameters<String>(report.Filters, "SELECT YEAR");
			var activeInactiveAllILAs = ExtractParameters<String>(report.Filters, "ACTIVE/INACTIVE/ALL ILAS");
			var ilaCompletionStatus = ExtractParameters<String>(report.Filters, "ILA COMPLETION STATUS");

			var idpSchedules = await _idpScheduleService.GetIDPSchedulesForEmployeeCourseScheduleforGivenYear(employeeIds, year, activeInactiveAllILAs, ilaCompletionStatus);

			return new EmployeeCourseScheduleforGivenYear(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement,idpSchedules);
		}
	}
}
