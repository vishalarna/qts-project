using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.ClassSchedule_StudentEvaluation_Link;
using System;
using System.Threading.Tasks;
namespace QTD2.API.QTD.Controllers
{
    public partial class StudentEvaluationController
    {
        [HttpPost]
        [Route("/studentEvaluations/{id}/classes")]
        public async Task<IActionResult> LinkClass(int id, ClassSchedule_StudentEvaluation_LinkCreateOptions options)
        {
            var result = await _studentEvaluationService.LinkClass(id, options);
            return Ok( new { result });
        }
        
        [HttpPut]
        [Route("/studentEvaluations/link/classes")]
        public async Task<IActionResult> updateLinkEvaluationData(ClassSchedule_StudentEvaluation_LinkUpdateOptions options)
        {
            var result = await _studentEvaluationService.UpdateLinkClassData(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Get classes linked to evaluation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/studentEvaluations/{id}/classes")]
        public async Task<IActionResult> GetLinkedClasses(int id)
        {
            var result = await _studentEvaluationService.GetLinkedClassesToEvaluation(id);
            return Ok( new { result });
        }
    }
}
