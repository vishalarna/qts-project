
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SubdutyArea;
using QTD2.Infrastructure.Model.SubDutyArea_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class DutyAreaController : ControllerBase
    {

        [HttpGet]
        [Route("/dutyAreas/subdutyAreas/all")]
        public async Task<IActionResult> GetAllSubDutyAreas()
        {
            var result = await _taskService.getAllSubDutyAreas();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a subduty area associated with the duty area
        /// </summary>
        /// <param name="id">Duty Area Id</param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/dutyAreas/{id}/subdutyAreas")]
        public async Task<IActionResult> AddSubDutyAreaAsync(int id, SubdutyAreaCreateOptions options)
        {
            var sda = await _taskService.CreateSubDutyAreaAsync(id, options);
            var histOptions = new SubDutyArea_HistoryCreateOptions();
            histOptions.ChangeEffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.ChangeNotes = options.ReasonForRevision;
            histOptions.SubDutyAreaId = sda.Id;
            await _subDutyArea_HistoryService.CreateAsync(histOptions);
            return Ok( new { sda, message = _localization["SubdutyAreaCreated"] });
        }

        [HttpGet]
        [Route("/dutyAreas/{daId}/sdas")]
        public async Task<IActionResult> GetSDAWithNumberBydaId(int daId)
        {
            var result = await _taskService.GetSDAWithNumberBydaId(daId);
            return Ok( new { result });
        }


        /// <summary>
        /// Get subduty areas associated with the duty area
        /// </summary>
        /// <param name="id">Duty Area Id</param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/dutyAreas/{id}/subdutyAreas")]
        public async Task<IActionResult> GetSubDutyAreaAsync(int id)
        {
            var sda = await _taskService.GetSubDutyAreas(id);
            return Ok( new { sda });
        }


        /// <summary>
        /// Get subduty areas by id
        /// </summary>
        /// <param name="id">Sub Duty Area Id</param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/dutyAreas/subdutyArea/{id}")]
        public async Task<IActionResult> GetSubDutyAreaByIdAsync(int id)
        {
            var sda = await _taskService.GetSubDutyArea(id);
            return Ok( new { sda });
        }

        /// <summary>
        /// Check if any tasks with this subduty area have any links
        /// </summary>
        /// <param name="sdaId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/dutyAreas/subdutyArea/{sdaId}/links")]
        public async Task<IActionResult> SDAHasTaskWithLinks(int sdaId)
        {
            var result = await _taskService.SDAHasTaskWithLinks(sdaId);
            return Ok( new { result });
        }


        /// <summary>
        /// Get new subduty area number with the duty area
        /// </summary>
        /// <param name="id">Duty Area Id</param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/dutyAreas/{id}/subdutyAreas/number")]
        public async Task<IActionResult> GetSubDutyAreaNumberAsync(int id)
        {
            var sda = await _taskService.GetSubDutyAreas(id);
            int number = sda.OrderByDescending(x => x.SubNumber).FirstOrDefault()?.SubNumber ?? 0;
            number++;
            return Ok( new { number });
        }


        /// <summary>
        /// Updates a sub duty area
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/dutyAreas/{id}/subdutyAreas")]
        public async Task<IActionResult> UpdateSubDutyAreaAsync(int id, SubdutyAreaUpdateOptions options)
        {
            var sda = await _taskService.UpdateSubDutyAreaAsync(id, options);
            var histOptions = new SubDutyArea_HistoryCreateOptions();
            histOptions.ChangeEffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.ChangeNotes = options.ReasonForRevision;
            histOptions.SubDutyAreaId = sda.Id;
            await _subDutyArea_HistoryService.CreateAsync(histOptions);
            return Ok( new { sda, message = _localization["SubDutyAreaUpdated"].Value });
        }


        /// <summary>
        /// Deletes, Inactive or active  a specific SubDutyArea by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/dutyAreas/{id}/subdutyAreas")]
        public async Task<IActionResult> DeleteSUbdutyAreaAsync(int id, SubDutyAreaOptions options)
        {
            var histOptions = new SubDutyArea_HistoryCreateOptions();
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _taskService.DeactivateSubDutyAreaAsync(id);
                    break;
                case "active":
                    await _taskService.ActivateSubDutyAreaAsync(id);
                    break;
                case "delete":
                    await _taskService.DeleteSubDutyAreaAsync(id);
                    break;
            }
            histOptions.ChangeEffectiveDate = (DateTime)options.ChangeEffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.SubDutyAreaId = id;
            await _subDutyArea_HistoryService.CreateAsync(histOptions);
            return Ok( new { message = _localization[$"SubDutyArea-{options.ActionType.ToLower()}"].Value });
        }
    }
}
