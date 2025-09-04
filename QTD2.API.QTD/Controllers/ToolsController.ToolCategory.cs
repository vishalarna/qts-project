using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ToolCategory;

namespace QTD2.API.QTD.Controllers
{
    public partial class ToolsController : ControllerBase
    {
        /// <summary>
        /// Gets a list of ToolCategoriess With tools
        /// </summary>
        /// <returns>Http Response Code with ToolCategories</returns>
        [HttpGet]
        [Route("/tools/categoryWithTools")]
        public async Task<IActionResult> GetToolCategoriesWithToolAsync()
        {
            var result = await _toolService.GetToolCategoriesAsync(true);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of ToolCategoriess
        /// </summary>
        /// <returns>Http Response Code with ToolCategories</returns>
        [HttpGet]
        [Route("/tools/category")]
        public async Task<IActionResult> GetToolCategoriesAsync()
        {
            var result = await _toolService.GetToolCategoriesAsync(false);
            return Ok( new { result });
        }


        /// <summary>
        /// Creates a new ToolCategory
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/tools/category")]
        public async Task<IActionResult> CreateToolCategoryAsync(ToolCategoryCreateOptions options)
        {
            var result = await _toolService.CreateToolCategoryAsync(options);
            await _toolCategoryHistoryDomainService.AddAsync(new Domain.Entities.Core.ToolCategory_StatusHistory(result.Id, options.Notes, options.EffectiveDate ?? System.DateTime.UtcNow));

            return Ok( new { result,message = _localizer["ToolCategoryCreated"] });
        }

        /// <summary>
        /// Gets a specific ToolCategory by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with ToolCategory</returns>
        [HttpGet]
        [Route("/tools/category/{id}")]
        public async Task<IActionResult> GetToolCategoryAsync(int id)
        {
            var result = await _toolService.GetToolCategoryAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific ToolCategory by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/tools/category/{id}")]
        public async Task<IActionResult> UpdateToolCategoryAsync(int id, ToolCategoryUpdateOptions options)
        {
            var result = await _toolService.UpdateToolCategoryAsync(id, options);
            await _toolCategoryHistoryDomainService.AddAsync(new Domain.Entities.Core.ToolCategory_StatusHistory(result.Id, options.Notes, options.EffectiveDate ?? System.DateTime.UtcNow));
            return Ok( new { result, message = _localizer["ToolCategoryUpdated"] });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific ToolCategory by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/tools/category/{id}")]
        public async Task<IActionResult> DeleteToolCategoryAsync(int id, ToolCategoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _toolService.InActiveToolCategoryAsync(id);
                    break;
                case "active":
                    await _toolService.ActiveToolCategoryAsync(id);
                    break;
                case "delete":
                    await _toolService.DeleteToolCategoryAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"ToolCategory-{options.ActionType.ToLower()}"].Value });
        }
    }
}
