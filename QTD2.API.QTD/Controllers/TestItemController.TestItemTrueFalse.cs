using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.TestItemTrueFalse;

namespace QTD2.API.QTD.Controllers
{
    public partial class TestItemController : ControllerBase
    {
        /// <summary>
        /// Gets a list of TestItemTrueFalse
        /// </summary>
        /// <returns>Http Response Code with TestItemTrueFalse</returns>
        [HttpGet]
        [Route("/testitem/trueFalse")]
        public async Task<IActionResult> GetTestItemTrueFalseAsync()
        {
            var result = await _testItemTrueFalseService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new  TestItemTrueFalse
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/testitem/trueFalse")]
        public async Task<IActionResult> CreateTestItemTrueFalseAsync(TestItemTrueFalseCreateOptions options)
        {
            var result = await _testItemTrueFalseService.CreateAsync(options);
            return Ok( new { message = _localizer["TestItemTrueFalseCreated"].Value });
        }

        /// <summary>
        /// Gets a specific  TestItemTrueFalse by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with  TestItemTrueFalse</returns>
        [HttpGet]
        [Route("/testitem/trueFalse/{id}")]
        public async Task<IActionResult> GetTestItemTrueFalseAsync(int id)
        {
            var result = await _testItemTrueFalseService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific  TestItemTrueFalse by test Item Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with  TestItemTrueFalse</returns>
        [HttpGet]
        [Route("/testitem/trueFalse/{id}/byItem")]
        public IActionResult GetTestItemTrueFalseByTestItemIdAsync(int id)
        {
            var result = _testItemTrueFalseService.GetByTestItemIdAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TestItemTrueFalse by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testitem/trueFalse/{id}")]
        public async Task<IActionResult> UpdateTestItemMCQAsync(int id, TestItemTrueFalseUpdateOptions options)
        {
            var result = await _testItemTrueFalseService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["TestItemTrueFalseUpdated"].Value });
        }

        /// <summary>
        /// Delete All true false test items for particular item id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testItem/trueFalse/byItemId/{id}")]
        public async Task<IActionResult> DeleteTrueFalseForItemId(int id)
        {
            await _testItemTrueFalseService.DeleteWithItemId(id);
            return Ok( new { message = _localizer["TestItemTrueFalseDeleted"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TestItemTrueFalse by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testitem/trueFalse/{id}")]
        public async Task<IActionResult> DeleteTestItemMCQAsync(int id, TestItemTrueFalseOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testItemTrueFalseService.InActiveAsync(id);
                    break;
                case "active":
                    await _testItemTrueFalseService.ActiveAsync(id);
                    break;
                case "delete":
                    await _testItemTrueFalseService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TestItemTrueFalse-{options.ActionType.ToLower()}"].Value });
        }
    }
}
