using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.Procedure_SaftyHazard_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ProceduresController : ControllerBase
    {
        /// <summary>
        /// Links a safty hazard to a procedure
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/procedures/{id}/saftyHazards")]
        public async Task<IActionResult> LinkSaftyHazardAsync(int id, Procedure_SaftyHazard_LinkOptions options)
        {
            var saftyHazard = await _procedureService.LinkSaftyHazardAsync(id, options);

            //foreach (var item in options.SaftyHazardIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { id }, false, true,options.SaftyHazardIds.Length +  "SH Linked to Procedure", System.DateTime.Now));
            //}

            // var procedure = await _procedureService.GetAsync(id);
            // await _versioningService.VersionProcedureAsync(procedure, options.IsSignificant);
            // if (options.IsSignificant)
            // {
            //     var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //     foreach (var vTask in versionedTasks)
            //     {
            //         await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //         await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //     }
            // }
            return Ok( new { saftyHazard, message = _stringLocalizer["procSHLinked"] });
        }

        [HttpGet]
        [Route("/procedures/{id}/saftyHazards")]
        public async Task<IActionResult> GetLinkedSaftyHazardsAsync(int id)
        {
            var result = await _procedureService.GetLinkedSaftyHazardsAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all procedures that safety hazard is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/procedures/saftyHazards/{id}")]
        public async Task<IActionResult> GetSHLinkedProcedures(int id)
        {
            var result = await _procedureService.GetSHLinkedProcedures(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks safty hazards from a procedure
        /// </summary>
        /// <param name="enablingObjectiveId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/procedures/{enablingObjectiveId}/saftyHazards")]
        public async Task<IActionResult> UnlinkSaftyHazardAsync(int enablingObjectiveId, Procedure_SaftyHazard_LinkOptions options)
        {
            await _procedureService.UnlinkSaftyHazardAsync(enablingObjectiveId, options);
            //foreach (var item in options.SaftyHazardIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { enablingObjectiveId }, false, true,options.SaftyHazardIds.Length +  "SH Unlinked from Procedure ", System.DateTime.Now));
            //}

            // var procedure = await _procedureService.GetAsync(enablingObjectiveId);
            // await _versioningService.VersionProcedureAsync(procedure, options.isSignificant);
            // if (options.isSignificant)
            // {
            //     var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //     foreach (var vTask in versionedTasks)
            //     {
            //         await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //         await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //     }
            // }
            return Ok( new { message = _stringLocalizer["SHprocedureUnlinked"] });
        }
    }
}
