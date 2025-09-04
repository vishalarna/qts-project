using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class PositonsSQAuthorizeHandler : QTDHandler<OperationAuthorizationRequirement, Positions_SQ>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Positions_SQ resource)
        {
           
            if (requirement.Name == Positions_SQOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Positions_SQOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Positions_SQOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Positions_SQOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
