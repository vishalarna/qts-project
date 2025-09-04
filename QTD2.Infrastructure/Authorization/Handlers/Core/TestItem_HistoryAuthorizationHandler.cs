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
    public class TestItem_HistoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TestItem_History>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TestItem_History resource)
        {
            if (requirement.Name == TestItem_HistoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TestItem_HistoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TestItem_HistoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TestItem_HistoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
