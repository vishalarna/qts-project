using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective_CategoryHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectives_CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("/enablingObjectives_categories/history")]
        public async Task<IActionResult> GetAllEOHistories()
        {
            var result = await _enablingObjectiveCategoryHistoryService.GetAllEOCatHistories();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives_categories/history/{id}")]
        public async Task<IActionResult> GetEOHistory(int id)
        {
            var result = await _enablingObjectiveCategoryHistoryService.GetEOCatHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/enablingObjectives_categories/history")]
        public async Task<IActionResult> CreateEOHistory(EnablingObjective_CategoryHistoryCreateOptions options)
        {
            var result = await _enablingObjectiveCategoryHistoryService.CreateEOCatHistory(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/enablingObjectives_categories/history/{id}")]
        public async Task<IActionResult> UpdateEOHistory(int id, EnablingObjective_CategoryHistoryCreateOptions options)
        {
            var result = await _enablingObjectiveCategoryHistoryService.UpdateEOCatHistory(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/enablingObjectives_categories/history/{id}")]
        public async Task<IActionResult> DeleteEOHistory(int id, EnablingObjective_CategoryHistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _enablingObjectiveCategoryHistoryService.ActiveEOCatHistory(id);
                    break;
                case "inactive":
                    await _enablingObjectiveCategoryHistoryService.InActiveEOCatHistory(id);
                    break;
                case "delete":
                    await _enablingObjectiveCategoryHistoryService.DeleteEOCatHistory(id);
                    break;
            }

            return Ok( new { message = _localizer[$"SafetyHazard_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
