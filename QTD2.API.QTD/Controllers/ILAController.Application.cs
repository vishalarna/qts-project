using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Reports;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the AssessmentTool with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/application")]
        public async Task<IActionResult> LinkApplicationAsync(IlaApplicationOptions options)
        {
            var result = await _ilaService.LinkApplicationAsync(options.ilaId, options);
            return Ok( new { result });
        }



        /// <summary>
        /// Get the AssessmentTools linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked AssessmentTools</returns>
        [HttpGet]
        [Route("/ila/{id}/application")]
        public async Task<IActionResult> GetApplicationDetailsAsync(int id)
        {
            var result = await _ilaService.GetApplicationDetailsAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{id}/preview")]
        public async Task<IActionResult> GetILAPreviewDetailsAsync(int id)
        {
            var result = await _ilaService.GetILAPreviewDetailsAsync(id);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/ila/{id}/nerccertdetail")]
        public async Task<IActionResult> GetILANERCCertificationDetailsAsync(int id)
        {
            var result = await _ilaService.GetILANERCCertificationDetailsAsync(id);
            return Ok(new { result });
        }

        [HttpPost]
        [Route("/ila/nercilaapplication/generateReport")]
        public async Task<IActionResult> GenerateReportAsync(ExportReportModel model)
        {
            var ilaFilter = model.Options.Filters.FirstOrDefault(x => x.Name.ToUpper() == "SELECT ILA");
            if (ilaFilter != null && !string.IsNullOrEmpty(ilaFilter.Value))
            {
                var encodedIds = ilaFilter.Value.Split(",").ToList();
                ilaFilter.Value = String.Join(",", encodedIds.Select(x => _hasher.Decode(x)));
            }
            else
            {
                throw new QTDServerException("ILA Id is missing in the given input");
            }
            var report = await _reportsService.CreateReportAsync(model.Options, false);
            var file = await _reportGeneratorService.ExportReportAsync(model.ExportType, report);
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = Path.GetFileName(file) }.ToString());
            return File(
                fileStream: fs,
                contentType: System.Net.Mime.MediaTypeNames.Application.Octet,
                fileDownloadName: Path.GetFileName(file));
        }
    }
}
