using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;

namespace QTD2.Web.Authentication.Controllers
{
    public class PasswordController : Controller
    {
        SignInManager<Data.Entities.Authentication.AppUser> _signInManager;
        UserManager<Data.Entities.Authentication.AppUser> _userManager;
        IStringLocalizer<PasswordController> _stringLocalizer;
        public PasswordController(SignInManager<Data.Entities.Authentication.AppUser> signInManager, IStringLocalizer<PasswordController> stringLocalizer)
        {
            _signInManager = signInManager;
            _userManager = _signInManager.UserManager;
            _stringLocalizer = stringLocalizer;
        }

        [HttpPost]
        public IActionResult Reset(Models.CreatePasswordViewModel model)
        {
            var user = _userManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                return new JsonResult(new Models.PostResult(_stringLocalizer["UserNotFound"]));
            }
            if (model.Password != model.ConfirmPassword)
            {
                return new JsonResult(new Models.PostResult(_stringLocalizer["PasswordDon'tMatch"]));
            }

            var passwordValid = _userManager.PasswordValidators.First().ValidateAsync(_userManager, user, model.Password).Result;

            if (!passwordValid.Succeeded)
            {
                return new JsonResult(new Models.PostResult(string.Join(Environment.NewLine, passwordValid.Errors.Select(x => x.Description))));
            }

            var result = _signInManager.UserManager.ResetPasswordAsync(user, model.Token, model.Password).Result;

            if (!result.Succeeded)
            {
                return new JsonResult(new Models.PostResult(string.Join(System.Environment.NewLine, result.Errors.Select(x => x.Description))));
            }
            else
            {
                return new JsonResult(new Models.PostResult());
            }
        }

        [HttpPost]
        public IActionResult Create(Models.CreatePasswordViewModel model)
        {
            throw new NotImplementedException();

            // _userManager.AddPasswordAsync()
        }

        [HttpPost]
        public IActionResult Change(Models.CreatePasswordViewModel model)
        {
            throw new NotImplementedException();

            //if user authenticated
            //get user, use that in change password, not the one from the model

            //if not -> 401

            //_userManager.ChangePasswordAsync
        }
    }
}

