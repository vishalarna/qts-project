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
    public class ReportSkeletonCategoryAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, ReportSkeleton_Category>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ReportSkeleton_Category resource)
        {
            if (requirement.Name == ReportSkeletonCategoryOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ReportSkeletonCategoryOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ReportSkeletonCategoryOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == ReportSkeletonCategoryOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
