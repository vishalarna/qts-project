using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Test;
using QTD2.Infrastructure.Model.Test_History;
using QTD2.Infrastructure.Model.Test_Item_Link;
using QTD2.Infrastructure.Model.TestItem;

namespace QTD2.API.QTD.Controllers
{
    public partial class TestController : ControllerBase
    {
        /// <summary>
        /// Links the Test with specific TestItem
        /// </summary>
        /// <returns>Http Response Code with result</returns>
        [HttpPost]
        [Route("/test/{id}/testItem")]

        public async Task<IActionResult> LinkTestItemAsync(int id, Test_Item_Link_LinkOptions options)
        {
            var result = await _testService.LinkTestItem(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks  the Test with specific TestItem
        /// </summary>
        /// <returns>Http Response Code with message</returns>
        [HttpDelete]
        [Route("/test/{id}/testItem")]
        public async Task<IActionResult> UnlinkTestItemAsync(int id, Test_Item_Link_LinkOptions options)
        {
            await _testService.UnLinkTestItem(id, options);

            var histOptions = new Test_HistoryCreateOptions();
            histOptions.TestId = options.TestId;
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            if(histOptions.ChangeNotes == null)
            {
                histOptions.ChangeNotes = options.TestItemIds.Length + " Test Questions Unlinked From Test";
            }
            return Ok( new { message = _localizer["TestItemUnlinked"].Value });
        }

        [HttpDelete]
        [Route("/test/{id}/testItem/all")]
        public async Task<IActionResult> UnlinkAllTestItems(int id)
        {
            await _testService.UnlinkAllTestItemsAsync(id);
            return Ok( new { message = _localizer["AllTestItemsUnlinked"] });
        }

        /// <summary>
        /// Get the Test with specific TestItem
        /// </summary>
        /// <returns>Http Response Code with Linked TestItem</returns>
        [HttpGet]
        [Route("/test/{id}/testItem")]
        public async Task<IActionResult> GetLinkedTestItemAsync(int id)
        {
            var result = await _testService.GetLinkedTestItemAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/test/testItem/copy")]
        public async Task<IActionResult> GetTestItemsForCopyModeAsync(TestItemCopyOptions option)
        {
            var result = await _testService.GetTestItemsForCopyModeAsync(option);
            return Ok( new { result });
        }

    }
}
