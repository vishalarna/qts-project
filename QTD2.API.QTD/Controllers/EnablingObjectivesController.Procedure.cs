using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjective_Procedure_Link;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        /// <summary>
        /// Links a procedure to a specific enabling objective
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/enablingObjectives/{id}/procedures")]
        public async Task<IActionResult> LinkProcedureAsync(EO_LinkOptions options)
        {
            await _enablingObjectiveService.LinkProcedureAsync(options);

            //var eo = await _enablingObjectiveService.GetAsync(id);
            //await _versioningService.VersionEnablingObjectiveAsync(eo);
            //if (options.IsSignificant)
            //{
            //    var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //    foreach (var vTask in versionedTasks)
            //    {
            //        await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //        await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //    }
            //}

            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.EnablingObjectiveId = options.EOId;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.EnablingObjectiveId = options.EOId;

            histOptions.ChangeNotes = options.ProcedureIds.Length + " Procedures Linked To EO";
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["EOProcLinked"] });
        }

        [HttpGet]
        [Route("/enablingObjectives/{eoId}/procedures/count")]
        public async Task<IActionResult> GetLinkedProceduresWithCount(int eoId)
        {
            var result = await _enablingObjectiveService.GetProcedureWithLinkCount(eoId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{metaId}/procedures/allcount")]
        public async Task<IActionResult> GetLinkedProceduresToMetaEO(int metaId)
        {
            var result = await _enablingObjectiveService.GetLinkedProceduresToMetaEOAsync(metaId);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks a procedure to a specific enabling objective
        /// </summary>
        /// <param name="enablingObjectiveId"></param>
        /// <param name="procedureId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/enablingObjectives/{enablingObjectiveId}/procedures/")]
        public async Task<IActionResult> UnlinkProcedureAsync(int enablingObjectiveId, EO_LinkOptions options)
        {
            await _enablingObjectiveService.UnlinkProcedureAsync(options);
            //var eo = await _enablingObjectiveService.GetAsync(enablingObjectiveId);
            //await _versioningService.VersionEnablingObjectiveAsync(eo);
            //if (options.isSignificant)
            //{
            //    var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //    foreach (var vTask in versionedTasks)
            //    {
            //        await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //        await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //    }
            //}

            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.OldStatus = true;
            histOptions.NewStatus = false;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.EnablingObjectiveId = options.EOId;
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["EOprocedureUnlinked"] });
        }
    }
}
