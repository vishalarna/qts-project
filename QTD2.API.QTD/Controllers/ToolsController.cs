using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Tool;
using QTD2.Infrastructure.Model.Tool_StatusHistory;
using QTD2.Infrastructure.Model.ToolCategory;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ToolsController : ControllerBase
    {
        private readonly IToolService _toolService;
        private readonly ITool_StatusHistoryService _tool_statusHistoryService;
        private readonly IStringLocalizer<ToolsController> _localizer;
        private readonly IVersioningService _versioningService;
        private readonly ITool_StatusHistoryService _historyService;
        private readonly Domain.Interfaces.Service.Core.IToolCategory_StatusHistoryService _toolCategoryHistoryDomainService;

        public ToolsController(Domain.Interfaces.Service.Core.IToolCategory_StatusHistoryService toolCategoryHistoryDomainService,IToolService toolService, IStringLocalizer<ToolsController> localizer, ITool_StatusHistoryService tool_statusHistoryService, IVersioningService versioningService, ITool_StatusHistoryService historyService)
        {
            _toolService = toolService;
            _localizer = localizer;
            _tool_statusHistoryService = tool_statusHistoryService;
            _versioningService = versioningService;
            _historyService = historyService;
            _toolCategoryHistoryDomainService = toolCategoryHistoryDomainService;
        }

        /// <summary>
        /// Gets a list of tools
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tools")]
        public async Task<IActionResult> GetAsync()
        {
            var tools = await _toolService.GetAsync();
            return Ok( new { tools });
        }

        /// <summary>
        /// Gets a tool
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tools/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var tool = await _toolService.GetAsync(id);
            return Ok( new { tool });
        }

        /// <summary>
        /// Creates a tool
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/tools")]
        public async Task<IActionResult> CreateAsync([FromBody] ToolCreateOptions options)
        {
            var tool = await _toolService.CreateAsync(options);
            await _versioningService.VersionToolAsync(tool);
            var historyOptions = new Tool_StatusHistoryCreateOptions();
            historyOptions.ChangeEffectiveDate = options.EffectiveDate;
            historyOptions.ChangeNotes = options.Notes;
            historyOptions.ToolIds = new int[] { tool.Id };
            await _historyService.CreateAsync(historyOptions);
            return Ok( new { tool, message = _localizer["toolcreated"] });
        }

        /// <summary>
        /// Get Tool number next to be saved in category
        /// </summary>
        /// <param name="catId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tools/{catId}/number")]
        public async Task<IActionResult> GetToolNumber(int catId)
        {
            var result = await _toolService.GetToolNumber(catId);
            return Ok( new { result });
        }

        /// <param name="id"></param>
        /// <param name="options"></param>
        [HttpPut]
        [Route("/tools/{id}")]
        public async Task<IActionResult> UpdateToolAsync(int id, [FromBody] ToolCreateOptions options)
        {
            var tool = await _toolService.UpdateToolAsync(id, options);
            await _versioningService.VersionToolAsync(tool);
            var historyOptions = new Tool_StatusHistoryCreateOptions();
            historyOptions.ChangeEffectiveDate = options.EffectiveDate ?? System.DateTime.UtcNow;
            historyOptions.ChangeNotes = options.Notes;
            historyOptions.ToolIds = new int[] { tool.Id };
            await _historyService.CreateAsync(historyOptions);
            return Ok( new { message = _localizer["ToolCategoryUpdated"].Value });
        }


        [HttpDelete]
        [Route("/tools/{id}")]
        public async Task<IActionResult> DeleteToolAsync(int id, ToolCategoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _toolService.InActiveToolAsync(id);
                    break;
                case "active":
                    await _toolService.ActiveToolAsync(id);
                    break;
                case "delete":
                    await _toolService.DeleteToolAsync(id);
                    break;
            }
            await _tool_statusHistoryService.CreateAsync(new Tool_StatusHistoryCreateOptions { ChangeNotes = options.ChangeNotes, ChangeEffectiveDate = options.ChangeEffectiveDate, ToolIds = new int[] { id } });
            return Ok( new { message = $"Tool {options.ActionType.ToLower()} successfully." });
        }


    }
}
