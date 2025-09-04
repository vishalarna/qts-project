using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ReportsSkeletonController : Controller
    {
      
        private readonly IReportSkeletonService _reportSkeletonService;

        public ReportsSkeletonController(
                IReportSkeletonService reportSkeletonService
            )
        {
            _reportSkeletonService = reportSkeletonService;
        }

        [HttpGet]
        [Route("/reportSkeletons")]
        public async Task<IActionResult> GetReportSkeletonsAsync()
        {
            var locList = await _reportSkeletonService.GetReportSkeletonsAsync();
            return Ok( new { locList });
        }
        [HttpGet]
        [Route("reportSkeletons/{reportSkeletonId}")]
        public async Task<IActionResult> GetReportSkeletonAsync(int reportSkeletonId)
        {
            var locList = await _reportSkeletonService.GetReportSkeletonAsync(reportSkeletonId);
            return Ok( new { locList });
        }

        [HttpGet]
        [Route("reportSkeletons/name/{name}")]
        public async Task<IActionResult> GetReportSkeletonByNameAsync(string name)
        {
            var locList = await _reportSkeletonService.GetReportSkeletonByNameAsync(name);
            return Ok( new { locList });
        }

    }
}
