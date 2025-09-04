using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA_EnablingObjective_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the EnablingObjective with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/enablingObjective")]
        public async Task<IActionResult> LinkEnablingObjectiveAsync(int id, ILA_EnablingObjective_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.EnablingObjectiveIds.Length.ToString() + " Enabling Objectives Linked", DateTime.Now, 1);
            var result = await _ilaService.LinkEnablingObjectiveAsync(id, options);
            await _ilaService.ReorderObjectiveLinks(id);
            return Ok( new { message = _localizer["EnablingObjectiveslinkedToILA"].Value });
        }

        /// <summary>
        /// Unlinks the EnablingObjective with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/enablingObjective")]
        public async Task<IActionResult> UnlinkEnablingObjectiveAsync(int id, ILA_EnablingObjective_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.EnablingObjectiveIds.Length.ToString() + " Enabling Objectives Unlinked", DateTime.Now, 0);
            await _ilaService.UnlinkEnablingObjectiveAsync(id, options);
            return Ok( new { message = _localizer["EnablingObjectivesUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the EnablingObjectives linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked EnablingObjectives</returns>
        [HttpGet]
        [Route("/ila/{id}/enablingObjective")]
        public async Task<IActionResult> GetLinkedEnablingObjectiveAsync(int id)
        {
                var result = await _ilaService.GetLinkedEnablingObjectivesAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all the EnablingObjectives linked to an ila along with the EnablingObjective_Categories
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/{id}/enablingObjective/enablingObjective_Category")]
        public async Task<IActionResult> GetLinkedEOWithCategories(int id)
        {
            var result = await _ilaService.GetLinkedEOWithCategories(id);
            return Ok( new { result });
        }


        [HttpPut]
        [Route("/ila/enroll")]
        public async Task<IActionResult> EnrollEmployeeToScheduleClass(ILAEmployeeEnrollOption options)
        {
            var result = await _ilaService.EnrollEmployee(options);
            return Ok( new { result });
        }
        
        
        [HttpGet]
        [Route("/ila/UnEnroll/{id}/ScheduleClasses/{empId}")]
        public async Task<IActionResult> UnEnrollEmployeeToScheduleClass(int id,int empId)
        {
            var result = await _ilaService.UnEnrollEmployee(id, empId);
            return Ok( new { result });
        }
    }
}
