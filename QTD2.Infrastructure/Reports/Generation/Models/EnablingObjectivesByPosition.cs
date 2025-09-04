using System.Collections.Generic;
using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Position;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class EnablingObjectivesByPosition : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<PositionEOsOptions> PositionEOsOptions { get; set; }
        public List<int> IncludeObjectives { get; set; } = new List<int>();
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public EnablingObjectivesByPosition(string title, string templatePath, List<string> displayColumns, string companyLogo, List<PositionEOsOptions> positionEOsOptions, List<int> includeObjectives, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            PositionEOsOptions = positionEOsOptions;
            IncludeObjectives = includeObjectives;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
