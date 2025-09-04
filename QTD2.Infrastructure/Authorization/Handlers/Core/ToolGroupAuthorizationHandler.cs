using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class ToolGroupAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Domain.Entities.Core.ToolGroup>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Domain.Entities.Core.ToolGroup resource)
        {
            context.Succeed(requirement);

            if (requirement.Name == ToolGroupOperations.Create.Name)
            {
                
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                
            }

            if (requirement.Name == ToolGroupOperations.Read.Name)
            {
                
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                
            }

            if (requirement.Name == ToolGroupOperations.Update.Name)
            {
                
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                
            }

            if (requirement.Name == ToolGroupOperations.Delete.Name)
            {
                
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                
            }

            return Task.CompletedTask;
        }
    }
}
