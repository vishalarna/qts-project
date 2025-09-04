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
        /// <summary>
        /// Link ILAs to Enabling Objective
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/enablingObjectives/{id}/ila")]
        public async Task<IActionResult> LinkILAs(EO_LinkOptions options)
        {
            await _enablingObjectiveService.LinkILAAsync(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.EnablingObjectiveId = options.EOId;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            histOptions.ChangeEffectiveDate = DateTime.Now;

            histOptions.ChangeNotes = options.IlaIds.Length + " ILAs Linked To Selected EO";
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["ILAsLinkedToEO"] });
        }

        /// <summary>
        /// Get the ILAs linked to EOs along with the link count
        /// </summary>
        /// <param name="eoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/ila/count")]
        public async Task<IActionResult> GetLinkedILAsWithCount(int eoId)
        {
            var result = await _enablingObjectiveService.GetILAWithLinkCount(eoId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{metaId}/ila/allcount")]
        public async Task<IActionResult> GetLinkedILAToMetaEOWithCount(int metaId)
        {
            var result = await _enablingObjectiveService.GetLinkedILAToMetaEOWithCountAsync(metaId);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink ILAs from Enabling Objective
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/enablingObjectives/{id}/ila")]
        public async Task<IActionResult> UnlinkILAs(EO_LinkOptions options)
        {
            await _enablingObjectiveService.UnlinkILAAsync(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.OldStatus = true;
            histOptions.NewStatus = false;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.EnablingObjectiveId = options.EOId;
            
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { message = _localizer["ILAsUnlinkedFromEO"] });
        }
    }
}
