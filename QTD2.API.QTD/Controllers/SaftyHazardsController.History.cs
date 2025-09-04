using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SafetyHazard_History;
using QTD2.Infrastructure.Model.SaftyHazard;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController : ControllerBase
    {
        [HttpGet]
        [Route("/saftyHazards/history/latest/{getLatest}")]
        public async Task<IActionResult> GetAllSHHistories(bool getLatest)
        {
            var result = await _safetyHazardHistory.GetAllSHHistories(getLatest);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/saftyHazards/history/{id}")]
        public async Task<IActionResult> GetSHHistory(int id)
        {
            var result = await _safetyHazardHistory.GetSHHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/saftyHazards/history")]
        public async Task<IActionResult> CreateSHHistory(SafetyHazard_HistoryCreateOptions options)
        {
            var newOptions = new SaftyHazardOptions();
            newOptions.SaftyHazardIds = new int[] { options.SafetyHazardId };
            newOptions.ChangeNotes = options.ChangeNotes;
            newOptions.EffectiveDate = options.ChangeEffectiveDate;
            var result = await _safetyHazardHistory.CreateSHHistory(newOptions);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/saftyHazards/history/{id}")]
        public async Task<IActionResult> UpdateSHHistory(int id, SafetyHazard_HistoryCreateOptions options)
        {
            var result = await _safetyHazardHistory.UpdateSHHistory(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/saftyHazards/history/{id}")]
        public async Task<IActionResult> DeleteSHHistory(int id, SafetyHazard_HistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _safetyHazardHistory.ActiveSHHistory(id);
                    break;
                case "inactive":
                    await _safetyHazardHistory.InActiveSHHistory(id);
                    break;
                case "delete":
                    await _safetyHazardHistory.DeleteSHHistory(id);
                    break;
            }

            return Ok( new { message = _localizer[$"SafetyHazard_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
