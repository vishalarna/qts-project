using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Employee_TaskAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Employee_Task>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Employee_Task resource)
        {
            if (requirement.Name == Employee_TaskOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Employee_TaskOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Employee_TaskOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Employee_TaskOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
