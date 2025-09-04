using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Position_Employee;
using QTD2.Infrastructure.Model.PositionHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class PositionsController : ControllerBase
    {
        [HttpPost]
        [Route("/positions/{id}/emp")]
        public async Task<IActionResult> LinkEmployee(int id, Position_Employee_LinkCreateOptions options)
        {
            var result = await _positionService.LinkEmployee(id, options);

            //foreach (var item in options.EmployeeIds)
            //{
                await _positionhistoryService.CreateAsync(new Position_HistoryCreateOptions()
                {
                    ChangeNotes =options.EmployeeIds.Length + " Employee Linked to Position",
                    EffectiveDate = DateTime.Now,
                    PositionId = id,
                    taskIds = options.EmployeeIds
                });
            //

            return Ok( new { result });
        }

        /// <summary>
        /// Get Employee linked to position
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/positions/{id}/emp")]
        public async Task<IActionResult> GetLinkedEmployees(int id)
        {
            var result = await _positionService.GetLinkedEmployees(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all positions that the Employee is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/positions/emp/{id}")]
        public async Task<IActionResult> GetPosEmployeeIsLinkedTo(int id)
        {
            var result = await _positionService.GetPositionsEmployeeIsLinkedTo(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink specific Employees linked to position provided by positionId
        /// </summary>
        /// <param name="posId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/positions/{posId}/emp/")]
        public async Task<IActionResult> UnlinkEmployee(int posId, Position_Employee_LinkCreateOptions options)
        {
            await _positionService.UnlinkEmployee(posId, options.EmployeeIds);
            await _positionhistoryService.CreateAsync(new Position_HistoryCreateOptions()
            {
                ChangeNotes = options.EmployeeIds.Length + " Employee UnLinked from Position",
                EffectiveDate = DateTime.Now,
                PositionId = posId,
                taskIds = options.EmployeeIds
            });
            return Ok( new { message = _localier["EmployeeUnlinked"].Value });
        }
    }
}
