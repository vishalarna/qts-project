using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.StudentEvaluation_Question_Link;
using QTD2.Infrastructure.Model.StudentEvaluationHistory;
using System;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class StudentEvaluationController
    {
        [HttpPost]
        [Route("/studentEvaluations/{id}/questions")]
        public async Task<IActionResult> LinkQuestion(int id, StudentEvaluation_Question_LinkCreateOptions options)
        {
            var result = await _studentEvaluationService.LinkQuestions(id, options);

            foreach (var item in options.QuestionIds)
            {
                await _studentEvaluationHistoryService.CreateAsync(new StudentEvaluationHistoryCreateOptions()
                {
                    StudentEvaluationNotes = "Student Evaluation Linked to Question  Id => " + item,
                    EffectiveDate = DateTime.Now,
                    StudentEvaluationId = id
                });
            }

            return Ok( new { result });
        }


        [HttpDelete]
        [Route("/studentEvaluations/{id}/questions/unlink")]
        public async Task<IActionResult> UnLinkQuestion(int id, StudentEvaluation_Question_LinkCreateOptions options)
        {
            var result = await _studentEvaluationService.UnLinkQuestions(id, options);

            foreach (var item in options.QuestionIds)
            {
                await _studentEvaluationHistoryService.CreateAsync(new StudentEvaluationHistoryCreateOptions()
                {
                    StudentEvaluationNotes = "Student Evaluation Linked to Question  Id => " + item,
                    EffectiveDate = DateTime.Now,
                    StudentEvaluationId = id
                });
            }

            return Ok( new { result });
        }

        /// <summary>
        /// Get Employee linked to position
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/studentEvaluations/{id}/questions")]
        public async Task<IActionResult> GetLinkedQuestions(int id)
        {
            var result = await _studentEvaluationService.GetLinkedQuestions(id);
            return Ok( new { result });
        }


        [HttpPost]
        [Route("/studentEvaluations/SaveQuestion")]
        public async Task<IActionResult> SaveQuestion(StudentEvaluation_SaveQuestion options)
        {
            var result = await _studentEvaluationService.SaveQuestion(options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/studentEvaluations/{evalId}/class/{classId}/emp/{empId}")]
        public async Task<IActionResult> GetSavedQuestionsData(int evalId, int classId, int empId)
        {
            var result = await _studentEvaluationService.GetSavedQuestionsDataAsync(evalId, classId, empId);
            return Ok( new { result });
        }

    }
}
