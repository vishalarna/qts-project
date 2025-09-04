using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.QtdUser;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.Instance
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstanceQTDUserController : ControllerBase
    {
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IStringLocalizer<InstanceClientUserController> _localizer;
        private readonly IQTDService _qTDService;
        private readonly IDatabaseResolver _databaseResolver;

        public InstanceQTDUserController(IInstanceFetcher instanceFetcher,
           IStringLocalizer<InstanceClientUserController> localizer,
           IQTDService qTDService,
           IDatabaseResolver databaseResolver)
        {
            _instanceFetcher = instanceFetcher;
            _localizer = localizer;
            _qTDService = qTDService;
            _databaseResolver = databaseResolver;
        }

        [HttpPost]
        [Route("/instance/{instanceName}/qtdUsers")]
        public async Task<IActionResult> CreateAsync(string instanceName, QtdUserVM qtdUserOption)
        {
            try
            {
                var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
                _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
                var qtdUser = await _qTDService.CreateAsync(qtdUserOption, true);
                return Ok( new { qtdUser });
            }
            catch (ConflictExceptionHelper ex)
            {
                return Conflict(new { ex.ConflictValue });
            }
        }

        [HttpPut]
        [Route("/instance/{instanceName}/qtdUsers/{id}")]
        public async Task<IActionResult> UpdateAsync(string instanceName, int id, QtdUserVM qtdUserOption)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var qtdUser = await _qTDService.UpdateAsync(id, qtdUserOption);
            return Ok( new { qtdUser });
        }

        [HttpPut]
        [Route("/instance/{instanceName}/qtdUsers/{id}/activate")]
        public async Task<IActionResult> ActivateAsync(string instanceName, int id)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var qtdUser = await _qTDService.ActivateAsync(id);
            return Ok( new { qtdUser });
        }

        [HttpPut]
        [Route("/instance/{instanceName}/qtdUsers/{id}/deactivate")]
        public async Task<IActionResult> DeactivateAsync(string instanceName, int id)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var qtdUser = await _qTDService.DeactivateAsync(id);
            return Ok( new { qtdUser });
        }

    }
}
