using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingGroup_CategoryController : ControllerBase
    {
        private readonly IStringLocalizer<TraineeEvaluationTypeController> _localizer;
        private readonly ITrainingGroupService _trainigGroup_Service;

        public TrainingGroup_CategoryController(IStringLocalizer<TraineeEvaluationTypeController> localizer,
            ITrainingGroupService trainigGroup_Service)
        {
            _localizer = localizer;
            _trainigGroup_Service = trainigGroup_Service;
        }

        /// <summary>
        /// Get all training groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingGroups")]
        public async Task<IActionResult> GetAllTrainingGroupsAsync()
        {
            var result = await _trainigGroup_Service.GetAllTrainingGroupsAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get training groups in specific category
        /// </summary>
        /// <param name="catId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingGroups/cat/{catId}")]
        public async Task<IActionResult> GetTrainingGroupInCategoryAsync(int catId)
        {
            var result = await _trainigGroup_Service.GetTrainingGroupsInCategoryAsync(catId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get A training group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingGroups/{id}")]
        public async Task<IActionResult> GetTrainingGroupAsync(int id)
        {
            var result = await _trainigGroup_Service.GetTrainingGroupAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all categories along with training groups in them
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingGroups/cat")]
        public async Task<IActionResult> GetTrainingGroupsWithCategoryAsync()
        {
            var result = await _trainigGroup_Service.GetAllGroupsWithCategory();
            return Ok( new { result });
        }
    }
}
