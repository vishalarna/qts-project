using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class IDP_ReviewAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, IDP_Review>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IDP_Review resource)
        {
            if (requirement.Name == IDP_ReviewOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == IDP_ReviewOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == IDP_ReviewOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == IDP_ReviewOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
