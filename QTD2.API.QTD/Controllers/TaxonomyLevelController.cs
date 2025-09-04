using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TaxonomyLevel;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxonomyLevelController : ControllerBase
    {
        private readonly ITaxonomyLevelService _taxonomyLevelService;
        private readonly IStringLocalizer<TaxonomyLevelController> _localizer;

        public TaxonomyLevelController(ITaxonomyLevelService taxonomyLevelService, IStringLocalizer<TaxonomyLevelController> localizer)
        {
            _taxonomyLevelService = taxonomyLevelService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of TaxonomyLevels
        /// </summary>
        /// <returns>Http Response Code with TaxonomyLevels</returns>
        [HttpGet]
        [Route("/taxonomylevel")]
        public async Task<IActionResult> GetTaxonomyLevelsAsync()
        {
            var result = await _taxonomyLevelService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new TaxonomyLevel
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/taxonomylevel")]
        public async Task<IActionResult> CreateTaxonomyLevelAsync(TaxonomyLevelCreateOptions options)
        {
            var result = await _taxonomyLevelService.CreateAsync(options);
            return Ok( new { message = _localizer["TaxonomyLevelCreated"].Value });
        }

        /// <summary>
        /// Gets a specific TaxonomyLevel by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TaxonomyLevel</returns>
        [HttpGet]
        [Route("/taxonomylevel/{id}")]
        public async Task<IActionResult> GetTaxonomyLevelAsync(int id)
        {
            var result = await _taxonomyLevelService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TaxonomyLevel by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/taxonomylevel/{id}")]
        public async Task<IActionResult> UpdateTaxonomyLevelAsync(int id, TaxonomyLevelUpdateOptions options)
        {
            var result = await _taxonomyLevelService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["TaxonomyLevelUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TaxonomyLevel by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/taxonomylevel/{id}")]
        public async Task<IActionResult> DeleteTaxonomyLevelAsync(int id, TaxonomyLevelOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _taxonomyLevelService.InActiveAsync(id);
                    break;
                case "active":
                    await _taxonomyLevelService.ActiveAsync(id);
                    break;
                case "delete":
                    await _taxonomyLevelService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TaxonomyLevel-{options.ActionType.ToLower()}"].Value });
        }
    }
}
