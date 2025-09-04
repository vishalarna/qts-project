using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.IssuingAuthorityStatusHistory;
using QTD2.Infrastructure.Model.Procedure_IssuingAuthority;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuingAuthoritiesController : ControllerBase
    {
        private readonly IProcedureService _procedureService;
        private readonly IProc_IssuingAuthority_HistoryService _proc_IssuingAuthorityHistoryService;
        private readonly IStringLocalizer<IssuingAuthoritiesController> _localizer;

        public IssuingAuthoritiesController(IProcedureService procedureService, IStringLocalizer<IssuingAuthoritiesController> localizer, IProc_IssuingAuthority_HistoryService proc_IssuingAuthorityHistoryService)
        {
            _procedureService = procedureService;
            _localizer = localizer;
            _proc_IssuingAuthorityHistoryService = proc_IssuingAuthorityHistoryService;
        }

        /// <summary>
        /// Gets a list of issuing authorities
        /// </summary>
        /// <returns>Http response code with message</returns>
        [HttpGet]
        [Route("/issuingAuthorities")]
        public async Task<IActionResult> GetAsync()
        {
            var iaList = await _procedureService.GetProcedure_IssuingAuthoritiesAsync();
            return Ok( new { iaList });
        }

        /// <summary>
        /// Get data for single Issuing Authority
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/issuingAuthorities/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ia = await _procedureService.GetProcedure_IssuingAuthorityAsync(id);
            return Ok( new { ia });
        }

        /// <summary>
        /// Creates an issuing authority
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpPost]
        [Route("/issuingAuthorities")]
        public async Task<IActionResult> CreateAsync(Procedure_IssuingAuthorityCreateOptions options)
        {
            var ia = await _procedureService.CreateProcedure_IssuingAuthorityAsync(options);
            return Ok( new { ia, message = _localizer["IACreated"] });
        }

        /// <summary>
        /// Update the data for given Procedure Issuing Authority
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/issuingAuthorities/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Procedure_IssuingAuthorityCreateOptions options)
        {
            var result = await _procedureService.UpdateProcedure_IssuingAuthorityAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Change the given Procedure Issuing Authority to deleted, inactive or active
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/issuingAuthorities/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, Procedure_IssuingAuthorityOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _procedureService.ActiveProcedure_IssuingAuthorityAsync(id);
                    break;
                case "inactive":
                    await _procedureService.InActiveProcedure_IssuingAuthorityAsync(id);
                    break;
                default:
                    await _procedureService.DeleteProcedure_IssuingAuthorityAsync(id, options);
                    break;
            }

            await _proc_IssuingAuthorityHistoryService.CreateAsync(new Proc_IssuingAuthority_HistoryCreateOptions { ChangeNotes = options.ChangeNotes, ChangeEffectiveDate = options.ChangeEffectiveDate, IssuingAuthorityId = id, NewStatus = true, OldStatus = false });
            return Ok( new { message = _localizer["Procedure_IssuingAuthority-" + options.ActionType] });
        }
    }
}
