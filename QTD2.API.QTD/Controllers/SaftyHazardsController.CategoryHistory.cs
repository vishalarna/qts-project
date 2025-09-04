using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SafetyHazard_CategoryHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController : ControllerBase
    {
        [HttpGet]
        [Route("/saftyHazards/categories/history")]
        public async Task<IActionResult> GetAllSHCatHistories()
        {
            var result = await _shCatHistoryService.GetAllSHCatHistories();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/saftyHazards/categories/history/{id}")]
        public async Task<IActionResult> GetSHCatHistory(int id)
        {
            var result = await _shCatHistoryService.GetSHCatHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/saftyHazards/categories/history")]
        public async Task<IActionResult> CreateSHCatHistory(SafetyHazard_CategoryHistoryCreateOptions options)
        {
            var result = await _shCatHistoryService.CreateSHCatHistory(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/saftyHazards/categories/history/{id}")]
        public async Task<IActionResult> UpdateSHCatHistory(int id, SafetyHazard_CategoryHistoryCreateOptions options)
        {
            var result = await _shCatHistoryService.UpdateSHCatHistory(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/saftyHazards/categories/history/{id}")]
        public async Task<IActionResult> DeleteSHCatHistory(int id, SafetyHazard_CategoryHistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _shCatHistoryService.ActiveSHCatHistory(id);
                    break;
                case "inactive":
                    await _shCatHistoryService.InActiveSHCatHistory(id);
                    break;
                case "delete":
                    await _shCatHistoryService.DeleteSHCatHistory(id);
                    break;
            }

            return Ok( new { message = _localizer[$"SafetyHazard_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
