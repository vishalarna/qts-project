using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;

using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class CertificationAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Certification>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Certification resource)
        {
            setUser(context);

            if (requirement.Name == CertificationOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CertificationOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CertificationOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CertificationOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
