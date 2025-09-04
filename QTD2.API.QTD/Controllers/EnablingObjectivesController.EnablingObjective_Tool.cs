using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;
using QTD2.Infrastructure.Model.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        /// <summary>
        /// Links a tool to a specific eo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/enablingObjectives/{id}/tools")]
        public async Task<IActionResult> AddToolsAsync(int id, ToolAddOptions options)
        {

            var tools = await _enablingObjectiveService.AddToolAsync(id, options);

            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "New Tool Created For EO";
            histOptions.EnablingObjectiveId = id;
            var eo = await _enablingObjectiveService.GetAsync(id);
            //var version = _versionEnablingObjectiveService.CreateLinkVersioning(eo, 1);

            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;

            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { tools, message = _localizer["TaskToolAdded"] });
        }

        /// <summary>
        /// Get all the tools for a eo
        /// </summary>
        /// <param name="eoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/tools")]
        public async Task<IActionResult> GetToolsAsync(int eoId)
        {
            var result = await _enablingObjectiveService.GetToolsAsync(eoId);
            return Ok( new { result });
        }

        /// <summary>
        /// Save the updated tools for task
        /// </summary>
        /// <param name="eoId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/enablingObjectives/{eoId}/tools")]
        public async Task<IActionResult> UpdateTools(int eoId, EnablingObjectiveOptions options)
        {
            await _enablingObjectiveService.UpdateToolsAsync(eoId, options);

            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "New Tool Created For EO";
            histOptions.EnablingObjectiveId = eoId;
            var eo = await _enablingObjectiveService.GetAsync(eoId);
            //var version = _versionEnablingObjectiveService.CreateLinkVersioning(eo, 1);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 2);
            histOptions.Version_EnablingObjectiveId = version.Id;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;

            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { _message = _localizer["TaskToolsUpdated"] });
        }

        /// <summary>
        /// Unlink Multiple Tools from eo
        /// </summary>
        /// <param name="eoId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/enablingObjectives/{eoId}/tools")]
        public async Task<IActionResult> RemoveTools(int eoId, EnablingObjectiveOptions options)
        {
            await _enablingObjectiveService.RemoveTools(eoId, options);

            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "Tool Removed For EO";
            histOptions.EnablingObjectiveId = eoId;
            var eo = await _enablingObjectiveService.GetAsync(eoId);
            //var version = _versionEnablingObjectiveService.CreateLinkVersioning(eo, 1);

            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;

            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { _message = _localizer["ToolsUnlinked"] });
        }

        /// <summary>
        /// Unlinks a tool from a specific eo
        /// </summary>
        /// <param name="eoId"></param>
        /// <param name="toolId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/enablingObjectives/{eoId}/tools/{toolId}")]
        public async Task<IActionResult> RemoveToolsAsync(int eoId, int toolId, EnablingObjectiveOptions options)
        {
            await _enablingObjectiveService.RemoveToolAsync(eoId, toolId);

            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "Tool Removed For EO";
            histOptions.EnablingObjectiveId = eoId;
            var eo = await _enablingObjectiveService.GetAsync(eoId);
            //var version = _versionEnablingObjectiveService.CreateLinkVersioning(eo, 1);

            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;

            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["ToolsRemovedFromTask"] });
        }
    }
}
