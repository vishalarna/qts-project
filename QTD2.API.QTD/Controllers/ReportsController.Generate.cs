using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.Reports;

namespace QTD2.API.QTD.Controllers
{
   
    public partial class ReportsController
    {

        [HttpPost]
        [Route("/reports/generate")]
        public async Task<IActionResult> GenerateReportAsync(ReportCreateOrUpdateOptions options)
        {
            var report = await _reportsService.CreateReportAsync(options, false);

            var locList = await _reportGeneratorService.GenerateReportAsync(report);
            return Ok( base.Content(locList, "text/html"));
        }

        [HttpPost]
        [Route("/reports/generate/{reportId}")]
        public async Task<IActionResult> GenerateReportAsync(int reportId, ReportCreateOrUpdateOptions options)
        {
            var reportData = await _reportsService.UpdateReportLastRunAsync(reportId, options);
            var report = await _reportsService.GetAsync(reportId, options);            
            var locList = await _reportGeneratorService.GenerateReportAsync(report);
            return Ok( base.Content(locList, "text/html"));

        }

    }
}
