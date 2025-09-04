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
    public class ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditHandler : QTDHandler<OperationAuthorizationRequirement, ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit resource)
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