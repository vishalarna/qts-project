using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using System.Linq;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Dashboard_SettingAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, DashboardSetting>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, DashboardSetting resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}