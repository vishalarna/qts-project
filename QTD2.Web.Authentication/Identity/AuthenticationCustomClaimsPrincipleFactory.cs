using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using QTD2.Data.Entities.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using QTD2.Infrastructure.Identity.Interfaces;

namespace QTD2.Web.Authentication.Identity
{
    public class AuthenticationCustomClaimsPrincipleFactory : UserClaimsPrincipalFactory<AppUser>
    {
        IIdentityBuilder _identityBuilder;

        public AuthenticationCustomClaimsPrincipleFactory(UserManager<AppUser> userManager,
                                            IOptions<IdentityOptions> optionsAccessor,
                                            IIdentityBuilder identityBuilder) : base(userManager, optionsAccessor)
        {
            _identityBuilder = identityBuilder;
        }

        protected async override Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaims(_identityBuilder.GetCustomClaims());
            return identity;
        }
    }
}
