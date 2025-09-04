using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class CertifiedEmployeesforGivenCertificate : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }

        public List<string> DisplayColumns { get; set; } 
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Certification> Certifications { get; set; }
        public List<Organization> Organizations { get; set; }

        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public CertifiedEmployeesforGivenCertificate(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Certification> certifications, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, List<Organization> organizations)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            Certifications = certifications;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            Organizations = organizations;
        }
    }
}
