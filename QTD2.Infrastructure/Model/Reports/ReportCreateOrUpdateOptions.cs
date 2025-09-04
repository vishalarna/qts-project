using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Reports
{
    public class ReportCreateOrUpdateOptions
    {
        public int ReportSkeletonId { get; set; }
        public string InternalReportTitle { get; set; }
        public List<ReportFilter> Filters { get; set; }
        public List<string> DisplayColumns { get; set; }
    }
}
