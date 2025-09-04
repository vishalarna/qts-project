using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_Task_Position_LinkAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_Task_Position_Link>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_Task_Position_Link resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_Task_Position_LinkOperations.Create.Name)
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_Task_Position_LinkOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_Task_Position_LinkOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_Task_Position_LinkOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
