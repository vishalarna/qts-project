using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SafetyHazard_Category;
using QTD2.Infrastructure.Model.SafetyHazard_CategoryHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController
    {
        /// <summary>
        /// Gets a list of safty hazard categories
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/saftyHazards/categories")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var sh_CatList = await _shCat_Service.GetAsync();
            return Ok( new { sh_CatList });
        }

        /// <summary>
        /// Save new Safety Hazard Category Data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/saftyHazards/categories")]
        public async Task<IActionResult> SaveCategoryAsync(SafetyHazard_CategoryCreateOptions options)
        {
            var result = await _shCat_Service.SaveCategoryAsync(options);
            var histOptions = new SaftyHazardCategoryOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.ChangeNotes = options.Notes;
            histOptions.SaftyHazardCategoryIds = new int[] { result.Id };
            await _shCatHistoryService.CreateDeleteHistAsync(histOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Update existing Safety Hazard Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/saftyHazards/categories/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, SafetyHazard_CategoryCreateOptions options)
        {
            var result = await _shCat_Service.UpdateAsync(id, options);
            var histOptions = new SaftyHazardCategoryOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.ChangeNotes = options.Notes;
            histOptions.SaftyHazardCategoryIds = new int[] { result.Id };
            await _shCatHistoryService.CreateDeleteHistAsync(histOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Get list of Safty Hazard Categories along with safety hazards in those categories;
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/categories/nest")]
        public async Task<IActionResult> GetSHCategoryWithSH()
        {
            var result = await _shCat_Service.GetSHCategoryWithSH();
            return Ok( new { result });
        }

        /// <summary>
        /// Gets safty hazard category with name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/saftyHazards/categories/{id}")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var sh_cat = await _shCat_Service.GetAsync(id);
            return Ok( new { sh_cat });
        }

        [HttpGet]
        [Route("/saftyHazards/categories/count")]
        public async Task<IActionResult> GetCountAsync()
        {
            var result = await _shCat_Service.getCount();
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/saftyHazards/categories/")]
        public async Task<IActionResult> DeleteAsync(SaftyHazardCategoryOptions options)
        {
            switch (options.ActionType.ToLower().Trim())
            {
                case "active":
                    await _shCat_Service.ActiveAsync(options);
                    break;
                case "inactive":
                    await _shCat_Service.InActiveAsync(options);
                    break;
                case "delete":
                    await _shCat_Service.DeleteAsync(options);
                    break;
            }

            await _shCatHistoryService.CreateDeleteHistAsync(options);
            return Ok( new { message = _localizer["SaftyHazardCategory - " + options.ActionType.ToLower()] });
        }
    }
}
