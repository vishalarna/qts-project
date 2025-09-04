using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
	public class EMPTestStatistics : IReportModel
	{
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<ClassSchedule> ClassSchedules { get; set; }
		public List<Test> Tests { get; set; }
        public string DefaultDateFormat { get; set; }
        public EMPTestStatistics(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements ,List<ClassSchedule> classSchedules, List<Test> tests, string defaultDateFormat)
		{
            Title = title;
            TemplatePath = templatePath;
            DisplayColumns = displayColumns;
            CompanyLogo = companyLogo;
            DefaultTimeZone = defaultTimeZone;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            ClassSchedules = classSchedules;
            Tests = tests;
            DefaultDateFormat = defaultDateFormat;
        }
    }
}
