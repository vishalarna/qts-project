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
    public class TQEmpSettingAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, TQEmpSetting>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TQEmpSetting resource)
        {
            if (requirement.Name == TQEmpSettingOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TQEmpSettingOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TQEmpSettingOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TQEmpSettingOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
