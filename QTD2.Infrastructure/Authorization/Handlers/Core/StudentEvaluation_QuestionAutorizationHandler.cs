using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Infrastructure.Authorization.Operations.Core;
namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class StudentEvaluation_QuestionAutorizationHandler : QTDHandler<OperationAuthorizationRequirement, Domain.Entities.Core.StudentEvaluation_Question>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Domain.Entities.Core.StudentEvaluation_Question resource)
        {
            context.Succeed(requirement);

            if (requirement.Name == StudentEvaluation_QuestionOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == StudentEvaluation_QuestionOperations.Read.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == StudentEvaluation_QuestionOperations.Update.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == StudentEvaluation_QuestionOperations.Delete.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}