using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.QuestionBankHistory;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
   
    public partial class QuestionBankController
    {
        [HttpGet]
        [Route("/questionBank/history")]
        public async Task<IActionResult> GetAllHistories()
        {
            var history =  _questionBankHistoryService.GetAsync();
            return Ok( new { history });
        }

        [HttpGet]
        [Route("/questionBank/history/{id}")]
        public async Task<IActionResult> GetInsHistory(int id)
        {
            var result = await _questionBankHistoryService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/questionBank/history")]
        public async Task<IActionResult> CreateInsistory(QuestionBankHistoryCreateOptions options)
        {
            var result = await _questionBankHistoryService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/questionBank/history/{id}")]
        public async Task<IActionResult> UpdateSHHistory(int id, QuestionBankHistoryCreateOptions options)
        {
            var result = await _questionBankHistoryService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/questionBank/history/{id}")]
        public async Task<IActionResult> DeleteSHHistory(int id, QuestionBankHistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _questionBankHistoryService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _questionBankHistoryService.DeactivateAsync(id);
                    break;
                case "delete":
                    await _questionBankHistoryService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"QuestionBank_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
