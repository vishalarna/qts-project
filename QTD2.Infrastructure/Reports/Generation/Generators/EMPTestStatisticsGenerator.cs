using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
	public class EMPTestStatisticsGenerator : ReportModelGenerator
	{
        private readonly IClassScheduleService _classScheduleService;
        private readonly ITestService _testService;
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public EMPTestStatisticsGenerator(IClassScheduleService classScheduleService, ITestService testService, IClientUserSettings_GeneralSettingService generalSettingService, IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService)
		{
			_classScheduleService = classScheduleService;
			_testService = testService;
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

		public override async Task<IReportModel> GenerateModel(Report report)
		{
            string templatePath = "EMPTestStatistics.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var companyLogo = "";
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat ?? "MM/dd/yyyy";
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            List<int> classScheduleIds = ExtractParameters<List<int>>(report.Filters, "SELECT CLASS");
            List<int> testIds = ExtractParameters<List<int>>(report.Filters, "SELECT TEST TYPE");

            var classSchedules = await _classScheduleService.GetClassSchedulesForEMPTestStatistics(classScheduleIds);

			//Limit testIds to Tests which actually exist on an ILA as an ILATraineeEvaluation
			var distinctILATraineeEvaluationTestIds = classSchedules.SelectMany(cs => cs.ILA.ILATraineeEvaluations.Select(item => item.TestId)).Distinct();
			testIds = testIds.Where(t => distinctILATraineeEvaluationTestIds.Contains(t)).ToList();

			var tests = await _testService.GetTestsForEMPTestStatistics(testIds);

			return new EMPTestStatistics(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement,classSchedules, tests, defaultDateFormat);
		}
	}
}
