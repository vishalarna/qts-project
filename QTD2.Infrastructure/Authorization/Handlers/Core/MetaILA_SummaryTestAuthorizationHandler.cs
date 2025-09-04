using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class MetaILA_SummaryTestAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, MetaILA_SummaryTest>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, MetaILA_SummaryTest resource)
        {
            if (requirement.Name == AuthorizationOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == AuthorizationOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == AuthorizationOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == AuthorizationOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
