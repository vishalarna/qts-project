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
    public class ILA_PerformTraineeEvalAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ILA_PerformTraineeEval>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ILA_PerformTraineeEval resource)
        {
            if (requirement.Name == ILA_PerformTraineeEvalOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ILA_PerformTraineeEvalOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ILA_PerformTraineeEvalOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ILA_PerformTraineeEvalOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
