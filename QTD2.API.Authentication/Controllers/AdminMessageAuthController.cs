using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Authorization;
using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Infrastructure.Model.AdminMessageAuth;
using System.Threading.Tasks;

namespace QTD2.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminMessageAuthController : ControllerBase
    {

        private readonly IAdminMessageAuthService _adminMessageAuthService;
        public AdminMessageAuthController(IAdminMessageAuthService adminMessageAuthService) 
        {
            _adminMessageAuthService = adminMessageAuthService;
        }

        [HttpPost]
        [Route("/adminMessage")]
        public async Task<IActionResult> CreateAdminMessage(AdminMessageAuthCreateOptions options)
        {
            await _adminMessageAuthService.CreateAdminMessageAsyn(options);
            return Ok();
        }

        [HttpGet]
        [EnableCors("AllowQTD")]
        [Authorize(Policy = Policies.QtdSystem)]
        [Route("/adminMessage/{instance}")]
        public async Task<IActionResult> GetAdminMessages(string instance)
        {
            var result = await _adminMessageAuthService.GetAllAdminMessageForInstanceAsync(instance);
            return Ok(new {result});
        }

        [HttpPut]
        [EnableCors("AllowQTD")]
        [Authorize(Policy = Policies.QtdSystem)]
        [Route("/adminMessage/{instance}")]
        public async Task<IActionResult> UpdateAdminMessageAuthReceivedStatus(string instance, AdminMessageSourceIdOptions options)
        {
            var result = await _adminMessageAuthService.UpdateAdminMessageAuthReceivedStatusAsync(instance, options);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/adminMessage")]
        public async Task<IActionResult> GetAllAdminMessages()
        {
            var result = await _adminMessageAuthService.GetAllAdminMessageAsync();
            return Ok(new { result });
        }
    }
}
