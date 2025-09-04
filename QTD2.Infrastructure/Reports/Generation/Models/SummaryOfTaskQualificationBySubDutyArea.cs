using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class SummaryOfTaskQualificationBySubDutyArea : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Domain.Entities.Core.Task> Tasks { get; set; }
        public bool IncludetaskQualificationDetails { get; set; }
        public bool OnlyRRTasks { get; set; }

        public SummaryOfTaskQualificationBySubDutyArea(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<Employee> employees, List<Domain.Entities.Core.Task> tasks, bool includetaskQualificationDetails, bool onlyRRTasks)
        {
            Title = title;
            TemplatePath = templatePath;
            DisplayColumns = displayColumns;
            CompanyLogo = companyLogo;
            DefaultTimeZone = defaultTimeZone;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            Employees = employees;
            Tasks = tasks;
            IncludetaskQualificationDetails = includetaskQualificationDetails;
            OnlyRRTasks = onlyRRTasks;
        }
    }
}
