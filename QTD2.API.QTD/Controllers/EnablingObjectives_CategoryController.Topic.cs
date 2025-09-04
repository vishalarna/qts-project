using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective_Category;
using QTD2.Infrastructure.Model.EnablingObjective_Topic;
using QTD2.Infrastructure.Model.EnablingObjective_TopicHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectives_CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("/enablingObjectives_topics/subcategories/topics")]
        public async Task<IActionResult> GetAllTopics()
        {
            var result = await _enablingObjectiveService.GetAllTopics();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates an enabling objective topic and links it to a subcategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="subcategoryId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/enablingObjectives_topics/{categoryId}/subcategories/{subcategoryId}/topics")]
        public async Task<IActionResult> CreateTopicAsync(int categoryId, int subcategoryId, EnablingObjective_TopicOptions options)
        {
            var eo_topic = await _enablingObjectiveService.CreateTopicAsync(subcategoryId, options);
            EnablingObjective_TopicHistoryCreateOptions histOptions = new EnablingObjective_TopicHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.EnablingObjectiveTopicId = eo_topic.Id;
            histOptions.ChangeNotes = options.Reason;
            histOptions.NewStatus = true;
            histOptions.OldStatus = false;
            await _enablingObjectiveTopicHistoryService.CreateEOTopicHistory(histOptions);
            return Ok( new { eo_topic, message = _localizer["eoTopicCreated"] });
        }

        /// <summary>
        /// gets list of enabling objective topics of specific subcategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="subCategoryId"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives_topics/{categoryId}/subcategories/{subcategoryId}/topics")]
        public async Task<IActionResult> GetTopicsAsync(int categoryId, int subCategoryId)
        {
            var eoTopics = await _enablingObjectiveService.GetTopicsAsync(subCategoryId);
            return Ok( new { eoTopics });
        }
        /// <summary>
        /// gets list of enabling objective topics of specific subcategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="subCategoryId"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives_topics/{categoryId}/subcategories/{subcategoryId}/topics/simplifiedlist")]
        public async Task<IActionResult> GetSimplifiedTopicsAsync(int categoryId, int subCategoryId)
        {
            var eoTopics = await _enablingObjectiveService.GetSimplifiedTopics(subCategoryId);
            return Ok( new { eoTopics });
        }

        /// <summary>
        /// Get Category Id for the selected Topic
        /// </summary>
        /// <param name="subCatId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives_topics/{subCatId}/catId")]
        public async Task<IActionResult> GetCategoryIdForTopicAsync(int subCatId)
        {
            var result = await _enablingObjectiveService.GetCategoryIdForTopic(subCatId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives_topics/topics")]
        public async Task<IActionResult> GetTopicsAsync()
        {
            var eoTopics = await _enablingObjectiveService.GetTopicsAsync();
            return Ok( new { eoTopics });
        }

        /// <summary>
        /// Get Latest Topic Number
        /// </summary>
        /// <param name="catId"></param>
        /// <param name="subCatId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives_topics/{catId}/{subCatId}/number")]
        public async Task<IActionResult> GetTopicNumberAsync(int catId, int subCatId)
        {
            var result = await _enablingObjectiveService.GetTopicNumberAsync(catId, subCatId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Latest Topic Number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives_categories/enablingObjectives_topics/topics/{id}")]
        public async Task<IActionResult> GetTopicAsync(int id)
        {
            var result = await _enablingObjectiveService.GetTopicAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives_categories/enablingObjectives_topics/{topicId}/haslinks")]
        public async Task<IActionResult> CheckTopicForEoLinks(int topicId)
        {
            var result = await _enablingObjectiveService.CheckTopicForEOWithLinkAsync(topicId);
            return Ok( new { result });
        }

        /// <summary>
        /// Update Topic Data
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/enablingObjectives_topics/{topicId}")]
        public async Task<IActionResult> UpdateTopicAsync(int topicId, EnablingObjective_TopicOptions options)
        {
            var result = await _enablingObjectiveService.UpdateTopicAsync(topicId, options);
            EnablingObjective_TopicHistoryCreateOptions histOptions = new EnablingObjective_TopicHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.EnablingObjectiveTopicId = result.Id;
            histOptions.ChangeNotes = options.Reason;
            histOptions.NewStatus = true;
            histOptions.OldStatus = false;
            await _enablingObjectiveTopicHistoryService.CreateEOTopicHistory(histOptions);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/enablingObjectives_categories/enablingObjectives_topics/topics/{id}")]
        public async Task<IActionResult> DeleteTopicAsync(int id, EnablingObjective_CategoryDeleteOptions options)
        {
            var histOptions = new EnablingObjective_TopicHistoryCreateOptions();
            switch (options.ActionType.Trim().ToLower())
            {
                case "delete":
                    await _enablingObjectiveService.DeleteTopicAsync(id);
                    break;
                case "inactive":
                    await _enablingObjectiveService.MakeInactiveTopicAsync(id);
                    histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.EnablingObjectiveTopicId = id;
                    histOptions.OldStatus = true;
                    histOptions.NewStatus = false;
                    await _enablingObjectiveTopicHistoryService.CreateEOTopicHistory(histOptions);
                    break;
                case "active":
                    await _enablingObjectiveService.MakeActiveTopicAsync(id);
                    histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.EnablingObjectiveTopicId = id;
                    histOptions.OldStatus = true;
                    histOptions.NewStatus = false;
                    await _enablingObjectiveTopicHistoryService.CreateEOTopicHistory(histOptions);
                    break;
            }
            return Ok( new { message = _localizer["EOTopicMade-" + options.ActionType.Trim().ToUpper()] });
        }
    }
}
