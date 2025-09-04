using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class Version_Procedure_Tool_LinkAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Version_Procedure_Tool_Link>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Version_Procedure_Tool_Link resource)
        {
            context.Succeed(requirement);
            if (requirement.Name == Version_Procedure_Tool_LinkOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == Version_Procedure_Tool_LinkOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_Procedure_Tool_LinkOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Version_Procedure_Tool_LinkOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
