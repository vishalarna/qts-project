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
    public class IDP_ReviewStatusAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, IDP_ReviewStatus>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IDP_ReviewStatus resource)
        {
            if (requirement.Name == IDP_ReviewStatusOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == IDP_ReviewStatusOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == IDP_ReviewStatusOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == IDP_ReviewStatusOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
