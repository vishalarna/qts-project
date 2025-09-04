using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingScaleNController : ControllerBase
    {
        private readonly IRatingScaleNService _ratingScaleService;
        private readonly IStringLocalizer<RatingScaleNController> _localizer;

        public RatingScaleNController(IRatingScaleNService ratingScaleService, IStringLocalizer<RatingScaleNController> localizer)
        {
            _ratingScaleService = ratingScaleService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Rating Scales
        /// </summary>
        /// <returns>Http Response Code with Rating Scales</returns>
        [HttpGet]
        [Route("/ratingscalen")]
        public async Task<IActionResult> GetRatingScaleNAsync()
        {
            var ratingScales = await _ratingScaleService.GetAsync();
            return Ok(new { ratingScales });
        }
    }
}
