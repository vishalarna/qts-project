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
    public class RR_IssuingAuthority_StatusHistoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, RR_IssuingAuthority_StatusHistory>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, RR_IssuingAuthority_StatusHistory resource)
        {
            if (requirement.Name == RR_IssuingAuthority_StatusHistoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == RR_IssuingAuthority_StatusHistoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == RR_IssuingAuthority_StatusHistoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == RR_IssuingAuthority_StatusHistoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
