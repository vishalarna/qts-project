using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TasksByTaskGroup : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public List<Domain.Entities.Core.Task> Tasks { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public bool TaskWithoutTaskGroup { get; set; }

        public TasksByTaskGroup(string title, string templatePath, List<string> displayColumns, List<Domain.Entities.Core.Task> tasks, string companyLogo, List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, bool taskWithoutTaskGroup)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Tasks = tasks;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            TaskWithoutTaskGroup = taskWithoutTaskGroup;
        }
    }
}
