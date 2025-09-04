using Microsoft.AspNetCore.Mvc;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        // Noo tab is available of TimeSheet in Entites - QTDContext.drawio & Application Service.drawio

        // [HttpPost]
        // [Route("/tasks/{name}/employeeTasks/{employeeName}/timesheets")]
        // public async Task<IActionResult> CreateTimeSheet(string name, string questionName)
        // {
        //    _taskService.RemoveQuestion(name, questionName);
        //    return StatusCode(StatusCodes.Status200OK, new { message = "QuestionRemovedFromTask" });
        // }
    }
}
