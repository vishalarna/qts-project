using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.MetaILA_Status;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MetaILA_StatusController : ControllerBase
    {
        private readonly IMetaILA_StatusService _metaILAStatusService;
        private readonly IStringLocalizer<MetaILA_StatusController> _localizer;

        public MetaILA_StatusController(IMetaILA_StatusService metaILAStatusService, IStringLocalizer<MetaILA_StatusController> localizer)
        {
            _metaILAStatusService = metaILAStatusService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of MetaILA_Status
        /// </summary>
        /// <returns>Http Response Code with MetaILA_Status</returns>
        [HttpGet]
        [Route("/metaILAStatus")]
        public async Task<IActionResult> GetMetaILA_StatusAsync()
        {
            var result = await _metaILAStatusService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new MetaILA_Status
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/metaILAStatus")]
        public async Task<IActionResult> CreateMetaILA_StatusAsync(MetaILA_StatusCreateOptions options)
        {
            var result = await _metaILAStatusService.CreateAsync(options);
            return Ok( new { result, message = _localizer["RegulatoryRequirementCreated"].Value });
        }

        /// <summary>
        /// Gets a specific MetaILA_Status by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with MetaILA_Status</returns>
        [HttpGet]
        [Route("/metaILAStatus/{id}")]
        public async Task<IActionResult> GetMetaILA_StatusAsync(int id)
        {
            var result = await _metaILAStatusService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific MetaILA_Status by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/metaILAStatus/{id}")]
        public async Task<IActionResult> UpdateMetaILA_StatusAsync(int id, MetaILA_StatusUpdateOptions options)
        {
            var result = await _metaILAStatusService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["MetaILA_StatusUpdated"].Value, result });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific MetaILA_Status by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/metaILAStatus/{id}")]
        public async Task<IActionResult> DeleteMetaILA_StatusAsync(int id, MetaILA_StatusOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _metaILAStatusService.InActiveAsync(id);
                    break;
                case "active":
                    await _metaILAStatusService.ActiveAsync(id);
                    break;
                case "delete":
                    await _metaILAStatusService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"MetaILA_Status-{options.ActionType.ToLower()}"].Value });
        }
    }
}
