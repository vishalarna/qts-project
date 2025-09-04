using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.EMP
{
    public partial class EmployeeController : EMPController
    {
        [HttpGet]
        [Route("/emp/empTaskQualification/pending")]
        public async Task<IActionResult> GetEmployeePendingTaskRequalification()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _taskRequalService.GetPendingTaskRequalificationByEmpId(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/empTaskQualification/completed/isEvaluator/{isEvaluator}")]
        public async Task<IActionResult> GetCompletedTaskRequalification(bool isEvaluator)
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _empSignOffService.GetCompletedTaskRequalificationsByEmpId(isEvaluator, employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/empTaskQualification/trainee/pending")]
        public async Task<IActionResult> GetPendingTaskQualificationsAsTraineeAsync()
        {
            var employeeId = await GetEmployeeIdAsync(); 
            var result = await _taskRequalService.GetPendingTaskQualificationsAsTraineeAsync(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/empTaskQualification/trainee/completed")]
        public async Task<IActionResult> GetCompletedTaskQualificationsAsTraineeAsync()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _taskRequalService.GetCompletedTaskQualificationsAsTraineeAsync(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/empTaskQualification/evaluator/completed")]
        public async Task<IActionResult> GetCompletedTaskQualificationsAsEvaluatorAsync()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _taskRequalService.GetCompletedTaskQualificationsAsEvaluatorAsync(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/empTaskQualification/tqevaluator")]
        public async Task<IActionResult> GetTQEvaluatorBitAsync()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _taskRequalService.GetTQEvaluatorBitAsync(employeeId);
            return Ok( new { result });
        }
    }
}
