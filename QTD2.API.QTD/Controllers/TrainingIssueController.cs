using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.DIFSurvey;
using QTD2.Infrastructure.Model.TrainingIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingIssueController : ControllerBase
    {
        private readonly ITrainingIssueService _trainingIssueService;
        private readonly IStringLocalizer<TrainingIssueController> _localizer;

        public TrainingIssueController(ITrainingIssueService trainingIssueService,
            IStringLocalizer<TrainingIssueController> localizer)
        {
            _trainingIssueService = trainingIssueService;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("/trainingIssues/overview")]
        public async Task<IActionResult> GetOverviewAsync()
        {
            var result = await _trainingIssueService.GetOverviewAsync();
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/trainingIssues/overview/withPendingActionItems")]
        public async Task<IActionResult> GetWithPendingActionItemsAsync()
        {
            var result = await _trainingIssueService.GetWithPendingActionItemsAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/trainingIssues/overview/withNoActionItems")]
        public async Task<IActionResult> GetWithNoActionItemsAsync()
        {
            var result = await _trainingIssueService.GetWithNoActionItemsAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/trainingIssues/dataElementsWithCategories")]
        public IActionResult GetAllDataElementsWithCategories()
        {
            var result = _trainingIssueService.GetAllDataElementsWithCategories();
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/trainingIssues")]
        public async Task<IActionResult> CreateAsync(TrainingIssue_VM options)
        {
            var result = await _trainingIssueService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/trainingIssues/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _trainingIssueService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/trainingIssues/{id}")]
        public async Task<IActionResult> CopyAsync(int id)
        {
            var result = await _trainingIssueService.CopyAsync(id);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/trainingIssues/{id}/activate")]
        public async Task<IActionResult> ActivateAsync(int id)
        {
            await _trainingIssueService.ActivateAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut]
        [Route("/trainingIssues/{id}/inactivate")]
        public async Task<IActionResult> InactivateAsync(int id)
        {
            await _trainingIssueService.InactivateAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete]
        [Route("/trainingIssues/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _trainingIssueService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut]
        [Route("/trainingIssues/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, TrainingIssue_VM options)
        {
            var result = await _trainingIssueService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/trainingIssues/{id}/actionItems")]
        public async Task<IActionResult> UpdateActionItemsAsync(int id, TrainingIssue_ActionItems_VM options, bool isStatusCheck)
        {
            var result = await _trainingIssueService.UpdateActionItemsAsync(id, options, isStatusCheck);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/trainingIssues/{id}/dataElement")]
        public async Task<IActionResult> UpdateDataElementAsync(int id, TrainingIssue_DataElement_VM option)
        {
            var result =  await _trainingIssueService.UpdateDataElementAsync(id, option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/trainingIssues/dataElement/{id}/Type/{type}")]
        public async Task<IActionResult> GetTrainingIssueByDataElementTypeAsync(int id,string type)
        {
            var result = await _trainingIssueService.GetTrainingIssueByDataElementTypeAndTypeIdAsync(id,type);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/trainingIssues/taskReview/{taskReviewId}")]
        public async Task<IActionResult> GetTrainingIssueAsync(int taskReviewId)
        {
            var result = await _trainingIssueService.GetTrainingIssueByTaskReviewIdAsync(taskReviewId);
            return Ok(new { result });
        }
    }
}
