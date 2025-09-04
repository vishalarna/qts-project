using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.TestItemShortAnswer;

namespace QTD2.API.QTD.Controllers
{
    public partial class TestItemController : ControllerBase
    {
        /// <summary>
        /// Gets a list of TestItemShortAnswers
        /// </summary>
        /// <returns>Http Response Code with TestItemShortAnswers</returns>
        [HttpGet]
        [Route("/testitem/shortanswer")]
        public async Task<IActionResult> GetTestItemShortAnswersAsync()
        {
            var result = await _testItemShortAnswerService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new TestItemShortAnswer
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/testitem/shortanswer")]
        public async Task<IActionResult> CreateTestItemShortAnswerAsync(TestItemShortAnswerCreateOptions options)
        {
            var result = await _testItemShortAnswerService.CreateAsync(options);
            return Ok( new { message = _localizer["TestItemShortAnswerCreated"].Value });
        }

        /// <summary>
        /// Gets a specific TestItemShortAnswer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TestItemShortAnswer</returns>
        [HttpGet]
        [Route("/testitem/shortanswer/{id}")]
        public async Task<IActionResult> GetTestItemShortAnswerAsync(int id)
        {
            var result = await _testItemShortAnswerService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific TestItemShortAnswers by item Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TestItemShortAnswer</returns>
        [HttpGet]
        [Route("/testitem/shortanswer/{id}/byItem")]
        public IActionResult GetTestItemShortAnswersbyItemIdAsync(int id)
        {
            var result = _testItemShortAnswerService.GetShortAnswersByIdAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TestItemShortAnswer by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testitem/shortanswer/{id}")]
        public async Task<IActionResult> UpdateTestItemShortAnswerAsync(int id, TestItemShortAnswerUpdateOptions options)
        {
            var result = await _testItemShortAnswerService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["TestItemShortAnswerUpdated"].Value });
        }

        /// <summary>
        /// Delete All Short Answers test items for particular item id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testItem/shortanswer/byItemId/{id}")]
        public async Task<IActionResult> DeleteShortAnswerForItemId(int id)
        {
            await _testItemShortAnswerService.DeleteWithItemId(id);
            return Ok( new { message = _localizer["TestItemShortAnswersDeleted"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TestItemShortAnswer by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testitem/shortanswer/{id}")]
        public async Task<IActionResult> DeleteTestItemShortAnswerAsync(int id, TestItemShortAnswerOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testItemShortAnswerService.InActiveAsync(id);
                    break;
                case "active":
                    await _testItemShortAnswerService.ActiveAsync(id);
                    break;
                case "delete":
                    await _testItemShortAnswerService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TestItemShortAnswer-{options.ActionType.ToLower()}"].Value });
        }
    }
}
