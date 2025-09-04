using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class TrainingProgramILALinkAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TrainingPrograms_ILA_Link>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TrainingPrograms_ILA_Link resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == TrainingPrograms_ILA_LinkOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingPrograms_ILA_LinkOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingPrograms_ILA_LinkOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingPrograms_ILA_LinkOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
