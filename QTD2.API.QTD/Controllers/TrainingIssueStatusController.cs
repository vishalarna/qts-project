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
    public class TrainingIssueStatusController : ControllerBase
    {
        private readonly ITrainingIssueStatusService _trainingIssueStatusService;
        private readonly IStringLocalizer<TrainingIssueStatusController> _localizer;

        public TrainingIssueStatusController(ITrainingIssueStatusService trainingIssueStatusService,
            IStringLocalizer<TrainingIssueStatusController> localizer)
        {
            _trainingIssueStatusService = trainingIssueStatusService;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("/trainingIssueStatuses")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _trainingIssueStatusService.GetAllAsync();
            return Ok(new { result });
        }
    }
}
