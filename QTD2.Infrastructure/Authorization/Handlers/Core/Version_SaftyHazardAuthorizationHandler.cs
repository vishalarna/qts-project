using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_SaftyHazardAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_SaftyHazard>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_SaftyHazard resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_SaftyHazardOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_SaftyHazardOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_SaftyHazardOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_SaftyHazardOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
