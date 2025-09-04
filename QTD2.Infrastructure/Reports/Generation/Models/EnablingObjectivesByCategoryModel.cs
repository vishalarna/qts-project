using System.Collections.Generic;
using QTD2.Infrastructure.Reports.Interfaces;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class EnablingObjectivesByCategoryModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<EnablingObjective> EnablingObjectives { get; set; }
        public bool ShowLabelsOnly { get; set; }
        public bool ShowMetaEosOnly { get; set; }
        public bool ShowSkillQualificationsOnly { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public EnablingObjectivesByCategoryModel(string title, string templatePath, List<string> displayColumns, string companyLogo, List<EnablingObjective> enablingObjectives, bool showLabelsOnly, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, bool showMetaEosOnly, bool showSkillQualificationsOnly)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            EnablingObjectives = enablingObjectives;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            ShowLabelsOnly = showLabelsOnly;
            ShowMetaEosOnly = showMetaEosOnly;
            ShowSkillQualificationsOnly = showSkillQualificationsOnly;
        }
    }
}
