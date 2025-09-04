using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.Infrastructure.Authorization.Handlers.Authentication
{
    public class UserAuthorizationHandler : AuthenticationHandler<OperationAuthorizationRequirement, AppUser>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, AppUser resource)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
