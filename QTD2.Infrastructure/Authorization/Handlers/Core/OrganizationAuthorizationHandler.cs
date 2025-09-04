using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class OrganizationAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Organization>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Organization resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == OrganizationOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == OrganizationOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == OrganizationOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == OrganizationOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}