using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class DIFSurveyController : ControllerBase
    {
        [HttpGet]
        [Route("/difSurvey/taskStatus")]
        public async Task<IActionResult> GetTaskStatusAsync()
        {
            var result = await _dIFSurveyTaskStatusService.GetTaskStatus();
            return Ok(new { result });
        }
    }
}
