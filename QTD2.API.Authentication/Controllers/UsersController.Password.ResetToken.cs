using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Exceptions;

namespace QTD2.API.Authentication.Controllers
{
    public partial class UsersController : ControllerBase
    {
        /// <summary>
        /// Creates a new password token reset and sends fogot password email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("/users/password/reset-token")]
        public async Task<IActionResult> RequestForgotPassword(string email)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(email);

            if (user == null)
            {
                // do not let the user know the user wasn't found
                // vector for attacks
                return Ok(new { message = "notificationsent" });
            }
            var identityProvider = await _identityProviderService.GetUserIdentityProviderByUsername(email);
            if (identityProvider != null && identityProvider.Name != null && !string.Equals(identityProvider.Type, "Password", StringComparison.OrdinalIgnoreCase))
            {
                throw new QTDServerException("That User is managed by a third party, please attempt to login again", false, System.Net.HttpStatusCode.Unauthorized);
            }
            string resetPasswordUrl = await _userService.GenerateResetPasswordUrlAsync(user);
            bool resetNotification = await _notificationService.SendResetPasswordNotificationAsync(user.Email, resetPasswordUrl);
            if (resetNotification)
            {
                return Ok( new { message = "notificationsent" });
            }
            else
            { 
                throw new QTDServerException("Something is wrong, Please contact support!", false, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
