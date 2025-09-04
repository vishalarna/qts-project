using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Task_PositionAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Task_Position>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Task_Position resource)
        {
            if (requirement.Name == Task_PositionOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Task_PositionOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_PositionOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_PositionOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
