using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class EmployeeTrainingStatusModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Employee> Employees { get; set; }
        public List<DateTime> DateRange { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public EmployeeTrainingStatusModel(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Employee> employees, List<DateTime> dateRange, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Employees = employees;
            Title = title;
            CompanyLogo = companyLogo;
            DateRange = dateRange;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
