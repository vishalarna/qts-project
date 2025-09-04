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
        [Route("/difSurvey/tasks")]
        public async Task<IActionResult> LinkTasksToDifSurveyAsync(DIFSurveyTaskLinkOptions options)
        {
            var result = await _dIFSurveyService.LinkTasksToDifSurveyAsync(options);
            return Ok( new { result });
        }
        [HttpDelete]
        [Route("/difSurveyTask/{id}")]
        public async Task<IActionResult> UnlinkTaskFromDifSurveyAsync(int id)
        {
            await _dIFSurveyService.UnlinkTaskFromDifSurveyAsync(id);
            return Ok();
        }

        [HttpPut]
        [Route("/difSurvey/task/{id}")]
        public async Task<IActionResult> UpdateDifTaskResultAsync(int id, DIFResult_UpdateOptions options)
        {
            var result = await _dIFSurveyService.UpdateDifTaskResultsAsync(id, options);
            return Ok( new { result });
        }
    }
}
