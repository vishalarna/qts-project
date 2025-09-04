using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class TrainingProgram_HistoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TrainingProgram_History>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TrainingProgram_History resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == TrainingProgram_HistoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingProgram_HistoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingProgram_HistoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingProgram_HistoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}