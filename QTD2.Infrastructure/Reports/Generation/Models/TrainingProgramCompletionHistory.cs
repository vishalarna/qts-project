using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TrainingProgramCompletionHistory : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<DateTime> DateRange { get; set; }
        public List<Position> Positions { get; set; }
        public List<IDPSchedule> IDPSchedules { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public TrainingProgramCompletionHistory(string title, string templatePath, List<string> displayColumns, string companyLogo, List<DateTime> dateRange, List<Position> positions , List<IDPSchedule> iDPSchedules, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            DateRange = dateRange;
            Positions = positions;
            IDPSchedules = iDPSchedules;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}