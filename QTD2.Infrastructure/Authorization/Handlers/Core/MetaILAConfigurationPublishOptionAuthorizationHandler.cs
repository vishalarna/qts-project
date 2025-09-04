using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class MetaILAConfigurationPublishOptionAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, MetaILAConfigurationPublishOption>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, MetaILAConfigurationPublishOption resource)
        {
            if (requirement.Name == MetaILAConfigurationPublishOptionOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == MetaILAConfigurationPublishOptionOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == MetaILAConfigurationPublishOptionOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == MetaILAConfigurationPublishOptionOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
