using System.Collections.Generic;
using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
   public class SkillQualificationReportsModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<EnablingObjective> EnablingObjectives { get; set; }
        public Employee? Employee{ get; set; }
        public SkillQualificationReportsModel(
            string title,
            string templatePath,
            List<string> displayColumns,
            string companyLogo,
            List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements,
            string defaultTimeZone,
            List<EnablingObjective> enablingObjectives,
            Employee? employee)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            EnablingObjectives = enablingObjectives;
            Employee = employee;
        }
    }
}
