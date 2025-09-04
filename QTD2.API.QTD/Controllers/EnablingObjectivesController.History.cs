using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        [HttpGet]
        [Route("/enablingObjectives/history")]
        public async Task<IActionResult> GetAllEOHistories()
        {
            var result = await _enablingObjectiveHistoryService.GetAllEOHistories();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/history/{id}")]
        public async Task<IActionResult> GetEOHistory(int id)
        {
            var result = await _enablingObjectiveHistoryService.GetEOHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/enablingObjectives/history")]
        public async Task<IActionResult> CreateEOHistory(EnablingObjectiveHistoryCreateOptions options)
        {
            var eo = await _enablingObjectiveService.GetAsync(options.EnablingObjectiveId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            options.Version_EnablingObjectiveId = version.Id;
            var result = await _enablingObjectiveHistoryService.CreateEOHistory(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/enablingObjectives/history/{id}")]
        public async Task<IActionResult> UpdateEOHistory(int id, EnablingObjectiveHistoryCreateOptions options)
        {
            var result = await _enablingObjectiveHistoryService.UpdateEOHistory(id, options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/history/latest/{trim}")]
        public async Task<IActionResult> GetLatestActivityAsync(bool trim)
        {
            var result = await _enablingObjectiveHistoryService.GetLatestActivity(trim);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{id}/history/latest")]
        public async Task<IActionResult> GetLastestActivityAsync(int id)
        {
            var result = await _enablingObjectiveHistoryService.GetLatestActivity(id);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/enablingObjectives/history/{id}")]
        public async Task<IActionResult> DeleteEOHistory(int id, EnablingObjectiveHistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _enablingObjectiveHistoryService.ActiveEOHistory(id);
                    break;
                case "inactive":
                    await _enablingObjectiveHistoryService.InActiveEOHistory(id);
                    break;
                case "delete":
                    await _enablingObjectiveHistoryService.DeleteEOHistory(id);
                    break;
            }

            return Ok( new { message = _localizer[$"SafetyHazard_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
