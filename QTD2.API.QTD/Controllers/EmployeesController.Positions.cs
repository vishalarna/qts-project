using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.EmployeePosition;

namespace QTD2.API.QTD.Controllers
{
    public partial class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Gets a list of positions for a specific employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Http response code with employeePositions</returns>
        [HttpGet]
        [Route("/employees/positions/{employeeId}/{filter}")]
        public async Task<IActionResult> GetPositionsAsync(int employeeId,string filter)
        {
            var employeePositions = await _employeeService.GetPositionsAsync(employeeId, filter);
            return Ok( new { employeePositions });
        }

        /// <summary>
        /// Assigns an employee a position
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with employeePosition</returns>
        [HttpPost]
        [Route("/employees/{employeeId}/positions")]
        public async Task<IActionResult> CreatePositionAsync(int employeeId, EmployeePositionCreateOptions options)
        {
            var employeePosition = await _employeeService.AddPositionAsync(employeeId, options);
            //var employee = _employeeService.GetAsync(employeeId);
            //if (options.IsSignificant)
            //{
            //    await _employeeTaskService.CreateAsync(
            //        new EmployeeTaskCreateOptions
            //        {
            //            EmployeeId = employee.Id,
            //            PositionId = employeePosition.PositionId,
            //        });
            //}

            return Ok( new { employeePosition, message = _localizer["empPosCreated"] });
        }

        /// <summary>
        /// Updates specific position for a specific employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="positionId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with employeePosition</returns>
        [HttpPut]
        [Route("/employees/{employeeId}/positions/{positionId}")]
        public async Task<IActionResult> EditPositionAsync(int employeeId, int positionId, EmployeePositionUpdateOptions options)
        {
            var employeePosition = await _employeeService.EditPositionAsync(employeeId, positionId, options);
            return Ok( new { employeePosition, message = _localizer["empPosUpdated"] });
        }

        /// <summary>
        /// Removes a position from an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="positionId"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/employees/{employeeId}/positions/{positionId}/{empPosId}")]
        public async Task<IActionResult> DeletePositionAsync(int employeeId, int positionId, int empPosId)
        {
            var empPos = await _employeeService.DeletePositionAsync(employeeId, positionId,empPosId);
            //var employee = await _employeeService.GetAsync(employeeId);
            //foreach (var item in empPos.Position.Position_Tasks)
            //{
            //    await _employeeTaskService.ArchiveEmployeeTaskAsync(item.TaskId, employee.Id, item.PositionId);
            //}

            return Ok( new { message = _localizer["empPositionDeleted"] });
        }
        [HttpGet]
        [Route("/employees/{employeeId}/positions/{positionId}/{empPosId}")]
        public async Task<IActionResult> GetPositionByEmployeeAndPositionIdAsync(int employeeId, int positionId,int empPosId)
        {
            var employeePositions = await _employeeService.GetPositionsByPositionaAndEmployeeIdAsync(employeeId,positionId, empPosId);
            return Ok( new { employeePositions });
        }
    }
}
