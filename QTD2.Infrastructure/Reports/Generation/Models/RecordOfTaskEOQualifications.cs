using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class RecordOfTaskEOQualifications : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public bool PrintCompletedTasksFirst { get; set; }
        public bool IncludeEvaluatorDetails { get; set; }
        public List<Domain.Entities.Core.TaskQualification> TaskQualifications { get; set; }
        public List<Domain.Entities.Core.Employee> Employees { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public RecordOfTaskEOQualifications(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Domain.Entities.Core.TaskQualification> taskQualifications, List<Domain.Entities.Core.Employee> employees, bool printCompletedTasksFirst,bool includeEvaluatorDetails, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            TaskQualifications = taskQualifications;
            Employees = employees;
            Title = title;
            CompanyLogo = companyLogo;
            PrintCompletedTasksFirst = printCompletedTasksFirst;
            IncludeEvaluatorDetails = includeEvaluatorDetails;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
