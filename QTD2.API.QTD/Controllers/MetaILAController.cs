using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.MetaILA;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class MetaILAController : ControllerBase
    {
        private readonly IMetaILAService _metaIlaService;
        private readonly IStringLocalizer<MetaILAController> _localizer;

        public MetaILAController(IMetaILAService metaIlaService, IStringLocalizer<MetaILAController> localizer)
        {
            _metaIlaService = metaIlaService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of MetaILAs
        /// </summary>
        /// <returns>Http Response Code with MetaILAs</returns>
        [HttpGet]
        [Route("/metailas")]
        public async Task<IActionResult> GetMetaILAsAsync()
        {
            var result = await _metaIlaService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new MetaILA
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/metailas")]
        public async Task<IActionResult> CreateMetaILAAsync(MetaILACreateOptions options)
        {
            var data = await _metaIlaService.CreateAsync(options);
            var result = _metaIlaService.MapMetaILAToMetaILAVM(data);
            return Ok( new { message = result });
        }

        /// <summary>
        /// Gets a specific MetaILA by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with MetaILA</returns>
        [HttpGet]
        [Route("/metailas/{id}")]
        public async Task<IActionResult> GetMetaILAAsync(int id)
        {
            var data = await _metaIlaService.GetAsync(id);
            var result = _metaIlaService.MapMetaILAToMetaILAVM(data);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific MetaILA by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/metailas/{id}")]
        public async Task<IActionResult> UpdateMetaILAAsync(int id, MetaILAUpdateOptions options)
        {
            var data = await _metaIlaService.UpdateAsync(id, options);
            var result = _metaIlaService.MapMetaILAToMetaILAVM(data);
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific MetaILA by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/metailas/{id}")]
        public async Task<IActionResult> DeleteMetaILAAsync(int id, MetaILAOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _metaIlaService.InActiveAsync(id);
                    break;
                case "active":
                    await _metaIlaService.ActiveAsync(id);
                    break;
                case "delete":
                    await _metaIlaService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"MetaILA-{options.ActionType.ToLower()}"].Value });
        }

        /// <summary>
        /// Get MetaILA Requirements by ILA Id
        /// </summary>
        [HttpGet]
        [Route("/metailas/{iLAId}/requirements")]
        public async Task<IActionResult> GetMetaILAILARequirements(int iLAId)
        {
            var result = await _metaIlaService.GetMetaILAILARequirements(iLAId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get MetaILA enabling objectives by MetaILA Id
        /// </summary>
        [HttpGet]
        [Route("/metailas/{id}/enabling-objectives")]
        public async Task<IActionResult> GetEnablingObjectivesLinkedToMetaILAAsync(int id)
        {
            var result = await _metaIlaService.GetEnablingObjectivesLinkedToMetaILAAsync(id);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/metailas/{id}/nerccertdetail")]
        public async Task<IActionResult> GetMetaILANERCCertificationDetailsAsync(int id)
        {
            var result = await _metaIlaService.GetMetaILANERCCertificationDetailsAsync(id);
            return Ok(new { result });
        }
    }
}
