using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjective_SaftyHazard_Link;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        /// <summary>
        /// Links a safty hazard to an enabling objective
        /// </summary>
        /// <param name="options"></param>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/enablingObjectives/{id}/saftyHazards")]
        public async Task<IActionResult> LinkSaftyHazardAsync(int id, EO_LinkOptions options)
        {
            await _enablingObjectiveService.LinkSaftyHazardAsync(options);

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

            histOptions.EnablingObjectiveId = id;
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            histOptions.ChangeNotes = options.SafetyHazardIds.Length + " Safety hazards Linked to EO.";
            var eo =await _enablingObjectiveService.GetAsync(id);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["EOSHLinked"] });
        }

        /// <summary>
        /// Get linked Safety Hazards along with link count
        /// </summary>
        /// <param name="eoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/saftyHazards/count")]
        public async Task<IActionResult> GetLinkedSHWithCount(int eoId)
        {
            var result = await _enablingObjectiveService.GetSafetyHazardWithLinkCounts(eoId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{metaId}/saftyHazards/allcount")]
        public async Task<IActionResult> GetLinkedSHWithMetaEO(int metaId)
        {
            var result = await _enablingObjectiveService.GetLinkedSHWithMetaEOAsync(metaId);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks a safty hazard to an enabling objective
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/enablingObjectives/{id}/saftyHazards")]
        public async Task<IActionResult> UnlinkSaftyHazardAsync(EO_LinkOptions options)
        {
            await _enablingObjectiveService.UnlinkSaftyHazardAsync(options);
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
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EnablingObjectiveId = options.EOId;
            histOptions.NewStatus = false;
            histOptions.OldStatus = true;
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["EOSHUnlinked"] });
        }
    }
}
