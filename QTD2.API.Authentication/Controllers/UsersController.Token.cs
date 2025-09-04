using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QTD2.API.Authentication.Model.Authentication;
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.API.Authentication.Controllers
{
    public partial class UsersController : ControllerBase
    {
        /// <summary>
        /// obtains a new authentication token from the refresh token
        /// </summary>
        /// <returns>Http Response Code with message along JWT and its refresh token if token validates</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/users/token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenModel refreshTokenModel)
        {
            var handler = new JwtSecurityTokenHandler();
            var refreshToken = handler.ReadJwtToken(refreshTokenModel.RefreshToken);

            if (DateTime.UtcNow >= refreshToken.ValidTo)
            {
                throw new SecurityTokenInvalidLifetimeException("InvalidRefreshJWT"); // matching the message in angular response,will find an alternate later until then we can skip their localization
            }

            var usernameClaim = refreshToken.Claims.Where(r => r.Type == Domain.CustomClaimTypes.UserName).FirstOrDefault();

            if (usernameClaim == null)
            {
                return Unauthorized();
            }

            var authTokenJwt = refreshTokenModel.AuthToken;

            if (string.IsNullOrEmpty(authTokenJwt))
            {
                return Unauthorized();
            }

            var expiredAuthToken = handler.ReadJwtToken(authTokenJwt);

            var user = await _userService.GetUserWithClaimsAsync(usernameClaim.Value);
            if (user == null)
            {
                return Unauthorized();
            }

            var authTokenOptions = new ClaimsBuilderOptions(expiredAuthToken.Claims.Where(x => x.Type.StartsWith(Domain.CustomClaimTypes.Prefix)).ToList());

            var updatedAuthToken = await _userSharedService.GetJwtAsync(authTokenOptions, user);

            var refreshTokenOptions = new ClaimsBuilderOptions(true, true);
            var updatedRefreshToken = await _userSharedService.GetJwtAsync(refreshTokenOptions, user);

            // matching the message in angular response,will find an alternate later until then we can skip their localization
            return Ok(new { message = $"TokenRefreshed {DateTime.Now.Minute}", authToken = updatedAuthToken, refreshToken = updatedRefreshToken });
        }

        /// <summary>
        /// Alters the authentication token and issues a new one with updated claims
        /// </summary>
        /// <returns>Http Response Code with message</returns>
        [HttpPut]
        [Route("/users/token")]
        public async Task<IActionResult> ModifyTokenAsync(ModifyTokenModel model)
        {
            var currentInstanceClaim = HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();

            var user = await _signInManager.UserManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var authTokenOptions = new ClaimsBuilderOptions(false, true, model.SetInstanceName, model.SetUserName);
            var updatedAuthToken = await _userSharedService.GetJwtAsync(authTokenOptions, user);

            var refreshTokenOptions = new ClaimsBuilderOptions(true, true);
            var updatedRefreshToken = await _userSharedService.GetJwtAsync(refreshTokenOptions, user);

            if (currentInstanceClaim != null && String.IsNullOrEmpty(model.SetInstanceName))
            {
                await _eventLogService.AddLeaveInstanceEvent(user.Id, currentInstanceClaim);
            }
            else
            {
                await _eventLogService.AddSelectInstanceEvent(user.Id, model.SetInstanceName);
            }

            // matching the message in angular response,will find an alternate later until then we can skip their localization
            return Ok(new { message = $"TokenModified {DateTime.Now.Minute}", authToken = updatedAuthToken, refreshToken = updatedRefreshToken });
        }

        /// <summary>
        /// Revokes authentication token
        /// </summary>
        /// <returns>Http Response Code with message</returns>
        [HttpDelete]
        [Route("/users/token")]
        public IActionResult RevokeToken()
        {
            // var loggedInUser = Convert.ToString(_httpContextAccessor.HttpContext.Items["User"]);

            // if (string.IsNullOrEmpty(loggedInUser))
            //    return NoContent();

            // var user = await _userManager.FindByEmailAsync(loggedInUser);
            // var result = await _userManager.RemoveAuthenticationTokenAsync(user, "QTSAuth", "RefreshToken");

            // if (!result.Succeeded)
            //    await _userManager.UpdateSecurityStampAsync(user); // an alternative if removal of refresh token is not succeeded
            return Ok(new { message = "TokenRevoked" });
        }
    }
}
