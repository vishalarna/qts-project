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
    public class Version_PositionsController : ControllerBase
    {
        private readonly IStringLocalizer<Version_PositionsController> _localizer;
        private readonly IVersion_PositionService _ver_posService;
        public Version_PositionsController(
            IStringLocalizer<Version_PositionsController> localizer, IVersion_PositionService ver_posService)
        {
            _localizer = localizer;
            _ver_posService = ver_posService;
        }

        [HttpPost]
        [Route("/positions/version")]
        public async Task<IActionResult> CreatePositionVersionAsync(Position position)
        {
            await _ver_posService.CreateVersionAsync(position);
            return Ok(new { message = _localizer["PositionVersionCreated"] });
        }
    }
}
