using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EmployeeOrganization;

namespace QTD2.API.QTD.Controllers
{
    public partial class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Gets a list of organizations for a specific employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Http Response Code with Organizations data</returns>
        [HttpGet]
        [Route("/employees/{employeeId}/organizations")]
        public async Task<IActionResult> GetOrganizationsAsync(int employeeId)
        {
            var organizations = await _employeeService.GetOrganizationsAsync(employeeId);
            return Ok( new { organizations });
        }

        /// <summary>
        /// Adds an Employee to an Organization
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="options"></param>
        /// <returns>Http Response Code with data</returns>
        ////[HttpPost]
        ////[Route("/employees/{employeeId}/organizations")]
        ////public async Task<IActionResult> CreateOrganizationAsync(int employeeId, EmployeeOrganizationCreateOptions options)
        ////{
        ////    var organization = await _employeeService.AddOrganizationAsync(employeeId, options);
        ////    return Ok( new { organization, message = _localizer["empOrgCreated"] });
        ////}

        /// <summary>
        /// Updates a specifci certificate from a provider by name
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="organizationId"></param>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPut]
        [Route("/employees/{employeeId}/organizations/{organizationId}")]
        public async Task<IActionResult> UpdateOrganizationAsync(int employeeId, int organizationId, EmployeeOrganizationUpdateOptions options)
        {
            await _employeeService.EditOrganizationAsync(employeeId, organizationId, options);
            return Ok( new { message = _localizer["empOrgUpdated"] });
        }

        /// <summary>
        /// Adds a new certificate to a certificate provider
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="organizationId"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpDelete]
        [Route("/employees/{employeeId}/organizations/{organizationId}")]
        public async Task<IActionResult> DeleteOrganizationAsync(int employeeId, int organizationId)
        {
            await _employeeService.DeleteOrganizationAsync(employeeId, organizationId);
            return Ok( new { message = _localizer["empOrgDeleted"] });
        }

        [HttpPost]
        [Route("/employees/{employeeId}/organizations")]
        public async Task<IActionResult> LinkOrganizations(int employeeId, EmployeeOrganizationCreateOptions options)
        {
            var result = await _employeeService.LinkOrganization(employeeId, options);

            //foreach (var item in options.EmployeeIds)
            //{
            //    await _positionhistoryService.CreateAsync(new Position_HistoryCreateOptions()
            //    {
            //        ChangeNotes = "Position Linked to Employee Id => " + item,
            //        EffectiveDate = DateTime.Now,
            //        PositionId = id,
            //        taskIds = options.EmployeeIds
            //    });
            //}

            return Ok( new { result });
        }
        [HttpGet]
        [Route("/employees/organizations/{id}")]
        public async Task<IActionResult> GetOrgEmployeeIsLinkedTo(int id)
        {
            var result = await _employeeService.GetOrganizationssEmployeeIsLinkedTo(id);
            return Ok( new { result });
        }
        [HttpPut]
        [Route("/employees/{employeeId}/organizations/")]
        public async Task<IActionResult> ChangeIsManagerStatus(int employeeId, EmployeeOrganizationCreateOptions options)
        {
            await _employeeService.ToggleIsManagerAsync(employeeId, options);
            return Ok( new { message = _localizer["empOrgstatusChange"] });
        }
        //[HttpGet]
        //[Route("/employees/{id}/organizations")]
        //public async Task<IActionResult> GetLinkedOrganization(int id)
        //{
        //    var result = await _positionService.GetLinkedEmployees(id);
        //    return StatusCode(StatusCodes.Status200OK, new { result });
        //}
    }
}