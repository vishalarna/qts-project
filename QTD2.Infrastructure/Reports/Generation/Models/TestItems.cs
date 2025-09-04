using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
  public  class TestItems : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<TestItem> AllTestItems { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public TestItems(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<TestItem> allTestItems)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            DefaultTimeZone = defaultTimeZone;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            AllTestItems = allTestItems;
        }
    }
}
