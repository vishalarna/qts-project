using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Get a list of procedure linked to a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/procedures")]
        public async Task<IActionResult> GetLinkedProceduresAsync(int id)
        {
            var tasksProc = await _taskService.GetLinkedProceduresAsync(id);
            return Ok( new { tasksProc });
        }

        /// <summary>
        /// Links a procedure to a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/tasks/{id}/procedures")]
        public async Task<IActionResult> LinkProceduresAsync(int id, TaskOptions options)
        {
            //var task_procs = _taskService.GetTaskProcLinks(id).Result;
            //await _versioningService.VersionTask_ProcedureLinkAsync(task_procs);
            await _taskService.LinkProcedureAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
            histOptions.TaskIds = new int[] { id };
            
            histOptions.ChangeNotes = options.ProcedureIds.Length + " Procedures Linked To Task";
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            
            return Ok( new { message = _localizer["TaskProcLinked"] });
        }

        /// <summary>
        /// Get All the procedures linked to task along with link count of procedure with tasks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{id}/procedures/count")]
        public async Task<IActionResult> GetLinkedProcedureWithCount(int id)
        {
            var result = await _taskService.GetLinkedProcedureWithCount(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/{metaId}/procedures/allcount")]
        public async Task<IActionResult> GetLinkedProceduresToMetaEO(int metaId)
        {
            var result = await _taskService.GetLinkedProceduresToMetaTaskAsync(metaId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All the Tasks that procedure is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/procedures/{id}")]
        public async Task<IActionResult> GetTasksProcIsLinkedTo(int id)
        {
            var result = await _taskService.GetTasksProcIsLinkedTo(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks a procedure to a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/tasks/{id}/procedures")]
        public async Task<IActionResult> UnlinkProceduresAsync(int id, TaskOptions options)
        {
            //var task_procs = _taskService.GetTaskProcLinks(id).Result;
            //await _versioningService.VersionTask_ProcedureLinkAsync(task_procs);
            await _taskService.UnlinkProcedureAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EffectiveDate = options.EffectiveDate;
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { message = _localizer["ProcedureUnlinkedFromTask"] });
        }
    }
}
