using Microsoft.AspNetCore.Identity;

namespace QTD2.Domain.Entities.Authentication
{
    public class AppClaim : IdentityUserClaim<string>
    {
        public AppClaim() { }
        public AppClaim(string type, string value)
        {
            ClaimType = type;
            ClaimValue = value;
        }
    }
}
