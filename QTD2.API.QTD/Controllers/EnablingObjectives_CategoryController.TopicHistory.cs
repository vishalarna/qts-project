using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective_TopicHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectives_CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("/enablingObjectives_topics/history")]
        public async Task<IActionResult> GetAllEOTopicHistories()
        {
            var result = await _enablingObjectiveTopicHistoryService.GetAllEOTopicHistories();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives_topics/history/{id}")]
        public async Task<IActionResult> GetEOTopicHistory(int id)
        {
            var result = await _enablingObjectiveTopicHistoryService.GetEOTopicHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/enablingObjectives_topics/history")]
        public async Task<IActionResult> CreateEOHistory(EnablingObjective_TopicHistoryCreateOptions options)
        {
            var result = await _enablingObjectiveTopicHistoryService.CreateEOTopicHistory(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/enablingObjectives_topics/history/{id}")]
        public async Task<IActionResult> UpdateEOHistory(int id, EnablingObjective_TopicHistoryCreateOptions options)
        {
            var result = await _enablingObjectiveTopicHistoryService.UpdateEOTopicHistory(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/enablingObjectives_topics/history/{id}")]
        public async Task<IActionResult> DeleteEOHistory(int id, EnablingObjective_TopicHistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _enablingObjectiveTopicHistoryService.ActiveEOTopicHistory(id);
                    break;
                case "inactive":
                    await _enablingObjectiveTopicHistoryService.InActiveEOTopicHistory(id);
                    break;
                case "delete":
                    await _enablingObjectiveTopicHistoryService.DeleteEOTopicHistory(id);
                    break;
            }

            return Ok( new { message = _localizer[$"SafetyHazard_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
