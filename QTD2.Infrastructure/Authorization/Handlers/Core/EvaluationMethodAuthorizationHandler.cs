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
    public class EvaluationMethodAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, EvaluationMethod>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, EvaluationMethod resource)
        {
            if (requirement.Name == EvaluationMethodOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EvaluationMethodOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EvaluationMethodOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EvaluationMethodOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
