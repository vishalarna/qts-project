using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class ListOfTaskEvaluators : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }

        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Employee> Evaluators { get; set; }

        public List<TaskQualification> TaskQualifications { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public ListOfTaskEvaluators(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Employee> evaluators, List<TaskQualification> taskQuals, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            Evaluators = evaluators;
            TaskQualifications = taskQuals;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
