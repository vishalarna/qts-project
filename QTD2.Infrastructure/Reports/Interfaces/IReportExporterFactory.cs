using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Infrastructure.Reports.Export;

namespace QTD2.Infrastructure.Reports.Interfaces
{
    public interface IReportExporterFactory
    {
        IReportExporter GetExporter(ReportExportType reportExportType);
    }
}
