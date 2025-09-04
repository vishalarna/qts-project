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
    public class TrainingModuleCompletionHistoryGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IMetaILAService _metaILAService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TrainingModuleCompletionHistoryGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IMetaILAService metaILAService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _metaILAService = metaILAService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TrainingModuleCompletionHistory.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            List<MetaILA> metaILAs = new List<MetaILA>();
            var metaILAIds = ExtractParameters<List<int>>(report.Filters, "Training Module");
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "Date Range");
            var includeTrainingModuleOption = ExtractParameters<String>(report.Filters, "Training Module Option");
            var includeInactiveILAs = ExtractParameters<bool>(report.Filters, "Include Inactive ILAs");
            var includeInactiveEmployee = ExtractParameters<bool>(report.Filters, "Include Inactive Employee");
            var isSelectCompleted = includeTrainingModuleOption == "Completed" ? true : false;
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            metaILAs = await _metaILAService.GetMetaILAsCompletionHistoryAsync(metaILAIds, dateRange[0], dateRange[1], isSelectCompleted, includeInactiveILAs, includeInactiveEmployee);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new TrainingModuleCompletionHistoryModel(report.InternalReportTitle, templatePath, displayColumns, metaILAs, companyLogo, labelReplacement, defaultTimeZone);
        }
    }
}
