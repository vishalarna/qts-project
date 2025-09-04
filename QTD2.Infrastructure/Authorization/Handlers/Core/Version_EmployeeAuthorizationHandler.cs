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
    public class Version_EmployeeAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_Employee>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_Employee resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_EmployeeOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_EmployeeOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_EmployeeOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_EmployeeOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
