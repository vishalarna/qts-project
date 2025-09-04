using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Authorization.Operations.Authentication;
using Microsoft.AspNetCore.Identity;

namespace QTD2.Infrastructure.Authorization.Handlers.Authentication
{
    public class InstanceAuthorizationHandler : AuthenticationHandler<OperationAuthorizationRequirement, Instance>
    {
        //private readonly UserManager<AppUser> _userManager;

        //public InstanceAuthorizationHandler(UserManager<AppUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Instance resource)
        {
            if (context.User.Claims.Where(x => x.Type == CustomClaimTypes.IsAdmin).Any())
            {
                context.Succeed(requirement);
                return;
            }

            //commented because claims are not having instances

            if (requirement.Name == InstanceOperations.SetInstance.Name)
            {
                //TODO check if we have the DB claims here
                
                //var user = await _userManager.GetUserAsync(context.User);
                //var claims = await _userManager.GetClaimsAsync(user);

                //if (context.User.Claims.Instances.Where(r => r.Name == resource.Name).count() > 0
                //        && context.User.Claims.Instances.Where(r => r.Name == resource.Name).Value == true)
                //{
                //    context.Succeed(requirement);
                //} else if(context.User.Claims.Where(r => r.IsAdmin == true && r.ImpersonatingUser != null){
                //context.Succeed(requirement);
                //}
            }

            if (requirement.Name == InstanceOperations.Create.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == InstanceOperations.CreateDatabase.Name)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == InstanceOperations.Read.Name)
            {
                context.Succeed(requirement);
            }
        }
    }
}
