using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Instructor_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class InstructorsController
    {
        [HttpGet]
        [Route("/instructors/history")]
        public async Task<IActionResult> GetAllInsHistories()
        {
            var history = await _instructorHistoryService.GetHistoryAsync();
            return Ok( new { history });
        }

        [HttpGet]
        [Route("/instructors/history/{id}")]
        public async Task<IActionResult> GetInsHistory(int id)
        {
            var result = await _instructorHistoryService.GetInsCatHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/instructors/history")]
        public async Task<IActionResult> CreateInsistory(Instructor_HistoryCreateOptions options)
        {
            var result = await _instructorHistoryService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/instructors/history/{id}")]
        public async Task<IActionResult> UpdateSHHistory(int id, Instructor_HistoryCreateOptions options)
        {
            var result = await _instructorHistoryService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/instructors/history/{id}")]
        public async Task<IActionResult> DeleteSHHistory(int id, Instructor_HistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _instructorHistoryService.ActiveAsync(id);
                    break;
                case "inactive":
                    await _instructorHistoryService.InActiveAsync(id);
                    break;
                case "delete":
                    await _instructorHistoryService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"Instructor_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
