
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Interfaces.Service.Core;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public partial class EmployeesSummaryController : ControllerBase
    {
        private readonly IEmployeeSummaryService _employeeService;
        private readonly IClientSettings_LicenseService _clientlicenseService;

        public EmployeesSummaryController(IEmployeeSummaryService employeeSercice, IClientSettings_LicenseService clientLicenseService)
        {
            _employeeService = employeeSercice;
            _clientlicenseService = clientLicenseService;
        }


        [HttpGet]
        [Route("/employees/list/{name}")]
        public async Task<IActionResult> GetEmpList(string name)
        {
            var result = await _employeeService.GetEmployeeLists(name);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/simplifiedlist")]
        public async Task<IActionResult> GetEmpSimplifiedList()
        {
            var result = await _employeeService.GetEmployeeLists("active");
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/statistics")]
        public async Task<IActionResult> GetEmpDashboardStatistics()
        {
            var result = await _employeeService.GetEmpDashboardStatisticsAsync();
            var currentLicense = await _clientlicenseService.GetCurrentLicense();
            result.TotalEmployeeRecordsAvailable = currentLicense.TotalEmployeeRecordsAvailable;
            return Ok( new { result });
        }
    }

}
