using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Procedure_IssuingAuthorityAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Domain.Entities.Core.Procedure_IssuingAuthority>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Domain.Entities.Core.Procedure_IssuingAuthority resource)
        {
            context.Succeed(requirement);

            if (requirement.Name == Procedure_IssuingAuthorityOperations.Create.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == Procedure_IssuingAuthorityOperations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == Procedure_IssuingAuthorityOperations.Update.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == Procedure_IssuingAuthorityOperations.Delete.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            return Task.CompletedTask;
        }
    }
}
