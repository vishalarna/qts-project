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
    public class TrainingIssueActionItemStatusController : ControllerBase
    {
        private readonly ITrainingIssueActionItemStatusService _trainingIssueActionItemStatusService;
        private readonly IStringLocalizer<TrainingIssueActionItemStatusController> _localizer;

        public TrainingIssueActionItemStatusController(ITrainingIssueActionItemStatusService trainingIssueActionItemStatusService,
            IStringLocalizer<TrainingIssueActionItemStatusController> localizer)
        {
            _trainingIssueActionItemStatusService = trainingIssueActionItemStatusService;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("/trainingIssueActionItemStatuses")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _trainingIssueActionItemStatusService.GetAllAsync();
            return Ok(new { result });
        }
    }
}
