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
    public class SafetyHazard_HistoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, SafetyHazard_History>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, SafetyHazard_History resource)
        {
            if (requirement.Name == SafetyHazard_HistoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == SafetyHazard_HistoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == SafetyHazard_HistoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == SafetyHazard_HistoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
