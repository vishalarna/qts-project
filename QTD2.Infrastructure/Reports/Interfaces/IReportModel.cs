using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Interfaces
{
    public interface IReportModel
    {
        string Title { get; set; }
        string TemplatePath { get; set; }
        List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
    }
}
