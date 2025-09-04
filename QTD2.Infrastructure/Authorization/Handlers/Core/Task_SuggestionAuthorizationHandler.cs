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
    public class Task_SuggestionAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Task_Suggestion>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Task_Suggestion resource)
        {
            if (requirement.Name == Task_SuggestionOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_SuggestionOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_SuggestionOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_SuggestionOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
