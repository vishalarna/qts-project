using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class IDPSchedulesAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, IDPSchedule>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IDPSchedule resource)
        {
            if (requirement.Name == IDPScheduleOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == IDPScheduleOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == IDPScheduleOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == IDPScheduleOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
