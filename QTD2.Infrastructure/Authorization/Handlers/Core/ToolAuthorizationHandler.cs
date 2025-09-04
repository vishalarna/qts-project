using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class ToolAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Domain.Entities.Core.Tool>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Domain.Entities.Core.Tool resource)
        {
            context.Succeed(requirement);

            if (requirement.Name == ToolOperations.Create.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == ToolOperations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == ToolOperations.Update.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == ToolOperations.Delete.Name)
            {


                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            return Task.CompletedTask;
        }
    }
}
