using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_TrainingProgramAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_TrainingProgram>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_TrainingProgram resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_TrainingProgramOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_TrainingProgramOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_TrainingProgramOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_TrainingProgramOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}