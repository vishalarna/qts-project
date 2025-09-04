using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.AdminMessage;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminMessageController : ControllerBase
    {
        private readonly IAdminMessageService _adminMessageService;
        private readonly IAdminMessage_QTDUserService _adminMessageQTDUserService;
        public AdminMessageController(IAdminMessageService adminMessageService, IAdminMessage_QTDUserService adminMessageQTDUserService)
        {
            _adminMessageService = adminMessageService;
            _adminMessageQTDUserService = adminMessageQTDUserService;
        }

        [HttpPost]
        [Route("/adminMessages")]
        public async Task<IActionResult> CreateAdminMessage(AdminMessageCreateOptions options)
        {
            await _adminMessageService.CreateAdminMessageAsync(options);
            return Ok();
        }

        [HttpGet]
        [Route("/adminMessages")]
        public async Task<IActionResult> GetAdminMessage()
        {
            var result = await _adminMessageQTDUserService.GetAdminMessagesAsync();
            return Ok(new {result});
        }

        [HttpPut]
        [Route("/adminMessages")]
        public async Task<IActionResult> UpdateAdminMessage(AdminMessageQTDUserUpdateOptions options)
        {
            var result = await _adminMessageQTDUserService.UpdateAdminMessagesAsync(options);
            return Ok(new { result });
        }
    }
}
