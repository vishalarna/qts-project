using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class StudentEvaluationForm : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public string DefaultDateFormat { get; set; }
        public List<Domain.Entities.Core.ClassSchedule_StudentEvaluations_Link> ClassSchedule_StudentEvaluations_Links { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public StudentEvaluationForm(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Domain.Entities.Core.ClassSchedule_StudentEvaluations_Link> classSchedule_StudentEvaluations_Links, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, string defaultDateFormat)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            ClassSchedule_StudentEvaluations_Links = classSchedule_StudentEvaluations_Links;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            DefaultDateFormat = defaultDateFormat;
        }
    }
}
