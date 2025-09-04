using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using System;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class DIFSurveyBlankForm : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public List<DIFSurvey> DIFSurvey { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public DIFSurveyBlankForm(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Domain.Entities.Core.DIFSurvey>difSurvey, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            DIFSurvey = difSurvey;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
