using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EvaluationMethodController : ControllerBase
    {
        private readonly IStringLocalizer<EvaluationMethodController> _localizer;
        private readonly IEvaluationMethodService _evaluationMethodService;

        public EvaluationMethodController(IStringLocalizer<EvaluationMethodController> localizer, IEvaluationMethodService evaluationMethodService)
        {
            _localizer = localizer;
            _evaluationMethodService = evaluationMethodService;
        }

        [HttpGet]
        [Route("/evalMethod")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _evaluationMethodService.GetAllAsync();
            return Ok( new { result });
        }
    }
}
