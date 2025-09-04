using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.IDP;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IDPsController : ControllerBase
    {
        private readonly IStringLocalizer<IDPsController> _localizer;
        private readonly IIDPService _idpService;
        public IDPsController(
            IStringLocalizer<IDPsController> localizer,
            IIDPService idpService)
        {
            _localizer = localizer;
            _idpService = idpService;
        }

        /// <summary>
        /// Get All Idps for the current employee
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/idp/{empId}/{idpYear}")]
        public async Task<IActionResult> GetIdpsForEmployee(int empId, int idpYear)
        {
            var result = await _idpService.GetAllIDPs(empId, idpYear);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All IDP reviews for current employee
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/idp/review/{empId}")]
        public async Task<IActionResult> GetIDPReviewsForEMP(int empId)
        {
            var result = await _idpService.GetIDPReviewsForEMPAsync(empId);
            return Ok( new { result });
        }

        /// <summary>
        /// Create a new IDP Review
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/idp/review")]
        public async Task<IActionResult> CreateIDPReview(IDP_ReviewCreateOptions options)
        {
            var result = await _idpService.CreateIDPReviewAsync(options);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/idp/linkEmployee/ILA/{empId}")]
        public async Task<IActionResult> DownloadIdp(int empId,IdpTrainingLinkOptions options)
        {
            await _idpService.LinkIDPAsync(empId, options);
            return Ok( new { message="updated successfully" });
        }

        [HttpGet]
        [Route("/idp/{id}/IDPSchedule/{empId}")]
        public async Task<IActionResult> GetLinkedSchedulingClasses(int id, int empId)
        {
            var result = await _idpService.GetLinkedSchedulingClasses(id, empId);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/idp/schedule/{id}/IDPSchedule/Enroll/{empId}")]
        public async Task<IActionResult> EnrollEmployeeToClass(int id, int empId, EnrollEmployeeOptions options)
        {
            var result = await _idpService.EnrollEmployeeToClass(id, empId,options);
            return Ok( new { result });
        }
        
        [HttpPut]
        [Route("/idp/score")]
        public async Task<IActionResult> UpdateIDPScore(UpdateGradeOptions options)
        {
            var result =await _idpService.UpdateIDPGrade(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/idp/date")]
        public async Task<IActionResult> UpdateIDPDates(UpdateIDPScheduleDateOptions options)
        {
            await _idpService.UpdateIDPDate(options);
            return Ok( new { message="Score updated successfully" });
        }


        [HttpGet]
        [Route("/idp/ILA/{id}/UnEnroll/{empId}")]
        public async Task<IActionResult> UnEnrollEmployeeFromClass(int id, int empiId)
        {
            await _idpService.UnEnrollEmployeeFromIDP(id,empiId);
            return Ok( new { message = "IDP schedule updated successfully" });
        }

        [HttpDelete]
        [Route("/idp/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _idpService.DeleteAsync(id);
            return Ok( new { message = "IDP Deleted successfully" });
        }
    }
}
