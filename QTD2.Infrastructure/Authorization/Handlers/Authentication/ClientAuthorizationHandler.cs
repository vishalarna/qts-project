using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Authorization.Operations.Authentication;

namespace QTD2.Infrastructure.Authorization.Handlers.Authentication
{
    public class ClientAuthorizationHandler : AuthenticationHandler<OperationAuthorizationRequirement, Client>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Client resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (requirement.Name == ClientOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClientOperations.CreateDatabase.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ClientOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
