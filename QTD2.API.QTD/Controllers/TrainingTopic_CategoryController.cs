using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TrainingTopic_Category;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTopic_CategoryController : ControllerBase
    {
        private readonly ITrainingTopic_CategoryService _trainingTopic_CategoryService;
        private readonly IStringLocalizer<TrainingTopic_CategoryController> _localizer;

        public TrainingTopic_CategoryController(ITrainingTopic_CategoryService trainingTopic_CategoryService, IStringLocalizer<TrainingTopic_CategoryController> localizer)
        {
            _trainingTopic_CategoryService = trainingTopic_CategoryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of TrainingTopic_Categories
        /// </summary>
        /// <returns>Http Response Code with TrainingTopic_Categories</returns>
        [HttpGet]
        [Route("/trainingTopic_Category")]
        public async Task<IActionResult> GetTrainingTopic_CategoriesAsync()
        {
            var result = await _trainingTopic_CategoryService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new TrainingTopic_Category
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/trainingTopic_Category")]
        public async Task<IActionResult> CreateTrainingTopic_CategoryAsync(TrainingTopic_CategoryCreateOptions options)
        {
            var result = await _trainingTopic_CategoryService.CreateAsync(options);
            return Ok( new { message = _localizer["TrainingTopic_CategoryCreated"].Value });
        }

        /// <summary>
        /// Gets a specific TrainingTopic_Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TrainingTopic_Category</returns>
        [HttpGet]
        [Route("/trainingTopic_Category/{id}")]
        public async Task<IActionResult> GetTrainingTopic_CategoryAsync(int id)
        {
            var result = await _trainingTopic_CategoryService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TrainingTopic_Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/trainingTopic_Category/{id}")]
        public async Task<IActionResult> UpdateTrainingTopic_CategoryAsync(int id, TrainingTopic_CategoryUpdateOptions options)
        {
            var result = await _trainingTopic_CategoryService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["TrainingTopic_CategoryUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TrainingTopic_Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/trainingTopic_Category/{id}")]
        public async Task<IActionResult> DeleteTrainingTopic_CategoryAsync(int id, TrainingTopic_CategoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _trainingTopic_CategoryService.InActiveAsync(id);
                    break;
                case "active":
                    await _trainingTopic_CategoryService.ActiveAsync(id);
                    break;
                case "delete":
                    await _trainingTopic_CategoryService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TrainingTopic_Category-{options.ActionType.ToLower()}"].Value });
        }
    }
}
