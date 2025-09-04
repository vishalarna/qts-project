using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.DIFSurvey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class DIFSurveyController
    {
        [HttpPost]
        [Route("/difSurvey/employees")]
        public async Task<IActionResult> LinkEmployeesToDifSurveyAsync(DIFSurveyEmployeeLinkUnlinkOptions options)
        {
            var result = await _dIFSurveyService.LinkEmployeesToDifSurveyAsync(options);
            return Ok( new { result });
        }
        [HttpDelete]
        [Route("/difSurvey/employees")]
        public async Task<IActionResult> UnlinkEmployeesToDifSurveyAsync(DIFSurveyEmployeeLinkUnlinkOptions options)
        {
            await _dIFSurveyService.UnlinkEmployeesFromDifSurveyAsync(options);
            return Ok();
        }

    }
}
