using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TrainingModuleCompletionHistoryByEmployee : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<MetaILA_Employee> MetaILAs { get; set; }
        public List<Employee> Employees { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public TrainingModuleCompletionHistoryByEmployee(string title, string templatePath, List<string> displayColumns, string companyLogo, List<MetaILA_Employee> metaILAs, List<Employee> employees, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            MetaILAs = metaILAs;
            Title = title;
            CompanyLogo = companyLogo;
            Employees = employees;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
