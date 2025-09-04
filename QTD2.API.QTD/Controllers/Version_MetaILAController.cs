using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Version_MetaILA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public partial class Version_MetaILAController : Controller
    {
        private readonly IVersion_MetaILAService _versionMetaIlaService;
        private readonly IStringLocalizer<Version_MetaILAController> _localizer;

        public Version_MetaILAController(IVersion_MetaILAService versionMetaIlaService, IStringLocalizer<Version_MetaILAController> localizer)
        {
            _versionMetaIlaService = versionMetaIlaService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Version_MetaILAs
        /// </summary>
        /// <returns>Http Response Code with Version_MetaILAs</returns>
        [HttpGet]
        [Route("/version_metailas")]
        public async Task<IActionResult> GetVersionMetaILAsAsync()
        {
            var result = await _versionMetaIlaService.GetAsync();
            return Ok(new { result });
        }

        /// <summary>
        /// Creates a new Version_MetaILA
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/version_metailas")]
        public async Task<IActionResult> CreateVersionMetaILAAsync(Version_MetaILACreateOptions options)
        {
            var result = await _versionMetaIlaService.CreateAsync(options);
            return Ok( new { message = result });
        }

        /// <summary>
        /// Gets a specific Version_MetaILA by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Version_MetaILA</returns>
        [HttpGet]
        [Route("/version_metailas/{id}")]
        public async Task<IActionResult> GetVersionMetaILAAsync(int id)
        {
            var result = await _versionMetaIlaService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific Version_MetaILA by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/version_metailas/{id}")]
        public async Task<IActionResult> UpdateVersionMetaILAAsync(int id, Version_MetaILAUpdateOptions options)
        {
            var result = await _versionMetaIlaService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["MetaILAUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Version_MetaILA by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/version_metailas/{id}")]
        public async Task<IActionResult> DeleteVersionMetaILAAsync(int id, Version_MetaILAOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _versionMetaIlaService.InActiveAsync(id);
                    break;
                case "active":
                    await _versionMetaIlaService.ActiveAsync(id);
                    break;
                case "delete":
                    await _versionMetaIlaService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"VersionMetaILA-{options.ActionType.ToLower()}"].Value });
        }
    }
}
