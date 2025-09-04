using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.NercStandardMember;

namespace QTD2.API.QTD.Controllers
{
    public partial class NercStandardController : ControllerBase
    {
        /// <summary>
        /// Gets a list of NercStandardMembers
        /// </summary>
        /// <returns>Http Response Code with NercStandardMembers</returns>
        [HttpGet]
        [Route("/nercStandards/{id}/members")]
        public async Task<IActionResult> GetNercStandardMembersAsync(int id)
        {
            var result = await _nercStandardMemberService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new NercStandardMember
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/nercStandards/{id}/members")]
        public async Task<IActionResult> CreateNercStandardMemberAsync(int id, NercStandardMemberCreateOptions options)
        {
            var result = await _nercStandardMemberService.CreateAsync(id, options);
            return Ok( new { message = _localizer["NercStandardMemberCreated"].Value });
        }

        /// <summary>
        /// Gets a specific NercStandardMember by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberId"></param>
        /// <returns>Http Response Code with NercStandardMember</returns>
        [HttpGet]
        [Route("/nercStandards/{id}/members/{memberId}")]
        public async Task<IActionResult> GetNercStandardMemberAsync(int id, int memberId)
        {
            var result = await _nercStandardMemberService.GetAsync(id, memberId);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific NercStandardMember by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/nercStandards/{id}/members/{memberId}")]
        public async Task<IActionResult> UpdateNercStandardMemberAsync(int id, int memberId, NercStandardMemberUpdateOptions options)
        {
            var result = await _nercStandardMemberService.UpdateAsync(id, memberId, options);
            return Ok( new { message = _localizer["NercStandardMemberUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific NercStandardMember by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/nercStandards/{id}/members/{memberId}")]
        public async Task<IActionResult> DeleteNercStandardMemberAsync(int id, int memberId, NercStandardMemberOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _nercStandardMemberService.InActiveAsync(id, memberId);
                    break;
                case "active":
                    await _nercStandardMemberService.ActiveAsync(id, memberId);
                    break;
                case "delete":
                    await _nercStandardMemberService.DeleteAsync(id, memberId);
                    break;
            }

            return Ok( new { message = _localizer[$"NercStandardMember-{options.ActionType.ToLower()}"].Value });
        }
    }
}
