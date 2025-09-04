using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

using QTD2.Application.Interfaces.Services.QTD;
using QTD2.Application.Interfaces.Services.Shared;

namespace _TestApp.Controllers
{
    public class ReportsController : Controller
    {
        IReportsService _reportsService;
        IReportGeneratorService _reportGeneratorService;
        QTD2.Application.Interfaces.Services.QTD.INotificationService _notificationService;


        public ReportsController(
            IReportsService reportsService,
            IReportGeneratorService reportGeneratorService,
            QTD2.Application.Interfaces.Services.QTD.INotificationService notificationService
            )
        {
            _reportsService = reportsService;
            _reportGeneratorService = reportGeneratorService;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            var reports = await _reportsService.GetAllAsync();
            return View(reports);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(int reportId)
        {
            var report = await _reportsService.GetAsync(reportId);
            var content = await _reportGeneratorService.GenerateReportAsync(report);
            return Content(content);
        }

        [HttpPost]
        public async Task<IActionResult> ExportReport(QTD2.Infrastructure.Reports.Export.ReportExportType exportType, int reportId)
        {
            var report = await _reportsService.GetAsync(reportId);
            var file = await _reportGeneratorService.ExportReportAsync(exportType, report);
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);

            return File(
                fileStream: fs,
                contentType: System.Net.Mime.MediaTypeNames.Application.Octet,
                fileDownloadName: Path.GetFileName(file));
        }

        [HttpPost]
        public async Task<IActionResult> SendReport(string sendTo, QTD2.Infrastructure.Reports.Export.ReportExportType exportType, int reportId)
        {
            var report = await _reportsService.GetAsync(reportId);
            var file = await _reportGeneratorService.ExportReportAsync(exportType, report);

            await _notificationService.SendReportAsync(report, file, new List<string>() { sendTo });

            System.IO.File.Delete(file);

            return Ok();
        }
    }
}
