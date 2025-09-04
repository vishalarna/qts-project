using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TrainingIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingIssueSeverityController : ControllerBase
    {
        private readonly ITrainingIssueSeverityService _trainingIssueSeverityService;
        private readonly IStringLocalizer<TrainingIssueSeverityController> _localizer;

        public TrainingIssueSeverityController(ITrainingIssueSeverityService trainingIssueSeverityService,
            IStringLocalizer<TrainingIssueSeverityController> localizer)
        {
            _trainingIssueSeverityService = trainingIssueSeverityService;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("/trainingIssueSeverities")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _trainingIssueSeverityService.GetAllAsync();
            return Ok(new { result });
        }
    }
}
