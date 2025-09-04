using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.TestItemMatch;

namespace QTD2.API.QTD.Controllers
{
    public partial class TestItemController : ControllerBase
    {
        /// <summary>
        /// Gets a list of TestItemMatchs
        /// </summary>
        /// <returns>Http Response Code with TestItemMatchs</returns>
        [HttpGet]
        [Route("/testitem/match")]
        public async Task<IActionResult> GetTestItemMatchsAsync()
        {
            var result = await _testItemMatchService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new TestItemMatch
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/testitem/match")]
        public async Task<IActionResult> CreateTestItemMatchAsync(TestItemMatchCreateOptions options)
        {
            var result = await _testItemMatchService.CreateAsync(options);
            return Ok( new { message = _localizer["TestItemMatchCreated"].Value });
        }

        /// <summary>
        /// Gets a specific TestItemMatch by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TestItemMatch</returns>
        [HttpGet]
        [Route("/testitem/match/{id}")]
        public async Task<IActionResult> GetTestItemMatchAsync(int id)
        {
            var result = await _testItemMatchService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a all TestItemMatch matching the item id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TestItemMatch</returns>
        [HttpGet]
        [Route("/testitem/match/byItemId/{id}")]
        public IActionResult GetTestItemMatchbyItemIdAsync(int id)
        {
            var result = _testItemMatchService.GetByItemIdAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TestItemMatch by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testitem/match/{id}")]
        public async Task<IActionResult> UpdateTestItemMatchAsync(int id, TestItemMatchUpdateOptions options)
        {
            var result = await _testItemMatchService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["TestItemMatchUpdated"].Value });
        }

        /// <summary>
        /// Delete All match column test items for particular item id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testItem/match/byItemId/{id}")]
        public async Task<IActionResult> DeleteMatchColForItemId(int id)
        {
            await _testItemMatchService.DeleteWithItemId(id);
            return Ok( new { message = _localizer["TestMatchColItemDeleted"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TestItemMatch by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testitem/match/{id}")]
        public async Task<IActionResult> DeleteTestItemMatchAsync(int id, TestItemMatchOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testItemMatchService.InActiveAsync(id);
                    break;
                case "active":
                    await _testItemMatchService.ActiveAsync(id);
                    break;
                case "delete":
                    await _testItemMatchService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TestItemMatch-{options.ActionType.ToLower()}"].Value });
        }
    }
}
