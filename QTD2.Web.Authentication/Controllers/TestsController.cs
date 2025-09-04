using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using QTD2.Application.Attributes;
using QTD2.Application.Interfaces;
using QTD2.Data.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;

namespace QTD2.Web.Authentication.Controllers
{
    [TestControllerAttribute]
    public class TestsController : Controller
    {
        INotificationService _notificationService;
        UserManager<AppUser> _userManager;
        QTD2.Application.Interfaces.Services.Authentication.IUserService _userService;
        IStringLocalizer<TestsController> _stringLocalizer;

        public TestsController(
                INotificationService notificationService,
                UserManager<AppUser> userManager,
                QTD2.Application.Interfaces.Services.Authentication.IUserService userService,
                IStringLocalizer<TestsController> stringLocalizer
                )
        {
            _notificationService = notificationService;
            _userManager = userManager;
            _userService = userService;
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Emails()
        {
            ViewBag.Hello = _stringLocalizer["Hello"];
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(string method)
        {
            var user = _userManager.FindByEmailAsync("nickolas@qualitytrainingsystems.com").Result;
            string token = _userService.Generate2FAToken(user);

            _notificationService.Send2FANotification(user, token);
            return new JsonResult(true);
        }
    }
}
