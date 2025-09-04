using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.Certification_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class CertificationsController
    {
        [HttpGet]
        [Route("/certifications/history/latest/{getLatest}")]
        public async Task<IActionResult> GetAllCertHistories(bool getLatest)
        {
            var history = await _certificationHistoryService.GetHistoryAsync(getLatest);
            return Ok( new { history });
        }

        [HttpGet]
        [Route("/certifications/history/{id}")]
        public async Task<IActionResult> GetCertHistory(int id)
        {
            var result = await _certificationHistoryService.GetCertCatHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/certifications/history")]
        public async Task<IActionResult> CreateCertHistory(Certification_HistoryCreateOptions options)
        {
            var result = await _certificationHistoryService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/certifications/history/{id}")]
        public async Task<IActionResult> UpdateLocHistory(int id, Certification_HistoryCreateOptions options)
        {
            var result = await _certificationHistoryService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/certifications/history/{id}")]
        public async Task<IActionResult> DeleteCertHistory(int id, Certification_HistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _certificationHistoryService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _certificationHistoryService.InActiveAsync(id);
                    break;
                case "delete":
                    await _certificationHistoryService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"Certification_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
