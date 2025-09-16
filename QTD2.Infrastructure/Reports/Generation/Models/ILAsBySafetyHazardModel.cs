using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class ILAsBySafetyHazardModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<SaftyHazard> SaftyHazards { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public bool IncludeSafetyHazardsDetail { get; set; }
        public bool IncludeMetaILA{ get; set; }

        public ILAsBySafetyHazardModel(string title, string templatePath, List<string> displayColumns, string companyLogo, List<SaftyHazard> saftyHazards, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, bool includeSafetyHazardsDetail, bool includeMetaILA)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            SaftyHazards = saftyHazards;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            IncludeSafetyHazardsDetail = includeSafetyHazardsDetail;
            IncludeMetaILA = includeMetaILA;
        }
    }
}
