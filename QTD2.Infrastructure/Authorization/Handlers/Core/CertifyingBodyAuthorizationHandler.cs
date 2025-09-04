using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class CertifyingBodyAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, CertifyingBody>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, CertifyingBody resource)
        {
            if (requirement.Name == CertifyingBodyOperations.Create.Name)
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == CertifyingBodyOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CertifyingBodyOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == CertifyingBodyOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
