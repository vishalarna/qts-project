using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class TasksMetbyEmployee : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<Employee> Employees { get; set; }
        public bool ReliabilityRelatedTask { get; set; }
        public bool PseudoTask { get; set; }
        public bool InactiveTask { get; set; }

        public TasksMetbyEmployee(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<Employee> employees, bool reliabilityRelatedTask, bool pseudoTask, bool inactiveTask)
        {
            Title = title;
            TemplatePath = templatePath;
            DisplayColumns = displayColumns;
            CompanyLogo = companyLogo;
            DefaultTimeZone = defaultTimeZone;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            Employees = employees;
            ReliabilityRelatedTask = reliabilityRelatedTask;
            PseudoTask = pseudoTask;
            InactiveTask = inactiveTask;
        }
    }
}
