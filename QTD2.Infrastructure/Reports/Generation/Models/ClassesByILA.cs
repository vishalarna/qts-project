using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using System;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class ClassesByILA : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public List<ClassSchedule> ClassSchedules { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<DateTime> DateRange { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public ClassesByILA(string title, string templatePath, List<string> displayColumns, string companyLogo, List<ClassSchedule> classSchedules, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<DateTime> dateRange, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            ClassSchedules = classSchedules;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            DateRange = dateRange;
        }
    }
}
