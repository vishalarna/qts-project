using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.TestItemMCQ;

namespace QTD2.API.QTD.Controllers
{
    public partial class TestItemController : ControllerBase
    {
        /// <summary>
        /// Gets a list of TestItemMCQs
        /// </summary>
        /// <returns>Http Response Code with TestItemMCQs</returns>
        [HttpGet]
        [Route("/testitem/mcq")]
        public async Task<IActionResult> GetTestItemMCQsAsync()
        {
            var result = await _testItemMCQService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new TestItemMCQ
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/testitem/mcq")]
        public async Task<IActionResult> CreateTestItemMCQAsync(TestItemMCQCreateOptions options)
        {
            var result = await _testItemMCQService.CreateAsync(options);
            return Ok( new { message = _localizer["TestItemMCQCreated"].Value });
        }

        /// <summary>
        /// Gets a specific TestItemMCQ by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TestItemMCQ</returns>
        [HttpGet]
        [Route("/testitem/mcq/{id}")]
        public async Task<IActionResult> GetTestItemMCQAsync(int id)
        {
            var result = await _testItemMCQService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific  TestItemMCQ by test Item Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with  TestItemMCQ</returns>
        [HttpGet]
        [Route("/testitem/mcq/{id}/byItem")]
        public IActionResult GetTestItemMCQByTestItemIdAsync(int id)
        {
            var result = _testItemMCQService.GetByTestItemIdAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TestItemMCQ by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testitem/mcq/{id}")]
        public async Task<IActionResult> UpdateTestItemMCQAsync(int id, TestItemMCQUpdateOptions options)
        {
            var result = await _testItemMCQService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["TestItemMCQUpdated"].Value });
        }

        /// <summary>
        /// Delete All true false test items for particular item id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testItem/mcq/byItemId/{id}")]
        public async Task<IActionResult> DeleteMCQForItemId(int id)
        {
            await _testItemMCQService.DeleteWithItemId(id);
            return Ok( new { message = _localizer["TestItemMCQDeleted"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TestItemMCQ by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testitem/mcq/{id}")]
        public async Task<IActionResult> DeleteTestItemMCQAsync(int id, TestItemMCQOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testItemMCQService.InActiveAsync(id);
                    break;
                case "active":
                    await _testItemMCQService.ActiveAsync(id);
                    break;
                case "delete":
                    await _testItemMCQService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TestItemMCQ-{options.ActionType.ToLower()}"].Value });
        }
    }
}
