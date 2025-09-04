using System;
using System.Security.Policy;
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
    [Route("api/[controller]")]
    [ApiController]
    public partial class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IStringLocalizer<TestController> _localizer;
        private readonly IVersion_TestService _version_testService;
        private readonly ITest_HistoryService _test_historyService;

        public TestController(ITestService testService, IStringLocalizer<TestController> localizer, IVersion_TestService version_testService, ITest_HistoryService test_historyService)
        {
            _testService = testService;
            _localizer = localizer;
            _version_testService = version_testService;
            _test_historyService = test_historyService;
        }

        /// <summary>
        /// Gets a list of Test
        /// </summary>
        /// <returns>Http Response Code with Test</returns>
        [HttpGet]
        [Route("/test")]
        public async Task<IActionResult> GetTestAsync()
        {
            var result = await _testService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Test
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/test")]
        public async Task<IActionResult> CreateTestAsync(TestCreateOptions options)
        {
            var result = await _testService.CreateAsync(options);
            var histOptions = new Test_HistoryCreateOptions();
            histOptions.TestId = result.Id;
            histOptions.EffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "New Test Created";
            await _test_historyService.CreateTestHistoryAsync(histOptions);
            await _version_testService.VersionTestAsync(result, 1);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/test/testItem/filter")]
        public async Task<IActionResult> FilterTestItems(TestItemFilter option)
        {
            var result = await _testService.FilterTestItems(option);
            
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/test/testItem/eo/{id}")]
        public async Task<IActionResult> GetLinkedEOs(int id)
        {
            var result = await _testService.GetLinkedEOs(id);

            return Ok( new { result });
        }

        [HttpGet]
        [Route("/test/ila/{classScheduleId}/{type}")]
        public async Task<IActionResult> GetSpecificTestTypeForILA(int classScheduleId, string type)
        {
            var result = await _testService.GetSpecificTestTypeForILAAsync(classScheduleId, type);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/test/eo/{id}")]
        public async Task<IActionResult> GetEOsLinkedtoILA(int id)
        {
            var result = await _testService.GetEOsLinkedtoILA(id);

            return Ok( new { result });
        }

        [HttpGet]
        [Route("/test/unlinked/{id}")]
        public async Task<IActionResult> GetUnlinkedQuestions(int id)
        {
            var result = await _testService.GetUnlinkedQuestionsAsync(id);

            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific Test by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Test</returns>
        [HttpGet]
        [Route("/test/{id}")]
        public async Task<IActionResult> GetTestAsync(int id)
        {
            var result = await _testService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of Test linked to ILA
        /// </summary>
        /// <returns>Http Response Code with Test</returns>
        [HttpGet]
        [Route("/test/ila/{ilaId}")]
        public async Task<IActionResult> GetTestLinkedToILAAsync(int ilaId)
        {
            var result = await _testService.GetTestLinkedtoILAAsync(ilaId);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of Test linked to ILA
        /// </summary>
        /// <returns>Http Response Code with Test</returns>
        [HttpGet]
        [Route("/test/ila")]
        public async Task<IActionResult> GetILAWithTestAsync()
        {
            var result = await _testService.GetILAWithTestAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/test/ila/tree")]
        public async Task<IActionResult> GetILAWithTestMinimalTreeAsync()
        {
            var result = await _testService.GetILAWithTestMinimalTreeAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific Test by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/test/{id}")]
        public async Task<IActionResult> UpdateTestAsync(int id, TestCreateOptions options)
        {
            var result = await _testService.UpdateAsync(id, options);
            await _version_testService.VersionTestAsync(result, 2);
            var histOptions = new Test_HistoryCreateOptions();
            histOptions.TestId = result.Id;
            histOptions.EffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "Test Was Updated";
            await _test_historyService.CreateTestHistoryAsync(histOptions);
            return Ok( new { result, message = _localizer["TestUpdated"].Value });
        }

        [HttpGet]
        [Route("/test/{testId}/EOs")]
        public async Task<IActionResult> GetEOsLinkedToTestsILA(int testId)
        {
            var result = await _testService.GetEOsLinkedToTestsILAAsync(testId);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/test/sequence/{id}")]
        public async Task<IActionResult> UpdateTestItemSequenceAsync(int id, Test_Item_Link_LinkOptions options)
        {
            var result = await _testService.UpdateTestItemSequenceAsync(id, options);
            await _version_testService.VersionTestAsync(result, 2);
            var histOptions = new Test_HistoryCreateOptions();
            histOptions.TestId = result.Id;
            histOptions.EffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = "Test Sequence Was Updated";
            await _test_historyService.CreateTestHistoryAsync(histOptions);
            return Ok( new { message = _localizer["TestUpdated"].Value });
        }

        [HttpGet]
        [Route("/test/stats")]
        public async Task<IActionResult> GetTestStatsAsync()
        {
            var result = await _testService.GetTestStatsAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get all the TestItems that are linked in Test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/test/{id}/testItem/all")]
        public async Task<IActionResult> GetTestItemsForTest(int id)
        {
            var result = await _testService.GetTestItemsForTestAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/test/{id}/testItemVM/all")]
        public async Task<IActionResult> GetTestItemsForTestVM(int id)
        {
            var result = await _testService.GetTestItemsForTestVMAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/test/{id}/random")]
        public async Task<IActionResult> CheckIfRandomItems(int id)
        {
            var result = await _testService.CheckIfRandomItems(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Test by id
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/test")]
        public async Task<IActionResult> DeleteTestStatusAsync(TestOptions options)
        {
            var histOptions = new Test_HistoryCreateOptions();
            histOptions.EffectiveDate = options.EffectiveDate;
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testService.InActiveAsync(options);
                    foreach (var id in options.TestIds)
                    {
                        histOptions.TestId = id;
                        histOptions.ChangeNotes = options.ChangeNotes;
                        if (histOptions.ChangeNotes == null)
                        {
                            histOptions.ChangeNotes = "Test Was Made Inactive";
                        }
                        await _test_historyService.CreateTestHistoryAsync(histOptions);
                    }
                    break;
                case "active":
                    await _testService.ActiveAsync(options);
                    foreach (var id in options.TestIds)
                    {
                        histOptions.TestId = id;
                        histOptions.ChangeNotes = options.ChangeNotes;
                        if (histOptions.ChangeNotes == null)
                        {
                            histOptions.ChangeNotes = "Test Was Made Active";
                        }
                        await _test_historyService.CreateTestHistoryAsync(histOptions);
                    }
                    break;
                case "delete":
                    await _testService.DeleteAsync(options);
                    foreach (var id in options.TestIds)
                    {
                        histOptions.TestId = id;
                        histOptions.ChangeNotes = options.ChangeNotes;
                        if (histOptions.ChangeNotes == null)
                        {
                            histOptions.ChangeNotes = "Test Was Deleted";
                        }
                        await _test_historyService.CreateTestHistoryAsync(histOptions);
                    }
                    break;
                case "draft":
                    await _testService.MarkAsDraft(options);
                    break;
                case "publish":
                    await _testService.MarkAsPublished(options);
                    foreach (var id in options.TestIds)
                    {
                        histOptions.TestId = id;
                        histOptions.ChangeNotes = options.ChangeNotes;
                        if (histOptions.ChangeNotes == null)
                        {
                            histOptions.ChangeNotes = "Test Was Published";
                        }
                        await _test_historyService.CreateTestHistoryAsync(histOptions);
                    }
                    break;
                case "change":
                    await _testService.ChangeILA(options);
                    foreach(var id in options.TestIds)
                    {
                        histOptions.TestId = id;
                        histOptions.ChangeNotes = options.ChangeNotes;
                        if (histOptions.ChangeNotes == null)
                        {
                            histOptions.ChangeNotes = "ILA Changed For Test";
                        }
                        await _test_historyService.CreateTestHistoryAsync(histOptions);
                    }
                    break;
            }
            return Ok( new { message = _localizer[$"Test-{options.ActionType.ToLower()}"].Value });
        }


        [HttpGet]
        [Route("/test/emp/{empId}/class/{classId}/statuses")]
        public async Task<IActionResult> GetRetakeStatuses(int empId,int classId)
        {
            var result = await _testService.GetRetakeStatusesAsync(empId, classId);
            return Ok( new { result });
        }


        [HttpGet]
        [Route("/test/{option}/list")]
        public async Task<IActionResult> GetTestActiveInactiveList(string option)
        {
            var result = await _testService.GetTestActiveInactive(option);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/test/{id}/reorder")]
        public async Task<IActionResult> ReorderTestItemsAsync(int id, ReorderTestItemOptions options)
        {
            await _testService.ReorderTestItemsAsync(id, options);
            return Ok( new { message = _localizer["TestItemsReordered"] });
        }

        [HttpGet]
        [Route("/test/{testType}/testsList")]
        public async Task<IActionResult> GetAllTestsWithTypeAsync(string testType)
        {
            var result = await _testService.GetAllTestsByTypeAsync(testType);
            return Ok( new { result });
        }
    }
}
