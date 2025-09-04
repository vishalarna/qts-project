using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Instructor_CategoryHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class InstructorsController
    {
        [HttpGet]
        [Route("/instructors/categories/history")]
        public async Task<IActionResult> GetAllInsCatHistories()
        {
            var result = await _insCatHistoryService.GetAllInsCatHistories();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/instructors/categories/history/{id}")]
        public async Task<IActionResult> GetInsCatHistory(int id)
        {
            var result = await _insCatHistoryService.GetInsCatHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/instructors/categories/history")]
        public async Task<IActionResult> CreateInsistory(Instructor_CategoryHistoryCreateOptions options)
        {
            var result = await _insCatHistoryService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/instructors/categories/history/{id}")]
        public async Task<IActionResult> UpdateSHHistory(int id, Instructor_CategoryHistoryCreateOptions options)
        {
            var result = await _insCatHistoryService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/instructors/categories/history/{id}")]
        public async Task<IActionResult> DeleteSHHistory(int id, Instructor_CategoryHistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _insCatHistoryService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _insCatHistoryService.InActiveAsync(id);
                    break;
                case "delete":
                    await _insCatHistoryService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"Instructor_CategoryHistory-{options.ActionType.ToLower()}"].Value });
        }
    }
}
