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
    public class TestListGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IILATraineeEvaluationService _iLATraineeEvaluationService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TestListGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         IILATraineeEvaluationService iLATraineeEvaluationService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
         )
        {
            _generalSettingService = generalSettingService;
            _iLATraineeEvaluationService = iLATraineeEvaluationService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TestList.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var courseFilter = report.Filters.FirstOrDefault(x => x.Name.ToUpper() == "SELECT ILA" && !String.IsNullOrEmpty(x.Value));
            var ilaIDs = courseFilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT ILA") : new List<int>();
            List<int> testTypesIds = ExtractParameters<List<int>>(report.Filters, "TEST TYPE");
            List<int> testStatusIds = ExtractParameters<List<int>>(report.Filters, "STATUS");

            var defaultTimeZone = "";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var IlaTraineeEvals = await _iLATraineeEvaluationService.GetLinkedTestsByTestTypeAndStatusAsync(ilaIDs,testTypesIds,testStatusIds);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            return new TestListModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, IlaTraineeEvals, labelReplacement, defaultTimeZone);
        }
    }
}