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
    public class TestItemTypeAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TestItemType>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TestItemType resource)
        {
            if (requirement.Name == TestItemTypeOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TestItemTypeOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TestItemTypeOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TestItemTypeOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
