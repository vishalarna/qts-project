using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ToolCategory;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.Tool;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    public partial class ToolsController : ControllerBase
    {
        [HttpGet]
        [Route("/tools/categories/nest")]
        public async Task<IActionResult> GetToolsWithCaregoriesAsync()
        {
            var tools = await _toolService.GetToolsWithCategoriesAsync();
            return Ok( new { tools });
        }


        [HttpGet]
        [Route("/tools/statistics")]
        public async Task<IActionResult> GetToolsStatisticsData()
        {
            var tools = await _toolService.GetToolsStatistics();
            return Ok( new { tools });
        }

        //    bulk edit

        [HttpPost]
        [Route("/tools/bulk/edit")]
        public async Task<IActionResult> BulkEditToolsAsync(ToolsBulkEditOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                    foreach (var id in options.toolIds)
                    {
                        await _toolService.InActiveToolAsync(id);
                    }
                    break;
                case "active":
                    foreach (var id in options.toolIds)
                    {
                        await _toolService.ActiveToolAsync(id);
                    }
                    break;
                case "delete":
                    foreach (var id in options.toolIds)
                    {
                        await _toolService.DeleteToolAsync(id);
                    }
                    break;
                case "linktask":
                    await _toolService.LinkTasksByTools(new LinkToolsOptions
                    {
                        toolIds = options.toolIds,
                        LinkedIds = options.LinkedIds
                    });
                    break;
                case "linkskills":
                    await _toolService.LinkEOsByTools(new LinkToolsOptions
                    {
                        toolIds = options.toolIds,
                        LinkedIds = options.LinkedIds
                    });
                    break;
                case "linksaftyhazards":
                    await _toolService.LinksafetyHazardsByTools(new LinkToolsOptions
                    {
                        toolIds=options.toolIds,
                        LinkedIds=options.LinkedIds
                    });
                    break;
                case "unlinktask":
                    await _toolService.unLinkTasksByTools(new LinkToolsOptions
                    {
                        toolIds = options.toolIds,
                        LinkedIds = options.LinkedIds
                    });
                    break;
                case "unlinkskills":
                    await _toolService.unLinkEOsByTools(new LinkToolsOptions
                    {
                        toolIds = options.toolIds,
                        LinkedIds = options.LinkedIds
                    });
                    break;
                case "unlinksaftyhazards":
                    await _toolService.unLinksafetyHazardsByTools(new LinkToolsOptions
                    {
                        toolIds = options.toolIds,
                        LinkedIds = options.LinkedIds
                    });
                    break;
            }

            return Ok( new { message = $"Tool {options.ActionType.ToLower()} successfully." });
        }


        //get linked tasks by tool id

        [HttpGet]
        [Route("/tool/tasks/{id}")]
        public async Task<IActionResult> GetLinkedTasksByToolId(int id)
        {
            var tasks = await _toolService.GetLinkedTasksByToolId(id);
            return Ok( new { tasks });
        }

        //get linked skill assesment

        [HttpGet]
        [Route("/tool/skills/{id}")]
        public async Task<IActionResult> GetLinkedSkillsByToolId(int id)
        {
            var tasks = await _toolService.GetLinkedSkillsByToolId(id);
            return Ok( new { tasks });
        }
        //get linked safety hazards

        [HttpGet]
        [Route("/tool/safetyHazards/{id}")]
        public async Task<IActionResult> GetLinkedsafetyHazardsByToolId(int id)
        {
            var tasks = await _toolService.GetLinkedsafetyHazardsByToolId(id);
            return Ok( new { tasks });
        }

        //link safety hazard

        [HttpPost]
        [Route("/tool/safetyHazards/link")]
        public async Task<IActionResult> LinksafetyHazardsByTool(LinkToolsOptions options)
        {
            await _toolService.LinksafetyHazardsByTools(options);
            return Ok( new { message = "Tools Linked to SafetyHazards Successfully." });
        }

        //link skill assesment


        [HttpPost]
        [Route("/tool/skills/link")]
        public async Task<IActionResult> LinkSkillsByTool(LinkToolsOptions options)
        {
            await _toolService.LinkEOsByTools(options);
            return Ok( new { message = "Tools Linked to Skills Successfully." });
        }

        //link tasks


        [HttpPost]
        [Route("/tool/tasks/link")]
        public async Task<IActionResult> LinkTaksByTool(LinkToolsOptions options)
        {
            await _toolService.LinkTasksByTools(options);
            return Ok( new { message="Tools Linked to Tasks Successfully." });
        }


        //unlink safety hazard

        [HttpPost]
        [Route("/tool/safetyHazards/unlink")]
        public async Task<IActionResult> unLinksafetyHazardsByTool(LinkToolsOptions options)
        {
            await _toolService.unLinksafetyHazardsByTools(options);
            return Ok( new { message = "Tools Linked to SafetyHazards Successfully." });
        }

        //unlink skill assesment


        [HttpPost]
        [Route("/tool/skills/unlink")]
        public async Task<IActionResult> unLinkSkillsByTool(LinkToolsOptions options)
        {
            await _toolService.unLinkEOsByTools(options);
            return Ok( new { message = "Tools Linked to Skills Successfully." });
        }

        //unlink tasks


        [HttpPost]
        [Route("/tool/tasks/unlink")]
        public async Task<IActionResult> unLinkTaksByTool(LinkToolsOptions options)
        {
            await _toolService.unLinkTasksByTools(options);
            return Ok( new { message = "Tools unLinked to Tasks Successfully." });
        }

        /// <summary>
        /// Get tasks linked to tools
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tool/{id}/tasklist")]
        public async Task<IActionResult> GetLinkedTasks(int id)
        {
            var result = await _toolService.GetToolTaskIsLinkedTo(id);
            return Ok( new { result });
        }


        /// <summary>
        /// Get all tools that the eo is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tool/skilllist/{id}")]
        public async Task<IActionResult> GetPosEoIsLinkedTo(int id)
        {
            var result = await _toolService.GetToolsEoIsLinkedTo(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all tools that the sh is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tool/shlist/{id}")]
        public async Task<IActionResult> GetPosSHIsLinkedTo(int id)
        {
            var result = await _toolService.GetToolsSHIsLinkedTo(id);
            return Ok( new { result });
        }


        [HttpGet]
        [Route("/tool/{option}/toollist")]
        public async Task<IActionResult> GetToolActiveInactiveList(string option)
        {
            var result = await _toolService.GetToolList(option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tool/{option}/catlist")]
        public async Task<IActionResult> GetCatActiveInactiveList(string option)
        {
            var result = await _toolService.GetCategoryList(option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tools/notLinkedToTask")]
        [ProducesResponseType(typeof(Result<IEnumerable<UnlinkedToolDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetToolsNotLinkedToTask()
        {
            var result = await _toolService.GetToolsNotLinkedToTaskAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpGet]
        [Route("/tools/notLinkedToEo")]
        [ProducesResponseType(typeof(Result<IEnumerable<UnlinkedToolDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetToolsNotLinkedToEo()
        {
            var result = await _toolService.GetToolsNotLinkedToEoAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
