using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class ClientUserAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ClientUser>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ClientUser resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == ClientUserOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClientUserOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClientUserOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClientUserOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
