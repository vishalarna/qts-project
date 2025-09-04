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
    public class Task_CollaboratorInvitationAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Task_CollaboratorInvitation>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Task_CollaboratorInvitation resource)
        {
            if (requirement.Name == Task_CollaboratorInvitationOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_CollaboratorInvitationOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_CollaboratorInvitationOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Task_CollaboratorInvitationOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
