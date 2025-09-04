using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
	public class EmployeeTrainingTowardCertAndAllRequiredTrainingSummary : IReportModel
	{
		public string Title { get; set; }
		public string TemplatePath { get; set; }
		public List<string> DisplayColumns { get; set; }
		public string CompanyLogo { get; set; }
		public string DefaultTimeZone { get; set; }
		public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
		public List<CertificationFulfillmentStatus> CertificationFulfillmentStatuses { get; set; }
		public int EmergencyResponseCertificationId { get; set; }
		public int RegCertificationId { get; set; }
		public int Reg2CertificationId { get; set; }
		public int OtherCertificationId { get; set; }

		public EmployeeTrainingTowardCertAndAllRequiredTrainingSummary(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, List<CertificationFulfillmentStatus> certificationFulfillmentStatuses, int emergencyResponseCertificationId, int regCertificationId, int reg2CertificationId, int otherCertificationId)
		{
			DisplayColumns = displayColumns;
			TemplatePath = templatePath;
			Title = title;
			CompanyLogo = companyLogo;
			ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
			DefaultTimeZone = defaultTimeZone;
			CertificationFulfillmentStatuses = certificationFulfillmentStatuses;
			EmergencyResponseCertificationId = emergencyResponseCertificationId;
			RegCertificationId = regCertificationId;
			Reg2CertificationId = reg2CertificationId;
			OtherCertificationId = otherCertificationId;
		}

	}
}
