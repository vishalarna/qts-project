using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
	public class TotalNERCCEHsForTheYearToDate : IReportModel
	{
		public string Title { get; set; }
		public string TemplatePath { get; set; }
		public List<string> DisplayColumns { get; set; }
		public string CompanyLogo { get; set; }
		public string DefaultTimeZone { get; set; }
		public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
		public List<CertificationFulfillmentStatus> CertificationFulfillmentStatuses { get; set; }

		public TotalNERCCEHsForTheYearToDate(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<CertificationFulfillmentStatus> certificationFulfillmentStatuses)
		{
			Title = title;
			TemplatePath = templatePath;
			DisplayColumns = displayColumns;
			CompanyLogo = companyLogo;
			DefaultTimeZone = defaultTimeZone;
			ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
			CertificationFulfillmentStatuses = certificationFulfillmentStatuses;
		}
	}
}
