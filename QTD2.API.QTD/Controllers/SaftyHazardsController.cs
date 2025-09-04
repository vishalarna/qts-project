using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.SaftyHazard;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class SaftyHazardsController : ControllerBase
    {
        private readonly ISaftyHazardService _saftyHazard;
        private readonly ISafetyHazard_HistoryService _safetyHazardHistory;
        private readonly ISaftyHazard_CategoryService _shCat_Service;
        private readonly IVersioningService _versioningService;
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly IStringLocalizer<SaftyHazardsController> _localizer;
        private readonly ISafetyHazard_CategoryHistoryService _shCatHistoryService;
        private readonly IVersion_TaskService _ver_taskService;
        private readonly ITask_HistoryService _task_histService;
        private readonly ITaskService _taskService;

        public SaftyHazardsController(
            ISaftyHazardService saftyHazard,
            IVersioningService versioningService,
            IEmployeeTaskService employeeTaskService,
            ISaftyHazard_CategoryService sh_cat_Service,
            IStringLocalizer<SaftyHazardsController> localizer,
            ISafetyHazard_HistoryService safetyHazardHistory,
            ISafetyHazard_CategoryHistoryService shCatHistoryService,
            IVersion_TaskService ver_taskService,
            ITask_HistoryService task_histService,
            ITaskService taskService)
        {
            _saftyHazard = saftyHazard;
            _versioningService = versioningService;
            _employeeTaskService = employeeTaskService;
            _shCat_Service = sh_cat_Service;
            _localizer = localizer;
            _safetyHazardHistory = safetyHazardHistory;
            _shCatHistoryService = shCatHistoryService;
            _ver_taskService = ver_taskService;
            _task_histService = task_histService;
            _taskService = taskService;
        }

        /// <summary>
        /// Gets a list of safty hazards
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/saftyHazards")]
        public async Task<IActionResult> GetAsync()
        {
            var shList = await _saftyHazard.GetAsync();
            return Ok( new { shList });
        }

        /// <summary>
        /// Gets safty hazard
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/saftyHazards/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var sh = await _saftyHazard.GetAsync(id);
            return Ok( new { sh });
        }

        /// <summary>
        /// Get Safety Hazard Data Along with sets
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/{id}/withSet")]
        public async Task<IActionResult> GetWithSets(int id)
        {
            var result = await _saftyHazard.GetWithSetsAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Stats for Safety Hazards
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/stats")]
        public async Task<IActionResult> GetSHStats()
        {
            var result = await _saftyHazard.GetSHStats();
            return Ok( new { result });
        }

        /// <summary>
        /// Get All tools linked to specific safety hazard
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/tool/{id}")]
        public async Task<IActionResult> GetToolsLinkedToSh(int id)
        {
            var result = await _saftyHazard.GetToolsLinkedToShAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a safty hazard
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/saftyHazards")]
        public async Task<IActionResult> CreateAsync(SaftyHazardCreateOptions options)
        {
            var sh = await _saftyHazard.CreateAsync(options);
            var hist = new SaftyHazardOptions();
            hist.EffectiveDate = options.EffectiveDate;
            hist.ChangeNotes = options.ChangeNotes;
            hist.SaftyHazardIds = new int[] { sh.Id };
            await _safetyHazardHistory.CreateSHHistory(hist);

            await _versioningService.VersionSaftyHazardAsync(sh);
            return Ok( new { sh, message = _localizer["SHCreated"] });
        }

        /// <summary>
        /// Make copy of saftyHazards along with linkages
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/saftyHazards/{id}/copy")]
        public async Task<IActionResult> CopySafetyHazardAsync(int id, SaftyHazardCreateOptions options)
        {
            var result = await _saftyHazard.CopySafetyHazardWithLinkages(id, options);
            await _versioningService.VersionSaftyHazardAsync(result);
            return Ok( new { result, message = _localizer["SafetyHazardCopyCreated"] });
        }

        /// <summary>
        /// Updates a safty hazard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/saftyHazards/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, SaftyHazardCreateOptions options)
        {
            var vSH = await _saftyHazard.GetAsync(id);
            await _versioningService.VersionSaftyHazardAsync(vSH);
            var sh = await _saftyHazard.UpdateAsync(id, options);
            var histOptions = new SaftyHazardOptions();
            histOptions.SaftyHazardIds = new int[] { id };
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            await _safetyHazardHistory.CreateSHHistory(histOptions);
            return Ok( new { sh, message = _localizer["SHUpdated"] });
        }

        /// <summary>
        /// Update Only description for safety hazard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/saftyHazards/{id}/desc")]
        public async Task<IActionResult> UpdateDescription(int id, SaftyHazardCreateOptions options)
        {
            var vSH = await _saftyHazard.GetAsync(id);
            await _versioningService.VersionSaftyHazardAsync(vSH);
            await _saftyHazard.UpdateDescriptionAsync(id, options);
            return Ok( new { message = _localizer["SHDescriptionUpdated"] });
        }

        /// <summary>
        /// Get Safety Hazards That have a particular Safety Hazard Category ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/cat/{id}")]
        public async Task<IActionResult> GetSHWithSHCatId(int id)
        {
            var result = await _saftyHazard.GetSHWithSHCatId(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All Safety Hazards not linked to specific option
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/{option}/notLinked")]
        public async Task<IActionResult> GetSHNotLinkedTo(string option)
        {
            var result = await _saftyHazard.GetSHNotLinkedTo(option);
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes a safty hazard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/saftyHazards/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, SaftyHazardOptions options)
        {
            var vSH = await _saftyHazard.GetAsync(id);
            await _versioningService.VersionSaftyHazardAsync(vSH);
            switch (options.ActionType.Trim().ToLower())
            {
                case "inactive":
                default:
                    await _saftyHazard.DeactivateAsync(options);
                    break;
                case "active":
                    await _saftyHazard.ActivateAsync(options);
                    break;
                case "delete":
                    await _saftyHazard.DeleteAsync(options);
                    break;
            }

            await _safetyHazardHistory.CreateSHHistory(options);
            return Ok( new { message = _localizer["SaftyHazard - " + options.ActionType.Trim().ToLower()] });
        }

        //active inactive sh and categories
        [HttpGet]
        [Route("/saftyHazards/{option}/shlist")]
        public async Task<IActionResult> GetSHActiveInactiveList(string option)
        {
            var result = await _saftyHazard.GetSHActiveInactive(option);
            return Ok( new { result });
        }

        //active inactive sh and categories
        [HttpGet]
        [Route("/saftyHazards/{option}/catlist")]
        public async Task<IActionResult> GetIAActiveInactiveList(string option)
        {
            var result = await _saftyHazard.GetIAActiveInactive(option);
            return Ok( new { result });
        }
    }
}
