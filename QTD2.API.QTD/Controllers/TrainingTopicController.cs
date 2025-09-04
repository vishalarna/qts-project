using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TrainingTopic;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTopicController : ControllerBase
    {
        private readonly ITrainingTopicService _trainingTopicService;
        private readonly IStringLocalizer<TrainingTopicController> _localizer;

        public TrainingTopicController(ITrainingTopicService trainingTopicService, IStringLocalizer<TrainingTopicController> localizer)
        {
            _trainingTopicService = trainingTopicService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of TrainingTopics
        /// </summary>
        /// <returns>Http Response Code with TrainingTopics</returns>
        [HttpGet]
        [Route("/trainingTopics")]
        public async Task<IActionResult> GetTrainingTopicsAsync()
        {
            var result = await _trainingTopicService.GetAsync();
            return Ok( new { result });
        }
        [HttpGet]
        [Route("/trainingTopicsCategories")]
        public async Task<IActionResult> GetTrainingTopicsCategoriesAsync()
        {
            var result = await _trainingTopicService.GetCategoriesAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new TrainingTopic
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/trainingTopics")]
        public async Task<IActionResult> CreateTrainingTopicAsync(TrainingTopicCreateOptions options)
        {
            var result = await _trainingTopicService.CreateAsync(options);
            return Ok( new { message = _localizer["TrainingTopicCreated"].Value });
        }

        /// <summary>
        /// Gets a specific TrainingTopic by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TrainingTopic</returns>
        [HttpGet]
        [Route("/trainingTopics/{id}")]
        public async Task<IActionResult> GetTrainingTopicAsync(int id)
        {
            var result = await _trainingTopicService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TrainingTopic by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/trainingTopics/{id}")]
        public async Task<IActionResult> UpdateTrainingTopicAsync(int id, TrainingTopicUpdateOptions options)
        {
            var result = await _trainingTopicService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["TrainingTopicUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TrainingTopic by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/trainingTopics/{id}")]
        public async Task<IActionResult> DeleteTrainingTopicAsync(int id, TrainingTopicOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _trainingTopicService.InActiveAsync(id);
                    break;
                case "active":
                    await _trainingTopicService.ActiveAsync(id);
                    break;
                case "delete":
                    await _trainingTopicService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TrainingTopic-{options.ActionType.ToLower()}"].Value });
        }
    }
}
