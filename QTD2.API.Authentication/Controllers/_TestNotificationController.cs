using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.API.Authentication.Controllers
{
    [Application.Attributes.TestController]
    public class _TestNotificationController : Controller
    {
        IUserService _userSharedService;
        Application.Interfaces.Services.Authentication.INotificationService _notificationService;

        public _TestNotificationController(
            Application.Interfaces.Services.Authentication.INotificationService notificationService,
            IUserService userSharedService
            )
        {
            _notificationService = notificationService;
            _userSharedService = userSharedService;
        }

        [HttpPost]
        [Route("/TestEmail/TwoFANotification")]
        public async Task<IActionResult> TwoFANotification([FromBody]string email)
        {
            string verificationToken = "Some test token";
            await _notificationService.Send2FANotificationAsync(email, verificationToken);

            return Ok();
        }

        [HttpPost]
        [Route("TestEmail/ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            string url = "www.qts.com/forgotpassword?sometken=kayyyasdasd";
            await _notificationService.SendResetPasswordNotificationAsync(email, url);

            return Ok();
        }
    }
}
