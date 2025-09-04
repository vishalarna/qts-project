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
	public class ReportScheduleByClassModelGenerator : ReportModelGenerator
	{
		private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
		private readonly IClassScheduleService _classScheduleService;
		private readonly IILAService _ilaService;
		private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

		public ReportScheduleByClassModelGenerator(
			IClientUserSettings_GeneralSettingService generalSettingService,
			IClassScheduleService classScheduleService,
			IILAService ilaService,
			IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
			)
		{
			_generalSettingService = generalSettingService;
			_classScheduleService = classScheduleService;
			_ilaService = ilaService;
			_clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
		}

		public override async Task<IReportModel> GenerateModel(Report report)
		{
			string templatePath = "ReportScheduleByClass.cshtml";
			var companyLogo = "";
			var generalSettings = await _generalSettingService.GetGeneralSettings();
			List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
			var activestatus = ExtractParameters<string>(report.Filters, "EMPLOYEE ACTIVE STATUS");
			var classScheduleIDs = ExtractParameters<List<int>>(report.Filters, "CLASS");
			var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
			var defaultTimeZone = "";
			var classSchedules = await _classScheduleService.GetTrainingScheduleByClassAsync(activestatus, classScheduleIDs);
			var ilas = await _ilaService.GetILAsWithCertificationInformationAsync(classSchedules.Select(r => r.ILAID.GetValueOrDefault()).Distinct().ToList());

			foreach (var classSchedule in classSchedules)
			{
				classSchedule.ILA = ilas.Where(r => r.Id == classSchedule.ILAID).FirstOrDefault();
			}

			if (generalSettings != null)
			{
				companyLogo = generalSettings.CompanyLogo;
				defaultTimeZone = generalSettings.DefaultTimeZone;
			}
			return new ReportScheduleByClass(report.InternalReportTitle, templatePath, displayColumns, classSchedules, companyLogo, labelReplacement, defaultTimeZone);
		}
    }
}
