using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.ClientUser;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.Instance
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstanceClientUserController : ControllerBase
    {
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IStringLocalizer<InstanceClientUserController> _localizer;
        private readonly IClientUserService _clientUserService;
        private readonly IDatabaseResolver _databaseResolver;

        public InstanceClientUserController(IInstanceFetcher instanceFetcher,
           IStringLocalizer<InstanceClientUserController> localizer,
           IClientUserService clientUserService,
            IDatabaseResolver databaseResolver)
        {
            _instanceFetcher = instanceFetcher;
            _localizer = localizer;
            _clientUserService = clientUserService;
            _databaseResolver = databaseResolver;
        }

        [HttpPost]
        [Route("/instance/{instanceName}/clientUsers")]
        public async Task<IActionResult> CreateAsync(string instanceName, ClientUserCreateOptions options)
        {
            try
            {
                var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
                _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
                var clientUser = await _clientUserService.CreateAsync(options, true);
                return Ok( new { clientUser, message = _localizer["ClientUserCreated"] });
            }
            catch (ConflictExceptionHelper ex)
            {
                return Conflict(new { ex.ConflictValue });
            }
        }

        [HttpPut]
        [Route("/instance/{instanceName}/clientUsers/{personId}/activate")]
        public async Task<IActionResult> ActivateAsync(string instanceName, int personId)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var clientUser = await _clientUserService.ActivateAsync(personId);
            return Ok( new { clientUser });
        }

        [HttpPut]
        [Route("/instance/{instanceName}/clientUsers/{personId}/deactivate")]
        public async Task<IActionResult> DeactivateAsync(string instanceName, int personId)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var clientUser = await _clientUserService.DeactivateAsync(personId);
            return Ok( new { clientUser });
        }

    }
}
