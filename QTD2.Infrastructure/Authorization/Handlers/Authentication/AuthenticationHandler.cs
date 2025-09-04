using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Infrastructure.Identity.Identities;

namespace QTD2.Infrastructure.Authorization.Handlers.Authentication
{
    public class AuthenticationHandler<TRequirement, TResouce> : AuthorizationHandler<TRequirement, TResouce>
        where TRequirement : OperationAuthorizationRequirement
    {
        protected QTDIdentity QtdUser { get; set; }

        protected void setUser(AuthorizationHandlerContext context)
        {
            // QtdUser = context.User as QTDIdentity;
            // do we need an AuthIdentity Server?
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, TResouce resource)
        {
            throw new NotImplementedException();
        }
    }
}
