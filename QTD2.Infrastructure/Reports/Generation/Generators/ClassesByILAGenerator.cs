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
    public class ClassesByILAGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        public ClassesByILAGenerator(
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
            string templatePath = "MyData/ClassesByILA.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";

            var ilaIds = ExtractParameters<List<int>>(report.Filters, "ILA");
            var dateRange = ExtractParameters<List<DateTime>>(report.Filters, "DATE RANGE");
            var instructorfilter = report.Filters.FirstOrDefault(x => x.Name.ToUpper() == "SELECT INSTRUCTOR" && !String.IsNullOrEmpty(x.Value));
            var instructorIds = instructorfilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT INSTRUCTOR") : new List<int>();
            var locationfilter = report.Filters.FirstOrDefault(x => x.Name.ToUpper() == "SELECT LOCATION" && !String.IsNullOrEmpty(x.Value));
            var locationIds = locationfilter != null ? ExtractParameters<List<int>>(report.Filters, "SELECT LOCATION") : new List<int>();
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var iLAs = await _classScheduleService.GetClassesByILAAsync(ilaIds, dateRange[0], dateRange[1], instructorIds, locationIds);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new ClassesByILA(report.InternalReportTitle, templatePath, displayColumns, companyLogo, iLAs, labelReplacement, dateRange, defaultTimeZone);
        }
    }
}
