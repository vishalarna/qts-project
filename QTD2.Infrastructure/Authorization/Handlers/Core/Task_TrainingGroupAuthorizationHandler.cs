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
    public class Task_TrainingGroupAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Task_TrainingGroup>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Task_TrainingGroup resource)
        {
            if (requirement.Name == Task_TrainingGroupOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_TrainingGroupOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_TrainingGroupOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_TrainingGroupOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
