using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Application.Settings;
using QTD2.Domain;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.QTD2Auth.Settings;

namespace QTD2.Application.Services.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly Application.Settings.DomainSettings _domainSettings;
        private readonly ResetPasswordSettings _resetPasswordSettings;


        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<Application.Settings.DomainSettings> domainSettings, IOptions<ResetPasswordSettings> resetPasswordSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _domainSettings = domainSettings.Value;
            _resetPasswordSettings = resetPasswordSettings.Value;
        }

        public async Task<AppUser> GetUserWithClaimsAsync(string name)
            {
            // maybe this needs to be changed down the line, i'm not sure.  ASPNet Core Identity is a mess
            var user = await _userManager.FindByNameAsync(name);
            var claims = await _userManager.GetClaimsAsync(user);

            user.Claims = new System.Collections.Generic.List<AppClaim>();

            foreach (var claim in claims)
            {
                user.Claims.Add(new AppClaim(claim.Type, claim.Value));
            }

            return user;
        }

        public async Task<string> Generate2FATokenAsync(AppUser user)
        {
            return await _userManager.GenerateTwoFactorTokenAsync(user, "CustomMfaTokenProvider");
        }

        public async Task<string> GenerateForgotPasswordTokenAsync(AppUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var expirationTimeStamp = DateTime.UtcNow.AddMinutes(_resetPasswordSettings.TokenLifetimeMinutes);
            var result = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(expirationTimeStamp.ToString() + "|" + token));
            return result;
        }

        public async Task<string> GenerateResetPasswordUrlAsync(AppUser user)
        {
            var token = await GenerateForgotPasswordTokenAsync(user);
            token = HttpUtility.UrlEncode(token);
            string baseDomain = _domainSettings.Angular;
            return $"{baseDomain}/auth/create?token={token}";
        }

        public async Task<bool> IsTrustedDeviceAsync(AppUser user, HttpContext httpContext)
        {
            string clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
            var claims = await _signInManager.UserManager.GetClaimsAsync(user);

            var trustedClaims = claims.Where(c => c.Type == CustomClaimTypes.TfaTrustedDevice).ToList();
            foreach (var claim in trustedClaims)
            {
                var parts = claim.Value.Split('|');
                if (parts.Length == 2 && parts[0] == clientIp)
                {
                    if (DateTime.TryParse(parts[1], null, System.Globalization.DateTimeStyles.AdjustToUniversal, out var storedTime))
                    {
                        if (storedTime.ToUniversalTime().AddDays(30) > DateTime.UtcNow)
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        public string[] DecodeResetPasswordToken(string encodedToken)
        {
            var decodedBytes = Convert.FromBase64String(encodedToken);
            var decodedString = System.Text.Encoding.UTF8.GetString(decodedBytes);

            var parts = decodedString.Split('|');
            return parts;
        }

        public bool IsResetPasswordTokenValidAsync(string encodedToken)
        {
            var parts = DecodeResetPasswordToken(encodedToken);

            if (parts.Length == 2 && DateTime.TryParse(parts[0], out DateTime expirationDateTime))
            {
                return expirationDateTime > DateTime.UtcNow;
            }
            return false;
        }
    }
}
