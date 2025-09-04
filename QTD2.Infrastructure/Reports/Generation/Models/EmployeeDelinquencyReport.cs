using QTD2.Domain.Entities.Core;
using System;
using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Certifications;
using QTD2.Domain.Certifications.Models;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class EmployeeDelinquencyReport : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }     
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<CertificationFulfillmentStatus> CertificationFulfillmentStatuses { get; set; }
        public List<Organization> Organizations { get; set; }
        public List<int> NERCCertificationIds { get; set; }
        public int RegCertificationId { get; set; }
        public int Reg2CertificationId { get; set; }
        public bool SortEmployeesByOrganization { get;set;}

        public EmployeeDelinquencyReport(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<CertificationFulfillmentStatus> certificationFulfillmentStatuses, List<Organization> organizations,  List<int> nercCertificationIds, int regCertificationId, int reg2CertificationId, bool sortEmployeesByOrganization)
        {
            Title = title;
            TemplatePath = templatePath;
            DisplayColumns = displayColumns;
            CompanyLogo = companyLogo;
            DefaultTimeZone = defaultTimeZone;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            CertificationFulfillmentStatuses = certificationFulfillmentStatuses;
            Organizations = organizations;
            NERCCertificationIds = nercCertificationIds;
            RegCertificationId = regCertificationId;
            Reg2CertificationId = reg2CertificationId;
            SortEmployeesByOrganization = sortEmployeesByOrganization;
        }
    }
}
