using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using QTD2.Application.Authorization.Requirements;

namespace QTD2.Application.Authorization.Handlers
{
    public class QtdSystemHandler : AuthorizationHandler<QtdSystemRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, QtdSystemRequirement requirement)
        {
            // Retrieve the IsSystem claim from the identity
            var isSystemClaim = context.User.Claims.FirstOrDefault(c => c.Type == "IsSystem");

            // Check if the IsSystem claim exists and has a value of "true"
            if (isSystemClaim != null && isSystemClaim.Value == "true")
            {
                // Claim exists and has a value of "true", authorize the request
                context.Succeed(requirement);
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
