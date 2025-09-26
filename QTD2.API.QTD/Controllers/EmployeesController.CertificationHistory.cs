using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EmployeeCertification;
using QTD2.Infrastructure.Model.EmployeeCertificationHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Create a new RR_Status History
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/employees/history")]
        public async Task<IActionResult> Create(EmployeeCertificationHistoryCreateOptions options)
        {
            var result = await _empCertification_historyService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/employees/certifications/{id}/history")]
        public async Task<IActionResult> UpdateAsync(int id,EmployeeCertificateUpdateOptions options)
        {
            await _empCertification_historyService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["CertificationHistoryUpdated"] });
        }

        [HttpDelete]
        [Route("/employees/certifications/{certLinkId}/history")]
        public async Task<IActionResult> DeleteAsync(int certLinkId)
        {
            await _empCertification_historyService.DeleteHistAsync(certLinkId);
            return Ok( new { message = _localizer["CertificationHistoryDeleted"] });
        }

        [HttpGet]
        [Route("/employees/certifications/history/{empCertId}")]
        public async Task<IActionResult> GetEmployeeCertificationHistory(int empCertId)
        {
            var result = await _empCertification_historyService.GetCertificationWithEmpCertificationHistory(empCertId);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/employees/certifications/history/bulk")]
        public async Task<IActionResult> DeleteBulkCertificationHistoryAsync(EmployeeCertificationHistoryDeleteOptions options)
        {
            await _empCertification_historyService.DeleteBulkHistoryAsync(options);
            return Ok(new { message = _localizer["CertificationHistoryDeleted"] });
        }
    }
}
