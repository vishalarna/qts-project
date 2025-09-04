using QTD2.Infrastructure.Reports.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Reports
{
    public class SendReportModel
    {
        public ReportCreateOrUpdateOptions CreateOrUpdateOptions { get; set; }
        public ReportExportType ExportType { get; set; }
        public List<string> Tos { get; set; }
    }
}
