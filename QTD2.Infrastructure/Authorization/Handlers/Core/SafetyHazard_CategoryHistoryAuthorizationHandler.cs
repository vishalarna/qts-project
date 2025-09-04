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
    public class SafetyHazard_CategoryHistoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, SafetyHazard_CategoryHistory>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, SafetyHazard_CategoryHistory resource)
        {
            if (requirement.Name == SafetyHazard_CategoryHistoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == SafetyHazard_CategoryHistoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == SafetyHazard_CategoryHistoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == SafetyHazard_CategoryHistoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
