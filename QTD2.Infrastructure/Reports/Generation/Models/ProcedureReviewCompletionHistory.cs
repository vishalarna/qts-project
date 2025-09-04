using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using System;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class ProcedureReviewCompletionHistory : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Procedure> Procedures { get; set; }
        public List<DateTime> DateRange { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<Position> Positions { get; set; }
        public List<Organization> Organizations { get; set; }

        public ProcedureReviewCompletionHistory(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Procedure> procedures, List<DateTime> dateRange, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements,
            string defaultTimeZone, List<Position> positions, List<Organization> organizations)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            Procedures = procedures;
            DateRange = dateRange;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            DefaultTimeZone = defaultTimeZone;
            Positions = positions;
            Organizations = organizations;
        }
    }
}
