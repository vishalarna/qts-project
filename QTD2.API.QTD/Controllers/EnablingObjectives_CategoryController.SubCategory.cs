using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective_Category;
using QTD2.Infrastructure.Model.EnablingObjective_CategoryHistory;
using QTD2.Infrastructure.Model.EnablingObjective_SubCategory;
using QTD2.Infrastructure.Model.EnablingObjective_SubCategoryHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectives_CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("/enablingObjectives_categories/subcategories/all")]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var result = await _enablingObjectiveService.GetAllSubCategories();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a sub category and links it to a spceific category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/enablingObjectives_categories/{id}/subcategories")]
        public async Task<IActionResult> CreateSubCategoryAsync(int id, EnablingObjective_SubCategoryOptions options)
        {
            var eo_subCat = await _enablingObjectiveService.CreateSubCategoryAsync(options);
            return Ok( new { eo_subCat, message = _localizer["eoSubCatCreated"] });
        }

        /// <summary>
        /// Gets all the subcategories for a specific category
        /// </summary>
        /// <param name="id">The category id</param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives_categories/{id}/subcategories")]
        public async Task<IActionResult> GetSubCategoryAsync(int id)
        {
            var eo_subCats = await _enablingObjectiveService.GetSubCategoriesAsync(id);
            return Ok( new { eo_subCats });
        }

        [HttpGet]
        [Route("/enablingObjectives_categories/{id}/subcategories/simplifiedlist")]
        public async Task<IActionResult> GetSubCategorySimplifiedListAsync(int id)
        {
            var eo_subCats = await _enablingObjectiveService.GetSimplifiedSubCategories(id);
            return Ok( new { eo_subCats });
        }

        /// <summary>
        /// Get SubCategory Data
        /// </summary>
        /// <param name="subCatId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives_categories/subcategories/{subCatId}")]
        public async Task<IActionResult> GetSubCategory(int subCatId)
        {
            var result = await _enablingObjectiveService.GetSubCategoryAsync(subCatId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Latest Sub Category Number
        /// </summary>
        /// <param name="catId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives_categories/{catId}/subcategories/number")]
        public async Task<IActionResult> GetSubCategoryNumber(int catId)
        {
            var result = await _enablingObjectiveService.GetSubCategoryNumberAsync(catId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives_categories/subcategories/{subCatId}/haslinks")]
        public async Task<IActionResult> CheckSubCatForEOLinks(int subCatId)
        {
            var result = await _enablingObjectiveService.CheckSubCatForEOWithLinkAsync(subCatId);
            return Ok( new { result });
        }

        /// <summary>
        /// Update Sub Category Data
        /// </summary>
        /// <param name="subCatId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/enablingObjectives_categories/subcategories/{subCatId}")]
        public async Task<IActionResult> UpdateSubCategoryAsync(int subCatId, EnablingObjective_SubCategoryOptions options)
        {
            var result = await _enablingObjectiveService.UpdateSubCategoryAsync(subCatId, options);
            EnablingObjective_SubCategoryHistoryCreateOptions histOptions = new EnablingObjective_SubCategoryHistoryCreateOptions();
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.EnablingObjectiveSubCategoryId = result.Id;
            histOptions.ChangeNotes = options.Reason;
            await _enablingObjectiveSubCategoryHistoryService.CreateEOSubCatHistory(histOptions);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/enablingObjectives_categories/subcategories/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, EnablingObjective_CategoryDeleteOptions options)
        {
            var histOptions = new EnablingObjective_SubCategoryHistoryCreateOptions();
            switch (options.ActionType.Trim().ToLower())
            {
                case "delete":
                    await _enablingObjectiveService.DeleteSubCategoryAsync(id);
                    break;
                case "inactive":
                    await _enablingObjectiveService.MakeInactiveSubCategoryAsync(id);
                    histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.EnablingObjectiveSubCategoryId = id;
                    histOptions.OldStatus = true;
                    histOptions.NewStatus = false;
                    await _enablingObjectiveSubCategoryHistoryService.CreateEOSubCatHistory(histOptions);
                    break;
                case "active":
                    await _enablingObjectiveService.MakeActiveSubCategoryAsync(id);
                    histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.EnablingObjectiveSubCategoryId = id;
                    histOptions.OldStatus = true;
                    histOptions.NewStatus = false;
                    await _enablingObjectiveSubCategoryHistoryService.CreateEOSubCatHistory(histOptions);
                    break;
            }
            return Ok( new { message = _localizer["EOSubCategoryMade-" + options.ActionType.Trim().ToUpper()] });
        }
    }
}
