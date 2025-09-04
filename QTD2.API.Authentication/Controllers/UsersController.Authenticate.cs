using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.API.Authentication.Model.Authentication;
using QTD2.Infrastructure.Identity.Settings;
using Microsoft.AspNetCore.Authentication;
using System;
using Sustainsys.Saml2.AspNetCore2;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authentication.Cookies;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model.IdentityProvider;
using QTD2.Domain.Exceptions;
using System.Linq;
using QTD2.Domain;

namespace QTD2.API.Authentication.Controllers
{
    public partial class UsersController : ControllerBase
    {
        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Http Response Code with message along token if token validates</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/users/authenticate")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            var user = await _signInManager.UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new QTDServerException("Invalid username or password. Try clicking 'Forgot Password' if you're having trouble signing in.", false, System.Net.HttpStatusCode.Unauthorized);
            }

            var identityProvider = await _identityProviderService.GetUserIdentityProviderByUsername(model.UserName);
            if (identityProvider != null && identityProvider.Name != null && !string.Equals(identityProvider.Type, "Password", StringComparison.OrdinalIgnoreCase))
            {
                throw new QTDServerException("That User is managed by a third party, please attempt to login again", false, System.Net.HttpStatusCode.Unauthorized);
            }

            bool isLockedOut = await _signInManager.UserManager.IsLockedOutAsync(user);

            if (isLockedOut)
            {
                throw new QTDServerException("Due to too many failed login attempts, your account has been locked. Please reset your password.", false, System.Net.HttpStatusCode.Unauthorized);

            }

            var signinResult = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, true);

            if (!signinResult.Succeeded && !signinResult.RequiresTwoFactor)
            {
                //Check for new lockout immediately
                isLockedOut = await _signInManager.UserManager.IsLockedOutAsync(user);

                if (isLockedOut)
                {
                    string resetPasswordUrl = await _userService.GenerateResetPasswordUrlAsync(user);
                    await _notificationService.SendAccountLockedNotificationAsync(user.Email, resetPasswordUrl);
                    throw new QTDServerException("Due to too many failed login attempts, your account has been locked. Please reset your password.", false, System.Net.HttpStatusCode.Unauthorized);
                }

                throw new QTDServerException("Invalid username or password. Try clicking 'Forgot Password' if you're having trouble signing in.", false, System.Net.HttpStatusCode.Unauthorized);
            }

            bool requiresMFA = await RequiresMFAAsync(user);
            bool isTrustedDevice = await _userService.IsTrustedDeviceAsync(user, HttpContext);
            bool is2FaApproved = !requiresMFA || isTrustedDevice;

            if (!isTrustedDevice && requiresMFA)
            {
                if (signinResult.RequiresTwoFactor)
                {
                    var token = await _userService.Generate2FATokenAsync(user);
                    await _notificationService.Send2FANotificationAsync(user.Email, token);
                    is2FaApproved = false;
                }
                else
                {
                    is2FaApproved = true;
                }
            }

            var authTokenOptions = new ClaimsBuilderOptions(false, is2FaApproved);
            var authToken = await _userSharedService.GetJwtAsync(authTokenOptions, user);

            var refreshTokenOptions = new ClaimsBuilderOptions(true, is2FaApproved);
            var refreshToken = await _userSharedService.GetJwtAsync(refreshTokenOptions, user);

            await _eventLogService.AddLoginEvent(user.Id);

            return Ok(new { message = getMessage(is2FaApproved), authToken = authToken, refreshToken = refreshToken, lockoutEnd = user.LockoutEnd?.ToString("o") });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/users/authenticate/initiate")]
        public async Task<IActionResult> InitiateLogin(string username)
        {

            var identityProvider = await _identityProviderService.GetUserIdentityProviderEntityByUsername(username);
            switch (identityProvider?.Type?.ToUpper())
            {
                case "SAML":
                    
                    return Challenge(new AuthenticationProperties 
                    {
                        RedirectUri = $"{_domainSettings.Authentication}/users/authenticate/sso/saml" // After successful login, redirect here,
                        ,Items = { { "idp", (identityProvider as SamlProvider).EntityIDUrl } }
                    }, "Saml2");
                case "OAUTH":
                    return Ok();
                default:
                    return new RedirectResult($"{_domainSettings.SPA}/auth/login-password?username={Uri.EscapeDataString(username)}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableCors]
        [Route("/users/authenticate/sso/saml")]
        public async Task<IActionResult> SamlCallback()
        {

            // Explicitly authenticate the user using the SAML2 scheme
            var authenticateResult = await HttpContext.AuthenticateAsync("External");

            if (!authenticateResult.Succeeded || !authenticateResult.Principal.Identity.IsAuthenticated)
            {
                Console.WriteLine("SAML authentication failed.");
                return Unauthorized("SAML authentication failed.");
            }

            var samlPrincipal = authenticateResult.Principal;

            // Log claims for debugging
            foreach (var claim in samlPrincipal.Claims)
            {
                Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
            }

            // Extract claims
            var nameId = samlPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            if (string.IsNullOrEmpty(nameId))
            {
                Console.WriteLine("Required claims are missing.");
                return Unauthorized("Required claims are missing.");
            }

            var user = await _userManager.FindByEmailAsync(nameId);

            //maybe we should do something else but we should at least return redirect back to spa
            if (user == null) return NotFound();

            var authTokenOptions = new ClaimsBuilderOptions(false, true);
            var authToken = await _userSharedService.GetJwtAsync(authTokenOptions, user);

            var refreshTokenOptions = new ClaimsBuilderOptions(true, true);
            var refreshToken = await _userSharedService.GetJwtAsync(refreshTokenOptions, user);

            //replace url with SPA from app settings
            return Redirect($"{_domainSettings.SPA}/auth/login/#authToken={authToken}&refreshToken={refreshToken}");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/users/authenticate/external")]
        public async Task<IActionResult> LoginExternal()
        {
            return Ok();
        }

        [HttpGet]
        [Route("/users/identityprovider/{username}")]
        public async Task<IActionResult> GetUserIdentityProviderByUsername(string username)
        {
            var identityProvider = await _identityProviderService.GetUserIdentityProviderByUsername(username);
            return Ok(new { identityProvider });
        }

        [HttpPut]
        [Route("/users/{username}/identityprovider")]
        public async Task<IActionResult> UpdateUserIdentityProviderClaimByUsername(string username, IdentityProviderUpdateModel identityProviderUpdateModel)
        {
            await _identityProviderService.UpdateUserIdentityProviderClaimByUsername(username, identityProviderUpdateModel);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/users/authenticate/logout")]
        public async Task<IActionResult> InitiateLogout()
        {
            await HttpContext.SignOutAsync("External");
            await HttpContext.SignOutAsync("Saml2");
            return SignOut(new AuthenticationProperties { RedirectUri = $"{_domainSettings.SPA}/auth/login" }, "Identity.Application", "Saml2");
        }

        private async Task<bool> RequiresMFAAsync(AppUser user)
        {
            bool requiresMFA = user.TwoFactorEnabled;
            if (requiresMFA)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var instanceNames = claims.Where(c => c.Type == CustomClaimTypes.InstanceName).Select(c => c.Value).ToList();
                var instances = await _instanceService.GetAllInstancesWithInstanceSettingsAsync();
                bool instanceRequiresMFA = instances.Any(i => instanceNames.Contains(i.Name) && i.InstanceSetting.MFAEnabled == true);
                return instanceRequiresMFA;
            }
            return false;
        }
    }
}