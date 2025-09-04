using QTD2.Infrastructure.Reports.Interfaces;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using System;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class ILAbyMetaILA : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public List<MetaILA> MetaILAs { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<int> ILAObjectivesLinks { get; set; }

        public ILAbyMetaILA(string title, string templatePath, List<string> displayColumns, string companyLogo, List<MetaILA> metaILAs, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, List<int> iLAObjectivesLinks)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            MetaILAs = metaILAs;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            ILAObjectivesLinks = iLAObjectivesLinks;
        }
    }
}
