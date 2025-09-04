using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective_MetaEO_Link;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        /// <summary>
        /// Link EOs with Meta EO
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/enablingObjectives/{id}/meta")]
        public async Task<IActionResult> LinkMetaEOs(EnablingObjective_MetaEO_LinkOptions options)
        {
            await _enablingObjectiveService.LinkMetaEOsAsync(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.EnablingObjectiveId = options.MetaEOId;
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;

            histOptions.ChangeNotes = options.EOIDs.Length + " EOs Linked To Selected Meta EO";

            var eo = await _enablingObjectiveService.GetAsync(options.MetaEOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["EOsLinkedToMetaEO"] });
        }

        [HttpGet]
        [Route("/enablingObjectives/{id}/meta")]
        public async Task<IActionResult> GetLinkedEOs(int id)
        {
            var result = await _enablingObjectiveService.GetMetaEOLinksWithAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/enablingObjectives/{id}/meta/order")]
        public async Task<IActionResult> ReorderMetaEOLinks(EnablingObjective_MetaEO_LinkOptions options)
        {
            await _enablingObjectiveService.ReorderMetaEOLinksAsync(options);
            return Ok( new { message = _localizer["MetaEOLinksReordered"] });
        }

        /// <summary>
        /// Unlink Enabling Objectives from Meta EO
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/enablingObjectives/{id}/meta")]
        public async Task<IActionResult> UnlinkMetaEOs(EnablingObjective_MetaEO_LinkOptions options)
        {
            await _enablingObjectiveService.UnlinkMetaEOsAsync(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.EnablingObjectiveId = options.MetaEOId;
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            histOptions.ChangeNotes = options.ChangeNotes;
            var eo = await _enablingObjectiveService.GetAsync(options.MetaEOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { message = _localizer["EOsUnlinkedFromMetaEO"] });
        }
    }
}
