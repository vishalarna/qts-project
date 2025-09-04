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
    public class ClassScheduleEmployeeAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ClassSchedule_Employee>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ClassSchedule_Employee resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == ClassScheduleEmployeeOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClassScheduleEmployeeOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClassScheduleEmployeeOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClassScheduleEmployeeOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}

