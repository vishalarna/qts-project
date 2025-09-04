using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.MetaILA_Employee;

namespace QTD2.API.QTD.Controllers
{
    public partial class MetaILAController : ControllerBase
    {
        [HttpGet]
        [Route("/metailas/{metaILAId}/employees")]
        public async Task<IActionResult> GetLinkedMetaILAEmployeesAsync(int metaILAId)
        {
            var result = await _metaIlaService.GetLinkedMetaILAEmployeesAsync(metaILAId);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/metailas/employees/link")]

        public async Task<IActionResult> LinkMetaILAEmployeesAsync(MetaILA_EmployeeOptions options)
        {
            var result = await _metaIlaService.LinkMetaILAEMployeesAsync(options);
            return Ok();
        }

        [HttpPut]
        [Route("/metailas/employees/unlink")]
        public async Task<IActionResult> UnlinkMetaILAEmployees(MetaILA_EmployeeOptions options)
        {
            var result = await _metaIlaService.UnlinkLinkMetaILAEMployeesAsync(options);
            return Ok();
        }

        [HttpGet]
        [Route("/metailas/employee/idp/{empId}")]
        public async Task<IActionResult> GetLinkedMetaILAsByEmployeeIdForIDPAsync(int empId)
        {
            var result = await _metaIlaService.GetLinkedMetaILAsByEmployeeIdForIDPAsync(empId);
            return Ok(new { result });
        }
    }
}
