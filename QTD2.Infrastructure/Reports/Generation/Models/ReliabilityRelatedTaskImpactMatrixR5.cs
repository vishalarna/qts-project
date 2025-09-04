using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
	public class ReliabilityRelatedTaskImpactMatrixR5 : IReportModel
	{
		public string Title { get; set; }
		public string TemplatePath { get; set; }
		public List<string> DisplayColumns { get; set; }
		public string CompanyLogo { get; set; }
		public string DefaultTimeZone { get; set; }
		public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
		public List<Position_Task> Position_Tasks { get; set; }
		public ReliabilityRelatedTaskImpactMatrixR5(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, List<Position_Task> position_Tasks)
		{
			DisplayColumns = displayColumns;
			TemplatePath = templatePath;
			Title = title;
			CompanyLogo = companyLogo;
			ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
			DefaultTimeZone = defaultTimeZone;
			Position_Tasks = position_Tasks;
		}
	}
}
