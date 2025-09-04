using QTD2.Domain.Entities.Authentication;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class TwoFactorEmailModel
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public TwoFactorEmailModel(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
    }
}
