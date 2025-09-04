using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.TrainingProgram_ILA_Link;
namespace QTD2.API.QTD.Controllers
{
    public partial class TrainingProgramsController
    {
        [HttpPost]
        [Route("/trainingPrograms/{id}/ila")]
        public async Task<IActionResult> LinkILA(int id, TrainingProgram_ILA_LinkCreateOptions options)
        {
            var result = await _trainingProgramService.LinkILA(id, options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/trainingPrograms/{id}/ila")]
        public async Task<IActionResult> GetLinkedILAs(int id)
        {
            var result = await _trainingProgramService.GetLinkedILAs(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink specific ILAs linked to procedure provided by procId
        /// </summary>
        /// <param name="trainingProgramId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/trainingPrograms/{trainingProgramId}/ila/")]
        public async Task<IActionResult> UnlinkILA(int trainingProgramId, TrainingProgram_ILA_LinkCreateOptions options)
        {
            await _trainingProgramService.UnlinkILA(trainingProgramId, options.ILAIds);
            return Ok( new { message = _localizer["ILAUnlinked"].Value });
        }

        /// <summary>
        /// Get all procedures that the task is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingPrograms/ila/{id}")]
        public async Task<IActionResult> GetTrainingProgramILAIsLinkedTo(int id)
        {
            var result = await _trainingProgramService.GetTrainingProgramILAIsLinkedTo(id);
            return Ok( new { result });
        }

       
    }
}
