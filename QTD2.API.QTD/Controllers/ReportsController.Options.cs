using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ReportsController
    {
        [HttpGet]
        [Route("/reports/options/{filterName}")]
        public async Task<IActionResult> GetReportFilterOptionsAsync(string filterName)
        {
            var locList = await _reportsService.GetReportFilterOptionsAsync(filterName);
            return Ok(new { locList });
        }
    }
}
