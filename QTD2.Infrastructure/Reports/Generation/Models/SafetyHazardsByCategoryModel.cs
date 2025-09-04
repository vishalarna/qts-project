using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class SafetyHazardsByCategoryModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<Domain.Entities.Core.SaftyHazard> SafetyHazards { get; set; }
        public bool IncludeSafetyHazardDetails;
        public string DefaultDateFormat { get; set; }

        public SafetyHazardsByCategoryModel(string title,string templatePath,List<string> displayColumns,string companyLogo,List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements,string defaultTimeZone,List<SaftyHazard> safetyHazards,bool includeSafetyHazardDetails, string defaultDateFormat)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            SafetyHazards = safetyHazards;
            IncludeSafetyHazardDetails = includeSafetyHazardDetails;
            DefaultDateFormat = defaultDateFormat;
        }
    }
}
