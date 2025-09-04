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
    public class TaskQualificationStatusAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TaskQualificationStatus>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TaskQualificationStatus resource)
        {
            if (requirement.Name == TaskQualificationStatusOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TaskQualificationStatusOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TaskQualificationStatusOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TaskQualificationStatusOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
