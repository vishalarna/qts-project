using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.DiscussionQuestion;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILATraineeEvaluationController : ControllerBase
    {
        /// <summary>
        /// Creates a new DiscussionQuestion
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/ilatraineeevaluation/discussionQuestion")]
        public async Task<IActionResult> CreateDiscussionQuestionAsync(DiscussionQuestionCreateOptions options)
        {
            var result = await _discussionQuestionService.CreateAsync(options);
            return Ok(new { message = _localizer["DiscussionQuestionCreated"].Value });
        }

        [HttpGet]
        [Route("/ilatraineeevaluation/{id}/discussionQuestion")]
        public async Task<IActionResult> GetDiscussionQuestions(int id)
        {
            var result = await _discussionQuestionService.GetDiscussionQuestionsAsync(id);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/ilatraineeevaluation/{id}/discussionQuestion")]
        public async Task<IActionResult> DeleteAllQuestions(int id)
        {
            await _discussionQuestionService.RemoveAllQuestions(id);
            return Ok( new { message = _localizer["DiscussionQuestionsRemoved"] });
        }

        /// <summary>
        /// Gets a specific DiscussionQuestion by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with DiscussionQuestion</returns>
        //[HttpGet]
        //[Route("/ilatraineeevaluation/discussionQuestion/{id}")]
        //public IActionResult GetDiscussionQuestionAsync(int id)
        //{
        //    var result = _discussionQuestionService.GetAsync(id);
        //    return Ok( new { result });
        //}
    }
}
