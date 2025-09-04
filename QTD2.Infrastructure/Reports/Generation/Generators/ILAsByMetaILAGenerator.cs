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
    public class ILAsByMetaILAGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskService _taskService;
        private readonly IILAService _iLAService;
        private readonly IMetaILAService _metaILAService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public ILAsByMetaILAGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         ITaskService taskService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
         IMetaILAService metaILAService,
         IILAService iLAService)
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _metaILAService = metaILAService;
            _iLAService = iLAService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ILAsByMetaILA.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var defaultDateFormat = "";

            var metaILAIds = ExtractParameters<List<int>>(report.Filters, "SELECT META ILA");
            var linkObjectivesFilter = report.Filters.FirstOrDefault(x => x.Name == "Include Objectives linked to ILAs" && !String.IsNullOrEmpty(x.Value));
            var includeObjectivesLinkIds = linkObjectivesFilter != null ? ExtractParameters<List<int>>(report.Filters, "INCLUDE OBJECTIVES LINKED TO ILAS") : new List<int>();
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var metaILAs = await _metaILAService.GetMetaILAByIDAsync(metaILAIds);
            var distinctILAIds = metaILAs.SelectMany(x => x.Meta_ILAMembers_Links).Select(m => m.ILAID).Distinct().ToList();
            var ilas = await _iLAService.GetILAsWithObjectivesForMetaILAAsync(distinctILAIds,includeObjectivesLinkIds);
            var ilaWithCertLinks = await _iLAService.GetILAsWithCertificationInformationAsync(distinctILAIds);
            foreach (var metaIla in metaILAs)
            {
                foreach(var ilaMember in metaIla.Meta_ILAMembers_Links)
                {
                    ilaMember.ILA = ilas.FirstOrDefault(x => x.Id == ilaMember.ILAID);
                    ilaMember.ILA.ILACertificationLinks = ilaWithCertLinks.FirstOrDefault(x=>x.Id==ilaMember.ILAID).ILACertificationLinks;
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new ILAbyMetaILA(report.InternalReportTitle, templatePath, displayColumns, companyLogo, metaILAs, labelReplacement, defaultTimeZone, includeObjectivesLinkIds);
        }
    }
}
