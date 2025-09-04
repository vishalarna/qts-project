using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Authentication
{
    public interface INotificationService
    {
        Task<bool> Send2FANotificationAsync(string emailAddress, string verificationToken);
        Task<bool> SendResetPasswordNotificationAsync(string emailAddress, string url);
        Task<bool> SendAccountLockedNotificationAsync(string emailAddress, string url);
    }
}
