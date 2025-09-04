using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TestSetting;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSettingController : Controller
    {
        private readonly ITestSettingService _testSettingService;
        private readonly IStringLocalizer<TestSettingController> _localizer;

        public TestSettingController(ITestSettingService testSettingService, IStringLocalizer<TestSettingController> localizer)
        {
            _testSettingService = testSettingService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Test Setting
        /// </summary>
        /// <returns>Http Response Code with Test Setting</returns>
        [HttpGet]
        [Route("/testsetting")]
        public async Task<IActionResult> GetTestSettingAsync()
        {
            var testSetting = await _testSettingService.GetAsync();
            return Ok( new { testSetting });
        }

        /// <summary>
        /// Creates a new Test Setting
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/testsetting")]
        public async Task<IActionResult> CreateTestSettingAsync(TestSettingCreateOptions options)
        {
            var result = await _testSettingService.CreateAync(options);
            return Ok( new { message = _localizer["testSettingCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Test Setting by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Test Setting</returns>
        [HttpGet]
        [Route("/testsetting/{id}")]
        public async Task<IActionResult> GetTestSettingAsync(int id)
        {
            var testSetting = await _testSettingService.GetAsync(id);
            return Ok( new { testSetting });
        }

        /// <summary>
        /// Updates  a specific Test Setting by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testsetting/{id}")]
        public async Task<IActionResult> UpdateTestSettingAsync(int id, TestSettingUpdateOptions options)
        {
            var testSetting = await _testSettingService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["testSettingUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Test Setting by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testsetting/{id}")]
        public async Task<IActionResult> DeleteTestSettingAsync(int id, TestSettingOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "delete":
                    await _testSettingService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"testSetting-{options.ActionType.ToLower()}"].Value });
        }
    }
}
