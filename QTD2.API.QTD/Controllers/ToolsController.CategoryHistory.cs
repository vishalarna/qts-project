using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Tool_StatusHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class ToolsController : ControllerBase
    { /// <summary>
      /// Gets a list of Tool_StatusHistories
      /// </summary>
      /// <returns>Http Response Code with Tool_StatusHistories</returns>
        [HttpGet]
        [Route("/toolsCategory/history")]
        public async Task<IActionResult> GetToolCategory_StatusHistorysAsync()
        {
            var result = await _toolCategoryHistoryDomainService.AllAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Tool_StatusHistory
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/toolsCategory/history")]
        public async Task<IActionResult> CreateToolCategory_StatusHistoryAsync(ToolCategory_StatusHistoryCreateOptions options)
        {
            await _toolCategoryHistoryDomainService.AddAsync(new Domain.Entities.Core.ToolCategory_StatusHistory(options.ToolCategoryId,options.ChangeNotes,options.ChangeEffectiveDate??System.DateTime.UtcNow));
            return Ok( new { message = _localizer["Tool_StatusHistoryCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Tool_StatusHistory by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Tool_StatusHistory</returns>
        [HttpGet]
        [Route("/toolsCategory/history/{id}")]
        public async Task<IActionResult> GetToolCategory_StatusHistoryAsync(int id)
        {
            var result = await _toolCategoryHistoryDomainService.FindAsync(x=>x.ToolCategoryId==id);
            return Ok( new { result });
        }

    }
}
