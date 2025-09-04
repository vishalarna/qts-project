using QTD2.Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using QTD2.Infrastructure.Identity.Extensions;
using System.Collections.Generic;

namespace QTD2.Web.Authentication.Identity
{
    public class AuthenticationIdentityBuilder : IIdentityBuilder
    {
        IHttpContextAccessor _httpContextAccessor;

        public AuthenticationIdentityBuilder(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Build()
        {
            if (_httpContextAccessor.HttpContext.User == null || !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return;

            var identity = _httpContextAccessor.HttpContext.User.GetAuthenticationClaimsIdentity();

            if (identity == null && _httpContextAccessor.HttpContext.User.Identities.Count() > 1)
            {
                //this shouldn't happen at this point
                _httpContextAccessor.HttpContext.SignOutAsync();
            }

            if(identity == null)
            {
                _httpContextAccessor.HttpContext.User.Identities.First().AddClaims(GetCustomClaims());
                _httpContextAccessor.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, _httpContextAccessor.HttpContext.User);
            }
        }

        public List<Claim> GetCustomClaims()
        {
            List<Claim> customClaims = new List<Claim>();
            customClaims.Add(new Claim(Domain.CustomClaimTypes.Site, Infrastructure.Identity.ClaimsAudienceType.Authentication.ToString()));

            return customClaims;

        }
    }
}
