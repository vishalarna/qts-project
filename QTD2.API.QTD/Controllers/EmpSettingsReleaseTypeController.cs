using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpSettingsReleaseTypeController : ControllerBase
    {

        private readonly IEmpSettingsReleaseTypeService _empSettingsReleaseTypeService;
        private readonly IStringLocalizer<EmpSettingsReleaseTypeController> _localizer;

        public EmpSettingsReleaseTypeController(IStringLocalizer<EmpSettingsReleaseTypeController> localizer,
            IEmpSettingsReleaseTypeService empSettingsReleaseTypeService)
        {
            _localizer = localizer;
            _empSettingsReleaseTypeService = empSettingsReleaseTypeService;
        }

        [HttpGet]
        [Route("/empSettingsReleaseType")]
        public async Task<IActionResult> GetEmpSettingsReleaseTypeAsync()
        {
            var result = await _empSettingsReleaseTypeService.GetAllAsync();
            return Ok(new { result });
        }
    }
}
