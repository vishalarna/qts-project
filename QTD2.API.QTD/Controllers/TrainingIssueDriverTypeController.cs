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
    public class TrainingIssueDriverTypeController : ControllerBase
    {
        private readonly ITrainingIssueDriverTypeService _trainingIssueDriverTypeService;
        private readonly IStringLocalizer<TrainingIssueDriverTypeController> _localizer;

        public TrainingIssueDriverTypeController(ITrainingIssueDriverTypeService trainingIssueDriverTypeService,
            IStringLocalizer<TrainingIssueDriverTypeController> localizer)
        {
            _trainingIssueDriverTypeService = trainingIssueDriverTypeService;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("/trainingIssueDriverTypes/withSubTypes")]
        public async Task<IActionResult> GetAllWithSubTypesAsync()
        {
            var result = await _trainingIssueDriverTypeService.GetAllWithSubTypesAsync();
            return Ok(new { result });
        }
    }
}
