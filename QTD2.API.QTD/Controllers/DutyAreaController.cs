using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.DutyArea;
using QTD2.Infrastructure.Model.DutyArea_History;
using QTD2.Infrastructure.Model.Position;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class DutyAreaController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IStringLocalizer<DutyAreaController> _localization;
        private readonly IDutyArea_HistoryService _historyService;
        private readonly ISubDutyArea_HistoryService _subDutyArea_HistoryService;

        public DutyAreaController(ITaskService taskService, IStringLocalizer<DutyAreaController> localization, IDutyArea_HistoryService historyService, ISubDutyArea_HistoryService subDutyArea_HistoryService)
        {
            _taskService = taskService;
            _localization = localization;
            _historyService = historyService;
            _subDutyArea_HistoryService = subDutyArea_HistoryService;
        }

        /// <summary>
        /// Gets a list of duty areas
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/dutyAreas")]
        public async Task<IActionResult> GetAsync()
        {
            var listDA = await _taskService.GetDutyAreasAsync();
            return Ok( new { listDA });
        }

        [HttpGet]
        [Route("/dutyAreas/order/{orderBy}")]
        public async Task<IActionResult> GetDutyAreasOrderedBy(string orderBy)
        {
            var result = await _taskService.GetDutyAreasOrderedByAsync(orderBy);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of duty areas
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/dutyAreas/subDutyAreas")]
        public async Task<IActionResult> GetWithSubDutyAreaAsync()
        {
            var listDA = await _taskService.GetDutyAreasWithSubDutyAreaAsync();
            return Ok( new { listDA });
        }

        [HttpGet]
        [Route("/dutyAreas/subDutyAreas/tree")]
        public async Task<IActionResult> GetMinimizedTreeDataForTaskTreeAsync()
        {
            var result = await _taskService.GetMinimizedTreeDataForTaskTree();
            return Ok( new { result });
        }

        /// <summary>
        /// Gets specific duty area
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/dutyAreas/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var da = await _taskService.GetDutyAreaAsync(id);
            return Ok( new { da });
        }

        /// <summary>
        /// Creates a new duty area
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/dutyAreas")]
        public async Task<IActionResult> CreateAsync(DutyAreaCreateOptions options)
        {
            var da = await _taskService.CreateDutyAreaAsync(options);
            var histOptions = new DutyArea_HistoryCreateOptions();
            histOptions.ChangeEffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.ChangeNotes = options.ReasonForRevision;
            histOptions.DutyAreaId = da.Id;
            await _historyService.CreateAsync(histOptions);
            return Ok( new { da });
        }

        /// <summary>
        /// Updates a duty area
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/dutyAreas/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, DutyAreaUpdateOptions options)
        {
            var da = await _taskService.UpdateDutyAreaAsync(id, options);
            var histOptions = new DutyArea_HistoryCreateOptions();
            histOptions.ChangeEffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.ChangeNotes = options.ReasonForRevision;
            histOptions.DutyAreaId = da.Id;
            await _historyService.CreateAsync(histOptions);
            return Ok( new { da, message = _localization["DutyAreaUpdated"].Value });
        }

        /// <summary>
        /// Get Duty are number based on letter
        /// </summary>
        /// <param name="letter"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/dutyAreas/number/{letter}")]
        public async Task<IActionResult> GetDutyAreaNumberAsync(string letter)
        {
            var da = await _taskService.GetDutyAreaNumberAsync(letter);
            return Ok( new { da });
        }

        /// <summary>
        /// Check if any tasks for this Duty area have any links created
        /// </summary>
        /// <param name="daId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/dutyAreas/{daId}/links")]
        public async Task<IActionResult> DAHasTaskWithLinks(int daId)
        {
            var result = await _taskService.DAHasTaskWithLinks(daId);
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific DutyArea by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/dutyAreas/{id}")]
        public async Task<IActionResult> DeleteAssessmentToolAsync(int id, DutyAreaOptions options)
        {
            var histOptions = new DutyArea_HistoryCreateOptions();
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _taskService.DeactivateDutyAreaAsync(id);
                    histOptions.ChangeEffectiveDate = (DateTime)options.ChangeEffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.DutyAreaId = id;
                    await _historyService.CreateAsync(histOptions);
                    break;
                case "active":
                    await _taskService.ActivateDutyAreaAsync(id);
                    histOptions.ChangeEffectiveDate = (DateTime)options.ChangeEffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.DutyAreaId = id;
                    await _historyService.CreateAsync(histOptions);
                    break;
                case "delete":
                    await _taskService.DeleteDutyAreaAsync(id);
                    histOptions.ChangeEffectiveDate = (DateTime)options.ChangeEffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.DutyAreaId = id;
                    await _historyService.CreateAsync(histOptions);
                    break;
            }

            return Ok( new { message = _localization[$"DutyArea-{options.ActionType.ToLower()}"].Value });
        }

        [HttpPost]
        [Route("/dutyAreas/subDutyAreas/taskTreeByPositions")]
        public async Task<IActionResult> GetTaskTreeDataByPositionAsync(PositionIdsModel options)
        {
            var result = await _taskService.GetTaskTreeDataByPositionsAsync(options.PositionIds);
            return Ok(new { result });
        }
    }
}
