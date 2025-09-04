using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_TestStausAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_TestStaus>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_TestStaus resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_TestStausOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_TestStausOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_TestStausOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_TestStausOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
