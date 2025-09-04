using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TrainingIssueDetailsModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.TrainingIssue> TrainingIssues { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public TrainingIssueDetailsModel(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Domain.Entities.Core.TrainingIssue> trainingIssues, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            TrainingIssues = trainingIssues;
            DisplayColumns = displayColumns;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
