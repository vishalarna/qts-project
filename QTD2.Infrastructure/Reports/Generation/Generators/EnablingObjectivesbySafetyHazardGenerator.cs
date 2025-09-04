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
    public class EnablingObjectivesbySafetyHazardGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ISaftyHazardService _saftyHazardService;
        private readonly IEnablingObjective_MetaEO_LinkService _enablingObjective_MetaEO_LinkService;

        public EnablingObjectivesbySafetyHazardGenerator(
         IClientUserSettings_GeneralSettingService generalSettingService,
         IEnablingObjectiveService enablingObjectiveService,
         IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
         ISaftyHazardService saftyHazardService,
         IEnablingObjective_MetaEO_LinkService enablingObjective_MetaEO_LinkService
        )
        {
            _generalSettingService = generalSettingService;
            _enablingObjectiveService = enablingObjectiveService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _saftyHazardService = saftyHazardService;
            _enablingObjective_MetaEO_LinkService = enablingObjective_MetaEO_LinkService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EnablingObjectivesBySafetyHazard.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var safetyHazardIds = ExtractParameters<List<int>>(report.Filters, "SAFETY HAZARDS");
            var includeSafetyHazardDetails = ExtractParameters<bool>(report.Filters, "INCLUDE SAFETY HAZARD DETAILS");
            var includeMetaEnablingObjectives = ExtractParameters<bool>(report.Filters, "INCLUDE META ENABLING OBJECTIVES");
            var includeSkillQualifications = ExtractParameters<bool>(report.Filters, "INCLUDE SKILL QUALIFICATIONS");
            var includeInactiveEnablingObjectives = ExtractParameters<bool>(report.Filters, "INCLUDE INACTIVE ENABLING OBJECTIVES");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var safetyHazards = await _saftyHazardService.GetSafetyHazardsByIdAsync(safetyHazardIds);

            // Get directly linked EOs
            var distinctEoIds = safetyHazards.SelectMany(x => x.SafetyHazard_EO_Links).Select(e => e.EOID).Distinct().ToList();
            var enablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesBySafetyHazardAsync(distinctEoIds, includeMetaEnablingObjectives, includeSkillQualifications, includeInactiveEnablingObjectives);

            if (includeMetaEnablingObjectives)
            {
                // Get meta EO ids linked to EOs which were loaded (have to load this way due to nav prop not existing from child to it's links)
                var metaEnablingObjectiveLinks = await _enablingObjective_MetaEO_LinkService.GetMetaEnablingObjectivesByEoIdAsync(enablingObjectives.Select(eo => eo.Id).ToList(), includeInactiveEnablingObjectives);

                // Get distinct list of MetaEOs which should have their downstream EOs loaded 
                // Can come from EO_MetaEO_Links...
                var metaEnablingObjectiveIds = metaEnablingObjectiveLinks.Select(eml => eml.MetaEO.Id).Distinct().ToList();
                // ...or can come from MetaEOs directly linked to SafetyHazards
                metaEnablingObjectiveIds.AddRange(enablingObjectives.Where(eo => eo.isMetaEO).Select(eo => eo.Id).ToList());
                metaEnablingObjectiveIds = metaEnablingObjectiveIds.Distinct().ToList();

                var metaEnablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesBySafetyHazardAsync(metaEnablingObjectiveIds, includeMetaEnablingObjectives, includeSkillQualifications, includeInactiveEnablingObjectives);

                // Finally, get downstream EOs linked to the retrieved Meta EOs
                var downstreamEOIds = metaEnablingObjectives.SelectMany(meo => meo.EnablingObjective_MetaEO_Links.Select(eml => eml.EOID)).Distinct().ToList();
                var downstreamEnablingObjectives = await _enablingObjectiveService.GetEnablingObjectivesBySafetyHazardAsync(metaEnablingObjectiveIds, includeMetaEnablingObjectives, includeSkillQualifications, includeInactiveEnablingObjectives);

                enablingObjectives.AddRange(metaEnablingObjectives.Where(meo => !enablingObjectives.Select(eo => eo.Id).Contains(meo.Id)));
                enablingObjectives.AddRange(downstreamEnablingObjectives.Where(deo => !enablingObjectives.Select(eo => eo.Id).Contains(deo.Id)));
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            return new EnablingObjectivesbySafetyHazardModel(
                report.InternalReportTitle,
                templatePath,
                displayColumns,
                companyLogo,
                safetyHazards,
                labelReplacement,
                defaultTimeZone,
                includeSafetyHazardDetails,
                enablingObjectives,
                includeMetaEnablingObjectives,
                includeSkillQualifications,
                includeInactiveEnablingObjectives
                );
        }
    }
}
