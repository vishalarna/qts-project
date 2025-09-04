using Microsoft.AspNetCore.Mvc;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /*
         *
         * Noo tab is available of Employee_Task in Entites - QTDContext.drawio & Application Service.drawio,

        [HttpGet]
        [Route("/tasks/{name}/employeeTasks/{employeeName}")]
        public async Task<IActionResult> GetEmployeeTasks(string name, string employeeName)
        {
            var employeeTasks = _taskService.GetEmployeeTask(name, employeeName);
            return StatusCode(StatusCodes.Status200OK, new { employeeTasks });
        }
        [HttpGet]
        [Route("/tasks/{name}/employeeTasks/{employeeName}/versions/{version}")]
        public async Task<IActionResult> GetEmployeeTasks(string name, string employeeName, string version)
        {
            var employeeTasks = _taskService.GetEmployeeTasks(name, version, employeeName);
            return StatusCode(StatusCodes.Status200OK, new { employeeTasks });
        }
        */
    }
}
