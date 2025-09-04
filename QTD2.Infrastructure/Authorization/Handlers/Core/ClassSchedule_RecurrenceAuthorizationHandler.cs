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
    public class ClassSchedule_RecurrenceAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ClassSchedule_Recurrence>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ClassSchedule_Recurrence resource)
        {
            if (requirement.Name == ClassSchedule_RecurrenceOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClassSchedule_RecurrenceOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClassSchedule_RecurrenceOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClassSchedule_RecurrenceOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

    }
}
