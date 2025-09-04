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
    public class Procedure_StatusHistoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Procedure_StatusHistory>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Procedure_StatusHistory resource)
        {
            if (requirement.Name == Procedure_StatusHistoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Procedure_StatusHistoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Procedure_StatusHistoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Procedure_StatusHistoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
