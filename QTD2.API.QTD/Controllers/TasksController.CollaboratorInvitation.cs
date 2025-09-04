using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Task_CollaboratorInvitation;
using QTD2.Infrastructure.Model.Task_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Get All Task Collaborator Invitation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/colabInvite")]
        public async Task<IActionResult> GetAllTaskColabInvites()
        {
            var result = await _taskColInvitService.GetAllAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get Single Task Collaborator Invitation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/colabInvite/{id}")]
        public async Task<IActionResult> GetTaskColabInvite(int id)
        {
            var result = await _taskColInvitService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/{taskId}/colabInvite")]
        public async Task<IActionResult> GetAllColaboratorsForTask(int taskId)
        {
            var result = await _taskColInvitService.GetCollaboratorsForTask(taskId);
            return Ok( new { result });
        }

        /// <summary>
        /// Create Task Collaborator Invitaiton
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/tasks/colabInvite")]
        public async Task<IActionResult> CreateTaskColabInvite(Task_CollaboratorInvitationCreateOptions options)
        {
            var result = await _taskColInvitService.CreateAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Update a Task Collaborator Invitation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/tasks/colabInvite/{id}")]
        public async Task<IActionResult> UpdateTaskColabInvite(int id, Task_CollaboratorInvitationCreateOptions options)
        {
            var result = await _taskColInvitService.UpdateAsync(id, options);

            //var histOptions = new Task_HistoryOptions();
            //histOptions.ChangeNotes = options.InviteeEIds.Length + " Collaborators Invited";
            //histOptions.EffectiveDate = DateTime.Now;
            //histOptions.TaskIds = new int[] { id };
            //var task = await _taskService.GetAsync(id);
            //var version = _ver_taskService.CreateTaskVersion(task, 1).Result;
            //histOptions.Version_TaskId = version.Id;
            //await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Change status of Task Collaborator invitation to deleted, active or inactive
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/tasks/colabInvite/{id}")]
        public async Task<IActionResult> DeleteTaskColabInvite(int id, Task_CollaboratorInvitationOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "delete":
                    await _taskColInvitService.DeleteAsync(id);
                    break;
                case "inactive":
                    await _taskColInvitService.InActiveAsync(id);
                    break;
                case "active":
                    await _taskColInvitService.ActiveAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TaskCollaboratorInvitation-{options.ActionType.ToLower()}"].Value });
        }
    }
}
