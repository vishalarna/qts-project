using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Locations;
using QTD2.Infrastructure.Model.Location_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class LocationsController
    {
        [HttpGet]
        [Route("/locations/history")]
        public async Task<IActionResult> GetAllLocHistories()
        {
            var history = await _locationHistoryService.GetHistoryAsync();
            return Ok( new { history });
        }

        [HttpGet]
        [Route("/locations/history/{id}")]
        public async Task<IActionResult> GetLocHistory(int id)
        {
            var result = await _locationHistoryService.GetLocCatHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/locations/history")]
        public async Task<IActionResult> CreateLocHistory(Location_HistoryCreateOptions options)
        {
            var result = await _locationHistoryService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/locations/history/{id}")]
        public async Task<IActionResult> UpdateLocHistory(int id, Location_HistoryCreateOptions options)
        {
            var result = await _locationHistoryService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/locations/history/{id}")]
        public async Task<IActionResult> DeleteSHHistory(int id, Location_HistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _locationHistoryService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _locationHistoryService.InActiveAsync(id);
                    break;
                case "delete":
                    await _locationHistoryService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"Location_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
