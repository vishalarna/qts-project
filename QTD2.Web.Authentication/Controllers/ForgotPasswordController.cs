using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using QTD2.Web.Authentication.Models;
using Microsoft.Extensions.Options;
using QTD2.Application.Interfaces.Services.Shared;
using Microsoft.Extensions.Localization;

namespace QTD2.Web.Authentication.Controllers
{
    [AllowAnonymous]
    public class ForgotPasswordController : Controller
    {
        SignInManager<Data.Entities.Authentication.AppUser> _signInManager;
        Application.Settings.DomainSettings _domainSettings;
        INotificationService _notificationService;
        QTD2.Application.Interfaces.Services.Authentication.IUserService _userService;
        IStringLocalizer<ForgotPasswordController> _stringLocalizer;
        public ForgotPasswordController(SignInManager<Data.Entities.Authentication.AppUser> signInManager, 
            INotificationService notificationService, 
            QTD2.Application.Interfaces.Services.Authentication.IUserService userService, 
            IOptions<Application.Settings.DomainSettings> domainSettings,
            IStringLocalizer<ForgotPasswordController> stringLocalizer)
        {
            _signInManager = signInManager;
            _domainSettings = domainSettings.Value;
            _notificationService = notificationService;
            _userService = userService;
            _stringLocalizer = stringLocalizer;
        }
        public IActionResult Index(string token = "")
        {
            if(String.IsNullOrEmpty(token))
            {
                //No token => requst password reset partial
                return View();
            }
            else
            {
                //get token from database
                //validate token
                    //invalid token? -> show error with request password reset partial
                //build model with token
                object model = new object();
                return View(model);
            }            
        }

        [HttpPost]
        public IActionResult RequestForgotPassword(string email)
        {
            string message = string.Empty;
            var user = _signInManager.UserManager.FindByEmailAsync(email).Result;

            if (user == null)
            {
                //TODO: error message
                return View();
            }

            try
            {
                string token = _userService.GenerateForgotPasswordToken(user);

                string baseDomain = _domainSettings.Authentication;
                _notificationService.SendResetPasswordNotification(user, $"{baseDomain}/ForgotPassword/CreatePassword?token={token}");
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            TempData.Add("Message", string.IsNullOrEmpty(message) ? "Email sent!" : message);
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult ResetPassword(object model)
        {
            //object should be changed to ResetPasswordModel
            //validate tokena again
            //validate password against password policy

            //all good?  return use to home page with success message.  they can click a destination from there and sign in
            throw new NotImplementedException();
        }


        public IActionResult CreatePassword(string token)
        {
            var vm = new CreatePasswordViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var user = _signInManager.UserManager.GetUserAsync(User).Result;
                vm.Email = user.Email;
            }
            
            if (!string.IsNullOrEmpty(token))
            {
                vm.Token = token;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreatePassword(CreatePasswordViewModel vm)
        {
            var user = _signInManager.UserManager.FindByEmailAsync(vm.Email).Result;

            var isvalidtoken = _signInManager.UserManager.VerifyUserTokenAsync(user, TokenOptions.DefaultEmailProvider, "forgot password", vm.Token).Result;

            if (!isvalidtoken)
            { 
                //TODO: 
                //return error
            }


            _signInManager.UserManager.RemovePasswordAsync(user).Wait();
            var result = _signInManager.UserManager.AddPasswordAsync(user, vm.Password).Result;

            if (!result.Succeeded)
            {
                TempData.Add("ErrorMessage", result.Errors.Any() ? result.Errors.First().Description : "Error");
                return View();
            }

            var status = _signInManager.SignInAsync(user, true).Status;

            TempData.Add("Message", _stringLocalizer["PasswordResetSuccess"]);
            return RedirectToAction("Index", "Home");

        }
    }
}
