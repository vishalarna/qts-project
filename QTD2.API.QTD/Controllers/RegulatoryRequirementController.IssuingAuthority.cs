using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.RR_IssuingAuthority;

namespace QTD2.API.QTD.Controllers
{
    public partial class RegulatoryRequirementController : ControllerBase
    {
        /// <summary>
        /// Gets a list of RR_IssuingAuthorities
        /// </summary>
        /// <returns>Http Response Code with RR_IssuingAuthorities</returns>
        [HttpGet]
        [Route("/rr/issuingauthority")]
        public async Task<IActionResult> GetRR_IssuingAuthoritiesAsync()
        {
            var result = await _rr_IssuingAuthorityService.GetAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/rr/issuingauthority/withoutInclude")]
        public async Task<IActionResult> GetComapnctRR_IAWithoutIncludesAsync()
        {
            var result = await _rr_IssuingAuthorityService.GetComapnctRR_IAWithoutIncludesAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new RR_IssuingAuthority
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/rr/issuingauthority")]
        public async Task<IActionResult> CreateRR_IssuingAuthorityAsync(RR_IssuingAuthorityCreateOptions options)
        {
            var result = await _rr_IssuingAuthorityService.CreateAsync(options);
            return Ok( new { result, message = _localizer["RR_IssuingAuthorityCreated"].Value });
        }

        /// <summary>
        /// Gets a specific RR_IssuingAuthority by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with RR_IssuingAuthority</returns>
        [HttpGet]
        [Route("/rr/issuingauthority/{id}")]
        public async Task<IActionResult> GetRR_IssuingAuthorityAsync(int id)
        {
            var result = await _rr_IssuingAuthorityService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All issuing authorities with their Regulatory requirements
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/issuingauthority/include")]
        public async Task<IActionResult> GetRR_IssuingAuthoritiesWithRR()
        {
            var result = await _rr_IssuingAuthorityService.getWithRR();
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific RR_IssuingAuthority by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/rr/issuingauthority/{id}")]
        public async Task<IActionResult> UpdateRR_IssuingAuthorityAsync(int id, RR_IssuingAuthorityCreateOptions options)
        {
            var result = await _rr_IssuingAuthorityService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["RR_IssuingAuthorityUpdated"].Value, result });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific RR_IssuingAuthority by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/rr/issuingauthority/{id}")]
        public async Task<IActionResult> DeleteRR_IssuingAuthorityAsync(int id, RR_IssuingAuthorityOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _rr_IssuingAuthorityService.InActiveAsync(id);
                    break;
                case "active":
                    await _rr_IssuingAuthorityService.ActiveAsync(id);
                    break;
                case "delete":
                    await _rr_IssuingAuthorityService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"RR_IssuingAuthority-{options.ActionType.ToLower()}"].Value });
        }
    }
}
