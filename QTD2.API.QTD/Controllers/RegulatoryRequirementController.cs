using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.RegulatoryRequirement;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class RegulatoryRequirementController : ControllerBase
    {
        private readonly IRR_IssuingAuthorityService _rr_IssuingAuthorityService;
        private readonly IRegulatoryRequirementService _regulatoryRequirementService;
        private readonly IRR_StatusHistoryService _rr_historyService;
        private readonly IStringLocalizer<RegulatoryRequirementController> _localizer;
        private readonly IVersioningService _versioningService;
        private readonly IVersion_TaskService _ver_taskService;
        private readonly ITask_HistoryService _task_histService;
        private readonly ITaskService _taskService;

        public RegulatoryRequirementController(IRR_IssuingAuthorityService rr_IssuingAuthorityService, IStringLocalizer<RegulatoryRequirementController> localizer, IRegulatoryRequirementService regulatoryRequirementService, IRR_StatusHistoryService rr_historyService, IVersioningService versioningService, IVersion_TaskService ver_taskService, ITask_HistoryService task_histService, ITaskService taskService)
        {
            _rr_IssuingAuthorityService = rr_IssuingAuthorityService;
            _localizer = localizer;
            _regulatoryRequirementService = regulatoryRequirementService;
            _rr_historyService = rr_historyService;
            _versioningService = versioningService;
            _ver_taskService = ver_taskService;
            _task_histService = task_histService;
            _taskService = taskService;
        }

        /// <summary>
        /// Gets a list of RegulatoryRequirements
        /// </summary>
        /// <returns>Http Response Code with RR_IssuingAuthorities</returns>
        [HttpGet]
        [Route("/rr")]
        public async Task<IActionResult> GetRegulatoryRequirmentsAsync()
        {
            var result = await _regulatoryRequirementService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new RegulatoryRequirement
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/rr")]
        public async Task<IActionResult> CreateRegulatoryRequirementAsync(RegulatoryRequirementCreateOptions options)
        {
            var result = await _regulatoryRequirementService.CreateAsync(options);
            await _versioningService.VersionRRAsync(result);
            return Ok( new { result, message = _localizer["RegulatoryRequirementCreated"].Value });
        }

        /// <summary>
        /// Make copy of Regulaltory Requirements along with linkages
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/rr/{id}/copy")]
        public async Task<IActionResult> CopyRRAsync(int id, RegulatoryRequirementCreateOptions options)
        {
            var result = await _regulatoryRequirementService.CopyRRWithLinkages(id, options);
            await _versioningService.VersionRRAsync(result);
            return Ok( new { result, message = _localizer["RegulatoryRequirementCopyCreated"] });
        }

        /// <summary>
        /// Gets a specific RegulatoryRequirement by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with RegulatoryRequirement</returns>
        [HttpGet]
        [Route("/rr/{id}")]
        public async Task<IActionResult> GetRegulatoryRequirementAsync(int id)
        {
            var result = await _regulatoryRequirementService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/rr/compact/{id}")]
        public async Task<IActionResult> GetRegulatoryRequirementCompactDataAsync(int id)
        {
            var result = await _regulatoryRequirementService.GetRegulatoryRequirementCompactDataAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific RegulatoryRequirement by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/rr/{id}")]
        public async Task<IActionResult> UpdateRegulatoryRequirementAsync(int id, RegulatoryRequirementCreateOptions options)
        {
            var rr = await _regulatoryRequirementService.GetAsync(id);
            await _versioningService.VersionRRAsync(rr);
            var result = await _regulatoryRequirementService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["RegulatoryRequirementUpdated"].Value, result });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific RegulatoryRequirement by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/rr/{id}")]
        public async Task<IActionResult> DeleteRegulatoryRequirementAsync(int id, RegulatoryRequirementOptions options)
        {
            var rr = await _regulatoryRequirementService.GetAsync(id);
            await _versioningService.VersionRRAsync(rr);
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _regulatoryRequirementService.InActiveAsync(id, options);
                    break;
                case "active":
                    await _regulatoryRequirementService.ActiveAsync(id, options);
                    break;
                case "delete":
                    await _regulatoryRequirementService.DeleteAsync(id, options);
                    break;
            }

            foreach (var rrId in options.RegulatoryRequirementIds)
            {
                var hist = await _rr_historyService.CreateAsync(new QTD2.Infrastructure.Model.RR_StatusHistory.RR_StatusHistoryCreateOptions(rrId, true, false, (DateTime)options.EffectiveDate, options.ChangeNotes));
            }

            return Ok( new { message = _localizer[$"RegulatoryRequirement-{options.ActionType.ToLower()}"].Value });
        }

        /// <summary>
        /// Get the count of regulatory requirements and issuing authorities and linkages
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/rr/stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var stats = await _regulatoryRequirementService.GetStatsCount();
            return Ok( new { stats });
        }

        /// <summary>
        /// Get all RR with Issuing authorities not linked to the task,eo,sh,ila or procedure procided in option
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/{option}/notLinked")]
        public async Task<IActionResult> GetNotLinkedTo(string option)
        {
            var result = await _regulatoryRequirementService.GetNotLinkedTo(option);
            return Ok( new { result });
        }

        //active inactive rr and categories
        [HttpGet]
        [Route("/rr/{option}/rrlist")]
        public async Task<IActionResult> GetSHActiveInactiveList(string option)
        {
            var result = await _regulatoryRequirementService.GetRRActiveInactive(option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/rr/{option}/catlist")]
        public async Task<IActionResult> GetIAActiveInactiveList(string option)
        {
            var result = await _regulatoryRequirementService.GetIAActiveInactive(option);
            return Ok( new { result });
        }
    }
}
