using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class SaftyHazard_ControlAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Domain.Entities.Core.SaftyHazard_Control>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Domain.Entities.Core.SaftyHazard_Control resource)
        {
            context.Succeed(requirement);

            if (requirement.Name == SaftyHazard_ControlOperations.Create.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == SaftyHazard_ControlOperations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == SaftyHazard_ControlOperations.Update.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == SaftyHazard_ControlOperations.Delete.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            return Task.CompletedTask;
        }
    }
}
