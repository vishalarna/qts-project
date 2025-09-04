using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
	public class EmployeeTrainingTowardProceduresAndRegulatoryRequirements : IReportModel
	{
		public string Title { get; set; }
		public string TemplatePath { get; set; }
		public List<string> DisplayColumns { get; set; }
		public string CompanyLogo { get; set; }
		public string DefaultTimeZone { get; set; }
		public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
		public List<Procedure> Procedures { get; set; }
		public List<RegulatoryRequirement> RegulatoryRequirements { get; set; }
		public List<Employee> Employees { get; set; }
        public string DefaultDateFormat { get; set; }
        public List<DateTime> DateRange { get; set; }
        public EmployeeTrainingTowardProceduresAndRegulatoryRequirements(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<Procedure> procedures, List<RegulatoryRequirement> regulatoryRequirements, List<Employee> employees, string defaultDateFormat, List<DateTime> dateRange)
        {
            Title = title;
            TemplatePath = templatePath;
            DisplayColumns = displayColumns;
            CompanyLogo = companyLogo;
            DefaultTimeZone = defaultTimeZone;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            Procedures = procedures;
            RegulatoryRequirements = regulatoryRequirements;
            Employees = employees;
            DefaultDateFormat = defaultDateFormat;
            DateRange = dateRange;
        }
    }
}
