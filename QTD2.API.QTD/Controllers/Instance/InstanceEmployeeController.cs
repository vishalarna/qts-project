using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.Employee;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.Instance
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstanceEmployeeController : ControllerBase
    {
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IStringLocalizer<InstanceClientUserController> _localizer;
        private readonly IEmployeeService _employeeService;
        private readonly IDatabaseResolver _databaseResolver;

        public InstanceEmployeeController(IInstanceFetcher instanceFetcher,
           IStringLocalizer<InstanceClientUserController> localizer,
           IEmployeeService employeeService,
           IDatabaseResolver databaseResolver)
        {
            _instanceFetcher = instanceFetcher;
            _localizer = localizer;
            _employeeService = employeeService;
            _databaseResolver = databaseResolver;
        }

        [HttpPost]
        [Route("/instance/{instanceName}/employees")]
        public async Task<IActionResult> CreateAsync(string instanceName, EmployeeCreateOptions options)
        {
            try
            {
                var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
                _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
                var employees = await _employeeService.CreateAsync(options, true);
                return Ok( new { employees, message = _localizer["employeeCreated"] });
            }
            catch (ConflictExceptionHelper ex)
            {
                return Conflict(new { ex.ConflictValue });
            }
        }

        [HttpPut]
        [Route("/instance/{instanceName}/employees/{id}")]
        public async Task<IActionResult> UpdateAsync(string instanceName, int id, EmployeeUpdateOptions options)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var employee = await _employeeService.UpdateAsync(id, options);
            return Ok( new { employee, message = _localizer["employeeUpdated"] });
        }

        [HttpPut]
        [Route("/instance/{instanceName}/employees/{id}/activate")]
        public async Task<IActionResult> ActivateAsync(string instanceName, int id)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            await _employeeService.ActivateAsync(id);
            return Ok( new { message = _localizer["employeeActivated"] });
        }

        [HttpPut]
        [Route("/instance/{instanceName}/employees/{id}/deactivate")]
        public async Task<IActionResult> DeactivateAsync(string instanceName, int id)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var employee = await _employeeService.DeactivateAsync(id);
            return Ok( new { employee, message = _localizer["employeeDeactivated"] });
        }

        [HttpGet]
        [Route("/instance/{instanceName}/employee/byEmployeeNumber/{employeeNumber}")]
        public async Task<IActionResult> GetEmployeeByNumberAsync(string instanceName, string employeeNumber)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var employee = await _employeeService.GetEmployeeByNumberAsync(employeeNumber);
            return Ok( new { employee });
        }

    }
}
