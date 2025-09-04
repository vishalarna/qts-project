using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Export
{
    public class DefaultReportExporterFactory : IReportExporterFactory
    {

        WkHtmlToPdfDotNet.Contracts.IConverter _converter;

        public DefaultReportExporterFactory(WkHtmlToPdfDotNet.Contracts.IConverter converter)
        {
            _converter = converter;
        }

        public IReportExporter GetExporter(ReportExportType reportExportType)
        {
           switch(reportExportType)
            {
                case ReportExportType.Excel:
                    return new ExcelExporter();
                case ReportExportType.Pdf:
                    return new WKHtmlToPdfExporter(_converter);
                default:
                    throw new System.InvalidOperationException("No Such Exporter");
            }
        }
    }
}
