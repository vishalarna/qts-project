using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class StudentEvalutationResultsInstructorLed : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public string DefaultDateFormat { get; set; }
        public bool ShowSummaryofCommentsOnly { get; set; }
        public List<InstructorLedDataModel> InstructorLedClasses { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public StudentEvalutationResultsInstructorLed(string title, string templatePath, List<string> displayColumns, string companyLogo, List<InstructorLedDataModel> instructorLedClasses, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, string defaultDateFormat, bool showSummaryofCommentsOnly)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            InstructorLedClasses = instructorLedClasses;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            DefaultDateFormat = defaultDateFormat;
            ShowSummaryofCommentsOnly = showSummaryofCommentsOnly;
        }
    }
}
