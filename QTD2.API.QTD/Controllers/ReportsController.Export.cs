using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Infrastructure.Reports;
using QTD2.Infrastructure.Reports.Export;
using System.IO;

namespace QTD2.API.QTD.Controllers
{

    public partial class ReportsController
    {
        [HttpPost]
        [Route("/reports/export/{reportId}")]
        public async Task<IActionResult> ExportReportAsync(int reportId, ExportReportModel model)
        {
            var report = await _reportsService.GetAsync(reportId, model.Options);
            var file = await _reportGeneratorService.ExportReportAsync(model.ExportType, report);

            var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = Path.GetFileName(file) }.ToString());
            return File(
                fileStream: fs,
                contentType: System.Net.Mime.MediaTypeNames.Application.Octet,
                fileDownloadName: Path.GetFileName(file));
        }

        [HttpPost]
        [Route("/reports/export")]
        public async Task<IActionResult> ExportReportAsync(ExportReportModel model)
        {
            var report = await _reportsService.CreateReportAsync(model.Options, false);

            var file = await _reportGeneratorService.ExportReportAsync(model.ExportType, report);
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = Path.GetFileName(file) }.ToString());
            return File(
                fileStream: fs,
                contentType: System.Net.Mime.MediaTypeNames.Application.Octet,
                fileDownloadName: Path.GetFileName(file));
        }

    }
}
