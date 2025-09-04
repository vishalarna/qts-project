using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Instructor_Category;
using QTD2.Infrastructure.Model.Instructor_CategoryHistory;

namespace QTD2.API.QTD.Controllers
{

    public partial class InstructorsController
    {/// <summary>
     /// Gets a list of instructor categories
     /// </summary>
     /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/instructors/categories")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var ins_CatList = await _instructor_CategoryService.GetAsync();
            return Ok( new { ins_CatList });
        }

        /// <summary>
        /// Save new instructor Category Data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/instructors/categories")]
        public async Task<IActionResult> SaveCategoryAsync(Instructor_CategoryCreateOptions options)
        {
            var result = await _instructor_CategoryService.CreateAsync(options);
            var histOptions = new Instructor_CategoryHistoryCreateOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.CategoryNotes = options.CategoryNotes;
            histOptions.ICategoryId = result.Id;
            await _insCatHistoryService.CreateAsync(histOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Update existing Instructor Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/instructors/categories/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Instructor_CategoryCreateOptions options)
        {
            var result = await _instructor_CategoryService.UpdateAsync(id, options);
            var histOptions = new Instructor_CategoryHistoryCreateOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.CategoryNotes = options.CategoryNotes;
            histOptions.ICategoryId = result.Id;
            await _insCatHistoryService.CreateAsync(histOptions);
            return Ok( new { result });
        }


        /// <summary>
        /// Gets safty hazard category with name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/instructors/categories/{id}")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var sh_cat = await _instructor_CategoryService.GetAsync(id);
            return Ok( new { sh_cat });
        }

        [HttpDelete]
        [Route("/instructors/categories/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, Instructor_CategoryHistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower().Trim())
            {
                case "active":
                    await _instructor_CategoryService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _instructor_CategoryService.InActiveAsync(id);
                    break;
                case "delete":
                    await _instructor_CategoryService.DeleteAsync(id);
                    break;
            }
            options.ICategoryId = id;
            await _insCatHistoryService.CreateAsync(options);
            return Ok( new { message = _localizer["SaftyHazardCategory - " + options.ActionType.ToLower()] });
        }

        [HttpGet]
        [Route("/instructors/categories/count")]
        public async Task<IActionResult> GetCountAsync()
        {
            var result = await _instructor_CategoryService.getCount();
            return Ok( new { result });
        }

        /// <summary>
        /// Get list of Instructor Categories along with instructor in those categories;
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/instructors/categories/nest")]
        public async Task<IActionResult> GetSHCategoryWithSH()
        {
            var result = await _instructor_CategoryService.GetInsCategoryWithIns();
            return Ok( new { result });
        }
    }
}