using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.CoverSheetType;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverSheetTypeController : Controller
    {
        private readonly ICoverSheetTypeService _coversheetTypeService;
        private readonly IStringLocalizer<CoverSheetTypeController> _localizer;

        public CoverSheetTypeController(ICoverSheetTypeService coversheetTypeService, IStringLocalizer<CoverSheetTypeController> localizer)
        {
            _coversheetTypeService = coversheetTypeService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Coversheet Type
        /// </summary>
        /// <returns>Http Response Code with Coversheet Types</returns>
        [HttpGet]
        [Route("/coversheettype")]
        public async Task<IActionResult> GetCoverSheetTypeAsync()
        {
            var coversheetType = await _coversheetTypeService.GetAsync();
            return Ok( new { coversheetType });
        }

        /// <summary>
        /// Creates a new Coversheet Type
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/coversheettype")]
        public async Task<IActionResult> CreateCoverSheetTypeAsync(CoverSheetTypeCreateOptions options)
        {
            var result = await _coversheetTypeService.CreateAsync(options);
            return Ok( new { message = _localizer["coversheetTypeCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Coversheet Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Coversheet Type</returns>
        [HttpGet]
        [Route("/coversheettype/{id}")]
        public async Task<IActionResult> GetCoverSheetTypeAsync(int id)
        {
            var coversheetType = await _coversheetTypeService.GetAsync(id);
            return Ok( new { coversheetType });
        }

        /// <summary>
        /// Updates  a specific Coversheet Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/coversheettype/{id}")]
        public async Task<IActionResult> UpdateCoverSheetTypeAsync(int id, CoverSheetTypeUpdateOptions options)
        {
            var coversheetType = await _coversheetTypeService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["coversheetTypeUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Coversheet Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/coversheettype/{id}")]
        public async Task<IActionResult> DeleteCoverSheetTypeAsync(int id, CoverSheetTypeOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _coversheetTypeService.InActiveAsync(id);
                    break;
                case "active":
                    await _coversheetTypeService.ActiveAsync(id);
                    break;
                case "delete":
                    await _coversheetTypeService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"coversheetType-{options.ActionType.ToLower()}"].Value });
        }
    }
}
