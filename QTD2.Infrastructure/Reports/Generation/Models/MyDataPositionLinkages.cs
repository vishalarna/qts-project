using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class MyDataPositionLinkages : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<EnablingObjective> EnablingObjectives { get; set; }
        public List<Task> Tasks { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public MyDataPositionLinkages(string title, string templatePath, List<string> displayColumns, string companyLogo, List<EnablingObjective> enablingObjectives, List<Task> tasks, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            EnablingObjectives = enablingObjectives;
            Title = title;
            CompanyLogo = companyLogo;
            Tasks = tasks;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
