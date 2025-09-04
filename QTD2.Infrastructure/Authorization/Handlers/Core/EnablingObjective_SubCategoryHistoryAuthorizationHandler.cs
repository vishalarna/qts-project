using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class EnablingObjective_SubCategoryHistoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, EnablingObjective_SubCategoryHistory>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, EnablingObjective_SubCategoryHistory resource)
        {
            if (requirement.Name == EnablingObjective_SubCategoryHistoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EnablingObjective_SubCategoryHistoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EnablingObjective_SubCategoryHistoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EnablingObjective_SubCategoryHistoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
