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
    public class Task_Reference_LinkAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Task_Reference_Link>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Task_Reference_Link resource)
        {
            if (requirement.Name == Task_Reference_LinkOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_Reference_LinkOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_Reference_LinkOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_Reference_LinkOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
