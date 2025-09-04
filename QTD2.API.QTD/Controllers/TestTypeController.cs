using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TestType;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTypeController : Controller
    {
        private readonly ITestTypeService _testTypeService;
        private readonly IStringLocalizer<TestTypeController> _localizer;

        public TestTypeController(ITestTypeService testTypeService, IStringLocalizer<TestTypeController> localizer)
        {
            _testTypeService = testTypeService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Test Type
        /// </summary>
        /// <returns>Http Response Code with Test Types</returns>
        [HttpGet]
        [Route("/testtype")]
        public async Task<IActionResult> GetTestTypeAsync()
        {
            var testType = await _testTypeService.GetAsync();
            return Ok( new { testType });
        }

        /// <summary>
        /// Creates a new Test Type
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/testtype")]
        public async Task<IActionResult> CreateTestTypeAsync(TestTypeCreateOptions options)
        {
            var result = await _testTypeService.CreateAsync(options);
            return Ok( new { message = _localizer["testTypeCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Test Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Test Type</returns>
        [HttpGet]
        [Route("/testtype/{id}")]
        public async Task<IActionResult> GetTestTypeAsync(int id)
        {
            var testType = await _testTypeService.GetAsync(id);
            return Ok( new { testType });
        }

        /// <summary>
        /// Updates  a specific Test Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testtype/{id}")]
        public async Task<IActionResult> UpdateTestTypeAsync(int id, TestTypeUpdateOptions options)
        {
            var testType = await _testTypeService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["testTypeUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Test Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testtype/{id}")]
        public async Task<IActionResult> DeleteTestTypeAsync(int id, TestTypeOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testTypeService.InActiveAsync(id);
                    break;
                case "active":
                    await _testTypeService.ActiveAsync(id);
                    break;
                case "delete":
                    await _testTypeService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"testType-{options.ActionType.ToLower()}"].Value });
        }
    }
}
