using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Certification;

namespace QTD2.API.QTD.Controllers
{
    public partial class CertifyingBodiesController : ControllerBase
    {
        /// <summary>
        /// Gets a list of certificates provided by the ceritifcation provider
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with certifications data</returns>
        [HttpGet]
        [Route("/certifyingBodies/{id}/certifications")]
        public async Task<IActionResult> GetCertificationsAsync(int id)
        {
            var certifications = await _certifyingBodiesService.GetCertificationsAsync(id);
            return Ok( new { certifications });
        }

        /// <summary>
        /// Gets a specific certificate from a provider by name
        /// </summary>
        /// <param name="certifyingBodyId"></param>
        /// <param name="certificationId"></param>
        /// <returns>Http Response Code with certification data</returns>
        [HttpGet]
        [Route("/certifyingBodies/{certifyingBodyId}/certifications/{certificationId}")]
        public async Task<IActionResult> GetCertificationAsync(int certifyingBodyId, int certificationId)
        {
            var certification = await _certifyingBodiesService.GetCertificationAsync(certificationId);
            return Ok( new { certification });
        }

        /// <summary>
        /// Adds a new certification to a certifying body
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http Response Code with data</returns>
        [HttpPost]
        [Route("/certifyingBodies/{id}/certifications")]
        public async Task<IActionResult> CreateCertificationAsync(int id, CertificationCreateOptions options)
        {
            var certification = await _certifyingBodiesService.CreateCertificationAsync(id, options);
            return Ok( new { certification, message = _localizer["CertCreated"] });
        }

        /// <summary>
        /// Updates a specifci certificate from a provider by name
        /// </summary>
        /// <param name="certifyingBodyId"></param>
        /// <param name="certificationId"></param>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPut]
        [Route("/certifyingBodies/{certifyingBodyId}/certifications/{certificationId}")]
        public async Task<IActionResult> UpdateCertificationAsync(int certifyingBodyId, int certificationId, CertificateUpdateOptions options)
        {
            await _certifyingBodiesService.UpdateCertificationAsync(certificationId, options);
            return Ok( new { message = _localizer["CertUpdated"] });
        }

        /// <summary>
        /// Adds a new certificate to a certificate provider
        /// </summary>
        /// <param name="certifyingBodyId"></param>
        /// <param name="certificationId"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpDelete]
        [Route("/certifyingBodies/{id}/certifications/{certificationName}")]
        public async Task<IActionResult> DeleteCertificationAsync(int certifyingBodyId, int certificationId)
        {
            await _certifyingBodiesService.DeleteCertificationAsync(certificationId);
            return Ok( new { message = _localizer["CertDeleted"] });
        }
        
    }
}
