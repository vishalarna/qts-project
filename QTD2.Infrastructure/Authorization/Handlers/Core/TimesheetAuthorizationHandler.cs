using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Handlers.Core
{
    public class TimesheetAuthorizationHandler : QTDHandler<OperationAuthorizationRequirement, Timesheet>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Timesheet resource)
        {
            if (requirement.Name == TimesheetOperations.Create.Name)
            {

                context.Succeed(requirement);
                return System.Threading.Tasks.Task.CompletedTask;

            }

            if (requirement.Name == TimesheetOperations.Read.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TimesheetOperations.Update.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == TimesheetOperations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
