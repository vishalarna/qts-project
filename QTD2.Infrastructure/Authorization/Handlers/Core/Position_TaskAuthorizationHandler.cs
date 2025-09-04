using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Position_TaskAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Position_Task>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Position_Task resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == Position_TaskOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Position_TaskOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Position_TaskOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Position_TaskOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
