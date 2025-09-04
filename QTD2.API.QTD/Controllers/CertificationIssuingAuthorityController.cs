using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Infrastructure.Model.CertificationIssuingAuthority;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationIssuingAuthorityController : ControllerBase
    {
        private readonly Application.Interfaces.Services.Shared.ICertificationIssuingAuthorityService _certIAService;
        private readonly IStringLocalizer<CertificationIssuingAuthorityController> _localizer;

        public CertificationIssuingAuthorityController(Application.Interfaces.Services.Shared.ICertificationIssuingAuthorityService certIAService, IStringLocalizer<CertificationIssuingAuthorityController> localizer)
        {
            _certIAService = certIAService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Certifying Bodies
        /// </summary>
        /// <returns>Http Response Code with certifyingBodies</returns>
        [HttpGet]
        [Route("/certificationIssuingAuthority")]
        public async Task<IActionResult> GetCertificationIssuingAuthorityAsync()
        {
            var result = await _certIAService.GetAsync();
            return Ok(new { result });
        }

        /// <summary>
        /// Creates a new certifying body
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/certificationIssuingAuthority")]
        public async Task<IActionResult> CreateCertificationIssuingAuthorityAsync(CertificationIssuingAuthorityCreateOptions options)
        {
            await _certIAService.CreateAsync(options);
            return Ok(new { message = _localizer["CertIACreated"] });
        }

        /// <summary>
        /// Gets a specific certification provider by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with certifying body</returns>
        [HttpGet]
        [Route("/certificationIssuingAuthority/{id}")]
        public async Task<IActionResult> GetCertificationIssuingAuthorityAsync(int id)
        {
            var result = await _certIAService.GetAsync(id);
            return Ok(new { result });
        }

        /// <summary>
        /// Updates  a specific certification provider by name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/certificationIssuingAuthority/{id}")]
        public async Task<IActionResult> UpdateCertificationIssuingAuthorityAsync(int id, CertificationIssuingAuthorityCreateOptions options)
        {
            await _certIAService.UpdateAsync(id, options);
            return Ok(new { message = _localizer["certIAUpdated"] });
        }

        /// <summary>
        /// Delets  a specific certification provider by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/certificationIssuingAuthority/{id}")]
        public async Task<IActionResult> DeleteCertificationIssuingAuthorityAsync(int id)
        {
            await _certIAService.DeleteAsync(id);
            return Ok(new { message = _localizer["certIADeleted"] });
        }

    }
}
