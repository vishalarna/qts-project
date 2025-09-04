using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QTD2.Domain;
using QTD2.Application.Authorization.Requirements;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using QTD2.Application.Authorization.Handlers;
using System.Linq;

namespace QTD2.Application.Startup
{
    public static class Authorization
    {
        public static void AddAuthorizationPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationHandler, QtdSystemHandler>();

            services.AddAuthorization(options =>
            {
                //TODO modify policy too check for TFA
                options.AddPolicy(Application.Authorization.Policies.Authenticated, policy => policy
                    .RequireAssertion(context =>
                    !context.User.HasClaim(c => c.Type == CustomClaimTypes.TfaRequired) ||
                    ((context.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.TfaRequired)?.Value ?? "false") == "false"))
                    .RequireAuthenticatedUser().Build());
                options.AddPolicy(Application.Authorization.Policies.SuperAdmin, policy => policy.RequireClaim(CustomClaimTypes.IsAdmin));
                options.AddPolicy(Application.Authorization.Policies.AdminSiteAccess, policy => policy.RequireAssertion(context => context.User.HasClaim(c => (c.Type == CustomClaimTypes.IsAdmin || c.Type == CustomClaimTypes.ClientAdmin))));
                options.AddPolicy(Application.Authorization.Policies.QtdSystem, policy => policy.Requirements.Add(new QtdSystemRequirement()));
            });
        }
    }
}
