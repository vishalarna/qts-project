using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Task_Reference_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Link Task reference to task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/task/{id}/reference")]
        public async Task<IActionResult> LinkTaskReference(int id, Task_Reference_LinkOptions options)
        {
            var result = await _taskService.LinkTaskReference(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink Task Reference from Task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskRefId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/task/{taskId}/reference/{taskRefId}")]
        public async Task<IActionResult> UnlinkTaskReference(int taskId, int taskRefId)
        {
            await _taskService.UnlinkTaskReference(taskId, taskRefId);
            return Ok( new { message = _localizer["TaskReferenceUnlinked"].Value });
        }
    }
}
