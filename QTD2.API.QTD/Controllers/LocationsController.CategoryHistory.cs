using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Location_CategoryHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class LocationsController
    {
        [HttpGet]
        [Route("/locations/categories/history")]
        public async Task<IActionResult> GetAllLocCatHistories()
        {
            var result = await _locCatHistoryService.GetAllLocCatHistories();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/locations/categories/history/{id}")]
        public async Task<IActionResult> GetLocCatHistory(int id)
        {
            var result = await _locCatHistoryService.GetLocCatHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/locations/categories/history")]
        public async Task<IActionResult> CreateInsistory(Location_CategoryHistoryCreateOptions options)
        {
            var result = await _locCatHistoryService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/locations/categories/history/{id}")]
        public async Task<IActionResult> UpdateSHHistory(int id, Location_CategoryHistoryCreateOptions options)
        {
            var result = await _locCatHistoryService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/locations/categories/history/{id}")]
        public async Task<IActionResult> DeleteSHHistory(int id, Location_CategoryHistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _locCatHistoryService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _locCatHistoryService.InActiveAsync(id);
                    break;
                case "delete":
                    await _locCatHistoryService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"Location_CategoryHistory-{options.ActionType.ToLower()}"].Value });
        }
    }
}
