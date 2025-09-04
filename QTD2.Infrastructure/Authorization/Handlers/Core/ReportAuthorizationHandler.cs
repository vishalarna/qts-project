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
    public class ReportAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Report>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Report resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == ReportOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ReportOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ReportOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ReportOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
