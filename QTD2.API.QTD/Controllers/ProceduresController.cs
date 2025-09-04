using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Procedure;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ProceduresController : ControllerBase
    {
        private readonly IProcedureService _procedureService;
        private readonly IVersioningService _versioningService;
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly IStringLocalizer<ProceduresController> _stringLocalizer;
        private readonly IProcedureStatusHistoryService _procedureStatusHistoryService;
        private readonly IVersion_TaskService _ver_taskService;
        private readonly ITask_HistoryService _task_histService;
        private readonly ITaskService _taskService;

        public ProceduresController(
            IProcedureService procedureService,
            IVersioningService versioningService,
            IEmployeeTaskService employeeTaskService,
            IStringLocalizer<ProceduresController> stringLocalizer,
            IProcedureStatusHistoryService procedureStatusHistoryService,
            IVersion_TaskService ver_taskService,
            ITask_HistoryService task_histService,
            ITaskService taskService)
        {
            _procedureService = procedureService;
            _versioningService = versioningService;
            _employeeTaskService = employeeTaskService;
            _stringLocalizer = stringLocalizer;
            _procedureStatusHistoryService = procedureStatusHistoryService;
            _ver_taskService = ver_taskService;
            _task_histService = task_histService;
            _taskService = taskService;
        }

        /// <summary>
        /// Gets a list of procedures
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/procedures")]
        public async Task<IActionResult> GetAsync()
        {
            var procedures = await _procedureService.GetAsync();
            return Ok( new { procedures });
        }

        /// <summary>
        /// Get the count of procedures and issuing authorities and linkages
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/procedures/stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var stats = await _procedureService.GetStatsCount();
            return Ok( new { stats });
        }

        /// <summary>
        /// Gets a procedure
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/procedures/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var procedure = await _procedureService.GetAsync(id);
            return Ok( new { procedure });
        }

        /// <summary>
        /// Creates a procedure
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/procedures")]
        public async Task<IActionResult> CreateAsync(ProcedureCreateOptions options)
        {
            var procedure = await _procedureService.CreateAsync(options);
            await _versioningService.VersionProcedureAsync(procedure);
            return Ok( new { procedure, message = _stringLocalizer["procCreated"] });
        }

        /// <summary>
        /// Make copy of Procedure along with linkages
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/procedures/{id}/copy")]
        public async Task<IActionResult> CopyProcedureAsync(int id, ProcedureCreateOptions options)
        {
            var result = await _procedureService.CopyProcedureWithLinkages(id, options);
            await _versioningService.VersionProcedureAsync(result);
            return Ok( new { result, message = _stringLocalizer["ProcedureCopyCreated"] });
        }

        /// <summary>
        /// Updates a procedure
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/procedures/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ProcedureUpdateOptions options)
        {
            var vProc = await _procedureService.GetAsync(id);
            var procedure = await _procedureService.UpdateAsync(id, options);
            await _versioningService.VersionProcedureAsync(vProc);
            return Ok( new { procedure, message = _stringLocalizer["procUpdated"] });
        }

        /// <summary>
        /// Deletes a procedure
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/procedures/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, ProcedureOptions options)
        {
            var vProc = await _procedureService.GetOnlyProc(id);
            await _versioningService.VersionProcedureAsync(vProc);
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _procedureService.DeactivateAsync(id, options);
                    break;
                case "active":
                    await _procedureService.ActivateAsync(id, options);
                    break;
                case "delete":
                    await _procedureService.DeleteAsync(id, options);
                    break;
            }

            var hist = await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(options.procedureIds, true, false, options.ChangeNotes, (DateTime)options.ChangeEffectiveDate));
            return Ok( new { message = $"Procedure-{options.ActionType.ToLower()}" });
        }

        [HttpGet]
        [Route("/procedures/{notlinkedWith}/notlinked")]
        public async Task<IActionResult> GetNotLinkedProcedure(string notlinkedWith)
        {
            var result = await _procedureService.GetNotLinked(notlinkedWith);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/procedures/issuingAuthority/{issuingAuthorityId}/releaseToEmp")]
        public async Task<IActionResult> IsIssuingAuthorityReleasedToEmp(int issuingAuthorityId)
        {
            var result = await _procedureService.IsIssuingAuthorityReleasedToEmp(issuingAuthorityId);
            return Ok( new { result });
        }
        [HttpGet]
        [Route("/procedures/procedure/{procedureId}/releaseToEmp")]
        public async Task<IActionResult> IsProcedureReleasedToEmp(int procedureId)
        {
            var result = await _procedureService.IsProcedureReleasedToEmp(procedureId);
            return Ok( new { result });
        }

        //active inactive procedure and IA
        [HttpGet]
        [Route("/procedures/{notlinkedWith}/listia")]
        public async Task<IActionResult> GetActiveInactiveIA(string notlinkedWith)
        {
            var result = await _procedureService.GetActiveInactiveIA(notlinkedWith);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/procedures/{notlinkedWith}/listproc")]
        public async Task<IActionResult> GetActiveInactiveProcedure(string notlinkedWith)
        {
            var result = await _procedureService.GetActiveInactiveProcedure(notlinkedWith);
            return Ok( new { result });
        }
    }
}
