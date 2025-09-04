using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class EnablingObjective_SubCategoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, EnablingObjective_SubCategory>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, EnablingObjective_SubCategory resource)
        {
            context.Succeed(requirement);

            if (requirement.Name == EnablingObjective_SubCategoryOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == EnablingObjective_SubCategoryOperations.Read.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == EnablingObjective_SubCategoryOperations.Update.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == EnablingObjective_SubCategoryOperations.Delete.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
