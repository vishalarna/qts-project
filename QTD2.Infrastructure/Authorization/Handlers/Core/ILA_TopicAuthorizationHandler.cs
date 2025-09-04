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
    public class ILA_TopicAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ILA_Topic>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ILA_Topic resource)
        {
            if (requirement.Name == ILA_TopicOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ILA_TopicOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ILA_TopicOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ILA_TopicOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
