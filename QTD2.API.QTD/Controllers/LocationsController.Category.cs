using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Locations;
using QTD2.Infrastructure.Model.Location_Category;
using QTD2.Infrastructure.Model.Location_CategoryHistory;

namespace QTD2.API.QTD.Controllers
{

    public partial class LocationsController
    {/// <summary>
     /// Gets a list of location categories
     /// </summary>
     /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/locations/categories")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var loc_CatList = await _location_CategoryService.GetAsync();
            return Ok( new { loc_CatList });
        }

        /// <summary>
        /// Save new location Category Data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/locations/categories")]
        public async Task<IActionResult> SaveCategoryAsync(Location_CategoryCreateOptions options)
        {
            var result = await _location_CategoryService.CreateAsync(options);
            var histOptions = new Location_CategoryHistoryCreateOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.CategoryNotes = options.CategoryNotes;
            histOptions.LocCategoryId = result.Id;
            await _locCatHistoryService.CreateAsync(histOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Update existing Location Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/locations/categories/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Location_CategoryCreateOptions options)
        {
            var result = await _location_CategoryService.UpdateAsync(id, options);
            var histOptions = new Location_CategoryHistoryCreateOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.CategoryNotes = options.CategoryNotes;
            histOptions.LocCategoryId = result.Id;
            await _locCatHistoryService.CreateAsync(histOptions);
            return Ok( new { result });
        }


        /// <summary>
        /// Gets location category with name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/locations/categories/{id}")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var loc_cat = await _location_CategoryService.GetAsync(id);
            return Ok( new { loc_cat });
        }

        [HttpDelete]
        [Route("/locations/categories/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, Location_CategoryHistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower().Trim())
            {
                case "active":
                    await _location_CategoryService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _location_CategoryService.InActiveAsync(id);
                    break;
                case "delete":
                    await _location_CategoryService.DeleteAsync(id);
                    break;
            }
            options.LocCategoryId = id;
            await _locCatHistoryService.CreateAsync(options);
            return Ok( new { message = _localizer["LocationCategory - " + options.ActionType.ToLower()] });
        }

        [HttpGet]
        [Route("/locations/categories/count")]
        public async Task<IActionResult> GetCountAsync()
        {
            var result = await _location_CategoryService.getCount();
            return Ok( new { result });
        }

        /// <summary>
        /// Get list of Location Categories along with location in those categories;
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/locations/categories/nest")]
        public async Task<IActionResult> GetLocCategoryWithLoc()
        {
            var result = await _location_CategoryService.GetLocCategoryWithLoc();
            return Ok( new { result });
        }
    }
}