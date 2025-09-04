using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.RatingScale;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingScaleController : ControllerBase
    {
        private readonly IRatingScaleService _ratingScaleService;
        private readonly IStringLocalizer<RatingScaleController> _localizer;

        public RatingScaleController(IRatingScaleService ratingScaleService, IStringLocalizer<RatingScaleController> localizer)
        {
            _ratingScaleService = ratingScaleService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Rating Scales
        /// </summary>
        /// <returns>Http Response Code with Rating Scales</returns>
        [HttpGet]
        [Route("/ratingscale")]
        public async Task<IActionResult> GetRatingScaleAsync()
        {
            var ratingScales = await _ratingScaleService.GetAsync();
            return Ok( new { ratingScales });
        }

        /// <summary>
        /// Creates a new Rating Scale
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/ratingscale")]
        public async Task<IActionResult> CreateRatingScaleAsync(RatingScaleCreateOptions options)
        {
            var result = await _ratingScaleService.CreateAsync(options);
            return Ok( new { message = _localizer["ratingScaleCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Rating Scale by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Rating Scale</returns>
        [HttpGet]
        [Route("/ratingscale/{id}")]
        public async Task<IActionResult> GetRatingScaleAsync(int id)
        {
            var ratingScale = await _ratingScaleService.GetAsync(id);
            return Ok( new { ratingScale });
        }

        /// <summary>
        /// Updates  a specific Rating Scale by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/ratingscale/{id}")]
        public async Task<IActionResult> UpdateRatingScaleAsync(int id, RatingScaleUpdateOptions options)
        {
            var ratingScale = await _ratingScaleService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["ratingScaleUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Rating Scale by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/ratingscale/{id}")]
        public async Task<IActionResult> DeleteRatingScaleAsync(int id, RatingScaleOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _ratingScaleService.InActiveAsync(id);
                    break;
                case "active":
                    await _ratingScaleService.ActiveAsync(id);
                    break;
                case "delete":
                    await _ratingScaleService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"ratingScale-{options.ActionType.ToLower()}"].Value });
        }
    }
}
