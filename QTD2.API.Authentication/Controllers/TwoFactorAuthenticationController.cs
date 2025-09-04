using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.API.Authentication.Model.Authentication;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoFactorAuthenticationController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly Application.Interfaces.Services.Authentication.IUserService _userService;
        private readonly IUserService _userSharedService;
        private readonly Application.Interfaces.Services.Authentication.INotificationService _notificationService;
        private readonly IStringLocalizer<TwoFactorAuthenticationController> _localization;

        public TwoFactorAuthenticationController(
            SignInManager<AppUser> signInManager,
            IUserService userSharedService,
            Application.Interfaces.Services.Authentication.IUserService userService,
            Application.Interfaces.Services.Authentication.INotificationService notificationService,
            IStringLocalizer<TwoFactorAuthenticationController> localization)
        {
            _signInManager = signInManager;
            _userService = userService;
            _userSharedService = userSharedService;
            _notificationService = notificationService;
            _localization = localization;
        }

        /// <summary>
        /// Requests a new 2FA Token and sends it to the user
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/twoFactorAuthentication")]
        public async Task<IActionResult> Send2FATokenAsync(AuthenticationViewModel vm)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(vm.UserName);

            if (user == null)
            {
                return BadRequest(new { message = _localization["UserNotFound"].Name });
            }

            var token = await _userService.Generate2FATokenAsync(user);

            var result = await _notificationService.Send2FANotificationAsync(user.Email, token);

            if (result)
            {
                return Ok(new { message = _localization["ResendSuccess"].Name });
            }
            else
            {
                return BadRequest( new { message = _localization["sysErr"].Name });
            }
        }

        /// <summary>
        /// Validates the 2FA Token supplied by the client
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Http Response Code with message along token if token validates</returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("/twoFactorAuthentication")]
        public async Task<IActionResult> Verify2FAAsync(AuthenticationViewModel vm)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(HttpContext.User.Identity.Name);

            if (user == null)
            {
                return BadRequest(new { message = _localization["UserNotFound"].Name });
            }

            if (string.IsNullOrWhiteSpace(vm.VerificationCode))
            {
                return BadRequest(new { message = _localization["InvalidToken"].Name });
            }

            var validtoken = await _signInManager.UserManager.VerifyTwoFactorTokenAsync(user, "CustomMfaTokenProvider", vm.VerificationCode);
            if (!validtoken)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = _localization["InvalidToken"].Name });
            }
            if (vm.DoNotAsk == true)
            {
                string clientIp = GetClientIp(HttpContext);
                string trustRecord = $"{clientIp}|{DateTime.UtcNow:o}";

                var claims = await _signInManager.UserManager.GetClaimsAsync(user);
                var existingClaims = claims.Where(c => c.Type == CustomClaimTypes.TfaTrustedDevice).ToList();

                if (existingClaims.Any())
                {
                    await _signInManager.UserManager.RemoveClaimsAsync(user, existingClaims);
                }
                await _signInManager.UserManager.AddClaimAsync(user, new Claim(CustomClaimTypes.TfaTrustedDevice, trustRecord));

               
            }
            var authTokenOptions = new ClaimsBuilderOptions(false, true);
            var refreshTokenOptions = new ClaimsBuilderOptions(true, true);

            var authToken = await _userSharedService.GetJwtAsync(authTokenOptions, user);
            var refreshToken = await _userSharedService.GetJwtAsync(refreshTokenOptions, user);

            return Ok(new
            {
                refreshToken = refreshToken,
                authToken = authToken,
                message = _localization["LoginSuccess"].Name,
            });

        }

        private string GetClientIp(HttpContext httpContext)
        {
            string clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            if (clientIp == "::1" || clientIp == "127.0.0.1")  // Localhost Fix
            {
                // Check the X-Forwarded-For Header (used by proxies/load balancers)
                if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    clientIp = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                }
            }

            return clientIp;
        }

    }
}
