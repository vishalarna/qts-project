using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TrainingProgramReview;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TrainingProgramReviewController : ControllerBase
    {

        private readonly IStringLocalizer<TrainingProgramsController> _localizer;
        private readonly ITrainingProgramReviewService _trainingProgramReviewService;
        private readonly ITrainingProgramReview_TrainingIssue_LinkService _trainingProgramReview_TrainingIssue_LinkService;

        public TrainingProgramReviewController(
            IStringLocalizer<TrainingProgramsController> localizer,
            ITrainingProgramReviewService trainingProgramReviewService,
            ITrainingProgramReview_TrainingIssue_LinkService trainingProgramReview_TrainingIssue_LinkService
            )
        {
            _localizer = localizer;
            _trainingProgramReviewService = trainingProgramReviewService;
            _trainingProgramReview_TrainingIssue_LinkService = trainingProgramReview_TrainingIssue_LinkService;
        }


        ///// <summary>
        ///// Create a TrainingProgramReview
        ///// </summary>
        ///// <returns></returns>
        [HttpPost]
        [Route("/trainingProgramReviews")]
        public async Task<IActionResult> CreateAsync(TrainingProgramReview_ViewModel options)
        {
            var result = await _trainingProgramReviewService.CreateAsync(options);
            return Ok(new { result });
        }


        /// <summary>
        /// Get a TrainingProgramReview by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingProgramReviews/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _trainingProgramReviewService.GetAsync(id);
            return Ok(new { result });
        }

        /// <summary>
        /// Update a TrainingProgramReview by id, and return the TrainingProgramReview
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/trainingProgramReviews/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, TrainingProgramReview_ViewModel trainingProgramReview_ViewModel)
        {
            var result = await _trainingProgramReviewService.UpdateAsync(id, trainingProgramReview_ViewModel);
            return Ok( new { result });
        }

        /// <summary>
        /// Create a copy of a TrainingProgramReview by id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/trainingProgramReviews/{id}")]
        public async Task<IActionResult> CopyAsync(int id)
        {
            var result = await _trainingProgramReviewService.CopyAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Delete a TrainingProgramReview by id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("/trainingProgramReviews/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _trainingProgramReviewService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Set Active to true for a TrainingProgramReview by id
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/trainingProgramReviews/{id}/activate")]
        public async Task<IActionResult> ActivateAsync(int id)
        {
            await _trainingProgramReviewService.ActivateAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Set Active to false for a TrainingProgramReview by id
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/trainingProgramReviews/{id}/inactivate")]
        public async Task<IActionResult> InactivateAsync(int id)
        {
            await _trainingProgramReviewService.InactivateAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Get Overview Statistics and TrainingProgramReview list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingProgramReviews/overview")]
        public async Task<IActionResult> GetOverviewAsync()
        {
            var result = await _trainingProgramReviewService.GetOverviewAsync();
            return Ok( new { result });
        }
    }
}
