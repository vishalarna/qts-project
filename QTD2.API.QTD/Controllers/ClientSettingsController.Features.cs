using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ClientSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{    
    public partial  class ClientSettingsController
    {

        [HttpGet]
        [Route("/clientSettings/features")]
        public async Task<IActionResult> GetAllFeaturesAsync()
        {
            var featureList = await _clientSettingsService.GetAllFeaturesAsync();
            return Ok( new { featureList });
        }

        [HttpPut]
        [Route("/clientSettings/features")]
        public async Task<IActionResult>UpdateFeatureAsync(ClientSettings_FeatureUpdateOptions options)
        {
            await _clientSettingsService.UpdateFeaturesAsync(options);
            return Ok();
        }
    }
}
