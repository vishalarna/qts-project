using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA_StudentEvaluation_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the Student Evaluation with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/studentEvaluation")]
        public async Task<IActionResult> LinkStudentEvaluationAsync(int id, ILA_StudentEvaluation_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Student Evaluation Link Created", DateTime.Now, 1);
            await _ilaService.LinkStudentEvaluationAsync(id, options);
            return Ok( new { result = _localizer["EvaluationsLinkedToILA"] });
        }
        [HttpPost]
        [Route("/ila/{id}/LinkstudentEvaluation")]
        public async Task<IActionResult> LinkStudentEvaluationsData(int id, LinkStudentEvaluationIlaModel data)
        {
            await _ilaService.LinkStudentEvaluationILAData(id, data);
            return Ok( new { result = _localizer["EvaluationsLinkedToILA"] });
        }


        [HttpGet]
        [Route("/ila/{id}/LinkstudentEvaluation")]
        public async Task<IActionResult> GetStudentEvaluationsData(int id)
        {
            var result=await _ilaService.GetStudentEvaluationILAData(id);
            return Ok( new { result });
        }


        /// <summary>
        /// Unlinks the Student Evaluation with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/studentEvaluation")]
        public async Task<IActionResult> UnlinkStudentEvaluationAsync(int id, ILA_StudentEvaluation_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Student Evaluation Link Removed", DateTime.Now, 0);
            await _ilaService.UnlinkstudentEvaluationAsync(id, options);
            return Ok( new { message = _localizer["StudentEvaluationUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the Student Evaluation linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked  NERCAudience</returns>
        [HttpGet]
        [Route("/ila/{id}/studentEvaluation")]
        public async Task<IActionResult> GetLinkedStudentEvaluationeAsync(int id)
        {
            var result = await _ilaService.GetLinkedStudentEvaluationAsync(id);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/ila/{id}/studentEvaluation/all")]
        public async Task<IActionResult> UnlinkAll(int id)
        {
            await _ilaService.UnlinkAllEvaluationsAsync(id);
            return Ok( new { message = _localizer["UnlinkedAllEvaluations"] });
        }
    }
}
