using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QTD2.Infrastructure.Model.ClientSettings;

namespace QTD2.API.QTD.Controllers
{
    public partial class ClientSettingsController
    {
        /// <summary>
        /// Gets a list of notification settings for a specific client
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/clientSettings/notifications")]
        public async Task<IActionResult> GetNotificationsAsync()
        {
            var locList = await _clientSettingsService.GetNotificationsAsync();
            return Ok( new { locList });
        }

        /// <summary>
        /// Gets a notification by name for a specific client
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/clientSettings/notifications/{name}")]
        public async Task<IActionResult> GetNotificationByNameAsync(string name)
        {
            var locList = await _clientSettingsService.GetNotificationByNameAsync(name);
            return Ok( new { locList });
        }

        /// <summary>
        /// Updates the specific notification setting for a specific client
        /// </summary>
        /// <returns>Http response code</returns>
        [HttpPut]
        [Route("/clientSettings/notifications/{notification}")]
        public async Task<IActionResult> PutNotificationAsync(string notification, ClientSettings_NotificationUpdateOptions options )
        {
            await _clientSettingsService.UpdateNotificationAsync(notification, options);
            return Ok();
        }
    }
}
