using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.MetaILAConfigurationPublishOption;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MetaILAConfigurationPublishOptionController : Controller
    {
        private readonly IMetaILAConfigurationPublishOptionService _metaILAConfigurationPublishOptionService;
        private readonly IStringLocalizer<MetaILAConfigurationPublishOptionController> _localizer;

        public MetaILAConfigurationPublishOptionController(IMetaILAConfigurationPublishOptionService metaILAConfigurationPublishOptionService, IStringLocalizer<MetaILAConfigurationPublishOptionController> localizer)
        {
            _metaILAConfigurationPublishOptionService = metaILAConfigurationPublishOptionService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of MetaILA Configuration Publish Option
        /// </summary>
        /// <returns>Http Response Code with MetaILA Configuration Publish Option</returns>
        [HttpGet]
        [Route("/metaILAConfigPublishOption")]
        public async Task<IActionResult> GetMetaILAConfigurationPublishOptionAsync()
        {
            var result = await _metaILAConfigurationPublishOptionService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new MetaILA Configuration Publish Option
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/metaILAConfigPublishOption")]
        public async Task<IActionResult> CreateMetaILAConfigurationPublishOptionAsync(MetaILAConfigurationPublishOptionCreateOptions options)
        {
            var result = await _metaILAConfigurationPublishOptionService.CreateAsync(options);
            return Ok( new { message = _localizer["testItemTypeCreated"].Value });
        }

        /// <summary>
        /// Gets a specific MetaILA Configuration Publish Option by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with MetaILA Configuration Publish Option</returns>
        [HttpGet]
        [Route("/metaILAConfigPublishOption/{id}")]
        public async Task<IActionResult> GetMetaILAConfigurationPublishOptionAsync(int id)
        {
            var result = await _metaILAConfigurationPublishOptionService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific MetaILA Configuration Publish Option by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/metaILAConfigPublishOption/{id}")]
        public async Task<IActionResult> UpdateMetaILAConfigurationPublishOptionAsync(int id, MetaILAConfigurationPublishOptionUpdateOptions options)
        {
            var result = await _metaILAConfigurationPublishOptionService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["metaILAConfigurationPublishOptionsUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific MetaILA Configuration Publish Option by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/metaILAConfigPublishOption/{id}")]
        public async Task<IActionResult> DeleteMetaILAConfigurationPublishOptionAsync(int id, MetaILAConfigurationPublishOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _metaILAConfigurationPublishOptionService.InActiveAsync(id);
                    break;
                case "active":
                    await _metaILAConfigurationPublishOptionService.ActiveAsync(id);
                    break;
                case "delete":
                    await _metaILAConfigurationPublishOptionService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"MetaILAConfigurationPublishOption-{options.ActionType.ToLower()}"].Value });
        }

    }
}
