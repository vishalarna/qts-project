using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class DIFSurveyController:ControllerBase
    {
        
        [HttpGet]
        [Route("/difSurvey/trainingFrequency")]
        public async Task<IActionResult> GetTrainingFrequencyAsync()
        {
            var result = await _dIFSurveyTaskTrainingFrequency.GetTaskTrainingFrequencyAsync();
            return Ok(new { result });
        }
    }
}
