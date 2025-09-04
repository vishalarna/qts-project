using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TestStatus;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestStatusController : ControllerBase
    {
        private readonly ITestStatusService _testStatusService;
        private readonly IStringLocalizer<TestStatusController> _localizer;

        public TestStatusController(ITestStatusService testStatusService, IStringLocalizer<TestStatusController> localizer)
        {
            _testStatusService = testStatusService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of TestStatuss
        /// </summary>
        /// <returns>Http Response Code with TestStatuss</returns>
        [HttpGet]
        [Route("/teststatus")]
        public async Task<IActionResult> GetTestStatussAsync()
        {
            var result = await _testStatusService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new TestStatus
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/teststatus")]
        public async Task<IActionResult> CreateTestStatusAsync(TestStatusCreateOptions options)
        {
            var result = await _testStatusService.CreateAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific TestStatus by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TestStatus</returns>
        [HttpGet]
        [Route("/teststatus/{id}")]
        public async Task<IActionResult> GetTestStatusAsync(int id)
        {
            var result = await _testStatusService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TestStatus by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/teststatus/{id}")]
        public async Task<IActionResult> UpdateTestStatusAsync(int id, TestStatusUpdateOptions options)
        {
            var result = await _testStatusService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["TestStatusUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TestStatus by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/teststatus/{id}")]
        public async Task<IActionResult> DeleteTestStatusAsync(int id, TestStatusOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testStatusService.InActiveAsync(id);
                    break;
                case "active":
                    await _testStatusService.ActiveAsync(id);
                    break;
                case "delete":
                    await _testStatusService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TestStatus-{options.ActionType.ToLower()}"].Value });
        }
    }
}
