using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ReportSkeletonCategoriesController : Controller
    {
        private readonly IReportSkeletonCategoriesService _reportSkeletonCategoriesService;

        public ReportSkeletonCategoriesController(
                IReportSkeletonCategoriesService reportSkeletonCategoriesService
            )
        {
            _reportSkeletonCategoriesService = reportSkeletonCategoriesService;
        }

        [HttpGet]
        [Route("/reportSkeletonCategories")]
        public async Task<IActionResult> GetActiveReportSkeletonCategoriesAsync()
        {
            var locList = await _reportSkeletonCategoriesService.GetActiveReportSkeletonCategoriesAsync();
            return Ok(new { locList });
        }

        [HttpGet]
        [Route("/reportSkeletonCategories/{reportSkeletonCategoryId}")]
        public async Task<IActionResult> GetReportSkeletonCategoryByIdAsync(int reportSkeletonCategoryId)
        {
            var locList = await _reportSkeletonCategoriesService.GetActiveReportSkeletonCategoryByIdAsync(reportSkeletonCategoryId);
            return Ok(new { locList });
        }

    }
}
