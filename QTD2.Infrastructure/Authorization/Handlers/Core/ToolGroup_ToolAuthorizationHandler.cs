using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class ToolGroup_ToolAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Domain.Entities.Core.ToolGroup_Tool>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Domain.Entities.Core.ToolGroup_Tool resource)
        {
            context.Succeed(requirement);

            if (requirement.Name == ToolGroup_ToolOperations.Create.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == ToolGroup_ToolOperations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == ToolGroup_ToolOperations.Update.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == ToolGroup_ToolOperations.Delete.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            return Task.CompletedTask;
        }
    }
}
