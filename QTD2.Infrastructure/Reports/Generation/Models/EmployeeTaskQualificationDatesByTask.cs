using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class EmployeeTaskQualificationDatesByTask : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<DateTime> DateRange { get; set; }
        public string DefaultDateFormat { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<Domain.Entities.Core.Task> Tasks { get; set; }
        public EmployeeTaskQualificationDatesByTask(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<DateTime> dateRange, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<Domain.Entities.Core.Task> tasks, string defaultDateFormat)
        {
            Title = title;
            TemplatePath = templatePath;
            DisplayColumns = displayColumns;
            CompanyLogo = companyLogo;
            DefaultTimeZone = defaultTimeZone;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DateRange = dateRange;
            Tasks = tasks;
            DefaultDateFormat = defaultDateFormat;
        }
    }
}
