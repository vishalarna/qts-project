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
    public class TestReleaseEMPSetting_Retake_LinkAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TestReleaseEMPSetting_Retake_Link>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TestReleaseEMPSetting_Retake_Link resource)
        {
            if (requirement.Name == TestReleaseEMPSetting_Retake_LinkOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TestReleaseEMPSetting_Retake_LinkOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TestReleaseEMPSetting_Retake_LinkOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TestReleaseEMPSetting_Retake_LinkOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
