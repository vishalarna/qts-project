using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TestItemType;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestItemTypeController : Controller
    {
        private readonly ITestItemTypeService _testItemTypeService;
        private readonly IStringLocalizer<TestItemTypeController> _localizer;

        public TestItemTypeController(ITestItemTypeService testItemTypeService, IStringLocalizer<TestItemTypeController> localizer)
        {
            _testItemTypeService = testItemTypeService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Test Item Type
        /// </summary>
        /// <returns>Http Response Code with Test Item Types</returns>
        [HttpGet]
        [Route("/testitemtype")]
        public async Task<IActionResult> GetTestItemTypeAsync()
        {
            var testItemType = await _testItemTypeService.GetAsync();
            return Ok( new { testItemType });
        }

        /// <summary>
        /// Creates a new Test Item Type
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/testitemtype")]
        public async Task<IActionResult> CreateTestItemTypeAsync(TestItemTypeCreateOptions options)
        {
            var result = await _testItemTypeService.CreateAsync(options);
            return Ok( new { message = _localizer["testItemTypeCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Test Item Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Test Item Type</returns>
        [HttpGet]
        [Route("/testitemtype/{id}")]
        public async Task<IActionResult> GetTestItemTypeTypeAsync(int id)
        {
            var testItemType = await _testItemTypeService.GetAsync(id);
            return Ok( new { testItemType });
        }

        /// <summary>
        /// Updates  a specific Test Item Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testitemtype/{id}")]
        public async Task<IActionResult> UpdateTestItemTypeAsync(int id, TestItemTypeUpdateOptions options)
        {
            var testItemType = await _testItemTypeService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["testItemTypeUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Test Item Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testitemtype/{id}")]
        public async Task<IActionResult> DeleteTestItemTypeAsync(int id, TestItemTypeOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testItemTypeService.InActiveAsync(id);
                    break;
                case "active":
                    await _testItemTypeService.ActiveAsync(id);
                    break;
                case "delete":
                    await _testItemTypeService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"testItemType-{options.ActionType.ToLower()}"].Value });
        }
    }
}
