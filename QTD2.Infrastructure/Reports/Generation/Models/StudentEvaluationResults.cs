using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class StudentEvaluationResults : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public List<Domain.Entities.Core.ClassSchedule_Evaluation_Roster> ClassEvalRosters{ get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<DateTime> DateRange { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public string DefaultDateFormat { get; set; }

        public StudentEvaluationResults(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Domain.Entities.Core.ClassSchedule_Evaluation_Roster> classEvalRosters, List<DateTime> dateRange, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, string defaultDateFormat)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            ClassEvalRosters = classEvalRosters;
            Title = title;
            CompanyLogo = companyLogo;
            DateRange = dateRange;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            DefaultDateFormat = defaultDateFormat;
        }
    }
}
