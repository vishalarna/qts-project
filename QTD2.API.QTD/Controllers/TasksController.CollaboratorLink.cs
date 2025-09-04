using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Task_Collaborator_Link;
using QTD2.Infrastructure.Model.Task_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Link Task Collaborator Invitation With Task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/task/{id}/collab")]
        public async Task<IActionResult> LinkTaskCollab(int id, Task_Collaborator_LinkOptions options)
        {
            var result = await _taskService.LinkTaskColab(id, options);

            //var histOptions = new Task_HistoryOptions();
            //histOptions.ChangeNotes = "Collaborator Linked To Task";
            //histOptions.EffectiveDate = DateTime.Now;
            //histOptions.TaskIds = new int[] { id };
            //var version = _ver_taskService.CreateTaskVersion(result, 1).Result;
            //histOptions.Version_TaskId = version.Id;
            //await _historyService.SaveHistoryAsync(histOptions);
            return Ok(new { result });
        }

        /// <summary>
        /// Unlink Task Collaborator Invitaiton from task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskColabId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/task/{taskId}/collab/{taskColabId}")]
        public async Task<IActionResult> UnlinkTaskCollab(int taskId, int taskColabId)
        {
            await _taskService.UnlinkTaskColab(taskId, taskColabId);

            //var histOptions = new Task_HistoryOptions();
            //histOptions.ChangeNotes = "Collaborator Unlinked From Task";
            //histOptions.EffectiveDate = DateTime.Now;
            //histOptions.TaskIds = new int[] { taskId };
            //var task = await _taskService.GetAsync(taskId);
            //var version = _ver_taskService.CreateTaskVersion(task, 0).Result;
            //histOptions.Version_TaskId = version.Id;
            //await _historyService.SaveHistoryAsync(histOptions);
            return Ok(new { message = _localizer["TaskCollaboratorInvitationUnlinked"] });
        }
    }
}
