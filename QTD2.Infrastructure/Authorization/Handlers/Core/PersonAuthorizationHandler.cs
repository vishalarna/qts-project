using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class PersonAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Person>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Person resource)
        {
            if (requirement.Name == PersonOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == PersonOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == PersonOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == PersonOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
