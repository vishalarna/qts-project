using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.TrainingProgram;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class TrainingProgramReviewController : ControllerBase
    {
        [HttpGet]
        [Route("/trainingProgram/{id}/trainingissues")]
        public async Task<IActionResult> GetTrainingProgramTrainingissueLinksByIdAsync(int id)
        {
            var result = await _trainingProgramReview_TrainingIssue_LinkService.GetTrainingProgramTrainingIssuesLinkById(id);
            return Ok(new { result });
        }

        [HttpPost]
        [Route("/trainingProgram/{id}/trainingissues")]

        public async Task<IActionResult> CreateTrainingProgramTrainingIssueLinks(int id, TrainingProgramTrainingIssueLinkOption options)
        {
            await _trainingProgramReview_TrainingIssue_LinkService.CreateTrainingProgramTrainingIssueLinks(id,options);
            return Ok();
        }

        [HttpPut]
        [Route("/trainingProgram/{id}/trainingissues")]

        public async Task<IActionResult> RemoveTrainingProgramTrainingIssueLinks(int id, TrainingProgramTrainingIssueLinkOption options)
        {
            await _trainingProgramReview_TrainingIssue_LinkService.RemoveTrainingProgramTrainingIssueLinks(id, options);
            return Ok();
        }

    }
}
