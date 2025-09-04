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
   public class NERCILAApplicationReportVersionGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskService _taskService;
        private readonly IILAService _iLAService;
        private readonly IILA_EnablingObjective_LinkService _iLA_EnablingObjective_LinkService;
        private readonly IILA_TaskObjective_LinkService _iLA_TaskObjective_LinkService;
        private readonly IILACustomObjective_LinkService _iLACustomObjective_LinkService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public NERCILAApplicationReportVersionGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        ITaskService taskService,
        IILAService iLAService,
        IILA_EnablingObjective_LinkService iLA_EnablingObjective_LinkService,
        IILA_TaskObjective_LinkService iLA_TaskObjective_LinkService,
        IILACustomObjective_LinkService iLACustomObjective_LinkService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _iLAService = iLAService;
            _iLA_EnablingObjective_LinkService = iLA_EnablingObjective_LinkService;
            _iLA_TaskObjective_LinkService = iLA_TaskObjective_LinkService;
            _iLACustomObjective_LinkService = iLACustomObjective_LinkService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "NERCILAApplicationReportVersion.cshtml";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var companyLogo = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var iLAIds = ExtractParameters<List<int>>(report.Filters, "SELECT ILA");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var iLAs = await _iLAService.GetILAsByILAIdAsync(iLAIds);
          
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            return new NERCILAApplicationReportVersionGeneratorModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo,iLAs.ToList(), labelReplacement, defaultTimeZone);
        }
    }
}
