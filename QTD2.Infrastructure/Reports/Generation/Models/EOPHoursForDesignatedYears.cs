using System.Collections.Generic;
using QTD2.Infrastructure.Reports.Interfaces;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class EOPHoursForDesignatedYears : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public bool IsSummaryReport{ get; set; }
        public List<Position> Positions { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public EOPHoursForDesignatedYears(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Position> positions,bool isSummaryReport, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            Title = title;
            TemplatePath = templatePath;
            DisplayColumns = displayColumns;
            CompanyLogo = companyLogo;
            Positions = positions;
            IsSummaryReport = isSummaryReport;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
