using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.StudentEvaluation;
using QTD2.Infrastructure.Model.StudentEvaluationWithoutEmp;
using System.Threading.Tasks;
namespace QTD2.API.QTD.Controllers
{
    public partial class ScheduleClassesController : ControllerBase
    {
        /// <summary>
        /// Create roster for student evaluation
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/schedules/studentEvaluationWithoutEmp")]
        public async Task<IActionResult> CreateRoster(StudentEvaluationWithoutEmpCreateOptions options)
        {
            await _studentEvaluationWithoutEmp.CreateAsync(options);
            return Ok( new { message = _localier["StudentEvaluationRecordCreated"] });
        }

        /// <summary>
        /// Get Employee and Evaluation Data for specific evaluation
        /// </summary>
        /// <param name="evalId"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/schedules/{classId}/studentEvaluationWithoutEmp/{evalId}")]
        public async Task<IActionResult> GetDataForEvalWithEMP(int evalId,int classId)
        {
            var result = await _studentEvaluationWithoutEmp.GetDataForEvalWithEMPAsync(evalId,classId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Student Evaluations Linked to specific class
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/schedules/{classId}/studentEvaluationWithoutEmp")]
        public async Task<IActionResult> GetEvaluationsForClass(int classId)
        {
            var result = await _studentEvaluationWithoutEmp.GetEvaluationsForClassAsync(classId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/schedules/{classId}/studentEvaluationWithoutEmp/all")]
        public async Task<IActionResult> GetAllEvaluationDataForClass(int classId)
        {
            var result = await _studentEvaluationWithoutEmp.GetAllEvaluationDataForClass(classId);
            return Ok( new { result });
        }


        /// <summary>
        /// Recall or release the Evaluation
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/schedules/studentEvaluationWithoutEmp")]
        public async Task<IActionResult> ReleaseOrRecallEval(EvalReleaseOptions options)
        {
            await _studentEvaluationWithoutEmp.ReleaseOrRecallEvalAsync(options);
            return Ok( new { message = _localier["Evaluation" + options.Action] });
        }
    }
}
