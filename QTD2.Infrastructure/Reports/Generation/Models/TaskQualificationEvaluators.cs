using System;
using System.Collections.Generic;
using System.Linq;
using QTD2.Infrastructure.Reports.Interfaces;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TaskQualificationEvaluators : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Employee> Evaluators { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public bool ShowAssignedAndPendingQualifications { get; set; }
        public bool ShowCompletedTaskQualifications { get; set; }
        public TaskQualificationEvaluators(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Employee> evaluators, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, bool showAssignedAndPendingQualifications, bool showCompletedTaskQualifications)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            Evaluators = evaluators;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            ShowAssignedAndPendingQualifications = showAssignedAndPendingQualifications;
            ShowCompletedTaskQualifications = showCompletedTaskQualifications;
        }
    }
}
