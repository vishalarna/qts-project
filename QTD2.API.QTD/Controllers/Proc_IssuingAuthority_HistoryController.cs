using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.IssuingAuthorityStatusHistory;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class Proc_IssuingAuthority_HistoryController : ControllerBase
    {
        private readonly IProc_IssuingAuthority_HistoryService _issuAuthStatusHistoryService;
        private readonly IStringLocalizer<Proc_IssuingAuthority_HistoryController> _localizer;

        public Proc_IssuingAuthority_HistoryController(IProc_IssuingAuthority_HistoryService issuAuthStatusHistoryService, IStringLocalizer<Proc_IssuingAuthority_HistoryController> localizer)
        {
            _issuAuthStatusHistoryService = issuAuthStatusHistoryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Get all Issuing Authority Status Histories
        /// </summary>
        /// <returns> Http response code with list of Issuing authority status histories </returns>
        [HttpGet]
        [Route("/procIssuAuthStatusHistory")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _issuAuthStatusHistoryService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get specific Issuing authority status history by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Http response code with Issuing authority status history data </returns>
        [HttpGet]
        [Route("/procIssuAuthStatusHistory/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _issuAuthStatusHistoryService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Create Issuing authority status history
        /// </summary>
        /// <param name="options"></param>
        /// <returns> Http Response code along with data of created Issuing authority </returns>
        [HttpPost]
        [Route("/procIssuAuthStatusHistory")]
        public async Task<IActionResult> Create(Proc_IssuingAuthority_HistoryCreateOptions options)
        {
            var result = await _issuAuthStatusHistoryService.CreateAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Update The specific issuing authority status history by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns> Http response code along with updated data </returns>
        [HttpPut]
        [Route("/procIssuAuthStatusHistory/{id}")]
        public async Task<IActionResult> Update(int id, Proc_IssuingAuthority_HistoryCreateOptions options)
        {
            var result = await _issuAuthStatusHistoryService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Delete the specific Issuing authority history
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns> Http response code along with delete option information message </returns>
        [HttpDelete]
        [Route("/procIssuAuthStatusHistory/{id}")]
        public async Task<IActionResult> Delete(int id, Proc_IssuingAuthority_HistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _issuAuthStatusHistoryService.DeactivateAsync(id);
                    break;
                case "active":
                    await _issuAuthStatusHistoryService.ActiveAsync(id);
                    break;
                case "delete":
                    await _issuAuthStatusHistoryService.DeleteAsync(id);
                    break;
            }
            return Ok( new { message = _localizer[$"IssuingAuthorityStatusHistory-{options.ActionType.ToLower()}"].Value });
        }
    }
}
