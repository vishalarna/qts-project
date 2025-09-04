using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/pos")]
        public async Task<IActionResult> GetLinkedPositionsWithCountAsync(int eoId)
        {
            var result = await _enablingObjectiveService.GetLinkedPositions(eoId);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/enablingObjectives/{eoId}/pos")]
        public async Task<IActionResult> LinkPositionsAsync(int eoId, EO_LinkOptions options)
        {
            await _enablingObjectiveService.LinkPositions(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.NewStatus = true;
            histOptions.OldStatus = false;
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = options.PositionIds.Length + " Linked to Skill Qualification";
            histOptions.EnablingObjectiveId = options.EOId;
            var eo = await _enablingObjectiveService.GetAsync(eoId);
            //await _versionEnablingObjectiveService.CreateAsync(eo,1);
            //foreach(var posId in options.PositionIds)
            //{
            //    await _versionEnablingObjectiveService.CreateEOPositionLinkVersion(eo.Id, posId);
            //}

            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { message = _localizer["PositionsLinkedToSQ"] });
        }

        [HttpDelete]
        [Route("/enablingObjectives/{eoId}/pos")]
        public async Task<IActionResult> UnlinkPositionsAsync(int eoId, EO_LinkOptions options)
        {
            await _enablingObjectiveService.UnlinkPositions(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.NewStatus = false;
            histOptions.OldStatus = true;
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EnablingObjectiveId = options.EOId;
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { message = _localizer["PositionsUnlinkedFromSQ"] });
        }
    }
}
