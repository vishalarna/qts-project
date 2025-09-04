using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using System;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
   public class ScormUploadAuthorizationHandler :QTDHandler<OperationAuthorizationRequirement, CBT_ScormUpload>
    { 
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, CBT_ScormUpload resource)
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
