using QTD2.Domain.Entities.Authentication;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class ForgotPasswordModel
    {
        public string UserName { get; set; }

        public string ResetPasswordLink { get; set; }

        public ForgotPasswordModel(string userName, string url)
        {
            UserName = userName;
            ResetPasswordLink = url;
        }
    }
}
