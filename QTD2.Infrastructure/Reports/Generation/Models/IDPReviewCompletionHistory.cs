using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class IDPReviewCompletionHistory : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.IDP_Review> IDPReviews { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public IDPReviewCompletionHistory(string title, string templatePath, List<string> displayColumns, List<Domain.Entities.Core.IDP_Review> idpReview, string companyLogo, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            IDPReviews = idpReview;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }

}
