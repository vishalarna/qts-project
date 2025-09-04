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
    public class ClassSignInSheetGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public ClassSignInSheetGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IClassScheduleService classScheduleService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _classScheduleService = classScheduleService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ClassSignInSheet.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var classScheduleIds = ExtractParameters<List<int>>(report.Filters, "Training Classes");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var classSchedules = await _classScheduleService.GetClassSignInSheetByIdsAsync(classScheduleIds);
            
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new ClassSignInSheet(report.InternalReportTitle, templatePath, displayColumns, companyLogo, classSchedules, labelReplacement, defaultTimeZone);
        }
    }
}
