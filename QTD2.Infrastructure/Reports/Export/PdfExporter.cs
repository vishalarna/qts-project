using HtmlAgilityPack;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Export
{
    public class PDFExporter : IReportExporter
    {
        public virtual string ExportReportToFile(string fileName, string content)
        {
            throw new System.NotImplementedException();
        }

        protected string prepareHtmlForExport(string content)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(content);
            HtmlNode pagerElement = document.DocumentNode.SelectSingleNode("//div[@class='pager']");
            if (pagerElement != null)
            {
                pagerElement?.Remove();
            }
            else
            {
                pagerElement = document.DocumentNode.SelectSingleNode("//div[@id='pager']");
                if (pagerElement != null)
                {
                    pagerElement?.Remove();
                }
            }
            HtmlNode stripElement = document.DocumentNode.SelectSingleNode("//td[@class='strip-container']");
            if (stripElement != null)
            {
                stripElement?.RemoveClass("strip-container");
            }
            var updatedContent = document.DocumentNode.OuterHtml;
            return updatedContent;
        }
    }
}
