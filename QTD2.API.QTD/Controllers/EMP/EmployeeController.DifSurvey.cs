using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.DIFSurvey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.EMP
{
    public partial class EmployeeController : EMPController
    {
        [HttpGet]
        [Route("/emp/difSurvey/completed")]
        public async Task<IActionResult> GetEmployeeCompletedSurveys()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _dIFSurveyService.GetCompletedSurveyByEmpId(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/difSurvey/pending")]
        public async Task<IActionResult> GetEmployeePendingSurveys()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _dIFSurveyService.GetPendingSurveyByEmpId(employeeId);
            return Ok( new { result });
        }


        [HttpGet]
        [Route("/emp/difSurvey/{id}/employeeResponses")]
        public async Task<IActionResult> GetEmployeeResponses(int id)
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _dIFSurveyService.GetEmployeeResponsesByEmpId(employeeId,id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/emp/difSurvey/{id}/employeeResponses/update")]
        public async Task<IActionResult> CreateEmployeeResponses(int id, DIFSurveyEmployeeResponseOptions options)
        {
            var employeeId = await GetEmployeeIdAsync();
            await _dIFSurveyService.CreateUpdateEmployeeResponsesAsync(id, employeeId, options,false);
            return Ok( new { message = _localizer["difSurveyEmpResponseUpdated"] });
        }

        [HttpPost]
        [Route("/emp/difSurvey/{id}/employeeResponses/complete")]
        public async Task<IActionResult> CompleteEmployeeResponses(int id, DIFSurveyEmployeeResponseOptions options)
        {
            var employeeId = await GetEmployeeIdAsync();
            await _dIFSurveyService.CreateUpdateEmployeeResponsesAsync(id, employeeId, options,true);
            return Ok( new { message = _localizer["difSurveyEmpResponseUpdated"] });
        }
    }
}
