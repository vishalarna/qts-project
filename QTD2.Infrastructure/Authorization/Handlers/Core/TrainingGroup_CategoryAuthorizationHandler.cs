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
    public class TrainingGroup_CategoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TrainingGroup_Category>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TrainingGroup_Category resource)
        {
            if (requirement.Name == TrainingGroup_CategoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingGroup_CategoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingGroup_CategoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingGroup_CategoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
