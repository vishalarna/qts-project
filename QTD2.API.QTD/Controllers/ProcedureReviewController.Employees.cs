using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ProcedureReview;
using QTD2.Infrastructure.Model.ProcedureReview_Employee;

namespace QTD2.API.QTD.Controllers
{
    public partial class ProcedureReviewController : ControllerBase
    {
        [HttpPost]
        [Route("/procedureReview/{id}/emp")]
        public async Task<IActionResult> LinkEmployee(int id, ProcedureReview_EmployeeCreateOptions options)
        {
            var result = await _procedureReviewService.LinkEmployee(id, options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/procedureReview/{id}/emp")]
        public async Task<IActionResult> GetLinkedEmployees(int id)
        {
            var result = await _procedureReviewService.GetLinkedEmployees(id);
            return Ok( new { result });
        }


        [HttpDelete]
        [Route("/procedureReview/{procedureReviewId}/emp/")]
        public async Task<IActionResult> UnlinkEmployee(int procedureReviewId, ProcedureReview_EmployeeCreateOptions options)
        {
            await _procedureReviewService.UnlinkEmployee(procedureReviewId, options.employeeIds);
            return Ok( new { message = _localier["EmployeeUnlinked"].Value });
        }

        [HttpPut]
        [Route("/procedureReview/{id}/response/emp/")]
        public async Task<IActionResult> UpdateResponseAsync(int id, ProcedureReviewResponseCreateOptions options)
        {
            var result = await _procedureReviewService.UpdateResponseAsync(id, options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/procedureReview/{id}/comments/emp/")]
        public async Task<IActionResult> UpdateCommentsAsync(int id, ProcedureReviewResponseCreateOptions options)
        {
            var result = await _procedureReviewService.UpdateCommentsAsync(id, options);
            return Ok( new { result });
        }
        [HttpGet]
        [Route("/procedureReview/draft")]
        public async Task<IActionResult> GetDraftProcedureReviews()
        {
            var result = await _procedureReviewService.GetDraftsProcedureReviews();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/procedureReview/pending")]
        public async Task<IActionResult> GetEmployeePendingtProcedureReviews()
        {
            var result = await _procedureReviewService.GetEmployeesPendingProcedureReviews();
            return Ok( new { result });
        }
    }
}
