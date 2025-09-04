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
    public class Version_EnablingObjective_StepAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_EnablingObjective_Step>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_EnablingObjective_Step resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_EnablingObjective_StepOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_EnablingObjective_StepOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_EnablingObjective_StepOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_EnablingObjective_StepOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
