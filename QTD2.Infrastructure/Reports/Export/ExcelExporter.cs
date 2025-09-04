using QTD2.Infrastructure.Reports.Interfaces;
using HtmlAgilityPack;
using System;
using ClosedXML.Excel;
using System.Linq;
using System.Net;
using System.IO;

namespace QTD2.Infrastructure.Reports.Export
{
    public class ExcelExporter : IReportExporter
    {
        string _path = "reports\\temp";
        public string ExportReportToFile(string fileName, string content)
        {
            string file = System.IO.Path.Combine(_path, fileName + ".xlsx");
            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Report");

            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            // Remove pager element if it exists
            HtmlNode pagerElement = doc.DocumentNode.SelectSingleNode("//tr[@class='pager']");
            pagerElement?.Remove();

            var table = doc.GetElementbyId("content-table");
            if (table == null) throw new InvalidOperationException("No table found in the provided HTML content.");

            HtmlNode headerNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'header-combined-line')]");

            if (headerNode != null)
            {
                var logoImg = headerNode.SelectSingleNode(".//img[@class='header-logo']");
                if (logoImg != null)
                {
                    int currentRow = 1;

                    // Reduce the row height to fit the image more closely
                    ws.Row(currentRow).Height = 45; // Adjust as needed

                    // Reduce the width of column A (1)
                    ws.Column(1).Width = 10; // Adjust to fit logo nicely

                    string src = logoImg.GetAttributeValue("src", "");
                    if (src.StartsWith("data:image"))
                    {
                        string base64Data = src.Split(',')[1];
                        byte[] imageBytes = Convert.FromBase64String(base64Data);

                        using (var imageStream = new MemoryStream(imageBytes))
                        {
                            ws.AddPicture(imageStream, "CompanyLogo")
                              .MoveTo(ws.Cell(currentRow, 1))
                              .WithSize(60, 40); // Smaller size for tighter fit
                        }
                    }
                }
            }

            var trs = table.Descendants("tr").Where(x=>!x.GetClasses().Contains("excel-not-include")).ToList();
            int maxColumns = trs.Max(tr => tr.SelectNodes("td | th")?.Count() ?? 0);
            bool[,] isMergedCell = new bool[trs.Count(), maxColumns];

            for (int rowIdx = 0; rowIdx < trs.Count(); rowIdx++)
            {
                var tr = trs[rowIdx];
                if (tr.Descendants("td").Any(td => string.IsNullOrWhiteSpace(td.InnerText) && td.Descendants("table").Any()))
                {
                    continue;
                }
                if (tr.GetClasses().Contains("excel-remove")) continue;
                if (tr.Descendants("table").Any() && !tr.GetClasses().Contains("excel-not-skip"))
                    continue;

                var tds = tr.SelectNodes("td | th");
                if (tds == null) continue;

                bool isExpired = tr.GetClasses().Contains("expired-row");
                int colIdx = 0;
                foreach (var td in tds)
                {
                    while (colIdx < maxColumns && isMergedCell[rowIdx, colIdx])
                    {
                        colIdx++;
                    }

                    var colSpan = Convert.ToInt32(td.GetAttributeValue("colspan", "1"));
                    var rowSpan = Convert.ToInt32(td.GetAttributeValue("rowspan", "1"));
                    var checkbox = td.Descendants("input")
                                     .FirstOrDefault(x => x.GetAttributeValue("type", "") == "checkbox");

                    bool isBold = td.DescendantsAndSelf()
                   .Any(node => node.Name == "b" ||
                                node.GetAttributeValue("style", "").Split(';')
                                    .Any(style => style.Trim().StartsWith("font-weight") &&
                                                  (style.Contains("bold") ||
                                                   int.TryParse(style.Split(':')[1].Trim(), out int weight) && weight >= 600)));
                    string cellValue = checkbox != null ? (checkbox.GetAttributeValue("checked", null) != null ? "✔" : "") : WebUtility.HtmlDecode(td.InnerText.Trim().Replace(Environment.NewLine, " "));

                    var cell = ws.Cell(rowIdx + 1, colIdx + 1);
                    cell.Value = cellValue;
                    if (isBold)
                    {
                        cell.Style.Font.Bold = true;
                    }
                    if (isExpired)
                    {
                        cell.Style.Font.Italic = true;
                    }

                    if (colSpan > 1 || rowSpan > 1)
                    {
                        int endRow = rowIdx + rowSpan;
                        int endCol = colIdx + colSpan;
                        ws.Range(rowIdx + 1, colIdx + 1, endRow, endCol).Merge();

                        for (int r = rowIdx; r < endRow; r++)
                        {
                            for (int c = colIdx; c < endCol; c++)
                            {
                                if (r < trs.Count() && c < maxColumns)
                                {
                                    isMergedCell[r, c] = true;
                                }
                            }
                        }
                    }

                    colIdx += colSpan;
                }
            }

            workbook.SaveAs(file);
            return file;
        }
    }
}
