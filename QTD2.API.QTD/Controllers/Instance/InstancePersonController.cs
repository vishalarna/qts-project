using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.Person;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.Instance
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstancePersonController : ControllerBase
    {
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IPersonService _personService;
        private readonly IDatabaseResolver _databaseResolver;

        public InstancePersonController(IPersonService personService,
            IInstanceFetcher instanceFetcher,
            IDatabaseResolver databaseResolver)
        {
            _personService = personService;
            _instanceFetcher = instanceFetcher;
            _databaseResolver = databaseResolver;
        }

        [HttpGet]
        [Route("/instance/{instanceName}/persons/userData")]
        public async Task<IActionResult> GetPersonsWithUserDataAsync(string instanceName)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var persons = await _personService.GetPersonsWithUserDataAsync();
            return Ok( new { persons });
        }

        [HttpGet]
        [Route("/instance/{instanceName}/persons/userData/{id}")]
        public async Task<IActionResult> GetUserDetailAsync(int id, string instanceName)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var persons = await _personService.GetUserDetailAsync(id);
            return Ok( new { persons });
        }

        [HttpGet]
        [Route("/instance/{instanceName}/persons/userdata/byUsername/{userName}")]
        public async Task<IActionResult> GetUserDetailByUserNameAsync(string instanceName, string userName)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var persons = await _personService.GetUserDetailByUserNameAsync(userName);
            return Ok( new { persons });
        }

        [HttpPost]
        [Route("/instance/{instanceName}/persons")]
        public async Task<IActionResult> CreateAsync(string instanceName, PersonCreateOptions personCreateOptions)
        {
            try
            {
                var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
                _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
                var person = await _personService.CreateAsync(personCreateOptions, true);
                return Ok( new { person });
            }
            catch (ConflictExceptionHelper ex)
            {
                return Conflict(new { ex.ConflictValue });
            }
        }

        [HttpPut]
        [Route("/instance/{instanceName}/persons/{id}")]
        public async Task<IActionResult> UpdateAsync(string instanceName, int id, PersonUpdateOptions personUpdateOptions)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var person = await _personService.UpdateAsync(id, personUpdateOptions);
            return Ok( new { person });
        }
        
        [HttpPut]
        [Route("/instance/{instanceName}/persons/{id}/deactivate")]
        public async Task<IActionResult> DeactivateAsync(string instanceName, int id)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var person = await _personService.DeactivateAsync(id);
            return Ok( new {person});
        }

        [HttpPut]
        [Route("/instance/{instanceName}/persons/{id}/activate")]
        public async Task<IActionResult> ActivateAsync(string instanceName, int id)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var person = await _personService.ActivateAsync(id);
            return Ok( new { person });
        }
    }
}
