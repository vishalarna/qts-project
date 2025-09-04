using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class ILAsByTaskModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Task> Tasks { get; set; }
        public string DefaultDateFormat { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public ILAsByTaskModel(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Task> tasks, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, string defaultDateFormat)
        {
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            Tasks = tasks;
            DisplayColumns = displayColumns;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            DefaultDateFormat = defaultDateFormat;
        }
    }
}
