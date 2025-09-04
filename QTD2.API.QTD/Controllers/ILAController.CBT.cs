using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA.CBTReleaseSetting;
using QTD2.Infrastructure.Model.ILA_Cbt_ScormUpload;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpPost]
        [Route("/ila/{ilaId}/cbt")]
        public async Task<IActionResult> CreateCBT(int ilaId, CBTCreateOptions option)
        {
            var result = await _ilaService.CreateCBTSettingAsync(ilaId, option);
            return Ok( new { result });
        }


        [HttpPut]
        [Route("/ila/{ilaId}/cbt/{cbtId}")]
        public async Task<IActionResult> UpdateCBT(int ilaId, int cbtId, CBTUpdateOptions option)
        {
            var result = await _ilaService.UpdateCBTSettingAsync(cbtId, option);
            return Ok( new { result });
        }

        /// <summary>
        /// Get a specific evaluation setting;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("cbt/{id}")]
        public async Task<IActionResult> GetCBTAsync(int id)
        {
            var result = await _ilaService.GetCBTSettingAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get specific evaluation release settings for an ila
        /// </summary>
        /// <param name="ilaId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/{ilaId}/cbt")]
        public async Task<IActionResult> GetCBTForILAAsync(int ilaId, bool current = true)
        {
            var locList = await _ilaService.GetCBTSettingForILAAsync(ilaId, current);
            return Ok( new { locList });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/cbtScormForms")]
        public async Task<IActionResult> GetCBTScormFormsForILAAsync(int ilaId, bool current = true)
        {
            var locList = await _ilaService.GetCBTScormFormsForILAAsync(ilaId, current);
            return Ok( new { locList });
        }

        [HttpGet]
        [Route("/cbt/{cbtId}/scorm")]
        public async Task<IActionResult> GetScormUploadsAsync(int cbtId, bool current = true)
        {
            var locList = await _ilaService.GetAllScormUploadAsync(cbtId, current);
            return Ok( new { locList });
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [Route("/cbt/{cbtId}/scorm")]
        public async Task<IActionResult> AddScormUploadAsync(int cbtId,IFormFile file)
        {
            var locList = await _ilaService.AttachScormUploadAsync(cbtId, file);
            return Ok( new { locList });
        }

        [HttpPut]
        [Route("/cbt/{cbtId}/scorm/delete")]
        public async Task<IActionResult> DisconnectScormUploadAsync(int cbtId, ScormUploadDeleteOptions options)
        {
            await _ilaService.DisconnectScormUploadAsync(cbtId);
            return Ok( new { message = _localizer[$"ILAScorm"].Value });
        }

        [HttpGet]
        [Route("/cbt/{cbtId}/scorm/{scormUploadId}")]
        public async Task<IActionResult> GetScormUploadAsync(int cbtId, int scormUploadId)
        {
            var locList = await _ilaService.GetCurrentScormUploadAsync(scormUploadId);
            return Ok( new { locList });
        }

        [HttpPost]
        [Route("/classSchedule/{classScheduleId}/employees/{employeeId}")]
        public async Task<IActionResult> RegisterEmployeeToCbtAsync(int classScheduleId, int employeeId)
        {
            Domain.Entities.Core.CBT_ScormRegistration registration = await _ilaService.RegisterEmployeeToCbtAsync(classScheduleId, employeeId);
            return Ok( new { registration });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/students")]
        public async Task<IActionResult> GetStudentsForILAAsync(int ilaId)
        {
            var locList = await _ilaService.GetStudentsForILAAsync(ilaId);
            return Ok( new { locList });
        }

        [HttpGet]
        [Route("/cbt/scormUploads/all")]
        public async Task<IActionResult> GetAllCBTAsync()
        {
            var result = await _ilaService.GetAllCBTScormUploads();
            return Ok( new { result });
        }
    }
}
