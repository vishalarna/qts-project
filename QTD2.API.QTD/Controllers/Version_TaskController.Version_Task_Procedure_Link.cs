using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Version_Task_Procedure_Link;
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
        [Route("/versionTask/procedure")]
        public async Task<IActionResult> GetVersion_TaskProceduresAsync()
        {
            var result = await _versionTaskProcedureService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Version_Task
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/versionTask/procedure")]
        public async Task<IActionResult> CreateVersion_TaskProcedureAsync(Version_Task_Procedure_LinkCreateOptions options)
        {
            var result = await _versionTaskProcedureService.CreateAsync(options);
            return Ok( new { message = _localizer["Version_TaskCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Version_Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Version_Task</returns>
        [HttpGet]
        [Route("/versionTask/{id}/procedure")]
        public async Task<IActionResult> GetVersion_TaskProcedureAsync(int id)
        {
            var result = await _versionTaskProcedureService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific Version_Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/versionTask/{id}/procedure")]
        public async Task<IActionResult> UpdateVersion_TaskProcedureAsync(int id, Version_Task_Procedure_LinkUpdateOptions options)
        {
            var result = await _versionTaskProcedureService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["Version_TaskUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Version_Task by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/versionTask/{id}/procedure")]
        public async Task<IActionResult> DeleteVersion_TaskProcedureAsync(int id, Version_Task_Procedure_LinkOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "delete":
                    await _versionTaskProcedureService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"Version_Task-{options.ActionType.ToLower()}"].Value });
        }
    }
}
