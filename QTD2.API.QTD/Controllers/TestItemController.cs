using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;
using QTD2.Infrastructure.Model.TestItem;
using QTD2.Infrastructure.Model.TestItem_History;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TestItemController : ControllerBase
    {
        private readonly ITestItemService _testItemService;
        private readonly ITestItemMatchService _testItemMatchService;
        private readonly ITestItemMCQService _testItemMCQService;
        private readonly ITestItemTrueFalseService _testItemTrueFalseService;
        private readonly ITestItemFillBlankService _testItemFillBlankService;
        private readonly ITestItemShortAnswerService _testItemShortAnswerService;
        private readonly IStringLocalizer<TestItemController> _localizer;
        private readonly ITestItem_HistoryService _testItem_historyService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IVersion_EnablingObjectiveService _versionEnablingObjectiveService;
        private readonly IEnablingObjectiveHistoryService _eo_histService;

        public TestItemController(ITestItemService testItemService,
            IStringLocalizer<TestItemController> localizer,
            ITestItemMatchService testItemMatchService,
            ITestItemMCQService testItemMCQService,
            ITestItemTrueFalseService testItemTrueFalseService,
            ITestItemFillBlankService testItemFillBlankService,
            ITestItemShortAnswerService testItemShortAnswerService,
            ITestItem_HistoryService testItem_historyService,
            IEnablingObjectiveService enablingObjectiveService,
            IVersion_EnablingObjectiveService versionEnablingObjectiveService,
            IEnablingObjectiveHistoryService eo_histService)
        {
            _testItemService = testItemService;
            _localizer = localizer;
            _testItemMatchService = testItemMatchService;
            _testItemMCQService = testItemMCQService;
            _testItemTrueFalseService = testItemTrueFalseService;
            _testItemFillBlankService = testItemFillBlankService;
            _testItemShortAnswerService = testItemShortAnswerService;
            _testItem_historyService = testItem_historyService;
            _enablingObjectiveService = enablingObjectiveService;
            _versionEnablingObjectiveService = versionEnablingObjectiveService;
            _eo_histService = eo_histService;
        }

        /// <summary>
        /// Gets a list of Test Item
        /// </summary>
        /// <returns>Http Response Code with Test Item</returns>
        [HttpGet]
        [Route("/testitem")]
        public async Task<IActionResult> GetTestItemAsync()
        {
            var result = await _testItemService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Test Item
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/testitem")]
        public async Task<IActionResult> CreateTestItemAsync(TestItemCreateOptions options)
        {
            var result = await _testItemService.CreateAsync(options);
            if (options.EOId != null && options.EOId != 0)
            {
                var eo = await _enablingObjectiveService.GetAsync(options.EOId ?? 0);
                var histOptions = new EnablingObjectiveHistoryCreateOptions();
                histOptions.EnablingObjectiveId = options.EOId ?? 0;
                histOptions.NewStatus = true;
                histOptions.OldStatus = false;
                histOptions.ChangeNotes = "New Test Item Added To EO";

                var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
                histOptions.Version_EnablingObjectiveId = version.Id;
                await _eo_histService.CreateEOHistory(histOptions);
            }

            var testhistOptions = new TestItem_HistoryCreateOptions();
            testhistOptions.OldStatus = true;
            testhistOptions.NewStatus = false;
            testhistOptions.TestItemId = result.Id;
            if (options.ChangeNotes == null)
            {
                var type = await _testItemService.GetTestItemType(result.TestItemTypeId);
                testhistOptions.ChangeNotes = "New Test Item of type " + type + " Created";
            }
            else
            {
                testhistOptions.ChangeNotes = options.ChangeNotes;
            }
            testhistOptions.EffectiveDate = options.EffectiveDate;
            await _testItem_historyService.CreateTestItemHistory(testhistOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific Test Item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Test Item</returns>
        [HttpGet]
        [Route("/testitem/{id}")]
        public async Task<IActionResult> GetTestItemAsync(int id)
        {
            var result = await _testItemService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/testitem/number")]
        public async Task<IActionResult> GetTestItemNumber()
        {
            var result = await _testItemService.GetTestItemNumberAsync();
            return Ok( new {  result });
        }

        [HttpGet]
        [Route("/testitem/filter/{option}/{id}")]
        public async Task<IActionResult> GetTestItemsWithFilter(string option, int id)
        {
            var result = await _testItemService.GetWithFilterAsync(option, id);
            return Ok( new { result });
        }
        
        [HttpGet]
        [Route("/testitem/filter/{option}")]
        public async Task<IActionResult> GetTestItemsWithFilter(string option)
        {
            var result = await _testItemService.GetWithFilterAsync(option, 0);
            return Ok( new { result });
        }

        /// <summary>
        /// Get the stats information for the test items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/testitem/stats")]
        public async Task<IActionResult> GetTestItemStatsAsync()
        {
            var result = await _testItemService.GetTestItemStats();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/testitem/{id}/individual")]
        public async Task<IActionResult> GetTestItemDataWithIndividualQuestions(int id)
        {
            var result = await _testItemService.GetItemWithDataAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific Test Item by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testitem/{id}")]
        public async Task<IActionResult> UpdateTestItemAsync(int id, TestItemCreateOptions options)
        {
            var result = await _testItemService.UpdateAsync(id, options);

            var histOptions = new TestItem_HistoryCreateOptions();
            histOptions.OldStatus = true;
            histOptions.NewStatus = false;
            histOptions.TestItemId = id;
            if (options.ChangeNotes == null)
            {
                var type = await _testItemService.GetTestItemType(result.TestItemTypeId);
                histOptions.ChangeNotes = "Test Item of type " + type + " Updated";
            }
            else
            {
                histOptions.ChangeNotes = options.ChangeNotes;
            }
            histOptions.EffectiveDate = options.EffectiveDate;
            await _testItem_historyService.CreateTestItemHistory(histOptions);
            return Ok( new { result, message = _localizer["TestItemUpdated"].Value });
        }

        /// <summary>
        /// Updates  a specific Test Item by id
        /// </summary>
        /// <param name="eos"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/testitem/eos/{eoId}")]
        public async Task<IActionResult> GetTestItemWithEOs(int eoId)
        {
            var result = await _testItemService.GetTestItemWithEOAsync(eoId);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/testitem/eos/multiple")]
        public async Task<IActionResult> GetTestItemsByEOIds(TestItemByEoOptions options)
        {
            var result = await _testItemService.GetTestItemsByEOIdsAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates description and EOID of a specific Test Item by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/testitem/description/{id}")]
        public async Task<IActionResult> UpdateTestItemDescriptionAsync(int id, TestItemUpdateOptions options)
        {
            var result = await _testItemService.UpdateDescriptionAndEoId(id, options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/testitem/{id}/changeEO")]
        public async Task<IActionResult> ChangeEOForItemAsync(int id, TestItemChangeOptions options)
        {
            await _testItemService.ChangeEOForItemAsync(id, options);
            if(options.EOId != 0)
            {
                var histOptions = new EnablingObjectiveHistoryCreateOptions();
                histOptions.EnablingObjectiveId = (int)options.EOId;
                histOptions.NewStatus = true;
                histOptions.OldStatus = false;
                histOptions.ChangeNotes = "New Test Item Added To EO";
                var eo = await _enablingObjectiveService.GetAsync((int)options.EOId);
                var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
                histOptions.Version_EnablingObjectiveId = version.Id;
                await _eo_histService.CreateEOHistory(histOptions);
            }

            var testHistory = new TestItem_HistoryCreateOptions();
            testHistory.OldStatus = false;
            testHistory.NewStatus = true;
            testHistory.TestItemId = id;
            if (options.Reason == null)
            {
                var testItem = await _testItemService.GetAsync(id);
                var type = await _testItemService.GetTestItemType(testItem.TestItemTypeId);
                testHistory.ChangeNotes = "Test Item of type " + type + " Updated";
            }
            else
            {
                testHistory.ChangeNotes = options.Reason;
            }
            testHistory.EffectiveDate = DateTime.Now;
            await _testItem_historyService.CreateTestItemHistory(testHistory);
            return Ok( new { message = _localizer["EOChangedForTestItem"] });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Test Item by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/testitem/{id}")]
        public async Task<IActionResult> DeleteTestItemStatusAsync(int id, TestItemOptions options)
        {
            var histOptions = new TestItem_HistoryCreateOptions();
            histOptions.OldStatus = true;
            histOptions.NewStatus = false;
            histOptions.EffectiveDate = options.EffectiveDate;
            var testItem = await _testItemService.GetAsync(id);
            var type = await _testItemService.GetTestItemType(testItem.TestItemTypeId);
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _testItemService.InActiveAsync(options);
                    if (options.ChangeNotes == null)
                    {
                        histOptions.ChangeNotes = "Test Item of type " + type + " Made Inactive";
                    }
                    else
                    {
                        histOptions.ChangeNotes = options.ChangeNotes;
                    }
                    break;
                case "active":
                    await _testItemService.ActiveAsync(options);
                    if (options.ChangeNotes == null)
                    {
                        histOptions.ChangeNotes = "Test Item of type " + type + " Made Active";
                    }
                    else
                    {
                        histOptions.ChangeNotes = options.ChangeNotes;
                    }
                    break;
                case "delete":
                    await _testItemService.DeleteAsync(options);
                    if (options.ChangeNotes == null)
                    {
                        histOptions.ChangeNotes = "Test Item of type " + type + " Deleted";
                    }
                    else
                    {
                        histOptions.ChangeNotes = options.ChangeNotes;
                    }
                    break;
                case "change":
                    var changeOptions = new TestItemChangeOptions();
                    changeOptions.EOId = options.EOId;
                    foreach(var change in options.TestIds)
                    {
                        await _testItemService.ChangeEOForItemAsync(change, changeOptions);
                    }
                    break;
            }
            foreach(var ids in options.TestIds)
            {
                histOptions.TestItemId = ids;
                await _testItem_historyService.CreateTestItemHistory(histOptions);
            }

            return Ok( new { message = _localizer[$"TestItem-{options.ActionType.ToLower()}"].Value });
        }

        [HttpGet]
        [Route("/testitem/{option}/list")]
        public async Task<IActionResult> GetTestItemList(string option)
        {
            var result = await _testItemService.GetTestItemList(option);
            return Ok( new { result });
        }
    }
}
