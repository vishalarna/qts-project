using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using QTD2.Infrastructure.Model.ClientUserSettings;

namespace QTD2.API.QTD.Controllers
{
    public partial class ClientUserSettingsController : Controller
    {
        /// <summary>
        /// Gets a the dashboard settings of the client user
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/clientUserSettings/dashboard")]
        public async Task<IActionResult> GetDashboardSettingsAsync()
        {
            var locList = await _clientUserSettingsService.GetDashboardSettingsAsync();
            return Ok(new { locList });
        }

        /// <summary>
        /// Updates the dashboard settings for the client user
        /// </summary>
        /// <returns>Http response code</returns>
        [HttpPut]
        [Route("/clientUserSettings/dashboard")]
        public async Task<IActionResult> UpdateDashboardSettingsAsync(CustomizeDashboardUpdateOptions options)
        {
            await _clientUserSettingsService.UpdateDashboardSettingsAsync(options);
            return Ok();
        }
    }
}
