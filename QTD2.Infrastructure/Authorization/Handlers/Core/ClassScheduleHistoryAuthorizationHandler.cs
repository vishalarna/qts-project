using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class ClassScheduleHistoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ClassScheduleHistory>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ClassScheduleHistory resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == ClassScheduleHistoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClassScheduleHistoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClassScheduleHistoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClassScheduleHistoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
