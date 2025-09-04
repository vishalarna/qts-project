using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_SaftyHazard_AbatementAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_SaftyHazard_Abatement>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_SaftyHazard_Abatement resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_SaftyHazard_AbatementOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_SaftyHazard_AbatementOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_SaftyHazard_AbatementOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_SaftyHazard_AbatementOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
