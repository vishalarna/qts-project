using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.CertifyingBody;
using QTD2.Infrastructure.Model.CertifyingBody_History;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CertifyingBodiesController : ControllerBase
    {
        private readonly Application.Interfaces.Services.Shared.ICertifyingBodiesService _certifyingBodiesService;
        private readonly IStringLocalizer<CertifyingBodiesController> _localizer;

        public CertifyingBodiesController(Application.Interfaces.Services.Shared.ICertifyingBodiesService certifyingBodiesService, IStringLocalizer<CertifyingBodiesController> localizer)
        {
            _certifyingBodiesService = certifyingBodiesService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Certifying Bodies
        /// </summary>
        /// <returns>Http Response Code with certifyingBodies</returns>
        [HttpGet]
        [Route("/certifyingBodies")]
        public async Task<IActionResult> GetCertifyingBodiesAsync()
        {
            var certifyingBodies = await _certifyingBodiesService.GetAsync();
            return Ok( new { certifyingBodies });
        }

        /// <summary>
        /// Creates a new certifying body
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/certifyingBodies")]
        public async Task<IActionResult> CreateCertifyingBodyAsync(CertifyingBodyCreateOptions options)
        {
            var result = await _certifyingBodiesService.CreateAsync(options);
            return Ok( new { result, message = _localizer["CertBodyCreated"] });
        }

        /// <summary>
        /// Gets a specific certification provider by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with certifying body</returns>
        [HttpGet]
        [Route("/certifyingBodies/{id}")]
        public async Task<IActionResult> GetCertifyingBodyAsync(int id)
        {
            var certifyingBody = await _certifyingBodiesService.GetAsync(id);
            return Ok( new { certifyingBody });
        }

        [HttpGet]
        [Route("/certifyingBodies/{id}/withoutInclude")]
        public async Task<IActionResult> GetCertifyingBodyWithoutIncludeAsync(int id)
        {
            var result = await _certifyingBodiesService.GetWithoutIncludeAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific certification provider by name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/certifyingBodies/{id}")]
        public async Task<IActionResult> UpdateCertifyingBodyAsync(int id, CertifyingBodyCreateOptions options)
        {
            await _certifyingBodiesService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["certBodyUpdated"] });
        }

        /// <summary>
        /// Delets  a specific certification provider by name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/certifyingBodies/{id}")]
        public async Task<IActionResult> DeleteCertifyingBodyAsync(int id, CertifyingBody_HistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower().Trim())
            {
                case "active":
                    await _certifyingBodiesService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _certifyingBodiesService.InActiveAsync(id);
                    break;
                case "delete":
                    await _certifyingBodiesService.DeleteAsync(id);
                    break;
            }

            //await _certifyingBodiesService.DeleteAsync(id);
            return Ok( new { message = _localizer["Issuing Authority -" + options.ActionType.ToLower()] });
        }

        [HttpGet]
        [Route("/certifyingBodies/{id}/isEmployeeCertification")]
        public async Task<IActionResult> IsEmployeeCertification(int id)
        {
            var result = await _certifyingBodiesService.IsEmployeeCertification(id);
            return Ok( new { result,message = _localizer["EmployeeCertifications"] });
        }

        [HttpGet]
        [Route("/certifyingBodies/{ilaId}/certifying/{isLevelEditing}")]
        public async Task<IActionResult> GetCertifyingBodiesByLevelEditingAsync(int ilaId, bool isLevelEditing = true)
        {
            var result = await _certifyingBodiesService.GetCertifyingBodiesByLevelEditingAsync(isLevelEditing, ilaId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/certifyingBodies/subrequirements/{isLevelEditing}")]
        public async Task<IActionResult> GetCertifyingBodiesWithSubRequirementsAsync(bool isLevelEditing = true)
        {
            var result = await _certifyingBodiesService.GetCertifyingBodiesWithSubRequirementsAsync(isLevelEditing);
            return Ok(new { subReqs = result });
        }

    }
}
