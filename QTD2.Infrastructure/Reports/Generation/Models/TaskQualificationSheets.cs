using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class TaskQualificationSheets : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string SheetByType { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.Task> Tasks { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public TaskQualificationSheets(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Domain.Entities.Core.Task> tasks, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string sheetByType, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            Tasks = tasks;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            SheetByType = sheetByType;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
