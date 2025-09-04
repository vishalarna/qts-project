using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Certifications.Models;
using System.Linq;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TrainingSummaryByPosition : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Position> Positions { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<CertificationFulfillmentStatus> CertificationFulfillmentStatuses { get; set; }
        public List<CertificationFulfillmentStatus> EmergencyResponseCertificationFulfillmentStatuses { get; set; }
        public int RegCertificationId { get; set; }
        public int Reg2CertificationId { get; set; }
        public int OtherCertificationId { get; set; }
        public int ProfHoursCertificationId { get; set; }
        public bool UseOrganizations { get; set; }
        public List<string> Organizations { get; set; }

        public TrainingSummaryByPosition(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Position> positions, bool useOrganizations, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, List<CertificationFulfillmentStatus> certificationFulfillmentStatuses, List<CertificationFulfillmentStatus> emergencyResponseCertificationFulfillmentStatuses, int regCertificationId, int reg2CertificationId, int otherCertificationId, int profHoursCertificationId)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Positions = positions;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            CertificationFulfillmentStatuses = certificationFulfillmentStatuses;
            EmergencyResponseCertificationFulfillmentStatuses = emergencyResponseCertificationFulfillmentStatuses;
            RegCertificationId = regCertificationId;
            Reg2CertificationId = reg2CertificationId;
            OtherCertificationId = otherCertificationId;
            UseOrganizations = useOrganizations;
            ProfHoursCertificationId = profHoursCertificationId;
            Organizations = positions.SelectMany(r => r.EmployeePositions).SelectMany(r => r.Employee.EmployeeOrganizations).Select(r => r.Organization.Name).Distinct().ToList();
        }
    }
}
