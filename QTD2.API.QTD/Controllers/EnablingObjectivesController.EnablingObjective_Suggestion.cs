using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjective_Suggestion;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        /// <summary>
        /// Create new EO Suggestion
        /// </summary>
        /// <param name="eoId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/enablingObjectives/{eoId}/suggestion")]
        public async Task<IActionResult> CreateEOSuggestionAsync(int eoId, EnablingObjective_SuggestionOptions options)
        {
            var result = await _enablingObjectiveService.CreateEOSuggestionAsync(eoId, options);

            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "New Suggestion Created For EO";
            histOptions.EnablingObjectiveId = eoId ;
            var eo = await _enablingObjectiveService.GetAsync(eoId);
            //var version = _versionEnablingObjectiveService.CreateLinkVersioning(eo, 1);
            
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);  
            histOptions.Version_EnablingObjectiveId = version.Id;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;

            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All EO Suggestions
        /// </summary>
        /// <param name="eoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/suggestion")]
        public async Task<IActionResult> GetAllSuggestionsAsync(int eoId)
        {
            var result = await _enablingObjectiveService.GetAllSuggestionsAsync(eoId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Suggestion Number
        /// </summary>
        /// <param name="eoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/suggestion/number")]
        public async Task<IActionResult> GetSuggestionNumber(int eoId)
        {
            var result = await _enablingObjectiveService.GetSuggestionNumberAsync(eoId);
            return Ok( new { result });
        }

        /// <summary>
        /// Update the Suggestion data for this task
        /// </summary>
        /// <param name="eoId"></param>
        /// <param name="suggestionId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/enablingObjectives/{eoId}/suggestion/{suggestionId}")]
        public async Task<IActionResult> UpdateSuggestionAsync(int eoId, int suggestionId, EnablingObjective_SuggestionOptions options)
        {
            await _enablingObjectiveService.UpdateSuggestionAsync(eoId, suggestionId, options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "Suggestion Updated For EO";
            histOptions.EnablingObjectiveId = eoId;
            var eo = await _enablingObjectiveService.GetAsync(eoId);
            //var version = _versionEnablingObjectiveService.CreateLinkVersioning(eo, 1);

            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;

            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["TaskSuggestionUpdated"] });
        }

        /// <summary>
        /// Update Order of suggestions
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/enablingObjectives/suggestion")]
        public async Task<IActionResult> UpdateSuggestionOrder(EnablingObjective_SuggestionNumberOptions options)
        {
            await _enablingObjectiveService.UpdateSuggestionNumbers(options);
            return Ok( new { message = _localizer["SuggestionsOrderUpdated"] });
        }

        /// <summary>
        /// Delete the Task Specific suggestion.
        /// </summary>
        /// <param name="eoId"></param>
        /// <param name="suggestionId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/enablingObjectives/{eoId}/suggestion/{suggestionId}")]
        public async Task<IActionResult> DeleteSuggestionAsync(int eoId, int suggestionId, EnablingObjectiveOptions options)
        {
            switch (options.ActionType.Trim().ToLower())
            {
                case "active":
                    break;
                case "inactive":
                    break;
                case "delete":
                    await _enablingObjectiveService.DeleteSuggestionAsync(eoId, suggestionId);
                    break;
            }
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "Suggestion Deleted From EO";
            histOptions.EnablingObjectiveId = eoId;
            var eo = await _enablingObjectiveService.GetAsync(eoId);

            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;

            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);


            return Ok( new { message = _localizer["EOSuggestionDeleted"] });
        }
    }
}