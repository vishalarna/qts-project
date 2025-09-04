using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Coversheet;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoversheetController : Controller
    {
        private readonly ICoversheetService _coversheetService;
        private readonly IStringLocalizer<CoversheetController> _localizer;

        public CoversheetController(ICoversheetService coversheetService, IStringLocalizer<CoversheetController> localizer)
        {
            _coversheetService = coversheetService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Coversheet
        /// </summary>
        /// <returns>Http Response Code with Coversheets</returns>
        [HttpGet]
        [Route("/coversheet")]
        public async Task<IActionResult> GetCoversheetAsync()
        {
            var coversheet = await _coversheetService.GetAsync();
            return Ok( new { coversheet });
        }

        /// <summary>
        /// Creates a new Coversheet
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/coversheet")]
        public async Task<IActionResult> CreatecoversheetAsync(CoversheetCreateOptions options)
        {
            var result = await _coversheetService.CreateAsync(options);
            return Ok( new { message = _localizer["coversheetCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Coversheet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Coversheet</returns>
        [HttpGet]
        [Route("/coversheet/{id}")]
        public async Task<IActionResult> GetCoversheetAsync(int id)
        {
            var coversheet = await _coversheetService.GetAsync(id);
            return Ok( new { coversheet });
        }

        /// <summary>
        /// Updates  a specific Coversheet by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/coversheet/{id}")]
        public async Task<IActionResult> UpdateCoversheetAsync(int id, CoversheetUpdateOptions options)
        {
            var coversheet = await _coversheetService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["coversheetUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Coversheet by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/coversheet/{id}")]
        public async Task<IActionResult> DeleteCoversheetAsync(int id, CoversheetOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _coversheetService.InActiveAsync(id);
                    break;
                case "active":
                    await _coversheetService.ActiveAsync(id);
                    break;
                case "delete":
                    await _coversheetService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"coversheet-{options.ActionType.ToLower()}"].Value });
        }
    }
}
