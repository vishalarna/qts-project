using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Interfaces.Services.QTD;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using QTD2.Infrastructure.Model.Reports;
using Microsoft.Extensions.Logging;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ReportsController : Controller
    {
        private readonly IReportsService _reportsService;
        private readonly IReportGeneratorService _reportGeneratorService;
        private readonly IReportSkeletonService _reportSkeletonService;
        private readonly QTD2.Application.Interfaces.Services.QTD.INotificationService _notificationService;
        private readonly IStringLocalizer<ReportsController> _localizer;

        public ReportsController(
                IReportsService reportsService,
                IReportGeneratorService reportGeneratorService,
                IReportSkeletonService reportSkeletonService,
                QTD2.Application.Interfaces.Services.QTD.INotificationService notificationService,
                IStringLocalizer<ReportsController> localizer
            )
        {
            _reportsService = reportsService;
            _reportGeneratorService = reportGeneratorService;
            _localizer = localizer;
            _reportSkeletonService = reportSkeletonService;
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("/reports")]
        public async Task<IActionResult> GetAllAsync()
        {
            var locList = await _reportsService.GetAllAsync();
            return Ok( new { locList });
        }

        [HttpPost]
        [Route("/reports")]
        public async Task<IActionResult> CreateReportAsync(ReportCreateOptions options)
        {
            await _reportsService.CreateReportAsync(options);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        [Route("/reports/{reportId}")]
        public async Task<IActionResult> GetAsync(int reportId)
        {
            var locList = await _reportsService.GetAsync(reportId);
            return Ok( new { locList });
        }

        [HttpPut]
        [Route("/reports/{reportId}")]
        public async Task<IActionResult> UpdateReportAsync(int reportId, ReportUpdateOptions options)
        {
            await _reportsService.UpdateReportAsync(reportId, options);
            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
