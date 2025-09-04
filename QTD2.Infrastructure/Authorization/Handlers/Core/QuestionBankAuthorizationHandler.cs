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
    public class QuestionBankAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, QuestionBank>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, QuestionBank resource)
        {
            if (requirement.Name == QuestionBankOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == QuestionBankOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == QuestionBankOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == QuestionBankOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
