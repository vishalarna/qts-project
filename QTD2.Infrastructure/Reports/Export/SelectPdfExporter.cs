using HtmlAgilityPack;
using QTD2.Infrastructure.Reports.Interfaces;
using SelectPdf;

namespace QTD2.Infrastructure.Reports.Export
{
    public class SelectPdfExporter : PDFExporter, IReportExporter
    {
        string _path = "reports\\temp";

        public override string ExportReportToFile(string fileName, string content)
        {
            content = prepareHtmlForExport(content);
            string file = System.IO.Path.Combine(_path, fileName + ".pdf");

            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.Letter;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(content, "");

            doc.Save(file);
            doc.Close();

            return file;
        }
    }
}
