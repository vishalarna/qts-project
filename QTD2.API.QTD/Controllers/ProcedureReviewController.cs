using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.ProcedureReview;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ProcedureReviewController : ControllerBase
    {
        private readonly IProcedureReviewService _procedureReviewService;
        private readonly IProcedureReviewEmpService _procedureReviewEmpService;
        private readonly IStringLocalizer<ProcedureReviewController> _localier;

        public ProcedureReviewController(IProcedureReviewService procedureReviewService,IStringLocalizer<ProcedureReviewController> localier, IProcedureReviewEmpService procedureReviewEmpService)
        {
            _procedureReviewService = procedureReviewService;
            _procedureReviewEmpService = procedureReviewEmpService;
            _localier = localier;
        }

        [HttpGet]
        [Route("/procedureReviews")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _procedureReviewService.GetListAsync();
            return Ok(new { result });
        }


        [HttpGet]
        [Route("/procedureReview/{id}")]
        [ProducesResponseType(typeof(Result<ProcedureReview>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _procedureReviewService.GetAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("/procedureReview")]
        [ProducesResponseType(typeof(Result<ProcedureReview>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateProcedureReviewDto options)
        {
            var result = await _procedureReviewService.CreateAsync(options);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpPut]
        [Route("/procedureReview/{id}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, CreateProcedureReviewDto options)
        {
            var result = await _procedureReviewService.UpdateAsync(id, options);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("/procedureReview/{id}")]
        public async Task<IActionResult> DeleteAsync(ProcedureReviewOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _procedureReviewService.DeactivateAsync(options);
                    break;
                case "active":
                    await _procedureReviewService.ActivateAsync(options);
                    break;
                case "delete":
                    await _procedureReviewService.DeleteAsync(options);
                    break;
            }

            return Ok(new { message = $"ProcedureReview-{options.ActionType.ToLower()}" });
        }

        [HttpPut]
        [Route("/procedureReview/{id}/publish")]
        public async Task<IActionResult> PublishProcedureReview(int id)
        {
            var publishEval = await _procedureReviewService.PublishProcedureReview(id);
            return Ok( new { publishEval });
        }

        [HttpGet]
        [Route("/procedureReview/stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var stats = await _procedureReviewService.GetStatsCount();
            return Ok( new { stats });
        }

        [HttpGet]
        [Route("/procedureReview/published")]
        public async Task<IActionResult> GetPublishedList()
        {
            var stats = await _procedureReviewService.GetPublishedList();
            return Ok( new { stats });
        }

    }
}
