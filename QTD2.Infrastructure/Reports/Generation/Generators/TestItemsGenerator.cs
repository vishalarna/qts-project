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
   public class TestItemsGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITestItemService _testItemService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TestItemsGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         ITestItemService testItemService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
         )
        {
            _generalSettingService = generalSettingService;
            _testItemService = testItemService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TestItems.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var testItems = ExtractParameters<string>(report.Filters, "SELECT TEST ITEMS");
            List<int> itemTypesIds = ExtractParameters<List<int>>(report.Filters, "ITEM TYPE");
            List<int> taxonomyLevelIds = ExtractParameters<List<int>>(report.Filters, "TAXONOMY LEVEL");
            var onlyEOLinkedTestItems = ExtractParameters<bool>(report.Filters, "ONLY TEST ITEMS WITH NO EO LINKED");
            var onlyTestLinkedTestItems = ExtractParameters<bool>(report.Filters, "ONLY TEST ITEMS NOT LINKED TO TEST");
            var defaultTimeZone = "";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var allTestItems = await _testItemService.GetTestItemsByIdandTaxonomyIdAsync(itemTypesIds, taxonomyLevelIds, testItems);
            if (onlyEOLinkedTestItems)
            {
                allTestItems = allTestItems.Where(x => x.EOId == null).ToList();
            }
            if (onlyTestLinkedTestItems)
            {
                allTestItems = allTestItems.Where(x => x.Test_Item_Links == null || x.Test_Item_Links.Count() == 0).ToList();
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            return new TestItems(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, allTestItems);
        }
    }
}
