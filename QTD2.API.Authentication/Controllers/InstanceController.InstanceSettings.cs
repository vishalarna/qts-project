using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using QTD2.Application.Authorization;
using Microsoft.AspNetCore.Cors;
using QTD2.Infrastructure.Model.Instance;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Database.Interfaces;

namespace QTD2.API.Authentication.Controllers
{
    public partial class InstanceController
    {
        /// <summary>
        /// Gets an instance by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Http Response Code with data</returns>
        [HttpGet]
        [EnableCors("AllowQTD")]
        [Authorize(Policy = Policies.QtdSystem)]
        [Route("/instances/{name}/settings")]

        public async Task<IActionResult> GetInstanceSettingsAsync(string name)
        {
            var settings = await _instanceService.GetInstanceSettingsAsync(name);

            return Ok(settings);
        }

        [HttpGet]
        [EnableCors("AllowQTD")]
        [Authorize(Policy = Policies.QtdSystem)]
        [Route("/instances/settings")]
        public async Task<IActionResult> GetAllInstanceSettingsAsync()
        {
            var locList = await _instanceService.GetAllInstanceSettingAsync();

            return Ok(new { locList });
        }

        [HttpGet]
        [EnableCors("AllowQTD")]
        [Authorize(Policy = Policies.QtdSystem)]
        [Route("/instances/settings/active")]
        public async Task<IActionResult> GetAllActiveInstanceSettingsAsync()
        {
            var locList = await _instanceService.GetAllActiveInstanceSettingAsync();

            return Ok(new { locList });
        }

        [HttpGet]
        [EnableCors("AllowQTD")]
        [Authorize(Policy = Policies.QtdSystem)]
        [Route("/instances/scorm/{tenantName}/settings")]
        public async Task<ActionResult> GetInstanceSettingsByScormTenant(string tenantName)
        {
            var settings = await _instanceService.GetInstanceSettingsByScormTenantAsync(tenantName);
            return Ok(settings);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/instances/{name}/settings/publicUrl")]

        public async Task<IActionResult> GetInstanceSettingsPublicUrlAsync(string name)
        {
            var settings = await _instanceService.GetInstanceSettingsAsync(name);
            return settings == null ? NotFound() : Ok(new { publicUrl = settings.PublicUrl });
        }

        [HttpPut]
        [Route("/instances/{name}/settings/publicUrl")]
        public async Task<IActionResult> UpdateInstanceSettingsPublicUrlAsync(string name, PublicInstanceSettingUpdatOptions options)
        {
            var settings = await _instanceService.UpdateInstanceSettingAsync(name, options);
            return Ok(new { settings });
        }
                
    }
}
