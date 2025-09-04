using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA.EvaluationReleaseSetting;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpPost]
        [Route("/ila/setting/eval")]
        public async Task<IActionResult> CreateEvalSetting(EvaluationReleaseEMPSettingCreateOptions options)
        {
            var ila = await _ilaService.GetAsync(options.ILAId);
            await _versioningService.VersionILAAsync(ila, "Evaluation Release Settings Created", DateTime.Now, 1);
            var result = await _ilaService.CreateEvalSettingAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/ila/setting/eval/{id}")]
        public async Task<IActionResult> UpdateEvalSetting(int id, EvaluationReleaseEMPSettingUpdateOptions options)
        {
            var ila = await _ilaService.GetAsync(options.ILAId);
            await _versioningService.VersionILAAsync(ila, "Evaluation Release Settings Updated", DateTime.Now, 2);
            var result = await _ilaService.UpdateEvalSettingAsync(id,options);
            return Ok( new { result });
        }

        /// <summary>
        /// Get a specific evaluation setting;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/setting/eval/{id}")]
        public async Task<IActionResult> GetEvalSetting(int id)
        {
            var result = await _ilaService.GetEvalSettingAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get specific evaluation release settings for an ila
        /// </summary>
        /// <param name="ilaId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/{ilaId}/setting/eval")]
        public async Task<IActionResult> GetEvalSettingForILA(int ilaId)
        {
            var result = await _ilaService.GetEvalSettingForILAAsync(ilaId);
            return Ok( new { result });
        }

       
    }
}
