using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class CBT_ScormUpload_Question_ChoiceHandler : QTDHandler<OperationAuthorizationRequirement, CBT_ScormUpload_Question_Choice>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, CBT_ScormUpload_Question_Choice
 resource)
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