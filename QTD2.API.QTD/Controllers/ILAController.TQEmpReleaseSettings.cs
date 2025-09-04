using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.TQEvaluatorILAEmpSettings;
using System;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpPost]
        [Route("/ila/setting/tqEval")]
        public async Task<IActionResult> CreateTQEvalSetting(TQEvaluatorILAEmpSettings options)
        {
            var ila = await _ilaService.GetAsync(options.ILAId);
            await _versioningService.VersionILAAsync(ila,"Task Qualification Emp Release Settings Created", DateTime.Now, 1);
            var result = await _ilaService.CreateTQEvaluatorSettingAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/ila/setting/tqEval/{id}")]
        public async Task<IActionResult> UpdateTQEvalSetting(int id, TQEvaluatorILAEmpSettings options)
        {
            var ila =await _ilaService.GetAsync(options.ILAId);
            await _versioningService.VersionILAAsync(ila, "Task Qualification Emp Release Settings Updated", DateTime.Now, 2);
            var result = await _ilaService.UpdateTQEvaluatorSettingAsync(id, options);
            return Ok( new { result });
        }


        /// <summary>
        /// Get specific self registration option settings for an ila
        /// </summary>
        /// <param name="ilaId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/{ilaId}/setting/tqEval")]
        public async Task<IActionResult> GetTQEvalSettingForILA(int ilaId)
        {
            var result = await _ilaService.GetTQEvaluatorSettingForILAAsync(ilaId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/evals")]
        public async Task<IActionResult> GetTQEvaluatorsForILA(int ilaId)
        {
            var result = await _ilaService.GetTQEvaluatorsForILAAsync(ilaId);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/ila/{ilaId}/evals/link")]
        public async Task<IActionResult> LinkEvaluatorsFromILA(ILA_EvaluatorOptions options)
        {
            await _ilaService.LinkEvaluators(options);
            return Ok( new { message = _localizer["EvaluatorsLinkedToILA"] });
        }

        [HttpDelete]
        [Route("/ila/{ilaId}/evals/unlink")]
        public async Task<IActionResult> UnlinkEvaluatorsFromILA(ILA_EvaluatorOptions options)
        {
            await _ilaService.UnlinkEvaluator(options);
            return Ok( new { message = _localizer["EvaluatorsLinkedToILA"] });
        }
    }

}