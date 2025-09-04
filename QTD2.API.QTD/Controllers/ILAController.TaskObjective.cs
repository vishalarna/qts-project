using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA_TaskObjective_Link;
using QTD2.Infrastructure.Model.Task_History;
using System.Linq;
using QTD2.Infrastructure.Model.Employee;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpPut]
        [Route("/ila/{id}/taskObjective/useForTQ")]
        public async Task<IActionResult> UpdateTObjUsedForTQ(int id, ILA_TaskObjective_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.TaskIds.Length.ToString() + " Task Objective(s) Updated for Task Qualification Use", DateTime.Now, 2);
            await _ilaService.UpdateTObjUsedForTQAsync(id, options);
            return Ok( new { message = _localizer["TQObjectivesUpdated"] });
        }

        /// <summary>
        /// Get the TaskObjectives linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked TaskObjectives</returns>
        [HttpGet]
        [Route("/ila/{id}/taskObjective")]
        public async Task<IActionResult> GetLinkedTaskObjectiveAsync(int id)
        {
            var result = await _ilaService.GetLinkedTaskObjectivesAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get the TaskObjectives and the duty areas linked with specific ILA
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/{id}/taskObjective/DutyAreas")]
        public async Task<IActionResult> GetDutyAreasForLinkedTasks(int id)
        {
            var result = await _ilaService.getDutyAreasForLinkedTasks(id);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/ila/{id}/taskObjective")]
        public async Task<IActionResult> UpdateILATaskObjectiveLinksAsync(int id,ILATaskObjectiveLinkUpdateOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            var result = await _ilaService.UpdateILATaskObjectiveLinksAsync(id, options);
            if (result.TasksAdded.Count > 0)
            {
                await _versioningService.VersionILAAsync(ila, result.TasksAdded.Count.ToString() + " Task Objective(s) Linked", DateTime.Now, 1);
            }
            if (result.TasksRemoved.Count > 0)
            {
                await _versioningService.VersionILAAsync(ila, result.TasksRemoved.Count.ToString() + " Task Objective(s) Unlinked", DateTime.Now, 0);
            }
            var allTaskChanges = result.TasksAdded.Concat(result.TasksRemoved).ToList();
            foreach (var task in allTaskChanges)
            {
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                var changeNotes = result.TasksAdded.Contains(task) ? "1 ILA Linked To Task" : "1 ILA Unlinked From Task";
                var histOptions = new Task_HistoryOptions(DateTime.UtcNow, new int[] { task.Id },changeNotes,version.Id);
                await _task_histService.SaveHistoryAsync(histOptions);
            }
            await _ilaService.ReorderObjectiveLinks(id);
            return Ok();
        }

        [HttpPost ]
        [Route("/ila/{id}/emp/pendingtaskObjective")]
        public async Task<IActionResult> GetPendingLinkedTaskObjectiveAsync(int id,EmployeeIdsModel options)
        {
            var result = await _ilaService.GetPendingLinkedTaskObjectivesAsync(id, options);
            return Ok(new { result });
        }

    }
}
