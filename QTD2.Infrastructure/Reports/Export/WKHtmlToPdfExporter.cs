using WkHtmlToPdfDotNet;
using QTD2.Infrastructure.Reports.Interfaces;
using HtmlAgilityPack;
using System.Net;
using System.Text;
using System;
using System.IO;

namespace QTD2.Infrastructure.Reports.Export
{
    public class WKHtmlToPdfExporter : PDFExporter, IReportExporter
    {
        string _path = "reports\\temp";

        WkHtmlToPdfDotNet.Contracts.IConverter _converter;

        public WKHtmlToPdfExporter(WkHtmlToPdfDotNet.Contracts.IConverter converter)
        {
            _converter = converter;
        }

        public override string ExportReportToFile(string fileName, string content)
        {
            content = prepareHtmlForExport(content);
            string file = System.IO.Path.Combine(_path, fileName + ".pdf");
            var directory = Path.GetDirectoryName(file);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(content);
            HtmlNode pagerElement = document.DocumentNode.SelectSingleNode("//div[@class='repeatedReportHeader']");
            HtmlNodeCollection footerElements = document.DocumentNode.SelectNodes("//div[@class='customized-footer']");
            HtmlNode landscapeElement = document.DocumentNode.SelectSingleNode("//table[contains(@class, 'landscape-report')]");
            if (pagerElement != null)
            {
                var currentStyle = pagerElement.GetAttributeValue("style", string.Empty);
                var newStyle = "font-family: Roboto, 'Helvetica Neue', sans-serif;padding-top:10px" + currentStyle;
                pagerElement.SetAttributeValue("style", newStyle); 
            }
            var headerData = pagerElement?.OuterHtml;

            var footerData =
            "<html>" +
            "<head><script>function substituteDocumentParameters() {  var vars={};  var x=document.location.search.substring(1).split('&');  for(var i in x) {var z=x[i].split('=',2);vars[z[0]] = unescape(z[1]);}  var x=['frompage','topage','page','webpage','section','subsection','subsubsection'];  for(var i in x) {    var y = document.getElementsByClassName(x[i]);    for(var j=0; j<y.length; ++j) y[j].textContent = vars[x[i]];  }}</script></head>" +
            "<body style='border:0; margin: 0;' onload='substituteDocumentParameters()'>" +
            "<table style='font-size: 12pt; width:100%; font-family: Roboto, Helvetica Neue, sans-serif; padding:10px 0px;'>" +
            "<tr>" +
            (footerElements != null ? "<td style='text-align:left;'>" + footerElements[0]?.OuterHtml + "</td>" : "") +
            "<td style='text-align:right;'>Page <span class='page'></span> of <span class='topage'></span></td>" +
            "</tr>" +
            "</table>" +
            "</body>" +
            "</html>";


            if (pagerElement != null)
            {
                pagerElement?.Remove();
            }
            if (footerElements != null)
            {
                foreach (var footerElement in footerElements)
                {
                    footerElement.Remove();
                }
            }
            var updatedContent = document.DocumentNode.OuterHtml;

            //change name to "Header - {Report Name} - {Instance Name} or "Header - New Guid()"
            string tempDirectory = Path.GetTempPath();
            string uniqueFileName = "Header -" + Guid.NewGuid().ToString() + ".html";
            string footerUniqueFileName = "Footer -" + Guid.NewGuid().ToString() + ".html";
            string headerFilePath = Path.Combine(tempDirectory, uniqueFileName);
            string footerFilePath = Path.Combine(tempDirectory, footerUniqueFileName);

            headerData = "<!DOCTYPE html> \n" + (headerData ?? "");
            File.WriteAllText(headerFilePath, headerData, Encoding.UTF8);
            File.WriteAllText(footerFilePath, footerData, Encoding.UTF8);
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = landscapeElement != null ? Orientation.Landscape : Orientation.Portrait,
                    PaperSize = landscapeElement != null ? PaperKind.A4 : PaperKind.Letter,
                    Out = file,
                    Margins = new MarginSettings()
                    {
                        Top = 22,
                        Bottom = 12
                    }
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HeaderSettings = {
                            HtmlUrl = headerFilePath,
                            Spacing = 3
                        },
                        HtmlContent = updatedContent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        FooterSettings = { HtmlUrl = footerFilePath, Line = true,Spacing = 3},
                    }
                }
            };

            try
            {
                _converter.Convert(doc);

                //delete the file
                if (File.Exists(headerFilePath))
                {
                    File.Delete(headerFilePath);
                }
                if (File.Exists(footerFilePath))
                {
                    File.Delete(footerFilePath);
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }


            return file;
        }
    }
}