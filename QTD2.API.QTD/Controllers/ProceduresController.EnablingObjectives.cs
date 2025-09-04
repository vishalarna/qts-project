using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.Procedure_EnablingObjective_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ProceduresController : ControllerBase
    {
        /// <summary>
        /// Links an enabling objective to a procedure
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/procedures/{id}/enablingObjectives")]
        public async Task<IActionResult> LinkEnablingObjectiveAsync(int id, Procedure_EnablingObjective_LinkOptions options)
        {
            var eo = await _procedureService.LinkEnablingObjectiveAsync(id, options);

            //foreach (var item in options.EOIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { id }, false, true,options.EOIds.Length + " EO Linked to Procedure", System.DateTime.Now));
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
            return Ok( new { eo, message = _stringLocalizer["procEOLinked"] });
        }

        /// <summary>
        /// Unlinks an enabling objective to a procedure
        /// </summary>
        /// <param name="procedureId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/procedures/{procedureId}/enablingObjectives")]
        public async Task<IActionResult> UnlinkEnablingObjectiveAsync(int procedureId, Procedure_EnablingObjective_LinkOptions options)
        {
            await _procedureService.UnlinkEnablingObjectiveAsync(procedureId, options.EOIds);
            //foreach (var item in options.EOIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { procedureId }, false, true, options.EOIds.Length + " EO Unlinked from Procedure", System.DateTime.Now));
            //}
            return Ok( new { message = _stringLocalizer["ProcedureEOUnlinked"] });
        }

        [HttpGet]
        [Route("/procedures/{id}/enablingObjectives")]
        public async Task<IActionResult> GetLinkedEnablingObjectivessAsync(int id)
        {
            var result = await _procedureService.GetLinkedEnablingObjectivesAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all procedures that enabling objective is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/procedures/enablingObjectives/{id}")]
        public async Task<IActionResult> GetEOLinkedProcedures(int id)
        {
            var result = await _procedureService.GetEOLinkedProcedures(id);
            return Ok( new { result });
        }
    }
}
