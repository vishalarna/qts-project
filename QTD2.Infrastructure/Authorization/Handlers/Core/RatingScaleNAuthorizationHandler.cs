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
    public class RatingScaleNAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, RatingScaleN>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, RatingScaleN resource)
        {
            if (requirement.Name == RatingScaleNOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == RatingScaleNOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == RatingScaleNOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == RatingScaleNOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
