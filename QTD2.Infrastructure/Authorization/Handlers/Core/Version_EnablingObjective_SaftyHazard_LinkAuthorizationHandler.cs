using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_EnablingObjective_SaftyHazard_LinkAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_EnablingObjective_SaftyHazard_Link>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_EnablingObjective_SaftyHazard_Link resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_EnablingObjective_Procedure_LinkOperations.Create.Name)
            {
                
                    context.Succeed(requirement);
                    return System.Threading.Tasks.Task.CompletedTask;
                
            }

            if (requirement.Name == Version_EnablingObjective_SaftyHazard_LinkOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_EnablingObjective_Procedure_LinkOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_EnablingObjective_Procedure_LinkOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
