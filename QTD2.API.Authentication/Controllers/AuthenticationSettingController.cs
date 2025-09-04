using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Authentication;
using System.Threading.Tasks;

namespace QTD2.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationSettingController : ControllerBase
    {
        private readonly IStringLocalizer<AuthenticationSettingController> _localization;
        private readonly IAuthenticationSettingService _authenticationSettingService;
        public AuthenticationSettingController(IStringLocalizer<AuthenticationSettingController> localization, IAuthenticationSettingService authenticationSettingService)
        {
            _localization = localization;
            _authenticationSettingService = authenticationSettingService;
        }

        /// <summary>
        /// Gets a list of AuthenticationSetting
        /// </summary>
        /// <returns>Http Response Code with data</returns>
        [HttpGet]
        [Route("/authenticationsettings")]
        public async Task<IActionResult> GetAsync()
        {
            var authenticationSetting = await _authenticationSettingService.GetAsync();
            return Ok(new { authenticationSetting });
        }
    }
}
