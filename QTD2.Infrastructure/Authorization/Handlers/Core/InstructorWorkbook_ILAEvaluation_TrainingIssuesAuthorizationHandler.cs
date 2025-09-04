using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class InstructorWorkbook_ILAEvaluation_TrainingIssuesAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, InstructorWorkbook_ILAEvaluation_TrainingIssues>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, InstructorWorkbook_ILAEvaluation_TrainingIssues resource)
        {
            if (requirement.Name == AuthorizationOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == AuthorizationOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == AuthorizationOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == AuthorizationOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
