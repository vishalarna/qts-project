using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Reports.Export;

namespace QTD2.Infrastructure.Model.Reports
{
    public class ExportReportModel
    {
        public ReportExportType ExportType { get; set; }
        public ReportCreateOrUpdateOptions Options { get; set; }
    }
}
