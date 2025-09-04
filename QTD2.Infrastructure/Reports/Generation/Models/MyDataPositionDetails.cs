using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class MyDataPositionDetails : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Position> Positions { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public MyDataPositionDetails(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Position> positions, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            Positions = positions;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
