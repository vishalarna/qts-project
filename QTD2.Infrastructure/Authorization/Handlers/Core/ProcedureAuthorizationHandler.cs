using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class ProcedureAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Domain.Entities.Core.Procedure>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Domain.Entities.Core.Procedure resource)
        {
            context.Succeed(requirement);

            if (requirement.Name == ProcedureOperations.Create.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == ProcedureOperations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == ProcedureOperations.Update.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            if (requirement.Name == ProcedureOperations.Delete.Name)
            {

                context.Succeed(requirement);
                return Task.CompletedTask;

            }

            return Task.CompletedTask;
        }
    }
}
