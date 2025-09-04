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
    public class ProviderAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Provider>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Provider resource)
        {
            if (requirement.Name == ProviderOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ProviderOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ProviderOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ProviderOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
