using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.Identity;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Identity.Extensions;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace QTD2.Web.Admin.Identity
{
    public class AdminIdentityBuilder : IIdentityBuilder
    {
        IHttpContextAccessor _httpContextAccessor;
        IUserService _userService;

        public AdminIdentityBuilder(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public void Build()
        {
            if (_httpContextAccessor.HttpContext.User == null || !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return;

            var adminIdentity = _httpContextAccessor.HttpContext.User.GetAdminClaimsIdentity();

            if (adminIdentity != null)
                return;

            adminIdentity = new ClaimsIdentity(GetCustomClaims());
            _httpContextAccessor.HttpContext.User.AddIdentity(adminIdentity);

            _httpContextAccessor.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, _httpContextAccessor.HttpContext.User);
        }

        public List<Claim> GetCustomClaims()
        {
            string username = _httpContextAccessor.HttpContext.User.Identities.Where(r => r.Label == ClaimsAudienceType.Authentication.ToString()).FirstOrDefault()?.Name;

            List<Claim> customClaims = new List<Claim>();

            customClaims.Add(new Claim(Domain.CustomClaimTypes.Site, ClaimsAudienceType.Admin.ToString()));

            var dataClaims = _userService.GetUserClaims(username);

            foreach (var claim in dataClaims.Where(claim => claim.ClaimType == Domain.CustomClaimTypes.ClientAdmin))
            {
                customClaims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            }

            var superUserClaim = dataClaims.Where(claim => claim.ClaimValue == Domain.CustomClaimTypes.IsSuperAdmin).FirstOrDefault();

            if (superUserClaim != null && bool.Parse(superUserClaim.ClaimValue))
            {
                customClaims.Add(new Claim(superUserClaim.ClaimType, superUserClaim.ClaimValue));
            }

            customClaims.Add(new Claim(Domain.CustomClaimTypes.IsSuperAdmin, "true"));

            return customClaims;
        }
    }
}
