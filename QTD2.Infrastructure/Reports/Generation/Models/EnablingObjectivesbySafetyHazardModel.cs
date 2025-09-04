using System.Collections.Generic;
using QTD2.Infrastructure.Reports.Interfaces;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class EnablingObjectivesbySafetyHazardModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<SaftyHazard> SaftyHazards { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public bool IncludeSafetyHazardsDetail { get; set; }
        public List<EnablingObjective> EnablingObjectives { get; set; }
        public bool IncludeMetaEnablingObjectives { get; set; }
        public bool IncludeSkillQualifications { get; set; }
        public bool IncludeInactiveEnablingObjectives { get; set; }

        public EnablingObjectivesbySafetyHazardModel(
            string title,
            string templatePath,
            List<string> displayColumns,
            string companyLogo,
            List<SaftyHazard> saftyHazards,
            List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements,
            string defaultTimeZone,
            bool includeSafetyHazardsDetail,
            List<EnablingObjective> enablingObjectives,
            bool includeMetaEnablingObjectives,
            bool includeSkillQualifications,
            bool includeInactiveEnablingObjectives)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            SaftyHazards = saftyHazards;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            IncludeSafetyHazardsDetail = includeSafetyHazardsDetail;
            EnablingObjectives = enablingObjectives;
            IncludeMetaEnablingObjectives = includeMetaEnablingObjectives;
            IncludeSkillQualifications = includeSkillQualifications;
            IncludeInactiveEnablingObjectives = includeInactiveEnablingObjectives;
        }
    }
}
