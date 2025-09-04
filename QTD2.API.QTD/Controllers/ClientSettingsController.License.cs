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
        /// Gets a the license for this instance of QTD
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/clientSettings/license")]
        public async Task<IActionResult> GetCurrentLicenseAsync()
        {
            var locList = await _clientSettingsService.GetCurrentLicenseAsync();
            return Ok( new { locList });
        }

        [HttpGet]
        [Route("/clientSettings/licenseVM")]
        public async Task<IActionResult> GetCurrentLicenseVMAsync()
        {
            var locList = await _clientSettingsService.GetCurrentLicenseVMAsync();
            return Ok( new { locList });
        }

        /// <summary>
        /// Updates the label replacements for this instance of QTD
        /// </summary>
        /// <returns>Http response code</returns>
        [HttpPut]
        [Route("/clientSettings/license")]
        public async Task<IActionResult> UpdateLicenseAsync(ClientSettings_LicenseUpdateOptions options)
        {
            var license = await _clientSettingsService.UpdateLicenseAsync(options);
            return Ok( new { license });
        }
       
    }
}
