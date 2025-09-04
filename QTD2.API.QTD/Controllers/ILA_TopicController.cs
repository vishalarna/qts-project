using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA_Topic;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ILA_TopicController : ControllerBase
    {
        private readonly IILA_TopicService _ilaTopicService;
        private readonly IStringLocalizer<ILA_TopicController> _localizer;

        public ILA_TopicController(IILA_TopicService ilaTopicService, IStringLocalizer<ILA_TopicController> localizer)
        {
            _ilaTopicService = ilaTopicService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of ILA Topics
        /// </summary>
        /// <returns>Http Response Code with ILA Topics</returns>
        [HttpGet]
        [Route("/ilatopics")]
        public async Task<IActionResult> GetILA_TopicsAsync()
        {
            var ilaTopics = await _ilaTopicService.GetAsync();
            return Ok( new { ilaTopics });
        }


        [HttpPost]
        [Route("/ilatopics/withCount/filter")]
        public async Task<IActionResult> GetILA_TopicsWithCountAndFilterAsync(FilterByOptions filterOptions)
        {
            var result = await _ilaTopicService.GetILA_TopicsWithCountAndFilterAsync(filterOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new ILA Topic
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/ilatopics")]
        public async Task<IActionResult> CreateILA_TopicAsync(ILA_TopicCreateOptions options)
        {
            var result = await _ilaTopicService.CreateAsync(options);
            return Ok( new { message = _localizer["ILA_TopicCreated"].Value });
        }

        /// <summary>
        /// Gets a specific ILA Topic by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Provider</returns>
        [HttpGet]
        [Route("/ilatopics/{id}")]
        public async Task<IActionResult> GetILA_TopicAsync(int id)
        {
            var ilaTopic = await _ilaTopicService.GetAsync(id);
            return Ok( new { ilaTopic });
        }

        /// <summary>
        /// Updates  a specific ILA Topic by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/ilatopics/{id}")]
        public async Task<IActionResult> UpdateILA_TopicAsync(int id, ILA_TopicUpdateOptions options)
        {
            var ilaTopic = await _ilaTopicService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["ILA_TopicUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific ILA Topic by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/ilatopics/{id}")]
        public async Task<IActionResult> DeleteILA_TopicAsync(int id, ILA_TopicOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _ilaTopicService.InActiveAsync(id);
                    break;
                case "active":
                    await _ilaTopicService.ActiveAsync(id);
                    break;
                case "delete":
                    await _ilaTopicService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"ILA_Topic-{options.ActionType.ToLower()}"].Value });
        }
    }
}
