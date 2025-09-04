using System.Collections.Generic;
using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class TestReportPaperBasedVersionModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Test> Tests { get; set; }
        public bool ShowCorrectAnswer { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public TestReportPaperBasedVersionModel(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<Test> tests, bool showCorrectAnswer )
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            Tests = tests;
            ShowCorrectAnswer = showCorrectAnswer; 
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
