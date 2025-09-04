using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.TestItemFillBlank;

namespace QTD2.API.QTD.Controllers
{
    public partial class TestItemController : ControllerBase
    {
        /// <summary>
        /// Gets a list of TestItemFillBlank
        /// </summary>
        /// <returns>Http Response Code with TestItemFillBlank</returns>
        [HttpGet]
        [Route("/testitem/fillBlank")]
        public async Task<IActionResult> GetTestItemFillBlankAsync()
        {
            var result = await _testItemFillBlankService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new TestItemFillBlank
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/testitem/fillBlank")]
        public async Task<IActionResult> CreateTestItemFillBlankAsync(TestItemFillBlankCreateOptions options)
        {
            var result = await _testItemFillBlankService.CreateAsync(options);
            return Ok( new { message = _localizer["TestItemFillBlankCreated"].Value });
        }

        /// <summary>
        /// Gets a specific TestItemFillBlank by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TestItemFillBlank</returns>
        [HttpGet]
        [Route("/testitem/fillBlank/{id}")]
        public async Task<IActionResult> GetTestItemFillBlankAsync(int id)
        {
            var result = await _testItemFillBlankService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/testitem/fillBlank/byItem/{id}")]
        public async Task<IActionResult> GetFillBlankByTestItemId(int id)
        {
            var result = await _testItemFillBlankService.GetFIBByTestItemId(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TestItemFillBlank by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testitem/fillBlank/{id}")]
        public async Task<IActionResult> UpdateTestItemFillBlankAsync(int id, TestItemFillBlankUpdateOptions options)
        {
            var result = await _testItemFillBlankService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["TestItemFillBlankUpdated"].Value });
        }

        /// <summary>
        /// Delete All fill in the blank test items for particular item id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testItem/fillBlank/byItemId/{id}")]
        public async Task<IActionResult> DeleteFillBlankForItemId(int id)
        {
            await _testItemFillBlankService.DeleteWithItemId(id);
            return Ok( new { message = _localizer["TestItemFillInTheBlankDeleted"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TestItemFillBlank by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testitem/fillBlank/{id}")]
        public async Task<IActionResult> DeleteTestItemFillBlankAsync(int id, TestItemFillBlankOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testItemFillBlankService.InActiveAsync(id);
                    break;
                case "active":
                    await _testItemFillBlankService.ActiveAsync(id);
                    break;
                case "delete":
                    await _testItemFillBlankService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TestItemFillBlank-{options.ActionType.ToLower()}"].Value });
        }
    }
}
