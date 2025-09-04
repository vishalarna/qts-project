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
    public class DeliveryMethodAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, DeliveryMethod>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, DeliveryMethod resource)
        {
            if (requirement.Name == DeliveryMethodOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == DeliveryMethodOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == DeliveryMethodOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == DeliveryMethodOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
