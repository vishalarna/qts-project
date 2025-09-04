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
    public class TrainingGroupAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TrainingGroup>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TrainingGroup resource)
        {
            if (requirement.Name == TrainingGroupOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingGroupOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingGroupOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingGroupOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
