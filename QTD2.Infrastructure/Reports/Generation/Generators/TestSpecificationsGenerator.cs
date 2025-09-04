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
    public class TestSpecificationsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITestService _testService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITest_Item_LinkService _test_Item_LinkService;

        public TestSpecificationsGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
          ITestService testService,
          IEnablingObjectiveService enablingObjectiveService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          ITest_Item_LinkService test_Item_LinkService
         )
        {
            _generalSettingService = generalSettingService;
            _testService = testService;
            _enablingObjectiveService = enablingObjectiveService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _test_Item_LinkService = test_Item_LinkService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TestSpecifications.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var testIds = ExtractParameters<List<int>>(report.Filters, "TESTS");
            var includeTestStatistics = ExtractParameters<bool>(report.Filters, "SHOW TEST COMPLETION STATISTICS");
            var isShowCorrectAnswer = ExtractParameters<bool>(report.Filters, "SHOW CORRECT ANSWER");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var tests = await _testService.GetTestSpecificationsByTestIdsAsync(testIds, includeTestStatistics);
            var testItemLinks = await _test_Item_LinkService.GetAllTestSpecificationAsync(testIds);
            foreach (var test in tests)
            {
                test.Test_Item_Links = testItemLinks.Where(link => link.TestId == test.Id).ToList();
            }
            var eoIDsLinkedToTest = tests.SelectMany(r => r.Test_Item_Links.Where(t => t.TestItem.EnablingObjective != null).Select(t => t.TestItem.EnablingObjective.Id)).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesByEOIDs(eoIDsLinkedToTest);
            foreach (var test in tests)
            {
                foreach (var testItemLink in test.Test_Item_Links)
                {
                    if (testItemLink.TestItem.EnablingObjective != null)
                    {
                        testItemLink.TestItem.EnablingObjective = enablingObjectives.Where(r => r.Id == testItemLink.TestItem.EnablingObjective.Id).FirstOrDefault();
                    }
                }
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TestSpecifications(report.InternalReportTitle, templatePath, displayColumns, companyLogo, tests.ToList(), isShowCorrectAnswer, includeTestStatistics, labelReplacement, defaultTimeZone);
        }

    }
}
