using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Position_Task_Link;
using QTD2.Infrastructure.Model.PositionHistory;
using QTD2.Infrastructure.Model.Task_History;
using System;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
   
    public partial class PositionsController : ControllerBase
    {
        [HttpPost]
        [Route("/positions/{id}/task")]
        public async Task<IActionResult> LinkTask(int id, Position_Task_LinkCreateOptions options)
        {
            var result = await _positionService.LinkTask(id, options);

            //foreach (var item in options.TaskIds)
            //{
                await _positionhistoryService.CreateAsync(new Position_HistoryCreateOptions()
                {
                    ChangeNotes =options.TaskIds.Length + " Task Linked to Position",
                    EffectiveDate = DateTime.Now,
                    PositionId = id,
                    taskIds = options.TaskIds
                });
            //}

            foreach (var taskId in options.TaskIds)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                histOptions.TaskIds = new int[] { taskId };
                histOptions.ChangeNotes = "1 Position Linked to Task";
                var task = await _taskService.GetAsync(taskId);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _task_histService.SaveHistoryAsync(histOptions);
            }

            return Ok();
        }

        /// <summary>
        /// Get tasks linked to position
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/positions/{id}/task")]
        public async Task<IActionResult> GetLinkedTasks(int id)
        {
            var result = await _positionService.GetLinkedTasks(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all positions that the task is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/positions/task/{id}")]
        public async Task<IActionResult> GetPosTaskIsLinkedTo(int id)
        {
            var result = await _positionService.GetPositionsTaskIsLinkedTo(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all positions that the task is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/positions/task/{id}/traininggroup")]
        public async Task<IActionResult> GetPosTaskIsLinkedToTrainingGroup(int id)
        {
            var result = await _positionService.GetPositionsTaskIsLinkedToTG(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink specific Tasks linked to position provided by positionId
        /// </summary>
        /// <param name="posId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/positions/{posId}/task/")]
        public async Task<IActionResult> UnlinkTask(int posId, Position_Task_LinkCreateOptions options)
        {
            await _positionService.UnlinkTask(posId, options.TaskIds);

            await _positionhistoryService.CreateAsync(new Position_HistoryCreateOptions()
                {
                    ChangeNotes = options.TaskIds.Length + " Task UnLinked from Position",
                    EffectiveDate = DateTime.Now,
                    PositionId = options.PositionId,
                    taskIds = options.TaskIds
                });

            foreach (var taskId in options.TaskIds)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                histOptions.TaskIds = new int[] { taskId };
                histOptions.ChangeNotes = "1 Position Unlinked From Task";
                var task = await _taskService.GetAsync(taskId);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _task_histService.SaveHistoryAsync(histOptions);
            }

            return Ok( new { message = _localier["TaskUnlinked"].Value });
        }
    }
}
