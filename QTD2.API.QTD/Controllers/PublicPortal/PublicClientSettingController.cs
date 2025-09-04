using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Database.Interfaces;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.PublicPortal
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PublicClientSettingController : ControllerBase
    {

        private readonly IDatabaseResolver _databaseResolver;
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IClientSettingsService _clientSettingsService;
        public PublicClientSettingController(IDatabaseResolver databaseResolver, IInstanceFetcher instanceFetcher, IClientSettingsService clientSettingsService) 
        {
            _databaseResolver = databaseResolver;
            _instanceFetcher = instanceFetcher;
            _clientSettingsService = clientSettingsService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/public/{instanceName}/clientSetting/feature")]
        public async Task<IActionResult> GetPublicClassFeaturesAsync(string instanceName)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var publicClassFeature = await _clientSettingsService.GetPublicClassFeaturesAsync();
            return Ok(new { publicClassFeature });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/public/{instanceName}/clientSetting/companyLogo")]
        public async Task<IActionResult> GetPublicClassCompanyLogoAsync(string instanceName)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var generalSetting = await _clientSettingsService.GetGeneralSettingsAsync();
            return Ok(new { generalSetting.CompanyName, generalSetting.CompanyLogo });
        }
    }
}
