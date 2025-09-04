
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.NercStandard;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class NercStandardController : ControllerBase
    {
        private readonly INercStandardService _nercStandardService;
        private readonly INercStandardMemberService _nercStandardMemberService;
        private readonly IStringLocalizer<NercStandardController> _localizer;

        public NercStandardController(INercStandardService nercStandardService, IStringLocalizer<NercStandardController> localizer, INercStandardMemberService nercStandardMemberService)
        {
            _nercStandardService = nercStandardService;
            _localizer = localizer;
            _nercStandardMemberService = nercStandardMemberService;
        }

        /// <summary>
        /// Gets a list of NercStandards
        /// </summary>
        /// <returns>Http Response Code with NercStandards</returns>
        [HttpGet]
        [Route("/nercStandards")]
        public async Task<IActionResult> GetNercStandardsAsync()
        {
            var result = await _nercStandardService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new NercStandard
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/nercStandards")]
        public async Task<IActionResult> CreateNercStandardAsync(NercStandardCreateOptions options)
        {
            var result = await _nercStandardService.CreateAsync(options);
            return Ok( new { message = _localizer["NercStandardCreated"].Value });
        }

        /// <summary>
        /// Gets a specific NercStandard by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with NercStandard</returns>
        [HttpGet]
        [Route("/nercStandards/{id}")]
        public async Task<IActionResult> GetNercStandardAsync(int id)
        {
            var result = await _nercStandardService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific NercStandard by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/nercStandards/{id}")]
        public async Task<IActionResult> UpdateNercStandardAsync(int id, NercStandardUpdateOptions options)
        {
            var result = await _nercStandardService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["NercStandardUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific NercStandard by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/nercStandards/{id}")]
        public async Task<IActionResult> DeleteNercStandardAsync(int id, NercStandardOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _nercStandardService.InActiveAsync(id);
                    break;
                case "active":
                    await _nercStandardService.ActiveAsync(id);
                    break;
                case "delete":
                    await _nercStandardService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"NercStandard-{options.ActionType.ToLower()}"].Value });
        }
    }
}
