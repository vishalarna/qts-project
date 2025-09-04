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
    public class EnablingObjective_MetaEO_LinkAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, EnablingObjective_MetaEO_Link>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, EnablingObjective_MetaEO_Link resource)
        {
            if (requirement.Name == EnablingObjective_MetaEO_LinkOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EnablingObjective_MetaEO_LinkOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EnablingObjective_MetaEO_LinkOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == EnablingObjective_MetaEO_LinkOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
