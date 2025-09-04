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
    public class CoverSheetTypeAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, CoverSheetType>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, CoverSheetType resource)
        {
            if (requirement.Name == CoverSheetTypeOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CoverSheetTypeOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CoverSheetTypeOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CoverSheetTypeOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
