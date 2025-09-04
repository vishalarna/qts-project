using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective_SubCategoryHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectives_CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("/enablingObjectives_categories/subcategory/history")]
        public async Task<IActionResult> GetAllEOSubCatHistories()
        {
            var result = await _enablingObjectiveSubCategoryHistoryService.GetAllEOSubCatHistories();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives_categories/subcategory/history/{id}")]
        public async Task<IActionResult> GetEOSubCatHistory(int id)
        {
            var result = await _enablingObjectiveSubCategoryHistoryService.GetEOSubCatHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/enablingObjectives_categories/subcategory/history")]
        public async Task<IActionResult> CreateEOSubCatHistory(EnablingObjective_SubCategoryHistoryCreateOptions options)
        {
            var result = await _enablingObjectiveSubCategoryHistoryService.CreateEOSubCatHistory(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/enablingObjectives_categories/subcategory/history/{id}")]
        public async Task<IActionResult> UpdateEOSubCatHistory(int id, EnablingObjective_SubCategoryHistoryCreateOptions options)
        {
            var result = await _enablingObjectiveSubCategoryHistoryService.UpdateEOSubCatHistory(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/enablingObjectives_categories/subcategory/history/{id}")]
        public async Task<IActionResult> DeleteEOSubCatHistory(int id, EnablingObjective_SubCategoryHistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _enablingObjectiveSubCategoryHistoryService.ActiveEOSubCatHistory(id);
                    break;
                case "inactive":
                    await _enablingObjectiveSubCategoryHistoryService.InActiveEOSubCatHistory(id);
                    break;
                case "delete":
                    await _enablingObjectiveSubCategoryHistoryService.DeleteEOSubCatHistory(id);
                    break;
            }

            return Ok( new { message = _localizer[$"SafetyHazard_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
