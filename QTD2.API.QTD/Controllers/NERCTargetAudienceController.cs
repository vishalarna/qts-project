using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.NERCTargetAudience;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NERCTargetAudienceController : ControllerBase
    {
        private readonly INERCTargetAudienceService _nercTargetAudienceService;
        private readonly IStringLocalizer<NERCTargetAudienceController> _localizer;

        public NERCTargetAudienceController(INERCTargetAudienceService nercTargetAudienceService, IStringLocalizer<NERCTargetAudienceController> localizer)
        {
            _nercTargetAudienceService = nercTargetAudienceService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of NERCTargetAudience
        /// </summary>
        /// <returns>Http Response Code with NERCTargetAudience</returns>
        [HttpGet]
        [Route("/nercTargetAudience")]
        public async Task<IActionResult> GetNERCTargetAudienceAsync()
        {
            var result = await _nercTargetAudienceService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new NERCTargetAudience
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/nercTargetAudience")]
        public async Task<IActionResult> CreateNERCTargetAudienceAsync(NERCTargetAudienceCreateOptions options)
        {
            var result = await _nercTargetAudienceService.CreateAsync(options);
            return Ok( new { message = _localizer["NERCTargetAudienceCreated"].Value });
        }

        /// <summary>
        /// Gets a specific NERCTargetAudience by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with NERCTargetAudience</returns>
        [HttpGet]
        [Route("/nercTargetAudience/{id}")]
        public async Task<IActionResult> GetNERCTargetAudienceAsync(int id)
        {
            var result = await _nercTargetAudienceService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific NERCTargetAudience by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/nercTargetAudience/{id}")]
        public async Task<IActionResult> UpdateNERCTargetAudienceAsync(int id, NERCTargetAudienceUpdateOptions options)
        {
            var result = await _nercTargetAudienceService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["NERCTargetAudienceUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific NERCTargetAudience by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/nercTargetAudience/{id}")]
        public async Task<IActionResult> DeleteNERCTargetAudienceAsync(int id, NERCTargetAudienceOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _nercTargetAudienceService.InActiveAsync(id);
                    break;
                case "active":
                    await _nercTargetAudienceService.ActiveAsync(id);
                    break;
                case "delete":
                    await _nercTargetAudienceService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"NERCTargetAudienceService-{options.ActionType.ToLower()}"].Value });
        }
    }
}
