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
        [HttpGet]
        [Route("/procedureReviewEmp/procedureReviews")]
        public async Task<IActionResult> GetEmployeeProcedureReviews()
        {
            var result = await _procedureReviewEmpService.GetEmpProcedureReviewsAsync();
            return Ok( new { result });
        }
        [HttpGet]
        [Route("/procedureReviewEmp/{procedureReviewId}")]
        public async Task<IActionResult> GetEmpProcedureReviewDataByIdAsync(int procedureReviewId)
        {
            var result = await _procedureReviewEmpService.GetEmpProcedureReviewDataByIdAsync(procedureReviewId);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/procedureReviewEmp/submit")]
        public async Task<IActionResult> SubmitProcedureReviewAsync(SubmitProcedureReviewDto submitOptions)
        {
            var result = await _procedureReviewEmpService.SubmitProcedureReviewAsync(submitOptions);
            return Ok( new { result });
        }
        //[HttpPost]
        //[Route("/procedureReviewEmp/{procedureReviewId}/cancelResponse/{response}/comments/{comments}")]
        //public async Task<IActionResult> CancelProcedureReviewAsync(int procedureReviewId, string response, string comments)
        //{
        //    var result = await _procedureReviewEmpService.CancelProcedureReviewAsync(procedureReviewId, response, comments);
        //    return Ok( new { result });
        //}
        [HttpPost]
        [Route("/procedureReviewEmp/exit")]
        public async Task<IActionResult> CancelProcedureReviewAsync(ProcedureReviewCancelOptions options)
        {
            var result = await _procedureReviewEmpService.CancelProcedureReviewAsync(options.procedureReviewId, options.response, options.comments);
            return Ok( new { result });
        }
    }
}
