using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_Task_StepAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_Task_Step>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_Task_Step resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_Task_StepOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_Task_StepOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_Task_StepOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_Task_StepOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
