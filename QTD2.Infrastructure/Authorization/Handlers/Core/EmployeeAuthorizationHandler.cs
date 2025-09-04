using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class EmployeeAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Employee>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Employee resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == EmployeeOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EmployeeOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EmployeeOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EmployeeOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
