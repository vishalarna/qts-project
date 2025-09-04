using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.SafetyHazard_Set;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class SafetyHazardSetController : ControllerBase
    {
        private readonly ISafetyHazard_SetService _safetyHazardSetService;
        private readonly IStringLocalizer<SafetyHazardSetController> _localizer;

        public SafetyHazardSetController(ISafetyHazard_SetService safetyHazardSetService, IStringLocalizer<SafetyHazardSetController> localizer)
        {
            _safetyHazardSetService = safetyHazardSetService;
            _localizer = localizer;
        }

        /// <summary>
        /// Get All Safety Hazard set Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazardSet")]
        public async Task<IActionResult> GetAllSafetyHazardSets()
        {
            var result = await _safetyHazardSetService.GetSafetyHazardSets();
            return Ok( new { result });
        }

        /// <summary>
        /// Get safety hazard set data for provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazardSet/{id}")]
        public async Task<IActionResult> GetSafetyHazardSet(int id)
        {
            var result = await _safetyHazardSetService.GetSafetyHazardSets(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all safety hazard sets for specific safety hazard
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazardSet/sh/{id}")]
        public async Task<IActionResult> GetSHSetWithSHId(int id)
        {
            var result = await _safetyHazardSetService.GetSetForSH(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Create Safety Hazard Set
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/saftyHazardSet")]
        public async Task<IActionResult> CreateSafetyHazardSet(SafetyHazard_SetCreateOptions options)
        {
            var result = await _safetyHazardSetService.CreateSafetyHazardSet(options);
            return Ok( new { result });
        }

        /// <summary>
        /// update existing safety hazard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/saftyHazardSet/{id}")]
        public async Task<IActionResult> UpdateSafetyHazardSet(int id, SafetyHazard_SetCreateOptions options)
        {
            var result = await _safetyHazardSetService.UpdateSafetyHazardSet(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Create Safety Hazard Set and Link it to Safety Hazard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/saftyHazardSet/{id}")]
        public async Task<IActionResult> CreateAndLinkWithSaftyHazard(int id, SafetyHazard_SetCreateOptions options)
        {
            var result = await _safetyHazardSetService.CreateAndLinkWithSH(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Delete, Activate or Inactivate Safety Hazard Set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/saftyHazardSet/{id}")]
        public async Task<IActionResult> DeleteSafetyHazardSet(int id, SafetyHazard_SetOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "delete":
                    await _safetyHazardSetService.DeleteSafetyHazardSet(id);
                    break;
            }

            return Ok( new { message = _localizer[$"SafetyHazard_Set-{options.ActionType.ToLower()}"].Value });
        }
    }
}
