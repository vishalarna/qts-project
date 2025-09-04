using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.DIFSurveyEmployeeResponse;
using QTD2.Infrastructure.Model.Import_CSV_VM;
using System;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ImportController : ControllerBase
    {
        private readonly IStringLocalizer<ImportController> _localizer;
        private readonly IImportService _importService;
        public ImportController(IStringLocalizer<ImportController> localizer,
             IImportService importService)
        {
            _localizer = localizer;
            _importService = importService;
        }

        [HttpPost]
        [Route("/import/difSurveryEmployeeResponse/validate")]
        public async Task<IActionResult> ValidateDIFSurveyEmployeeResponseAsync([FromForm] ValidateCSV_DIFSurveyEmployeeResponse_VM model)
        {
            var result = await _importService.ValidateDIFEmployeeResponseCSVFileAsync(model);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/import/ila/validate")]
        public async Task<IActionResult> ValidateILAAsync([FromForm] ValidateCSV_VM model)
        {
            var result = await _importService.ValidateILAAsync(model);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/import/employee/validate")]
        public async Task<IActionResult> ValidateEmployeeAsync([FromForm] ValidateCSV_VM model)
        {
            var result = await _importService.ValidateEmployeeAsync(model);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/import/class/validate")]
        public async Task<IActionResult> ValidateClassAsync([FromForm] ValidateCSV_VM model)
        {
            var result = await _importService.ValidateClassAsync(model);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/import/difSurveryEmployeeResponse")]
        public async Task<IActionResult> ImportDIFSurveyEmployeeResponseAsync([FromBody] ImportData_DIFSurveyEmployeeResponse_VM model)
        {
            var result = await _importService.ImportDIFSurveyEmployeeResponseAsync(model);
            var fileName = "DIFSurveyEmployeeResponseData" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = fileName }.ToString());
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/import/ila")]
        public async Task<IActionResult> ImportILAAsync([FromBody] ImportData_ILA_VM model)
        {
            var result = await _importService.ImportILAAsync(model);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/import/employee")]
        public async Task<IActionResult> ImportEmployeeAsync([FromBody] ImportData_Employee_VM model)
        {
            var result = await _importService.ImportEmployeeAsync(model);
            var fileName = "EmployeeResponseData" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = fileName }.ToString());
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/import/class")]
        public async Task<IActionResult> ImportClassAsync([FromBody] ImportData_Class_VM model)
        {
            var result = await _importService.ImportClassAsync(model);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/import/template/{type}")]
        public async Task<IActionResult> GetTemplateAsync(string type)
        {
            var fileBytes = await _importService.GetTemplateAsync(type);
            var fileName = $"{type}.csv";
            return File(fileBytes, "text/csv", fileName);
        }


    }
}
