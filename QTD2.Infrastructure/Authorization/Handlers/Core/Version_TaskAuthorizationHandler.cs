using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using Task = System.Threading.Tasks.Task;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_TaskAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_Task>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_Task resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_TaskOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_TaskOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_TaskOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_TaskOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
