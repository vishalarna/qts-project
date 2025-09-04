using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Version_Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class Version_TaskController : ControllerBase
    {
        private readonly IVersion_TaskService _versionTaskService;
        private readonly IVersion_Task_EnablingObjective_LinkService _versionTaskEOService;
        private readonly IVersion_Task_Procedure_LinkService _versionTaskProcedureService;
        private readonly IVersion_Task_SaftyHazard_LinkService _versionTaskSHService;
        private readonly IStringLocalizer<Version_TaskController> _localizer;

        public Version_TaskController(IVersion_TaskService versionTaskService, IStringLocalizer<Version_TaskController> localizer, IVersion_Task_EnablingObjective_LinkService versionTaskEOService, IVersion_Task_Procedure_LinkService versionTaskProcedureService, IVersion_Task_SaftyHazard_LinkService versionTaskSHService)
        {
            _versionTaskService = versionTaskService;
            _localizer = localizer;
            _versionTaskEOService = versionTaskEOService;
            _versionTaskProcedureService = versionTaskProcedureService;
            _versionTaskSHService = versionTaskSHService;
        }



        /// <summary>
        /// Gets a list of Version_Tasks
        /// </summary>
        /// <returns>Http Response Code with Version_Tasks</returns>
        [HttpGet]
        [Route("/versionTask")]
        public async Task<IActionResult> GetVersion_TasksAsync()
        {
            var result = await _versionTaskService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get all versions with history for a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/versionTask/task/{taskId}")]
        public async Task<IActionResult> GetVersionsForTaskWithHist(int taskId)
        {
            var result = await _versionTaskService.GetTaskVersionsWithHistoryAsync(taskId);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Version_Task
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        //[HttpPost]
        //[Route("/versionTask")]
        //public async Task<IActionResult> CreateVersion_TaskAsync(Version_EnablingObjective_StepCreateOptions options)
        //{
        //    var result = await _versionTaskService.CreateAsync(options);
        //    return Ok( new { message = _localizer["Version_TaskCreated"].Value });
        //}

        /// <summary>
        /// Gets a specific Version_Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Version_Task</returns>
        [HttpGet]
        [Route("/versionTask/{id}")]
        public async Task<IActionResult> GetVersion_TaskAsync(int id)
        {
            var result = await _versionTaskService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific Version_Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/versionTask/{id}")]
        public async Task<IActionResult> UpdateVersion_TaskAsync(int id, Version_TaskUpdateOptions options)
        {
            var result = await _versionTaskService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["Version_TaskUpdated"].Value });
        }
        [HttpPut]
        [Route("/versionTask/requalification/{id}")]
        public async Task<IActionResult> UpdateVersionRequalification_TaskAsync(int id, Version_TaskRequalificationInfo options)
        {
            var result = await _versionTaskService.UpdateVersionTaskRequalificationInfoAsync(id, options);
            return Ok( new { message = _localizer["Version_TaskUpdated"].Value , versionTask =result});
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Version_Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/versionTask/{id}")]
        public async Task<IActionResult> DeleteVersion_TaskAsync(int id, Version_TaskOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "delete":
                    await _versionTaskService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"Version_Task-{options.ActionType.ToLower()}"].Value });
        }
    }
}
