using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model.ClientSettings;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.Instance
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstanceClientSettingsController : ControllerBase
    {
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IStringLocalizer<InstanceClientUserController> _localizer;
        private readonly IClientSettingsService _clientSettingsService;
        private readonly IDatabaseResolver _databaseResolver;

        public InstanceClientSettingsController(IInstanceFetcher instanceFetcher,
           IStringLocalizer<InstanceClientUserController> localizer,
           IClientSettingsService clientSettingsService,
            IDatabaseResolver databaseResolver)
        {
            _instanceFetcher = instanceFetcher;
            _localizer = localizer;
            _clientSettingsService = clientSettingsService;
            _databaseResolver = databaseResolver;
        }

        [HttpGet]
        [Route("/instance/{instanceName}/clientSettings/license/history")]
        public async Task<IActionResult> GetLicenseHistoryAsync(string instanceName)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var licenseHistory = await _clientSettingsService.GetLicenseHistoryAsync();
            return Ok( new { licenseHistory });
        }

        [HttpPost]
        [Route("/instance/{instanceName}/clientSettings/license/analyze")]
        public async Task<IActionResult> AnalyzeLicenseAsync(string instanceName, ClientSettings_AnalyzeLicenseOptions options)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var license = await _clientSettingsService.AnalyzeLicenseAsync(options);
            return Ok( new { license });
        }

        [HttpPut]
        [Route("/instance/{instanceName}/clientSettings/license")]
        public async Task<IActionResult> UpdateLicenseAsync(string instanceName, ClientSettings_LicenseUpdateOptions options)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var license = await _clientSettingsService.UpdateLicenseAsync(options);
            return Ok( new { license });
        }

    }
}
