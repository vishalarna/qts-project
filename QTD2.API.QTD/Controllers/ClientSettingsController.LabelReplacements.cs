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
        /// Gets a the general settings for this instance of QTD
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/clientSettings/labelReplacements")]
        public async Task<IActionResult> GetLabelReplacementsAsync()
        {
            var locList = await _clientSettingsService.GetLabelReplacementsAsync();
            return Ok(new { locList });
        }

        /// <summary>
        /// Updates the label replacements for this instance of QTD
        /// </summary>
        /// <returns>Http response code</returns>
        [HttpPut]
        [Route("/clientSettings/labelReplacements")]
        public async Task<IActionResult> UpdateLabelReplacementsAsync(ClientSettings_LabelReplacementUpdateOptions options)
        {
            await _clientSettingsService.UpdateLabelReplacementsAsync(options);
            return Ok();
        }
    }
}
