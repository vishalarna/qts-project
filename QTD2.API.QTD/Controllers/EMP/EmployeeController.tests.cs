using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EmployeeTest;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.EMP
{
    public partial class EmployeeController : EMPController
    {
        [HttpGet]
        [Route("/emp/tests")]
        public async Task<IActionResult> GetEmployeesTestAsync()
        {
            throw new System.NotImplementedException();
            //var locList = await _employeeTestService.GetEmployeesTestByIdAsync(EmployeeId);
            //return StatusCode(StatusCodes.Status200OK, new { locList });
        }

        [HttpPost]
        [Route("/emp/tests")]
        public async Task<IActionResult> SaveTestResponseAsync(EmpTestCreateOptions options)
        {
            throw new System.NotImplementedException();

            //var employeeTest = await _employeeTestService.CreateAsync(options, EmployeeId);
            //return StatusCode(StatusCodes.Status200OK, new { employeeTest, message = _localizer["empTestresponseCreated"] });
        }
    }
}
