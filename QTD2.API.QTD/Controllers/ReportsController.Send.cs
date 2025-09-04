using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ReportsController
    {
        [HttpPost]
        [Route("/reports/send/{reportId}")]
        public async Task<IActionResult> SendReportAsync(int reportId, SendReportModel model)
        {
            var report = await _reportsService.GetAsync(reportId, model.CreateOrUpdateOptions);
            var file = await _reportGeneratorService.ExportReportAsync(model.ExportType, report);

            await _notificationService.SendReportAsync(report, file, model.Tos);

            //does this go here or in the notification service?
            System.IO.File.Delete(file);

            return Ok();
        }

        [HttpPost]
        [Route("/reports/send")]
        public async Task<IActionResult> SendReportAsync(SendReportModel sendOptions)
        {
            var report = await _reportsService.CreateReportAsync(sendOptions.CreateOrUpdateOptions, false);
            var file = await _reportGeneratorService.ExportReportAsync(sendOptions.ExportType, report);

            await _notificationService.SendReportAsync(report, file, sendOptions.Tos);

            //does this go here or in the notification service?
            System.IO.File.Delete(file);

            return Ok();
        }
    }
}
