using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.Application.Interfaces.Services.Authentication
{
    public interface IUserService
    {
        Task<string> Generate2FATokenAsync(AppUser user);

        Task<string> GenerateForgotPasswordTokenAsync(AppUser user);

        Task<string> GenerateResetPasswordUrlAsync(AppUser user);

        Task<AppUser> GetUserWithClaimsAsync(string name);

        Task<bool> IsTrustedDeviceAsync(AppUser user, HttpContext httpContext);

        string[] DecodeResetPasswordToken(string encodedToken);

        bool IsResetPasswordTokenValidAsync(string encodedToken);
    }
}
