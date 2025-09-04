using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Jobs.Notifications;
using QTD2.Infrastructure.Model.ClientSettings;

namespace QTD2.API.QTD.Controllers
{
    public partial class ClientSettingsController
    {
        /// <summary>
        /// Gets a the general settings for this instance of QTD
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/clientSettings/general")]
        public async Task<IActionResult> GetGeneralSettingssAsync()
        {
            var locList = await _clientSettingsService.GetGeneralSettingsAsync();
            return Ok( new { locList });
        }
            
        /// <summary>
        /// Updates the general settings for this instance of QTD
        /// </summary>
        /// <returns>Http response code</returns>
        [HttpPut]
        [Route("/clientSettings/general")]
        public async Task<IActionResult> UpdateGeneralSettingsAsync(ClientSettings_GeneralSettingsUpdateOptions options)
        {
            await _clientSettingsService.UpdateGeneralSettingsAsync(options);
            return Ok();
        }

        [HttpGet]
        [Route("/clientSettings/general/timezones")]
        public  IActionResult GetAllTimeZonesAsync()
        {
            var timeZones =  _clientSettingsService.GetAllTimeZonesAsync();
            return Ok( new { timeZones });
        }
    }
}
