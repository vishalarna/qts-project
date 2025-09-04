using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.EnablingObjective_Category;
using QTD2.Infrastructure.Model.EnablingObjective_CategoryHistory;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EnablingObjectives_CategoryController : ControllerBase
    {
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IEnablingObjective_CategoryHistoryService _enablingObjectiveCategoryHistoryService;
        private readonly IEnablingObjective_SubCategoryHistoryService _enablingObjectiveSubCategoryHistoryService;
        private readonly IEnablingObjective_TopicHistoryService _enablingObjectiveTopicHistoryService;
        private readonly IStringLocalizer<EnablingObjectives_CategoryController> _localizer;

        public EnablingObjectives_CategoryController(IEnablingObjectiveService enablingObjectiveService, IStringLocalizer<EnablingObjectives_CategoryController> localizer, IEnablingObjective_CategoryHistoryService enablingObjectiveCategoryHistoryService, IEnablingObjective_TopicHistoryService enablingObjectiveTopicHistoryService, IEnablingObjective_SubCategoryHistoryService enablingObjectiveSubCategoryHistoryService)
        {
            _enablingObjectiveService = enablingObjectiveService;
            _localizer = localizer;
            _enablingObjectiveCategoryHistoryService = enablingObjectiveCategoryHistoryService;
            _enablingObjectiveTopicHistoryService = enablingObjectiveTopicHistoryService;
            _enablingObjectiveSubCategoryHistoryService = enablingObjectiveSubCategoryHistoryService;
        }

        /// <summary>
        /// Gets a list of enabling objective categories
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives_categories")]
        public async Task<IActionResult> GetAsync()
        {
            var eo_cats = await _enablingObjectiveService.GetCategoriesAsync();
            return Ok( new { eo_cats });
        }

        [HttpGet]
        [Route("/enablingObjectives_categories/simplifiedlist")]
        public async Task<IActionResult> GetSimplifiedListAsync()
        {
            var eo_cats = await _enablingObjectiveService.GetSimplifiedCategories("num");
            return Ok( new { eo_cats });
        }

        /// <summary>
        /// Gets a specific enabling objective category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives_categories/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var eo_cat = await _enablingObjectiveService.GetCategoryAsync(id);
            return Ok( new { eo_cat });
        }

        [HttpGet]
        [Route("/enablingObjectives_categories/{id}/subCat/number")]
        public async Task<IActionResult> GetSubCatWithNumber(int id)
        {
            var result = await _enablingObjectiveService.GetSubCatWithNumberAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives_categories/subCat/{id}/topic/number")]
        public async Task<IActionResult> GetTopicWithNumber(int id)
        {
            var result = await _enablingObjectiveService.GetTopicNumberAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a enabling objective category along with its subcategories
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives_categories/subCategories")]
        public async Task<IActionResult> GetWithSubCategoriesAsync()
        {
            var eo_cat = await _enablingObjectiveService.GetCategoryWithSubCategoryAsync();
            return Ok( new { eo_cat });
        }

        [HttpGet]
        [Route("/enablingObjectives_categories/{catId}/haslinks")]
        public async Task<IActionResult> CheckCatForEOWithLink(int catId)
        {
            var result = await _enablingObjectiveService.CheckCatForEOWithLinkAsync(catId);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates an enabling objective category
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/enablingObjectives_categories")]
        public async Task<IActionResult> CreateAsync(EnablingObjective_CategoryOptions options)
        {
            var eo_cat = await _enablingObjectiveService.CreateCategoryAsync(options);
            EnablingObjective_CategoryHistoryCreateOptions histOptions = new EnablingObjective_CategoryHistoryCreateOptions();
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.Reason;
            histOptions.EnablingObjectiveCategoryId = eo_cat.Id;
            await _enablingObjectiveCategoryHistoryService.CreateEOCatHistory(histOptions);
            return Ok( new { eo_cat, message = _localizer["eoCatCreated"] });
        }

        /// <summary>
        /// Get Latest Category Number
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives_categories/number")]
        public async Task<IActionResult> GetCategoryNumber()
        {
            var result = await _enablingObjectiveService.GetCategoryNumberAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Update CategoryData
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/enablingObjectives_categories/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, EnablingObjective_CategoryOptions options)
        {
            var result = await _enablingObjectiveService.UpdateCategoryAsync(id, options);
            EnablingObjective_CategoryHistoryCreateOptions histOptions = new EnablingObjective_CategoryHistoryCreateOptions();
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.Reason;
            histOptions.EnablingObjectiveCategoryId = result.Id;
            await _enablingObjectiveCategoryHistoryService.CreateEOCatHistory(histOptions);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/enablingObjectives_categories/{id}")]
        public async Task<IActionResult> DeleteCatAsync(int id, EnablingObjective_CategoryDeleteOptions options)
        {
            var histOptions = new EnablingObjective_CategoryHistoryCreateOptions();
            switch (options.ActionType.Trim().ToLower())
            {
                case "delete":
                    await _enablingObjectiveService.DeleteCategoryAsync(id);
                    break;
                case "inactive":
                    await _enablingObjectiveService.MakeInactiveCategoryAsync(id);
                    histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.EnablingObjectiveCategoryId = id;
                    histOptions.OldStatus = true;
                    histOptions.NewStatus = false;
                    await _enablingObjectiveCategoryHistoryService.CreateEOCatHistory(histOptions);
                    break;
                case "active":
                    await _enablingObjectiveService.MakeActiveCategoryAsync(id);
                    histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.EnablingObjectiveCategoryId = id;
                    histOptions.OldStatus = true;
                    histOptions.NewStatus = false;
                    await _enablingObjectiveCategoryHistoryService.CreateEOCatHistory(histOptions);
                    break;
            }
            return Ok( new { message = _localizer["EOCategoryMade-" + options.ActionType.Trim().ToUpper()] });
        }
    }
}
