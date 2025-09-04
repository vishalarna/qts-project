using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Task_Reference;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Get All Task Reference Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/task/reference")]
        public async Task<IActionResult> GetAllRefAsync()
        {
            var result = await _taskRefService.GetAllAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get Task Reference data With given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/task/reference/{id}")]
        public async Task<IActionResult> GetRefAsync(int id)
        {
            var result = await _taskRefService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Create new Task Reference Data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/task/reference")]
        public async Task<IActionResult> CreateRefAsync(Task_ReferenceCreateOptions options)
        {
            var result = await _taskRefService.CreateAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Update Existing task refernce object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/task/reference/{id}")]
        public async Task<IActionResult> UpdateRefAsync(int id, Task_ReferenceCreateOptions options)
        {
            var result = await _taskRefService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Set Status of given task reference data with id (options = active, inactive or deleted)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/task/reference/{id}")]
        public async Task<IActionResult> DeleteRefAsync(int id, Task_ReferenceOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "delete":
                    await _taskRefService.DeleteAsync(id);
                    break;
                case "inactive":
                    await _taskRefService.InActiveAsync(id);
                    break;
                case "active":
                    await _taskRefService.ActiveAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TaskReference-{options.ActionType.ToLower()}"].Value });
        }
    }
}
