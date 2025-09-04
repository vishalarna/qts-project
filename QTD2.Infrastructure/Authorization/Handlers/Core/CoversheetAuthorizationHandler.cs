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
    public class CoversheetAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Coversheet>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Coversheet resource)
        {
            if (requirement.Name == CoversheetOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CoversheetOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CoversheetOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CoversheetOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
