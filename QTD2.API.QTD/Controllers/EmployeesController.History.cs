using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EmployeeHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EmployeesController : ControllerBase
    {
        [HttpPost]
        [Route("/employees/history2")]
        public async Task<IActionResult> CreateEmployeeHistory(EmployeeHistoryCreateOptions options)
        {
            var result = await _employeeHistoryService.CreateEmployeeHistory(options);
            return Ok(new { result });
        }
    }
}
