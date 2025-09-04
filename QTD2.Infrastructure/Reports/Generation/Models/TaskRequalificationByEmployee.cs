using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TaskRequalificationByEmployee : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.Employee> Employees { get; set; }
        public DateTime TRStartDate { get; set; }
        public DateTime TREndDate { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public TaskRequalificationByEmployee(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Domain.Entities.Core.Employee> employees, DateTime trStartDate, DateTime trEndDate, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Employees = employees;
            Title = title;
            CompanyLogo = companyLogo;
            TRStartDate = trStartDate;
            TREndDate = trEndDate;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
