using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class TrainingProgramReviewAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TrainingProgramReview>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TrainingProgramReview resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;
            }

            if (requirement.Name == TrainingProgramReviewOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingProgramReviewOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingProgramReviewOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TrainingProgramReviewOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
