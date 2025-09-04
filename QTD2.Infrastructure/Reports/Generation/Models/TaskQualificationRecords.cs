using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TaskQualificationRecords : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<TaskQualification> TaskQualifications { get; set; }
        public List<Employee> Employees { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<DateTime> DateRange { get; set; }
        public string DefaultDateFormat { get; set; }
        public TaskQualificationRecords(string title, string templatePath, List<string> displayColumns,string companyLogo, List<TaskQualification> taskQualifications,List<Employee> employees, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, List<DateTime> dateRange, string defaultDateFormat)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            TaskQualifications = taskQualifications;
            Employees = employees;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            DateRange = dateRange;
            DefaultDateFormat = defaultDateFormat;
        }
    }
}
