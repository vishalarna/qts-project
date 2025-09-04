using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Procedure_Task_Link;
using QTD2.Infrastructure.Model.Task_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class ProceduresController : ControllerBase
    {
        /// <summary>
        /// Link the Task with Procedure whose Id is provided
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns> Http Response code along with linked data </returns>
        [HttpPost]
        [Route("/procedures/{id}/task")]
        public async Task<IActionResult> LinkTask(int id, Procedure_Task_LinkCreateOptions options)
        {
            var result = await _procedureService.LinkTask(id, options);

            //foreach (var item in options.TaskIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { id }, false, true, options.TaskIds.Length + " Task Linked to Procedure", DateTime.Now));
            //}

            foreach(var taskId in options.TaskIds)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                histOptions.TaskIds = new int[] { taskId };

                histOptions.ChangeNotes = "1 Procedure Linked To Task";
                var task = await _taskService.GetAsync(taskId);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _task_histService.SaveHistoryAsync(histOptions);
            }

            return Ok( new { result });
        }

        /// <summary>
        /// Get tasks linked to procedure
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/procedures/{id}/task")]
        public async Task<IActionResult> GetLinkedTasks(int id)
        {
            var result = await _procedureService.GetLinkedTasks(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all procedures that the task is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/procedures/task/{id}")]
        public async Task<IActionResult> GetProcTaskIsLinkedTo(int id)
        {
            var result = await _procedureService.GetProcTaskIsLinkedTo(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink specific Tasks linked to procedure provided by procId
        /// </summary>
        /// <param name="procId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/procedures/{procId}/task/")]
        public async Task<IActionResult> UnlinkTask(int procId, Procedure_Task_LinkCreateOptions options)
        {
            await _procedureService.UnlinkTask(procId, options.TaskIds);
            //foreach (var item in options.TaskIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { procId }, false, true,options.TaskIds.Length +  " Task Unlinked from Procedure", DateTime.Now));
            //}
            foreach (var taskId in options.TaskIds)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                histOptions.TaskIds = new int[] { taskId };
                histOptions.ChangeNotes = "1 Procedure Unlinked From Task";
                var task = await _taskService.GetAsync(taskId);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _task_histService.SaveHistoryAsync(histOptions);
            }
            return Ok( new { message = _stringLocalizer["TaskUnlinked"].Value });
        }

    }
}
