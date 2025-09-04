using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Identity.Identities;
using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.Application.Middleware
{
    public class IdentityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IClaimsBuilderFactory _claimsBuilderFactory;

        public IdentityMiddleware(
            RequestDelegate next,
            IClaimsBuilderFactory claimsBuilderFactory)
        {
            _next = next;

            // _userManager = userManager; // => Ref https://www.py4u.net/discuss/716606
            _claimsBuilderFactory = claimsBuilderFactory;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager)
        {
            // to do
            // inject QTD value
            string username = context.User.Identity.Name;

            if (string.IsNullOrEmpty(username))
            {
                await _next(context);
            }
            else
            {
                ClaimsBuilderOptions options = new ClaimsBuilderOptions("QTD");

                var claimsBulder = _claimsBuilderFactory.GetBuilder(options);
                var user = await userManager.FindByNameAsync(username);

                var dbClaims = (await userManager.GetClaimsAsync(user)).ToList();
                dbClaims.AddRange(context.User.Claims.Where(r => r.Type.StartsWith(Domain.CustomClaimTypes.Prefix)));
                var claims = claimsBulder.Build(user, dbClaims, options);

                var identity = new QTDIdentity(claims);
                context.User = identity;

                await _next(context);
            }
        }
    }
}
