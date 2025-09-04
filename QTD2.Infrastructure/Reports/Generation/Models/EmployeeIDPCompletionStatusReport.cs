using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
	public class EmployeeIDPCompletionStatusReport : IReportModel
	{
		public string Title { get; set; }
		public string TemplatePath { get; set; }
		public List<string> DisplayColumns { get; set; }
		public string CompanyLogo { get; set; }
		public List<CertificationFulfillmentStatus> CertificationFulfillmentStatuses { get; set; }
		public Certification EmergencyResponseCertification { get; set; }
		public int RegCertificationId { get; set; }
		public int Reg2CertificationId { get; set; }
		public int OtherCertificationId { get; set; }
		public List<IDPSchedule> IdpSchedules { get; set; }
		public string DefaultTimeZone { get; set; }
		public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
		public int Year { get; set; }
        public string CompletionStatus { get; set; }
        public EmployeeIDPCompletionStatusReport(string title, string templatePath, List<string> displayColumns, string companyLogo, List<CertificationFulfillmentStatus> certificationFulfillmentStatuses, Certification emergencyResponseCertification, int regCertificationId, int reg2CertificationId, int otherCertificationId, List<IDPSchedule> idpSchedules, string defaultTimeZone, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, int year, string completionStatus)
		{
			Title = title;
			TemplatePath = templatePath;
			DisplayColumns = displayColumns;
			CompanyLogo = companyLogo;
			CertificationFulfillmentStatuses = certificationFulfillmentStatuses;
            EmergencyResponseCertification = emergencyResponseCertification;
			RegCertificationId = regCertificationId;
			Reg2CertificationId = reg2CertificationId;
			OtherCertificationId = otherCertificationId;
            IdpSchedules = idpSchedules;
			DefaultTimeZone = defaultTimeZone;
			ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
			Year = year;
			CompletionStatus = completionStatus;

        }
	}
}
