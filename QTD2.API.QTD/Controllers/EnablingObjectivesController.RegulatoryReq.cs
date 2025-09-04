using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        [HttpPost]
        [Route("/enablingObjectives/{id}/rr")]
        public async Task<IActionResult> LinkRegulatoryReq(EO_LinkOptions options)
        {
            await _enablingObjectiveService.linkRegReqAsync(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.EnablingObjectiveId = options.EOId;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = options.RRIds.Length + " Regulations Linked To EO";
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["RRLinkedToSH"] });
        }

        [HttpGet]
        [Route("/enablingObjectives/{eoId}/rr")]
        public async Task<IActionResult> GetLinkedRRWithCount(int eoId)
        {
            var result = await _enablingObjectiveService.GetLinkedRRWithCount(eoId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{metaId}/rr/allcount")]
        public async Task<IActionResult> GetLinkedRRWithMetaEO(int metaId)
        {
            var result = await _enablingObjectiveService.GetLinkedRRWithMetaEOAsync(metaId);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/enablingObjectives/{id}/rr")]
        public async Task<IActionResult> UnlinkRR(EO_LinkOptions options)
        {
            await _enablingObjectiveService.UnlinkRRAsync(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.OldStatus = true;
            histOptions.NewStatus = false;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.EnablingObjectiveId = options.EOId;
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { message = _localizer["RRUnlinkedFromEO"] });
        }
    }
}
