using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Reports.Interfaces;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
	public class TasksMetbyPosition : IReportModel
	{
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<Position> Positions { get; set; }
        public bool IsRRTask { get; set; }
        public bool IncludePseudoTasks { get; set; }
        public bool IncludeInactiveTasks { get; set; }
        public TasksMetbyPosition(string title, string templatePath, List<string> displayColumns, string companyLogo, string defaultTimeZone, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, List<Position> positions, bool isRRTask, bool includePseudoTasks, bool includeInactiveTasks)
        {
            Title = title;
            TemplatePath = templatePath;
            DisplayColumns = displayColumns;
            CompanyLogo = companyLogo;
            DefaultTimeZone = defaultTimeZone;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            Positions = positions;
            IsRRTask = isRRTask;
            IncludePseudoTasks = includePseudoTasks;
            IncludeInactiveTasks = includeInactiveTasks;
        }
    }
}
