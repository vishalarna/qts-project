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
    public class ReportSkeletonAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ReportSkeleton>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ReportSkeleton resource)
        {
            if (requirement.Name == ReportSkeletonOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ReportSkeletonOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ReportSkeletonOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ReportSkeletonOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
