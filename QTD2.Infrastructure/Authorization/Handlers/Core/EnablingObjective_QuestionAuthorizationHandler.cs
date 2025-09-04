using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class EnablingObjective_QuestionAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, EnablingObjective_Question>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, EnablingObjective_Question resource)
        {
            setUser(context);

            if (requirement.Name == EnablingObjective_QuestionOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EnablingObjective_QuestionOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EnablingObjective_QuestionOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EnablingObjective_QuestionOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
