using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class StudentEvaluationWithoutEmpAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Domain.Entities.Core.StudentEvaluationWithoutEmp>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Domain.Entities.Core.StudentEvaluationWithoutEmp resource)
        {
            context.Succeed(requirement);

            if (requirement.Name == StudentEvaluationWithoutEmpOperations.Create.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == StudentEvaluationWithoutEmpOperations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == StudentEvaluationWithoutEmpOperations.Update.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == StudentEvaluationWithoutEmpOperations.Delete.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            return Task.CompletedTask;
        }
    }
}
