using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_SaftyHazard_ControlAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_SaftyHazard_Control>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_SaftyHazard_Control resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_SaftyHazard_ControlOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_SaftyHazard_ControlOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_SaftyHazard_ControlOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_SaftyHazard_ControlOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
