using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.EMP
{
  
    public partial class EmployeeController : EMPController
    {

        [HttpGet]
        [Route("/emp/procedureReviewEmp/procedureReviews")]
        public async Task<IActionResult> GetEmployeeProcedureReviews()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _procedureReviewEmpService.GetEmpProcedureReviewsByIdAsync(employeeId);
            return Ok(new { result });
        }
    }
}
