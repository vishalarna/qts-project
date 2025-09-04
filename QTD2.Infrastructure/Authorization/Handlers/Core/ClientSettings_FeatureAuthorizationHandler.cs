using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class ClientSettings_FeatureAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ClientSettings_Feature>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ClientSettings_Feature resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            context.Succeed(requirement);
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
