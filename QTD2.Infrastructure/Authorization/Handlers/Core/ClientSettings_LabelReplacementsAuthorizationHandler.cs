using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class ClientSettings_LabelReplacementsAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ClientSettings_LabelReplacement>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ClientSettings_LabelReplacement resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            context.Succeed(requirement);
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
