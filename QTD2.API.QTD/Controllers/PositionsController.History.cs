using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.PositionHistory;

namespace QTD2.API.QTD.Controllers
{
  
    public partial class PositionsController
    {
        [HttpGet]
        [Route("/positions/history/latest/{getLatest}")]
        public async Task<IActionResult> GetAllPositionHistories(bool getLatest)
        {
            var history = await _positionhistoryService.GetHistoryAsync(getLatest);
            return Ok( new { history });
        }

        [HttpGet]
        [Route("/positions/history/{id}")]
        public async Task<IActionResult> GetPosHistory(int id)
        {
            var result = await _positionhistoryService.GetPositionHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/positions/history")]
        public async Task<IActionResult> CreatePosHistory(Position_HistoryCreateOptions options)
        {
            var result = await _positionhistoryService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/positions/history/{id}")]
        public async Task<IActionResult> UpdatePositionHistory(int id, Position_HistoryCreateOptions options)
        {
            var result = await _positionhistoryService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/positions/history/{id}")]
        public async Task<IActionResult> DeletePositionHistory(int id, Position_HistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _positionhistoryService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _positionhistoryService.InActiveAsync(id);
                    break;
                case "delete":
                    await _positionhistoryService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localier[$"Position_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
