using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Version_Task_EnablingObjective_Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class Version_TaskController : ControllerBase
    {
        /// <summary>
        /// Gets a list of Version_Tasks
        /// </summary>
        /// <returns>Http Response Code with Version_Tasks</returns>
        [HttpGet]
        [Route("/versionTask/eo")]
        public async Task<IActionResult> GetVersion_TaskEOsAsync()
        {
            var result = await _versionTaskEOService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Version_Task
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/versionTask/eo")]
        public async Task<IActionResult> CreateVersion_TaskEOAsync(Version_Task_EnablingObjective_LinkCreateOptions options)
        {
            var result = await _versionTaskEOService.CreateAsync(options);
            return Ok( new { message = _localizer["Version_TaskCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Version_Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Version_Task</returns>
        [HttpGet]
        [Route("/versionTask/{id}/eo")]
        public async Task<IActionResult> GetVersion_TaskEOAsync(int id)
        {
            var result = await _versionTaskEOService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific Version_Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/versionTask/{id}/eo")]
        public async Task<IActionResult> UpdateVersion_TaskEOAsync(int id, Version_Task_EnablingObjective_LinkUpdateOptions options)
        {
            var result = await _versionTaskEOService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["Version_TaskUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Version_Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/versionTask/{id}/eo")]
        public async Task<IActionResult> DeleteVersion_TaskEOAsync(int id, Version_Task_EnablingObjective_LinkOptions options)
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
