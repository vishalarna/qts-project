using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QTD2.Web.Authentication.Models;
using QTD2.Application.Interfaces.Services.Shared;
using Microsoft.Extensions.Options; 
using QTD2.Infrastructure.Identity.Interfaces;
using Microsoft.Extensions.Localization;

namespace QTD2.Web.Authentication.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        SignInManager<Data.Entities.Authentication.AppUser> _signInManager;
        INotificationService _notificationService;
        QTD2.Application.Interfaces.Services.Authentication.IUserService _userService;
        Application.Settings.DomainSettings _domainSettings;
        IIdentityBuilder _identityBuilder;
        IStringLocalizer<LoginController> _stringLocalizer;
        public LoginController(
                SignInManager<Data.Entities.Authentication.AppUser> signInManager,
                INotificationService notificationService,
                QTD2.Application.Interfaces.Services.Authentication.IUserService userService,
                IOptions<Application.Settings.DomainSettings> domainSettings,
                 IIdentityBuilder identityBuilder,
                 IStringLocalizer<LoginController> stringLocalizer)

        {
            _signInManager = signInManager;
            _notificationService = notificationService;
            _userService = userService;
            _domainSettings = domainSettings.Value;
            _identityBuilder = identityBuilder;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index(string redirect = null)
        {
            return View(new LoginModel(redirect));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = _signInManager.UserManager.FindByEmailAsync(model.UserName).Result;
            var signinResult = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, true);

            if (signinResult.RequiresTwoFactor)
            {
                //Return 2FA request Page
                //Send Auth Email               
                var token = _userService.Generate2FAToken(user);
                _notificationService.Send2FANotification(user, token);

                return RedirectToAction("Authentication", new { email = model.UserName });

            }
            else if (signinResult.Succeeded)
            {
                _identityBuilder.Build();

                if (!string.IsNullOrEmpty(model.Redirect))
                {
                    return Redirect(model.Redirect);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                //back to login screen with error
                ModelState.AddModelError("", _stringLocalizer["InvalidLogin"]);
                return View("Index", model);
            }
        }

        public IActionResult Signout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Login");
        }


        //public async Task<IActionResult> SetPassword(Models.LoginModel model)
        //{
        //    return View("Index", model);
        //}

        //[Authorize]
        //public async Task<IActionResult> Success(string redirect)
        //{
        //    Identity.AuthenticationIdentity identity = new Identity.AuthenticationIdentity(HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor, redirect);
        //    await HttpContext.SignInAsync("Identity.Application", identity.ClaimsPrincipal);

        //    return Redirect(identity.Destination);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> RequestTwoFactorCode()
        //{
        //    //send notification based on some rules
        //    //i.e. -> no email ? use sms :  vice versa:  if both?  preference?  select?

        //    throw new NotImplementedException();

        //    //_notificationService.Send2FANotification(null);
        //}

        public IActionResult AuthenticationHelp()
        {
            return View();
        }

        public IActionResult Authentication(string email)
        {
            var vm = new AuthenticationViewModel()
            {
                UserName = email
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Authentication(string submitaction, AuthenticationViewModel vm)
        {
            var user = _signInManager.UserManager.FindByEmailAsync(vm.UserName).Result;

            if (submitaction == "verify")
            {
                if (string.IsNullOrWhiteSpace(vm.VerificationCode))
                {
                    ModelState.AddModelError("", _stringLocalizer["InvalidToken"]);
                }
                else
                {
                    var validtoken = _signInManager.UserManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, vm.VerificationCode).Result;
                    if (validtoken)
                    {
                        _signInManager.SignInAsync(user, true).Wait();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", _stringLocalizer["InvalidToken"]);
                    }
                }

            }
            else if (submitaction == "resend")
            {
                var token = _userService.Generate2FAToken(user);
                _notificationService.Send2FANotification(user, token);

                ViewData.Add("Message", _stringLocalizer["ResendSuccess"]);
            }

            return View(vm);
        }

    }
}
