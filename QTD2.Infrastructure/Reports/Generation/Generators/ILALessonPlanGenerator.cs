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
  public  class ILALessonPlanGenerator : ReportModelGenerator
    {

        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IILAService _ilaService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public ILALessonPlanGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IILAService ilaService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
          )
        {
            _generalSettingService = generalSettingService;
            _ilaService = ilaService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ILALessonPlan.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var iLAIDs = ExtractParameters<List<int>>(report.Filters, "ILA");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var iLAs = await _ilaService.GetILALessonPlanAsync(iLAIDs);
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new ILADesignSpecification(report.InternalReportTitle, templatePath, displayColumns, companyLogo, iLAs, labelReplacement, defaultTimeZone);

        }
    }
    }
