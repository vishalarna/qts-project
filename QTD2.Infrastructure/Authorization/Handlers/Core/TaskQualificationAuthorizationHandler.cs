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
    public class TaskQualificationAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TaskQualification>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TaskQualification resource)
        {
            if (requirement.Name == TaskQualificationOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TaskQualificationOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TaskQualificationOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TaskQualificationOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
