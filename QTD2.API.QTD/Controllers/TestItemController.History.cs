using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.TestItem_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class TestItemController : ControllerBase
    {
        [HttpPost]
        [Route("/testitem/history")]
        public async Task<IActionResult> CreateTestItemHistAsync(TestItem_HistoryCreateOptions options)
        {
            var result = await _testItem_historyService.CreateTestItemHistory(options);
            return Ok( new { result });
        }
    }
}
