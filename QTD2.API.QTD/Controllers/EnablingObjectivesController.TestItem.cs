using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;
using QTD2.Infrastructure.Model.TestItem;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/test")]
        public async Task<IActionResult> GetLinkedTestItems(int eoId)
        {
            var result = await _enablingObjectiveService.GetLinkedTestItemsAsync(eoId);
            return Ok( new { result });
        }


        [HttpGet]
        [Route("/enablingObjectives/{eoId}/test/count")]
        public async Task<IActionResult> GetLinkedTestItemsWithCount(int eoId)
        {
            var result = await _enablingObjectiveService.GetLinkedTestItemsWithCountAsync(eoId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{metaId}/test/allcount")]
        public async Task<IActionResult> GetLinkedTestItemWithMetaEO(int metaId)
        {
            var result = await _enablingObjectiveService.GetLinkedTestItemWithMetaEOAsync(metaId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/test/{testItemId}/linkedtest")]
        public async Task<IActionResult> GetTestTestsItemIsLinkedTo(int testItemId)
        {
            var result = await _enablingObjectiveService.GetTestTestsItemIsLinkedToAsync(testItemId);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/enablingObjectives/{eoId}/test")]
        public async Task<IActionResult> UnlinkTestsAsync(int eoId, TestItemOptions options)
        {
            await _enablingObjectiveService.UnlinkTestsAsync(eoId,options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.NewStatus = false;
            histOptions.OldStatus = true;
            histOptions.EnablingObjectiveId = eoId;
            var eo = await _enablingObjectiveService.GetAsync(eoId);
           // var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            //histOptions.Version_EnablingObjectiveId = version.Id;
            //await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { message = _localizer["TestsUnlinkedFromEO"] });
        }
    }
}
