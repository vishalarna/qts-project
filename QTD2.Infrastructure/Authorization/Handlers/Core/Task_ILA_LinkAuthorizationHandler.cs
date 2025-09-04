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
    public class Task_ILA_LinkAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Task_ILA_Link>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Task_ILA_Link resource)
        {
            if (requirement.Name == Task_ILA_LinkOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_ILA_LinkOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_ILA_LinkOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_ILA_LinkOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
