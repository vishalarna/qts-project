using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QTD2.API.Authentication.Model.Authentication;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Exceptions;

namespace QTD2.API.Authentication.Controllers
{
    public partial class UsersController : ControllerBase
    {
        /// <summary>
        /// Resets the password using a reset-token
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/users/password")]
        public async Task<IActionResult> ResetPasswordAsync(CreatePasswordViewModel vm)
        {
            var user = await _userSharedService.GetAsync(vm.Email);

            if (user == null)
            {
                // TODO: error message
                throw new QTDServerException(_localization["InvalidUser"].Name,false);
            }

            var result = await _signInManager.UserManager.PasswordValidators.First().ValidateAsync(_userManager, user, vm.Password);
            if (!result.Succeeded)
            {
                // TODO:
                // return error
                throw new QTDServerException(string.Join(" ", result.Errors.Select(x=>x.Description)),false);
            }

            var resetPasswordToken = _userService.DecodeResetPasswordToken(vm.Token)[1];
            result = await _signInManager.UserManager.ResetPasswordAsync(user, resetPasswordToken, vm.Password);

            if (result.Succeeded)
            {
                user.LockoutEnd = null;
                user.AccessFailedCount = 0;
                await _signInManager.UserManager.UpdateAsync(user);
            } else {
                throw new QTDServerException(string.Join(" ", result.Errors.Select(r => r.Description)),false);
            }

            return Ok(new { message = _localization["PasswordResetSuccess"].Name });
        }

        /// <summary>
        /// Verify reset-token is expired or not, for resetting password
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/users/password/expiration")]
        public async Task<IActionResult> VerifyResetTokenExpiration(CreatePasswordViewModel vm)
        {
            bool isValid = _userService.IsResetPasswordTokenValidAsync(vm.Token);

            if (!isValid)
            {
                throw new QTDServerException("Password reset token is invalid.",false,System.Net.HttpStatusCode.Unauthorized);
            }

            return Ok();
        }

        /// <summary>
        /// Changes the password of an authenticated used, confirming the existing password
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("/users/password")]
        public async Task<IActionResult> UpdatePasswordAsync(CreatePasswordViewModel vm)
        {
            var user = await _userSharedService.GetAsync(vm.Email);

            if (user == null)
            {
                throw new BadHttpRequestException(_localization["InvalidToken"].Name, StatusCodes.Status400BadRequest);
            }

            var validPass = await _userManager.CheckPasswordAsync(user, vm.OldPassword);

            if (!validPass)
            {
                throw new BadHttpRequestException(_localization["InvalidOldPass"].Name, StatusCodes.Status400BadRequest);
            }

            var validationErrs = new List<IdentityError>();

            _userManager.PasswordValidators.ToList().ForEach(async x =>
            {
                var validation = await x.ValidateAsync(_userManager, user, vm.Password);
                validationErrs.AddRange(validation.Errors);
            });

            if (validationErrs.Any())
            {
                throw new BadHttpRequestException(string.Join(',', validationErrs), StatusCodes.Status400BadRequest);
            }

            var result = await _userManager.RemovePasswordAsync(user);

            if (!result.Succeeded)
            {
                throw new BadHttpRequestException(_localization["sysErr"].Name, StatusCodes.Status400BadRequest);
            }

            result = await _userManager.AddPasswordAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                throw new BadHttpRequestException(_localization["sysErr"].Name, StatusCodes.Status400BadRequest);
            }

            return Ok(new { message = _localization["PasswordUpdateSuccess"].Name });
        }
    }
}
