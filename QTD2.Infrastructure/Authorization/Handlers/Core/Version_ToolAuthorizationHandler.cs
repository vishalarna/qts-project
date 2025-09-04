using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_ToolAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_Tool>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_Tool resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_ToolOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_ToolOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_ToolOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_ToolOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
