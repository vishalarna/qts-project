using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TrainingMaterialForTaskEOByPositions : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Position> Positions { get; set; }
        public bool IncludeILAsOfEOTask { get; set; }
        public bool ShowTrainingResources { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public TrainingMaterialForTaskEOByPositions(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Position> positions,bool includeILAsOfEOTask,bool showTrainingResources, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Positions = positions;
            Title = title;
            CompanyLogo = companyLogo;
            IncludeILAsOfEOTask = includeILAsOfEOTask;
            ShowTrainingResources = showTrainingResources;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
        }
    }
}
