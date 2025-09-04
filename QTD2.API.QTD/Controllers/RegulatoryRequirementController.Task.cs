using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Model.RR_StatusHistory;
using QTD2.Infrastructure.Model.RR_Task_Link;
using QTD2.Infrastructure.Model.Task_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class RegulatoryRequirementController : ControllerBase
    {
        [HttpPost]
        [Route("/rr/{id}/task")]
        public async Task<IActionResult> LinkTask(int id, RR_Task_LinkOptions options)
        {
            var result = await _regulatoryRequirementService.LinkTask(id, options);

            //foreach (var item in options.TaskIds)
            //{
                await _rr_historyService.CreateAsync(new RR_StatusHistoryCreateOptions
                {
                    ChangeEffectiveDate = System.DateTime.Now,
                    ChangeNotes =options.TaskIds.Length + "Task Linked to RR ",
                    RegulatoryRequirementId = id,
                });
            //  }

            foreach (var taskId in options.TaskIds)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                histOptions.TaskIds = new int[] { taskId };
                histOptions.ChangeNotes = "1 Regulatory Requirement Linked to Task";
                var task = await _taskService.GetAsync(taskId);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _task_histService.SaveHistoryAsync(histOptions);
            }

            return Ok( new { result });
        }

        /// <summary>
        /// Get the Tasks linked to the Regulatory Requirement given by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/{id}/task")]
        public async Task<IActionResult> GetTaskLinks(int id)
        {
            var result = await _regulatoryRequirementService.GetTaskLinkedToRR(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All Regulatory Requirements Task is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/task/{id}")]
        public async Task<IActionResult> getRRLinkedToTask(int id)
        {
            var result = await _regulatoryRequirementService.getRRLinkedToTask(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get the Tasks linked to the Regulatory Requirement given by id along with the number of links for that task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/{id}/task/count")]
        public async Task<IActionResult> GetTaskLinksWithCount(int id)
        {
            var result = await _regulatoryRequirementService.GetLinkedTasksWithCount(id);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/rr/{rrId}/task/")]
        public async Task<IActionResult> UnlinkTask(int rrId, RR_Task_LinkOptions tasks)
        {
            await _regulatoryRequirementService.UnlinkTask(rrId, tasks);

            foreach (var taskId in tasks.TaskIds)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                histOptions.TaskIds = new int[] { taskId };
                histOptions.ChangeNotes = "1 Regulatory Requirement Unlinked from Task";
                var task = await _taskService.GetAsync(taskId);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _task_histService.SaveHistoryAsync(histOptions);
            }
            return Ok( new { message = _localizer["TaskUnlinked"].Value });
        }
    }
}
