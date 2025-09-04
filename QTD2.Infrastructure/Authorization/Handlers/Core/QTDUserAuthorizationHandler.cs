using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class QTDUserAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, QTDUser>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, QTDUser resource)
        {
            if (requirement.Name == QTDUserOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == QTDUserOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == QTDUserOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == QTDUserOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
